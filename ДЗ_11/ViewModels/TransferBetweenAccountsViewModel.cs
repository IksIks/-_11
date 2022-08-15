using ДЗ_11.Data;
using ДЗ_11.Services;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.ViewModels
{
    internal class TransferBetweenAccountsViewModel : ViewModel
    {
        public double TransferAmount { get; set; } = HelpClass.TempClient.NonDepositAccount.BalanceRUB_Account;


        public Cash Currency { private get; set; }



        public TransferBetweenAccountsViewModel()
        {

        }

    }
}
