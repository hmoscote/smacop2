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
    class op_proyectos
    {

        public static List<consulta_02> GellAllproyectosC()
        {
            string sql = @"select 
                           IFNULL(`lstsalidasmat`.`ccos`,'N/A') AS CCOS,
                            count(`lstsalidasmat`.`nrec`) as nrec,
                            sum(`lstsalidasmat`.`matfle`) as matfle,
                            sum(`lstsalidasmat`.`matflet`) as matflet
                            from `lstsalidasmat` 
                            where anul=0
                            group by 1;";
            List<consulta_02> list = new List<consulta_02>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(LoadproyectosC(reader));
                }
            }
            return list;
        }

                   public static List<consulta_02> GellAllproyectosCP1(int mes, int ano)
        {
            string sql = @"select 
                           IFNULL(`lstsalidasmat`.`ccos`,'N/A') AS CCOS,
                            count(`lstsalidasmat`.`nrec`) as nrec,
                            sum(`lstsalidasmat`.`matfle`) as matfle,
                            sum(`lstsalidasmat`.`matflet`) as matflet
                            from `lstsalidasmat` 
                            where anul=0 and year(fech)=@ano and month(fech)=@mes
                            group by 1;";
            List<consulta_02> list = new List<consulta_02>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@mes", mes);
                cmd.Parameters.AddWithValue("@ano", ano);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(LoadproyectosC(reader));
                }
            }
            return list;
        }

                   public static List<consulta_02> GellAllproyectosCP2(DateTime a, DateTime b)
                   {
                        string sql=null;
              
                            sql = @"select 
                            IFNULL(`lstsalidasmat`.`ccos`,'N/A') AS CCOS,
                            count(`lstsalidasmat`.`nrec`) as nrec,
                            sum(`lstsalidasmat`.`matfle`) as matfle,
                            sum(`lstsalidasmat`.`matflet`) as matflet
                            from `lstsalidasmat` 
                            where anul=0  and fech between @a and @b
                            group by 1;";
                       
                       List<consulta_02> list = new List<consulta_02>();
                       using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                       {
                           conn.Open();
                           MySqlCommand cmd = new MySqlCommand(sql, conn);
                           cmd.Parameters.AddWithValue("@a",a);
                           cmd.Parameters.AddWithValue("@b", b);
                      
                           MySqlDataReader reader = cmd.ExecuteReader();
                           if (reader.HasRows == true)
                           {
                               while (reader.Read())
                                   list.Add(LoadproyectosC(reader));
                           }
                       }
                       return list;
                   }


           private static consulta_02 LoadproyectosC(IDataReader reader)
           {
               consulta_02 item = new consulta_02();
               item.ccos= Convert.ToString(reader["ccos"]);
               item.nrec = Convert.ToInt32(reader["nrec"]);
               item.matf = Convert.ToDecimal(reader["matfle"]);
               item.matft = Convert.ToDecimal(reader["matflet"]);
             
               return item;
           }

        public static List<proyectos> GellAllproyectos()
        {
            string sql = @"SELECT
                            *
                            FROM `logicop`.`lievproy`
                            ORDER BY nomproy ASC";
            List<proyectos> list = new List<proyectos>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadproyectos(reader));
                }
            }
            return list;
        }

        // devuleve un registro en particular
        public static proyectos GellIdproyectos(string c, int i)
        {
            proyectos list = null;

            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd;
                if (i == 1)
                    cmd = new MySqlCommand("SELECT * FROM proyectos WHERE idproy like @cod", conn);
                else
                    cmd = new MySqlCommand("SELECT * FROM proyectos WHERE nomproy like @cod", conn);

                cmd.Parameters.AddWithValue("cod", c);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list = Loadproyectos(reader);
                }
            }

            return list;
        }


        // Escribe los valores en la clase proyectos

        private static proyectos Loadproyectos(IDataReader reader)
        {
            proyectos item = new proyectos();
            item.idproy = Convert.ToString(reader["idproy"]);
            item.nomproy = Convert.ToString(reader["nomproy"]);
            item.desproy = Convert.ToString(reader["desproy"]);
            item.contproy = Convert.ToString(reader["contproy"]);
            item.dirproy = Convert.ToString(reader["dirproy"]);
            item.respproy = Convert.ToString(reader["respproy"]);
            item.actproy = Convert.ToBoolean(reader["activo"]);
            item.tipproy= Convert.ToString(reader["tipproy"]);
            return item;
        }


        // hace las operaciones correspondiente al procedimiento almacenado.
        public static int accion(proyectos c, int op)
        {
            int rowsAffected = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();
                    MySqlCommand command = new MySqlCommand("SP_proyectos", con);
                    command.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramId = new MySqlParameter("msj", MySqlDbType.Int32);
                    paramId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(paramId);

                    command.Parameters.AddWithValue("tipproy1", c.tipproy);
                    command.Parameters.AddWithValue("idproy1", c.idproy);
                    command.Parameters.AddWithValue("nomproy1", c.nomproy);
                    command.Parameters.AddWithValue("desproy1", c.desproy);
                    command.Parameters.AddWithValue("contproy1", c.contproy);
                    command.Parameters.AddWithValue("dirproy1", c.dirproy);
                    command.Parameters.AddWithValue("respproy1", c.respproy);
                    command.Parameters.AddWithValue("actproy1", c.actproy);

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


        // devuleve un lista de registros de los proyectos de forma servidor
        public static List<proyectos> GellAllproyectos2(string Id, int op)
        {
            List<proyectos> list = new List<proyectos>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SP_BUSCAR_PROYECTO", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("cod1", Id);
                    cmd.Parameters.AddWithValue("opc", op);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                            list.Add(Loadproyectos(reader));
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
