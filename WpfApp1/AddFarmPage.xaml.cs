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
using static System.Console;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AddFarmPage.xaml
    /// </summary>
    public partial class AddFarmPage : Window
    {
        public AddFarmPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHelper dbh = new DatabaseHelper();
            string fName = FarmName.Text;
            float fArea = float.Parse(FarmArea.Text);
            float fDistance = float.Parse(FarmDistance .Text);

            
            try
            {
                dbh.OpenConnection();
                dbh.InsertFarm(fName, fArea, fDistance);
                dbh.CloseConnection();
                MessageBox.Show("Succesful Addition of Farm","congrats");
            }
            catch(ServerException se)
            {
                WriteLine(se.Message);
                MessageBox.Show("Server not found", "Sorry");
            }
            this.Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
