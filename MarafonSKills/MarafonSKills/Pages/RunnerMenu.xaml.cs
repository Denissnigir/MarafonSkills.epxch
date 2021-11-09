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
    /// Логика взаимодействия для RunnerMenu.xaml
    /// </summary>
    public partial class RunnerMenu : Page
    {
        private User curUser { get; }
        public RunnerMenu(User user)
        {
            InitializeComponent();
            curUser = user;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Information information = new Information();
            information.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var curRunner = Context._con.Runner.Where(p => p.Email == curUser.Email).FirstOrDefault();
            NavigationService.Navigate(new MarathonRegister(curRunner));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterRunner(curUser));
        }
    }
}
