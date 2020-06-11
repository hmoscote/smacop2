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
    class op_equipos
    {
        public static List<equipos> GellAllequipos()
        {
//            string sql = @"SELECT
//                        `equipos`.`codeq`,
//                        `equipos`.`deseq`,
//                        `equipos`.`modeq`,
//                        `equipos`.`poteq`,
//                        `equipos`.`capeq`,
//                        `equipos`.`comeq`,
//                        `equipos`.`sereq`,
//                        `equipos`.`tipeq`,
//                        `equipos`.`plaeq`,
//                        `equipos`.`hmeq`,
//                        `equipos`.`kmeq`,
//                        `equipos`.`opeeq`,
//                        `equipos`.`vhoeq`,
//                        `equipos`.`mhoeq`,
//                         `equipos`.`medeq`
//                        FROM `logicop`.`equipos` order by 1 ;";
            string sql = @"SELECT * FROM `logicop`.`lista_equipos` order by 1 ;";
            List<equipos> list = new List<equipos>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadequipos(reader));
                }
            }
            return list;
        }

        // devuleve un registro en particular
        public static equipos GellIdequipos(string c, int i)
        {
            equipos list = null;

            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand command;
                if (i == 1)
                    command = new MySqlCommand("SELECT * FROM equipos WHERE codeq like @cod", conn);
                else
                    command = new MySqlCommand("SELECT * FROM equipos WHERE nomeq like @cod", conn);

                command.Parameters.AddWithValue("@cod", c);


                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list = Loadequipos(reader);
                }
            }
            return list;
        }


        // Escribe los valores en la clase equipos
        private static equipos Loadequipos(IDataReader reader)
        {
            equipos item = new equipos();
            item.cod = Convert.ToString(reader["codeq"]);
            item.des = Convert.ToString(reader["deseq"]);
            item.mod = Convert.ToString(reader["modeq"]);
            item.pot = Convert.ToString(reader["poteq"]);
            item.cap = Convert.ToString(reader["capeq"]);
            item.cmb = Convert.ToString(reader["comeq"]);
            item.ser = Convert.ToString(reader["sereq"]);
            item.teq = Convert.ToString(reader["tipeq"]);
            item.pla = Convert.ToString(reader["plaeq"]);
            item.hms = Convert.ToDecimal(reader["hmeq"]);
            item.kms = Convert.ToDecimal(reader["kmeq"]);
            item.ope = Convert.ToString(reader["opeeq"]);
            item.vho = Convert.ToDecimal(reader["vhoeq"]);
            item.csh = Convert.ToDecimal(reader["mhoeq"]);
            item.ccg = Convert.ToDecimal(reader["medeq"]);
            return item;
        }


        // hace las operaciones correspondiente al procedimiento almacenado.
        public static int accion(equipos c, int op)
        {
            int rowsAffected = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();

                    MySqlCommand command = new MySqlCommand("SP_equipos", con);
                    command.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramId = new MySqlParameter("msj", MySqlDbType.Int32);
                    paramId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(paramId);

                    command.Parameters.AddWithValue("codeq1", c.cod);
                    command.Parameters.AddWithValue("deseq1", c.des);
  
                    command.Parameters.AddWithValue("modeq1", c.mod);
                    command.Parameters.AddWithValue("poteq1", c.pot);
                    command.Parameters.AddWithValue("capeq1", c.cap);
                    command.Parameters.AddWithValue("comeq1", c.cmb);
                    command.Parameters.AddWithValue("sereq1", c.ser);
                    command.Parameters.AddWithValue("tipeq1", c.teq);
                    command.Parameters.AddWithValue("plaeq1", c.pla);
                    command.Parameters.AddWithValue("hmeq1", c.hms);
                    command.Parameters.AddWithValue("kmeq1", c.kms);
                    command.Parameters.AddWithValue("opeeq1", c.ope);
                    command.Parameters.AddWithValue("vhoeq1", c.vho);
                    command.Parameters.AddWithValue("mhoeq1", c.csh);
                    command.Parameters.AddWithValue("medeq1", c.ccg);

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


        // devuleve un lista de registros de los equipos de forma servidor
        public static List<equipos> GellAllequipos2(string Id, int op)
        {
            List<equipos> list = new List<equipos>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    conn.Open();
                    //MySqlCommand command;
                    MySqlCommand command = new MySqlCommand("SP_buscar_equipo", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("cod1", Id);
                    command.Parameters.AddWithValue("opc", op);

                    //command.Parameters.AddWithValue("cod", Id);

                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                            list.Add(Loadequipos(reader));
                    }
                    else
                    {
                        MessageBox.Show("No existe información concerniente al parámetro", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
