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

namespace smacop2.Movimientos
{
    public partial class frmcalendario : Form
    {
        public frmcalendario()
        {
            InitializeComponent();
        }

        private void frmcalendario_Load(object sender, EventArgs e)
        {
            string sql = @"SELECT c.`tabla`, c.`diainicial`, c.`diafinal` FROM logicop.cohorte_tablas c where tabla='entrada';";
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        this.numericUpDown1.Value=Convert.ToInt32(reader[1]);
                        this.numericUpDown2.Value = Convert.ToInt32(reader[2]);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int rec = 0;
            try
            {

                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE `logicop`.`cohorte_tablas`SET`diainicial`=" + numericUpDown1.Value + ",`diafinal` = " + numericUpDown2.Value + " WHERE `tabla`='entrada';", conn);
                   cmd.CommandType=CommandType.Text;
                   rec=cmd.ExecuteNonQuery();

                    if (rec>0)
                    {
                        MessageBox.Show(mensajes.MsjProc2, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
