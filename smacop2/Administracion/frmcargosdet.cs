using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using smacop2.Entity;
using smacop2.Operaciones;
using System.Runtime.InteropServices;


namespace smacop2
{
    public partial class frmcargosdet : Form
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

        public frmcargosdet()
        {
            InitializeComponent();
            textBox1.KeyPress+=new KeyPressEventHandler(textBox1_KeyPress);
            textBox2.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            textBox3.KeyPress += new KeyPressEventHandler(textBox3_KeyPress);
            textBox4.KeyPress += new KeyPressEventHandler(textBox3_KeyPress);
            textBox4.KeyDown+=new KeyEventHandler(textBox4_KeyDown);
            textBox4.GotFocus += new EventHandler(textBox4_GotFocus);
         
            this.KeyDown+=new KeyEventHandler(frmcargosdet_KeyDown);
          
        }
        //private Entity.cargos c { get; set; }
        private string _id;
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

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
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

        public void textBox4_GotFocus(object sender, EventArgs e)
        {
            //textBox4.Text=
        }

        public frmcargosdet(string id)
            : this()
        {
            _id = id;
           
        }

        private bool validacion()
        {
            bool o = true;
            errorProvider1.Clear();

            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "El Código es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "El Descripción es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "El Nivel es Obligatorio");
                o = false;
            }
            
            return o;
        }

        private void frmcargosdet_Load(object sender, EventArgs e)
        {
            IntPtr hmenu = GetSystemMenu(this.Handle, 0);
            int cnt = GetMenuItemCount(hmenu);
            // remove 'close' action
            RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            // remove extra menu line
            RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            DrawMenuBar(this.Handle);


            if (!string.IsNullOrEmpty(_id))
            {
                cargos c = op_cargos.GellIdCargos(_id, 1);

                if (c != null)
                {
                    this.Text = "Editando Registro:["+c.NombreCompleto+"]";
                    this.textBox1.Text = c.cod;
                    this.textBox2.Text = c.nom;
                    this.textBox3.Text = c.niv.ToString();
                    this.textBox4.Text = c.sal.ToString();
                    //this.label10.Text = "CARGO: " + c.NombreCompleto;

                    toolStripButton3.Visible = false;
                    toolStripButton5.Visible = true;
                    toolStripButton1.Visible = false;
                   
                    foreach (Control i in this.Controls)
                    {
                        if (i is TextBox)
                            i.Enabled = false;
                    }
                }
            }
            else
            {
                toolStripButton3.Visible = false;
                toolStripButton5.Visible = false;
                toolStripButton1.Visible = true;
                this.Text = "Nuevo Registro de Cargos";
                //this.label10.Text = "NUEVO CARGO...";
               
            }
            _id = null;

        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            if (!validacion()) return;

            //cargos c = new cargos();
            ////this.label10.Text = "CARGO: " + c.NombreCompleto;
            //c.cod = this.textBox1.Text;
            //c.nom = this.textBox2.Text;
            //c.niv = int.Parse(this.textBox3.Text);
            //c.sal = string.IsNullOrEmpty(this.textBox4.Text) ? 0 : decimal.Parse(this.textBox4.Text);
            //int recibe = 0;



            int rowsAffected = 0, opc = 0;
            if (string.IsNullOrEmpty(_id))
                opc = 1;
            else
            {
                opc = 2;
                this.textBox1.ReadOnly = true;
            }
        u:
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();
                    MySqlCommand command = new MySqlCommand("SP_cargos", con);
                    command.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramId = new MySqlParameter("msj", MySqlDbType.Int32);
                    paramId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(paramId);

                    command.Parameters.AddWithValue("cod1", this.textBox1.Text);
                    command.Parameters.AddWithValue("nom1", this.textBox2.Text);
                    command.Parameters.AddWithValue("niv1", int.Parse(this.textBox3.Text));
                    command.Parameters.AddWithValue("sal1", string.IsNullOrEmpty(this.textBox4.Text) ? 0 : decimal.Parse(this.textBox4.Text));
                    command.Parameters.AddWithValue("opc", opc);

                    command.ExecuteNonQuery();
                    rowsAffected = int.Parse(command.Parameters["msj"].Value.ToString());

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
                                //accion(c, 2);
                                opc = 2;
                                goto u;
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show(mensajes.MsjProc0, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex1)
            {
                MessageBox.Show("Error: " + ex1.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //if (string.IsNullOrEmpty(_id))
            //{
            //    recibe=op_cargos.accion(c, 1);
            //    //this.label10.Text = "CARGO: " + c.NombreCompleto;
            //}
            //else
            //{
            //    this.textBox1.ReadOnly = true;
            //    recibe=op_cargos.accion(c, 2);
            //    //this.label10.Text = "CARGO: " + c.NombreCompleto;
            //}


            //if (recibe > 0 )
            //{
            //    operacion();

            //    this.toolStripButton3.Visible = true;
            //    //this.label10.Text = "CARGO: " + c.NombreCompleto;
            //}

        }
        private void operacion()
        {
                foreach (Control e in this.Controls)
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
            //this.label10.Text = "EDITANDO CARGO...";
            toolStripButton3.Visible = false;
            toolStripButton5.Visible = false;
            toolStripButton1.Visible = true;
          
            foreach (Control i in this.Controls)
            {
                if (i is TextBox)
                    i.Enabled = true;
            }
            this.textBox1.ReadOnly = true;
            textBox1.Focus();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            _id = null;
            //this.label10.Text = "NUEVO CARGO...";
            toolStripButton3.Visible = false;
            toolStripButton5.Visible = false;
            toolStripButton1.Visible = true;
           
            foreach (Control i in this.Controls)
            {
                if (i is TextBox)
                {
                    i.Enabled = true;
                    i.Text = null;
                }
            }
            this.textBox1.ReadOnly = false;
            textBox1.Focus();
        }


       

      
    }
}
