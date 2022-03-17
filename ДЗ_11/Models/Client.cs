using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ДЗ_11.Models
{
    internal class Client
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public uint PhoneNumber { get; set; }
        public Passport PassportNumber { get; set; }

        public Client(string name, string lastName, string patronymic, uint phoneNumber, Passport passportNumber)
        {
            Id = Guid.NewGuid();
            Name = name;
            LastName = lastName;
            Patronymic = patronymic;
            PhoneNumber = phoneNumber;
            PassportNumber = passportNumber;
        }
    }

    internal class Passport
    {
        public ushort Series { get; set; }
        public uint Number { get; set; }
        public Passport(ushort series, uint number)
        {
            Series = series;
            Number = number;
        }
    }
}
