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
    /// Логика взаимодействия для RunnerSponsor.xaml
    /// </summary>
    public partial class RunnerSponsor : Page
    {
        Registration regData = new Registration();
        private MainMenu curWin { get; }
        
        public RunnerSponsor(MainMenu mainMenu)
        {
            InitializeComponent();
            RunnerName.ItemsSource = Context._con.Registration.ToList();
            curWin = mainMenu;
        }

        private void RunnerName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var runner = (Registration)RunnerName.SelectedItem;
            regData = runner;
            var charity = Context._con.Charity.Where(p => p.CharityId == runner.CharityId).FirstOrDefault();
            OrganizationTB.Text = charity.CharityName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int amount = Convert.ToInt32(CharityAmount.Text);
            amount -= Convert.ToInt32(AddMinusCharityAmount.Text);
            CharityAmount.Text = Convert.ToString(amount);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            curWin.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                int amount = Convert.ToInt32(CharityAmount.Text);
                amount += Convert.ToInt32(AddMinusCharityAmount.Text);
                CharityAmount.Text = Convert.ToString(amount);
            }
            catch
            {
                MessageBox.Show("Нужно больше золота!");
            }
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(CharityAmount.Text) > 0 && !string.IsNullOrWhiteSpace(NameTB.Text) && RunnerName.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(CardHolderTB.Text) && !string.IsNullOrWhiteSpace(CardNumTB.Text) && !string.IsNullOrWhiteSpace(CardMonthTB.Text) && !string.IsNullOrWhiteSpace(CardYearTB.Text) && !string.IsNullOrWhiteSpace(CardCVCTB.Text))
            {
                
                Sponsorship sponsorship = new Sponsorship();
                sponsorship.SponsorName = NameTB.Text;
                sponsorship.RegistrationId = regData.RegistrationId;
                sponsorship.Amount = Convert.ToDecimal(CharityAmount.Text);
                Context._con.Sponsorship.Add(sponsorship);
                Context._con.SaveChanges();
                
                int sum = Convert.ToInt32(CharityAmount.Text);
                NavigationService.Navigate(new ThanksSponsor(regData, sum, curWin));
            }
            else
            {
                MessageBox.Show("Введите корректную сумму пожертвования или заполните все поля!");
            }
        }
    }
}
