namespace PayRemind
{
    public partial class MainPage : TabbedPage
    {

        private DateTime _selectedDateTime;
        public MainPage()
        {
            InitializeComponent();
            myDatePicker.MinimumDate = new DateTime(2000, 1, 1);
            myDatePicker.MaximumDate = DateTime.Today;
        }

        private void OnDateSelected(object sender, DateChangedEventArgs e)
        {
            // Aquí puedes manejar el cambio de fecha
            DateTime selectedDate = e.NewDate;
            // Haz algo con la fecha seleccionada
        }

        private void OnTimeChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Time")
            {
                _selectedDateTime = _selectedDateTime.Date + timePicker.Time;
                UpdateDateTimeLabel();
            }
        }

        private void UpdateDateTimeLabel()
        {
            selectedDateTime.Text = $"Selected: {_selectedDateTime:g}";
        }
    }
}
