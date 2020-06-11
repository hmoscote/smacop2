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
    class op_sql
    {

        public static string dpto(string mun)
        {
            string ndpto = null;
            try
            {
                string sql = @"SELECT dpto FROM mpio where cod=@cod";
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@cod", mun);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                            ndpto = Convert.ToString(reader["dpto"]);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ndpto;
        }

        public static string contar(string mun)
        {
            string a = null;
            try
            {
                //string sql = @"SELECT dpto FROM mpio where cod=@cod";
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(mun, conn);
                    cmd.CommandType = CommandType.Text;
                    //cmd.Parameters.AddWithValue(string.Concat("@",para), cod);
                    a = cmd.ExecuteScalar().ToString();

                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return a;
        }

        public static string dato(string sql, string campo, string parametro_interno, string parametro_externo)
        {
            string ndpto = null;
            try
            {

                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue(parametro_interno, parametro_externo);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                            ndpto = Convert.ToString(reader[campo]);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ndpto;
        }
        public static string parametro(string sql, string parametro_interno, string parametro_externo)
        {
            string nombre = null;
            try
            {

                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue(parametro_interno, parametro_externo);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                            nombre = Convert.ToString(reader[0]);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return nombre;
        }

       
        public static string parametro1(string sql)
        {
            string nombre = null;
            try
            {

                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                            nombre = Convert.ToString(reader[0]);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return nombre;
        }

        public static void parametro2(string sql)
        {
            int a = 0;
            try
            {

                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    a=cmd.ExecuteNonQuery();
                    if (a>0)
                        MessageBox.Show("Revise los cambios" , Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public static bool comprobar(string sql)
        {
           
            bool U = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                //using (MySqlConnection con = new MySqlConnection("server=localhost;User Id=hilder;password=Hilder123;database=logicop;Persist Security Info=True"))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    U = reader.HasRows;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de procedimiento: " + ex.Message , Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return U;
        }
    }
}

