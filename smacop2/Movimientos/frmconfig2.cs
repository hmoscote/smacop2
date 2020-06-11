
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using smacop2.Entity;
using smacop2.Administracion;
using smacop2.Operaciones;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;


namespace smacop2.Movimientos
{
    public partial class frmconfig2 : Form
    {
        public frmconfig2()
        {
            InitializeComponent();
            //this.textBox1.KeyPress+=new KeyPressEventHandler(numero);
            this.dataGridView1.CellContentClick+=new DataGridViewCellEventHandler(dataGridView1_CellContentClick);
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
        private void numero(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))) e.Handled = true;
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            op_conf_transp_mat.ctranspmat i = new op_conf_transp_mat.ctranspmat();

            //i.valfle = Convert.ToDecimal(this.textBox1.Text);
            //i.ttransp1 = Convert.ToString(this.comboBox3.SelectedValue);
            //i.ttransp2 = Convert.ToString(this.comboBox2.SelectedValue);
            //i.ttransp3 = Convert.ToString(this.comboBox1.SelectedValue);
            //i.entradas = Convert.ToBoolean(this.checkBox4.Checked);
            //i.salidas = Convert.ToBoolean(this.checkBox5.Checked);
            int rowsAffected = 0;

            if (a > 0)
            {
                i.id = a;
                rowsAffected = op_conf_transp_mat.accion(i, 2);
            }
            else
                rowsAffected = op_conf_transp_mat.accion(i, 1);

            if (rowsAffected > 0)
            {
                if (rowsAffected == 1)
                    MessageBox.Show(mensajes.MsjProc1, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (rowsAffected == 2)
                    MessageBox.Show(mensajes.MsjProc2, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (rowsAffected == 3)
                    MessageBox.Show(mensajes.MsjProc3, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private int a = 0;
        private void frmconfig2_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = op_rangos.GellAllrango4("select * from rangos_fle"); 
            
            //comboBox1.DataSource = op_combos.FillCombo("select cod, nom from tipo_equipo");
            //comboBox1.DisplayMember = "nom";
            //comboBox1.ValueMember = "cod";
            //comboBox1.Text = null;

            //comboBox2.DataSource = op_combos.FillCombo("select cod, nom from tipo_equipo");
            //comboBox2.DisplayMember = "nom";
            //comboBox2.ValueMember = "cod";
            //comboBox2.Text = null;

            //comboBox3.DataSource = op_combos.FillCombo("select cod, nom from tipo_equipo");
            //comboBox3.DisplayMember = "nom";
            //comboBox3.ValueMember = "cod";
            //comboBox3.Text = null;

               //op_conf_transp_mat.ctranspmat  c =  op_conf_transp_mat.GellIdtranspmat();
           
               // if (c != null)
               // {
               //     a=c.id ;
               //     this.textBox1.Text = c.valfle.ToString();
               //     this.comboBox1.SelectedValue = c.ttransp2;
               //     this.comboBox2.SelectedValue = c.ttransp3;
               //     this.comboBox3.SelectedValue = c.ttransp1;
               //     this.checkBox4.Checked = c.entradas;
               //     this.checkBox5.Checked = c.salidas;
               //     //this.maskedTextBox1.Text = c.cod;
               //     //this.textBox2.Text = c.des;
               //     //this.textBox5.Text = c.cap;
                    
               //     //this.textBox7.Text = c.pla;
               //     //this.textBox3.Text = c.mod;
               //     //this.textBox11.Text = c.ser;
               //     //this.textBox10.Text = c.hms.ToString();
               //     //this.textBox6.Text = c.kms.ToString();
               //     //this.textBox4.Text = c.pot;
               //     //this.textBox8.Text = c.vho.ToString();
               //     //this.textBox9.Text = c.csh.ToString();
               //     //this.comboBox1.SelectedValue = c.teq;
               //     //this.comboBox2.SelectedValue = c.cmb.ToString();
               //     //this.comboBox3.SelectedValue = c.ope;
               //     //this.textBox1.Text = c.ccg.ToString();
               //     //toolStripButton5.Visible = true;
               //     //toolStripButton1.Visible = false;
                    
               //     //operacion();
               //     //toolStripButton3.Visible = false;
               //     //toolStripButton2.Visible = false;
                   
               // }
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
                            rangofle c = new rangofle();
                            c.cod = row.Cells[0].Value.ToString();
                            //c.rini = int.Parse(row.Cells[1].Value.ToString());
                            //c.rfin = int.Parse(row.Cells[2].Value.ToString());

                            op_rangos.accion1(c, 3);
                            dataGridView1.AutoGenerateColumns = false;
                            dataGridView1.DataSource = op_rangos.GellAllrango3("select * from rangos_fle where cod='" + c.cod + "'");
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
        private void button4_Click(object sender, EventArgs e)
        {
            frmrangosfle fr = new frmrangosfle();
            fr.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = op_rangos.GellAllrango4("select * from rangos_fle"); 
        }

        //private void button3_Click(object sender, System.EventArgs e)
        //{
        //    comboBox3.Text = null;
        //}

        //private void button4_Click(object sender, System.EventArgs e)
        //{
        //    comboBox1.Text = null;
        //}

        //private void button5_Click(object sender, System.EventArgs e)
        //{
        //    comboBox2.Text = null;
        //}
    }
}
