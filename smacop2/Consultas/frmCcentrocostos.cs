using System;
using System.Windows.Forms;
using smacop2.Operaciones;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Runtime.InteropServices;

namespace smacop2.Consultas
{

    public partial class frmCcentrocostos : Form
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
        public frmCcentrocostos()
        {
            InitializeComponent();
            this.dataGridView5.CellContentClick+=new DataGridViewCellEventHandler(dataGridView5_CellContentClick);
            this.radioButton1.Click += new EventHandler(radioButton1_Click);
            this.radioButton2.Click += new EventHandler(radioButton2_Click);
        }
        private void radioButton1_Click(object sender, EventArgs e)
        {
            panel2.Enabled = true;
            panel3.Enabled = false;
            this.errorProvider1.SetError(dateTimePicker1, null);
            this.errorProvider1.SetError(dateTimePicker2, null);
            this.errorProvider1.SetError(comboBox5, null);
            this.errorProvider1.SetError(comboBox6, null);
        }
        private void radioButton2_Click(object sender, EventArgs e)
        {
            panel2.Enabled = false;
            panel3.Enabled = true;
            this.errorProvider1.SetError(dateTimePicker1, null);
            this.errorProvider1.SetError(dateTimePicker2, null);
            this.errorProvider1.SetError(comboBox5, null);
            this.errorProvider1.SetError(comboBox6, null);


        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        private bool validacion()
        {
            bool o = true;
            errorProvider1.Clear();
            if (radioButton1.Checked)
            {
                if (string.IsNullOrEmpty(this.comboBox6.Text))
                {
                    errorProvider1.SetError(comboBox6, "El año es Obligatorio");
                    o = false;
                }

                if (string.IsNullOrEmpty(comboBox5.Text))
                {
                    errorProvider1.SetError(comboBox5, "El mes es Obligatorio");
                    o = false;
                }
            }
            else
            {
                if (dateTimePicker1.Value >= dateTimePicker2.Value)
                {
                    errorProvider1.SetError(dateTimePicker1, "La fecha dede ser menor a la final");
                    o = false;
                }
                if (dateTimePicker2.Value <= dateTimePicker1.Value)
                {
                    errorProvider1.SetError(dateTimePicker2, "La fecha dede ser mayor a la Inicial");
                    o = false;
                }
            }
            return o;
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (uso_general == false)
            {
                if (!validacion()) return;


                if (radioButton1.Checked)
                {
                    if (checkBox2.Checked == false)
                    {
                        op_var.a = Convert.ToDateTime(string.Concat("01", "/", Convert.ToString(comboBox5.SelectedIndex + 1).PadLeft(2, '0'), "/", comboBox6.Text));// pongo el 1 porque siempre es el primer día obvio
                        op_var.b = Convert.ToDateTime(string.Concat("01", "/", Convert.ToString(comboBox5.SelectedIndex + 2).PadLeft(2, '0'), "/", comboBox6.Text)).AddDays(-1); //resto un día al mes y con esto obtengo el ultimo día
                    }
                    else
                    {
                        string diai = op_sql.parametro1(@"SELECT c.`diainicial` FROM logicop.cohorte_tablas c  where tabla='entrada';");
                        string numd = op_sql.parametro1(@"SELECT c.`diafinal` FROM logicop.cohorte_tablas c where tabla='entrada';");

                        op_var.a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(op_var.mes(comboBox5.SelectedIndex) - 1).PadLeft(2, '0'), "/", comboBox6.Text));// pongo el 1 porque siempre es el primer día obvio
                        op_var.b = op_var.a.AddDays(int.Parse(numd)); //resto un día al mes y con esto obtengo el ultimo dí         
                    }
                    panel2.Enabled = true;
                    panel3.Enabled = false;

                }
                else
                {
                    //ErrorProvider u = new ErrorProvider();
                    //if (dateTimePicker1.Value >= dateTimePicker2.Value)
                    //{

                    //    u.SetError(dateTimePicker1, "La fecha dede ser menor a la final");
                    //    return;
                    //}
                    //if (dateTimePicker2.Value <= dateTimePicker1.Value)
                    //{

                    //    u.SetError(dateTimePicker2, "La fecha dede ser mayor a la Inicial");
                    //    return;
                    //}

                    //u.SetError(dateTimePicker2, null);
                    //u.SetError(dateTimePicker1, null);

                    op_var.a = dateTimePicker1.Value;
                    op_var.b = dateTimePicker2.Value;
                    panel2.Enabled = false;
                    panel3.Enabled = true;
                }
            }
            try
            {
                if (e.ColumnIndex >= 0)
                {
                    DataGridViewRow row = dataGridView5.CurrentRow as DataGridViewRow;
                    if (this.dataGridView5.Columns[e.ColumnIndex].Name == "Tipo") //salidasmat
                    {
                        if (uso_general == false)
                        {
                            frmverificacion frm = new frmverificacion(op_var.a, op_var.b, row.Cells[0].Value.ToString(),null, 3);
                            frm.MdiParent = this.MdiParent;
                            frm.Show();
                        }
                        else
                        {
                            frmverificacion frm = new frmverificacion(op_var.a, op_var.b, row.Cells[0].Value.ToString(),null, 4);
                            frm.MdiParent = this.MdiParent;
                            frm.Show();
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
        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            try
            {
                dataGridView5.DataSource = null;

                
                if (radioButton1.Checked)
                {
                    if ((string.IsNullOrEmpty(this.comboBox6.Text) || string.IsNullOrEmpty(this.comboBox5.Text)))
                    {
                        this.errorProvider1.SetError(comboBox6, "El año es Obligatorio");
                        this.errorProvider1.SetError(comboBox5, "El mes es Obligatorio");
                       
                    }
                    else
                    {
                       

                        if (checkBox2.Checked == false)
                        {
                            dataGridView5.AutoGenerateColumns = false;
                            dataGridView5.DataSource = op_proyectos.GellAllproyectosCP1(comboBox5.SelectedIndex + 1, int.Parse(comboBox6.Text));

                            //    fin = Convert.ToDateTime(string.Concat("01", "/", Convert.ToString(comboBox5.SelectedIndex + 2).PadLeft(2, '0'), "/", comboBox6.Text)).AddDays(-1); //resto un día al mes y con esto obtengo el ultimo día
                        }
                        else
                        {
                            DateTime inicio, fin;
                            this.errorProvider1.SetError(comboBox6, null);
                            this.errorProvider1.SetError(comboBox5, null);
                            string diai = op_sql.parametro1(@"SELECT c.`diainicial` FROM logicop.cohorte_tablas c  where tabla='entrada';");
                            string numd = op_sql.parametro1(@"SELECT c.`diafinal` FROM logicop.cohorte_tablas c where tabla='entrada';");
                            inicio = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(comboBox5.SelectedIndex + 1).PadLeft(2, '0'), "/", comboBox6.Text));// pongo el 1 porque siempre es el primer día obvio
                            fin = inicio.AddDays(int.Parse(numd) - 1); //resto un día al mes y con esto obtengo el ultimo día
                            dataGridView5.AutoGenerateColumns = false;
                            dataGridView5.DataSource = op_proyectos.GellAllproyectosCP2(inicio, fin);
                        }
                    }


                }
                else
                {
                    dataGridView5.DataSource = op_proyectos.GellAllproyectosCP2(dateTimePicker1.Value, dateTimePicker2.Value);
                }

                uso_general = false;


                this.toolStripStatusLabel4.Text = dataGridView5.Rows.Count.ToString();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmCcentrocostos_Load(object sender, EventArgs e)
        {
            IntPtr hmenu = GetSystemMenu(this.Handle, 0);
            int cnt = GetMenuItemCount(hmenu);
            // remove 'close' action
            RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            // remove extra menu line
            RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            DrawMenuBar(this.Handle);
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select year(fech) as fech  from salidasmat group by year(fech);", conn);


                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        this.comboBox6.Items.Add(Convert.ToString(reader[0]));
                }
            }
        }

       
        private bool uso_general = false;

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            uso_general = true;
            dataGridView5.AutoGenerateColumns = false;
            dataGridView5.DataSource = op_proyectos.GellAllproyectosC();
            this.toolStripStatusLabel4.Text = dataGridView5.Rows.Count.ToString();
            comboBox5.Text = null;
            comboBox6.Text = null;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (this.dataGridView5.RowCount > 0)
                op_exportacion.opcion(dataGridView5, "xls Excel (*.xls)|*.xls", 0, this.Text);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (this.dataGridView5.RowCount > 0)
                op_exportacion.opcion(dataGridView5, "csv (*.csv)|*.csv", 1, this.Text);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (this.dataGridView5.RowCount > 0)
                op_exportacion.opcion(dataGridView5, "html (*.html)|*.html", 0, this.Text);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
