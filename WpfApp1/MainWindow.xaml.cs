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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password!="" && usernameBox.Text!="")
            {
                try
                {
                    DatabaseHelper dbh = new DatabaseHelper();
                    dbh.OpenConnection();
                    if (dbh.AuthenticateUser(usernameBox.Text.Trim(),PasswordBox.Password.Trim()))
                    {
                        this.Content = new FarmsViewTemplate();
                    }
                    else
                    {
                        MessageBox.Show("Wrong Username/Password");
                    }

                }
                catch(Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
                
            }
            else
            {
                MessageBox.Show("Some requiers fields are empty");
            }
            
        }
    }
}
