using System;
using ДЗ_11.Models.AbstractModels;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.Models
{
    internal class DepositAccount : NonDepositAccount
    {
       
        public bool DepositInsurance { get; set; }
        public float DepositPercent { get; set; }

        public DepositAccount() : base()
        {
            
        }
    }
}
