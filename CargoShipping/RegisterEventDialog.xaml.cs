using System.Windows;
using System.Windows.Controls;

namespace CargoShipping
{
    public partial class RegisterEventDialog : Window
    {
        public string? EventData { get; private set; }
        public string? TrackingId { get; private set; }
        public string? EventType { get; private set; }
        public string? Location { get; private set; }
        public DateTime? CompletionTime { get; private set; }

        private readonly List<string> _trackingIds;
        private readonly List<LocationViewModel> _locations;

        public RegisterEventDialog(List<string> trackingIds, List<LocationViewModel> locations)
        {
            InitializeComponent();
            _trackingIds = trackingIds;
            _locations = locations;
            
            // Populate ComboBoxes
            TrackingIdComboBox.ItemsSource = _trackingIds;
            LocationComboBox.ItemsSource = _locations;
            
            // Set default completion time to today
            CompletionDatePicker.SelectedDate = DateTime.Now;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var trackingId = TrackingIdComboBox.Text.Trim();
                var eventTypeText = (EventTypeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
                var location = LocationComboBox.SelectedItem as LocationViewModel;
                var completionDate = CompletionDatePicker.SelectedDate;

                if (string.IsNullOrEmpty(trackingId))
                {
                    MessageBox.Show("Please enter or select a tracking ID.", "Validation Error", 
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(eventTypeText))
                {
                    MessageBox.Show("Please select an event type.", "Validation Error", 
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (location == null)
                {
                    MessageBox.Show("Please select a location.", "Validation Error", 
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!completionDate.HasValue)
                {
                    MessageBox.Show("Please select a completion time.", "Validation Error", 
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                TrackingId = trackingId;
                EventType = eventTypeText;
                Location = location.Name;
                CompletionTime = completionDate.Value;
                EventData = $"{eventTypeText} for {trackingId} at {location.Name}";

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error registering event: {ex.Message}", "Error", 
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}