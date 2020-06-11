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
    public partial class frmproduccionmescm : Form
    {
        public frmproduccionmescm()
        {
            InitializeComponent();
            this.radioButton1.Click += new EventHandler(radioButton1_Click);
            this.radioButton2.Click += new EventHandler(radioButton2_Click);
            this.dataGridView1.MouseDoubleClick +=new MouseEventHandler(dataGridView1_MouseDoubleClick);
            this.dataGridView2.MouseDoubleClick += new MouseEventHandler(dataGridView2_MouseDoubleClick);
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                DataGridViewRow row = dataGridView1.CurrentRow as DataGridViewRow;
                frmverificacion frm = new frmverificacion(op_var.a, op_var.b, this.comboBox1.Text, row.Cells[1].Value.ToString(), 7);
       
                frm.ShowDialog();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                DataGridViewRow row = dataGridView2.CurrentRow as DataGridViewRow;
                frmverificacion frm;
                if (comboBox2.Text == "CANTERAS")
                    frm = new frmverificacion(op_var.a, op_var.b, this.comboBox1.Text, row.Cells[1].Value.ToString(), 9);
                else
                    frm = new frmverificacion(op_var.a, op_var.b, this.comboBox1.Text, row.Cells[1].Value.ToString(), 8);
               
                frm.MdiParent = this.MdiParent;
                frm.ShowDialog();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            panel2.Enabled = false;
            panel3.Enabled = true;
            this.errorProvider1.SetError(dateTimePicker3, null);
            this.errorProvider1.SetError(dateTimePicker4, null);
            this.errorProvider1.SetError(comboBox5, null);
            this.errorProvider1.SetError(comboBox6, null);
        }
        private void radioButton1_Click(object sender, EventArgs e)
        {
            panel2.Enabled = true;
            panel3.Enabled = false;
            this.errorProvider1.SetError(dateTimePicker3, null);
            this.errorProvider1.SetError(dateTimePicker4, null);
            this.errorProvider1.SetError(comboBox5, null);
            this.errorProvider1.SetError(comboBox6, null);
        }

        private void frmproduccionmespar_Load(object sender, EventArgs e)
        {
          

            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select year(fech) as fech  from entradasmat group by year(fech);", conn);


                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        this.comboBox6.Items.Add(Convert.ToString(reader[0]));
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string tabla=null;

            //if (comboBox1.SelectedIndex==0)
            //    tabla = "entradasmat ";
            //else
            //    tabla="salidasmat ";

            //using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            //{
            //    conn.Open();
            //    MySqlCommand cmd = new MySqlCommand("select year(fech) as fech  from "+tabla+" group by year(fech);", conn);
            //    this.comboBox6.Items.Clear();
            //    MySqlDataReader reader = cmd.ExecuteReader();
            //    if (reader.HasRows == true)
            //    {
            //        while (reader.Read())
            //            this.comboBox6.Items.Add(Convert.ToString(reader[0]));
            //    }
            //}
        }
        private bool validacion()
        {
            bool o = true;
            ErrorProvider u = new ErrorProvider();

            if (string.IsNullOrEmpty(comboBox5.Text))
            {
                u.SetError(comboBox5, "El Mes es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(comboBox6.Text))
            {
                u.SetError(comboBox6, "El año es Obligatorio");
                o = false;
            }
            return o;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            status fr = new status();
            fr.Show("Analizando Datos");
          

            if (radioButton2.Checked)
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
                    op_var.b = op_var.a.AddDays(int.Parse(numd)); //resto un día al mes y con esto obtengo el ultimo dí     
              
                }
                panel2.Enabled = true;
                panel3.Enabled = false;

                if (comboBox2.Text == "CANTERAS")
                {
                    dataGridView1.AutoGenerateColumns = false;
                    this.dataGridView1.DataSource = op_entradasmat.GellAllcmat1(op_var.a, op_var.b, comboBox1.SelectedValue.ToString());

                    dataGridView2.AutoGenerateColumns = false;
                    this.dataGridView2.DataSource = op_salidasmat.GellAllcmat1(op_var.a, op_var.b, comboBox1.SelectedValue.ToString());
                }
                else
                {
                    dataGridView1.AutoGenerateColumns = false;
                    this.dataGridView1.DataSource = op_entradasmat.GellAllcmat2(op_var.a, op_var.b, comboBox1.SelectedValue.ToString());

                    dataGridView2.AutoGenerateColumns = false;
                    this.dataGridView2.DataSource = op_salidasmat.GellAllcmat2(op_var.a, op_var.b, comboBox1.SelectedValue.ToString());
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            status fr = new status();
            fr.Show("Analizando Datos");

            if (radioButton1.Checked)
            {
                ErrorProvider u = new ErrorProvider();
                if (dateTimePicker4.Value > dateTimePicker3.Value)
                {
                    u.SetError(dateTimePicker4, "La fecha debe ser menor a la final");
                    return;
                }
                else if (dateTimePicker3.Value < dateTimePicker4.Value)
                {
                    u.SetError(dateTimePicker3, "La fecha debe ser mayor a la Inicial");
                    return;
                }
                else
                {
                    u.SetError(dateTimePicker3, null);
                    u.SetError(dateTimePicker4, null);
                }




                //op_var.a = dateTimePicker4.Value;
                //op_var.b = dateTimePicker3.Value;
                panel2.Enabled = false;
                if (comboBox2.Text == "CANTERAS")
                {
                    dataGridView1.AutoGenerateColumns = false;
                    this.dataGridView1.DataSource = op_entradasmat.GellAllcmat1(dateTimePicker4.Value, dateTimePicker3.Value, comboBox1.SelectedValue.ToString());

                    dataGridView2.AutoGenerateColumns = false;
                    this.dataGridView2.DataSource = op_salidasmat.GellAllcmat1(dateTimePicker4.Value, dateTimePicker3.Value, comboBox1.SelectedValue.ToString());
                }
                else
                {
                    dataGridView1.AutoGenerateColumns = false;
                    this.dataGridView1.DataSource = op_entradasmat.GellAllcmat2(dateTimePicker4.Value, dateTimePicker3.Value, comboBox1.SelectedValue.ToString());
                }

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            comboBox1.Text = null;
            comboBox2.Text = null;
            comboBox5.Text = null;
            comboBox6.Text = null;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
//            CANTERAS
//CENTROS DE COSTOS
            if (comboBox2.Text == "CENTROS DE COSTOS")
            {
                comboBox1.DataSource = op_combos.FillCombo("select idproy as cod, nomproy as nom from proyectos");
                comboBox1.DisplayMember = "nom";
                comboBox1.ValueMember = "cod";
                comboBox1.Text = null;
            }
            else
            {
                comboBox1.DataSource = op_combos.FillCombo("select cod, nom from proveedores");
                comboBox1.DisplayMember = "nom";
                comboBox1.ValueMember = "cod";
                comboBox1.Text = null;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (this.dataGridView2.RowCount > 0)
                op_exportacion.opcion(dataGridView2, "xls Excel (*.xls)|*.xls", 0, this.comboBox1.Text + op_var.a + op_var.b);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.RowCount > 0)
                op_exportacion.opcion(dataGridView1, "xls Excel (*.xls)|*.xls", 0, this.comboBox1.Text + op_var.a + op_var.b);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
