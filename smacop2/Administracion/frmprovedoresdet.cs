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
using smacop2.Entity;
using smacop2.Operaciones;
using System.Runtime.InteropServices;


namespace smacop2.Administracion
{
    public partial class frmprovedoresdet : Form
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

        public frmprovedoresdet()
        {
            InitializeComponent();
            textBox1.KeyPress += new KeyPressEventHandler(textBox3_KeyPress);
            textBox2.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            textBox3.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            textBox4.KeyPress += new KeyPressEventHandler(textBox3_KeyPress);
            textBox5.KeyPress += new KeyPressEventHandler(textBox3_KeyPress);
            textBox6.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            textBox6.KeyDown += new KeyEventHandler(textBox6_KeyDown);
            this.KeyDown += new KeyEventHandler(frmcargosdet_KeyDown);
            this.dataGridView1.CellContentClick+=new DataGridViewCellEventHandler(dataGridView1_CellContentClick);
            this.dataGridView1.CellDoubleClick+=new DataGridViewCellEventHandler(dataGridView1_CellDoubleClick);
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

        public frmprovedoresdet(string id)
            : this()
        {
            _id = id;

        }
        private void dataGridView1_CellDoubleClick(object sender,  DataGridViewCellEventArgs e )
        {
            DataGridViewRow row = dataGridView1.CurrentRow as DataGridViewRow;
            frmcostomaterial frm = new frmcostomaterial(row.Cells["cod"].Value.ToString(), row.Cells["cmat"].Value.ToString());
            op_var.tmp_cantera = row.Cells["cod"].Value.ToString();
            frm.ShowDialog();
        }

        private void frmcargosdet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1) toolStripButton3_Click(toolStripButton3, e);
            if (e.KeyCode == Keys.F4) MessageBox.Show("Imprime");
            if (e.KeyCode == Keys.F3) toolStripButton5_Click(toolStripButton5, e);
        }

        private void checkBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) toolStripButton1_Click(toolStripButton1, e);
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) toolStripButton1_Click(toolStripButton1, e);
        }

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


        private bool validacion()
        {
            bool o = true;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(this.comboBox1.Text))
            {
                errorProvider1.SetError(comboBox1, "El Cargo es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "El No. ID es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Los Nombres y Apellidos son Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.textBox5.Text))
            {
                errorProvider1.SetError(textBox5, "El Móvil es Obligatorio");
                o = false;
            }

            return o;
        }




        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!validacion()) return;
            proveedores c = new proveedores();
            c.tipid = this.comboBox3.SelectedValue.ToString();
            c.cod = this.textBox1.Text;
            c.nom = this.textBox2.Text;
            c.dir = this.textBox3.Text;
            c.tel = this.textBox4.Text;
            c.cel = this.textBox5.Text;
            c.email = this.textBox6.Text;
            c.cont = this.textBox7.Text;
            c.mun = this.comboBox2.SelectedValue.ToString();
        

            //this.label10.Text = "PROVEEDOR: " + c.NombreCompleto;

            int rowsAffected = 0;
            if (string.IsNullOrEmpty(_id))
                rowsAffected = op_proveedores.accion(c, 1);
            else
            {
                this.textBox1.ReadOnly = true;
                rowsAffected = op_proveedores.accion(c, 2);
            }

            if (rowsAffected > 0)
            {
                if (rowsAffected == 1)
                {
                    MessageBox.Show(mensajes.MsjProc1, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    operacion();
                    this.toolStripButton3.Visible = true;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    toolStripButton2.Visible = false;
                    op_var.boton1 = true;
                    
                }
                if (rowsAffected == 2)
                {
                    MessageBox.Show(mensajes.MsjProc2, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    operacion();
                    this.toolStripButton3.Visible = true;
                    toolStripButton2.Visible = false;
                
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
                        rowsAffected = op_proveedores.accion(c, 2);
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
                if (e is TextBox || e is ComboBox || e is NumericUpDown || e is Button)
                    e.Enabled = false;
            }
            foreach (Control e in this.groupBox1.Controls)
            {
                if (e is TextBox)
                    e.Enabled = false;
            }

            this.toolStripButton3.Visible = true;
            this.toolStripButton1.Visible = false;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //this.label10.Text = "EDITANDO PROVEEDOR...";
            toolStripButton3.Visible = false;
            toolStripButton5.Visible = false;
            toolStripButton1.Visible = true;
            toolStripButton2.Visible = true;
            foreach (Control i in this.Controls)
            {
                if (i is TextBox || i is ComboBox || i is NumericUpDown || i is Button)
                {
                    i.Enabled = true;
                    //i.Text = null;
                }
            }

                foreach (Control r in this.groupBox1.Controls)
                {
                    if (r is TextBox)
                    {
                        r.Enabled = true;
                        //r.Text = null;
                    }

                }
           
            this.textBox1.ReadOnly = true;
            textBox1.Focus();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //this.label10.Text = "NUEVO PROVEEDOR...";
            _id = null;
            toolStripButton3.Visible = false;

            toolStripButton5.Visible = false;
            toolStripButton1.Visible = true;
           
            foreach (Control i in this.Controls)
            {
                if (i is TextBox || i is ComboBox || i is NumericUpDown || i is Button)
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
            //this.comboBox4.Enabled = false;
            //this.button5.Enabled = false;

            this.textBox1.ReadOnly = false;
            textBox1.Focus();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.CurrentRow as DataGridViewRow;
                    if (this.dataGridView1.Columns[e.ColumnIndex].Name == "eli")
                    {
                        int a = Convert.ToInt32(MessageBox.Show("Está seguro que desea eliminar el registro", Application.ProductName.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Information));
                        if (a == 6)
                        {
                            cant_material c = new cant_material();
                            c.cnt = row.Cells[0].Value.ToString();
                            c.mat = row.Cells[2].Value.ToString();

                            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                            {
                                con.Open();
                                MySqlCommand cmd = new MySqlCommand("delete from materiales_canteras where cantera=@cant and material =@mat", con);
                                cmd.Parameters.AddWithValue("@cant", c.cnt);
                                cmd.Parameters.AddWithValue("@mat", c.mat);
                                int i = cmd.ExecuteNonQuery();
                                if (i > 0)
                                    MessageBox.Show("Registro Elimanado", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("No se puedo Eliminar", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }

                            dataGridView1.AutoGenerateColumns = false;
                            dataGridView1.DataSource = op_proveedores.GellIdcanteraxproducto(this.textBox1.Text);
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

        private void frmprovedoresdet_Load(object sender, EventArgs e)
        {

            IntPtr hmenu = GetSystemMenu(this.Handle, 0);
            int cnt = GetMenuItemCount(hmenu);
            // remove 'close' action
            RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            // remove extra menu line
            RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            DrawMenuBar(this.Handle);


            comboBox1.DataSource = op_combos.FillCombo(@"SELECT cod, nom FROM dpto ORDER BY nom ASC");
            comboBox1.DisplayMember = "nom";
            comboBox1.ValueMember = "cod";
            comboBox1.Text = null;

            comboBox3.DataSource = op_documentos.documentos();
            comboBox3.DisplayMember = "nom";
            comboBox3.ValueMember = "cod";
            comboBox3.Text = null;
            toolStripButton2.Visible = true;

            if (!string.IsNullOrEmpty(_id))
            {
                proveedores c = op_proveedores.GellIdproveedores(_id, 1);
               
                if (c != null)
                {
                    this.Text = "Editando Proveedores: " + c.cod + "-" + c.nom;
                    this.textBox1.Text = c.cod;
                    this.textBox2.Text = c.nom;
                    this.textBox3.Text = c.dir;
                    this.textBox4.Text = c.tel;
                    this.textBox5.Text = c.cel;
                    this.textBox6.Text = c.email;
                    this.textBox7.Text = c.cont;
                    this.comboBox2.SelectedValue = c.mun;
                    this.comboBox3.SelectedValue = c.tipid;
                    this.comboBox1.SelectedValue = op_sql.dpto(c.mun);

                    toolStripButton5.Visible = true;
                    toolStripButton1.Visible = false;
                   
                    operacion();
                    toolStripButton3.Visible = false;
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.DataSource = op_proveedores.GellIdcanteraxproducto(c.cod);
                    button1.Enabled = true;
                    button2.Enabled = true;
                }
            }
            else
            {
                this.Text = "Nuevo Registro Proveedores";
                toolStripButton3.Visible = false;
                toolStripButton5.Visible = false;
                toolStripButton1.Visible = true;
                button1.Enabled = false;
                button2.Enabled = false;
                //this.comboBox4.Enabled = false;
                //this.button5.Enabled = false;
               
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                comboBox2.DataSource = op_combos.FillCombo(@"SELECT cod, nom, dpto FROM mpio where dpto='" + this.comboBox1.SelectedValue.ToString() + "' ORDER BY nom ASC");
                comboBox2.DisplayMember = "nom";
                comboBox2.ValueMember = "cod";
                //comboBox2.Text = null;
            }
            this.textBox7.Focus();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox7.Focus();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
                this.textBox7.Text = textBox2.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //if (this.comboBox4.Text == null)
            //    MessageBox.Show("Escoja la Opción");
            //else
            //{
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && textBox1.Enabled == false)
            {
                frmcostomaterial frm = new frmcostomaterial();
                op_var.tmp_cantera = this.textBox1.Text;
                frm.MdiParent = this.MdiParent;
                frm.ShowDialog();
            }
            else MessageBox.Show("Registre el Propietario de la Cantera o quite el modo de  edición", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = op_proveedores.GellIdcanteraxproducto(this.textBox1.Text);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            comboBox1.DataSource = op_combos.FillCombo(@"SELECT cod, nom FROM dpto ORDER BY nom ASC");
            comboBox1.DisplayMember = "nom";
            comboBox1.ValueMember = "cod";
            comboBox1.Text = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            frmlugares f = new frmlugares();
            f.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            comboBox1.DataSource = op_combos.FillCombo(@"SELECT cod, nom FROM dpto ORDER BY nom ASC");
            comboBox1.DisplayMember = "nom";
            comboBox1.ValueMember = "cod";
            comboBox1.Text = null;

            comboBox3.DataSource = op_documentos.documentos();
            comboBox3.DisplayMember = "nom";
            comboBox3.ValueMember = "cod";
            comboBox3.Text = null;
        }

       


    }
}
