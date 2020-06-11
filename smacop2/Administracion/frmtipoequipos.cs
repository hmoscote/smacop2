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
using smacop2.Operaciones;
using System.Configuration;
using System.Runtime.InteropServices;

namespace smacop2.Administracion
{
    public partial class frmtipoequipos : Form
    {
        private string _id;
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
        public frmtipoequipos()
        {

            InitializeComponent();
            textBox1.KeyPress += new KeyPressEventHandler(textBox3_KeyPress);
            textBox2.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            textBox2.KeyDown += new KeyEventHandler(textBox2_KeyDown);
            this.KeyDown += new KeyEventHandler(frmtipoequipos_KeyDown);
        }

        public frmtipoequipos(string id)
            : this()
        {
            _id = id;
           
        }
        private bool validacion()
        {
            bool o = true;
            errorProvider1.Clear();

           
            if (string.IsNullOrEmpty(this.comboBox1.Text))
            {
                errorProvider1.SetError(comboBox1, "La Linea es Obligatoria");
                o = false;
            }

            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "El Código Obligatorio");
                o = false;
            }

            if (string.IsNullOrEmpty(this.textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "La Descripción es Obligatoria");
                o = false;
            }

            return o;
        }
        
            
        
        private void operacion()
        {
                foreach (Control e in this.Controls)
                {
                    if (e is TextBox || e is ComboBox || e is Button || e is CheckBox)
                        e.Enabled = false;
                }
               
                this.toolStripButton3.Visible = true;
                this.toolStripButton1.Visible = false;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!validacion()) return;

            tipo_equipos c = new tipo_equipos();
            c.tip = this.comboBox1.SelectedValue.ToString();
            c.cod = this.textBox1.Text;
            c.nom = this.textBox2.Text;

            int rowsAffected = 0;
            if (string.IsNullOrEmpty(_id))
                rowsAffected = op_tipo_equipos.accion(c, 1);
            else
            {
                this.textBox1.ReadOnly = true;
                rowsAffected = op_tipo_equipos.accion(c, 2);
            }

            if (rowsAffected > 0)
            {
                if (rowsAffected == 1)
                {
                    MessageBox.Show(mensajes.MsjProc1, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    operacion();
                    this.toolStripButton3.Visible = true;
                }
                if (rowsAffected == 2)
                {
                    MessageBox.Show(mensajes.MsjProc2, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    operacion();
                    this.toolStripButton3.Visible = true;
                }
                if (rowsAffected == 3)
                {
                    MessageBox.Show(mensajes.MsjProc3, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    operacion();
                    this.toolStripButton3.Visible = true;
                }
                if (rowsAffected == 4)
                {
                    if (MessageBox.Show(mensajes.MsjProc4 + " " + mensajes.MsjProc5, Application.ProductName.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        rowsAffected = op_tipo_equipos.accion(c, 2);
                    }

                }
            }
            else
            {
                MessageBox.Show(mensajes.MsjProc0, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void frmtipoequipos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1) toolStripButton3_Click(toolStripButton3, e);
            if (e.KeyCode == Keys.F4) MessageBox.Show("Imprime");
            if (e.KeyCode == Keys.F3) toolStripButton5_Click(toolStripButton5, e);
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) toolStripButton1_Click(toolStripButton1, e);
        }

        //private void textBox6_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.F2) toolStripButton1_Click(toolStripButton1, e);
        //}

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetterOrDigit(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsSeparator(e.KeyChar) || char.IsPunctuation(e.KeyChar))) e.Handled = true;
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }


        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))) e.Handled = true;

            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }


        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //_id = null;
            toolStripButton3.Visible = false;
            toolStripButton5.Visible = false;
            toolStripButton1.Visible = true;
           
            foreach (Control i in this.Controls)
            {
                if (i is TextBox || i is ComboBox)
                {
                    i.Enabled = true;
                    //i.Text = null;
                }


            }
            this.textBox1.ReadOnly = true;
            textBox1.Focus();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            _id = null;
            toolStripButton3.Visible = false;

            toolStripButton5.Visible = false;
            toolStripButton1.Visible = true;
          
            foreach (Control i in this.Controls)
            {
                if (i is TextBox || i is ComboBox)
                {
                    i.Enabled = true;
                    i.Text = null;
                }


            }

            this.textBox1.ReadOnly = false;
            textBox1.Focus();
        }

      

        private void frmtipoequipos_Load(object sender, EventArgs e)
        {
            IntPtr hmenu = GetSystemMenu(this.Handle, 0);
            int cnt = GetMenuItemCount(hmenu);
            // remove 'close' action
            RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            // remove extra menu line
            RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            DrawMenuBar(this.Handle);

            this.comboBox1.DataSource = op_documentos.linea();
            comboBox1.DisplayMember = "nom";
            comboBox1.ValueMember = "cod";
            comboBox1.Text = null;

            if (!string.IsNullOrEmpty(_id))
            {
                tipo_equipos c = op_tipo_equipos.GellIdtipoE(_id,1);
               
                if (c != null)
                {
                    this.Text = "Editando Tipo de Equipo: " + c.cod;
                    this.textBox1.Text = c.cod;
                    this.textBox2.Text = c.nom;
                    this.comboBox1.SelectedValue = c.tip;

                    toolStripButton5.Visible = true;
                    toolStripButton1.Visible = false;
                    
                    operacion();
                    toolStripButton3.Visible = false;
                    foreach (Control u in this.Controls)
                    {
                        if (u is Button)
                            u.Enabled = true;
                    }
                }
            }
            else
            {
                foreach (Control u in this.Controls)
                {
                    if (u is Button)
                        u.Enabled = false;
                }
                this.Text = "Nuevo Registro Tipo de Equipo";
                toolStripButton3.Visible = false;
                toolStripButton5.Visible = false;
                toolStripButton1.Visible = true;
                
            }
        }

       
    }
}
