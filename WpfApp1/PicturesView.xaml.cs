using Microsoft.Win32;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class PicturesView : Window
    {
        string farm=null;
        public PicturesView(string farmName)
        {
            farm = farmName;
            InitializeComponent();
            DatabaseHelper dbh = new DatabaseHelper();
            dbh.OpenConnection();
            List<string> myImages = dbh.GetImages(farmName);
            try
            {
                image.Source = new BitmapImage(new Uri(myImages.Last()));
                image.Width = 600;
                image.Height = 600;
            }
            catch
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                if (op.ShowDialog() == true)
                {
                    DatabaseHelper dbh = new DatabaseHelper();
                    dbh.OpenConnection();
                    Random r = new Random();
                    String name = "pic";
                    for (int i = 0; i < 5; i++)
                    {
                        name += r.Next().ToString();
                    }
                    dbh.UplodeImage(op.FileName, name, farm);
                    dbh.CloseConnection();
                    FilePath.Text = op.FileName;
                    image.Height = 800;
                    image.Width = 800;
                    image.Source = new BitmapImage(new Uri(op.FileName));
                }
            upd.IsDefault = true;
            MessageBox.Show("Succesful Update");
            this.Close();


        }

        private void FilePath_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
