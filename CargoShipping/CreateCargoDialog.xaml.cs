using System.Windows;

namespace CargoShipping
{
    public partial class CreateCargoDialog : Window
    {
        public string? CreatedCargoId { get; private set; }
        public string? OriginLocation { get; private set; }
        public string? DestinationLocation { get; private set; }
        public DateTime? ArrivalDeadline { get; private set; }

        private readonly List<LocationViewModel> _locations;

        public CreateCargoDialog(List<LocationViewModel> locations)
        {
            InitializeComponent();
            _locations = locations;
            
            // Populate ComboBoxes
            OriginComboBox.ItemsSource = _locations;
            DestinationComboBox.ItemsSource = _locations;
            
            // Set default deadline to 30 days from now
            DeadlineDatePicker.SelectedDate = DateTime.Now.AddDays(30);
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var trackingId = TrackingIdTextBox.Text.Trim();
                var origin = OriginComboBox.SelectedItem as LocationViewModel;
                var destination = DestinationComboBox.SelectedItem as LocationViewModel;
                var deadline = DeadlineDatePicker.SelectedDate;

                if (string.IsNullOrEmpty(trackingId))
                {
                    MessageBox.Show("Please enter a tracking ID.", "Validation Error", 
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (origin == null)
                {
                    MessageBox.Show("Please select an origin location.", "Validation Error", 
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (destination == null)
                {
                    MessageBox.Show("Please select a destination location.", "Validation Error", 
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (origin.Code == destination.Code)
                {
                    MessageBox.Show("Origin and destination cannot be the same.", "Validation Error", 
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!deadline.HasValue || deadline.Value <= DateTime.Now)
                {
                    MessageBox.Show("Please select a valid future deadline.", "Validation Error", 
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                CreatedCargoId = trackingId;
                OriginLocation = origin.Name;
                DestinationLocation = destination.Name;
                ArrivalDeadline = deadline.Value;

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating cargo: {ex.Message}", "Error", 
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}