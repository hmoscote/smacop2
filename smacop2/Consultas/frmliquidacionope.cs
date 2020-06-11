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

    public partial class frmliquidacionope : Form
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
        public frmliquidacionope()
        {
            InitializeComponent();
            this.listBox1.MouseDoubleClick+=new MouseEventHandler(listBox1_MouseDoubleClick);
            this.dataGridView2.CellDoubleClick+=new DataGridViewCellEventHandler(dataGridView2_CellDoubleClick);
            this.dataGridView3.CellDoubleClick += new DataGridViewCellEventHandler(dataGridView3_CellDoubleClick);
            this.radioButton1.Click += new EventHandler(radioButton1_Click);
            this.radioButton2.Click += new EventHandler(radioButton2_Click);
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
        public bool opcion = false;
        private string diai = op_sql.parametro1(@"SELECT c.`diainicial` FROM logicop.cohorte_tablas c  where tabla='entrada';");
        private string numd = op_sql.parametro1(@"SELECT c.`diafinal` FROM logicop.cohorte_tablas c where tabla='entrada';");

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           op_var.ope = listBox1.SelectedValue.ToString();
           dataGridView2.AutoGenerateColumns = false;
           dataGridView2.DataSource = op_comision.GellAllCargos(op_comision.vista.maestro, op_comision.opciones.parametro_fecha_operador,null);
           dataGridView3.DataSource = null;
           this.toolStripStatusLabel2.Text = dataGridView2.RowCount.ToString();
           this.toolStripStatusLabel4.Text = dataGridView3.RowCount.ToString();
           toolStripButton2.Enabled = false;
        }



        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (avisar_parametrizada == true)
            {

                if (checkBox2.Checked == false)
                {
                    op_var.a = op_var.cohorte(1, comboBox5.SelectedIndex + 1, int.Parse(comboBox6.Text));
                    op_var.b = op_var.cohorte(1, comboBox5.SelectedIndex + 2, int.Parse(comboBox6.Text)).AddDays(-1);
                }
                else
                {
                    op_var.a = op_var.cohorte(int.Parse(diai), comboBox5.SelectedIndex, int.Parse(comboBox6.Text));
                    op_var.b = op_var.a.AddDays(int.Parse(numd));
                }
            }
            else
            {
                op_var.b = op_var.cohorte(int.Parse(diai) - 1, DateTime.Now.Month, DateTime.Now.Year);
                if (op_var.b > DateTime.Now)
                {
                    op_var.a = op_var.cohorte(int.Parse(diai), DateTime.Now.Month - 1, DateTime.Now.Year);
                    op_var.b = op_var.cohorte(int.Parse(diai) - 1, DateTime.Now.Month, DateTime.Now.Year);
                }
                else
                {
                    op_var.a = op_var.cohorte(int.Parse(diai), DateTime.Now.Month, DateTime.Now.Year);
                    op_var.b = op_var.cohorte(int.Parse(diai) - 1, DateTime.Now.Month + 1, DateTime.Now.Year);
                }
            }

            DataGridViewRow row = dataGridView2.CurrentRow as DataGridViewRow;
            dataGridView3.AutoGenerateColumns = false;
            op_var.ope=row.Cells["po"].Value.ToString();
            dataGridView3.DataSource = op_comision.GellAllCargos(op_comision.vista.detalle, op_comision.opciones.parametro_acceso, row.Cells["co"].Value.ToString());

            this.toolStripStatusLabel2.Text = dataGridView2.RowCount.ToString();
            this.toolStripStatusLabel4.Text = dataGridView3.RowCount.ToString();

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {        
            status fr = new status();
            fr.Show("Cargando Datos");

            op_var.b = op_var.cohorte(int.Parse(diai) - 1, DateTime.Now.Month, DateTime.Now.Year);
            if (op_var.b > DateTime.Now)
            {
                op_var.a = op_var.cohorte(int.Parse(diai), DateTime.Now.Month - 1, DateTime.Now.Year);
                op_var.b = op_var.cohorte(int.Parse(diai) - 1, DateTime.Now.Month, DateTime.Now.Year);
            }
            else
            {
                op_var.a = op_var.cohorte(int.Parse(diai), DateTime.Now.Month, DateTime.Now.Year);
                op_var.b = op_var.cohorte(int.Parse(diai) - 1, DateTime.Now.Month + 1, DateTime.Now.Year);
            }
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = op_comision.GellAllCargos(op_comision.vista.maestro, op_comision.opciones.parametro_fecha, null);
            
           
            toolStripButton2.Enabled = true;
            listBox1.DataSource = op_combos.FillCombo("select codempl as cod, nomempl as nom from empleados");
            listBox1.ValueMember = "cod";
            listBox1.DisplayMember = "nom";
            dataGridView3.DataSource = null;
            this.toolStripStatusLabel2.Text = dataGridView2.RowCount.ToString();
            this.toolStripStatusLabel4.Text = dataGridView3.RowCount.ToString();
            opcion = false;
            avisar_parametrizada = false;
            this.textBox1.Text = null;
            this.button2.Enabled = false;
            comboBox5.Text = null;
            comboBox6.Text = null;
            checkBox2.Checked = false;
            //dataGridView3.DataSource = op_comision.GellAllCargos(2);
            //SELECT 'SALIDAS' AS ACCESO,ope,  nomempl, count(nrec) as viajes,sum(rkm) as recorrido, sum(tifkm) as rec_km_fle, sum(tifkm*0.07) as comision FROM `logicop`.`salidasmat` join empleados on codempl=ope  group by 1,2 
            //union SELECT 'ENTRADAS' AS ACCESO,ope,  nomempl, count(nrec) as viajes,sum(rkm) as recorrido, sum(tifkm) as rec_km_fle, sum(tifkm*0.07) as comision FROM `logicop`.`entradasmat` join empleados on codempl=ope group by 1,2;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView3.CurrentRow as DataGridViewRow;
            if (row.Cells["ac"].Value.ToString() == "ENTRADAS")
            {
                Movimientos.frmmovimientosdetEXT frm = new Movimientos.frmmovimientosdetEXT(row.Cells["re"].Value.ToString());
                frm.MdiParent = this.MdiParent;
                frm.Show();
            }
            else
            {
                Movimientos.frmmovimientosdetINT frm = new Movimientos.frmmovimientosdetINT(row.Cells["re"].Value.ToString());
                frm.MdiParent = this.MdiParent;
                frm.Show();
            }
            
        }



        private void frmliquidacionope_Load(object sender, EventArgs e)
        {
            IntPtr hmenu = GetSystemMenu(this.Handle, 0);
            int cnt = GetMenuItemCount(hmenu);
            RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            DrawMenuBar(this.Handle);
              DateTime a, b;

              b = op_var.cohorte(int.Parse(diai) - 1, DateTime.Now.Month, DateTime.Now.Year);
              if (b > DateTime.Now)
              {
                  a = op_var.cohorte(int.Parse(diai), DateTime.Now.Month - 1, DateTime.Now.Year);
                  b = op_var.cohorte(int.Parse(diai) - 1, DateTime.Now.Month, DateTime.Now.Year);
              }
              else
              {
                 a = op_var.cohorte(int.Parse(diai), DateTime.Now.Month, DateTime.Now.Year);
                 b = op_var.cohorte(int.Parse(diai) - 1, DateTime.Now.Month + 1, DateTime.Now.Year);
               }

          
            this.Text = "Listado de Producción de Operadores - Cohorte Actual: " + a.ToShortDateString() + " hasta " + b.ToShortDateString();

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
            this.toolStripStatusLabel2.Text = dataGridView2.RowCount.ToString();
            this.toolStripStatusLabel4.Text = dataGridView3.RowCount.ToString();
            this.toolStripStatusLabel6.Text = this.listBox1.Items.Count.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.textBox1.Text))
            {
                listBox1.DataSource = op_combos.FillCombo("select codempl as cod, nomempl as nom from empleados where nomempl like '%" + this.textBox1.Text + "%'");
                listBox1.ValueMember = "cod";
                listBox1.DisplayMember = "nom";
                this.toolStripStatusLabel6.Text = this.listBox1.Items.Count.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = null;
            this.button2.Enabled = false;
            comboBox5.Text = null;
            comboBox6.Text = null;
            checkBox2.Checked = false;
        }

        private void vistaPorProyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reportes.Reportes04 frm = new Reportes.Reportes04(1);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void entradasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reportes.Reportes04 frm = new Reportes.Reportes04();
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }
        private void para(bool o)
        {
            if (radioButton1.Checked)
            {
                op_var.a =dateTimePicker3.Value;// pongo el 1 porque siempre es el primer día obvio
                op_var.b = dateTimePicker4.Value; //resto un día al mes y con esto obtengo el ultimo día
            }
            else
            {
                if (o == false)
                {
                    op_var.a = Convert.ToDateTime(string.Concat("01", "/", Convert.ToString(comboBox5.SelectedIndex + 1).PadLeft(2, '0'), "/", comboBox6.Text));// pongo el 1 porque siempre es el primer día obvio
                    op_var.b = Convert.ToDateTime(string.Concat("01", "/", Convert.ToString(comboBox5.SelectedIndex + 2).PadLeft(2, '0'), "/", comboBox6.Text)).AddDays(-1); //resto un día al mes y con esto obtengo el ultimo día
                }
                else
                {
                    op_var.b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
                    if (op_var.b > DateTime.Now)
                    {
                        op_var.a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(comboBox5.SelectedIndex).PadLeft(2, '0'), "/", comboBox6.Text));// pongo el 1 porque siempre es el primer día obvio
                        op_var.b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(comboBox5.SelectedIndex + 1).PadLeft(2, '0'), "/", comboBox6.Text));//resto un día al mes y con esto obtengo el ultimo día
                    }
                    else
                    {
                        op_var.a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
                        op_var.b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month + 1).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
                    }
                }
            }

            op_var.ope = op_sql.parametro1("select nomempl from empleados where codempl ='" + listBox1.SelectedValue.ToString() + "'");
            Reportes.Reportes03 frm = new Reportes.Reportes03(1);
            frm.MdiParent = this.MdiParent;
            frm.Show();

        }

        private void verCohorteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            op_var.b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día

            if (op_var.b > DateTime.Now)
            {
                op_var.a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month - 1).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
                op_var.b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
            }
            else
            {
                op_var.a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
                op_var.b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month + 1).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
            }

            op_var.c = listBox1.SelectedValue.ToString() ;
            //op_var.c = op_sql.parametro1("select nomempl from empleados where codempl ='" + listBox1.SelectedValue.ToString() + "'");
            Reportes.Reportes03 frm = new Reportes.Reportes03(2);
            frm.MdiParent = this.MdiParent;
            frm.Show();   
        }

        private bool validacion()
        {
            bool o = true;
            errorProvider1.Clear();


            if (string.IsNullOrEmpty(this.comboBox6.Text))
            {
                errorProvider1.SetError(comboBox6, "El año es Obligatorio");
                o = false;
            }

            if (string.IsNullOrEmpty(this.comboBox5.Text))
            {
                errorProvider1.SetError(comboBox5, "El Mes es Obligatorio");
                o = false;
            }
            return o;
        }
        private bool validacion1()
        {
            bool o = true;
            errorProvider1.Clear();
            if (dateTimePicker4.Value >= dateTimePicker3.Value)
            {

                errorProvider1.SetError(dateTimePicker4, "La fecha dede ser menor a la final");
                o = false;
            }
            if (dateTimePicker3.Value <= dateTimePicker4.Value)
            {

                errorProvider1.SetError(dateTimePicker3, "La fecha dede ser mayor a la Inicial");
                o = false;
            }

            return o;
        }
        bool avisar_parametrizada=false;

        private void button1_Click(object sender, EventArgs e)
        {
            if (!validacion()) return;

            status fr = new status();
            fr.Show("Cargando Datos");
            avisar_parametrizada = true;
            if (checkBox2.Checked == false)
            {
                op_var.a = op_var.cohorte(1, comboBox5.SelectedIndex + 1, int.Parse(comboBox6.Text));
                op_var.b = op_var.cohorte(1, comboBox5.SelectedIndex + 2, int.Parse(comboBox6.Text)).AddDays(-1);
            }
            else
            {
                op_var.a = op_var.cohorte(int.Parse(diai), comboBox5.SelectedIndex, int.Parse(comboBox6.Text));
                op_var.b = op_var.a.AddDays(int.Parse(numd));
            }

            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = op_comision.GellAllCargos(op_comision.vista.maestro, op_comision.opciones.parametro_fecha,  null);

            this.listBox1.DataSource = null;

            dataGridView3.DataSource = null;
            this.toolStripStatusLabel2.Text = dataGridView2.RowCount.ToString();
            this.toolStripStatusLabel4.Text = dataGridView3.RowCount.ToString();

            if (dataGridView2.Rows.Count > 0)
            {
                listBox1.DataSource = op_combos.FillCombo("select codempl as cod, nomempl as nom from empleados");
                listBox1.ValueMember = "cod";
                listBox1.DisplayMember = "nom";
                this.toolStripStatusLabel6.Text = this.listBox1.Items.Count.ToString();
                toolStripButton2.Enabled = false;
                this.button2.Enabled = true;
            }
            else
                MessageBox.Show("Los parámetros no contienen información", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void producciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
           //if (!validacion()) return;
           op_var.ope=op_sql.parametro1("select nomempl from empleados where codempl ='"+listBox1.SelectedValue.ToString()+"'");
           Reportes.Reportes04 frm = new Reportes.Reportes04(2);
           frm.MdiParent = this.MdiParent;
           frm.Show();
           
        }

        private void verProducciónPorProyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!validacion()) return;
            op_var.ope = op_sql.parametro1("select nomempl from empleados where codempl ='" + listBox1.SelectedValue.ToString() + "'");

            op_var.aa = comboBox5.SelectedIndex + 1;
            op_var.yy=int.Parse(comboBox6.Text);
            Reportes.Reportes04 frm = new Reportes.Reportes04(3);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void vistaPorResumenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //op_var.a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month - 1).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
            //op_var.b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
            op_var.b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día

            if (op_var.b > DateTime.Now)
            {
                op_var.a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month - 1).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
                op_var.b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
            }
            else
            {
                op_var.a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
                op_var.b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month + 1).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
            }
            Reportes.Reportes03 frm = new Reportes.Reportes03(1);
            frm.MdiParent = this.MdiParent;
            frm.Show();
            //if (!checkBox2.Checked)
            //para(checkBox2.Checked);
        }

        private void vistaEnDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            op_var.b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día

            if (op_var.b > DateTime.Now)
            {
                op_var.a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month - 1).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
                op_var.b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
            }
            else
            {
                op_var.a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
                op_var.b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month + 1).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
            }
          
            Reportes.Reportes02 frm = new Reportes.Reportes02(8);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void despachoCanterasToolStripMenuItem_Click(object sender, EventArgs e)
        {
                op_var.b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día

            if (op_var.b > DateTime.Now)
            {
                op_var.a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month - 1).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
                op_var.b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
            }
            else
            {
                op_var.a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
                op_var.b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month + 1).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
            }
            Reportes.Reportes01 frm = new Reportes.Reportes01(8);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
                para(checkBox2.Checked);
        }

        private void verProducciónDeOperadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            para(checkBox2.Checked);
        }

        
     

        private void exportarACVSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView2.RowCount > 0)
                op_exportacion.opcion(dataGridView2, "csv (*.csv)|*.csv", 1, this.Text);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void exportarAHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView2.RowCount > 0)
                op_exportacion.opcion(dataGridView2, "html (*.html)|*.html", 0, this.Text);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.dataGridView2.RowCount > 0)
                op_exportacion.opcion(dataGridView2, "xls Excel (*.xls)|*.xls", 0, this.Text);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

       

        private void producciónPorEquipoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            op_var.b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día

            if (op_var.b > DateTime.Now)
            {
                op_var.a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month - 1).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
                op_var.b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
            }
            else
            {
                op_var.a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
                op_var.b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month + 1).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
            }
            Reportes.Reportes05 frm = new Reportes.Reportes05(2);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (this.dataGridView2.RowCount > 0)
                op_exportacion.opcion(dataGridView2, "xls Excel (*.xls)|*.xls", 0, this.Text);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (this.dataGridView2.RowCount > 0)
                op_exportacion.opcion(dataGridView2, "csv (*.csv)|*.csv", 1, this.Text);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (this.dataGridView2.RowCount > 0)
                op_exportacion.opcion(dataGridView2, "html (*.html)|*.html", 0, this.Text);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (this.dataGridView3.RowCount > 0)
                op_exportacion.opcion(dataGridView3, "xls Excel (*.xls)|*.xls", 0, this.Text);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (this.dataGridView3.RowCount > 0)
                op_exportacion.opcion(dataGridView3, "csv (*.csv)|*.csv", 1, this.Text);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (this.dataGridView3.RowCount > 0)
                op_exportacion.opcion(dataGridView3, "html (*.html)|*.html", 0, this.Text);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!validacion1()) return;
            op_var.a = dateTimePicker4.Value;
            op_var.b = dateTimePicker3.Value;
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = op_comision.GellAllCargos(op_comision.vista.maestro, op_comision.opciones.parametro_fecha, null);

        }
    }
}
