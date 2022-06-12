using System;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.Models
{
    internal class NonDepositeAccount : ViewModel
    {
        private int balance;
        public  Guid Id { get; }
        public  DateTime DateOfCreation { get; set; }
        public  DateTime DateOfClose { get; set; }
        public int Balance
        {
            get => balance;
            set => Set(ref balance, value);
        }

        public Cash Currency { get; set; }

        public enum Cash
        {
            RUB,
            USD,
            EURO
        }
        public NonDepositeAccount()
        {
            Id = Guid.NewGuid();
            DateOfCreation = DateTime.Now;
            Balance = 0;
        }
    }
}
