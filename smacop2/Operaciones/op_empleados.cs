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
    class op_empleados
    {
        // devuleve un lista de registros de los empleados de forma cliente
        public static List<empleados> GellAllempleados()
        {
            string sql = @"SELECT nom as codcargo, (case tcodempl when 'CC' then 'CEDULA DE CIUDADANIA' when 'NI' then 'NIT' when 'RU' then 'RUT' when 'PS' then 'PASAPORTE' when 'LM' then 'LIBRETA MILITAR' else 'N/A' end) AS tcodempl, codempl, nomempl, dirempl, telempl, celempl, emailempl, activo, fotoempl FROM empleados join cargos c on c.cod=empleados.codcargo ORDER BY nomempl ASC";
            List<empleados> list = new List<empleados>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadempleados(reader));
                }
            }
            return list;
        }
      
        // devuleve un registro en particular
        public static empleados GellIdempleados(string c, int i)
        {
            empleados list = null;

            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                con.Open();
                MySqlCommand cmd;
                if (i == 1)
                    cmd = new MySqlCommand("SELECT * FROM empleados WHERE codempl like @cod", con);
                else
                    cmd = new MySqlCommand("SELECT * FROM empleados WHERE nomempl like @cod", con);

                cmd.Parameters.AddWithValue("cod", c);


                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list = Loadempleados(reader);
                }
            }
            return list;
        }


        // Escribe los valores en la clase empleados
        private static empleados Loadempleados(IDataReader reader)
        {
            empleados item = new empleados();
            item.cargo = Convert.ToString(reader["codcargo"]);
            item.tipid = Convert.ToString(reader["tcodempl"]);
            item.cod = Convert.ToString(reader["codempl"]);
            item.nom = Convert.ToString(reader["nomempl"]);
            item.dir = Convert.ToString(reader["dirempl"]);
            item.tel = Convert.ToString(reader["telempl"]);
            item.cel = Convert.ToString(reader["celempl"]);
            item.email = Convert.ToString(reader["emailempl"]);
            item.act = Convert.ToBoolean(reader["activo"]);
            if (reader["fotoempl"] != DBNull.Value) 
                item.foto = (byte[])reader["fotoempl"];
            return item;
        }


        // hace las operaciones correspondiente al procedimiento almacenado.
        public static int accion(empleados c, int op)
        {
            int rowsAffected = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();

                    MySqlCommand command = new MySqlCommand("SP_empleados", con);
                    command.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramId = new MySqlParameter("msj", MySqlDbType.Int32);
                    paramId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(paramId);

                    command.Parameters.AddWithValue("codc", c.cargo);
                    command.Parameters.AddWithValue("tcod", c.tipid);
                    command.Parameters.AddWithValue("cod1", c.cod);
                    command.Parameters.AddWithValue("nom1", c.nom);
                    command.Parameters.AddWithValue("dir1", c.dir);
                    command.Parameters.AddWithValue("tel1", c.tel);
                    command.Parameters.AddWithValue("cel1", c.cel);
                    command.Parameters.AddWithValue("emai1", c.email);
                    command.Parameters.AddWithValue("act", c.act);
                    command.Parameters.AddWithValue("foto", c.foto);
                    
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
        public static List<empleados> GellAllempleados2(string Id, int op)
        {
            List<empleados> list = new List<empleados>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();
                    MySqlCommand command = new MySqlCommand("SP_buscar_empleados", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("cod", Id);
                    command.Parameters.AddWithValue("opc", op);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                            list.Add(Loadempleados(reader));
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
