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
    public partial class frmlugares : Form
    {
        //[DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        //private static extern IntPtr GetSystemMenu(IntPtr hwnd, int revert);
        //[DllImport("user32.dll", EntryPoint = "GetMenuItemCount")]
        //private static extern int GetMenuItemCount(IntPtr hmenu);
        //[DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        //private static extern int RemoveMenu(IntPtr hmenu, int npos, int wflags);
        //[DllImport("user32.dll", EntryPoint = "DrawMenuBar")]
        //private static extern int DrawMenuBar(IntPtr hwnd);
        //private const int MF_BYPOSITION = 0x0400;
        //private const int MF_DISABLED = 0x0002;
        public frmlugares()
        {
            InitializeComponent();
            this.dataGridView1.CellContentClick+=new DataGridViewCellEventHandler(dataGridView1_CellContentClick);
            this.textBox3.KeyPress += new KeyPressEventHandler(numero);
            this.textBox4.KeyPress += new KeyPressEventHandler(numero);
            this.textBox2.KeyPress += new KeyPressEventHandler(letras_numero);
            this.textBox5.KeyPress += new KeyPressEventHandler(letras_numero);
            this.textBox1.KeyPress += new KeyPressEventHandler(letras_numero);
            dataGridView3.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView3_CellPainting);
            dataGridView2.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView2_CellPainting);

        } 

        private void dataGridView3_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dataGridView3.Columns[e.ColumnIndex].Name == "eli" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = this.dataGridView3.Rows[e.RowIndex].Cells["eli"] as DataGridViewButtonCell;
                Icon icoAtomico = new Icon(Environment.CurrentDirectory + @"\delete_16x.ico");

                e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 3, e.CellBounds.Top + 3);

                this.dataGridView3.Rows[e.RowIndex].Height = icoAtomico.Height + 8;
                this.dataGridView3.Columns[e.ColumnIndex].Width = icoAtomico.Width + 8;

                e.Handled = true;
            }
        }
        private void dataGridView2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dataGridView2.Columns[e.ColumnIndex].Name == "eli1" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = this.dataGridView2.Rows[e.RowIndex].Cells["eli1"] as DataGridViewButtonCell;
                Icon icoAtomico = new Icon(Environment.CurrentDirectory + @"\delete_16x.ico");

                e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 3, e.CellBounds.Top + 3);

                this.dataGridView2.Rows[e.RowIndex].Height = icoAtomico.Height + 8;
                this.dataGridView2.Columns[e.ColumnIndex].Width = icoAtomico.Width + 8;

                e.Handled = true;
            }
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
                            try
                            {
                                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                                {
                                    con.Open();
                                    MySqlCommand cmd = new MySqlCommand("delete from lugares where cod=@nom", con);

                                    cmd.Parameters.AddWithValue("@nom", row.Cells[0].Value.ToString());

                                    int i = cmd.ExecuteNonQuery();
                                    if (i > 0)
                                        MessageBox.Show("Registro Eliminado", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    else
                                        MessageBox.Show("No se puedo Grabar", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                                }
                                dataGridView1.AutoGenerateColumns = false;
                                dataGridView1.DataSource = op_combos.FillCombo("SELECT * FROM LUGARES");

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error tecnico: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
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

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.CurrentRow as DataGridViewRow;
                    //this.toolStripLabel1.Text = row.Cells[0].Value.ToString();
                    if (this.dataGridView1.Columns[e.ColumnIndex].Name == "eli1")
                    {
                        int a = Convert.ToInt32(MessageBox.Show("Está seguro que desea eliminar el registro", Application.ProductName.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Information));
                        if (a == 6)
                        {
                            try
                            {
                                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                                {
                                    con.Open();
                                    MySqlCommand cmd = new MySqlCommand("delete from dpto where cod=@nom", con);

                                    cmd.Parameters.AddWithValue("@nom", row.Cells[0].Value.ToString());

                                    int i = cmd.ExecuteNonQuery();
                                    if (i > 0)
                                        MessageBox.Show("Registro Eliminado", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    else
                                        MessageBox.Show("No se puedo Grabar", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                                }
                                dataGridView1.AutoGenerateColumns = false;
                                dataGridView1.DataSource = op_combos.FillCombo("SELECT * FROM dpto");

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error tecnico: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (!validacion()) return;

           bool a= op_sql.comprobar("select * from lugares where nom='"+this.textBox1.Text+"'");
           if (a == false)
           {
               try
               {
                   using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                   {
                       con.Open();
                       MySqlCommand cmd = new MySqlCommand("insert into lugares values(null,@nom)", con);

                       cmd.Parameters.AddWithValue("@nom", textBox1.Text);

                       int i = cmd.ExecuteNonQuery();
                       if (i > 0)
                       {
                           MessageBox.Show("Registro Grabado", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                           errorProvider1.SetError(textBox1,null);
                           op_var.boton2 = true;
                           this.textBox1.Text = null;
                       }
                       else
                           MessageBox.Show("No se puedo Grabar", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                   }
                   dataGridView1.AutoGenerateColumns = false;
                   dataGridView1.DataSource = op_combos.FillCombo("SELECT * FROM LUGARES");
                 

               }
               catch (Exception ex)
               {
                   MessageBox.Show("Error tecnico: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);

               }
           }
           else
               errorProvider1.SetError(textBox1, "El lugar ya existe");
        }
        private bool validacion()
        {
            bool o = true;
            errorProvider1.Clear();


            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "El Lugar es Obligatorio");
                o = false;
            }

            return o;
        }

        private bool validacion1()
        {
            bool o = true;
            errorProvider1.Clear();


            if (string.IsNullOrEmpty(this.textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "El Nombre es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.textBox4.Text))
            {
                errorProvider1.SetError(textBox4, "El Código es Obligatorio");
                o = false;
            }
            return o;
        }
        private bool validacion2()
        {
            bool o = true;
            errorProvider1.Clear();

            if (string.IsNullOrEmpty(this.comboBox1.Text))
            {
                errorProvider1.SetError(comboBox1, "El Departamento es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.textBox5.Text))
            {
                errorProvider1.SetError(textBox5, "El Nombre es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "El Código es Obligatorio");
                o = false;
            }
            return o;
        }
        private void frmlugares_Load(object sender, EventArgs e)
        {
            //IntPtr hmenu = GetSystemMenu(this.Handle, 0);
            //int cnt = GetMenuItemCount(hmenu);
            //// remove 'close' action
            //RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            //// remove extra menu line
            //RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            //DrawMenuBar(this.Handle);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = op_combos.FillCombo("SELECT * FROM LUGARES");

            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = op_combos.FillCombo("SELECT * FROM dpto");

            dataGridView3.AutoGenerateColumns = false;
            dataGridView3.DataSource = op_combos.FillCombo2("SELECT * FROM  lstmunicipios");

            comboBox1.DataSource = op_combos.FillCombo("SELECT * FROM dpto");
            comboBox1.DisplayMember = "nom";
            comboBox1.ValueMember = "cod";
            comboBox1.Text = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!validacion1()) return;

            bool a = op_sql.comprobar("select * from dpto where cod='" + this.textBox4.Text + "' and nom='" + this.textBox2.Text + "'");
            if (a == false)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("insert into dpto values(@cod,@nom)", con);

                        cmd.Parameters.AddWithValue("@cod", textBox4.Text);
                        cmd.Parameters.AddWithValue("@nom", textBox2.Text);
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Registro Grabado", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            errorProvider1.SetError(textBox4, null);
                            errorProvider1.SetError(textBox2, null);
                            comboBox1.DataSource = op_combos.FillCombo("SELECT * FROM dpto");
                            comboBox1.DisplayMember = "nom";
                            comboBox1.ValueMember = "cod";
                            comboBox1.Text = null;
                            this.textBox4.Text = null;
                            this.textBox2.Text = null;
                            //op_var.boton2 = true;
                        }
                        else
                            MessageBox.Show("No se puedo Grabar", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                    dataGridView2.AutoGenerateColumns = false;
                    dataGridView2.DataSource = op_combos.FillCombo("SELECT * FROM dpto");


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error tecnico: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                errorProvider1.SetError(textBox2, "El Nombre ya existe");
                errorProvider1.SetError(textBox4, "El Código ya existe");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!validacion2()) return;

            bool a = op_sql.comprobar("select * from mpio where cod='" + string.Concat(this.comboBox1.SelectedValue.ToString(),this.textBox3.Text) + "' and nom='" + this.textBox5.Text + "'");
            if (a == false)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("insert into mpio values(@cod,@nom,@dpto)", con);
                        cmd.Parameters.AddWithValue("@dpto",this.comboBox1.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@cod", string.Concat(this.comboBox1.SelectedValue.ToString(),this.textBox3.Text));
                        cmd.Parameters.AddWithValue("@nom", this.textBox5.Text);
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Registro Grabado", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            errorProvider1.SetError(textBox3, null);
                            errorProvider1.SetError(textBox5, null);
                            errorProvider1.SetError(this.comboBox1, null);
                            this.textBox3.Text = null;
                            this.textBox5.Text = null;
                            this.comboBox1.Text = null;
                                op_var.boton2 = true;
                        }
                        else
                            MessageBox.Show("No se puedo Grabar", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                    dataGridView3.AutoGenerateColumns = false;
                    dataGridView3.DataSource = op_combos.FillCombo2("SELECT * FROM lstmunicipios");


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error tecnico: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                errorProvider1.SetError(this.comboBox1, "El Departamento ya existe");
                errorProvider1.SetError(this.textBox5, "El Nombre ya existe");
                errorProvider1.SetError(this.textBox3, "El Código ya existe");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox3.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
        
    }
}
