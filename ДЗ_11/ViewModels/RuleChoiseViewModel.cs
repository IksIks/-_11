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
        public static bool CanSeeText;
        private void OpenWindow()
        {
            MainWindow window = new MainWindow();
            window.Show();
            //double height = SystemParameters.FullPrimaryScreenHeight;
            //double width = SystemParameters.FullPrimaryScreenWidth;
            //window.Top = (height - window.Height) / 2;
            //window.Left = (width - window.Width) / 2;
            var ruleChoise = Application.Current.Windows[0];
            ruleChoise.Close();
        }

        #region Команда выбора прав доступа для консультанта
        public ICommand ConsultantRuleApplicationCommand { get; }
        private void OnConsultantRuleApplicationCommandExecuted(object parametr)
        {
            CanSeeText = false;
            OpenWindow();
        }
        private bool CanConsultantRuleApplicationCommandCanExecute(object parametr) => true;

        #endregion

        #region Команда выбора прав доступа для менеджера
        public ICommand ManagerRuleApplicationCommand { get; }
        private void OnManagerRuleApplicationCommandExecuted(object parametr)
        {
            CanSeeText = true;
            OpenWindow();
        }
        private bool CanManagerRuleApplicationCommandCanExecute(object parametr) => true;

        #endregion

        public RuleChoiseViewModel()
        {
            ConsultantRuleApplicationCommand = new RelayCommand(OnConsultantRuleApplicationCommandExecuted, CanConsultantRuleApplicationCommandCanExecute);
            ManagerRuleApplicationCommand = new RelayCommand(OnManagerRuleApplicationCommandExecuted, CanManagerRuleApplicationCommandCanExecute);
            
        }
    }
}
