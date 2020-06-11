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

namespace smacop2.Operaciones
{
    class autocompletar
    {
        public static DataTable LoadDataTable()
        {
            DataTable dt = new DataTable();

            string connectionstring = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connectionstring);
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn;
           
            //string t1= " tipeq='" + op_sql.parametro1("select transp1 from conf_transp where id=1")+"'";
            //string t2 = "or tipeq='" + op_sql.parametro1("select transp2 from conf_transp where id=1")+"'";
            //string t3 = "or tipeq='" + op_sql.parametro1("select transp3 from conf_transp where id=1")+"'";

            command.CommandText = "SELECT * FROM equipos"; // where " + t1+ t2 + t3;
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);

            if (dt.Rows.Count <= 0)
            {
                //command.CommandText = "SELECT * FROM equipos";
                da = new MySqlDataAdapter(command);
                da.Fill(dt);
            }
            return dt;
        }

        //public static DataTable LoadDataTableempleados()
        //{
        //    DataTable dt = new DataTable();

        //    string connectionstring = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        //    MySqlConnection conn = new MySqlConnection(connectionstring);
        //    MySqlCommand command = new MySqlCommand();
        //    command.Connection = conn;
        //    command.CommandText = "SELECT codempl, nomempl FROM empleados";
        //    MySqlDataAdapter da = new MySqlDataAdapter(command);
        //    da.Fill(dt);
        //    return dt;
        //}
        //public static AutoCompleteStringCollection LoadAutoComplete()
        //{
        //    DataTable dt = LoadDataTable();
        //    AutoCompleteStringCollection stringCol = new AutoCompleteStringCollection();
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        stringCol.Add(Convert.ToString(row["codeq"]));
        //    }
        //    return stringCol;
        //}

        public static AutoCompleteStringCollection LoadAutoComplete()
        {
            DataTable dt = LoadDataTable();
            AutoCompleteStringCollection stringCol = new AutoCompleteStringCollection();
            foreach (DataRow row in dt.Rows)
            {
                stringCol.Add(Convert.ToString(row["codeq"]));
            }
            return stringCol;
        }

    }
}
