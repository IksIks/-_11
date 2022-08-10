using System;
using ДЗ_11.Models.AbstractModels;
using ДЗ_11.Services;

namespace ДЗ_11.Models
{
    internal class NonDepositAccount : BaseAccount
    {    
        private uint balanceRUB_Account;
        private uint balanceUSD_Account;
        private uint balanceEURO_Account;
        //private Cash currency;

        
        public override DateTime DateOfCreation { get; set; }
        public override DateTime DateOfClose { get; set; }

        public override uint BalanceRUB_Account
        {
            get => balanceRUB_Account;
            set => Set(ref balanceRUB_Account, value);
        }
        public uint BalanceUSD_Account
        {
            get => balanceUSD_Account;
            set => Set(ref balanceUSD_Account, value);
        }
        public uint BalanceEURO_Account
        {
            get => balanceEURO_Account;
            set => Set(ref balanceEURO_Account, value);
        }
        //public override Cash Currency
        //{
        //    get => currency;
        //    set => Set(ref currency, value);
        //}

        public NonDepositAccount() : base()
        {            
            balanceRUB_Account = 0;
            balanceUSD_Account = 0;
            BalanceEURO_Account = 0;
        }

    }
}
