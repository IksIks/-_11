using System;
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

        public Passport passportNumber { get; set; }

        public Client(string name, string lastName, string patronymic, uint phoneNumber, Passport passportNumber)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.lastName = lastName;
            this.patronymic = patronymic;
            this.phoneNumber = phoneNumber;
            this.passportNumber = passportNumber;
        }
    }

    internal class Passport: ViewModel
    {
        private ushort series;
        public ushort Series
        {
            get => series;
            set => Set(ref series, value);
        }

        private uint number;
        public uint Number
        {
            get => number;
            set => Set(ref number, value);
        }

        public Passport(ushort series, uint number)
        {
            this.series = series;
            this.number = number;
        }
    }
}
