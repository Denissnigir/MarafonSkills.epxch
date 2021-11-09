using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MarafonSKills.Windows;
using MarafonSKills.Model;

namespace MarafonSKills
{
    /// <summary>
    /// Логика взаимодействия для RegistrationConfirmation.xaml
    /// </summary>
    public partial class RegistrationConfirmation : Page
    {
        private User curUser { get; }
        public RegistrationConfirmation(User user)
        {
            InitializeComponent();
            curUser = user;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RunnerMenu(curUser));
        }
    }
}
