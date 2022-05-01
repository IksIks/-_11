using System;
using ДЗ_11.ViewModels;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.Models
{
    internal class Client : ViewModel
    {
        
        private Guid id;
        public Guid Id
        {
            get => id;
            //set => Set(ref id, value);
        }


        private string name;
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set => Set(ref lastName, value);
        }

        private string patronymic;
        public string Patronymic
        {
            get => patronymic;
            set => Set(ref patronymic, value);
        }

        private uint phoneNumber;
        public uint PhoneNumber
        {
            get => phoneNumber;
            set => Set(ref phoneNumber, value);
        }

        public Passport PassportNumber { get; set; }

        public Client(string name, string lastName, string patronymic, uint phoneNumber, Passport passportNumber)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.lastName = lastName;
            this.patronymic = patronymic;
            this.phoneNumber = phoneNumber;
            this.PassportNumber = passportNumber;
        }
    }

    internal class Passport: ViewModel
    {
        private string series;
        public string Series
        {
            get
            {
                if (!RuleChoiseViewModel.canSeeText)
                {
                    return new String('*', 4);
                }
                return series;
            }
            set => Set(ref series, value);
        }

        private string number;
        public string Number
        {
            get
            {
                if (!RuleChoiseViewModel.canSeeText)
                {
                    return new String('*', 6);
                }
                return number;
            }
            set => Set(ref number, value);
        }


        public Passport(string series, string number)
        {
            this.series = series;
            this.number = number;
        }
        public override string ToString()
        {
            return $"{Series} {Number}";
        }
    }
}
