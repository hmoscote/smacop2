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
    public partial class frmfacturacion : Form
    {
        public frmfacturacion()
        {
            InitializeComponent();
            this.textBox2.MouseDoubleClick+=new MouseEventHandler(textBox2_MouseDoubleClick);
            this.textBox2.KeyDown+=new KeyEventHandler(textBox2_KeyDown);
            dataGridView5.CellContentClick+=new DataGridViewCellEventHandler(dataGridView5_CellContentClick);
            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(dataGridView1_CellContentClick);
            this.dataGridView5.CellDoubleClick+=new DataGridViewCellEventHandler(dataGridView5_CellDoubleClick);
            this.dataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(dataGridView1_CellDoubleClick);
            this.radioButton1.Click+=new EventHandler(radioButton1_Click);
            this.radioButton2.Click += new EventHandler(radioButton2_Click);
            this.textBox2.GotFocus+=new EventHandler(textBox2_GotFocus);
        }
        private void textBox2_GotFocus(object sender, EventArgs e)
        {
            if (op_var.boton4 == true)
            {
                this.textBox2.Text = op_var.cod;
                this.textBox1.Text = op_var.nom;
                op_var.cod = null;
                op_var.nom = null;
                op_var.boton4 = false;
            }
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
        public void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                Administracion.frmlistatmp fr = new Administracion.frmlistatmp(2);
                op_var.boton4 = true;
                fr.ShowDialog();
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0)
                {
                    DataGridViewRow row = dataGridView5.CurrentRow as DataGridViewRow;
                    if (this.dataGridView5.Columns[e.ColumnIndex].Name == "eli")
                    {
                        int a = Convert.ToInt32(MessageBox.Show("Está seguro que desea ANULAR el registro", Application.ProductName.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Information));
                        if (a == 6)
                        {
                            proveedores c = new proveedores();
                            c.cod = row.Cells[1].Value.ToString();
                            op_sql.parametro("update salidasmat set anul=1 where nrec=@nrec", "nrec", c.cod);
                            dataGridView5.AutoGenerateColumns = false;
                            dataGridView5.DataSource = op_salidasmat.GellAllsalidasmat();
                            this.toolStripStatusLabel4.Text = dataGridView5.RowCount.ToString();
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

        private void textBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            dataGridView1.DataSource = null;
            this.toolStripButton2.Enabled = false;
            this.checkBox2.Checked = false;
            this.comboBox5.Text = null;
            this.comboBox6.Text = null;

            label6.Text = "TOTAL: " + string.Format("{0:C}", 0);
            label7.Text = "PROMEDIO DIARIO: " + string.Format("{0:C}", 0);

            this.textBox1.Text = null;
            this.textBox2.Text = null;
 
            if (this.comboBox4.Text == "CANTERA ENTRADAS" || this.comboBox4.Text == "CANTERA SALIDAS")
            {
                Administracion.frmlistatmp fr = new Administracion.frmlistatmp(9);
                op_var.boton4 = true;
                fr.ShowDialog();
            }
            else if (this.comboBox4.Text == "CENTRO DE COSTOS")
            {
                Administracion.frmlistatmp fr = new Administracion.frmlistatmp(10);
                op_var.boton4 = true;
                fr.ShowDialog();
            }
            else
            {
                Administracion.frmlistatmp fr = new Administracion.frmlistatmp(8);
                op_var.boton4 = true;
                fr.ShowDialog();
            }
          
        }

        private void dataGridView5_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView5.CurrentRow as DataGridViewRow;
            Movimientos.frmmovimientosdetEXT frm = new Movimientos.frmmovimientosdetEXT(row.Cells["Tipo"].Value.ToString());
            frm.MdiParent = this.MdiParent;
            frm.ShowDialog();
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow as DataGridViewRow;
            Movimientos.frmmovimientosdetINT frm = new Movimientos.frmmovimientosdetINT(row.Cells["t"].Value.ToString());
            frm.MdiParent = this.MdiParent;
            frm.ShowDialog();
        }
        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0)
                {
                    DataGridViewRow row = dataGridView5.CurrentRow as DataGridViewRow;
                    if (this.dataGridView5.Columns[e.ColumnIndex].Name == "elii")
                    {
                        int a = Convert.ToInt32(MessageBox.Show("Está seguro que desea ANULAR el registro", Application.ProductName.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Information));
                        if (a == 6)
                        {
                            proveedores c = new proveedores();
                            c.cod = row.Cells[1].Value.ToString();
                            op_sql.parametro("update entradasmat set anul=1 where nrec=@nrec", "nrec", c.cod);
                            dataGridView5.AutoGenerateColumns = false;
                            dataGridView5.DataSource = op_entradasmat.GellAllentradasmat();
                            this.toolStripStatusLabel4.Text = dataGridView5.RowCount.ToString();
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

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox4.Text == "CANTERA ENTRADAS")
            {
             
                dataGridView1.SendToBack();
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("select year(fech) as fech  from entradasmat group by year(fech);", conn);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    this.comboBox6.Items.Clear();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                            this.comboBox6.Items.Add(Convert.ToString(reader[0]));
                    }
                }
            }
            else if (this.comboBox4.Text == "CENTRO DE COSTOS" || this.comboBox4.Text == "CANTERA SALIDAS")
            {
                dataGridView1.BringToFront();
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
            else
            {
                comboBox4.Visible = true;
            }
            dataGridView1.DataSource = null;
            this.toolStripButton2.Enabled = false;
            this.checkBox2.Checked = false;
            this.comboBox5.Text = null;
            this.comboBox6.Text = null;

                label6.Text = "TOTAL: " + string.Format("{0:C}", 0);
                label7.Text = "PROMEDIO DIARIO: " + string.Format("{0:C}", 0);
           
            this.textBox1.Text = null;
            this.textBox2.Text = null;
            this.textBox2.Focus();
        }

        private void frmfacturacion_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'logicopDataSet.lstentradas_x_canteras' Puede moverla o quitarla según sea necesario.
            op_var.boton4 = false;
            this.comboBox4.Text = "CANTERA ENTRADAS";
        }

        private bool validacion()
        {
            bool o = true;
            errorProvider1.Clear();

            if (string.IsNullOrEmpty(this.textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "La Código es Obligatorio");
                o = false;
            }
         
            if (string.IsNullOrEmpty(comboBox4.Text))
            {
                errorProvider1.SetError(comboBox4, "El Objecto contractual es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(comboBox5.Text))
            {
                errorProvider1.SetError(comboBox5, "El Mes es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(comboBox6.Text))
            {
                errorProvider1.SetError(comboBox6, "El año es Obligatorio");
                o = false;
            }
            return o;
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                status fr = new status();
                fr.Show("Cargando Datos");

                if (this.radioButton1.Checked)
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

                    this.errorProvider1.SetError(dateTimePicker1, null);
                    this.errorProvider1.SetError(dateTimePicker2, null);
                    this.errorProvider1.SetError(comboBox5, null);
                    this.errorProvider1.SetError(comboBox6, null);
                    panel2.Enabled = true;
                    panel3.Enabled = false;
                }
                else
                {
                    if (string.IsNullOrEmpty(this.textBox2.Text))
                    {
                        errorProvider1.SetError(textBox2, "La Código es Obligatorio");
                        return;
                    }

                    if (string.IsNullOrEmpty(comboBox4.Text))
                    {
                        errorProvider1.SetError(comboBox4, "El Objecto contractual es Obligatorio");
                        return;
                    }


                    if (dateTimePicker1.Value >= dateTimePicker2.Value)
                    {
                        this.errorProvider1.SetError(dateTimePicker1, "La fecha dede ser menor a la final");
                        return;
                    }
                    if (dateTimePicker2.Value <= dateTimePicker1.Value)
                    {
                        this.errorProvider1.SetError(dateTimePicker2, "La fecha dede ser mayor a la Inicial");
                        return;
                    }

                    this.errorProvider1.SetError(dateTimePicker1, null);
                    this.errorProvider1.SetError(dateTimePicker2, null);
                    this.errorProvider1.SetError(comboBox5, null);
                    this.errorProvider1.SetError(comboBox6, null);

                    op_var.a = dateTimePicker1.Value;
                    op_var.b = dateTimePicker2.Value;
                    panel2.Enabled = false;
                    panel3.Enabled = true;
                }


                if (this.comboBox4.Text == "CANTERA ENTRADAS")
                {

                    dataGridView5.AutoGenerateColumns = false;
                    dataGridView5.DataSource = op_entradasmat.GellAllentradasmat4(op_var.a, op_var.b, textBox1.Text, 1);
                    dataGridView5.BringToFront();
                    try
                    {

                        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                        {
                            conn.Open();
                            MySqlCommand cmd = new MySqlCommand(@"SELECT
                                                                    SUM(entradasmat.matfle) AS TOTAL,
                                                                    AVG(entradasmat.matfle) AS prom
                                                                    FROM
                                                                    entradasmat
                                                                    WHERE
                                                                    entradasmat.prov=@a and fech BETWEEN @b and @c", conn);
                            cmd.Parameters.AddWithValue("@a", this.textBox2.Text);
                            cmd.Parameters.AddWithValue("@b", op_var.a);
                            cmd.Parameters.AddWithValue("@c", op_var.b);
                            MySqlDataReader reader = cmd.ExecuteReader();
                            if (reader.HasRows == true)
                            {
                                while (reader.Read())
                                {
                                    label6.Text = "TOTAL: " + string.Format("{0:C}", reader[0]);
                                    label7.Text = "PROMEDIO DIARIO: " + string.Format("{0:C}", reader[1]);
                                }
                            }
                            else
                            {
                                label6.Text = "TOTAL: " + string.Format("{0:C}", 0);
                                label7.Text = "PROMEDIO DIARIO: " + string.Format("{0:C}", 0);
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    this.toolStripButton2.Enabled = true;

                    if (dataGridView5.RowCount <= 0)
                        this.toolStripButton2.Enabled = false;
                    else
                        toolStripButton4.Visible = true;

                    this.toolStripStatusLabel4.Text = dataGridView5.RowCount.ToString();
                
           
                    dataGridView1.DataSource = null;
                  
                }
                else if (this.comboBox4.Text == "CENTRO DE COSTOS")
                {
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.DataSource = op_salidasmat.GellAllsalidasmat4(op_var.a, op_var.b, textBox1.Text, 1);
                    try
                    {

                        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                        {
                            conn.Open();
                            MySqlCommand cmd = new MySqlCommand(@"SELECT
                                                                    SUM(salidasmat.matflet) AS TOTAL,
                                                                    AVG(salidasmat.matflet) AS prom
                                                                    FROM
                                                                    salidasmat
                                                                    WHERE
                                                                    salidasmat.ccos=@a and fech BETWEEN @b and @c", conn);
                            cmd.Parameters.AddWithValue("@a", this.textBox2.Text);
                            cmd.Parameters.AddWithValue("@b", op_var.a);
                            cmd.Parameters.AddWithValue("@c", op_var.b);
                            MySqlDataReader reader = cmd.ExecuteReader();
                            if (reader.HasRows == true)
                            {
                                while (reader.Read())
                                {
                                    label6.Text = "TOTAL: " + string.Format("{0:C}", reader[0]);
                                    label7.Text = "PROMEDIO DIARIO: " + string.Format("{0:C}", reader[1]);
                                }
                               
                            }
                            else
                            {
                                label6.Text = "TOTAL: " + string.Format("{0:C}", 0);
                                label7.Text = "PROMEDIO DIARIO: " + string.Format("{0:C}", 0);
                            }
                        }
                    }


                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    this.toolStripButton2.Enabled = true;
                    if (dataGridView1.RowCount <= 0)
                        this.toolStripButton2.Enabled = false;
                    else
                        toolStripButton4.Visible = true;

                    this.toolStripStatusLabel4.Text = dataGridView1.RowCount.ToString();
                    dataGridView5.DataSource = null;
                  
                }
                else if (this.comboBox4.Text == "CANTERA SALIDAS")
                {
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.DataSource = op_salidasmat.GellAllsalidasmat4(op_var.a, op_var.b, textBox1.Text, 3);
                    try
                    {

                        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                        {
                            conn.Open();
                            MySqlCommand cmd = new MySqlCommand(@"SELECT
                                                                    SUM(salidasmat.matflet) AS TOTAL,
                                                                    AVG(salidasmat.matflet) AS prom
                                                                    FROM
                                                                    salidasmat
                                                                    WHERE
                                                                    salidasmat.prov=@a and fech BETWEEN @b and @c", conn);
                            cmd.Parameters.AddWithValue("@a", this.textBox2.Text);
                            cmd.Parameters.AddWithValue("@b", op_var.a);
                            cmd.Parameters.AddWithValue("@c", op_var.b);
                            MySqlDataReader reader = cmd.ExecuteReader();
                            if (reader.HasRows == true)
                            {
                                while (reader.Read())
                                {
                                    label6.Text = "TOTAL: " + string.Format("{0:C}", reader[0]);
                                    label7.Text = "PROMEDIO DIARIO: " + string.Format("{0:C}", reader[1]);
                                }

                            }
                            else
                            {
                                label6.Text = "TOTAL: " + string.Format("{0:C}", 0);
                                label7.Text = "PROMEDIO DIARIO: " + string.Format("{0:C}", 0);
                            }
                        }
                    }


                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    this.toolStripButton2.Enabled = true;
                    if (dataGridView1.RowCount <= 0)
                        this.toolStripButton2.Enabled = false;
                    else
                        toolStripButton4.Visible = true;
                    dataGridView1.BringToFront();
                    this.toolStripStatusLabel4.Text = dataGridView1.RowCount.ToString();
                    dataGridView5.DataSource = null;

                }
                else 
                {
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.DataSource = op_salidasmat.GellAllsalidasmat4(op_var.a, op_var.b, textBox1.Text,2);
                    dataGridView1.BringToFront();
                    try
                    {

                        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                        {
                            conn.Open();
                            MySqlCommand cmd = new MySqlCommand(@"SELECT
                                                                    SUM(salidasmat.matflet) AS TOTAL,
                                                                    AVG(salidasmat.matflet) AS prom
                                                                    FROM
                                                                    salidasmat
                                                                    WHERE
                                                                    salidasmat.cont=@a and fech BETWEEN @b and @c", conn);
                            cmd.Parameters.AddWithValue("@a", this.textBox2.Text);
                            cmd.Parameters.AddWithValue("@b", op_var.a);
                            cmd.Parameters.AddWithValue("@c", op_var.b);
                            MySqlDataReader reader = cmd.ExecuteReader();
                            if (reader.HasRows == true)
                            {
                                while (reader.Read())
                                {
                                    label6.Text = "TOTAL: " + string.Format("{0:C}", reader[0]);
                                    label7.Text = "PROMEDIO DIARIO: " + string.Format("{0:C}", reader[1]);
                                }
                            }
                            else
                            {
                                label6.Text = "TOTAL: " + string.Format("{0:C}", 0);
                                label7.Text = "PROMEDIO DIARIO: " + string.Format("{0:C}", 0);
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    this.toolStripButton2.Enabled = true;
                    if (dataGridView1.RowCount <= 0)
                        this.toolStripButton2.Enabled = false;
                    else
                        toolStripButton4.Visible = true;

                    this.toolStripStatusLabel4.Text = dataGridView1.RowCount.ToString();
                    dataGridView5.DataSource = null;

                }
             
            }

            catch (MySqlException ex)
            {
                MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




            if (string.IsNullOrEmpty(comboBox4.Text) || string.IsNullOrEmpty(this.textBox2.Text) || string.IsNullOrEmpty(comboBox6.Text) || string.IsNullOrEmpty(comboBox5.Text))
            {

            }
            else
            {

            }
        }

        private consulta_01 loaddatos(IDataReader readers)
        {
            consulta_01 item = new consulta_01();
            item.nombre = Convert.ToString(readers["mat"]);
            item.cantidad=Convert.ToDecimal(readers["volm"]);
            return item;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
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
            }
            else
            {
                op_var.a = dateTimePicker1.Value;
                op_var.b = dateTimePicker2.Value;
            }

            Operaciones.op_var.c = textBox1.Text;

            if (comboBox4.Text == "CANTERA ENTRADAS")
            {
                Reportes.Reportes01 frm = new Reportes.Reportes01(9);
                frm.ShowDialog();
            }
            else if (comboBox4.Text == "CANTERA SALIDAS")
            {
                Reportes.Reportes02 frm = new Reportes.Reportes02(10);
                frm.ShowDialog();
            }
            else if (comboBox4.Text == "CENTRO DE COSTOS")
            {
                Reportes.Reportes02 frm = new Reportes.Reportes02(9);
                frm.ShowDialog();
            }
            else
            {
                Reportes.Reportes02 frm = new Reportes.Reportes02(3);
                frm.ShowDialog();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void exportarACVSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView5.RowCount > 0)
                op_exportacion.opcion(dataGridView5, "csv (*.csv)|*.csv", 1, this.Text);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void exportarAHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView5.RowCount > 0)
                op_exportacion.opcion(dataGridView5, "html (*.html)|*.html", 0, this.Text);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

      

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.RowCount > 0)
                op_exportacion.opcion(dataGridView1, "html (*.html)|*.html", 0, this.Text);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (this.comboBox4.Text == "CANTERA ENTRADAS")
            {
                if (this.dataGridView5.RowCount > 0)
                    op_exportacion.opcion(dataGridView5, "xls Excel (*.xls)|*.xls", 0, this.Text);
                else
                    MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (this.dataGridView1.RowCount > 0)
                    op_exportacion.opcion(dataGridView1, "xls Excel (*.xls)|*.xls", 0, this.Text);
                else
                    MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (this.comboBox4.Text == "CANTERA ENTRADAS")
            {
                if (this.dataGridView5.RowCount > 0)
                    op_exportacion.opcion(dataGridView5, "csv (*.csv)|*.csv", 1, this.Text);
                else
                    MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {

                if (this.dataGridView1.RowCount > 0)
                    op_exportacion.opcion(dataGridView1, "csv (*.csv)|*.csv", 1, this.Text);
                else
                    MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (this.comboBox4.Text == "CANTERA ENTRADAS")
            {
                if (this.dataGridView5.RowCount > 0)
                    op_exportacion.opcion(dataGridView5, "html (*.html)|*.html", 0, this.Text);
                else
                    MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {

                if (this.dataGridView1.RowCount > 0)
                    op_exportacion.opcion(dataGridView1, "html (*.html)|*.html", 0, this.Text);
                else
                    MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
             if (!validacion()) return;
            DateTime inicio, fin;
          
                if (checkBox2.Checked == false)
                {
                    inicio = Convert.ToDateTime(string.Concat("01", "/", Convert.ToString(comboBox5.SelectedIndex + 1).PadLeft(2, '0'), "/", comboBox6.Text));// pongo el 1 porque siempre es el primer día obvio
                    fin = Convert.ToDateTime(string.Concat("01", "/", Convert.ToString(comboBox5.SelectedIndex + 2).PadLeft(2, '0'), "/", comboBox6.Text)).AddDays(-1); //resto un día al mes y con esto obtengo el ultimo día
                }
                else
                {
                    string diai = op_sql.parametro1(@"SELECT c.`diainicial` FROM logicop.cohorte_tablas c  where tabla='entrada';");
                    string numd = op_sql.parametro1(@"SELECT c.`diafinal` FROM logicop.cohorte_tablas c where tabla='entrada';");

                    inicio = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(op_var.mes(comboBox5.SelectedIndex) - 1).PadLeft(2, '0'), "/", comboBox6.Text));// pongo el 1 porque siempre es el primer día obvio
                    fin = inicio.AddDays(int.Parse(numd)); //resto un día al mes y con esto obtengo el ultimo dí         
                }
                frmresumenmov frm = new frmresumenmov(inicio, fin);
                frm.Show();
           
        }

        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
            if (this.comboBox4.Text == "CANTERA ENTRADAS")
            {
                frmgraficos fr = new frmgraficos(this.textBox1.Text, 1);
                fr.ShowDialog();
            }
            else if (this.comboBox4.Text == "CANTERA SALIDAS")
            {
                frmgraficos fr = new frmgraficos(this.textBox1.Text, 3);
                fr.ShowDialog();
            }
            else if (this.comboBox4.Text == "CENTRO DE COSTOS")
            {
                frmgraficos fr = new frmgraficos(this.textBox1.Text, 2);
                fr.ShowDialog();
            }
            else
            {
                frmgraficos fr = new frmgraficos(this.textBox1.Text, 4);
                fr.ShowDialog();
            }
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void activated(object sender, EventArgs e)
        {
            
        }
    }
}
