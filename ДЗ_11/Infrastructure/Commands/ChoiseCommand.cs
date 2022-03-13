using System.Windows;
using ДЗ_11.Infrastructure.Commands.Base;
using ДЗ_11.ViewModels;

namespace ДЗ_11.Infrastructure.Commands
{
    internal class ChoiseCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            MainWindow window = new MainWindow();
            window.Show();
            var ruleChoise = Application.Current.Windows[0];
            ruleChoise.Close();
            MainWindowViewModel temp = new MainWindowViewModel();
            temp.AddClients();

        }
    }
}
