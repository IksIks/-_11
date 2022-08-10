using System;
using ДЗ_11.Models.AbstractModels;
using ДЗ_11.Services;

namespace ДЗ_11.Models
{
    internal class DepositAccount : BaseAccount
    {
        private uint balanceRUB_Account;

        public bool DepositInsurance { get; set; }
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
    }
}
