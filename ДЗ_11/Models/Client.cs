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
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                if(RuleChoiseViewModel.CanSeeOrChangeText)
                    Set(ref name, value);
                else
                    Set(ref name, name);
            }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                if (RuleChoiseViewModel.CanSeeOrChangeText)
                    Set(ref lastName, value);
                else
                    Set(ref lastName, lastName);
            }
        }

        private string patronymic;
        public string Patronymic
        {
            get => patronymic;
            set
            {
                if (RuleChoiseViewModel.CanSeeOrChangeText)
                    Set(ref patronymic, value);
                else
                    Set(ref patronymic, patronymic);
            }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get => phoneNumber;
            set => Set(ref phoneNumber, value);
        }

        private string passport;
        public string Passport
        {
            get
            {
                if (!RuleChoiseViewModel.CanSeeOrChangeText)
                {
                    return new String('*', 10);
                }
                return passport;
            }
            set
            {
                if (RuleChoiseViewModel.CanSeeOrChangeText)
                    Set(ref passport, value);
                else
                    Set(ref passport, passport);
            }
        }

        public Client()
        {
            id = Guid.NewGuid();
        }
        public Client(Guid id)
        {
            this.id = id;
        }

        public Client(string name, string lastName, string patronymic, string phoneNumber, string passportNumber)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.lastName = lastName;
            this.patronymic = patronymic;
            this.phoneNumber = phoneNumber;
            this.passport = passportNumber;
        }
    }

    //internal class Passport: ViewModel
    //{
    //    private string series;
    //    public string Series
    //    {
    //        get
    //        {
    //            if (!RuleChoiseViewModel.CanSeeOrChangeText)
    //            {
    //                return new String('*', 4);
    //            }
    //            return series;
    //        }
    //        set
    //        {
    //            if (RuleChoiseViewModel.CanSeeOrChangeText)
    //                Set(ref series, value);
    //            else
    //                Set(ref series, series);
    //        }
    //    }

    //    private string number;
    //    public string Number
    //    {
    //        get
    //        {
    //            if (!RuleChoiseViewModel.CanSeeOrChangeText)
    //            {
    //                return new String('*', 6);
    //            }
    //            return number;
    //        }
    //        set
    //        {
    //            if (RuleChoiseViewModel.CanSeeOrChangeText)
    //                Set(ref number, value);
    //            else
    //                Set(ref number, number);
    //        }
    //    }

        //public Passport() { }
        //public Passport(string series, string number)
        //{
        //    this.series = series;
        //    this.number = number;
        //}
        //public override string ToString()
        //{
        //    return $"{Series} {Number}";
        //}
    //}
}
