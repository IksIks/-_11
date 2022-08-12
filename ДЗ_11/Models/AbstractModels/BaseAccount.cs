using System;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.Models.AbstractModels
{
    internal abstract class BaseAccount : ViewModel
    {
        public abstract DateTime DateOfCreation { get; set; }
        public abstract DateTime DateOfClose { get; set; }
        public abstract double BalanceRUB_Account { get; set; }
        //public abstract Cash Currency { get; set; }

        public BaseAccount()
        {
            DateOfCreation = DateTime.Now;
            DateOfClose = DateTime.MinValue;
        }
    }
   
}
