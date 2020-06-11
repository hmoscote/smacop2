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
    class op_rangos
    {
        public static List<rango> GellAllrango()
        {
            string sql = @"SELECT
                            `rangos_mat`.`codmat`,
                            `rangos_mat`.`rini`,
                            `rangos_mat`.`rfin`,
                            `rangos_mat`.`precio`
                            FROM `logicop`.`rangos_mat`;";
            List<rango> list = new List<rango>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadrango(reader));
                }
            }
            return list;
        }

        public static List<rango> GellAllrango3(  string sql)
        {
          
            List<rango> list = new List<rango>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadrango(reader));
                }
            }
            return list;
        }

        // devuleve un registro en particular
        public static rango GellIdrango(string c)
        {
            rango list = null;

            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd;
                cmd = new MySqlCommand("SELECT * FROM rangos_mat WHERE codmat like @cod", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@cod", c);


                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list = Loadrango(reader);
                }
            }

            return list;
        }


        // Escribe los valores en la clase cargos
        private static rango Loadrango(IDataReader reader)
        {
            rango item = new rango();
            item.mat = Convert.ToString(reader["codmat"]);
            item.rini = Convert.ToInt32(reader["rini"]);
            item.rfin = Convert.ToInt32(reader["rfin"]);
            item.precio = Convert.ToDecimal(reader["precio"]);
            return item;
        }
        public static List<rangofle> GellAllrango4(string sql)
        {

            List<rangofle> list = new List<rangofle>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadrango1(reader));
                }
            }
            return list;
        }

        private static rangofle Loadrango1(IDataReader reader)
        {
            rangofle item = new rangofle();
            item.cod = Convert.ToString(reader["cod"]);
            item.vini = Convert.ToDecimal(reader["vini"]);
            item.vfin = Convert.ToDecimal(reader["vfin"]);
            item.precio = Convert.ToDecimal(reader["costo"]);
            return item;
        }
        // hace las operaciones correspondiente al procedimiento almacenado.
        public static int accion(rango c, int op)
        {
            int rowsAffected = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();
                    MySqlCommand command = new MySqlCommand("SP_rango", con);
                    command.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramId = new MySqlParameter("msj", MySqlDbType.Int32);
                    paramId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(paramId);

                    command.Parameters.AddWithValue("codmat1", c.mat);
                    command.Parameters.AddWithValue("rini1", c.rini);
                    command.Parameters.AddWithValue("rfin1", c.rfin);
                     command.Parameters.AddWithValue("precio1", c.precio);
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

        public static int accion1(rangofle c, int op)
        {
            int rowsAffected = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();
                    MySqlCommand command = new MySqlCommand("SP_rangos_fle", con);
                    command.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramId = new MySqlParameter("msj", MySqlDbType.Int32);
                    paramId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(paramId);

                    //command.Parameters.AddWithValue("cod1", null);
                    command.Parameters.AddWithValue("vini1", c.vini);
                    command.Parameters.AddWithValue("vfin1", c.vfin);
                    command.Parameters.AddWithValue("costo1", c.precio);
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
        public static List<rango> GellAllrango2(string Id, int op)
        {
            List<rango> list = new List<rango>();
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
                            list.Add(Loadrango(reader));
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
