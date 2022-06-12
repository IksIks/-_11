using System;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.Models.AbstractModels
{
    internal abstract class BaseAccount : ViewModel
    {
        public abstract Guid Id { get; }
        public abstract DateTime DateOfCreation { get; set; }
        public abstract DateTime DateOfClose { get; set; }
    }
   
}
