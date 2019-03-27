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
    /// Interaction logic for Dynamic2.xaml
    /// </summary>
    public partial class BlockViewTemplate : Page
    {
        public BlockViewTemplate(object sender)
        {
            InitializeComponent();
            Expander expandedFrom = (Expander) sender;
            List<Farm> farms = new List<Farm>();
            DatabaseHelper db = new DatabaseHelper();
            try
            {
                db.OpenConnection();
                farms = db.GetAllFarms();
            }
            catch
            {
                MessageBox.Show("Couldn't connect to database server");
            }

            foreach (Farm fm in farms)
            {
                Expander outFarm = new Expander
                {
                    Header = fm.Name,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Background = new SolidColorBrush(Colors.Lavender),
                    Width = 300,
                    Margin = new Thickness(0, 0, 0, 0)
                };
                StackPanel inn = new StackPanel
                {
                    Orientation = Orientation.Vertical
                };
                outFarm.MouseDoubleClick += new MouseButtonEventHandler(Double_Click);
                outFarm.Content = inn;
                List<Block> blocks = db.GetBlocksForFarm(fm.Name);
                foreach (Block blc in blocks)
                {
                    Expander ins = new Expander
                    {
                        Header = blc.Name,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Background = new SolidColorBrush(Colors.Lavender),
                        Width = 300,
                        Margin = new Thickness(20, 0, 0, 0)
                    };
                    inn.Children.Add(ins);
                    ins.MouseDoubleClick += new MouseButtonEventHandler(Double_Click_inner);

                    if (blc.Name == expandedFrom.Header.ToString())
                    {
                        outFarm.IsExpanded = true;
                        ins.IsExpanded = true;
                    }
                }
                LeftPanel.Children.Add(outFarm);
            }
            try
            {
                db.CloseConnection();
            }
            catch
            {
                MessageBox.Show("No Connection Found");
            }
            db.OpenConnection();
            Block b= db.GetBlockDetail(expandedFrom.Header.ToString());
            BName.Text = b.Name;
            BId.Text = b.Id.ToString();
            BLength.Text =b.Length.ToString();
            BWidth.Text = b.Width.ToString();

            BSoilType.Text =b.SoilType;
            BSoilPh.Text ="7.0";
            BSoilMoisture.Text =b.Soil_moisture.ToString();
            BSunLightIntensity.Text = "50%";
            BPhosphorous.Text =b.Phosphorus.ToString();
            BNitrogen.Text =b.Nitrogen.ToString();
            BPotassium.Text =b.Potassium.ToString();
            BSulphur.Text =b.Sulphur.ToString();
            BCalcium.Text =b.Calcium.ToString();
            BMagnesium.Text =b.Magnesium.ToString();

               
        }
        private void Double_Click(object sender, RoutedEventArgs e)
        {
            WriteLine("found the handler");
            Expander expa = (Expander)sender;
            expa.Content = "Dynamicaly updated";
            Application.Current.MainWindow.Content = new FarmsViewTemplate();
        }
        private void Double_Click_inner(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new BlockViewTemplate(sender);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_info(object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.5;
            Window ad = new weather_info();
            ad.ShowDialog();
            this.Opacity = 1;
        }

        private void currentUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {

            

            string fn = BName.Text;
            int fid = int.Parse(BId.Text);
            float bLen = float.Parse(BLength.Text);
            float bWid = float.Parse(BWidth.Text);

            string st = BSoilType.Text;

            float sm = float.Parse(BSoilMoisture.Text);
            float bp = float.Parse(BPhosphorous.Text);
            float bn = float.Parse(BNitrogen.Text);
            float bpt = float.Parse(BPotassium.Text);
            float bs = float.Parse(BSulphur.Text);
            float bca = float.Parse(BCalcium.Text);
            float bmg = float.Parse(BMagnesium.Text);

            DatabaseHelper dbh = new DatabaseHelper();
            try
            {
                dbh.OpenConnection();
            }
            catch
            {
                MessageBox.Show("No connection");
            }
            dbh.updateBlock(fn, bWid, bLen, st, sm, bp, bn, bpt, bs, bca, bmg, fid);
            MessageBox.Show("Succesful Update");
            
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DatabaseHelper dbh = new DatabaseHelper();
            try
            {
                dbh.OpenConnection();
            }
            catch
            {
                MessageBox.Show("No connection");
            }
            dbh.DeleteBlock(int.Parse(BId.Text), BName.Text);
            MessageBox.Show("You deleted this Block");
        }
    }
}
