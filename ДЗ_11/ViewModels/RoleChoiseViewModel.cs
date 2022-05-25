using System;
using System.Windows;
using System.Windows.Input;
using ДЗ_11.Infrastructure.Commands;
using ДЗ_11.Models;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.ViewModels
{
    internal class RoleChoiseViewModel : ViewModel
    {
        /// <summary>
        /// Определяет права доступа для работы в приложении
        /// </summary>
        public static bool ManadgerRole { get; set; }

        /// <summary>
        /// Метод создания основного окна для работы с клиентами
        /// </summary>
        private void OpenWindow()
        {
            MainWindow window = new MainWindow();
            window.Show();
            var roleChoise = Application.Current.Windows[0];
            roleChoise.Close();
        }

        #region Команда выбора прав доступа для консультанта

        public ICommand ConsultantRoleApplicationCommand { get; }
        private void OnConsultantRoleApplicationCommandExecuted(object parametr)
        {
            ManadgerRole = false;
            OpenWindow();
        }
        private bool CanConsultantRoleApplicationCommandCanExecute(object parametr) => true;

        #endregion

        #region Команда выбора прав доступа для менеджера
        public ICommand ManagerRoleApplicationCommand { get; }
        private void OnManagerRoleApplicationCommandExecuted(object parametr)
        {
            ManadgerRole = true;
            OpenWindow();
        }
        private bool CanManagerRoleApplicationCommandCanExecute(object parametr) => true;

        #endregion

        public RoleChoiseViewModel()
        {
            ConsultantRoleApplicationCommand = new RelayCommand(OnConsultantRoleApplicationCommandExecuted, CanConsultantRoleApplicationCommandCanExecute);
            ManagerRoleApplicationCommand = new RelayCommand(OnManagerRoleApplicationCommandExecuted, CanManagerRoleApplicationCommandCanExecute);
        }
    }
}
