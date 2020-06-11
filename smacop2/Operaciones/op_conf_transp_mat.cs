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
    class op_conf_transp_mat
    {
        public class ctranspmat
        {
            public int id { get; set; }
            public decimal valfle { get; set; }
            public string ttransp1 { get; set; }
            public string ttransp2 { get; set; }
            public string ttransp3 { get; set; }
            public bool entradas { get; set; }
            public bool salidas { get; set; }
        }

        public static ctranspmat GellIdtranspmat ()
        {
            ctranspmat list = null;
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd=null;
                
                    cmd = new MySqlCommand("SELECT * FROM conf_transp", conn);
                

                //cmd.Parameters.AddWithValue("@cod", c);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list = Loadtranspmat(reader);
                }
            }
            return list;
        }


        // Escribe los valores en la clase cargos
        private static ctranspmat Loadtranspmat(IDataReader reader)
        {
            ctranspmat item = new ctranspmat();
            item.id = Convert.ToInt32(reader["id"]);
            item.valfle = Convert.ToDecimal(reader["flete"]);
            item.ttransp1 = Convert.ToString(reader["transp1"]);
            item.ttransp2 = Convert.ToString(reader["transp2"]);
            item.ttransp3 = Convert.ToString(reader["transp3"]);
            item.entradas = Convert.ToBoolean(reader["entrada"]);
            item.salidas = Convert.ToBoolean(reader["salida"]);
            return item;
        }


        public static int accion(ctranspmat c, int op)
        {
            int rowsAffected = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();
                    MySqlCommand command = new MySqlCommand("SP_conf_transp_mat", con);
                    command.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramId = new MySqlParameter("msj", MySqlDbType.Int32);
                    paramId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(paramId);

                    command.Parameters.AddWithValue("id1", c.id);
                    command.Parameters.AddWithValue("flete1", c.valfle);
                    command.Parameters.AddWithValue("transp11", c.ttransp1);
                    command.Parameters.AddWithValue("transp22", c.ttransp2);
                    command.Parameters.AddWithValue("transp33", c.ttransp3);
                    command.Parameters.AddWithValue("entradas11", c.entradas);
                    command.Parameters.AddWithValue("salidas11", c.salidas);
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
