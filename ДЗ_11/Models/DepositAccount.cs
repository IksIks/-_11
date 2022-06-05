using System;
using ДЗ_11.Models.AbstractModels;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.Models
{
    internal class DepositAccount : BaseAccount
    {

        public bool DepositInsurance { get; set; }
        public Cash Currency { get; set; }
        

        public enum Cash
        {
            RUB,
            USD,
            EURO
        }
    }
}
