using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using MySql.Data.MySqlClient;
using smacop2.Entity;
using smacop2.Operaciones;
using System.Runtime.InteropServices;

namespace smacop2.Administracion
{
    public partial class frmcostomaterial : Form
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

        public frmcostomaterial()
        {
            InitializeComponent();
            this.textBox1.KeyPress += new KeyPressEventHandler(numero);
        }
        int u = 0;
        private void numero(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))) e.Handled = true;
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
        private string a, b;

        public frmcostomaterial(string _a, string _b): this()
        {
            a = _a;
            b = _b;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmcostomaterial_Load(object sender, EventArgs e)
        {
            IntPtr hmenu = GetSystemMenu(this.Handle, 0);
            int cnt = GetMenuItemCount(hmenu);
            // remove 'close' action
            RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            // remove extra menu line
            RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            DrawMenuBar(this.Handle);

            comboBox2.DataSource = op_combos.FillCombo("select cod, nom from materiales where tip='EXT'");
            comboBox2.DisplayMember = "nom";
            comboBox2.ValueMember = "cod";
            comboBox2.Text = null;

            if (!(string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b)))
            {

               cant_material c = op_proveedores.GellIdcanteraxproducto1(a, b);

                if (c != null)
                {
                   
                    this.comboBox2.SelectedValue = c.cod;
                    this.textBox1.Text = c.costo.ToString();
                    //operacion();
                    comboBox2.Enabled = false;
                    //button1.Enabled = true;
                    //button2.Enabled = true;
                }
                u = 2;
            }
            else
                u = 1;

      
        }

        private void toolStripButton5_Click(object sender, System.EventArgs e)
        {

        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        private bool validacion()
        {
            bool o = true;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                errorProvider1.SetError(textBox1,"El Costo es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.comboBox2.Text))
            {
                errorProvider1.SetError(comboBox2, "El Material es Obligatorio");
                o = false;
            }
            return o;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (!validacion()) return;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
               
                    con.Open();
                p:
                    MySqlCommand cmd = new MySqlCommand("sp_materiales_cantera", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramId = new MySqlParameter("msj", MySqlDbType.Int32);
                    paramId.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(paramId);

                    cmd.Parameters.AddWithValue("cant", op_var.tmp_cantera);
                    cmd.Parameters.AddWithValue("mate", this.comboBox2.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("cost", Convert.ToDecimal(textBox1.Text));
                    cmd.Parameters.AddWithValue("opc", u);
                    int i = cmd.ExecuteNonQuery();
                    int rowsAffected = int.Parse(cmd.Parameters["msj"].Value.ToString());
                    if (rowsAffected == 4)
                    {
                        if (MessageBox.Show(mensajes.MsjProc4 + " " + mensajes.MsjProc5, Application.ProductName.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            u = 2;
                            goto p;
                        }
                    }
                    if (i > 0)
                    {
                        if (rowsAffected == 1)
                        {
                            MessageBox.Show(mensajes.MsjProc1, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            op_var.tmp_cantera = null;
                        }
                        if (rowsAffected == 2)
                        {
                            MessageBox.Show(mensajes.MsjProc2, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            op_var.tmp_cantera = null;
                        }
                        if (rowsAffected == 3)
                        {
                            MessageBox.Show(mensajes.MsjProc3, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            op_var.tmp_cantera = null;
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(mensajes.MsjProc0, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error tecnico: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void operacion()
        {
            foreach (Control r in this.Controls)
            {
                if (r is TextBox || r is ComboBox)
                {
                    r.Enabled = true;
                    //r.Text = null;
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.textBox1.Focus();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            comboBox2.DataSource = op_combos.FillCombo("select cod, nom from materiales where tip='EXT'");
            comboBox2.DisplayMember = "nom";
            comboBox2.ValueMember = "cod";
            comboBox2.Text = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Administracion.frmmaterialesdet f = new Administracion.frmmaterialesdet();
            f.ShowDialog();
        }
    }
}
