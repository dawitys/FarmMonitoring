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
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes;
using MaterialDesignColors;
using MahApps;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for DynamicExpanderDemo.xaml
    /// </summary>
    public partial class FarmsViewTemplate : Page
    {
        Dictionary<string, int> Name_to_Id = new Dictionary<string, int>();
        string Selected_Farm;
        int Selected_Farm_Id;

        public FarmsViewTemplate()
        {
            InitializeComponent();
            RefreshPage(null);
            add_btn.IsEnabled = false;
            update_btn.IsEnabled = false;
            pic_btn.IsEnabled = false;
        }
            public FarmsViewTemplate(string FName)
        {
            InitializeComponent();
            RefreshPage(FName);
            add_btn.IsEnabled = false;
            update_btn.IsEnabled = false;
            pic_btn.IsEnabled = false;
        }
        private void RefreshPage(string Fname)
        {
            LeftPanel.Children.Clear();
            currentUser.Icon = currentUser.Content.ToString().Substring(0, 1);
            List<Farm> farms = new List<Farm>();
            DatabaseHelper db = new DatabaseHelper();
            try
            {
                db.OpenConnection();
                farms = db.GetAllFarms();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace);
            }

            foreach (Farm fm in farms)
            {
                Name_to_Id[fm.Name] = fm.Id;
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
                }
                
                LeftPanel.Children.Add(outFarm);
                if (Fname == fm.Name)
                {
                    outFarm.IsExpanded = true;
                }
                {

                }
            }
            try
            {
                db.CloseConnection();
            }
            catch
            {
                MessageBox.Show("No Connection Found");
            }
        }
        
        private void Double_Click(object sender,RoutedEventArgs e)
        {
            Expander expan = (Expander) sender;

            Selected_Farm = expan.Header.ToString();
            Selected_Farm_Id = Name_to_Id[Selected_Farm];

            MainTitle.Text = $"Welcome to {Selected_Farm}";

            expan.IsExpanded = true;
            add_btn.IsEnabled = true;
            update_btn.IsEnabled = true;
            pic_btn.IsEnabled = true;

            DatabaseHelper dbh = new DatabaseHelper();
            try
            { 
                dbh.OpenConnection();
            }
            catch
            {
                MessageBox.Show("No connection");
            }


            Farm selectedFarm = dbh.GetFarmsDetail(expan.Header.ToString().Trim());

            dbh.CloseConnection();

            FName.Text = selectedFarm.Name;
            FName.IsEnabled = false;
            FId.Text = selectedFarm.Identity;
            FArea.Text = selectedFarm.Area.ToString();
            FDistance.Text = selectedFarm.Distance.ToString();

            FRegion.Text = selectedFarm.Region;
            FWoreda.Text = selectedFarm.Woreda;
            FKebele.Text = selectedFarm.Kebele;
            FAltitude.Text = selectedFarm.Altitude.ToString();

            FAnnualRainFall.Text = selectedFarm.AnnualRainFall.ToString();
            FSeasonalRainfall.Text = selectedFarm.SeasonalRainFall.ToString();
            FAirHumidity.Text = selectedFarm.AirHumidity.ToString();
            FMaxTemp.Text = selectedFarm.MaxTemp.ToString();
            FMinTemp.Text = selectedFarm.MinTemp.ToString();

            /*
            int newI = FClimate.Items.Add(selectedFarm.ClimateType);
            FClimate.SelectedIndex = newI;
            */
            UpdateComboBox(ref FClimate, selectedFarm.ClimateType);
            expan.IsExpanded = true;

        }
        private void UpdateComboBox(ref ComboBox comboBox,string element)
        {
            foreach (ComboBoxItem item in comboBox.Items)
            {
                if (item.ToString() == element)
                {
                    comboBox.SelectedItem = item;
                }
            }
        }
        private void Double_Click_inner(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new BlockViewTemplate(sender);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.5;
            Window ad = new AddBlockPage(Selected_Farm);
            ad.ShowDialog();
            RefreshPage(null);
            this.Opacity = 1;
        }

        private void currentUser_Click(object sender, RoutedEventArgs e)
        {
            Chip c= (Chip) sender;
            if(c.Content.ToString()=="Dawit Yonas")
            {
                c.Content = "ADMIN";
            }
            else if(c.Content.ToString() == "ADMIN"){
                c.Content = "Dawit Yonas";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.5;
            Window ad = new PicturesView(Selected_Farm);
            ad.ShowDialog();
            this.Opacity = 1;
        }

        private void Farm_Add_Click(object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.5;
            Window ad = new AddFarmPage();
            ad.ShowDialog();
            RefreshPage(null);
            this.Opacity = 1;
        }

        private void FClimate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*ComboBox cb = (ComboBox) sender;
            MessageBox.Show(cb.SelectedValue.ToString());
            */
        }

        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 w = new Window1();
            Window ow = Application.Current.MainWindow;
            ow.Close();
            Application.Current.MainWindow = w;
            w.Show();
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            string fn = FName.Text;
            string fid = FId.Text;
            float farea = float.Parse(FArea.Text);
            float fd = float.Parse(FDistance.Text);

            string fr = FRegion.Text;
            string fw = FWoreda.Text;
            string fk = FKebele.Text;
            float falt = float.Parse(FAltitude.Text);

            float far = float.Parse(FAnnualRainFall.Text); 
            float fsr = float.Parse(FSeasonalRainfall.Text);
            float fah = float.Parse(FAirHumidity.Text);
            float fmaxt = float.Parse(FMaxTemp.Text);
            float fmint = float.Parse(FMinTemp.Text);

            DatabaseHelper dbh = new DatabaseHelper();
            try
            {
                dbh.OpenConnection();
            }
            catch
            {
                MessageBox.Show("No connection");
            }
            dbh.updateFarm(fn, farea, fd, fr, fw, fk, fah, fsr, "Tropical / Mega-Thermal", far, fmint, fmaxt, falt, Selected_Farm_Id);
            MessageBox.Show("Succesful Update");
            RefreshPage(null);
        }

        private void Farm_Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
