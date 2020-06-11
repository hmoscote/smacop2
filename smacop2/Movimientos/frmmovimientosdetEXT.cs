using System;
using System.Windows.Forms;
using System.Configuration;
using smacop2.Entity;
using smacop2.Operaciones;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;

namespace smacop2.Movimientos
{

    public partial class frmmovimientosdetEXT : Form
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

        public frmmovimientosdetEXT()
        {
            InitializeComponent();
            //this.textBox8.MouseDoubleClick+=new MouseEventHandler(textBox8_MouseDoubleClick);
            this.textBox14.MouseDoubleClick += new MouseEventHandler(textBox14_MouseDoubleClick);
            //this.textBox8.KeyDown+=new KeyEventHandler(textBox8_KeyDown);
            this.textBox14.KeyDown += new KeyEventHandler(textBox14_KeyDown);
            textBox2.KeyPress += new KeyPressEventHandler(numero);
            textBox7.KeyPress += new KeyPressEventHandler(numero);
            this.textBox6.KeyPress += new KeyPressEventHandler(numero);
            this.textBox5.KeyPress += new KeyPressEventHandler(numero_decimal);
            this.textBox4.KeyPress += new KeyPressEventHandler(numero);
            this.textBox3.KeyPress += new KeyPressEventHandler(letras_numero);
            this.textBox14.KeyPress += new KeyPressEventHandler(numero);
            this.textBox3.LostFocus += new EventHandler(textBox3_LostFocus);
            this.textBox7.KeyDown+=new KeyEventHandler(textBox7_KeyDown);
            textBox5.KeyDown += new KeyEventHandler(textBox5_KeyDown);
            textBox4.LostFocus += new EventHandler(textBox4_LostFocus);
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
        public void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                this.textBox7.ReadOnly = false;
            }
        }
        public void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                this.textBox5.ReadOnly = false;
            }
        }

        private void textBox3_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.textBox3.Text))
            {
                this.textBox5.Text = op_sql.parametro1("select capeq from equipos where codeq='"+this.textBox3.Text+"'");
                if (this.textBox5.Text != "0")
                    this.textBox5.ReadOnly = true;
                else
                {
                    this.textBox5.Text = null;
                    this.textBox5.ReadOnly = false;
                }

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

        public frmmovimientosdetEXT(string id)
            : this()
        {
            _id = id;
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
            if (!(char.IsLetterOrDigit(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsPunctuation(e.KeyChar))) e.Handled = true;
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        public void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                Administracion.frmlistatmp fr = new Administracion.frmlistatmp(4);
                fr.ShowDialog();
            }
        }

        public void textBox14_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                Administracion.frmlistatmp fr = new Administracion.frmlistatmp(5);
                fr.ShowDialog();
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


            if (string.IsNullOrEmpty(this.comboBox2.Text))
            {
                errorProvider1.SetError(comboBox2, "El Material es Obligatorio");
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

            if (string.IsNullOrEmpty(this.comboBox3.Text))
            {
                errorProvider1.SetError(comboBox3, "La Sitio de Cargue es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.comboBox4.Text))
            {
                errorProvider1.SetError(comboBox4, "La Sitio de Descargue es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.comboBox1.Text))
            {
                errorProvider1.SetError(comboBox1, "La Transacción es Obligatoria");
                o = false;
            }
            
            if (string.IsNullOrEmpty(this.textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "El Equipo es Obligatorio");
                o = false;
            }

            if (string.IsNullOrEmpty(this.textBox14.Text))
            {
                errorProvider1.SetError(textBox14, "El Operador es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.textBox4.Text))
            {
                errorProvider1.SetError(textBox4, "El Recorrido es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.textBox5.Text))
            {
                errorProvider1.SetError(textBox5, "El Volumen es Obligatorio");
                o = false;
            }

            if (string.IsNullOrEmpty(this.textBox6.Text))
            {
                errorProvider1.SetError(textBox6, "El Costo es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.textBox7.Text))
            {
                errorProvider1.SetError(textBox7, "El Flete es Obligatorio");
                o = false;
            }

            if (this.maskedTextBox1.MaskFull == false)
            {
                errorProvider1.SetError(maskedTextBox1, "La hora salida es Obligatoria");
                o = false;
            }
            else if (this.maskedTextBox2.MaskFull == false)
            {
                errorProvider1.SetError(maskedTextBox2, "La hora llegada es Obligatoria");
                o = false;
            }
            else if (DateTime.Parse(this.maskedTextBox1.Text) > DateTime.Parse(this.maskedTextBox2.Text))
            {
                errorProvider1.SetError(maskedTextBox1, "La hora salida es menor");
                errorProvider1.SetError(maskedTextBox2, "La hora llegada es mayor");
                o = false;
            }
            return o;
        }
        

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
        

            if (!validacion()) return;

            entradasmat c = new entradasmat();
            c.nsis = this.textBox1.Text.Trim();
            c.nrec = this.textBox2.Text.Trim();
            c.fech = this.dateTimePicker1.Value;
            c.prov = this.comboBox3.SelectedValue.ToString();
            c.mat = this.comboBox2.SelectedValue.ToString();
            c.equ = this.textBox3.Text.Trim();
            c.ope = this.textBox14.Text.Trim();
            c.rkm = Convert.ToDecimal(this.textBox4.Text.Trim()); 
            c.volm = Convert.ToDecimal(this.textBox5.Text.Trim());
            c.cosm = Convert.ToDecimal(this.textBox6.Text.Trim());
            c.flem = Convert.ToDecimal(this.textBox7.Text.Trim());
            c.ttra = this.comboBox1.SelectedValue.ToString();
            c.matfle = Convert.ToDecimal(c.Matfle);
            c.tifkm = Convert.ToDecimal(c.Tifkm);
            c.timat = Convert.ToDecimal(c.Timat);
            c.hfin = this.maskedTextBox2.Text;
            c.hini = this.maskedTextBox1.Text;
            c.dcarg = this.comboBox4.SelectedValue.ToString();
            c.usu = Operaciones.op_var.usu;

            TimeSpan a =  TimeSpan.Parse(c.hfin) -TimeSpan.Parse(c.hini) ;
            this.label5.Text = "Tiempo de Respuesta: " +a.ToString();
            //this.label15.Text = String.Format("{0:0.00}",c.matfle);
            //this.textBox10.Text =String.Format("{0:0.00}", c.tifkm);
            //this.textBox11.Text=String.Format("{0:0.00}",c.timat);
            //c.matfle = Convert.ToDecimal(this.label15.Text.Trim());
            //c.tifkm = Convert.ToDecimal(this.textBox10.Text.Trim());
            //c.timat = Convert.ToDecimal(this.textBox11.Text.Trim());

            dataGridView1.Visible = true;
            DataGridViewRowCollection dr = dataGridView1.Rows;
            dr.Clear();
            dr.Add("INGRESO POR FLETE - KM", c.tifkm.ToString("c"));
            dr.Add("INGRESO POR MATERIAL", c.timat.ToString("c"));
            dr.Add("TOTAL", c.matfle.ToString("c"));

            int rowsAffected = 0;
            if (string.IsNullOrEmpty(_id))
                rowsAffected = op_entradasmat.accion(c, 1);
            else
            {
                this.textBox1.ReadOnly = true;
                //this.textBox12.ReadOnly = true;
                rowsAffected = op_entradasmat.accion(c, 2);
            }

                if (rowsAffected > 0)
                {
                    if (rowsAffected == 1)
                    {
                        MessageBox.Show(mensajes.MsjProc1, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        operacion();
                        this.toolStripButton3.Visible = true;
                        this.toolStripButton1.Visible = false;
                       
                    }
                    if (rowsAffected == 2)
                    {
                        MessageBox.Show(mensajes.MsjProc2, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        operacion();
                        this.toolStripButton3.Visible = true;
                        this.toolStripButton1.Visible = false;
                      
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

                
                //this.label10.Text = "EMPLEADO: " + c.NombreCompleto;
            
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            toolStripButton3.Visible = false;
            toolStripButton5.Visible = false;
            toolStripButton1.Visible = true;
          
            //textBox8.Focus();
            foreach (Control r in this.Controls)
            {
                if (r is TextBox || r is ComboBox || r is Button)
                    r.Enabled = true;
            }
           
            foreach (Control r in this.groupBox2.Controls)
            {
                if (r is TextBox || r is ComboBox || r is Button)
                    r.Enabled = true;
            }
            foreach (Control r in this.groupBox3.Controls)
            {
                if (r is TextBox || r is ComboBox || r is RadioButton || r is DateTimePicker || r is Button || r is MaskedTextBox)
                {
                    r.Enabled = true;
                   
                }
            }
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox7.ReadOnly = true;
            textBox14.ReadOnly = true;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            toolStripButton3.Visible = false;
            toolStripButton5.Visible = false;
            toolStripButton1.Visible = true;
   

                foreach (Control r in this.Controls)
                {
                    if (r is TextBox || r is ComboBox || r is Button)
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
                foreach (Control r in this.groupBox3.Controls)
                {
                    if (r is TextBox || r is ComboBox || r is MaskedTextBox || r is Button)
                    {
                        r.Enabled = true;
                        r.Text = null;
                    }
                }

                this.dataGridView1.Rows.Clear();
                this.dataGridView1.Visible = false;
                this.dateTimePicker1.Enabled = true;

            //this.textBox12.Text = DateTime.Now.ToShortDateString();
            this.textBox1.ReadOnly = false;
            this.textBox2.ReadOnly = false;
            this.label5.Text = null;
            //label15.Text = "0.00";
            //this.textBox11.Text = "0.00";
            //this.textBox10.Text = "0.00";
            textBox1.Text = op_sql.parametro("Select num from tmpautonumerico where tabla=@tabla", "tabla", "entmat").PadLeft(10, '0');
            textBox7.Text = op_sql.parametro1("Select flete from conf_transp");
            textBox1.Focus();
        }

        private void textBox8_MouseDoubleClick(object sender, EventArgs e)
        {
            Administracion.frmlistatmp fr = new Administracion.frmlistatmp(4);
            fr.ShowDialog();
        }
        private void textBox14_MouseDoubleClick(object sender, EventArgs e)
        {
            Administracion.frmlistatmp fr = new Administracion.frmlistatmp(5);
            fr.ShowDialog();
        }

        private void frmmovimientosdetEXT_Load(object sender, EventArgs e)
        {
            //textBox7.Text = op_sql.parametro1("Select flete from conf_transp");
            IntPtr hmenu = GetSystemMenu(this.Handle, 0);
            int cnt = GetMenuItemCount(hmenu);
            // remove 'close' action
            RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            // remove extra menu line
            RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            DrawMenuBar(this.Handle);

            //int hora=DateTime.Now.Hour;
            //int minuto = DateTime.Now.Minute;
            //maskedTextBox1.Text = DateTime.Now.Hour();
            textBox1.Text = op_sql.parametro("Select num from tmpautonumerico where tabla=@tabla", "tabla", "entmat").PadLeft(10, '0'); 

            //textBox12.Text = DateTime.Now.ToShortDateString();

            comboBox3.DataSource = op_combos.FillCombo("select cod, nom from proveedores order by 2");
            comboBox3.DisplayMember = "nom";
            comboBox3.ValueMember = "cod";
            comboBox3.Text = null;

            comboBox4.DataSource = op_combos.FillCombo("select idproy as cod, nomproy as nom from proyectos where  activo=1 order by 2");
            comboBox4.DisplayMember = "nom";
            comboBox4.ValueMember = "cod";
            comboBox4.Text = null;


            comboBox1.DataSource = op_documentos.transaccion();
            comboBox1.DisplayMember = "nom";
            comboBox1.ValueMember = "cod";
            comboBox1.Text = null;
       

            if (!string.IsNullOrEmpty(_id))
            {

               entradasmat c = op_entradasmat.GellIdentradasmat(_id, 1);
                if (c != null)
                {
                    this.Text="Editando Registro: [Recibo: "+c.nrec+" Equipo: "+c.equ+"]";
                    this.textBox1.Text = c.nsis;
                    this.textBox2.Text = c.nrec;
                    this.textBox3.Text = c.equ;
                    this.textBox7.Text = c.flem.ToString();
                    this.comboBox2.SelectedValue = c.mat;
                    this.comboBox3.SelectedValue = c.prov;
                    this.comboBox4.SelectedValue = c.dcarg;
         
                    this.textBox14.Text = c.ope;
                    this.maskedTextBox2.Text = c.hfin;
                    this.maskedTextBox1.Text = c.hini;
                    this.dateTimePicker1.Value = c.fech;
                    this.textBox4.Text = c.rkm.ToString();
                    this.textBox5.Text = c.volm.ToString();
                    this.textBox6.Text = c.cosm.ToString();
                    
                    this.comboBox1.SelectedValue = c.ttra;
                
                    TimeSpan a = TimeSpan.Parse(c.hfin) - TimeSpan.Parse(c.hini);
                    this.label5.Text = "Tiempo de Respuesta: " + a.ToString();
                    dataGridView1.Visible = true;
                    DataGridViewRowCollection dr = dataGridView1.Rows;
                    dr.Clear();
                    dr.Add("INGRESO POR FLETE - KM", c.Tifkm.ToString("c"));
                    dr.Add("INGRESO POR MATERIAL", c.Timat.ToString("c"));
                    dr.Add("TOTAL", c.matfle.ToString("c"));
                    
                     this.textBox13.Text = op_sql.parametro("select nomempl from empleados where codempl=@cod order by 1", "@cod", c.ope);
                    toolStripButton5.Visible = true;
                    toolStripButton1.Visible = false;
                    toolStripButton3.Visible = false;
                    operacion();
                  
                    //foreach (Control u in this.Controls)
                    //{
                    //    if (u is Button)
                    //        u.Enabled = true;
                    //}
                }
            }
            else
            {
       
                toolStripButton3.Visible = false;
                toolStripButton5.Visible = false;
                toolStripButton1.Visible = true;
              
                this.label5.Text = null;
                this.Text = "Nuevo Registro - Entrada de Materiales";
           
            }

            textBox3.AutoCompleteCustomSource = Operaciones.autocompletar.LoadAutoComplete();
            textBox3.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        private void operacion()
        {
            foreach (Control e in this.Controls)
            {
                if (e is TextBox || e is ComboBox || e is Button)
                    e.Enabled = false;
            }
           

            foreach (Control e in this.groupBox2.Controls)
            {
                if (e is TextBox || e is ComboBox || e is Button)
                    e.Enabled = false;
            }
            foreach (Control r in this.groupBox3.Controls)
            {
                if (r is TextBox || r is ComboBox || r is RadioButton || r is DateTimePicker || r is Button || r is MaskedTextBox)
                {
                    r.Enabled = false;

                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox2.Text != "")
            {
                this.textBox6.Text=Operaciones.op_sql.parametro1("select costo from materiales_canteras where material='"+this.comboBox2.SelectedValue.ToString()+"'");
                this.textBox6.ReadOnly = true;
            }

            this.textBox3.Focus();
        }

    

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //textBox8.Focus();
        }




        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox3.Text != "")
            {
                comboBox2.DataSource = op_combos.FillCombo("select m.cod, m.nom, mc.cantera from proveedores p join materiales_canteras mc on mc.cantera=p.cod join materiales m on m.cod=mc.material where mc.cantera='" + this.comboBox3.SelectedValue.ToString() + "' order by 2;");
                comboBox2.DisplayMember = "nom";
                comboBox2.ValueMember = "cod";
                //comboBox2.Text = null;
                maskedTextBox1.Focus();
                if (comboBox2.Items.Count == 0 && !string.IsNullOrEmpty(this.textBox2.Text))
                    MessageBox.Show("No ha registrado los materiales en la cantera actual", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
             if (this.comboBox4.Text != "")
            {
                this.textBox4.Text = op_sql.parametro1("SELECT dist  FROM distancias WHERE `lug1` = '" + comboBox3.Text + "' and `lug2` = '" + comboBox4.Text + "'");
                if (string.IsNullOrEmpty(op_sql.parametro1("SELECT dist  FROM distancias WHERE `lug1` = '" + comboBox3.Text + "' and `lug2` = '" + comboBox4.Text + "'")))
                    this.textBox4.Text = op_sql.parametro1("SELECT dist  FROM distancias WHERE `lug1` = '" + comboBox4.Text + "' and `lug2` = '" + comboBox3.Text + "'");

                if (!string.IsNullOrEmpty(this.textBox4.Text))
                    this.textBox4.ReadOnly = true;

                maskedTextBox1.Focus();
            }

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox3.Text != "")
            {
                this.textBox4.Text = op_sql.parametro1("SELECT dist  FROM distancias WHERE `lug1` = '" + comboBox3.Text + "' and `lug2` = '" + comboBox4.Text + "'");
                if (string.IsNullOrEmpty(op_sql.parametro1("SELECT dist  FROM distancias WHERE `lug1` = '" + comboBox3.Text + "' and `lug2` = '" + comboBox4.Text + "'")))
                    this.textBox4.Text = op_sql.parametro1("SELECT dist  FROM distancias WHERE `lug1` = '" + comboBox4.Text + "' and `lug2` = '" + comboBox3.Text + "'");
            
            if (!string.IsNullOrEmpty(this.textBox4.Text))
                this.textBox4.ReadOnly = true;
            }
            maskedTextBox2.Focus();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            comboBox1.DataSource = op_documentos.transaccion();
            comboBox1.DisplayMember = "nom";
            comboBox1.ValueMember = "cod";
            comboBox1.Text = null;
        }

       

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Administracion.frmprovedoresdet f = new Administracion.frmprovedoresdet();
            f.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Administracion.frmproyectosdet f = new Administracion.frmproyectosdet();
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Administracion.frmmaterialesdet f = new Administracion.frmmaterialesdet();
            f.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Administracion.frmequiposdet f = new Administracion.frmequiposdet();
            f.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox3.Enabled)
            {
                comboBox3.DataSource = op_combos.FillCombo("select cod, nom from proveedores order by 2");
                comboBox3.DisplayMember = "nom";
                comboBox3.ValueMember = "cod";
                comboBox3.Text = null;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox4.Enabled)
            {
                comboBox4.DataSource = op_combos.FillCombo("select idproy as cod, nomproy as nom from proyectos where  activo=1 order by 2");
                comboBox4.DisplayMember = "nom";
                comboBox4.ValueMember = "cod";
                comboBox4.Text = null;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.comboBox3.Text != ""  && comboBox3.Enabled)
            {
                comboBox2.DataSource = op_combos.FillCombo("select m.cod, m.nom, mc.cantera from proveedores p join materiales_canteras mc on mc.cantera=p.cod join materiales m on m.cod=mc.material where mc.cantera='" + this.comboBox3.SelectedValue.ToString() + "' order by 2;");
                comboBox2.DisplayMember = "nom";
                comboBox2.ValueMember = "cod";
                //comboBox2.Text = null;
                maskedTextBox1.Focus();
                if (comboBox2.Items.Count == 0 && !string.IsNullOrEmpty(this.textBox2.Text))
                    MessageBox.Show("No ha registrado los materiales en la cantera actual", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
