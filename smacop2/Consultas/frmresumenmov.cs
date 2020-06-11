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
using smacop2.Entity;
using smacop2.Operaciones;
using System.Runtime.InteropServices;


namespace smacop2.Consultas
{
    public partial class frmresumenmov : Form
    {
        public frmresumenmov()
        {
            InitializeComponent();
            this.radioButton1.Click+=new EventHandler(radioButton1_Click);
            this.radioButton2.Click += new EventHandler(radioButton2_Click);
            this.dataGridView3.CellContentClick += new DataGridViewCellEventHandler(dataGridView5_CellContentClick);
        }

        
        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             if (radioButton1.Checked)
            {
                if (!validacion()) return;
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
                   op_var.b= op_var.a.AddDays(int.Parse(numd)); //resto un día al mes y con esto obtengo el ultimo dí         
                }
                panel2.Enabled = true;
                panel3.Enabled = false;
            }
            else 
            {
                ErrorProvider u = new ErrorProvider();
                if (dateTimePicker1.Value >= dateTimePicker2.Value)
                {
                   
                    u.SetError(dateTimePicker1,"La fecha dede ser menor a la final");
                    return;
                }
                if (dateTimePicker2.Value <= dateTimePicker1.Value)
                {
                   
                    u.SetError(dateTimePicker2, "La fecha dede ser mayor a la Inicial");
                    return;
                }

                u.SetError(dateTimePicker2, null);
                u.SetError(dateTimePicker1, null);

                op_var.a = dateTimePicker1.Value;
                op_var.b = dateTimePicker2.Value;
                panel2.Enabled = false;
                panel3.Enabled = true;
            }

            try
            {
                if (e.ColumnIndex >= 0)
                {
                    DataGridViewRow row = dataGridView3.CurrentRow as DataGridViewRow;
                    //this.toolStripLabel1.Text = row.Cells[0].Value.ToString();
                    if (this.dataGridView3.Columns[e.ColumnIndex].Name == "Column1") //salidasmat
                    {
                        frmverificacion frm = new frmverificacion(op_var.a, op_var.b, row.Cells[0].Value.ToString(),null,1);
                        frm.MdiParent = this.MdiParent;
                        frm.Show();
                    }

                    if (this.dataGridView3.Columns[e.ColumnIndex].Name == "dataGridViewTextBoxColumn2") //entradasmat
                    {
                        frmverificacion frm = new frmverificacion(op_var.a, op_var.b, row.Cells[0].Value.ToString(),null, 2);
                        frm.MdiParent = this.MdiParent;
                        frm.Show();
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
        DateTime _a, _b;
        public frmresumenmov(DateTime a, DateTime b):this()
        {
            _a = a;
            _b = b;
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
        private void radioButton1_Click(object sender, EventArgs e)
        {
            panel2.Enabled = true;
            panel3.Enabled = false;
            this.errorProvider1.SetError(dateTimePicker1, null);
            this.errorProvider1.SetError(dateTimePicker2, null);
            this.errorProvider1.SetError(comboBox5, null);
            this.errorProvider1.SetError(comboBox6, null);
        }
        private void frmresumenmov_Load(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select year(fech) as fech  from salidasmat group by year(fech);", conn);
                this.comboBox6.Items.Clear();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        this.comboBox6.Items.Add(Convert.ToString(reader[0]));
                }
            }
        }
        private bool validacion()
        {
            bool o = true;
           

            if (string.IsNullOrEmpty(comboBox5.Text))
            {
                this.errorProvider1.SetError(comboBox5, "El Mes es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(comboBox6.Text))
            {
                this.errorProvider1.SetError(comboBox6, "El año es Obligatorio");
                o = false;
            }
            return o;
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            status fr = new status();
            fr.Show("Analizando Datos");
            op_sql.parametro1("DELETE from tmp_liq_eq");
           

            if (radioButton1.Checked)
            {
                if (!validacion()) return;
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
                   op_var.b= op_var.a.AddDays(int.Parse(numd)); //resto un día al mes y con esto obtengo el ultimo dí         
                }
                panel2.Enabled = true;
                panel3.Enabled = false;
            }
            else 
            {
            
                if (dateTimePicker1.Value >= dateTimePicker2.Value)
                {
                   
                   this.errorProvider1.SetError(dateTimePicker1,"La fecha dede ser menor a la final");
                    return;
                }
                if (dateTimePicker2.Value <= dateTimePicker1.Value)
                {
                   
                  this.errorProvider1.SetError(dateTimePicker2, "La fecha dede ser mayor a la Inicial");
                    return;
                }

                op_var.a = dateTimePicker1.Value;
                op_var.b = dateTimePicker2.Value;
                panel2.Enabled = false;
                panel3.Enabled = true;
            }

            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();
                    if (comboBox4.Text == "INTERNOS")
                    {
                        this.Cursor = Cursors.WaitCursor;
                        MySqlCommand command = new MySqlCommand("SP_liq_eq", con);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("inicio", op_var.a);
                        command.Parameters.AddWithValue("fin", op_var.b);
                        command.ExecuteNonQuery();
                        status fr1 = new status();
                        fr1.Show("Cargando Datos");
                        dataGridView3.AutoGenerateColumns = false;
                        dataGridView3.DataSource = op_salidasmat.Gellresumenall(op_salidasmat.ac.interno);
                        this.toolStripStatusLabel4.Text = this.dataGridView3.RowCount.ToString();
                        this.Cursor = Cursors.Default;
                    }
                    else
                    {
                        this.Cursor = Cursors.WaitCursor;
                        MySqlCommand command = new MySqlCommand("SP_liq_eq_ext", con);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("inicio", op_var.a);
                        command.Parameters.AddWithValue("fin", op_var.b);
                        command.ExecuteNonQuery();
                        status fr1 = new status();
                        fr1.Show("Cargando Datos");
                        dataGridView3.AutoGenerateColumns = false;
                        dataGridView3.DataSource = op_salidasmat.Gellresumenall(op_salidasmat.ac.externo);
                        this.toolStripStatusLabel4.Text = this.dataGridView3.RowCount.ToString();
                        this.Cursor = Cursors.Default;
                    } 
                }
                if (dataGridView3.Rows.Count > 0)
                {
                   this.errorProvider1.SetError(dateTimePicker2, null);
                   this.errorProvider1.SetError(dateTimePicker1, null);
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
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
        //    op_sql.parametro1("DELETE from tmp_liq_eq");
        //    this.Close();
        //    if frmmenu.
        //     If Me.Tabs.TabPages.Count = 0 Then Exit Sub
        //CType(Me.Tabs.SelectedTab.Tag, Form).Close()
        //Me.Tabs.TabPages.Remove(Me.Tabs.SelectedTab)
            
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (this.dataGridView3.RowCount > 0)
                op_exportacion.opcion(dataGridView3, "xls Excel (*.xls)|*.xls", 0, this.Text);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (this.dataGridView3.RowCount > 0)
                op_exportacion.opcion(dataGridView3, "csv (*.csv)|*.csv", 1, this.Text);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (this.dataGridView3.RowCount > 0)
                op_exportacion.opcion(dataGridView3, "html (*.html)|*.html", 0, this.Text);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

     

    
    }
}
