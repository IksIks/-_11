using System;
using ДЗ_11.Models.AbstractModels;
using ДЗ_11.Services;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.Models
{
    internal class NonDepositAccount : ViewModel
    {    private int balance;
        private Cash currency;

        
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
            DateOfCreation = DateTime.Now;
            balance = 0;
        }

    }
}
