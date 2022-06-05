using System;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.Models.AbstractModels
{
    internal abstract class BaseAccount : ViewModel
    {
        public abstract int Id { get; set; }
        public abstract DateTime DateOfCreation { get; set; }
        public abstract DateTime DateOfClose { get; set; }
        public abstract int Balance { get; set; }
        
    }
   
}
