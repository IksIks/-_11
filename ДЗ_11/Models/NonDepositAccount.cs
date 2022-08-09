using System;
using ДЗ_11.Models.AbstractModels;
using ДЗ_11.Services;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.Models
{
    internal class NonDepositAccount : ViewModel
    {
        private int balance;
        private Cash currency;

        //public Guid Id { get; }
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfClose { get; set; }
        public int Balance
        {
            get => balance;
            set => Set(ref balance, value);
        }
        public Cash Currency
        {
            get => currency;
            set => Set(ref currency, value);
        }

        

        public NonDepositAccount()
        {
            //Id = Guid.NewGuid();
            DateOfCreation = DateTime.Now;
            balance = 0;
        }

    }
}
