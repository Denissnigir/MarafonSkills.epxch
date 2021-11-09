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
using System.IO;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace MarafonSKills
{
    /// <summary>
    /// Логика взаимодействия для RegisterRunner.xaml
    /// </summary> 
    public partial class RegisterRunner : Page
    {

        private string filePath = null;
        private string fileName = null;

        User actualUser;
        User userData;
        Runner runnerData;

        public RegisterRunner(User user = null)
        {
            InitializeComponent();

            GenderCB.ItemsSource = Context._con.Gender.ToList();
            CountryCB.ItemsSource = Context._con.Country.ToList();

            if (user == null)
            {
                userData = new User();
                runnerData = new Runner();
            }
            else
            {
                userData = user;
                runnerData = Context._con.Runner.Where(p => p.Email == userData.Email).FirstOrDefault();
                EmailTB.IsEnabled = false;
                HeaderTB.Text = "Редактирование профиля";
                HeaderDesriptionTB.Text = "Оставьте поля с паролем нетронутым, если вы не хотите изменять пароль.";
                AcceptButton.Content = "Редактировать";
            }


            if(user != null)
            {
                GenderCB.SelectedItem = runnerData.Gender1;
                CountryCB.SelectedItem = runnerData.Country;
            }

            actualUser = user;

            RunnerGrid.DataContext = runnerData;
            UserGrid.DataContext = userData;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if ((bool)openFileDialog.ShowDialog())
            {
                filePath = openFileDialog.FileName;
                fileName = filePath.Split('\\').Last();
                ProfilePhoto.Source = new BitmapImage(new Uri(filePath));
                ChoosePhotoTB.Text = fileName;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Regex regex = new Regex("[a-z0-9].*[a-z0-9]@[a-z]+[.][a-z]+");
            if (!string.IsNullOrWhiteSpace(filePath) && !string.IsNullOrWhiteSpace(EmailTB.Text) && !string.IsNullOrWhiteSpace(PasswordTB.Text) && !string.IsNullOrWhiteSpace(RepeatPasswordTB.Text) &&
                !string.IsNullOrWhiteSpace(NameTB.Text) && !string.IsNullOrWhiteSpace(SurnameTB.Text) && GenderCB.SelectedIndex >= 0 && BirthdateTB.SelectedDate != null && CountryCB.SelectedIndex >= 0)
            {
                if(BirthdateTB.SelectedDate >= new DateTime(2010, 01, 01))
                {
                    MessageBox.Show("Выберите корректную дату!");
                }
                else
                {
                    if(PasswordTB.Text == RepeatPasswordTB.Text)
                    {

                        if (Regex.IsMatch(EmailTB.Text, Convert.ToString(regex), RegexOptions.IgnoreCase))
                        {
                            try
                            {
                                File.Copy(filePath, $@"..\..\{fileName}");
                            }
                            catch
                            {

                            }

                            userData.RoleId = "R";
                            if(actualUser == null)
                            {
                                Context._con.User.Add(userData);
                            }
                            Context._con.SaveChanges();

                            if(actualUser == null)
                            {
                                runnerData.Photo = fileName;
                                runnerData.Email = userData.Email;
                                Context._con.Runner.Add(runnerData);
                            }
                            runnerData.Photo = fileName;
                            Context._con.SaveChanges();

                            if (actualUser == null)
                            {
                                NavigationService.Navigate(new MarathonRegister(runnerData));
                            }
                            else
                            {
                                NavigationService.Navigate(new RunnerMenu(userData));
                            }
                        }
                        else
                        {
                            MessageBox.Show("Введите корректную почту!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пароли не совпадают!");
                    }
                }

                
            }
            else
            {
                MessageBox.Show("Заполните все поля!!");
            }
            
        }
    }
}
