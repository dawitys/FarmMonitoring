using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using static System.Console;

namespace WpfApp1
{
    class DatabaseHelper
    {
        private MySqlConnection connection;
        private string Server;
        private string Database;
        private string Uid;
        private string Password;
        private string Port;
        private bool IsConn;

        public DatabaseHelper()
        {
            Initialize();
        }


        private void Initialize()
        {
            Server = "localhost";
            Database = "farming";
            Uid = "root";
            Password = "";
            Port = "3306";
            string connectionString;

            connectionString = $"datasource={Server};DATABASE={Database};username={Uid};PASSWORD={Password};PORT={Port};SslMode=none;";
            connection = new MySqlConnection(connectionString);
        }
        public bool OpenConnection()
        {
            if (connection != null)
            {
                try
                {
                    connection.Open();
                    IsConn = true;
                    WriteLine("Connection opened");
                    return true;
                }
                catch (MySqlException e)
                {
                    throw new ServerException("couldn't connect to database server");
                }
            }
            return false;
        }

        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException)
            {
                WriteLine("Failed to close connection");
                return false;
            }
        }
        public void UplodeImage(string fullPath, string fileName, string farmName)
        {
            String appStartPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

            string destinationPath = String.Format(appStartPath + "\\{0}\\" + fileName + ".jpg", "Images");

            string newD = destinationPath.Replace("\\", "\\\\");
            //MessageBox.Show(newD);
            //destinationPath = destinationPath.Replace("\","\\");
            File.Copy(fullPath, newD, true);
            string query = $"INSERT INTO farm_pictures(farm_name,pic_uri) VALUES('{farmName}','{newD}');";
            if (IsConn == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
            }

        }
        public List<string> GetImages(string farmName)
        {
            List<string> imageString = new List<string>();
            string query = $"SELECT * FROM farm_pictures WHERE farm_name ='{farmName}';";
            if (IsConn == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    imageString.Add(reader["pic_uri"].ToString());
                    //MessageBox.Show(imageString.Last());
                }
                reader.Close();
            }
            return imageString;
        }
        public bool AuthenticateUser(string email, string pass)
        {
            string query = $"SELECT * FROM users WHERE email ='{email}';";
            bool isAuth = false;
            if (IsConn == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (reader["role"].ToString() == "ADMIN" && reader["password"].ToString() == pass)
                    {
                        isAuth = true;
                    }
                }
                reader.Close();
            }
            return isAuth;
        }
        public Block GetBlockDetail(string blockName)
        {
            string query = $"SELECT* FROM `blocks` WHERE `block_name` LIKE '{blockName}'";
            Block selected = new Block();
            if (IsConn == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    selected = new Block(int.Parse(reader[0].ToString()), reader[1].ToString(), float.Parse(reader[2].ToString()), float.Parse(reader[3].ToString()),
                        reader[4].ToString(), float.Parse(reader[5].ToString()),float.Parse(reader[6].ToString()), float.Parse(reader[7].ToString()),
                        float.Parse(reader[8].ToString()), float.Parse(reader[9].ToString()), float.Parse(reader[10].ToString()), float.Parse(reader[11].ToString()),reader[12].ToString());
                }
                reader.Close();
            }
            return selected;
        }
        
        
        public Farm GetFarmsDetail(string farmName)
        {
            string query = $"SELECT * FROM farms WHERE name ='{farmName}'";
            Farm selected = new Farm();
            if (IsConn == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    selected = new Farm(reader[1].ToString(), reader[2].ToString(), float.Parse(reader[3].ToString()), float.Parse(reader[4].ToString()),
                        reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), float.Parse(reader[8].ToString()),
                        float.Parse(reader[9].ToString()), reader[10].ToString(), float.Parse(reader[11].ToString()), float.Parse(reader[12].ToString()),
                        float.Parse(reader[13].ToString()), float.Parse(reader[14].ToString()));
                }
                reader.Close();
            }
            return selected;
        }
        public List<Farm> GetAllFarmsWithDetail()
        {
            string query = "SELECT * FROM farms";
            List<Farm> farmsList = new List<Farm>();

            if (IsConn == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    farmsList.Add(
                        new Farm(reader[1].ToString(), reader[2].ToString(), float.Parse(reader[3].ToString()), float.Parse(reader[4].ToString()),
                        reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), float.Parse(reader[8].ToString()),
                        float.Parse(reader[9].ToString()), reader[10].ToString(), float.Parse(reader[11].ToString()), float.Parse(reader[12].ToString()),
                        float.Parse(reader[13].ToString()), float.Parse(reader[14].ToString()))
                        );
                }
                reader.Close();
            }
            return farmsList;
        }

        public List<Farm> GetAllFarms()
        {
            string query = "SELECT * FROM farms";
            List<Farm> farmsList = new List<Farm>();

            if (IsConn == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    farmsList.Add(new Farm(int.Parse(reader["id"].ToString()), reader["name"].ToString(), reader["farm_id"].ToString(), reader["region"].ToString(), reader["woreda"].ToString()));
                }
                reader.Close();
            }
            return farmsList;
        }
        public List<Block> GetBlocksForFarm(string farmName)
        {
            string query = $"SELECT * FROM blocks WHERE farm_name ='{farmName}'";
            List<Block> blocksList = new List<Block>();

            if (IsConn == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader reader2 = cmd.ExecuteReader();
                while (reader2.Read())
                {
                    blocksList.Add(new Block(reader2["block_name"].ToString(), reader2["farm_name"].ToString(), float.Parse(reader2["length"].ToString()), float.Parse(reader2["width"].ToString())));
                }
                reader2.Close();
            }
            return blocksList;
        }
        public void InsertFarm(string f_name, float f_area, float f_distance)
        {
            //string query = "INSERT INTO `farms` (`name`, `location`, `description`, `soil_type`, `humidity`, `annual_rainfall`, `min_temp`, `max_temp`, `altitude`) VALUES (?,?,?,?,?,?,?,?,?);";
            //string query = string.Format($"INSERT INTO `farms` (`name`, `location`, `description`,`humidity`, `annual_rainfall`, `min_temp`, `max_temp`, `altitude`) VALUES ('{name}','{location}','{description}',200,5,20,40,100);");

            string tempQuery = "SELECT * FROM farms where id = (SELECT MAX(id) FROM farms)";
            string temp1 = $"FRM/0";
            string temp2 = "";
            if (IsConn == true)
            {
                MySqlCommand cmd = new MySqlCommand(tempQuery, connection);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    temp2 = (int.Parse(reader["id"].ToString()) + 1).ToString();
                }
                reader.Close();
                temp1 = temp1 + temp2;
            }

            string query = $"INSERT INTO `farms` (`id`, `name`, `farm_id`,`area`, `distance`, `region`, `woreda`, `kebele`, `humidity`, `seasonal_rainfall`, `climate_type`, `annual_rainfall`, `min_temp`, `max_temp`, `altitude`) VALUES (NULL, '{f_name}','{temp1}',{f_area}, {f_distance}, 'Tigray', 'Adarkay', '04', 0, 0, 0, 0, 0, 0, '880');";
            if (IsConn == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
        }
        public void InsertBlock(string b_name, float b_width, float b_length, string f_name)
        {
            string query = $"INSERT INTO `blocks` (`id`, `block_name`, `width`, `length`, `soil_type`, `soil_moisture`, `phosphorus_content`, `nitrogen_content`, `potassium_content`, `sulphur_content`, `calcium_content`, `magnessium_content`, `farm_name`) VALUES(NULL, '{b_name}', {b_width} , {b_length} , '', '', 0, 0, 0, 0, 0, 0, '{f_name}'); ";
            if (IsConn == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
        }

        public void updateBlock(string bn, float wd, float ln, string s_t, float s_m, float p_c, float n_c, float pt_c,
            float s_c, float ca_c, float mg_c, int block_id)
        {

            string query = $"UPDATE `blocks` SET `block_name` = '{bn}', `width` = {wd}, `length` = {ln}, `soil_type` = '{s_t}', `soil_moisture` = '{s_m}', " +
                $"`phosphorus_content` = '{p_c}', `nitrogen_content` = '{n_c}', `potassium_content` = '{pt_c}', `sulphur_content` = '{s_c}', " +
                $"`calcium_content` = '{ca_c}', `magnessium_content` = '{mg_c}' WHERE `blocks`.`id` = {block_id}";
            if (IsConn == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
        }

        public void updateFarm(string f_name, float f_a, float f_d, string f_region, string f_woreda, string f_kebele, float f_h,
            float f_sr, string f_ctype, float f_ar, float f_mintemp, float f_maxtemp, float f_alt, int farm_id)
        {
            string query = $"UPDATE `farms` SET `name` = '{f_name}', `area` = {f_a}, `distance` = {f_d}, `region` = '{f_region}', `woreda` = '{f_woreda}'," +
                $" `kebele` = '{f_kebele}', `humidity` = {f_h}, `seasonal_rainfall` = {f_sr} , `climate_type` = '{f_ctype}', `annual_rainfall` = {f_ar}, " +
                $"`min_temp` = {f_mintemp}, `max_temp` = {f_maxtemp}, `altitude` = {f_alt} WHERE `farms`.`id` = {farm_id}";
            if (IsConn == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteBlock(int bId,string bName)
        {
            string query = $"DELETE FROM `blocks` WHERE `blocks`.`id` = {bId};";
            if (IsConn == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(int blId, string bName)
        {
            string query = $"DELETE FROM `farms` WHERE `farms`.`id` = {blId}";
            if (IsConn == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
        }
        

    }
}
