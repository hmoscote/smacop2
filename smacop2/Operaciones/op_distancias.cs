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
    class op_distancias
    {

        public class distancias
        {

            public string tip1 { get; set; }
            public string lug1 { get; set; }
            public string tip2 { get; set; }
            public string lug2 { get; set; }
            public decimal cant { get; set; }
        }

        private static distancias Loaddistancias(IDataReader reader)
        {
            distancias item = new distancias();
      
            item.tip1= Convert.ToString(reader["tip1"]);
            item.lug1 = Convert.ToString(reader["lug1"]);
            item.tip2 = Convert.ToString(reader["tip2"]);
            item.lug2= Convert.ToString(reader["lug2"]);
            item.cant = Convert.ToDecimal(reader["dist"]);
            return item;
        }


        public static List<distancias> GellAlldistancias()
        {
            string sql = @"SELECT distancias.tip1,distancias.lug1,distancias.tip2,distancias.lug2, distancias.dist
                           FROM `distancias`";
            List<distancias> list = new List<distancias>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loaddistancias(reader));
                }
            }
            return list;
        }

        public static int accion(distancias c, int op)
        {
            int rowsAffected = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();

                    MySqlCommand command = new MySqlCommand("SP_distancias", con);
                    command.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramId = new MySqlParameter("msj", MySqlDbType.Int32);
                    paramId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(paramId);

                    command.Parameters.AddWithValue("tipt1", c.tip1);
                    command.Parameters.AddWithValue("lugt1", c.lug1);
                    command.Parameters.AddWithValue("tipt2", c.tip2);
                    command.Parameters.AddWithValue("lugt2", c.lug2);
                    command.Parameters.AddWithValue("distt", c.cant);
                   

                    command.Parameters.AddWithValue("opc", op);
                    command.ExecuteNonQuery();
                    rowsAffected = int.Parse(command.Parameters["msj"].Value.ToString());


                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex1)
            {
                MessageBox.Show("Error: " + ex1.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return rowsAffected;
        }


    }
}
