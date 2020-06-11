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
using Microsoft.VisualBasic;

using System.IO;
using System.Drawing.Imaging;
using System.Reflection;
using smacop2.Entity;
using smacop2.Operaciones;
using System.Runtime.InteropServices;

namespace smacop2.Administracion
{
    public partial class frmequiposdet : Form
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
        public frmequiposdet()
        {
            InitializeComponent();
            this.textBox8.KeyPress+=new KeyPressEventHandler(numero);
            this.textBox9.KeyPress += new KeyPressEventHandler(numero);
            this.textBox10.KeyPress += new KeyPressEventHandler(numero_decimal);
            this.textBox6.KeyPress += new KeyPressEventHandler(numero_decimal);
            this.textBox3.KeyPress += new KeyPressEventHandler(numero);
            this.textBox1.KeyPress += new KeyPressEventHandler(numero_decimal);
            this.textBox2.KeyPress += new KeyPressEventHandler(letras_numero);
            this.textBox7.KeyPress += new KeyPressEventHandler(letras_numero);
            this.textBox11.KeyPress += new KeyPressEventHandler(letras_numero);
            this.textBox4.KeyPress += new KeyPressEventHandler(letras_numero);
            this.textBox5.KeyPress += new KeyPressEventHandler(letras_numero);
            this.textBox4.KeyDown+=new KeyEventHandler(textBox4_KeyDown);
            this.maskedTextBox1.KeyPress += new KeyPressEventHandler(letras_numero);
        }
      
        public frmequiposdet(string id):this()
        {
            _id = id;
        }
        private void textBox4_KeyDown(object sender,KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) toolStripButton1_Click(toolStripButton1, e);
        }

        private void numero(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))) e.Handled = true;
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void numero_decimal(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsPunctuation(e.KeyChar))) e.Handled = true;
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
        private void letras_numero(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetterOrDigit(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsSeparator(e.KeyChar) || char.IsPunctuation(e.KeyChar))) e.Handled = true;
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool validacion()
        {
            bool o = true;
            errorProvider1.Clear();

            if (string.IsNullOrEmpty(this.comboBox1.Text))
            {
                errorProvider1.SetError(comboBox1, "El Tipo es Obligatorio");
                o = false;
            }

            if (string.IsNullOrEmpty(this.comboBox2.Text))
            {
                errorProvider1.SetError(comboBox2, "El Combustible es Obligatorio");
                o = false;
            }

            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "El CCMB GL/KM: es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.textBox9.Text))
            {
                errorProvider1.SetError(textBox9, "El CCMB GL/HORA es Obligatorio");
                o = false;
            }

            if (string.IsNullOrEmpty(this.textBox8.Text))
            {
                errorProvider1.SetError(textBox8, "El CALQ HORA es Obligatorio");
                o = false;
            }

            //if (string.IsNullOrEmpty(this.textBox2.Text))
            //{
            //    errorProvider1.SetError(textBox2, "La descripción es Obligatorio");
            //    o = false;
            //}


            return o;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!validacion()) return;

            equipos c = new equipos();
            c.teq = this.comboBox1.SelectedValue.ToString();
           
            c.cod = this.maskedTextBox1.Text;
            c.des = this.textBox2.Text;
            c.cap = this.textBox5.Text;
            c.cmb = this.comboBox2.SelectedValue.ToString();
            c.ope = this.comboBox3.SelectedValue.ToString();
            c.pla = this.textBox7.Text;
            c.mod = this.textBox3.Text;
            c.ser = this.textBox11.Text;
            c.hms= 0;
            c.kms = 0;
            c.ccg = decimal.Parse(this.textBox1.Text);
    
            c.pot = this.textBox4.Text;

            c.vho = decimal.Parse(this.textBox8.Text);
            c.csh = decimal.Parse(this.textBox9.Text);


            int rowsAffected = 0;
            if (string.IsNullOrEmpty(_id))
                rowsAffected = op_equipos.accion(c, 1);
            else
            {
                this.maskedTextBox1.ReadOnly = true;
                c.hms = decimal.Parse(this.textBox10.Text);
                c.kms = decimal.Parse(this.textBox6.Text);
                rowsAffected = op_equipos.accion(c, 2);
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
                        rowsAffected =  op_equipos.accion(c, 2);
                    }

                }
            }
            else
            {
                MessageBox.Show(mensajes.MsjProc0, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void operacion()
        {
            foreach (Control e in this.Controls)
            {
                if (e is TextBox || e is ComboBox || e is Button || e is MaskedTextBox )
                    e.Enabled = false;
            }

            foreach (Control e in this.groupBox1.Controls)
            {
                if ( e is TextBox)
                    e.Enabled = false;
            }

            this.toolStripButton3.Visible = true;
            this.toolStripButton1.Visible = false;
        }

        private void frmequiposdet_Load(object sender, EventArgs e)
        {

            IntPtr hmenu = GetSystemMenu(this.Handle, 0);
            int cnt = GetMenuItemCount(hmenu);
            // remove 'close' action
            RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            // remove extra menu line
            RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            DrawMenuBar(this.Handle);

            comboBox1.DataSource = op_combos.FillCombo("select cod, nom from tipo_equipo");
            comboBox1.DisplayMember = "nom";
            comboBox1.ValueMember = "cod";
            comboBox1.Text = null;

            comboBox2.DataSource = op_combos.FillCombo("select cod, nom from combustible");
            comboBox2.DisplayMember = "nom";
            comboBox2.ValueMember = "cod";
            comboBox2.Text = null;

            comboBox3.DataSource = op_combos.FillCombo("select codempl as cod, nomempl as nom from empleados");
            comboBox3.DisplayMember = "nom";
            comboBox3.ValueMember = "cod";
            comboBox3.Text = null;
   

            if (!string.IsNullOrEmpty(_id))
            {
                equipos c = op_equipos.GellIdequipos(_id, 1);

                if (c != null)
                {
                   
                    this.maskedTextBox1.Text = c.cod;
                    this.textBox2.Text = c.des;
                    this.textBox5.Text = c.cap;
                    
                    this.textBox7.Text = c.pla;
                    this.textBox3.Text = c.mod;
                    this.textBox11.Text = c.ser;
                    this.textBox10.Text = c.hms.ToString();
                    this.textBox6.Text = c.kms.ToString();
                    this.textBox4.Text = c.pot;
                    this.textBox8.Text = c.vho.ToString();
                    this.textBox9.Text = c.csh.ToString();
                    this.comboBox1.SelectedValue = c.teq;
                    this.comboBox2.SelectedValue = c.cmb.ToString();
                    this.comboBox3.SelectedValue = c.ope;
                    this.textBox1.Text = c.ccg.ToString();
                    toolStripButton5.Visible = true;
                    toolStripButton1.Visible = false;
                    
                    operacion();
                    toolStripButton3.Visible = false;
                  
                    //foreach (Control u in this.Controls)
                    //{
                    //    if (u is Button)
                    //        u.Enabled = true;
                    //}
                }
            }
            else
            {
                //foreach (Control u in this.Controls)
                //{
                //    if (u is Button)
                //        u.Enabled = false;
                //}

                toolStripButton3.Visible = false;
                toolStripButton5.Visible = false;
                toolStripButton1.Visible = true;
              
            }

          

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (maskedTextBox1.Enabled == true && this.maskedTextBox1.MaskFull==true)
            {
                string a=op_sql.parametro1("select LPAD(cast((max(right(codeq,2)) + 1) as CHAR) ,2,'0') as nummax from equipos where tipeq='" + comboBox1.SelectedValue.ToString()+"'");
                maskedTextBox1.Text = comboBox1.SelectedValue.ToString() + a;
                maskedTextBox1.Focus();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox9.Focus();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox7.Focus();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            toolStripButton3.Visible = false;
            toolStripButton5.Visible = false;
            toolStripButton1.Visible = true;
      
           
            foreach (Control W in this.Controls)
            {
                if (W is TextBox || W is ComboBox || W is Button || W is MaskedTextBox )
                   W.Enabled = true;
            }

            foreach (Control W in this.groupBox1.Controls)
            {
                if ( W is TextBox)
                    W.Enabled = true;
            }

                //foreach (Control r in this.groupBox1.Controls)
                //{
                //    if (r is TextBox)
                //    {
                //        r.Enabled = true;
                //        //r.Text = null;
                //    }

                //}
          
            this.maskedTextBox1.ReadOnly = true;
            maskedTextBox1.Focus();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            _id = null;
            toolStripButton3.Visible = false;

            toolStripButton5.Visible = false;
            toolStripButton1.Visible = true;
           
            foreach (Control i in this.Controls)
            {
                if (i is TextBox || i is ComboBox || i is MaskedTextBox)
                {
                    i.Enabled = true;
                    i.Text = null;
                }

                foreach (Control r in this.groupBox1.Controls)
                {
                    if (r is TextBox)
                    {
                        r.Enabled = true;
                        r.Text = null;
                    }

                }
            }

            this.maskedTextBox1.ReadOnly = false;
            maskedTextBox1.Focus();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
           
           
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
         

            comboBox2.DataSource = op_combos.FillCombo("select cod, nom from combustible");
            comboBox2.DisplayMember = "nom";
            comboBox2.ValueMember = "cod";
            comboBox2.Text = null;

           
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Administracion.frmtipoequipos f = new Administracion.frmtipoequipos();
            f.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            comboBox1.DataSource = op_combos.FillCombo("select cod, nom from tipo_equipo");
            comboBox1.DisplayMember = "nom";
            comboBox1.ValueMember = "cod";
            comboBox1.Text = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Administracion.frmempleadosdet f = new Administracion.frmempleadosdet();
            f.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            comboBox3.DataSource = op_combos.FillCombo("select codempl as cod, nomempl as nom from empleados");
            comboBox3.DisplayMember = "nom";
            comboBox3.ValueMember = "cod";
            comboBox3.Text = null;
        }

      
    }
}

