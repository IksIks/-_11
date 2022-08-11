using System;
using ДЗ_11.Models.AbstractModels;
using ДЗ_11.Services;

namespace ДЗ_11.Models
{
    internal class DepositAccount : BaseAccount
    {
        private uint balanceRUB_Account;

        public double DepositPercent { get; set; }
        public override DateTime DateOfCreation { get ; set ; }
        public override DateTime DateOfClose { get ; set ; }
        public override uint BalanceRUB_Account
        {
            get => balanceRUB_Account;
            set => Set(ref balanceRUB_Account, value);
        }
        //public override Cash Currency { get ; set ; }

        public DepositAccount() : base()
        {
            balanceRUB_Account = 0;
            DepositPercent = 20;
        }
        public DepositAccount(DateTime dateOfCreation, double depositPercent, uint balanceRUB_Account, DateTime dateOfClose)
        {
            DateOfCreation = dateOfCreation;
            DepositPercent = depositPercent;
            this.balanceRUB_Account = balanceRUB_Account;
            DateOfClose = dateOfClose;
        }
        public override string ToString()
        {
            return $"Кредитный счет {DateOfCreation} {DepositPercent} {BalanceRUB_Account} {DateOfClose}";
        }
    }
}
