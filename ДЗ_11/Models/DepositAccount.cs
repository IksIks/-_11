using System;
using ДЗ_11.Models.AbstractModels;

namespace ДЗ_11.Models
{
    internal class DepositAccount : BaseAccount
    {
        public bool DepositNotExist { get; set; }
        public double DepositPercent { get; set; }
        public override DateTime DateOfCreation { get; set; }
        public override DateTime DateOfClose { get; set; }
        public override double BalanceRUB_Account
        {
            get => balanceRUB_Account;
            set => Set(ref balanceRUB_Account, value);
        }

        private double balanceRUB_Account;

        public DepositAccount() : base()
        {
            balanceRUB_Account = 0;
            DepositPercent = 5.6;
            DepositNotExist = true;
        }
        public DepositAccount(DateTime dateOfCreation, double depositPercent, double balanceRUB_Account, DateTime dateOfClose, bool depositNotExist)
        {
            DateOfCreation = dateOfCreation;
            DepositPercent = depositPercent;
            this.balanceRUB_Account = balanceRUB_Account;
            DateOfClose = dateOfClose;
            DepositNotExist = depositNotExist;
        }
        public override string ToString()
        {
            return $"Депозитный счет {DateOfCreation} {DepositPercent} {BalanceRUB_Account} {DateOfClose} {DepositNotExist}";
        }
    }
}
