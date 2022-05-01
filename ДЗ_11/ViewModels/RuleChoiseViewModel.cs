using System;
using System.Windows;
using System.Windows.Input;
using ДЗ_11.Infrastructure.Commands;
using ДЗ_11.Models;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.ViewModels
{
    internal class RuleChoiseViewModel : ViewModel
    {
        public static bool canSeeText = false;
        private void OpenWindow()
        {
            MainWindow window = new MainWindow();
            window.Show();
            var ruleChoise = Application.Current.Windows[0];
            ruleChoise.Close();
        }
        public void CreateClients()
        {
            Random r = new Random();
            for (int i = 0; i < 5; i++)
            {
                Client temp = new Client("name" + i,
                                        "LastName" + i,
                                        "Patronymic" + i,
                                        (uint)(i * Math.Pow(10, 8)),
                                        new Passport((i * Math.Pow(10, 32)).ToString(),
                                        (i * Math.Pow(10, 3)).ToString()));
                MainWindowViewModel.Clients.Add(temp);
            }
        }

        #region Команда при выборе пользователя
        public ICommand ConsultantRuleApplicationCommand { get; }
        private void OnConsultantRuleApplicationCommandExecuted(object parametr)
        {
            CreateClients();
            OpenWindow();
        }


        private bool CanConsultantRuleApplicationCommandCanExecute(object parametr) => true;
        #endregion
        public RuleChoiseViewModel()
        {
            ConsultantRuleApplicationCommand = new RelayCommand(OnConsultantRuleApplicationCommandExecuted, CanConsultantRuleApplicationCommandCanExecute);
            
        }
    }
}
