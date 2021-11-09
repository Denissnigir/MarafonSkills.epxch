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
    /// Логика взаимодействия для AuthMenu.xaml
    /// </summary>
    public partial class AuthMenu : Page
    {
        private MainMenu curWin { get; }

        public AuthMenu(MainMenu mainMenu)
        {
            InitializeComponent();
            curWin = mainMenu;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            curWin.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var user = Context._con.User.Where(p => p.Email == EmailTB.Text && p.Password == PasswordTB.Text).FirstOrDefault();
            if(user != null)
            {
                if(user.RoleId == "R")
                {
                    NavigationService.Navigate(new RunnerMenu(user));
                } 
                else if(user.RoleId == "C")
                {
                    NavigationService.Navigate(new CoordMenu());
                }
                else if(user.RoleId == "A")
                {
                    NavigationService.Navigate(new AdminMenu());
                }
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль!");
                PasswordTB.Text = "";
            }
        }

        private void EmailTB_GotFocus(object sender, RoutedEventArgs e)
        {
            if(EmailTB.Text == "Введите свой Email")
            {
                EmailTB.Text = "";
            }
        }

        private void EmailTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTB.Text))
            {
                EmailTB.Text = "Введите свой Email";
            }
        }

        private void PasswordTB_GotFocus(object sender, RoutedEventArgs e)
        {
            if(PasswordTB.Text == "Введите свой пароль")
            {
                PasswordTB.Text = "";
            }
        }

        private void PasswordTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PasswordTB.Text)){
                PasswordTB.Text = "Введите свой пароль";
            }
        }
    }
}
