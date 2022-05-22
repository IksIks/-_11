using System;
using System.Collections.Generic;
using ДЗ_11.ViewModels;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.Models
{
    internal class Client : ViewModel
    {
        #region Поля
        private Guid id;
        private string name;
        private string lastName;
        private string patronymic;
        private string phoneNumber;
        private string passport;
        //private DateTime dateClientChange;

        #endregion
        #region Свойства
        public Guid Id
        {
            get => id;
        }

        public string Name
        {
            get => name;
            set
            {
                if (RuleChoiseViewModel.CanSeeOrChangeText)
                    Set(ref name, value);
                else
                    Set(ref name, name);
            }
        }

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

        public string PhoneNumber
        {
            get => phoneNumber;
            set => Set(ref phoneNumber, value);
        }

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
        public DateTime DateClientChange { get; set; }
        //{
        //    get => dateClientChange;
        //    set => Set(ref dateClientChange, value);
        //}

        /// <summary>
        /// Словарь для перевода свойств в кирилицу
        /// </summary>
        public Dictionary<string, string> ClientPropertyTranslater { get; } = new Dictionary<string, string>()
        {
            {"Name", "Имя"},
            {"LastName", "Фамилия"},
            {"Patronymic", "Отчество"},
            {"PhoneNumber", "Телефон"},
            {"Passport", "Паспорт"},
            {"DateClientChange", "Дата изменения клиента"}
        };
        #endregion

        public Client()
        {
            id = Guid.NewGuid();
            DateClientChange = DateTime.Now;
        }

        public Client(string name, string lastName, string patronymic, string phoneNumber, string passportNumber)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.lastName = lastName;
            this.patronymic = patronymic;
            this.phoneNumber = phoneNumber;
            this.passport = passportNumber;
            DateClientChange = DateTime.Now;
        }
    }
    
}
