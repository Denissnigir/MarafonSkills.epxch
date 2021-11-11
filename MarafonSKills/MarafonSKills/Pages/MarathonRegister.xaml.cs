using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для MarathonRegister.xaml
    /// </summary>
    public partial class MarathonRegister : Page
    {
        private Runner curRunner { get; }

        private int paymentCharityAmount;
        private int paymentKitAmount;
        private int paymentDistanceAmount;
        public MarathonRegister(Runner runner)
        {
            InitializeComponent();
            BuyAmount.Text = "0";
            CharityCB.ItemsSource = Context._con.Charity.ToList();
            curRunner = runner;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var curUser = Context._con.User.Where(p => p.Email == curRunner.Email).FirstOrDefault();
            NavigationService.Navigate(new RunnerMenu(curUser));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("^0"); // Регекс на некорректную сумму
            if (Regex.IsMatch(CharityAmount.Text, Convert.ToString(regex), RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Введите корректную сумму!");
                CharityAmount.Text = "";
            }

            if (string.IsNullOrWhiteSpace(CharityAmount.Text) || string.IsNullOrEmpty(CharityAmount.Text))
            {
                paymentCharityAmount = 0;
            }
            else
            {
                paymentCharityAmount = Convert.ToInt32(CharityAmount.Text);
            }
            PriceCount();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) // НЕ ЗАБУДЬ!!!! ВВОД ТОЛЬКО ЦИФРЫ
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void KitOne_Checked(object sender, RoutedEventArgs e)
        {
            paymentKitAmount = 0;
            PriceCount();
        }

        private void KitTwo_Checked(object sender, RoutedEventArgs e)
        {
            paymentKitAmount = 20;
            PriceCount();
        }

        private void KitThree_Checked(object sender, RoutedEventArgs e)
        {
            paymentKitAmount = 45;
            PriceCount();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            paymentDistanceAmount += 145;
            PriceCount();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            paymentDistanceAmount -= 145;
            PriceCount();
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            paymentDistanceAmount += 75;
            PriceCount();
        }

        private void CheckBox_Unchecked_1(object sender, RoutedEventArgs e)
        {
            paymentDistanceAmount -= 75;
            PriceCount();
        }

        private void CheckBox_Checked_2(object sender, RoutedEventArgs e)
        {
            paymentDistanceAmount += 20;
            PriceCount();
        }

        private void CheckBox_Unchecked_2(object sender, RoutedEventArgs e)
        {
            paymentDistanceAmount -= 20;
            PriceCount();
        }

        private void PriceCount()
        {
            if(BuyAmount != null)
            {
                BuyAmount.Text = Convert.ToString(paymentDistanceAmount + paymentCharityAmount + paymentKitAmount);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(Check1.IsChecked == false && Check2.IsChecked == false && Check3.IsChecked == false)
            {
                MessageBox.Show("Выберите хотя бы один вид марафона!");
            }
            else
            {
                string raceKit = "A";
                if (KitOne.IsChecked == true)
                {
                    raceKit = "A";
                }
                else if(KitTwo.IsChecked == true)
                {
                    raceKit = "B";
                } else if(KitThree.IsChecked == true)
                {
                    raceKit = "C";
                }

                Registration registration = new Registration();
                registration.RunnerId = curRunner.RunnerId;
                registration.RegistrationDateTime = DateTime.Now;
                registration.RaceKitOptionId = raceKit;
                registration.RegistrationStatusId = 1;
                registration.Cost = Convert.ToDecimal(BuyAmount.Text);
                registration.CharityId = CharityCB.SelectedIndex + 1;
                registration.SponsorshipTarget = Convert.ToDecimal(CharityAmount.Text);

                Context._con.Registration.Add(registration);
                Context._con.SaveChanges();

                var curUser = Context._con.User.Where(p => p.Email == curRunner.Email).FirstOrDefault();
                NavigationService.Navigate(new RegistrationConfirmation(curUser));
            }
        }
    }
}
