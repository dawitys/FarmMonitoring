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
using System.Windows.Shapes;
using static System.Console;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for weather_info.xaml
    /// </summary>
    public partial class weather_info : Window
    {
        public weather_info()
        { 
            InitializeComponent();
            delete_btn.IsEnabled = false;

            DatabaseHelper db = new DatabaseHelper();
            db.OpenConnection();
            List<Farm> farms = db.GetAllFarms();
            foreach (Farm fm in farms)
            {
                farmsList.Items.Add(fm);
            }
            db.CloseConnection();
        }

        private void FarmsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WriteLine("selected");
            delete_btn.IsEnabled = true;
        }

        private void View_more_btn_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.MainWindow.Content = new FarmsViewTemplate();

            /*
            this.Opacity = 0.5;
            Window ad = new AddBlockPage();
            ad.ShowDialog();
            this.Opacity = 1;
            */
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
