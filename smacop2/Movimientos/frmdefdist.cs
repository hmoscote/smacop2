using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.VisualBasic;

using System.IO;
using System.Drawing.Imaging;
using System.Reflection;
using smacop2.Entity;
using smacop2.Operaciones;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;



namespace smacop2.Movimientos
{
    public partial class frmdefdist : Form
    {
        public frmdefdist()
        {
            InitializeComponent();
            this.dataGridView1.CellContentClick+=new DataGridViewCellEventHandler(dataGridView1_CellContentClick);
            this.textBox1.KeyPress += new KeyPressEventHandler(numero_decimal);
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
        private void numero_decimal(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsPunctuation(e.KeyChar))) e.Handled = true;
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
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
                        int a = Convert.ToInt32(MessageBox.Show("Está seguro que desea ANULAR el registro", Application.ProductName.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Information));
                        if (a == 6)
                        {
                            op_distancias.distancias c = new op_distancias.distancias();
                            for (int i = 0; i < 5; i++)
                            c.tip1 = row.Cells[0].Value.ToString();
                            c.lug1 = row.Cells[1].Value.ToString();
                            c.tip2 = row.Cells[2].Value.ToString();
                            c.lug2 = row.Cells[3].Value.ToString();
                            op_sql.parametro1(@"DELETE FROM `logicop`.`distancias` WHERE `tip1` = '" + c.tip1 + "' and `lug1` = '" + c.lug1 + "' and `tip2` = '" + c.tip2 + "' AND `lug2` = '" + c.lug2 + "';");
                            this.dataGridView1.AutoGenerateColumns = false;
                            this.dataGridView1.DataSource = op_distancias.GellAlldistancias();
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
        private bool validacion()
        {
            bool o = true;
            errorProvider1.Clear();


            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "El precio es Obligatorio");
                o = false;
            }

            if (string.IsNullOrEmpty(this.comboBox1.Text))
            {
                errorProvider1.SetError(comboBox1, "Tipo de Ubicación es Obligatorio...");
                o = false;
            }

            if (string.IsNullOrEmpty(this.comboBox2.Text))
            {
                errorProvider1.SetError(comboBox2, "El Origen es Obligatorio...");
                o = false;
            }

            if (string.IsNullOrEmpty(this.comboBox3.Text))
            {
                errorProvider1.SetError(comboBox3, "El Destino es Obligatorio...");
                o = false;
            }
            if (string.IsNullOrEmpty(this.comboBox4.Text))
            {
                errorProvider1.SetError(comboBox4, "Tipo de Ubicación es Obligatorio...");
                o = false;
            }
            return o;
        }
        private void frmdefdist_Load(object sender, EventArgs e)
        {
            status fr = new status();
            fr.Show("Cargando Datos");
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = op_distancias.GellAlldistancias();
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                comboBox2.DataSource = op_combos.FillCombo("select cod, nom from proveedores order by 2");
                comboBox2.DisplayMember = "nom";
                comboBox2.ValueMember = "cod";
                comboBox2.Text = null;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                comboBox2.DataSource = op_combos.FillCombo("select idproy as cod, nomproy as nom from proyectos where  activo=1 order by 2");
                comboBox2.DisplayMember = "nom";
                comboBox2.ValueMember = "cod";
                comboBox2.Text = null;
            }
            else
            {
                comboBox2.DataSource = op_combos.FillCombo("select COD as cod, nom as nom from lugares order by 2");
                comboBox2.DisplayMember = "nom";
                comboBox2.ValueMember = "cod";
                comboBox2.Text = null;
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex == 0)
            {
                comboBox3.DataSource = op_combos.FillCombo("select cod, nom from proveedores order by 2");
                comboBox3.DisplayMember = "nom";
                comboBox3.ValueMember = "cod";
                comboBox3.Text = null;
            }
            else if (comboBox4.SelectedIndex == 1)
            {
                comboBox3.DataSource = op_combos.FillCombo("select idproy as cod, nomproy as nom from proyectos where  activo=1 order by 2");
                comboBox3.DisplayMember = "nom";
                comboBox3.ValueMember = "cod";
                comboBox3.Text = null;
            }
            else 
            {
                comboBox3.DataSource = op_combos.FillCombo("select COD as cod, nom as nom from lugares order by 2");
                comboBox3.DisplayMember = "nom";
                comboBox3.ValueMember = "cod";
                comboBox3.Text = null;
            }
           
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox1.Focus();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox1.Focus();
        }
        private void borrar()
        {
            this.comboBox1.Text = null;
            this.comboBox2.Text = null;
            this.comboBox3.Text = null;
            this.comboBox4.Text = null;
            this.textBox1.Text = null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool recibe1 , recibe2 = false;
            if (!validacion()) return;
            op_distancias.distancias a = new op_distancias.distancias();
            a.lug1 = this.comboBox2.Text;
            a.lug2 = this.comboBox3.Text;
            a.tip1 = this.comboBox1.Text;
            a.tip2 = this.comboBox4.Text;
            a.cant = int.Parse(this.textBox1.Text);

            if (a.lug1 == a.lug2)
            {
                MessageBox.Show("El lugar debe ser diferente", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            recibe1=op_sql.comprobar("SELECT * FROM distancias WHERE `tip1` = '" + a.tip1 + "' and`lug1` = '" + a.lug1 + "' and`tip2` = '" + a.tip2 + "' and `lug2` = '"+a.lug2+"'");
            if (recibe1)
            {
                MessageBox.Show(mensajes.MsjProc4, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            recibe2=op_sql.comprobar("SELECT * FROM distancias WHERE `tip1` = '" + a.tip2 + "' and`lug1` = '" + a.lug2 + "' and`tip2` = '" + a.tip1 + "' and `lug2` = '"+a.lug1+"'");
            if (recibe2)
            {
                MessageBox.Show("El origen y el destino se aplica de forma viceversa", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            int c = op_distancias.accion(a, 1);

            if (c > 0)
            {
                if (c == 1)
                {
                    MessageBox.Show(mensajes.MsjProc1, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    borrar();
                }
                if (c == 2)
                {
                    MessageBox.Show(mensajes.MsjProc2, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    borrar();
                }
                if (c == 3)
                {
                    MessageBox.Show(mensajes.MsjProc3, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    borrar();
                }
                if (c == 4)
                {
                    //if (MessageBox.Show(mensajes.MsjProc4 + " " + mensajes.MsjProc5, Application.ProductName.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    //    rowsAffected = op_equipos.accion(a, 2);
                    //}
                    MessageBox.Show(mensajes.MsjProc4 + " " + mensajes.MsjProc5, Application.ProductName.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(mensajes.MsjProc0, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            status fr = new status();
            fr.Show("Cargando Datos");
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = op_distancias.GellAlldistancias();
        }
    }
}
