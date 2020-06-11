using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.VisualBasic;

using System.IO;
using System.Drawing.Imaging;
using System.Reflection;
using smacop2.Entity;
using smacop2.Operaciones;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;

namespace smacop2.Administracion
{
    public partial class frmmaterialesdet : Form
    {
        //private string _id;
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
        public frmmaterialesdet()
        {
            InitializeComponent();
            textBox1.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            textBox2.KeyPress += new KeyPressEventHandler(textBox3_KeyPress);
            textBox2.KeyDown += new KeyEventHandler(textBox2_KeyDown);
            this.textBox2.LostFocus+=new EventHandler(textBox2_LostFocus);
            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(dataGridView1_CellContentClick);
            dataGridView1.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView1_CellPainting);
          

        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dataGridView1.Columns[e.ColumnIndex].Name == "eli" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = this.dataGridView1.Rows[e.RowIndex].Cells["eli"] as DataGridViewButtonCell;
                Icon icoAtomico = new Icon(Environment.CurrentDirectory + @"\delete_16x.ico");

                e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 3, e.CellBounds.Top + 3);

                this.dataGridView1.Rows[e.RowIndex].Height = icoAtomico.Height + 8;
                this.dataGridView1.Columns[e.ColumnIndex].Width = icoAtomico.Width + 8;

                e.Handled = true;
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.CurrentRow as DataGridViewRow;
                    //this.toolStripLabel1.Text = row.Cells[0].Value.ToString();
                    if (this.dataGridView1.Columns[e.ColumnIndex].Name == "eli")
                    {
                        int a = Convert.ToInt32(MessageBox.Show("Está seguro que desea eliminar el registro", Application.ProductName.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Information));
                        if (a == 6)
                        {
                            rango c = new rango();
                            c.mat = row.Cells[0].Value.ToString();
                            c.rini = int.Parse(row.Cells[1].Value.ToString());
                            c.rfin = int.Parse(row.Cells[2].Value.ToString());
                       
                            op_rangos.accion(c, 3);
                            dataGridView1.AutoGenerateColumns = false;
                            dataGridView1.DataSource = op_rangos.GellAllrango3("select * from rangos_mat where codmat='" + c.mat+ "'");
                        }
                    }

                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1451)
                    MessageBox.Show("El dato está siendo utilizado, por lo tanto es imposible de eliminar", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    MessageBox.Show(ex.Message, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void textBox2_LostFocus(object sender, EventArgs e)
        {
           this.textBox2.Text= this.textBox2.Text.PadLeft(4, '0');
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
        private void frmmateriales_Load(object sender, EventArgs e)
        {
            IntPtr hmenu = GetSystemMenu(this.Handle, 0);
            int cnt = GetMenuItemCount(hmenu);
            // remove 'close' action
            RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            // remove extra menu line
            RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            DrawMenuBar(this.Handle);

            comboBox1.DataSource = op_documentos.tipo_produccion();
            comboBox1.DisplayMember = "nom";
            comboBox1.ValueMember = "cod";
            comboBox1.Text = null;
            this.textBox3.Text = null;


            if (!string.IsNullOrEmpty(_id))
            {
                materiales c = op_materiales.GellIdMateriales(_id, 1);

                if (c != null)
                {
                    this.Text = "Editando Material: " + c.cod + "-" + c.nom;
                    this.textBox1.Text = c.nom;
                    this.textBox3.Text = c.cod.Substring(1,2);
                    this.textBox2.Text = c.cod.Substring(4, 4);
                    this.comboBox1.SelectedValue = c.tip;
                    this.dataGridView1.AutoGenerateColumns = false;
                    this.dataGridView1.DataSource = op_rangos.GellAllrango3("select * from rangos_mat where codmat='"+c.cod+"'"); ;
                    this.panel1.Enabled = false;
                    //toolStripButton5.Visible = true;
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
                this.Text = "Nuevo Registro de Material";
                this.panel1.Enabled = false;
                toolStripButton3.Visible = false;
                toolStripButton5.Visible = false;
                toolStripButton1.Visible = true;

            }
        }
        private string _id;
        public frmmaterialesdet(string id)
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

            if (string.IsNullOrEmpty(this.textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "El Código Obligatorio");
                o = false;
            }

            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "La Descripción es Obligatoria");
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

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            _id = null;
            toolStripButton3.Visible = false;
            this.panel1.Enabled = false;
            toolStripButton5.Visible = false;
            toolStripButton1.Visible = true;

            foreach (Control i in this.Controls)
            {
                if (i is TextBox || i is ComboBox || i is MaskedTextBox)
                {
                    i.Enabled = true;
                    i.Text = null;
                }

            }

            this.dataGridView1.DataSource = op_rangos.GellAllrango3("select * from rangos_mat where codmat='" + string.Concat(textBox3.Text.Trim(), "-", textBox2.Text.Trim()) + "'"); ;
            this.textBox2.ReadOnly =false;
            this.textBox1.ReadOnly = false;
            this.textBox1.Focus();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //_id = null;
            toolStripButton3.Visible = false;
            toolStripButton5.Visible = false;
            toolStripButton1.Visible = true;
            this.panel1.Enabled = true;
            foreach (Control i in this.Controls)
            {
                if (i is TextBox )
                {
                    i.Enabled = true;
                    //i.Text = null;
                }


            }
            this.textBox2.ReadOnly = true;
            textBox1.Focus();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!validacion()) return;

            materiales c = new materiales();
            c.tip = this.comboBox1.SelectedValue.ToString();
            c.cod = string.Concat(this.textBox3.Text,"-", this.textBox2.Text.PadLeft(4,'0'));
            c.nom = this.textBox1.Text;



            int rowsAffected = 0;
            if (string.IsNullOrEmpty(_id))
            {
                rowsAffected = op_materiales.accion(c, 1);
                op_var.boton3 = true;
            }
            else
            {
                this.textBox1.ReadOnly = true;
                rowsAffected = op_materiales.accion(c, 2);
            }

            if (rowsAffected > 0)
            {
                if (rowsAffected == 1)
                {
                    MessageBox.Show(mensajes.MsjProc1, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    operacion();
                    this.toolStripButton3.Visible = true;
                    toolStripButton2.Visible = false;
                    this.panel1.Enabled = true;
                }
                if (rowsAffected == 2)
                {
                    MessageBox.Show(mensajes.MsjProc2, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    operacion();
                    this.toolStripButton3.Visible = true;
                    toolStripButton2.Visible = false;
                    this.panel1.Enabled = true;
                }
                if (rowsAffected == 3)
                {
                    MessageBox.Show(mensajes.MsjProc3, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    operacion();
                    this.toolStripButton3.Visible = true;
                    toolStripButton2.Visible = false;
                }
                if (rowsAffected == 4)
                {
                    if (MessageBox.Show(mensajes.MsjProc4 + " " + mensajes.MsjProc5, Application.ProductName.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        rowsAffected = op_materiales.accion(c, 2);
                    }

                }
            }
            else
            {
                MessageBox.Show(mensajes.MsjProc0, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text!="")
            {
                this.textBox2.Text = op_sql.parametro1("select LPAD(cast((max(right(cod,4)) + 1) as CHAR) ,4,'0') as nummax from materiales where tip='" + comboBox1.SelectedValue.ToString() + "'");
                this.textBox3.Text = comboBox1.SelectedValue.ToString();
                this.textBox2.Focus();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmrangosmat frm = new frmrangosmat(string.Concat(textBox3.Text.Trim(),"-",textBox2.Text.Trim()));
            frm.Text = string.Concat(textBox3.Text.Trim(), "-", textBox2.Text.Trim());
            frm.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = op_rangos.GellAllrango3("select * from rangos_mat where codmat='" + string.Concat(textBox3.Text.Trim(), "-", textBox2.Text.Trim()) + "'"); 
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

   
       
       
        
     
       

       
    }
}
