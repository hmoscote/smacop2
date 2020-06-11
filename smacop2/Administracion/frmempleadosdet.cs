using System;
using System.Drawing;
using System.Windows.Forms;
using smacop2.Entity;
using smacop2.Operaciones;
using System.Runtime.InteropServices;

namespace smacop2.Administracion
{
    public partial class frmempleadosdet : Form
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
        public frmempleadosdet()
        {
            InitializeComponent();
            textBox1.KeyPress += new KeyPressEventHandler(textBox4_KeyPress);
            textBox2.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            textBox3.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            textBox4.KeyPress += new KeyPressEventHandler(textBox3_KeyPress);
            textBox5.KeyPress += new KeyPressEventHandler(textBox3_KeyPress);
            textBox6.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            checkBox1.KeyPress += new KeyPressEventHandler(checkBox1_KeyPress);
            textBox6.KeyDown += new KeyEventHandler(textBox6_KeyDown);
            this.checkBox1.KeyDown += new KeyEventHandler(checkBox1_KeyDown);
            this.KeyDown += new KeyEventHandler(frmcargosdet_KeyDown);
        }
        //private Entity.cargos c { get; set; }
        //private string _id;
        //FORMULARIO ENVIA VALOR
        //public int IdCliente
        //{
        //    get { return _idCliente; }
        //}

        //FORMULARIO RECIBE VALOR
        //  cliente = CustomerBO.GetById(frm.IdCliente);

       // F1="Nuevo" F2="Grabar" F3="Editar" F4="Imprimir"
        
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

        private void checkBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (checkBox1.Checked) checkBox1.Checked = false; else checkBox1.Checked = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) )) e.Handled = true;
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsPunctuation(e.KeyChar))) e.Handled = true;
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
      

        public frmempleadosdet (string id)
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
                errorProvider1.SetError(comboBox1, "El Cargo es Obligatorio");
                o = false;
            }

            if (string.IsNullOrEmpty(this.comboBox2.Text))
            {
                errorProvider1.SetError(comboBox2, "El Tipo de ID es Obligatorio");
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
                errorProvider1.SetError(textBox3, "El Móvil es Obligatorio");
                o = false;
            }
            
            return o;
        }


        private void toolStripButton1_Click(object sender, EventArgs e) 
        {
        
            if (!validacion()) return;

            empleados c = new empleados();
            c.cargo = this.comboBox1.SelectedValue.ToString();
            c.tipid = this.comboBox2.SelectedValue.ToString();
            c.cod = this.textBox1.Text;
            c.nom = this.textBox2.Text;
            c.dir = this.textBox3.Text;
            c.tel = this.textBox4.Text;
            c.cel = this.textBox5.Text;
            c.email = this.textBox6.Text;
            c.act = this.checkBox1.Checked;
            //this.label10.Text = "EMPLEADO: " + c.NombreCompleto;
            if (ruta != null)
                c.foto = convertir_imagen.Image2Bytes(Image.FromFile(String.Format(ruta)));

            int rowsAffected = 0;
            if (string.IsNullOrEmpty(_id))
                rowsAffected = op_empleados.accion(c, 1);
            else
            {
                this.textBox1.ReadOnly = true;
                rowsAffected = op_empleados.accion(c, 2);
            }


            if (rowsAffected > 0)
            {
                if (rowsAffected == 1)
                {
                    MessageBox.Show(mensajes.MsjProc1, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    operacion();
                    this.toolStripButton3.Visible = true;
                    //toolStripButton2.Visible = false;
                }
                if (rowsAffected == 2)
                {
                    MessageBox.Show(mensajes.MsjProc2, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    operacion();
                    this.toolStripButton3.Visible = true;
                    //toolStripButton2.Visible = false;
                }
                if (rowsAffected == 3)
                {
                    MessageBox.Show(mensajes.MsjProc3, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    operacion();
                    this.toolStripButton3.Visible = true;
                    //toolStripButton2.Visible = false;
                }
                if (rowsAffected == 4)
                {
                    if (MessageBox.Show(mensajes.MsjProc4 + " " + mensajes.MsjProc5, Application.ProductName.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        rowsAffected = op_empleados.accion(c, 2);
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
                    if (e is TextBox || e is ComboBox || e is Button || e is CheckBox)
                        e.Enabled = false;
                }
                this.checkBox1.Enabled = false;
                this.toolStripButton3.Visible = true;
                this.toolStripButton1.Visible = false;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //toolStripButton2.Visible = true;
            toolStripButton3.Visible = false;
            toolStripButton5.Visible = false;
            toolStripButton1.Visible = true;
          
            //this.label10.Text = "EDITANDO EMPLEADO...";
            foreach (Control i in this.Controls)
            {
                if (i is TextBox || i is ComboBox || i is Button)
                    i.Enabled = true;
            }
            this.checkBox1.Enabled = true;
            this.textBox1.ReadOnly = true;
            textBox1.Focus();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

            _id = null;
            //this.label10.Text = "NUEVO EMPLEADO...";
            toolStripButton3.Visible = false;
            toolStripButton5.Visible = false;
            toolStripButton1.Visible = true;
           
            pictureBox2.Image = null;

            foreach (Control i in this.Controls)
            {
                if (i is TextBox || i is ComboBox || i is Button)
                {
                    i.Enabled = true;
                    if (!(i is Button))
                    i.Text = null;
                }
            }
            this.checkBox1.Enabled = true;
            this.textBox1.ReadOnly = false;
            textBox1.Focus();
        }
        private void frmempleadosdet_Load(object sender, EventArgs e)
        {
            IntPtr hmenu = GetSystemMenu(this.Handle, 0);
            int cnt = GetMenuItemCount(hmenu);
            // remove 'close' action
            RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            // remove extra menu line
            RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            DrawMenuBar(this.Handle);

            comboBox1.DataSource = op_combos.FillCombo(@"SELECT cod, nom FROM cargos ORDER BY nom ASC");
            comboBox1.DisplayMember = "nom";
            comboBox1.ValueMember = "cod";
            comboBox1.Text = null;

            comboBox2.DataSource = op_documentos.documentos();
            comboBox2.DisplayMember = "nom";
            comboBox2.ValueMember = "cod";
            comboBox2.Text = null;

            //toolStripButton2.Visible = true;
            if (!string.IsNullOrEmpty(_id))
            {
                empleados c = op_empleados.GellIdempleados(_id, 1);

                if (c != null)
                {
                    this.Text = "Editando Empleado: " + c.NombreCompleto;
                    this.comboBox1.SelectedValue = c.cargo;
                    this.comboBox2.SelectedValue = c.tipid;
                    this.textBox1.Text = c.cod;
                    this.textBox2.Text = c.nom;
                    this.textBox3.Text = c.dir;
                    this.textBox4.Text = c.tel;
                    this.textBox5.Text = c.cel;
                    this.textBox6.Text = c.email;
                    this.checkBox1.Checked = c.act;
                    if (pictureBox2.Image == null)
                    this.pictureBox2.Image = convertir_imagen.Bytes2Image(c.foto);

                    toolStripButton3.Visible = false;
                    toolStripButton5.Visible = true;
                    toolStripButton1.Visible = false;
                   

                    foreach (Control i in this.Controls)
                    {
                        if (i is TextBox || i is ComboBox || i is CheckBox || i is Button)
                            i.Enabled = false;
                    }
                }
            }
            else
            {
                this.Text = "Registro Nuevo Empleado";
                toolStripButton3.Visible = false;
                toolStripButton5.Visible = false;
                toolStripButton1.Visible = true;
                //toolStripButton2.Visible = false;
               
            }
            _id = null;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox1.Focus();
        }
        private string ruta = null;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Archivo JPG|*.jpg";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox2.Image = Image.FromFile(fileDialog.FileName);
                ruta = fileDialog.FileName;
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            frmcargosdet frm = new frmcargosdet();
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Focus();

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripButton6_Click_1(object sender, EventArgs e)
        {
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmcargosdet frm = new frmcargosdet();
            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox1.Enabled)
            {

                comboBox1.DataSource = op_combos.FillCombo(@"SELECT cod, nom FROM cargos ORDER BY nom ASC");
                comboBox1.DisplayMember = "nom";
                comboBox1.ValueMember = "cod";
                comboBox1.Text = null;
            }

        }

       
    }
}
