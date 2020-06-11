using System;
using System.Windows.Forms;
using smacop2.Entity;
using smacop2.Operaciones;
using System.Runtime.InteropServices;

namespace smacop2.Administracion
{

    public partial class frmclientesdet : Form
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

        public frmclientesdet()
        {
            InitializeComponent();
            textBox1.KeyPress += new KeyPressEventHandler(textBox3_KeyPress);
            textBox2.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            textBox3.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            textBox4.KeyPress += new KeyPressEventHandler(textBox3_KeyPress);
            textBox5.KeyPress += new KeyPressEventHandler(textBox3_KeyPress);
            textBox6.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            textBox7.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            textBox6.KeyDown += new KeyEventHandler(textBox6_KeyDown);
            this.KeyDown += new KeyEventHandler(frmcargosdet_KeyDown);
        }

          public frmclientesdet(string id)
            : this()
        {
            _id = id;
           
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
              if (string.IsNullOrEmpty(this.comboBox3.Text))
              {
                  errorProvider1.SetError(comboBox3, "El Tipo de ID es Obligatorio");
                  o = false;
              }
              if (string.IsNullOrEmpty(this.textBox1.Text))
              {
                  errorProvider1.SetError(textBox1, "El No. ID es Obligatorio");
                  o = false;
              }
              if (string.IsNullOrEmpty(this.textBox2.Text))
              {
                  errorProvider1.SetError(textBox2, "La Razón social es Obligatoria");
                  o = false;
              }
            
              if (string.IsNullOrEmpty(this.textBox5.Text) && string.IsNullOrEmpty(this.textBox4.Text) )
              {
                  errorProvider1.SetError(textBox3, "El Móvil o Telefono es Obligatorio");
                  o = false;
              }
              if (string.IsNullOrEmpty(this.textBox7.Text))
              {
                  errorProvider1.SetError(textBox7, "El Contacto es Obligatorio");
                  o = false;
              }
              return o;
          }




          private void toolStripButton1_Click(object sender, EventArgs e)
          {
              if (!validacion()) return;
              clientes c = new clientes();

              c.tipid = this.comboBox3.SelectedValue.ToString();
              c.cod = this.textBox1.Text;
              c.nom = this.textBox2.Text;
              c.cargo = this.textBox7.Text;
              c.dir = this.textBox3.Text;
              c.tel = this.textBox4.Text;
              c.cel = this.textBox5.Text;
              c.email = this.textBox6.Text;


              //this.label10.Text = "CLIENTE/CONTRATISTA: " + c.NombreCompleto;

              int rowsAffected = 0;
              if (string.IsNullOrEmpty(_id))
                  rowsAffected = op_clientes.accion(c, 1);
              else
              {
                  this.textBox1.ReadOnly = true;
                  rowsAffected = op_clientes.accion(c, 2);
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
                          rowsAffected = op_clientes.accion(c, 2);
                      }

                  }
              }
              else
              {
                  MessageBox.Show(mensajes.MsjProc0, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              }


              //if (recibe > 0 )
              //{
              //    operacion();
               
              //    this.toolStripButton3.Visible = true;
              //    foreach (Control u in this.Controls)
              //    {
              //        if (u is Button)
              //            u.Enabled = true;
              //    }
              //    //this.label10.Text = "CLIENTE/CONTRATISTA: " + c.NombreCompleto;
              //}


          }
          private void operacion()
          {
              foreach (Control e in this.Controls)
              {
                  if (e is TextBox || e is ComboBox )
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
              //this.label10.Text = "EDITANDO CLIENTE/CONTRATISTA...";
              //_id = null;
              toolStripButton3.Visible = false;
              toolStripButton5.Visible = false;
              toolStripButton1.Visible = true;
             
              foreach (Control i in this.Controls)
              {
                  if (i is TextBox || i is ComboBox )
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
              //this.label10.Text = "NUEVO CLIENTE/CONTRATISTA...";
              _id = null;
              toolStripButton3.Visible = false;

              toolStripButton5.Visible = false;
              toolStripButton1.Visible = true;
             
              foreach (Control i in this.Controls)
              {
                  if (i is TextBox || i is ComboBox )
                  {
                      i.Enabled = true;
                      i.Text = null;
                  }

                
              }

              this.textBox1.ReadOnly = false;
              textBox1.Focus();
          }
        

          private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
          {
              //if (!string.IsNullOrEmpty(comboBox1.Text))
              //{
              //    comboBox2.DataSource = op_combos.FillCombo(@"SELECT cod, nom, dpto FROM mpio where dpto='" + this.comboBox1.SelectedValue.ToString() + "' ORDER BY nom ASC");
              //    comboBox2.DisplayMember = "nom";
              //    comboBox2.ValueMember = "cod";
              //    //comboBox2.Text = null;
              //}
              ////this.textBox1.Focus();
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

      

        

        private void frmclientesdet_Load(object sender, EventArgs e)
        {
            IntPtr hmenu = GetSystemMenu(this.Handle, 0);
            int cnt = GetMenuItemCount(hmenu);
            // remove 'close' action
            RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            // remove extra menu line
            RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            DrawMenuBar(this.Handle);


            //comboBox1.DataSource = op_combos.FillCombo(@"SELECT cod, nom FROM dpto ORDER BY nom ASC");
            //comboBox1.DisplayMember = "nom";
            //comboBox1.ValueMember = "cod";
            //comboBox1.Text = null;

            comboBox3.DataSource = op_documentos.documentos();
            comboBox3.DisplayMember = "nom";
            comboBox3.ValueMember = "cod";
            comboBox3.Text = null;


            if (!string.IsNullOrEmpty(_id))
            {
                clientes c = op_clientes.GellIdClientes(_id, 1);
                if (c != null)
                {
                    this.Text = "Editando Cliente: " + c.cod + "-" + c.nom;
                    this.textBox1.Text = c.cod;
                    this.textBox2.Text = c.nom;
                    this.textBox3.Text = c.dir;
                    this.textBox4.Text = c.tel;
                    this.textBox5.Text = c.cel;
                    this.textBox6.Text = c.email;
                    this.textBox7.Text = c.cargo;

                    this.comboBox3.SelectedValue = c.tipid;

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
                this.Text = "Nuevo Registro de Cliente";
                toolStripButton3.Visible = false;
                toolStripButton5.Visible = false;
                toolStripButton1.Visible = true;
             
            }
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            this.textBox7.Text = textBox2.Text;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox1.Focus();
        }

        

        private void button6_Click(object sender, EventArgs e)
        {
            frmproyectos frm = new frmproyectos(textBox1.Text);
            frm.Text = string.Concat("CENTRO DE COSTO DE ", textBox2.Text);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

      
        
        

       
      

       
      
    }
}
