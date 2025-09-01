using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace CargoShipping
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Simplified demo data
        private readonly ObservableCollection<CargoViewModel> _cargoList = new();
        private readonly ObservableCollection<HandlingEventViewModel> _eventsList = new();
        private readonly ObservableCollection<LocationViewModel> _locations = new();
        private readonly ObservableCollection<VoyageViewModel> _voyages = new();
        
        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
            SetupDataBindings();
        }

        private void InitializeData()
        {
            // Sample Locations
            _locations.Add(new LocationViewModel { Code = "DEHAM", Name = "Hamburg" });
            _locations.Add(new LocationViewModel { Code = "NLRTM", Name = "Rotterdam" });
            _locations.Add(new LocationViewModel { Code = "USNYC", Name = "New York" });
            _locations.Add(new LocationViewModel { Code = "JPTYO", Name = "Tokyo" });

            // Sample Voyages
            _voyages.Add(new VoyageViewModel { VoyageNumber = "V001" });
            _voyages.Add(new VoyageViewModel { VoyageNumber = "V002" });

            // Sample Cargo
            _cargoList.Add(new CargoViewModel 
            { 
                TrackingId = "CARGO001",
                Origin = "Hamburg",
                Destination = "New York",
                Status = "IN_PORT",
                CurrentLocation = "Hamburg",
                Eta = DateTime.Now.AddDays(30).ToString("dd.MM.yyyy"),
                Misdirected = false
            });

            _cargoList.Add(new CargoViewModel 
            { 
                TrackingId = "CARGO002",
                Origin = "Rotterdam",
                Destination = "Tokyo",
                Status = "ONBOARD_CARRIER",
                CurrentLocation = "At Sea",
                Eta = DateTime.Now.AddDays(15).ToString("dd.MM.yyyy"),
                Misdirected = false
            });

            // Sample Events
            _eventsList.Add(new HandlingEventViewModel 
            { 
                EventId = Guid.NewGuid().ToString(),
                TrackingId = "CARGO001",
                Type = "RECEIVE",
                Location = "Hamburg",
                CompletionTime = DateTime.Now.AddDays(-5).ToString("dd.MM.yyyy HH:mm"),
                RegistrationTime = DateTime.Now.AddDays(-5).ToString("dd.MM.yyyy HH:mm")
            });

            StatusTextBlock.Text = "Sample data loaded";
        }

        private void SetupDataBindings()
        {
            CargoDataGrid.ItemsSource = _cargoList;
            EventsDataGrid.ItemsSource = _eventsList;
            LocationsListBox.ItemsSource = _locations;
            VoyagesListBox.ItemsSource = _voyages;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var trackingId = TrackingIdTextBox.Text.Trim();
            if (string.IsNullOrEmpty(trackingId))
            {
                MessageBox.Show("Please enter a tracking ID.", "Validation Error", 
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var cargo = _cargoList.FirstOrDefault(c => c.TrackingId == trackingId);
            if (cargo != null)
            {
                DisplayCargoDetails(cargo);
                StatusTextBlock.Text = $"Found cargo: {trackingId}";
            }
            else
            {
                MessageBox.Show($"Cargo with tracking ID '{trackingId}' not found.", 
                              "Search Result", MessageBoxButton.OK, MessageBoxImage.Information);
                StatusTextBlock.Text = $"Cargo not found: {trackingId}";
            }
        }

        private void DisplayCargoDetails(CargoViewModel cargo)
        {
            CargoDetailsPanel.Children.Clear();
            
            var detailsStack = new StackPanel();
            
            detailsStack.Children.Add(new TextBlock 
            { 
                Text = $"Tracking ID: {cargo.TrackingId}", 
                FontWeight = FontWeights.Bold, 
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 10)
            });
            
            detailsStack.Children.Add(new TextBlock { Text = $"Origin: {cargo.Origin}" });
            detailsStack.Children.Add(new TextBlock { Text = $"Destination: {cargo.Destination}" });
            detailsStack.Children.Add(new TextBlock { Text = $"Status: {cargo.Status}" });
            detailsStack.Children.Add(new TextBlock { Text = $"Current Location: {cargo.CurrentLocation}" });
            detailsStack.Children.Add(new TextBlock { Text = $"ETA: {cargo.Eta}" });
            detailsStack.Children.Add(new TextBlock 
            { 
                Text = $"Misdirected: {(cargo.Misdirected ? "Yes" : "No")}", 
                Foreground = cargo.Misdirected ? System.Windows.Media.Brushes.Red : System.Windows.Media.Brushes.Green 
            });
            
            CargoDetailsPanel.Children.Add(detailsStack);
        }

        private void CreateCargoButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CreateCargoDialog(_locations.ToList());
            if (dialog.ShowDialog() == true)
            {
                var cargoId = dialog.CreatedCargoId;
                if (!string.IsNullOrEmpty(cargoId))
                {
                    _cargoList.Add(new CargoViewModel 
                    { 
                        TrackingId = cargoId,
                        Origin = dialog.OriginLocation ?? "Unknown",
                        Destination = dialog.DestinationLocation ?? "Unknown",
                        Status = "NOT_RECEIVED",
                        CurrentLocation = dialog.OriginLocation ?? "Unknown",
                        Eta = dialog.ArrivalDeadline?.ToString("dd.MM.yyyy") ?? "Not set",
                        Misdirected = false
                    });
                    StatusTextBlock.Text = $"Created new cargo: {cargoId}";
                }
            }
        }

        private void RegisterEventButton_Click(object sender, RoutedEventArgs e)
        {
            var trackingIds = _cargoList.Select(c => c.TrackingId).ToList();
            var dialog = new RegisterEventDialog(trackingIds, _locations.ToList());
            if (dialog.ShowDialog() == true)
            {
                var eventData = dialog.EventData;
                if (!string.IsNullOrEmpty(eventData))
                {
                    // Add event to the events list
                    _eventsList.Add(new HandlingEventViewModel 
                    { 
                        EventId = Guid.NewGuid().ToString(),
                        TrackingId = dialog.TrackingId ?? "",
                        Type = dialog.EventType ?? "",
                        Location = dialog.Location ?? "",
                        CompletionTime = dialog.CompletionTime?.ToString("dd.MM.yyyy HH:mm") ?? "",
                        RegistrationTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm")
                    });

                    // Update cargo status if applicable
                    var cargo = _cargoList.FirstOrDefault(c => c.TrackingId == dialog.TrackingId);
                    if (cargo != null && dialog.EventType != null)
                    {
                        cargo.CurrentLocation = dialog.Location ?? cargo.CurrentLocation;
                        cargo.Status = dialog.EventType switch
                        {
                            "RECEIVE" => "IN_PORT",
                            "LOAD" => "ONBOARD_CARRIER", 
                            "UNLOAD" => "IN_PORT",
                            "CLAIM" => "CLAIMED",
                            "CUSTOMS" => "IN_PORT",
                            _ => cargo.Status
                        };
                    }

                    StatusTextBlock.Text = $"Registered event: {eventData}";
                }
            }
        }

        private void ViewAllButton_Click(object sender, RoutedEventArgs e)
        {
            // Switch to the cargo list tab (simplified - just show message)
            MessageBox.Show("Switching to Cargo List view", "Info", 
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RefreshListButton_Click(object sender, RoutedEventArgs e)
        {
            StatusTextBlock.Text = "Cargo list refreshed";
        }

        private void CargoDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CargoDataGrid.SelectedItem is CargoViewModel selectedCargo)
            {
                DisplayCargoDetails(selectedCargo);
            }
        }

        private void RegisterEventSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var trackingId = EventTrackingIdTextBox.Text.Trim();
                var eventTypeText = (EventTypeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
                var location = EventLocationTextBox.Text.Trim();
                var completionDate = EventDatePicker.SelectedDate;

                if (string.IsNullOrEmpty(trackingId) || string.IsNullOrEmpty(eventTypeText) || 
                    string.IsNullOrEmpty(location) || !completionDate.HasValue)
                {
                    MessageBox.Show("Please fill in all fields.", "Validation Error", 
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _eventsList.Add(new HandlingEventViewModel 
                { 
                    EventId = Guid.NewGuid().ToString(),
                    TrackingId = trackingId,
                    Type = eventTypeText,
                    Location = location,
                    CompletionTime = completionDate.Value.ToString("dd.MM.yyyy HH:mm"),
                    RegistrationTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm")
                });

                // Clear form
                EventTrackingIdTextBox.Clear();
                EventTypeComboBox.SelectedIndex = -1;
                EventLocationTextBox.Clear();
                EventDatePicker.SelectedDate = null;

                StatusTextBlock.Text = $"Event registered successfully for {trackingId}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error registering event: {ex.Message}", "Error", 
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddLocationButton_Click(object sender, RoutedEventArgs e)
        {
            var code = NewLocationCodeTextBox.Text.Trim();
            var name = NewLocationNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter both location code and name.", "Validation Error", 
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_locations.Any(l => l.Code == code))
            {
                MessageBox.Show("Location with this code already exists.", "Validation Error", 
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _locations.Add(new LocationViewModel { Code = code, Name = name });
            NewLocationCodeTextBox.Clear();
            NewLocationNameTextBox.Clear();
            StatusTextBlock.Text = $"Added location: {code} - {name}";
        }

        private void AddVoyageButton_Click(object sender, RoutedEventArgs e)
        {
            var voyageNumber = NewVoyageNumberTextBox.Text.Trim();

            if (string.IsNullOrEmpty(voyageNumber))
            {
                MessageBox.Show("Please enter a voyage number.", "Validation Error", 
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_voyages.Any(v => v.VoyageNumber == voyageNumber))
            {
                MessageBox.Show("Voyage with this number already exists.", "Validation Error", 
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _voyages.Add(new VoyageViewModel { VoyageNumber = voyageNumber });
            NewVoyageNumberTextBox.Clear();
            StatusTextBlock.Text = $"Added voyage: {voyageNumber}";
        }
    }

    // Simplified View Models
    public class CargoViewModel
    {
        public string TrackingId { get; set; } = "";
        public string Origin { get; set; } = "";
        public string Destination { get; set; } = "";
        public string Status { get; set; } = "";
        public string CurrentLocation { get; set; } = "";
        public string Eta { get; set; } = "";
        public bool Misdirected { get; set; }
    }

    public class HandlingEventViewModel
    {
        public string EventId { get; set; } = "";
        public string TrackingId { get; set; } = "";
        public string Type { get; set; } = "";
        public string Location { get; set; } = "";
        public string CompletionTime { get; set; } = "";
        public string RegistrationTime { get; set; } = "";
    }

    public class LocationViewModel
    {
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public override string ToString() => $"{Code} - {Name}";
    }

    public class VoyageViewModel
    {
        public string VoyageNumber { get; set; } = "";
        public override string ToString() => VoyageNumber;
    }
}