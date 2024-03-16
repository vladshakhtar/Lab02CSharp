using System;

namespace Lab02Stoliarov
{
    public class Person
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string EmailAddress { get; }
        public DateTime DateOfBirth { get; }

        public Person(string firstName, string lastName, string emailAddress, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            DateOfBirth = dateOfBirth;
        }

        public Person(string firstName, string lastName, string emailAddress)
            : this(firstName, lastName, emailAddress, DateTime.MinValue)
        {
        }

        public Person(string firstName, string lastName, DateTime dateOfBirth)
            : this(firstName, lastName, null, dateOfBirth)
        {
        }

        public bool IsAdult => CalculateAge() >= 18;

        public string SunSign => CalculateSunSign();

        public string ChineseSign => CalculateChineseSign();

        public bool IsBirthday => DateTime.Today.Month == DateOfBirth.Month &&
                                  DateTime.Today.Day == DateOfBirth.Day;

        public int CalculateAge()
        {
            var today = DateTime.Today;
            var age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }

        private string CalculateSunSign()
        {
            int day = DateOfBirth.Day;
            switch (DateOfBirth.Month)
            {
                case 1: return day <= 19 ? "Capricorn" : "Aquarius";
                case 2: return day <= 18 ? "Aquarius" : "Pisces";
                case 3: return day <= 20 ? "Pisces" : "Aries";
                case 4: return day <= 19 ? "Aries" : "Taurus";
                case 5: return day <= 20 ? "Taurus" : "Gemini";
                case 6: return day <= 20 ? "Gemini" : "Cancer";
                case 7: return day <= 22 ? "Cancer" : "Leo";
                case 8: return day <= 22 ? "Leo" : "Virgo";
                case 9: return day <= 22 ? "Virgo" : "Libra";
                case 10: return day <= 22 ? "Libra" : "Scorpio";
                case 11: return day <= 21 ? "Scorpio" : "Sagittarius";
                case 12: return day <= 21 ? "Sagittarius" : "Capricorn";
                default: throw new ArgumentOutOfRangeException(nameof(DateOfBirth), "Invalid month");
            }
        }

        private string CalculateChineseSign()
        {
            string[] signs = { "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig" };

            int cycleStartYear = 1900; 
            int yearDifference = DateOfBirth.Year - cycleStartYear;

            int adjustedIndex = (yearDifference % 12 + 12) % 12;

            return signs[adjustedIndex];
        }

    }
}
