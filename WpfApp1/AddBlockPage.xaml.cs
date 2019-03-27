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
    public partial class AddBlockPage : Window
    {
        string Selected_from;
        public AddBlockPage(string selected_from)
        {
            Selected_from = selected_from;
            InitializeComponent();
            head_text.Content = head_text.Content + Selected_from;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHelper dbh = new DatabaseHelper();
            string bName = BlockName.Text;
            float bWidth = float.Parse(BlockWidth.Text);
            float bLength = float.Parse(BlockLength.Text);

            
            try
            {
                dbh.OpenConnection();
                dbh.InsertBlock(bName, bWidth, bLength,Selected_from);
                dbh.CloseConnection();
                MessageBox.Show("Succesful Addition of Block");
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
