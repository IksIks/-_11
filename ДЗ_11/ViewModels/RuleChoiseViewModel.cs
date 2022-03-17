using System.Windows;
using System.Windows.Input;
using ДЗ_11.Infrastructure.Commands;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.ViewModels
{
    internal class RuleChoiseViewModel : ViewModel
    {
        public ICommand ChoiseRuleApplicationCommand { get; }
        private void OnChoiseRuleApplicationCommandExecuted(object parametr)
        {
            MainWindow window = new MainWindow();
            window.Show();
            var ruleChoise = Application.Current.Windows[0];
            ruleChoise.Close();
        }
        private bool CanChoiseRuleApplicationCommandCanExecute(object parametr) => true;
        public RuleChoiseViewModel()
        {
            ChoiseRuleApplicationCommand = new RelayCommand(OnChoiseRuleApplicationCommandExecuted, CanChoiseRuleApplicationCommandCanExecute);
        }
    }
}
