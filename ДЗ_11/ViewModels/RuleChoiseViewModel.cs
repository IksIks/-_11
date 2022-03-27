using System.Windows;
using System.Windows.Input;
using ДЗ_11.Infrastructure.Commands;
using ДЗ_11.ViewModels.Base;
using ДЗ_11.ViewModels;

namespace ДЗ_11.ViewModels
{
    internal class RuleChoiseViewModel : ViewModel
    {
        
        #region Команда при выборе пользователя
        public ICommand ChoiseRuleApplicationCommand { get; }
        private void OnChoiseRuleApplicationCommandExecuted(object parametr)
        {
            MainWindow window = new MainWindow();
            window.Show();
            var ruleChoise = Application.Current.Windows[0];
            ruleChoise.Close();
        }
        private bool CanChoiseRuleApplicationCommandCanExecute(object parametr) => true;
        #endregion
        public RuleChoiseViewModel()
        {
            ChoiseRuleApplicationCommand = new RelayCommand(OnChoiseRuleApplicationCommandExecuted, CanChoiseRuleApplicationCommandCanExecute);
            
        }
    }
}
