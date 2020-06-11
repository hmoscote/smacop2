using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
//using MySql.Data.MySqlClient;
using System.Configuration;
using smacop2.Entity;
using smacop2.Operaciones;

namespace smacop2.Operaciones
{
    class opconf
    {

        private static Entity.conf LoadConf(IDataReader reader)
        {
            conf item = new conf();
            item.tabla = Convert.ToString(reader["tabla"]);
            item.campo = Convert.ToString(reader["campo"]);
            item.valor = Convert.ToString(reader["valor"]);
            item.cargar = Convert.ToBoolean(reader["cform"]);
            return item;
        }

        public static Entity.conf GellIdConf(string Id)
        {
          
            conf list = null;

            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("configuracion_tabla", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@tabla", Id);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list = LoadConf(reader);
                }
            }
            return list;
        }


        public static int accion(Entity.conf c)
        {
            int rowsAffected = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();
                    MySqlCommand command = new MySqlCommand("SP_cargosconf", con);
                    command.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramId = new MySqlParameter("msj", MySqlDbType.Int32);
                    paramId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(paramId);

                    command.Parameters.AddWithValue("tlb", c.tabla);
                    command.Parameters.AddWithValue("cmp", c.campo);
                    command.Parameters.AddWithValue("vl", c.valor);
                    command.Parameters.AddWithValue("cfo", c.cargar);
                    command.ExecuteNonQuery();
                    rowsAffected = int.Parse(command.Parameters["msj"].Value.ToString());

                    if (rowsAffected == 1)
                        MessageBox.Show(mensajes.Msjconf, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show(mensajes.MsjProc0, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
