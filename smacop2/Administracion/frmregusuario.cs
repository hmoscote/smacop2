using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using smacop2.Entity;
using smacop2.Administracion;
using smacop2.Operaciones;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Runtime.InteropServices;

namespace smacop2.Administracion
{
    public partial class frmregusuario : Form
    {
        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        private static extern IntPtr GetSystemMenu(IntPtr hwnd, int revert);
        [DllImport("user32.dll", EntryPoint = "GetMenuItemCount")]
        private static extern int GetMenuItemCount(IntPtr hmenu);
        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        private static extern int RemoveMenu(IntPtr hmenu, int npos, int wflags);
        [DllImport("user32.dll", EntryPoint = "DrawMenuBar")]
        private static extern int DrawMenuBar(IntPtr hwnd);
        private const int MF_BYPOSITION = 0x0400;
        private const int MF_DISABLED = 0x0002;
        public frmregusuario()
        {
            InitializeComponent();
        }
        private int a;
        public frmregusuario(int _a):this()
        {
            a = _a;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool validacion()
        {
            bool o = true;
            errorProvider1.Clear();


            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "El Usuario es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "La Contraseña es Obligatorio");
                o = false;
            }

            if (string.IsNullOrEmpty(this.textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "La Contraseña es Obligatorio");
                o = false;
            }
            return o;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = null;
            if (!validacion()) return;
            try
            {
                if (a == 1)
                {
                    if (textBox3.Text != textBox2.Text)
                    {
                        MessageBox.Show("La contraseña no concuerda", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                        sql = "insert into usuarios values (@usu,AES_ENCRYPT(@con,'84089809011053'),@pro,@act);";
                }
                else
                {
                    sql = "UPDATE usuarios SET usu=@usu, con=AES_ENCRYPT(@con1,'84089809011053') WHERE usu=@usu and AES_DECRYPT(con,'84089809011053')=@con";
                }

                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@usu", textBox1.Text);
                    cmd.Parameters.AddWithValue("@con", textBox2.Text);
                    cmd.Parameters.AddWithValue("@con1", textBox3.Text);
                    cmd.Parameters.AddWithValue("@act", 1);
                    cmd.Parameters.AddWithValue("@pro", 1);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                        MessageBox.Show("Registro Grabado, debe esperar que el administrador defina su rol", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("No se puedo Grabar", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error tecnico: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void frmregusuario_Load(object sender, EventArgs e)
        {

            //IntPtr hmenu = GetSystemMenu(this.Handle, 0);
            //int cnt = GetMenuItemCount(hmenu);
            //// remove 'close' action
            //RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            //// remove extra menu line
            //RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            //DrawMenuBar(this.Handle);
            if(a==2)
            this.label3.Text = "Nueva Contraseña:";
        }
    }
}
