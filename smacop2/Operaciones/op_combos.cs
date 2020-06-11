using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Windows.Forms;
using MySql.Data.MySqlClient;
//using MySql.Data.MySqlClient;
using System.Configuration;
using smacop2.Entity;

namespace smacop2.Operaciones
{
    class op_combos
    {

        private static combos LoadCombos(IDataReader reader)
        {
            combos item = new combos();
            item.cod = Convert.ToString(reader["cod"]);
            item.nom = Convert.ToString(reader["nom"]);
            return item;
        }
        private static combos2 LoadCombos2(IDataReader reader)
        {
            combos2 item = new combos2();
            item.cod = Convert.ToString(reader["cod"]);
            item.nom = Convert.ToString(reader["nom"]);
            item.cod1 = Convert.ToString(reader["ndto"]);
            return item;
        }

        private static combos2 LoadCombos3(IDataReader reader)
        {
            combos2 item = new combos2();
            item.cod = Convert.ToString(reader["cod"]);
            item.nom = Convert.ToString(reader["nom"]);
            item.cod1 = Convert.ToString(reader["ndto1"]); 
        
            return item;
        }

        public static List<combos> FillCombo(string sql)
        {
            //string sql = @"SELECT cod, nom FROM cargos ORDER BY nom ASC";
            List<combos> list = new List<combos>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(LoadCombos(reader));
                }
            }
            return list;
        }
        public static List<combos2> FillCombo2(string sql)
        {
            //string sql = @"SELECT cod, nom FROM cargos ORDER BY nom ASC";
            List<combos2> list = new List<combos2>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(LoadCombos2(reader));
                }
            }
            return list;
        }
        public static List<combos2> FillCombo3(string sql)
        {
            //string sql = @"SELECT cod, nom FROM cargos ORDER BY nom ASC";
            List<combos2> list = new List<combos2>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(LoadCombos3(reader));
                }
            }
            return list;
        }
    }
}
