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
using MarafonSKills.Model;
using MarafonSKills.Windows;

namespace MarafonSKills
{
    /// <summary>
    /// Логика взаимодействия для ThanksSponsor.xaml
    /// </summary>
    public partial class ThanksSponsor : Page
    {
        private MainMenu curWin { get; }
        public ThanksSponsor(Registration registration, int sum, MainMenu mainMenu)
        {
            InitializeComponent();
            RunnerInfo.Text = string.Format("{0}", registration.FullName);
            OrgName.Text = registration.Charity.CharityName;
            CharityAmount.Text = Convert.ToString(sum);
            curWin = mainMenu;
        }

        private void WindowShutDown(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            curWin.Close();
        }
    }
}
