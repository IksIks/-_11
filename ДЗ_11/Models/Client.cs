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
        private string name = default;
        private string lastName = default;
        private string patronymic = default;
        private string phoneNumber = default;
        private string passport = default;

        
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
                if (RoleChoiseViewModel.ManadgerRole)
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
                if (RoleChoiseViewModel.ManadgerRole)
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
                if (RoleChoiseViewModel.ManadgerRole)
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
                if (!RoleChoiseViewModel.ManadgerRole)
                {
                    return new String('*', 10);
                }
                return passport;
            }
            set
            {
                if (RoleChoiseViewModel.ManadgerRole)
                    Set(ref passport, value);
                else
                    Set(ref passport, passport);
            }
        }

        public NonDepositAccount NonDepositAccount { get; set; }
        public DepositAccount DepositAccount { get; set; }
        public DateTime DateClientChange { get; set; }

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

        #region Конструкторы
        public Client()
        {
            id = Guid.NewGuid();
            DateClientChange = DateTime.Now;
            NonDepositAccount = new NonDepositAccount();
            DepositAccount = new DepositAccount();
        }

        public Client(string lastName, string name, string patronymic, string phoneNumber, string passportNumber,
                      NonDepositAccount nonDepositAccount, DepositAccount depositAccount)
        {
            this.id = Guid.NewGuid();
            this.lastName = lastName;
            this.name = name;
            this.patronymic = patronymic;
            this.phoneNumber = phoneNumber;
            this.passport = passportNumber;
            DateClientChange = DateTime.Now;
            NonDepositAccount = nonDepositAccount;
            DepositAccount = depositAccount;
        }
        /// <summary>
        /// Конструктор при загрузки базы клиентов из файла
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lastName"></param>
        /// <param name="name"></param>
        /// <param name="patronymic"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="passportNumber"></param>
        /// <param name="dateClientChange"></param>
        public Client(Guid id, string lastName, string name, string patronymic,
                      string phoneNumber, string passportNumber, DateTime dateClientChange, NonDepositAccount nonDepositAccount, DepositAccount depositAccount)
                      : this(lastName, name, patronymic, phoneNumber, passportNumber, nonDepositAccount, depositAccount)
        {
            this.id = id;
            DateClientChange = dateClientChange;
        }
        #endregion

        public override string ToString()
        {
            return $"{Id} {LastName} {Name} {Patronymic} {PhoneNumber} {Passport} {DateClientChange} {NonDepositAccount} {DepositAccount}";
        }
    }

}
