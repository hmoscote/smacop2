using System;
using System.Drawing;
using System.Windows.Forms;
using smacop2.Entity;
using smacop2.Administracion;
using smacop2.Operaciones;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;

namespace smacop2
{
    public partial class frmequipos : Form
    {
        //private string _id;
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

        public frmequipos()
        {
            InitializeComponent();
            this.listView1.DoubleClick += new EventHandler(listView1_DoubleClick);
            this.listView1.Click += new EventHandler(listView1_Click);
            this.listView1.MouseDown += new MouseEventHandler(listView1_MouseDown);
            this.toolStripTextBox1.KeyDown+=new KeyEventHandler(toolStripTextBox1_KeyDown);
            //this.toolStripButton8.Click+=new EventHandler(toolStripButton8_Click);
            dataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(dataGridView1_CellDoubleClick);
            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(dataGridView1_CellContentClick);
            dataGridView1.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView1_CellPainting);

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
        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            if (e.KeyCode == Keys.F1)
                dataGridView1.DataSource = op_equipos.GellAllequipos2(toolStripTextBox1.Text + "%", 1);
            if (e.KeyCode == Keys.F2)
                dataGridView1.DataSource = op_equipos.GellAllequipos2(toolStripTextBox1.Text + "%", 2);
            if (e.KeyCode == Keys.F3)
                dataGridView1.DataSource = op_equipos.GellAllequipos2(toolStripTextBox1.Text + "%", 3);
            if (dataGridView1.RowCount == 0)
                MessageBox.Show("El parámetro no tiene información", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            //if ((!this.dataGridView1.Focused)) return base.ProcessCmdKey(ref msg, keyData);
            //if (keyData != Keys.Enter) return base.ProcessCmdKey(ref msg, keyData);
            //DataGridViewRow row = this.dataGridView1.CurrentRow;
            //frmclientesdet fr = new frmclientesdet(row.Cells["cod"].Value.ToString());
            //fr.Show();


            if ((!this.listView1.Focused)) return base.ProcessCmdKey(ref msg, keyData);
            if (keyData != Keys.Enter) return base.ProcessCmdKey(ref msg, keyData);
            int i = listView1.SelectedIndices[0];
            frmtipoequipos frm = new frmtipoequipos(this.listView1.Items[i].Text);
        
            frm.ShowDialog();
            return true;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow as DataGridViewRow;
            frmequiposdet frm = new frmequiposdet(row.Cells["tipo"].Value.ToString());
            frm.ShowDialog();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.CurrentRow as DataGridViewRow;
                    //this.toolStripLabel1.Text = row.Cells[0].Value.ToString();
                    if (this.dataGridView1.Columns[e.ColumnIndex].Name == "eli")
                    {
                        int a = Convert.ToInt32(MessageBox.Show("Está seguro que desea eliminar el registro", Application.ProductName.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Information));
                        if (a == 6)
                        {

                            equipos c = new equipos();
                            c.cod = row.Cells["tipo"].Value.ToString();
                            //c.nom = row.Cells[1].Value.ToString();
                            //c.niv = Convert.ToInt32(row.Cells[2].Value.ToString());
                            //c.sal = Convert.ToDecimal(row.Cells[3].Value.ToString());
                            op_equipos.accion(c, 3);
                            //this.toolStripStatusLabel1.Text = "Cargando...";
                            dataGridView1.AutoGenerateColumns = false;
                            dataGridView1.DataSource = op_equipos.GellAllequipos();
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
        private void listView1_MouseDown(object sender, EventArgs e)
        {
            try
            {
                int i = listView1.SelectedIndices[0];
            }
            catch (Exception)
            {
                this.label6.Text = "<>";
                this.label5.Text = "<>";
                this.label4.Text = "<>";
                this.label7.Text = "<>";
                return;
            }
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            try
            {
                int i = listView1.SelectedIndices[0];


                tipo_equipos c = op_tipo_equipos.GellIdtipoE(this.listView1.Items[i].Text, 1);
                if (c != null)
                {
                    this.label6.Text = c.cod;
                    this.label5.Text = c.nom;
                    if (c.tip == "1")
                        this.label4.Text = "AMARILLA";
                    else
                        this.label4.Text = "BLANCA";
                    this.label7.Text = op_sql.contar("select count(codeq) from equipos where tipeq='" + c.cod+"'");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT cod FROM tipo_equipo";
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    this.listView1.Items.Clear();
                    while (reader.Read())
                        this.listView1.Items.Add(reader["cod"].ToString(), 6);
                }
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      


        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int i = listView1.SelectedIndices[0];
                frmtipoequipos frm = new frmtipoequipos(this.listView1.Items[i].Text);
              
                frm.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Señale el tipo de Equipo", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        //protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        //{
        //    if ((!this.listView1.Focused)) return base.ProcessCmdKey(ref msg, keyData);
        //    if (keyData != Keys.Enter) return base.ProcessCmdKey(ref msg, keyData);
        //    int i = listView1.SelectedIndices[0];
        //    frmtipoequipos frm = new frmtipoequipos(this.listView1.Items[i].Text);
        //    frm.MdiParent = this.MdiParent;
        //    frm.Show();
        //    return true;
        //}

        private void frmequipos_Load(object sender, EventArgs e)
        {
            IntPtr hmenu = GetSystemMenu(this.Handle, 0);
            int cnt = GetMenuItemCount(hmenu);
            // remove 'close' action
            RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            // remove extra menu line
            RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            DrawMenuBar(this.Handle);
            


            try
            {

                conf c = opconf.GellIdConf("equipos");
                if (c != null)
                {
                    if (c.cargar == true)
                    {
                        status fr = new status();
                        fr.Show("Cargando Datos");
                        dataGridView1.AutoGenerateColumns = false;
                        dataGridView1.DataSource = op_equipos.GellAllequipos();
                        this.toolStripStatusLabel1.Text = dataGridView1.RowCount.ToString();
                    }

                }

                splitContainer1.Panel1Collapsed = true;
                string sql = @"SELECT cod FROM tipo_equipo";
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        this.listView1.Items.Clear();
                        while (reader.Read())
                            this.listView1.Items.Add(reader["cod"].ToString(), 6);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            status fr = new status();
            fr.Show("Cargando Datos");
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = op_equipos.GellAllequipos();
            this.toolStripStatusLabel1.Text = dataGridView1.RowCount.ToString();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {

        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1_DoubleClick(listView1, e);
        }

        private void filtrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int i = listView1.SelectedIndices[0];
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = op_equipos.GellAllequipos2(this.listView1.Items[i].Text, 3);
                this.toolStripStatusLabel1.Text = dataGridView1.RowCount.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Señale el tipo de Equipo", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            frmtipoequipos frm = new frmtipoequipos();
            frm.ShowDialog();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = listView1.SelectedIndices[0];
            try
            {
                int a = Convert.ToInt32(MessageBox.Show("Está seguro que desea eliminar el registro", Application.ProductName.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Information));
                if (a == 6)
                {
                    tipo_equipos c = new tipo_equipos();
                    c.cod = this.listView1.Items[i].Text;
                    op_tipo_equipos.accion(c, 3);
                    frmequipos_Load(this, e);
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

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            frmconflistados frm = new frmconflistados(4);
            frm.ShowDialog();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            Administracion.frmequiposdet frm = new Administracion.frmequiposdet();
            frm.ShowDialog();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.toolStripTextBox1.Text))
            {
                conf c = opconf.GellIdConf("EQUIPOS");
                if (c != null)
                {
                    if (c.campo == "CODIGO" && c.valor == "C")
                        dataGridView1.DataSource = op_equipos.GellAllequipos2(toolStripTextBox1.Text + "%", 1);
                    if (c.campo == "CODIGO" && c.valor == "E")
                        dataGridView1.DataSource = op_equipos.GellAllequipos2("%" + toolStripTextBox1.Text + "%", 1);
                    if (c.campo == "CODIGO" && c.valor == "T")
                        dataGridView1.DataSource = op_equipos.GellAllequipos2("%" + toolStripTextBox1.Text, 1);
                    if (c.campo == "NOMBRE" && c.valor == "C")
                        dataGridView1.DataSource = op_equipos.GellAllequipos2(toolStripTextBox1.Text + "%", 2);
                    if (c.campo == "NOMBRE" && c.valor == "E")
                        dataGridView1.DataSource = op_equipos.GellAllequipos2("%" + toolStripTextBox1.Text + "%", 2);
                    if (c.campo == "NOMBRE" && c.valor == "T")
                        dataGridView1.DataSource = op_equipos.GellAllequipos2("%" + toolStripTextBox1.Text, 2);
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

        private void exportarAExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void exportarACVSToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void exportarAHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            if (toolStripButton2.Checked == false)
            {
                splitContainer1.Panel1Collapsed = false;
                toolStripButton2.Checked = true;
                toolStripButton2.CheckState = CheckState.Checked;
            }
            else
            {
                splitContainer1.Panel1Collapsed = true;
                toolStripButton2.Checked = false;
                toolStripButton2.CheckState = CheckState.Unchecked;
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
