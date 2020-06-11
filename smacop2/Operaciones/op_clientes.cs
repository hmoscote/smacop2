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
    class op_clientes
    {
        // devuleve un lista de registros de los empleados de forma cliente
        public static List<clientes> GellAllClientes()
        {
            string sql = @"SELECT (case tcodclie when 'CC' then 'CEDULA DE CIUDADANIA' when 'NI' then 'NIT' when 'RU' then 'RUT' when 'PS' then 'PASAPORTE' when 'LM' then 'LIBRETA MILITAR' else 'N/A' end) AS tcodclie, codclie, nomclie, dirclie, telclie, celclie, emailclie, razclie 
                            FROM clientes 
                            ORDER BY nomclie ASC";
            List<clientes> list = new List<clientes>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(LoadClientes(reader));
                }
            }
            return list;
        }

        // devuleve un registro en particular
        public static clientes GellIdClientes(string c, int i)
        {
            clientes list = null;

            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                con.Open();
                MySqlCommand cmd;
                if (i== 1)
                    cmd = new MySqlCommand("SELECT * FROM clientes WHERE codclie like @cod", con);
                else
                    cmd = new MySqlCommand("SELECT * FROM clientes WHERE nomclie like @cod", con);

                cmd.Parameters.AddWithValue("cod", c);


                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list = LoadClientes(reader);
                }
            }
            return list;
        }


        // Escribe los valores en la clase empleados
        private static clientes LoadClientes(IDataReader reader)
        {
            clientes item = new clientes();
            item.cargo = Convert.ToString(reader["razclie"]);
            item.tipid = Convert.ToString(reader["tcodclie"]);
            item.cod = Convert.ToString(reader["codclie"]);
            item.nom = Convert.ToString(reader["nomclie"]);
            item.dir = Convert.ToString(reader["dirclie"]);
            item.tel = Convert.ToString(reader["telclie"]);
            item.cel = Convert.ToString(reader["celclie"]);
            item.email = Convert.ToString(reader["emailclie"]);
            return item;
        }


        // hace las operaciones correspondiente al procedimiento almacenado.
        public static int accion(clientes c, int op)
        {
            int rowsAffected = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();

                    MySqlCommand command = new MySqlCommand("SP_clientes", con);
                    command.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramId = new MySqlParameter("msj", MySqlDbType.Int32);
                    paramId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(paramId);

                    command.Parameters.AddWithValue("raz1", c.cargo);
                    command.Parameters.AddWithValue("tcod", c.tipid);
                    command.Parameters.AddWithValue("cod1", c.cod);
                    command.Parameters.AddWithValue("nom1", c.nom);
                    command.Parameters.AddWithValue("dir1", c.dir);
                    command.Parameters.AddWithValue("tel1", c.tel);
                    command.Parameters.AddWithValue("cel1", c.cel);
                    command.Parameters.AddWithValue("emai1", c.email);

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


        // devuleve un lista de registros de los empleados de forma servidor
        public static List<clientes> GellAllClientes2(string Id, int op)
        {
            List<clientes> list = new List<clientes>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();
                    //MySqlCommand cmd;

                    MySqlCommand cmd = new MySqlCommand("SP_buscar_clientes", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("cod", Id);
                    cmd.Parameters.AddWithValue("opc", op);
                    //if (op == 1)
                    //    cmd = new MySqlCommand("SELECT * FROM clientes WHERE codclie like @cod", con);
                    //else
                    //    cmd = new MySqlCommand("SELECT * FROM clientes WHERE nomclie like @cod", con);

                    //cmd.Parameters.AddWithValue("cod", Id);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                            list.Add(LoadClientes(reader));
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
