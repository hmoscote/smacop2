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


namespace smacop2.Movimientos
{
    public partial class frmmovimientosdetINT : Form
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
        public frmmovimientosdetINT()
        {
       
            InitializeComponent();
            this.textBox6.KeyDown+=new KeyEventHandler(textBox6_KeyDown);
            this.textBox2.KeyPress += new KeyPressEventHandler(numero);
            this.textBox7.KeyPress += new KeyPressEventHandler(numero);
            this.textBox10.KeyPress += new KeyPressEventHandler(numero);
            this.textBox6.KeyPress += new KeyPressEventHandler(numero);
            this.textBox4.KeyPress += new KeyPressEventHandler(numero_decimal);
            this.textBox8.MouseDoubleClick += new MouseEventHandler(textBox8_MouseDoubleClick);
            this.textBox14.MouseDoubleClick += new MouseEventHandler(textBox14_MouseDoubleClick);
            this.textBox8.KeyDown += new KeyEventHandler(textBox8_KeyDown);
            this.textBox14.KeyDown += new KeyEventHandler(textBox14_KeyDown);
            this.radioButton1.Click+=new EventHandler(radioButton1_Click);
            this.radioButton2.Click += new EventHandler(radioButton2_Click);
            this.radioButton3.Click += new EventHandler(radioButton3_Click);
            this.radioButton4.Click += new EventHandler(radioButton4_Click);
            this.textBox3.KeyPress += new KeyPressEventHandler(letras_numero);
            this.textBox6.LostFocus+=new EventHandler(textBox6_LostFocus);
            this.textBox6.MouseDoubleClick+=new MouseEventHandler(textBox6_MouseDoubleClick);
            this.textBox3.LostFocus+=new EventHandler(textBox3_LostFocus);
            this.textBox7.KeyDown+=new KeyEventHandler(textBox7_KeyDown);
            this.textBox5.KeyDown += new KeyEventHandler(textBox5_KeyDown);
            this.textBox10.KeyDown += new KeyEventHandler(textBox5_KeyDown);
            this.textBox4.LostFocus+=new EventHandler(textBox4_LostFocus);
        }
        private string _id;
        private void textBox4_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.textBox4.Text) || textBox4.Text != "0.00" || textBox4.Text != "" || textBox4.Enabled)
            {
                if (textBox7.Enabled)
                    textBox7.Text = op_sql.parametro1("select costo from rangos_fle where vini<=" + int.Parse(this.textBox4.Text) + " and vfin>=" + int.Parse(this.textBox4.Text));

                if (!string.IsNullOrEmpty(this.textBox7.Text))
                    this.textBox7.ReadOnly = true;
                else
                {
                    MessageBox.Show("Debe definir los rangos del flete", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.textBox7.ReadOnly = false;
                }
            }
        }
        public void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                this.textBox5.ReadOnly = false;
            }
        }
        public void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                this.textBox10.ReadOnly = false;
            }
        }
        public void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                this.textBox7.ReadOnly = false;
            }
        }
        private void textBox3_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.textBox3.Text))
            {
                //string nombre = null;
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("select codempl, nomempl, codeq from empleados em join equipos eq on eq.opeeq=em.codempl where codeq=@equ order by 2", conn);
                        cmd.Parameters.AddWithValue("@equ", textBox3.Text);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows == true)
                        {
                            while (reader.Read())
                            {
                                this.textBox14.Text = Convert.ToString(reader[0]);
                                this.textBox13.Text = Convert.ToString(reader[1]);
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private void radioButton2_Click(object sender, EventArgs e)
        {

            if (this.maskedTextBox1.ReadOnly == false)
                {
                    comboBox3.DataSource = op_combos.FillCombo("select COD as cod, nom as nom from lugares order by 2");
                    comboBox3.DisplayMember = "nom";
                    comboBox3.ValueMember = "cod";
                    comboBox3.Text = null;
                    button1.Enabled = true;
                    if (comboBox3.Items.Count == 0)
                        MessageBox.Show("El registrar los lugares geográficos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            
        }

        private void textBox6_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.textBox6.ReadOnly = false;
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (this.maskedTextBox1.ReadOnly == false)
            {
                comboBox3.DataSource = op_combos.FillCombo("select idproy as cod, nomproy as nom from proyectos where contproy='" + this.textBox8.Text.Trim() + "' and activo=1 order by 2");
                comboBox3.DisplayMember = "nom";
                comboBox3.ValueMember = "cod";
                comboBox3.Text = null;
                button1.Enabled = false;
                if (comboBox3.Items.Count==0)
                    MessageBox.Show("El cliente no tiene centro de costo definido",Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            if (this.maskedTextBox1.ReadOnly == false)
            {
                comboBox5.DataSource = op_combos.FillCombo("select cod, nom from proveedores order by 2");
                comboBox5.DisplayMember = "nom";
                comboBox5.ValueMember = "cod";
                comboBox5.Text = null;
                if (comboBox5.Items.Count == 0)
                    MessageBox.Show("No existen proveedores registrados", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            if (this.maskedTextBox1.ReadOnly == false)
            {
                comboBox5.DataSource = op_combos.FillCombo("select idproy as cod, nomproy as nom from proyectos where activo=1 order by 2");
                comboBox5.DisplayMember = "nom";
                comboBox5.ValueMember = "cod";
                comboBox5.Text = null;
                if (comboBox5.Items.Count == 0)
                    MessageBox.Show("No existen centros de costos registrados y/o activos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void textBox6_LostFocus(object sender, EventArgs e)
        {
            this.textBox6.ReadOnly = true;
        }
        private void textBox8_MouseDoubleClick(object sender, EventArgs e)
        {
            Administracion.frmlistatmp fr = new Administracion.frmlistatmp(6);
            fr.ShowDialog();
        }
        private void textBox14_MouseDoubleClick(object sender, EventArgs e)
        {
            Administracion.frmlistatmp fr = new Administracion.frmlistatmp(7);
            fr.ShowDialog();
        }


        public frmmovimientosdetINT(string id)
            : this()
        {
            _id = id;
           
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                Administracion.frmlistatmp fr = new Administracion.frmlistatmp(6);
                fr.ShowDialog();
            }
        }
        public void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                this.textBox6.ReadOnly = false;
            }
        }

        public void textBox14_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                Administracion.frmlistatmp fr = new Administracion.frmlistatmp(7);
                fr.ShowDialog();
            }
        }
        private bool validacion()
        {
            bool o = true;
            errorProvider1.Clear();


            if (string.IsNullOrEmpty(this.comboBox2.Text))
            {
                errorProvider1.SetError(comboBox2, "El Material es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.comboBox3.Text))
            {
                errorProvider1.SetError(comboBox3, "El Sitio de Descargue es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.textBox10.Text))
            {
                errorProvider1.SetError(textBox10, "El Recorrido en KM es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.comboBox5.Text))
            {
                errorProvider1.SetError(comboBox5, "El Sitio de Cargue Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "El Número del Sistema es Obligatorio");
                o = false;
            }

            if (string.IsNullOrEmpty(this.textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "La Nùmero del Recibo es Obligatorio");
                o = false;
            }
            //if (string.IsNullOrEmpty(this.textBox14.Text))
            //{
            //    errorProvider1.SetError(textBox14, "El Recorrido en KM es Obligatorio");
            //    o = false;
            //}
            if (string.IsNullOrEmpty(this.textBox8.Text))
            {
                errorProvider1.SetError(textBox8, "El Contratista es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.comboBox1.Text))
            {
                errorProvider1.SetError(comboBox1, "La transacción es Obligatoria");
                o = false;
            }
           
            if (this.maskedTextBox1.MaskFull==false)
            {
                errorProvider1.SetError(maskedTextBox1, "La Hora de salida es Obligatoria");
                o = false;
            }
            if (this.comboBox4.SelectedValue.ToString() == "01" || this.comboBox4.SelectedValue.ToString() == "03")
            {
                //if (string.IsNullOrEmpty(this.comboBox6.Text))
                //{
                //    errorProvider1.SetError(comboBox6, "El Equipo es Obligatorio");
                //    o = false;
                //}
            }
            else
            {
                if (string.IsNullOrEmpty(this.textBox3.Text))
                {
                    errorProvider1.SetError(textBox3, "El Equipo es Obligatorio");
                    o = false;
                }
            }

            if (string.IsNullOrEmpty(this.textBox4.Text))
            {
                errorProvider1.SetError(textBox4, "La Cantidad es Obligatoria");
                o = false;
            }
            if (string.IsNullOrEmpty(this.textBox5.Text))
            {
                errorProvider1.SetError(textBox5, "El F. Flete es Obligatorio");
                o = false;
            }
            if(string.IsNullOrEmpty(comboBox4.Text))
            {
                errorProvider1.SetError(comboBox4, "El Tipo de Venta es Obligatorio");
                o = false;
            }
            if (!string.IsNullOrEmpty(comboBox4.Text))
            {
                if (this.comboBox4.SelectedValue.ToString() == "01" || this.comboBox4.SelectedValue.ToString() == "03")
                {
                    if (string.IsNullOrEmpty(this.textBox14.Text))
                    {
                        errorProvider1.SetError(textBox14, "El Operador es Obligatorio");
                        o = false;
                    }
                }
            }

           
            if (string.IsNullOrEmpty(this.textBox6.Text))
            {
                errorProvider1.SetError(textBox6, "El Costo es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.textBox7.Text))
            {
                errorProvider1.SetError(textBox7, "El L. Flete es Obligatorio");
                o = false;
            }
            return o;
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
            if (!(char.IsLetterOrDigit(e.KeyChar) || char.IsControl(e.KeyChar)  || char.IsPunctuation(e.KeyChar))) e.Handled = true;
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
        private void operacion()
        {
            
            foreach (Control e in this.groupBox3.Controls )
            {
                if (e is TextBox || e is ComboBox || e is RadioButton || e is DateTimePicker || e is Button)
                    e.Enabled = false;
            }
            foreach (Control e in this.groupBox1.Controls)
            {
                if (e is TextBox || e is ComboBox || e is RadioButton || e is Button || e is MaskedTextBox)
                    e.Enabled = false;
            }
            foreach (Control e in this.groupBox4.Controls)
            {
                if (e is TextBox || e is ComboBox || e is RadioButton || e is Button)
                    e.Enabled = false;
            }


            foreach (Control e in this.groupBox2.Controls)
            {
                if (e is TextBox || e is ComboBox || e is Button)
                    e.Enabled = false;
            }
            foreach (Control e in this.Controls)
            {
                if (e is TextBox || e is ComboBox)
                    e.Enabled = false;
            }
        }
        private int opcion_check()
        {
            int a = 0;

            if (this.radioButton1.Checked) a = 1;
            if (this.radioButton2.Checked) a = 2;
            return a;
        }

        private int opcion_check1()
        {
            int a = 0;

            if (this.radioButton3.Checked) a = 1;
            if (this.radioButton4.Checked) a = 2;
            return a;
        }

        private void opcion_check_inversa(int a)
        {
            if (a==1)this.radioButton1.Checked=true;
            if (a == 2) this.radioButton2.Checked = true;
        }
        private void opcion_check_inversa1(int a)
        {
            if (a == 1) this.radioButton3.Checked = true;
            if (a == 2) this.radioButton4.Checked = true;
        }

        private void frmmovimientosdetINT_Load(object sender, EventArgs e)
        {
            op_var.boton1 = false;
            op_var.boton2 = false;
            op_var.boton3 = false;
            op_var.boton4 = false;
            IntPtr hmenu = GetSystemMenu(this.Handle, 0);
            int cnt = GetMenuItemCount(hmenu);
            // remove 'close' action
            RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            // remove extra menu line
            RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            DrawMenuBar(this.Handle);

            textBox1.Text = op_sql.parametro("Select num from tmpautonumerico where tabla=@tabla", "tabla", "salmat").PadLeft(10, '0');

            //textBox7.Text = op_sql.parametro1("Select flete from conf_transp");

            comboBox2.DataSource = op_combos.FillCombo("select cod, nom from materiales order by 2");
            comboBox2.DisplayMember = "nom";
            comboBox2.ValueMember = "cod";
            comboBox2.Text = null;

            comboBox1.DataSource = op_documentos.transaccion();
            comboBox1.DisplayMember = "nom";
            comboBox1.ValueMember = "cod";
            comboBox1.Text = null;
           

        

            if (!string.IsNullOrEmpty(_id))
            {

                salidasmat c = op_salidasmat.GellIdsalidasmat(_id, 1);
                if (c != null)
                {
                    operacion();
                    this.Text = "Editando Registro: [Recibo: " + c.nrec + " Equipo: " + c.equ + "] - Salida de Materiales";
                    this.comboBox1.SelectedValue = c.ttra;
                    this.comboBox2.SelectedValue = c.mat;
                    this.textBox1.Text = c.nsis;
                    this.textBox2.Text = c.nrec;
                    this.comboBox4.SelectedValue = c.tven;
                    //if (this.comboBox4.SelectedValue.ToString() == "01" || this.comboBox4.SelectedValue.ToString() == "03")
                    //{
                    //    this.comboBox6.SelectedValue = c.equ;
                    //    this.comboBox6.BringToFront();
                    //}
                    //else
                    //{
                        this.textBox3.Text = c.equ;
                    //    this.comboBox6.SendToBack();
                    //}
                    opcion_check_inversa(c.opcd);
                    this.textBox7.Text = c.flem.ToString();
                    this.textBox8.Text = c.cont; //contratista
                    opcion_check_inversa1(c.opcdt);
                    this.maskedTextBox1.Text = c.hfin;
                    this.textBox10.Text = c.rkm.ToString();
                    this.textBox14.Text = c.ope;
                    this.textBox6.Text = c.cosm.ToString();
                    this.textBox4.Text = String.Format("{0:0.00}", c.cant);
                    this.textBox5.Text = c.flemt.ToString();
                    this.dateTimePicker1.Value = c.fech;
                    dataGridView1.Visible = true;
                    DataGridViewRowCollection dr = dataGridView1.Rows;
                    dr.Clear();
                    dr.Add("INGRESO POR FLETE - KM", c.tifkm.ToString("c"), c.tifkmt.ToString("c"));
                    dr.Add("INGRESO POR MATERIAL", c.timat.ToString("c"), c.timatt.ToString("c"));
                    dr.Add("TOTAL", c.matfle.ToString("c"), c.matflet.ToString("c"));

                    if (c.opcd == 1)
                    {
                        comboBox3.DataSource = op_combos.FillCombo("select idproy as cod, nomproy as nom from proyectos where  activo=1 order by 2");
                        comboBox3.DisplayMember = "nom";
                        comboBox3.ValueMember = "cod";
                        this.comboBox3.SelectedValue = c.ccos;
                        radioButton1.Checked = true;
                    }
                    else if (c.opcd == 2)
                    {
                        comboBox3.DataSource = op_combos.FillCombo("select COD as cod, nom as nom from lugares order by 2");
                        comboBox3.DisplayMember = "nom";
                        comboBox3.ValueMember = "cod";
                        this.comboBox3.SelectedValue = c.lug;
                        radioButton2.Checked = true;
                    }

                    if (c.opcdt == 1)
                    {
                        comboBox5.DataSource = op_combos.FillCombo("select cod, nom from proveedores order by 2");
                        comboBox5.DisplayMember = "nom";
                        comboBox5.ValueMember = "cod";
                        this.comboBox5.SelectedValue = c.prov;
                        radioButton3.Checked = true;
                    }
                    else if (c.opcdt == 2)
                    {
                        comboBox5.DataSource = op_combos.FillCombo("select idproy as cod, nomproy as nom from proyectos where  activo=1 order by 2");
                        comboBox5.DisplayMember = "nom";
                        comboBox5.ValueMember = "cod";
                        this.comboBox5.SelectedValue = c.ccost;
                        radioButton4.Checked = true;
                    }



                    this.comboBox1.SelectedValue = c.ttra;

                    this.textBox9.Text = op_sql.parametro("select nomclie from clientes where codclie=@cod", "@cod", c.cont);
                    this.textBox13.Text = op_sql.parametro("select nomempl from empleados where codempl=@cod", "@cod", c.ope);
                
                    toolStripButton5.Visible = true;
                    toolStripButton1.Visible = false;
                    toolStripButton3.Visible = false;

                    button1.Enabled = false;

                    if (!string.IsNullOrEmpty(this.comboBox4.Text))
                    {
                        if (this.comboBox4.SelectedValue.ToString() == "01" || this.comboBox4.SelectedValue.ToString() == "03")
                        {
                            this.label13.Enabled = true;
                            this.textBox13.Enabled = true;
                            this.textBox14.Enabled = true;
                        }
                        else
                        {
                            this.label13.Enabled = false;
                            this.textBox13.Enabled = false;
                            this.textBox14.Enabled = false;
                        }
                    }
                    operacion();
                }
            }
            else
            {
                toolStripButton3.Visible = false;
                toolStripButton5.Visible = false;
                toolStripButton1.Visible = true;
         
                this.Text = "Nuevo Registro - Salida de Materiales";
            }
            textBox3.AutoCompleteCustomSource = Operaciones.autocompletar.LoadAutoComplete();
            textBox3.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!validacion()) return;

            //if (!validacion2()) return;

            salidasmat c = new salidasmat();
            c.nsis = this.textBox1.Text.Trim();
            c.nrec = this.textBox2.Text.Trim();
            c.fech = this.dateTimePicker1.Value;
            c.cont= this.textBox8.Text.Trim(); 
            c.opcd = opcion_check();

            if (radioButton1.Checked)
            c.ccos = this.comboBox3.SelectedValue.ToString();
            else
            c.lug = this.comboBox3.SelectedValue.ToString();

            c.tven = this.comboBox4.SelectedValue.ToString();
            c.mat = this.comboBox2.SelectedValue.ToString();

           //if(!string.IsNullOrEmpty(textBox3.Text))
            c.equ = this.textBox3.Text.Trim();
           //else
           // //c.equ = this.comboBox6.SelectedValue.ToString();

            c.ope = this.textBox14.Text.Trim();
            c.rkm = Convert.ToDecimal(this.textBox10.Text.Trim());
            c.cant = Convert.ToDecimal(this.textBox4.Text.Trim());
            c.cosm = Convert.ToDecimal(this.textBox6.Text.Trim());
            c.flem = Convert.ToDecimal(this.textBox7.Text.Trim());
            c.ttra = this.comboBox1.SelectedValue.ToString();
           

            if (this.comboBox4.SelectedValue.ToString() == "02" || this.comboBox4.SelectedValue.ToString() == "04")
            {
                c.tifkm = Convert.ToDecimal(0.00);
                c.timat = Convert.ToDecimal(0.00);
                c.matfle = Convert.ToDecimal(0.00);
            }
            else
            {
                c.tifkm = Convert.ToDecimal(c.Tifkm1);
                c.timat = Convert.ToDecimal(c.Timat1);
                c.matfle = Convert.ToDecimal(c.Matfle1);
            }
           
            c.hfin =this.maskedTextBox1.Text;
            c.opcdt = opcion_check1();
            c.usu = Operaciones.op_var.usu;
            

            if (radioButton4.Checked)
                c.ccost = this.comboBox5.SelectedValue.ToString();
            else
                c.prov = this.comboBox5.SelectedValue.ToString();

            c.flemt = Convert.ToDecimal(this.textBox5.Text.Trim());
            
            c.matflet = Convert.ToDecimal(c.Matflet1);
            c.tifkmt = Convert.ToDecimal(c.Tifkmt1);
            c.timatt = Convert.ToDecimal(c.Timat1);
          
          
           
            int rowsAffected = 0;
            if (string.IsNullOrEmpty(_id))
                rowsAffected = op_salidasmat.accion(c, 1);
            else
            {
                this.textBox1.ReadOnly = true;
              
                rowsAffected = op_salidasmat.accion(c, 2);
            }


            if (rowsAffected > 0)
            {
                if (rowsAffected == 1)
                {
                    MessageBox.Show(mensajes.MsjProc1, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    operacion();
                    this.toolStripButton3.Visible = true;
                    this.toolStripButton1.Visible = false;
                    
                    dataGridView1.Visible = true;
                   
                        DataGridViewRowCollection dr = dataGridView1.Rows;
                        dr.Clear();
                        dr.Add("INGRESO POR FLETE - KM", c.tifkm.ToString("c"), c.tifkmt.ToString("c"));
                        dr.Add("INGRESO POR MATERIAL", c.timat.ToString("c"), c.timatt.ToString("c"));
                        dr.Add("TOTAL", c.matfle.ToString("c"), c.matflet.ToString("c"));
                       
                }

                if (rowsAffected == 2)
                {
                    MessageBox.Show(mensajes.MsjProc2, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    operacion();
                    this.toolStripButton3.Visible = true;
                    this.toolStripButton1.Visible = false;
             
                    dataGridView1.Visible = true;
                    DataGridViewRowCollection dr = dataGridView1.Rows;
                    dr.Clear();
                    dr.Add("INGRESO POR FLETE - KM", c.tifkm.ToString("c"), c.tifkmt.ToString("c"));
                    dr.Add("INGRESO POR MATERIAL", c.timat.ToString("c"), c.timatt.ToString("c"));
                    dr.Add("TOTAL", c.matfle.ToString("c"), c.matflet.ToString("c"));
                
                }
                if (rowsAffected == 3)
                    MessageBox.Show(mensajes.MsjProc3, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (rowsAffected == 4)
                {
                    if (MessageBox.Show(mensajes.MsjProc4 + " " + mensajes.MsjProc5, Application.ProductName.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        rowsAffected = op_entradasmat.accion(c, 2);
                }
            }
            else
            {
                MessageBox.Show(mensajes.MsjProc0, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
    
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            toolStripButton3.Visible = false;
            toolStripButton5.Visible = false;
            toolStripButton1.Visible = true;
       
            textBox8.Focus();
            foreach (Control r in this.Controls)
            {
                if (r is TextBox || r is ComboBox)
                    r.Enabled = true;
            }
            foreach (Control r in this.groupBox3.Controls)
            {
                if (r is TextBox || r is ComboBox || r is Button || r is DateTimePicker || r is RadioButton)
                {
                    r.Enabled = true;

                }
            }
            foreach (Control r in this.groupBox4.Controls)
            {
                if (r is TextBox || r is ComboBox || r is RadioButton || r is Button)
                    r.Enabled = true;
            }

            foreach (Control r in this.groupBox1.Controls)
            {
                if (r is TextBox || r is ComboBox || r is RadioButton || r is Button || r is MaskedTextBox)
                    r.Enabled = true;
            }

            foreach (Control r in this.groupBox2.Controls)
            {
                if (r is TextBox || r is ComboBox || r is Button)
                    r.Enabled = true;
            }

            if (radioButton2.Checked)
                button1.Enabled = true;
            else
                button1.Enabled = false;

            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            textBox10.ReadOnly = true;
            textBox14.ReadOnly = true;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            toolStripButton3.Visible = false;
            toolStripButton5.Visible = false;
            toolStripButton1.Visible = true;
      
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Visible = false;
         
            foreach (Control r in this.Controls)
            {
                if (r is TextBox || r is ComboBox)
                {
                    r.Enabled = true;
                    r.Text = null;
                }
            }
            foreach (Control r in this.groupBox4.Controls)
            {
                if (r is TextBox || r is ComboBox  || r is Button)
                {
                    r.Enabled = true;
                    r.Text = null;
                }
               
            }
            this.radioButton1.Enabled = true;
            this.radioButton2.Enabled = true;
            this.radioButton3.Enabled = true;
            this.radioButton4.Enabled = true;
            foreach (Control r in this.groupBox1.Controls)
            {
                if (r is TextBox || r is ComboBox || r is Button || r is MaskedTextBox)
                {
                    r.Enabled = true;
                    r.Text = null;
                }
            }

              foreach (Control r in this.groupBox3.Controls)
            {
            if (r is TextBox || r is ComboBox )
            {
               
                    r.Enabled = true;
                    r.Text = null;
                }
            }
            foreach (Control r in this.groupBox2.Controls)
            {
                if (r is TextBox || r is ComboBox || r is Button)
                {
                    r.Enabled = true;
                    r.Text = null;
                }
            }
            this.radioButton1.Enabled = true;
            this.radioButton2.Enabled = true;
            this.button1.Enabled = true;
            this.dateTimePicker1.Enabled = true;
           
            this.textBox1.ReadOnly = false;
            this.textBox2.ReadOnly = false;
            //textBox7.Text = op_sql.parametro1("Select flete from conf_transp");
            textBox5.Text = textBox7.Text;
            textBox1.Text = op_sql.parametro("Select num from tmpautonumerico where tabla=@tabla", "tabla", "salmat").PadLeft(10, '0');
            textBox1.Focus();
        }

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    if(_id==null)
        //    textBox12.Text = DateTime.Now.ToString();
        //}

       
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.comboBox4.Text))
            {
                if (this.comboBox4.SelectedValue.ToString() == "01" || this.comboBox4.SelectedValue.ToString() == "03")
                {
                    //textBox3.AutoCompleteCustomSource = Operaciones.autocompletar.LoadAutoComplete();
                    //textBox3.AutoCompleteMode = AutoCompleteMode.Suggest;
                    //textBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    //string t1 = " tipeq='" + op_sql.parametro1("select transp1 from conf_transp where id=1") + "'";
                    //string t2 = "or tipeq='" + op_sql.parametro1("select transp2 from conf_transp where id=1") + "'";
                    //string t3 = "or tipeq='" + op_sql.parametro1("select transp3 from conf_transp where id=1") + "'";

                  
                    //comboBox6.DataSource = op_combos.FillCombo("select codeq as cod, deseq as nom from equipos where " +t1 + t2 + t3);
                    //comboBox6.DisplayMember = "cod";
                    //comboBox6.ValueMember = "cod";
                    //comboBox6.Text = null;

                    //comboBox6.BringToFront();
                    this.textBox3.Text = null;
                    this.label13.Enabled = true;
                    this.textBox13.Enabled = true;
                    this.textBox14.Enabled = true;
                    this.textBox13.Text = null;
                    this.textBox14.Text = null;
                }
                else
                {
                    //comboBox6.SendToBack();
          
                    this.textBox3.Text = null;
                    //comboBox6.DataSource =null;
                    //comboBox6.DisplayMember = null;
                    //comboBox6.ValueMember = null;
                    //comboBox6.Text = null;
                    this.textBox13.Text = null;
                    this.textBox14.Text = null;
                    //textBox3.AutoCompleteCustomSource = null;
                    //textBox3.AutoCompleteMode = AutoCompleteMode.None;
                    //textBox3.AutoCompleteSource = AutoCompleteSource.None;
                    //this.label13.Enabled = false;
                    //this.textBox13.Enabled = false;
                    //this.textBox14.Enabled = false;
                }

                if (this.comboBox4.SelectedValue.ToString() == "02" || this.comboBox4.SelectedValue.ToString() == "04")
                {
                    textBox10.Text = "0.00";
                    textBox10.ReadOnly = true;
                    textBox7.Text = "0.00";
                    textBox7.ReadOnly = true;
                    textBox5.Text = "0.00";
                    textBox5.ReadOnly = true;
                }
                else
                {
                    if (textBox10.Text == "0.00")
                    {
                        textBox10.Text = null;
                        textBox10.ReadOnly = false;
                    }
                    //textBox7.Text = op_sql.parametro1("Select flete from conf_transp");
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox4.Text = null;
            textBox3.Focus();

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.comboBox2.Text))
                {
                    if (!string.IsNullOrEmpty(this.textBox4.Text) || textBox4.Text != "0.00" || textBox4.Text != ""|| textBox4.Enabled)
                    {
                       if (this.textBox6.Enabled)
                        textBox6.Text = op_sql.parametro1("select precio from rangos_mat where codmat='" + this.comboBox2.SelectedValue.ToString() + "' and rini<=" + int.Parse(this.textBox4.Text) + " and rfin>=" + int.Parse(this.textBox4.Text));
                       
                        if (string.IsNullOrEmpty(textBox6.Text) && textBox4.Text.Length==1)
                            MessageBox.Show("Debe definir los rangos de precios según la cantidad", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (FormatException)
            {
                textBox6.Text = null;
                return;
            }
        }

      

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if(textBox7.Enabled==true)
            textBox5.Text = textBox7.Text;
        }
       
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            if (comboBox1.Enabled == true)
            {
                if (this.radioButton3.Checked)
                {
                  
                    if (comboBox5.Text != "" && comboBox5.Text != "smacop2.Entity.combos")
                    {
                        comboBox2.DataSource = op_combos.FillCombo(@"SELECT
                                                                     m.cod as cod,
                                                                     m.nom as nom
                                                                     FROM materiales m join materiales_canteras mc on m.cod=mc.material
                                                                     join proveedores p on p.cod=mc.cantera 
                                                                     where mc.cantera='" + comboBox5.SelectedValue.ToString() + "';");
                        comboBox2.DisplayMember = "nom";
                        comboBox2.ValueMember = "cod";
                     
                    }
                }
                if (this.radioButton4.Checked)
                {
                    comboBox2.DataSource = op_combos.FillCombo("select cod, nom from materiales order by 2");
                    comboBox2.DisplayMember = "nom";
                    comboBox2.ValueMember = "cod";
                    comboBox2.Text = null;
                    if (comboBox2.Items.Count <= 0 )
                        MessageBox.Show("No hay materiales registrados", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            if (comboBox3.Text != "" && textBox10.Enabled==true)
            {
                this.textBox10.Text = op_sql.parametro1("SELECT dist  FROM distancias WHERE `lug1` = '" + comboBox3.Text + "' and `lug2` = '" + comboBox5.Text + "'");
                if (string.IsNullOrEmpty(op_sql.parametro1("SELECT dist  FROM distancias WHERE `lug1` = '" + comboBox3.Text + "' and `lug2` = '" + comboBox5.Text + "'")))
                    this.textBox10.Text = op_sql.parametro1("SELECT dist  FROM distancias WHERE `lug1` = '" + comboBox5.Text + "' and `lug2` = '" + comboBox3.Text + "'");
            }
           
            if (!string.IsNullOrEmpty(this.textBox10.Text))
                this.textBox10.ReadOnly = true;
           
            maskedTextBox1.Focus();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                comboBox4.DataSource = op_documentos.ventas(comboBox1.SelectedValue.ToString());
                comboBox4.DisplayMember = "nom";
                comboBox4.ValueMember = "cod";
                comboBox4.Text = null;
                this.textBox13.Text = null;
                this.textBox14.Text = null;
            }
            this.textBox8.Focus();
        }

     

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            comboBox2.DataSource = op_combos.FillCombo("select cod, nom from materiales order by 2");
            comboBox2.DisplayMember = "nom";
            comboBox2.ValueMember = "cod";
            comboBox2.Text = null;

            comboBox1.DataSource = op_documentos.transaccion();
            comboBox1.DisplayMember = "nom";
            comboBox1.ValueMember = "cod";
            comboBox1.Text = null;

            //comboBox6.DataSource = op_combos.FillCombo("select codeq as cod, nomeq as nom from equipos");
            //comboBox6.DisplayMember = "nom";
            //comboBox6.ValueMember = "cod";
            //comboBox6.Text = null;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text != "" && textBox10.Enabled==true)
            {
                this.textBox10.Text = op_sql.parametro1("SELECT dist FROM distancias WHERE `lug1` = '" + comboBox3.Text + "' and `lug2` = '" + comboBox5.Text + "'");
                if (string.IsNullOrEmpty(op_sql.parametro1("SELECT dist  FROM distancias WHERE `lug1` = '" + comboBox3.Text + "' and `lug2` = '" + comboBox5.Text + "'")))
                    this.textBox10.Text = op_sql.parametro1("SELECT dist  FROM distancias WHERE `lug1` = '" + comboBox5.Text + "' and `lug2` = '" + comboBox3.Text + "'");
            }
            if (!string.IsNullOrEmpty(this.textBox10.Text))
                this.textBox10.ReadOnly = true;
        }

        private void frmact(object sender, EventArgs e)
        {
            if (op_var.boton1 == true)
            {
                op_var.botones(op_var.boton1, button2);
                op_var.boton1 = false;
            }

            if (op_var.boton2 == true)
            {
                op_var.botones(op_var.boton2, button3);
                op_var.boton2 = false;
            }
            if (op_var.boton3 == true)
            {
                op_var.botones(op_var.boton3, button4);
                op_var.boton3 = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                Administracion.frmproyectosdet f = new Administracion.frmproyectosdet();
                f.ShowDialog();
            }
            else if (radioButton3.Checked)
            {
                Administracion.frmprovedoresdet f = new Administracion.frmprovedoresdet();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione la opción", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox5.Enabled)
            {
                if (radioButton4.Checked)
                {
                    comboBox5.DataSource = op_combos.FillCombo("select idproy as cod, nomproy as nom from proyectos where  activo=1 order by 2");
                    comboBox5.DisplayMember = "nom";
                    comboBox5.ValueMember = "cod";

                }
                else if (radioButton3.Checked)
                {
                    comboBox5.DataSource = op_combos.FillCombo("select cod, nom from proveedores order by 2");
                    comboBox5.DisplayMember = "nom";
                    comboBox5.ValueMember = "cod";
                }

                comboBox5.Text = null;
                button6.SendToBack();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Administracion.frmproyectosdet f = new Administracion.frmproyectosdet();
                f.ShowDialog();
            }
            else if (radioButton2.Checked)
            {
                Administracion.frmlugares f = new Administracion.frmlugares();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione la opción", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox3.Enabled)
            {
                if (radioButton1.Checked)
                {
                    comboBox3.DataSource = op_combos.FillCombo("select idproy as cod, nomproy as nom from proyectos where  activo=1 order by 2");
                    comboBox3.DisplayMember = "nom";
                    comboBox3.ValueMember = "cod";

                }
                else if (radioButton2.Checked)
                {
                    comboBox3.DataSource = op_combos.FillCombo("select COD as cod, nom as nom from lugares order by 2");
                    comboBox3.DisplayMember = "nom";
                    comboBox3.ValueMember = "cod";

                }
                comboBox3.Text = null;
                button7.SendToBack();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Administracion.frmmaterialesdet f = new Administracion.frmmaterialesdet();
            f.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            comboBox2.DataSource = op_combos.FillCombo("select cod, nom from materiales order by 2");
            comboBox2.DisplayMember = "nom";
            comboBox2.ValueMember = "cod";
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Administracion.frmequiposdet f = new Administracion.frmequiposdet();
            f.ShowDialog();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (comboBox5.Enabled == true && comboBox2.Enabled == true)
            {
                if (this.radioButton3.Checked)
                {

                    if (comboBox5.Text != "" && comboBox5.Text != "smacop2.Entity.combos")
                    {
                        comboBox2.DataSource = op_combos.FillCombo(@"SELECT
                                                                     m.cod as cod,
                                                                     m.nom as nom
                                                                     FROM materiales m join materiales_canteras mc on m.cod=mc.material
                                                                     join proveedores p on p.cod=mc.cantera 
                                                                     where mc.cantera='" + comboBox5.SelectedValue.ToString() + "';");
                        comboBox2.DisplayMember = "nom";
                        comboBox2.ValueMember = "cod";

                    }
                }
                if (this.radioButton4.Checked)
                {
                    comboBox2.DataSource = op_combos.FillCombo("select cod, nom from materiales order by 2");
                    comboBox2.DisplayMember = "nom";
                    comboBox2.ValueMember = "cod";
                    comboBox2.Text = null;
                    if (comboBox2.Items.Count <= 0)
                        MessageBox.Show("No hay materiales registrados", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Administracion.frmmaterialesdet f = new Administracion.frmmaterialesdet();
            f.ShowDialog();
        }

    }
}
