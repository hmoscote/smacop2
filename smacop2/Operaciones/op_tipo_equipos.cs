using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using smacop2.Entity;
using smacop2.Operaciones;
using System.Runtime.InteropServices;


namespace smacop2.Operaciones
{
    class op_tipo_equipos
    {

        // devuleve un lista de registros de los cargos de forma cliente
        public static List<tipo_equipos> GellAlltipoE()
        {
            string sql = @"SELECT cod, nom, tip FROM tipo_equipo ORDER BY cod ASC";
            List<tipo_equipos> list = new List<tipo_equipos>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(LoadtipoE(reader));
                }
            }
            return list;
        }

        // devuleve un registro en particular
        public static tipo_equipos GellIdtipoE(string c, int i)
        {
            tipo_equipos list = null;

            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd;
                if (i == 1)
                    cmd = new MySqlCommand("SELECT * FROM tipo_equipo WHERE cod like @cod", conn);
                else
                    cmd = new MySqlCommand("SELECT * FROM tipo_equipo WHERE nom like @cod", conn);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@cod", c);


                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list = LoadtipoE(reader);
                }
            }

            return list;
        }


        // Escribe los valores en la clase cargos

        private static tipo_equipos LoadtipoE(IDataReader reader)
        {
            tipo_equipos item = new tipo_equipos();
            item.cod = Convert.ToString(reader["cod"]);
            item.nom = Convert.ToString(reader["nom"]);
            item.tip = Convert.ToString(reader["tip"]);
            return item;
        }


        // hace las operaciones correspondiente al procedimiento almacenado.
        public static int accion(tipo_equipos c, int op)
        {
            int rowsAffected = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();
                    MySqlCommand command = new MySqlCommand("SP_tip_maquinas", con);
                    command.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramId = new MySqlParameter("msj", MySqlDbType.Int32);
                    paramId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(paramId);

                    command.Parameters.AddWithValue("cod1", c.cod);
                    command.Parameters.AddWithValue("nom1", c.nom);
                    command.Parameters.AddWithValue("tip1", c.tip);
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


        // devuleve un lista de registros de los cargos de forma servidor
        public static List<tipo_equipos> GellAlltipoE2(string Id, int op)
        {
            List<tipo_equipos> list = new List<tipo_equipos>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();
                    MySqlCommand command = new MySqlCommand("SP_buscar_cargos", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("cod", Id);
                    command.Parameters.AddWithValue("opc", op);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                            list.Add(LoadtipoE(reader));
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return list;
        }
    }
}
