using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using smacop2.Entity;
using System.Configuration;

namespace smacop2.Operaciones
{
    class op_usuarios
    {

        public static List<usu> GellAllusu()
        {
            string sql = @"SELECT
                            usuarios.usu,
                            usuarios.act
                            FROM `usuarios`";
            List<usu> list = new List<usu>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadusu(reader));
                }
            }
            return list;
        }

        private static usu Loadusu(IDataReader reader)
        {
            usu item = new usu();
            item.nombre = Convert.ToString(reader["usu"]);
            item.act = Convert.ToBoolean(reader["act"]);
            return item;
        }
    }

}
