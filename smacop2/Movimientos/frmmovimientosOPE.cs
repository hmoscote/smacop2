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

namespace smacop2.Movimientos
{
    public partial class frmmovimientosOP : Form
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
        DateTime dinicio, dfin;
      bool  i = false;

        public frmmovimientosOP()
        {
            InitializeComponent();
            dataGridView5.CellDoubleClick += new DataGridViewCellEventHandler(dataGridView5_CellDoubleClick);
            dataGridView4.CellDoubleClick += new DataGridViewCellEventHandler(dataGridView4_CellDoubleClick);
            dataGridView2.CellDoubleClick += new DataGridViewCellEventHandler(dataGridView2_CellDoubleClick);
            dataGridView3.CellDoubleClick += new DataGridViewCellEventHandler(dataGridView3_CellDoubleClick);
            dataGridView5.CellContentClick+=new DataGridViewCellEventHandler(dataGridView5_CellContentClick);
            this.listBox1.MouseDoubleClick+=new MouseEventHandler(listBox1_MouseDoubleClick);

        }
      
        private string diai = op_sql.parametro1(@"SELECT c.`diainicial` FROM logicop.cohorte_tablas c  where tabla='entrada';");
        private string numd = op_sql.parametro1(@"SELECT c.`diafinal` FROM logicop.cohorte_tablas c where tabla='entrada';");

        private void dataGridView5_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView5.CurrentRow as DataGridViewRow;
            Movimientos.frmmovimientosdetEXT frm = new Movimientos.frmmovimientosdetEXT(row.Cells["Tipo"].Value.ToString());
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }
        private void dataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView4.CurrentRow as DataGridViewRow;
            if (i == true)
                dataGridView5.DataSource = op_entradasmat.Gellresumenfechadet3(row.Cells["dataGridViewTextBoxColumn4"].Value.ToString(), dinicio, dinicio, 2);
            else
                dataGridView5.DataSource = op_entradasmat.GellIdsentradasmat(row.Cells["dataGridViewTextBoxColumn4"].Value.ToString(),dinicio, dfin, 3);
            this.toolStripStatusLabel1.Text = dataGridView5.RowCount.ToString();
        }
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView2.CurrentRow as DataGridViewRow;
            if (i == true)
                dataGridView5.DataSource = op_entradasmat.Gellresumenfechadet2(row.Cells["Column2"].Value.ToString(), dinicio, dinicio, 2);
            else
                dataGridView5.DataSource = op_entradasmat.GellIdsentradasmat(row.Cells["Column2"].Value.ToString(),dinicio, dfin, 2);
            this.toolStripStatusLabel1.Text = dataGridView5.RowCount.ToString();
        }
        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView3.CurrentRow as DataGridViewRow;
            if (i == true)
                dataGridView5.DataSource = op_entradasmat.Gellresumenfechadet1(row.Cells["dataGridViewTextBoxColumn1"].Value.ToString(), dinicio, dinicio, 2);
            else
                dataGridView5.DataSource = op_entradasmat.GellIdsentradasmat(row.Cells["dataGridViewTextBoxColumn1"].Value.ToString(),dinicio, dfin, 1);

            this.toolStripStatusLabel1.Text = dataGridView5.RowCount.ToString();
        }

      

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0)
                {
                    DataGridViewRow row = dataGridView5.CurrentRow as DataGridViewRow;
                    if (this.dataGridView5.Columns[e.ColumnIndex].Name == "dataGridViewButtonColumn1")
                    {
                        int a = Convert.ToInt32(MessageBox.Show("Está seguro que desea ANULAR el registro", Application.ProductName.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Information));
                        if (a == 6)
                        {
                            proveedores c = new proveedores();
                            c.cod = row.Cells[1].Value.ToString();
                            op_sql.parametro("update entradasmat set anul=1 where nrec=@nrec", "nrec", c.cod);
                            op_sql.parametro1("insert into logaccesos values (null, now(),concat('"+op_var.usu+"',' anuló el recibo entrada ','"+c.cod+"'));");
                            toolStripButton3_Click(toolStripButton3, e);
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Movimientos.frmmovimientosdetEXT frm = new Movimientos.frmmovimientosdetEXT();
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            status fr = new status();
            fr.Show("Cargando Datos");

            dataGridView5.AutoGenerateColumns = false;
            dataGridView5.DataSource = op_entradasmat.GellAllentradasmat();
           

            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = op_entradasmat.Gellresumen2();

            dataGridView3.AutoGenerateColumns = false;
            dataGridView3.DataSource = op_entradasmat.Gellresumen1();

            dataGridView4.AutoGenerateColumns = false;
            dataGridView4.DataSource = op_entradasmat.Gellresumen3();
            toolStripLabel2.Text = "Vista General";
            this.toolStripStatusLabel1.Text = dataGridView5.RowCount.ToString();

            this.Text = "Listado Entradas de Materiales - Cohorte Actual: " + a.ToShortDateString() + " hasta " + b.ToShortDateString();

            if (this.dataGridView5.RowCount > 0)
                this.toolStripButton2.Enabled = true;

            listBox1.Items.Clear();
            button2.Enabled = false;
            this.comboBox1.Text = null;
            this.comboBox2.Text = null;
            this.checkBox1.Checked = false;
            dinicio = a;
            dfin = b;
            i = false;

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        DateTime a, b;
        private void frmmovimientos_Load(object sender, EventArgs e)
        {
            IntPtr hmenu = GetSystemMenu(this.Handle, 0);
            int cnt = GetMenuItemCount(hmenu);
            // remove 'close' action
            RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            // remove extra menu line
            RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            DrawMenuBar(this.Handle);
            splitContainer1.Panel1Collapsed = true;
            splitContainer4.Panel1Collapsed = true;

          
                b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year)).AddDays(1);//resto un día al mes y con esto obtengo el ultimo día

                if (b > DateTime.Now)
                {
                    a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month - 1).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
                    b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
                }
                else
                {
                    a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(DateTime.Now.Month).PadLeft(2, '0'), "/", DateTime.Now.Year));// pongo el 1 porque siempre es el primer día obvio
                    b = Convert.ToDateTime(string.Concat(Convert.ToString(int.Parse(diai) - 1), "/", Convert.ToString(DateTime.Now.Month + 1).PadLeft(2, '0'), "/", DateTime.Now.Year));//resto un día al mes y con esto obtengo el ultimo día
                } 


            this.Text = "Listado Entradas de Materiales - Cohorte Actual: " + a.ToShortDateString() + " hasta " + b.ToShortDateString();
            
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select year(fech) as fech  from entradasmat group by year(fech);", conn);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                        this.comboBox1.Items.Add(Convert.ToString(reader[0]));
                }
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (toolStripButton5.Checked == false)
            {
                splitContainer1.Panel1Collapsed = false;
                toolStripButton5.Checked = true;
                toolStripButton5.CheckState = CheckState.Checked;
            }
            else
            {
                splitContainer1.Panel1Collapsed = true;
                toolStripButton5.Checked = false;
                toolStripButton5.CheckState = CheckState.Unchecked;
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (toolStripButton6.Checked == false)
            {
                splitContainer4.Panel1Collapsed = false;
                toolStripButton6.Checked = true;
                toolStripButton6.CheckState = CheckState.Checked;
            }
            else
            {
                splitContainer4.Panel1Collapsed = true;
                toolStripButton6.Checked = false;
                toolStripButton6.CheckState = CheckState.Unchecked;
            }
        }
        public DateTime fecha ;
        public bool opcion;

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {

                if (!(string.IsNullOrEmpty(this.comboBox1.Text) || string.IsNullOrEmpty(this.comboBox1.Text)))
                {
                    DateTime inicio, fin;

                    inicio = Convert.ToDateTime(Convert.ToString(listBox1.SelectedItem).Substring(0, 10));

                    fin = Convert.ToDateTime(string.Concat("01", "/", Convert.ToString(comboBox2.SelectedIndex + 2).PadLeft(2, '0'), "/", comboBox1.Text)).AddDays(-1); //resto un día al mes y con esto obtengo el ultimo día

                    dataGridView5.AutoGenerateColumns = false;
                    dataGridView5.DataSource = op_entradasmat.GellAllentradasmat3(inicio, fin, 2);
                    if (dataGridView5.RowCount > 0)
                    {

                        dataGridView2.AutoGenerateColumns = false;
                        dataGridView2.DataSource = op_entradasmat.Gellresumenfecha2(inicio, fin, 2);

                        dataGridView3.AutoGenerateColumns = false;
                        dataGridView3.DataSource = op_entradasmat.Gellresumenfecha1(inicio, fin, 2);

                        dataGridView4.AutoGenerateColumns = false;
                        dataGridView4.DataSource = op_entradasmat.Gellresumenfecha3(inicio, fin, 2);
                        toolStripLabel2.Text = "Vista Parametrizada";
                        ///////////////////////////////////////////
                        //fecha = DateTime.Parse(this.listBox1.SelectedItem.ToString());
                    
                        dinicio = inicio;
                        dfin = fin;
                        this.Text = "Listado Salidas de Materiales - Fecha: " + dinicio.ToShortDateString();
                    
                        i = true;
                    }
                    this.toolStripStatusLabel1.Text = dataGridView5.RowCount.ToString();
                }
                else
                {
                    MessageBox.Show("Escoja los parámetros", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (!(string.IsNullOrEmpty(this.comboBox1.Text) || string.IsNullOrEmpty(this.comboBox2.Text)))
                {
                    DateTime inicio, fin;
                    if (checkBox1.Checked == false)
                    {
                        inicio = Convert.ToDateTime(string.Concat("01", "/", Convert.ToString(comboBox2.SelectedIndex + 1).PadLeft(2, '0'), "/", comboBox1.Text));// pongo el 1 porque siempre es el primer día obvio
                        fin = Convert.ToDateTime(string.Concat("01", "/", Convert.ToString(comboBox2.SelectedIndex + 2).PadLeft(2, '0'), "/", comboBox1.Text)).AddDays(-1); //resto un día al mes y con esto obtengo el ultimo día
                    }
                    else
                    {
                    
                        inicio = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(op_var.mes(comboBox2.SelectedIndex) - 1).PadLeft(2, '0'), "/", comboBox1.Text));// pongo el 1 porque siempre es el primer día obvio
                        fin = inicio.AddDays(int.Parse(numd)); //resto un día al mes y con esto obtengo el ultimo dí            fin = inicio.AddDays(int.Parse(numd) - 1); //resto un día al mes y con esto obtengo el ultimo dí         
                    }

                    dataGridView5.AutoGenerateColumns = false;
                    dataGridView5.DataSource = op_entradasmat.GellAllentradasmat3(inicio, fin, 1);

                    dataGridView2.AutoGenerateColumns = false;
                    dataGridView2.DataSource = op_entradasmat.Gellresumenfecha2(inicio, fin, 1);

                    dataGridView3.AutoGenerateColumns = false;
                    dataGridView3.DataSource = op_entradasmat.Gellresumenfecha1(inicio, fin, 1);

                    dataGridView4.AutoGenerateColumns = false;
                    dataGridView4.DataSource = op_entradasmat.Gellresumenfecha3(inicio, fin, 1);

                    toolStripLabel2.Text = "Vista Parametrizada";
                    dinicio = inicio;
                    dfin = fin;
                
                    this.Text = "Listado Entradas de Materiales - Cohorte : " + inicio.ToShortDateString() + " hasta " + fin.ToShortDateString();

                    listBox1.Items.Clear();
                    using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("select fech from entradasmat where fech between @i and @f and anul=0 group by fech;", conn);
                        cmd.Parameters.AddWithValue("@i", inicio);
                        cmd.Parameters.AddWithValue("@f", fin);
                        MySqlDataReader reader = cmd.ExecuteReader();


                        if (reader.HasRows == true)
                        {
                            while (reader.Read())
                                listBox1.Items.Add(Convert.ToDateTime(reader[0]));
                        }
                        else
                            MessageBox.Show("No existe registro correspodiente al parámetro", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    this.button2.Enabled = true;

                    if (dataGridView5.RowCount <= 0)
                    {
                        listBox1.Items.Clear();
                        this.button2.Enabled = false;
                        dataGridView2.DataSource = null;
                        dataGridView3.DataSource = null;
                        dataGridView4.DataSource = null;

                    }
                    else
                        this.button2.Enabled = true;
                  
                    this.toolStripButton2.Enabled = false;
                    this.toolStripStatusLabel1.Text = dataGridView5.RowCount.ToString();
                }
                else
                {
                    MessageBox.Show("Escoja los parámetros", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         
        }

        private void filtrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (!(string.IsNullOrEmpty(this.comboBox1.Text) || string.IsNullOrEmpty(this.comboBox1.Text)))
                {
                    DateTime inicio, fin;

                    inicio = Convert.ToDateTime(Convert.ToString(listBox1.SelectedItem).Substring(0, 10));
                    fin = Convert.ToDateTime(string.Concat("01", "/", Convert.ToString(comboBox2.SelectedIndex + 2).PadLeft(2, '0'), "/", comboBox1.Text)).AddDays(-1); 

                    dataGridView5.AutoGenerateColumns = false;
                    dataGridView5.DataSource = op_entradasmat.GellAllentradasmat3(inicio, fin, 2);

                    dataGridView2.AutoGenerateColumns = false;
                    dataGridView2.DataSource = op_entradasmat.Gellresumenfecha2(inicio, fin, 2);

                    dataGridView3.AutoGenerateColumns = false;
                    dataGridView3.DataSource = op_entradasmat.Gellresumenfecha1(inicio, fin, 2);

                    dataGridView4.AutoGenerateColumns = false;
                    dataGridView4.DataSource = op_entradasmat.Gellresumenfecha3(inicio, fin, 2);
                    toolStripLabel2.Text = "Vista Parametrizada";

                    dinicio = inicio;
                    dfin = fin;
                    i = true;
                    this.Text = "Listado Salidas de Materiales - Fecha: " + dinicio.ToShortDateString();
                    this.toolStripStatusLabel1.Text = dataGridView5.RowCount.ToString();
                }
                else
                {
                    MessageBox.Show("Escoja los parámetros", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            button2.Enabled = false;
            this.comboBox1.Text = null;
            this.comboBox2.Text = null;
            this.checkBox1.Checked = false;
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
              
                if (checkBox1.Checked == false)
                {
                    op_var.a = Convert.ToDateTime(string.Concat("01", "/", Convert.ToString(comboBox2.SelectedIndex + 1).PadLeft(2, '0'), "/", comboBox1.Text));// pongo el 1 porque siempre es el primer día obvio
                    op_var.b = Convert.ToDateTime(string.Concat("01", "/", Convert.ToString(comboBox2.SelectedIndex + 2).PadLeft(2, '0'), "/", comboBox1.Text)).AddDays(-1); //resto un día al mes y con esto obtengo el ultimo día
                }

                else
                {
                    op_var.a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(op_var.mes(comboBox2.SelectedIndex) - 1).PadLeft(2, '0'), "/", comboBox1.Text));// pongo el 1 porque siempre es el primer día obvio
                    op_var.b = op_var.a.AddDays(int.Parse(numd)); //resto un día al mes y con esto obtengo el ultimo día
                }
                Reportes.Reportes01 frm = new Reportes.Reportes01(1);
                frm.MdiParent = this.MdiParent;
                frm.Show();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            op_var.a = Convert.ToDateTime(listBox1.SelectedItem.ToString());
            Reportes.Reportes01 frm = new Reportes.Reportes01(2);
            frm.MdiParent = this.MdiParent;
            frm.Show();

        }

        private void cohorteToolStripMenuItem_Click(object sender, EventArgs e)
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
            Reportes.Reportes01 frm = new Reportes.Reportes01(1);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

    

        private void porFechaToolStripMenuItem_Click(object sender, EventArgs e)
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
            Reportes.Reportes01 frm = new Reportes.Reportes01(3);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void porEquipoToolStripMenuItem_Click(object sender, EventArgs e)
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
            Reportes.Reportes01 frm = new Reportes.Reportes01(4);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void porViajesToolStripMenuItem_Click(object sender, EventArgs e)
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
            Reportes.Reportes01 frm = new Reportes.Reportes01(5);
            frm.Show();
        }

        private void porTransacciónToolStripMenuItem_Click(object sender, EventArgs e)
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
            Reportes.Reportes01 frm = new Reportes.Reportes01(6);
            frm.MdiParent = this.MdiParent;
            frm.Show();
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

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            Consultas.frmverificacion fr = new Consultas.frmverificacion(DateTime.Now, DateTime.Now, null,null, 6);
            fr.MdiParent = this.MdiParent;
            fr.Show();
        }

       
    }
}
