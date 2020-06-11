
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Collections;


namespace smacop2.Movimientos
{
    public partial class frmmovimientosINT : Form
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
        public frmmovimientosINT()
        {
            InitializeComponent();
            dataGridView5.CellDoubleClick += new DataGridViewCellEventHandler(dataGridView5_CellDoubleClick);
            dataGridView4.CellDoubleClick += new DataGridViewCellEventHandler(dataGridView4_CellDoubleClick);
            dataGridView2.CellDoubleClick += new DataGridViewCellEventHandler(dataGridView2_CellDoubleClick);
            dataGridView3.CellDoubleClick += new DataGridViewCellEventHandler(dataGridView3_CellDoubleClick);
            this.listBox1.MouseDoubleClick += new MouseEventHandler(listBox1_MouseDoubleClick);
            dataGridView5.CellContentClick += new DataGridViewCellEventHandler(dataGridView5_CellContentClick);
            dataGridView5.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView5_CellPainting);
        }

        private void dataGridView5_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dataGridView5.Columns[e.ColumnIndex].Name == "eli" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = this.dataGridView5.Rows[e.RowIndex].Cells["eli"] as DataGridViewButtonCell;
                Icon icoAtomico = new Icon(Environment.CurrentDirectory + @"\anular.ico");

                e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 3, e.CellBounds.Top + 3);

                this.dataGridView5.Rows[e.RowIndex].Height = icoAtomico.Height + 8;
                this.dataGridView5.Columns[e.ColumnIndex].Width = icoAtomico.Width + 8;

                e.Handled = true;
            }
        }
        DateTime INICIO, FIN;
        bool OPC;
        public frmmovimientosINT(DateTime I, DateTime F, bool O)
            : this()
        {
            INICIO = I;
            FIN = F;
            OPC = O;

        }
        private string diai = op_sql.parametro1(@"SELECT c.`diainicial` FROM logicop.cohorte_tablas c  where tabla='entrada';");
        private string numd = op_sql.parametro1(@"SELECT c.`diafinal` FROM logicop.cohorte_tablas c where tabla='entrada';");

        private void dataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView4.CurrentRow as DataGridViewRow;
            if (i == 1)
                dataGridView5.DataSource = op_salidasmat.Gellresumenfechadet3(row.Cells["dataGridViewTextBoxColumn4"].Value.ToString(), dinicio, dinicio, 2);
            else
                dataGridView5.DataSource = op_salidasmat.GellIdssalidasmat(row.Cells["dataGridViewTextBoxColumn4"].Value.ToString(), dinicio, dfin, 3);

            this.toolStripStatusLabel4.Text = dataGridView5.RowCount.ToString();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView2.CurrentRow as DataGridViewRow;
            if (i == 1)
                dataGridView5.DataSource = op_salidasmat.Gellresumenfechadet2(row.Cells["Column2"].Value.ToString(), dinicio, dinicio, 2);
            else
                dataGridView5.DataSource = op_salidasmat.GellIdssalidasmat(row.Cells["Column2"].Value.ToString(), dinicio, dfin, 2);
            this.toolStripStatusLabel4.Text = dataGridView5.RowCount.ToString();
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView3.CurrentRow as DataGridViewRow;
            if (i == 1)
                dataGridView5.DataSource = op_salidasmat.Gellresumenfechadet1(row.Cells["dataGridViewTextBoxColumn1"].Value.ToString(), dinicio, dinicio, 2);
            else
                dataGridView5.DataSource = op_salidasmat.GellIdssalidasmat(row.Cells["dataGridViewTextBoxColumn1"].Value.ToString(), dinicio, dfin, 1);

            this.toolStripStatusLabel4.Text = dataGridView5.RowCount.ToString();
        }
        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0)
                {
                    DataGridViewRow row = dataGridView5.CurrentRow as DataGridViewRow;
                    //this.toolStripLabel1.Text = row.Cells[0].Value.ToString();
                    if (this.dataGridView5.Columns[e.ColumnIndex].Name == "eli")
                    {
                        int a = Convert.ToInt32(MessageBox.Show("Está seguro que desea ANULAR el registro", Application.ProductName.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Information));
                        if (a == 6)
                        {
                            proveedores c = new proveedores();
                            c.cod = row.Cells[1].Value.ToString();
                            op_sql.parametro("update salidasmat set anul=1 where nrec=@nrec", "nrec", c.cod);
                            op_sql.parametro1("insert into logaccesos values (null, now(),concat('" + op_var.usu + "',' anuló el recibo salida ','" + c.cod + "'));");
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
                        dataGridView5.DataSource = op_salidasmat.GellAllsalidasmat3(inicio, fin, 2);

                        dataGridView2.AutoGenerateColumns = false;
                        dataGridView2.DataSource = op_salidasmat.Gellresumenfecha2(inicio, fin, 2);

                        dataGridView3.AutoGenerateColumns = false;
                        dataGridView3.DataSource = op_salidasmat.Gellresumenfecha1(inicio, fin, 2);

                        dataGridView4.AutoGenerateColumns = false;
                        dataGridView4.DataSource = op_salidasmat.Gellresumenfecha3(inicio, fin, 2);

                        dinicio = inicio;
                        dfin = fin;
                        this.Text = "Listado Salidas de Materiales - Fecha: " + dinicio.ToShortDateString();
                        i = 1;
                        toolStripLabel2.Text = "Vista Parametrizada";

                        this.toolStripStatusLabel4.Text = dataGridView5.RowCount.ToString();
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

        private void dataGridView5_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView5.CurrentRow as DataGridViewRow;
            Movimientos.frmmovimientosdetINT frm = new Movimientos.frmmovimientosdetINT(row.Cells["tipo"].Value.ToString());
            frm.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Movimientos.frmmovimientosdetINT frm = new Movimientos.frmmovimientosdetINT();
            frm.ShowDialog();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            status fr = new status();
            fr.Show("Cargando Datos");
         
                dataGridView5.AutoGenerateColumns = false;
                dataGridView5.DataSource = op_salidasmat.GellAllsalidasmat();
                this.toolStripStatusLabel4.Text = dataGridView5.RowCount.ToString();

                dataGridView2.AutoGenerateColumns = false;
                dataGridView2.DataSource = op_salidasmat.Gellresumen2();

                dataGridView3.AutoGenerateColumns = false;
                dataGridView3.DataSource = op_salidasmat.Gellresumen1();

                dataGridView4.AutoGenerateColumns = false;
                dataGridView4.DataSource = op_salidasmat.Gellresumen3();
                this.toolStripStatusLabel4.Text = dataGridView5.RowCount.ToString();
                toolStripLabel2.Text = "Vista General";
                i = 0;
              
                if (this.dataGridView5.RowCount > 0)
                    this.toolStripButton2.Enabled = true;

                listBox1.Items.Clear();
                button2.Enabled = false;
                this.comboBox1.Text = null;
                this.comboBox2.Text = null;
                this.checkBox1.Checked = false;
                this.Text = "Listado Salidas de Materiales - Cohorte Actual: " + a.ToShortDateString() + " hasta " + b.ToShortDateString();
                dinicio = a;
                dfin = b;
            
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
        DateTime a, b;

        private void frmmovimientosINT_Load(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = true;
            splitContainer4.Panel1Collapsed = true;

            IntPtr hmenu = GetSystemMenu(this.Handle, 0);
            int cnt = GetMenuItemCount(hmenu);
            // remove 'close' action
            RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            // remove extra menu line
            RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            DrawMenuBar(this.Handle);
            //DateTime a, b;

       
            status fr = new status();
            fr.Show("Cargando Datos");
           
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
                this.Text = "Listado Salidas de Materiales - Cohorte Actual: " + a.ToShortDateString() + " hasta " + b.ToShortDateString();


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
                this.toolStripStatusLabel4.Text = dataGridView5.RowCount.ToString();

            
        }
        DateTime dinicio, dfin;
        int i = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (!(string.IsNullOrEmpty(this.comboBox1.Text) || string.IsNullOrEmpty(this.comboBox1.Text)))
                {
                    DateTime inicio, fin;


                    if (checkBox1.Checked == false)
                    {
                        inicio = Convert.ToDateTime(string.Concat("01", "/", Convert.ToString(comboBox2.SelectedIndex + 1).PadLeft(2, '0'), "/", comboBox1.Text));// pongo el 1 porque siempre es el primer día obvio
                        fin = Convert.ToDateTime(string.Concat("01", "/", Convert.ToString(comboBox2.SelectedIndex + 2).PadLeft(2, '0'), "/", comboBox1.Text)).AddDays(-1); //resto un día al mes y con esto obtengo el ultimo día
                    }
                    else
                    {
                        inicio = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(comboBox2.SelectedIndex + 1).PadLeft(2, '0'), "/", comboBox1.Text));// pongo el 1 porque siempre es el primer día obvio
                        fin = inicio.AddDays(int.Parse(numd) - 1); //resto un día al mes y con esto obtengo el ultimo día
                    }


                    dataGridView5.AutoGenerateColumns = false;
                    dataGridView5.DataSource = op_salidasmat.GellAllsalidasmat3(inicio, fin, 1);

                    dataGridView2.AutoGenerateColumns = false;
                    dataGridView2.DataSource = op_salidasmat.Gellresumenfecha2(inicio, fin, 1);

                    dataGridView3.AutoGenerateColumns = false;
                    dataGridView3.DataSource = op_salidasmat.Gellresumenfecha1(inicio, fin, 1);

                    dataGridView4.AutoGenerateColumns = false;
                    dataGridView4.DataSource = op_salidasmat.Gellresumenfecha3(inicio, fin, 1);

                    toolStripLabel2.Text = "Vista Parametrizada";
                    this.Text = "Listado Salidas de Materiales - Cohorte : " + inicio.ToShortDateString() + " hasta " + fin.ToShortDateString();

                    using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                    {
                        conn.Open();

                        MySqlCommand cmd = new MySqlCommand("select fech from salidasmat where fech between @i and @f and anul=0 group by fech;", conn);
                        cmd.Parameters.AddWithValue("@i", inicio);
                        cmd.Parameters.AddWithValue("@f", fin);
                        listBox1.Items.Clear();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows == true)
                        {
                            while (reader.Read())
                                listBox1.Items.Add(Convert.ToDateTime(reader[0]));
                        }
                    }


                    if (dataGridView5.RowCount <= 0)
                    {
                        dataGridView2.DataSource = null;
                        dataGridView3.DataSource = null;
                        dataGridView4.DataSource = null;
                        
                        listBox1.Items.Clear();
                       
                    }
                    else
                        dinicio = inicio;
                    dfin = fin;
                    button2.Enabled = true;
                    toolStripButton2.Enabled = false;
                    this.toolStripStatusLabel4.Text = dataGridView5.RowCount.ToString();

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

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            button2.Enabled = false;
            this.comboBox1.Text = null;
            this.comboBox2.Text = null;
            this.checkBox1.Checked = false;
        }

        private void filtrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(this.comboBox1.Text) || string.IsNullOrEmpty(this.comboBox1.Text)))
                {
                    DateTime inicio, fin;

                    inicio = Convert.ToDateTime(Convert.ToString(listBox1.SelectedItem).Substring(0, 10));

                    fin = Convert.ToDateTime(string.Concat("01", "/", Convert.ToString(comboBox2.SelectedIndex + 2).PadLeft(2, '0'), "/", comboBox1.Text)).AddDays(-1); //resto un día al mes y con esto obtengo el ultimo día

                    dataGridView5.AutoGenerateColumns = false;
                    dataGridView5.DataSource = op_salidasmat.GellAllsalidasmat3(inicio, fin, 2);

                    dataGridView2.AutoGenerateColumns = false;
                    dataGridView2.DataSource = op_salidasmat.Gellresumenfecha2(inicio, fin, 2);

                    dataGridView3.AutoGenerateColumns = false;
                    dataGridView3.DataSource = op_salidasmat.Gellresumenfecha1(inicio, fin, 2);

                    dataGridView4.AutoGenerateColumns = false;
                    dataGridView4.DataSource = op_salidasmat.Gellresumenfecha3(inicio, fin, 2);

                    dinicio = inicio;
                    dfin = fin;
                    i = 1;
                    this.Text = "Listado Salidas de Materiales - Fecha: " + dinicio.ToShortDateString();
                    this.toolStripStatusLabel4.Text = dataGridView5.RowCount.ToString();
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

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            op_var.a = Convert.ToDateTime(listBox1.SelectedItem.ToString());
            Reportes.Reportes02 frm = new Reportes.Reportes02(2);
            frm.ShowDialog();
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
                ///reporte parametro listbox
                op_var.a = Convert.ToDateTime(string.Concat(diai, "/", Convert.ToString(Operaciones.op_var.mes(comboBox2.SelectedIndex) - 1).PadLeft(2, '0'), "/", comboBox1.Text));// pongo el 1 porque siempre es el primer día obvio
                op_var.b = op_var.a.AddDays(int.Parse(numd)-1); //resto un día al mes y con esto obtengo el ultimo día
            }
            Reportes.Reportes02 frm = new Reportes.Reportes02(1);
            frm.ShowDialog();
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
            Reportes.Reportes02 frm = new Reportes.Reportes02(4);
            frm.ShowDialog();
        }

        private void cohorteToolStripMenuItem1_Click(object sender, EventArgs e)
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
            Reportes.Reportes02 frm = new Reportes.Reportes02(1);
            frm.ShowDialog();
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
            Reportes.Reportes02 frm = new Reportes.Reportes02(5);
            frm.ShowDialog();
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
            Reportes.Reportes02 frm = new Reportes.Reportes02(6);
            frm.ShowDialog();
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
            Reportes.Reportes02 frm = new Reportes.Reportes02(7);
            frm.ShowDialog();
        }

        //private void toolStripButton7_Click(object sender, EventArgs e)
        //{
        //    //try
        //    //{
            //    DataTable dt = new DataTable();
            //    MySqlDataAdapter dAdapter = new MySqlDataAdapter("select *from tablaPrueba", "Data Source = TestExport.sqlite; Version=3;");
            //    dAdapter.Fill(dt);
            //    dataGridView5.DataSource = dt;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        //}


        ////Función que genera el documento Pdf
        //public void GenerarDocumento(Document document)
        //{
        //    //se crea un objeto PdfTable con el numero de columnas del dataGridView
        //    PdfPTable datatable = new PdfPTable(dataGridView5.ColumnCount);
        //    //asignanos algunas propiedades para el diseño del pdf
        //    datatable.DefaultCell.Padding = 3;
        //    float[] headerwidths = GetTamañoColumnas(dataGridView5);
        //    datatable.SetWidths(headerwidths);
        //    datatable.WidthPercentage = 100;
        //    datatable.DefaultCell.BorderWidth = 1;
        //    datatable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;

        //    //SE GENERA EL ENCABEZADO DE LA TABLA EN EL PDF
        //    for (int i = 0; i < dataGridView5.ColumnCount; i++)
        //    {
        //        datatable.AddCell(dataGridView5.Columns[i].HeaderText);
        //    }

        //    datatable.HeaderRows = 1;
        //    datatable.DefaultCell.BorderWidth = 1;


        //    //SE GENERA EL CUERPO DEL PDF
        //    for (int i = 0; i < dataGridView5.RowCount; i++)
        //    {
        //        for (int j = 0; j < dataGridView5.ColumnCount; j++)
        //        {
        //            datatable.AddCell(dataGridView5[j, i].Value.ToString());
        //        }
        //        datatable.CompleteRow();
        //    }

        //    //SE AGREGAR LA PDFPTABLE AL DOCUMENTO
        //    document.Add(datatable);
        //}

        ////Obtiene los tamaños de las columnas del grid
        //public float[] GetTamañoColumnas(DataGridView dg)
        //{
        //    float[] values = new float[dg.ColumnCount];

        //    for (int i = 0; i < dg.ColumnCount; i++)
        //    {
        //        values[i] = (float)dg.Columns[i].Width;
        //    }
        //    return values;
        //}


        //private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Document doc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
        //        string filename = "DataGridViewTest.pdf";
        //        FileStream file = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

        //        PdfWriter.GetInstance(doc, file);

        //        doc.Open();
        //        GenerarDocumento(doc);
        //        doc.Close();
        //        Process.Start(filename);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}



        //private void wordToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ArrayList titulos = new ArrayList();
        //        DataTable datosTabla = new DataTable();
        //        //Especificar rutal del archivo con extencion de excel.
        //        op_formatos OF = new op_formatos(Application.StartupPath + @"\\test.doc");

        //        //obtenemos los titulos del grid y creamos las columnas de la tabla
        //        foreach (DataGridViewColumn item in dataGridView5.Columns)
        //        {
        //            titulos.Add(item.HeaderText);
        //            datosTabla.Columns.Add();
        //        }
        //        //se crean los renglones de la tabla
        //        foreach (DataGridViewRow item in dataGridView5.Rows)
        //        {
        //            DataRow rowx = datosTabla.NewRow();
        //            datosTabla.Rows.Add(rowx);
        //        }
        //        //se pasan los datos del dataGridView a la tabla
        //        foreach (DataGridViewColumn item in dataGridView5.Columns)
        //        {
        //            foreach (DataGridViewRow itemx in dataGridView5.Rows)
        //            {
        //                datosTabla.Rows[itemx.Index][item.Index] = dataGridView5[item.Index, itemx.Index].Value;
        //            }
        //        }
        //        OF.Export(titulos, datosTabla);
        //        Process.Start(OF.xpath);
        //        MessageBox.Show("Procceso Completo");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

       

        private void toolStripButton7_Click_1(object sender, EventArgs e)
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

            Consultas.frmverificacion fr = new Consultas.frmverificacion(DateTime.Now, DateTime.Now, null,null, 5);

            fr.ShowDialog();
        }
    }
}
