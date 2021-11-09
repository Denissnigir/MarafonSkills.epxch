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
using System.Windows.Threading;
using MarafonSKills.Model;
using MarafonSKills.Windows;

namespace MarafonSKills
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DateTime dateOfEvent = new DateTime(2022, 11, 24, 6, 0, 0);
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            string curDate = DateTime.Now.ToLongDateString();
            DateTB.Text = curDate;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += tick;
            dispatcherTimer.Start();
        }

        private void tick(object sender, EventArgs e)
        {
            var curDate = DateTime.Now;
            TimeSpan timeSpan = dateOfEvent - curDate;
            EventTB.Text = string.Format("{0} days, {1} hours and {2} minutes {3} seconds untill the Marafon starts!", timeSpan.Days.ToString(), timeSpan.Hours.ToString(), timeSpan.Minutes.ToString(), timeSpan.Seconds.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu(2);
            mainMenu.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu(3);
            mainMenu.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu(4);
            mainMenu.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu(1);
            mainMenu.Show();
            this.Close();
        }
    }
}
