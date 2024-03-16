using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Lab02Stoliarov
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private string firstName;
        private string lastName;
        private string emailAddress;
        private DateTime? dateOfBirth;
        private bool isProcessing = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public string FirstName
        {
            get => firstName;
            set { firstName = value; OnPropertyChanged(nameof(FirstName)); OnPropertyChanged(nameof(CanProceed)); }
        }

        public string LastName
        {
            get => lastName;
            set { lastName = value; OnPropertyChanged(nameof(LastName)); OnPropertyChanged(nameof(CanProceed)); }
        }

        public string EmailAddress
        {
            get => emailAddress;
            set { emailAddress = value; OnPropertyChanged(nameof(EmailAddress)); OnPropertyChanged(nameof(CanProceed)); }
        }

        public DateTime? DateOfBirth
        {
            get => dateOfBirth;
            set { dateOfBirth = value; OnPropertyChanged(nameof(DateOfBirth)); OnPropertyChanged(nameof(CanProceed)); }
        }

        public bool IsProcessing
        {
            get => isProcessing;
            set { isProcessing = value; OnPropertyChanged(nameof(IsProcessing)); }
        }

        // This property will enable or disable the Proceed button
        public bool CanProceed => !IsProcessing && !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName) && !string.IsNullOrWhiteSpace(EmailAddress) && DateOfBirth.HasValue;

        public ICommand ProceedCommand { get; }

        public PersonViewModel()
        {
            ProceedCommand = new RelayCommand(async (o) => await ProceedAsync(), (o) => CanProceed);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task ProceedAsync()
        {
            IsProcessing = true;

            await Task.Delay(100); // Short delay to ensure UI updates

            if (DateOfBirth.HasValue)
            {
                var person = new Person(FirstName, LastName, EmailAddress, DateOfBirth.Value);
                int age = person.CalculateAge();

                // Check for future birthdate
                if (person.DateOfBirth > DateTime.Now)
                {
                    MessageBox.Show("Date of birth cannot be in the future.", "Invalid Date of Birth", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                // Corrected age validation logic: Check for unrealistic age
                else if (age < 0 || age > 135)
                {
                    MessageBox.Show("The age calculated is unrealistically high or negative. Please enter a valid date of birth.", "Invalid Age", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    // Construct the message to display all details
                    string message = $"Full Name: {person.FirstName} {person.LastName}\n" +
                                     $"Email Address: {person.EmailAddress}\n" +
                                     $"Date of Birth: {person.DateOfBirth.ToShortDateString()}\n" +
                                     $"Is Adult: {person.IsAdult}\n" +
                                     $"Sun Sign: {person.SunSign}\n" +
                                     $"Chinese Sign: {person.ChineseSign}\n" +
                                     $"Is Birthday: {person.IsBirthday}";

                    MessageBox.Show(message, "Person Information", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Check for birthday
                    if (person.IsBirthday)
                    {
                        MessageBox.Show("Happy Birthday!", "Birthday Greeting", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }

            IsProcessing = false;
        }


    }
}
