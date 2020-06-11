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
    class op_proveedores
    {
        // devuleve un lista de registros de los proveedores de forma cliente
        public static List<proveedores> GellAllproveedores()
        {
            string sql = @"SELECT (case tipc when 'CC' then 'CEDULA DE CIUDADANIA' when 'NI' then 'NIT' when 'RU' then 'RUT' when 'PS' then 'PASAPORTE' when 'LM' then 'LIBRETA MILITAR' else 'N/A' end) AS tipc, cod, nom, dir, tel, cel, email, cont, ciud FROM proveedores  ORDER BY nom ASC";
            List<proveedores> list = new List<proveedores>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list.Add(Loadproveedores(reader));
                }
            }
            return list;
        }

        public static List<cant_material> GellIdcanteraxproducto(string c)
        {

            List<cant_material> list = new List<cant_material>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd;

                cmd = new MySqlCommand("SELECT cantera, nom, material, costo FROM materiales_canteras mc join  materiales m on mc.material=m.cod WHERE cantera = @cod", conn);
                cmd.Parameters.AddWithValue("@cod", c);
             
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                       list.Add(Loadmaterialescantera(reader));
                }
            }

            return list;
        }

        public static cant_material GellIdcanteraxproducto1(string c, string m)
        {

            cant_material list = null;
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd;

                cmd = new MySqlCommand("SELECT cantera, nom, material, costo FROM materiales_canteras mc join materiales m on m.cod=mc.material  WHERE cantera = @cod and material=@mat", conn);
                cmd.Parameters.AddWithValue("@cod", c);
                cmd.Parameters.AddWithValue("@mat", m);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list=Loadmaterialescantera(reader);
                }
            }

            return list;
        }


        // devuleve un registro en particular
        public static proveedores GellIdproveedores(string c, int i)
        {
            proveedores list = null;

            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd;
                if (i == 1)
                    cmd = new MySqlCommand("SELECT * FROM proveedores WHERE cod = @cod", conn);
                else
                    cmd = new MySqlCommand("SELECT * FROM proveedores WHERE nom = @cod", conn);

                cmd.Parameters.AddWithValue("cod", c);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        list = Loadproveedores(reader);
                }
            }

            return list;
        }


        // Escribe los valores en la clase proveedores

        private static proveedores Loadproveedores(IDataReader reader)
        {
            proveedores item = new proveedores();
           
            item.cod = Convert.ToString(reader["cod"]);
            item.tipid = Convert.ToString(reader["tipc"]);
            item.nom = Convert.ToString(reader["nom"]);
            item.dir = Convert.ToString(reader["dir"]);
            item.tel = Convert.ToString(reader["tel"]);
            item.cel = Convert.ToString(reader["cel"]);
            item.email = Convert.ToString(reader["email"]);
            item.cont = Convert.ToString(reader["cont"]);
            item.mun = Convert.ToString(reader["ciud"]);
           
            return item;
        }
        private static cant_material Loadmaterialescantera(IDataReader reader)
        {
            cant_material item = new cant_material();

            item.cnt = Convert.ToString(reader["cantera"]);
            item.mat = Convert.ToString(reader["nom"]);
            item.costo = Convert.ToDecimal(reader["costo"]);
            item.cod = Convert.ToString(reader["material"]);
            return item;
        }

        // hace las operaciones correspondiente al procedimiento almacenado.
        public static int accion(proveedores c, int op)
        {
            int rowsAffected = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();
                    MySqlCommand command = new MySqlCommand("SP_proveedores", con);
                    command.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramId = new MySqlParameter("msj", MySqlDbType.Int32);
                    paramId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(paramId);

                    command.Parameters.AddWithValue("tipc", c.tipid);
                    command.Parameters.AddWithValue("cod1", c.cod);
                    command.Parameters.AddWithValue("nom1", c.nom);
                    command.Parameters.AddWithValue("dir1", c.dir);
                    command.Parameters.AddWithValue("tel1", c.tel);
                    command.Parameters.AddWithValue("cel1", c.cel);
                    command.Parameters.AddWithValue("email1", c.email);
                    command.Parameters.AddWithValue("cont1", c.cont);
                    command.Parameters.AddWithValue("ciud1", c.mun);
                
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


        // devuleve un lista de registros de los proveedores de forma servidor
        public static List<proveedores> GellAllproveedores2(string Id, int op)
        {
            List<proveedores> list = new List<proveedores>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();
                    //MySqlCommand cmd;
                    //if (op == 1)
                    //    cmd = new MySqlCommand("SELECT * FROM proveedores WHERE cod like @cod", con);
                    //else if (op == 2)
                    //    cmd = new MySqlCommand("SELECT * FROM proveedores WHERE nom like @cod", con);
                    //else
                    //    cmd = new MySqlCommand("SELECT * FROM proveedores WHERE CONT like @cod", con);

                    //cmd.Parameters.AddWithValue("cod", Id);
                    MySqlCommand cmd = new MySqlCommand("SP_buscar_proveedores", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("cod1", Id);
                    cmd.Parameters.AddWithValue("opc", op);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                            list.Add(Loadproveedores(reader));
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
