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
    class op_cargos
    {


        // devuleve un lista de registros de los cargos de forma cliente
        public static List<cargos> GellAllCargos()
        {
            string sql = @"SELECT cod, nom, niv, sal FROM cargos ORDER BY nom ASC";
            List<cargos> list = new List<cargos>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(LoadCargos(reader));
                }
            }
            return list;
        }

        // devuleve un registro en particular
        public static cargos GellIdCargos(string c, int i)
        {
            cargos list = null;
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd;
                if (i == 1)
                    cmd = new MySqlCommand("SELECT * FROM cargos WHERE cod like @cod", conn);
                else
                    cmd = new MySqlCommand("SELECT * FROM cargos WHERE nom like @cod", conn);

                cmd.Parameters.AddWithValue("@cod", c);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list = LoadCargos(reader);
                }
            }
            return list;
        }


        // Escribe los valores en la clase cargos
        private static cargos LoadCargos(IDataReader reader)
        {
            cargos item = new cargos();
            item.cod = Convert.ToString(reader["cod"]);
            item.nom = Convert.ToString(reader["nom"]);
            item.niv = Convert.ToInt32(reader["niv"]);
            item.sal = Convert.ToDecimal(reader["sal"]);
            return item;
        }


        // hace las operaciones correspondiente al procedimiento almacenado.
        public static int accion(cargos c, int op)
        {
            int rowsAffected = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();
                    MySqlCommand command = new MySqlCommand("SP_cargos", con);
                    command.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramId = new MySqlParameter("msj", MySqlDbType.Int32);
                    paramId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(paramId);

                    command.Parameters.AddWithValue("cod1", c.cod);
                    command.Parameters.AddWithValue("nom1", c.nom);
                    command.Parameters.AddWithValue("niv1", c.niv);
                    command.Parameters.AddWithValue("sal1", c.sal);
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
        public static List<cargos> GellAllCargos2(string Id, int op)
        {
            List<cargos> list = new List<cargos>();
           
            try
            { 
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();
                    //MySqlCommand command;
                    //if (op == 1)
                    //    command = new MySqlCommand("SELECT * FROM cargos WHERE cod like @cod", con);
                    //else
                    //    command = new MySqlCommand("SELECT * FROM cargos WHERE nom like @cod", con);
                    MySqlCommand command = new MySqlCommand("SP_buscar_cargos", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("cod1", Id);
                    command.Parameters.AddWithValue("opc", op);
                    //command.CommandType = CommandType.Text;        
                    //command.Parameters.AddWithValue("cod", Id);
                    //command.Parameters.AddWithValue("opc", op);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                            list.Add(LoadCargos(reader));
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
