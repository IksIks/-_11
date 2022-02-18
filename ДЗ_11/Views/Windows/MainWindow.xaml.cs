using System.Windows;
using ДЗ_11.Views.Windows;

namespace ДЗ_11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RuleChoise rule = new RuleChoise();
            rule.ShowDialog();
        }
    }
}
