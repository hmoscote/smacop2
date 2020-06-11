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

namespace smacop2.Administracion
{
    public partial class frmmateriales : Form
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

        public frmmateriales()
        {
            InitializeComponent();

            //toolStripTextBox1.GotFocus += new EventHandler(toolStripTextBox1_GotFocus);
            //toolStripTextBox1.LostFocus+=new EventHandler(toolStripTextBox1_LostFocus);
            toolStripTextBox1.KeyDown += new KeyEventHandler(toolStripTextBox1_KeyDown);
            toolStripTextBox1.KeyPress += new KeyPressEventHandler(toolStripTextBox1_KeyPress);
            

        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dataGridView1.Columns[e.ColumnIndex].Name == "eli" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = this.dataGridView1.Rows[e.RowIndex].Cells["eli"] as DataGridViewButtonCell;
                Icon icoAtomico = new Icon(Environment.CurrentDirectory + @"\delete_16x.ico");
                e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 3, e.CellBounds.Top + 3);

                this.dataGridView1.Rows[e.RowIndex].Height = icoAtomico.Height + 8;
                this.dataGridView1.Columns[e.ColumnIndex].Width = icoAtomico.Width + 8;

                e.Handled = true;
            }
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if ((!this.dataGridView1.Focused)) return base.ProcessCmdKey(ref msg, keyData);
            if (keyData != Keys.Enter) return base.ProcessCmdKey(ref msg, keyData);
            DataGridViewRow row = this.dataGridView1.CurrentRow;
            frmmaterialesdet frm = new frmmaterialesdet(row.Cells["cod"].Value.ToString());
            frm.ShowDialog();
            return true;
        }
        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                toolStripButton6_Click(toolStripButton6, e);
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow as DataGridViewRow;
            frmmaterialesdet frm = new frmmaterialesdet(row.Cells["cod"].Value.ToString());
            frm.ShowDialog();


          
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            if (e.KeyCode == Keys.F1)
                dataGridView1.DataSource = op_materiales.GellAllMateriales2(toolStripTextBox1.Text + "%", 1);
            if (e.KeyCode == Keys.F2)
                dataGridView1.DataSource = op_materiales.GellAllMateriales2(toolStripTextBox1.Text + "%", 2);
            if (dataGridView1.RowCount == 0)
                MessageBox.Show("El parámetro no tiene información", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0)
                {
                    if (this.dataGridView1.Columns[e.ColumnIndex].Name == "eli")
                    {
                        int a = Convert.ToInt32(MessageBox.Show("Está seguro que desea eliminar el registro", Application.ProductName.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Information));
                        if (a == 6)
                        {
                            DataGridViewRow row = dataGridView1.CurrentRow as DataGridViewRow;
                            materiales c = new materiales();
                            c.cod = row.Cells[1].Value.ToString();
                            op_materiales.accion(c, 3);
                            dataGridView1.AutoGenerateColumns = false;
                            dataGridView1.DataSource = op_materiales.GellAllMateriales();
                            this.toolStripStatusLabel1.Text = dataGridView1.RowCount.ToString();
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
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            status fr = new status();
            fr.Show("Cargando Datos");
            this.toolStripStatusLabel1.Text = "Cargando...";
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = op_materiales.GellAllMateriales();
            this.toolStripStatusLabel1.Text = dataGridView1.RowCount.ToString();            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmmaterialesdet frm = new frmmaterialesdet();
            frm.ShowDialog();
        }

       

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            frmconflistados frm = new frmconflistados(6);
            frm.ShowDialog();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmmateriales_Load(object sender, EventArgs e)
        {
            IntPtr hmenu = GetSystemMenu(this.Handle, 0);
            int cnt = GetMenuItemCount(hmenu);
            // remove 'close' action
            RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            // remove extra menu line
            RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            DrawMenuBar(this.Handle);

            conf c = opconf.GellIdConf("MATERIALES");
            if (c != null)
            {
                if (c.cargar == true)
                {
                    status fr = new status();
                    fr.Show("Cargando Datos");
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.DataSource = op_materiales.GellAllMateriales();
                    this.toolStripStatusLabel1.Text = dataGridView1.RowCount.ToString();       
                }

            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(this.toolStripTextBox1.Text) || this.toolStripTextBox1.Text == "Buscar"))
            {
                conf c = opconf.GellIdConf("materiales");
                if (c != null)
                {
                    if (c.campo == "CODIGO" && c.valor == "C")
                        dataGridView1.DataSource = op_materiales.GellAllMateriales2(toolStripTextBox1.Text + "%", 1);
                    if (c.campo == "CODIGO" && c.valor == "E")
                        dataGridView1.DataSource = op_materiales.GellAllMateriales2("%" + toolStripTextBox1.Text + "%", 1);
                    if (c.campo == "CODIGO" && c.valor == "T")
                        dataGridView1.DataSource = op_materiales.GellAllMateriales2("%" + toolStripTextBox1.Text, 1);
                    if (c.campo == "NOMBRE" && c.valor == "C")
                        dataGridView1.DataSource = op_materiales.GellAllMateriales2(toolStripTextBox1.Text + "%", 2);
                    if (c.campo == "NOMBRE" && c.valor == "E")
                        dataGridView1.DataSource = op_materiales.GellAllMateriales2("%" + toolStripTextBox1.Text + "%", 2);
                    if (c.campo == "NOMBRE" && c.valor == "T")
                        dataGridView1.DataSource = op_materiales.GellAllMateriales2("%" + toolStripTextBox1.Text, 2);
                    if (dataGridView1.RowCount == 0)
                        MessageBox.Show("El parámetro no tiene información", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("El parámetro no tiene información", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Por favor digite el parámetro", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

       
        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.RowCount > 0)
                op_exportacion.opcion(dataGridView1, "xls Excel (*.xls)|*.xls", 0, this.Text);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void cSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.RowCount > 0)
                op_exportacion.opcion(dataGridView1, "csv (*.csv)|*.csv", 1, this.Text);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void hTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.RowCount > 0)
                op_exportacion.opcion(dataGridView1, "html (*.html)|*.html", 0, this.Text);
            else
                MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

       
       
    }
}
