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
namespace smacop2.Consultas
{
    public partial class frmverificacion : Form
    {
        public frmverificacion()
        {
            InitializeComponent();
            dataGridView5.CellContentClick+=new DataGridViewCellEventHandler(dataGridView5_CellContentClick);
            dataGridView2.CellContentClick += new DataGridViewCellEventHandler(dataGridView2_CellContentClick);
            dataGridView5.CellDoubleClick+=new DataGridViewCellEventHandler(dataGridView5_CellDoubleClick);
            dataGridView2.CellDoubleClick += new DataGridViewCellEventHandler(dataGridView2_CellDoubleClick);
        }
        DateTime _a, _b;
        string _s, _c;
        int _o;
        public frmverificacion(DateTime a, DateTime b, string s,string c, int o)
            : this()
        {
            _a = a;
            _b = b;
            _s = s;
            _o = o;
            _c = c;
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView2.CurrentRow as DataGridViewRow;
            Movimientos.frmmovimientosdetEXT frm = new Movimientos.frmmovimientosdetEXT(row.Cells[1].Value.ToString());
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }
        private void dataGridView5_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView5.CurrentRow as DataGridViewRow;
            Movimientos.frmmovimientosdetINT frm = new Movimientos.frmmovimientosdetINT(row.Cells[1].Value.ToString());
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0)
                {
                    DataGridViewRow row = dataGridView5.CurrentRow as DataGridViewRow;
                    if (this.dataGridView5.Columns[e.ColumnIndex].Name == "Column2")
                    {
                        int a = Convert.ToInt32(MessageBox.Show("Está seguro que desea RESTAURAR el registro", Application.ProductName.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Information));
                        if (a == 6)
                        {
                            proveedores c = new proveedores();
                            c.cod = row.Cells[1].Value.ToString();
                            op_sql.parametro("update salidasmat set anul=0 where nrec=@nrec", "nrec", c.cod);
                            op_sql.parametro1("insert into logaccesos values (null, now(),concat('" + op_var.usu + "',' restauró el recibo SALIDA DE MATERIAL ','" + c.cod + "'));");
                            dataGridView5.AutoGenerateColumns = false;
                            dataGridView5.DataSource = op_salidasmat.GellAllsalidasmatver(_a, _b, null,null, 4);
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


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0)
                {
                    DataGridViewRow row = dataGridView2.CurrentRow as DataGridViewRow;
                    if (this.dataGridView2.Columns[e.ColumnIndex].Name == "dataGridViewButtonColumn1")
                    {
                        int a = Convert.ToInt32(MessageBox.Show("Está seguro que desea RESTAURAR el registro", Application.ProductName.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Information));
                        if (a == 6)
                        {
                            proveedores c = new proveedores();
                            c.cod = row.Cells[1].Value.ToString();
                            op_sql.parametro("update entradasmat set anul=0 where nrec=@nrec", "nrec", c.cod);
                            op_sql.parametro1("insert into logaccesos values (null, now(),concat('" + op_var.usu + "',' restauró el recibo ENTRADA DE MATERIAL ','" + c.cod + "'));");
                            dataGridView2.AutoGenerateColumns = false;
                            dataGridView2.DataSource = op_entradasmat.GellAllentradasmatver(_a, _b, null,null, 4);
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

        private void frmverificacion_Load(object sender, EventArgs e)
        {
            
            status fr = new status();
            fr.Show("Cargando Datos");
           
            if (_o == 1)
            {
                this.Text = "Verificación del Equipo " + _s + " con Facturación desde " + _a.ToShortDateString() + " hasta " + _b.ToShortDateString();
                dataGridView5.BringToFront();
                dataGridView5.AutoGenerateColumns = false;
                Column2.Visible = false;
                dataGridView5.DataSource = op_salidasmat.GellAllsalidasmatver(_a, _b, _s, _c, 1);
                this.toolStripStatusLabel4.Text = dataGridView5.RowCount.ToString();

            }
            if (_o == 2)
            {
                this.Text = "Verificación del Equipo " + _s + " con Facturación desde " + _a.ToShortDateString() + " hasta " + _b.ToShortDateString();
                dataGridView5.SendToBack();
                dataGridView2.AutoGenerateColumns = false;
                dataGridViewButtonColumn1.Visible = false;
                dataGridView2.DataSource = op_entradasmat.GellAllentradasmatver(_a, _b, _s,_c,1);
                this.toolStripStatusLabel4.Text = dataGridView2.RowCount.ToString();
            }
            if (_o == 3)
            {
                this.Text = "Verificación del Centro de Costo " + _s + " con Facturación desde " + _a.ToShortDateString() + " hasta " + _b.ToShortDateString();
                dataGridView5.BringToFront();
                dataGridView5.AutoGenerateColumns = false;
                Column2.Visible = false;
                dataGridView5.DataSource = op_salidasmat.GellAllsalidasmatver(_a, _b, _s, _c, 2);
                this.toolStripStatusLabel4.Text = dataGridView5.RowCount.ToString();
            }
            if (_o == 4)
            {
                this.Text = "Verificación del Centro de Costo " + _s + " con Facturación desde " + _a.ToShortDateString() + " hasta " + _b.ToShortDateString();
                dataGridView5.BringToFront();
                dataGridView5.AutoGenerateColumns = false;
                Column2.Visible = false;
                dataGridView5.DataSource = op_salidasmat.GellAllsalidasmatver(_a, _b, _s, _c, 3);
                this.toolStripStatusLabel4.Text = dataGridView5.RowCount.ToString();
            }

            if (_o == 5)
            {
                this.Text = "Listado de Recibos de Salidas Anulados en Facturación" ;
                dataGridView5.BringToFront();
                dataGridView5.AutoGenerateColumns = false;
                Column2.Visible = true;
                dataGridView5.DataSource = op_salidasmat.GellAllsalidasmatver(_a, _b, null,_c, 4);
                this.toolStripStatusLabel4.Text = dataGridView5.RowCount.ToString();
            }
            if (_o == 6)
            {
                this.Text = "Listado de Recibos de Entradas Anulados en Facturación";
                dataGridView5.SendToBack();
                dataGridView2.AutoGenerateColumns = false;
                dataGridViewButtonColumn1.Visible = true;
                dataGridView2.DataSource = op_entradasmat.GellAllentradasmatver(_a, _b,null,_c,4);
                this.toolStripStatusLabel4.Text = dataGridView2.RowCount.ToString();
            }
            if (_o == 7)
            {
                this.Text = "Verificación de Cantera " + _s + " con Facturación desde " + _a.ToShortDateString() + " hasta " + _b.ToShortDateString();
                dataGridView5.SendToBack();
                dataGridView2.AutoGenerateColumns = false;
                dataGridViewButtonColumn1.Visible = false;
                dataGridView2.DataSource = op_entradasmat.GellAllentradasmatver(_a, _b, _s,_c, 5);
                this.toolStripStatusLabel4.Text = dataGridView2.RowCount.ToString();
            }
            if (_o == 8)
            {
                this.Text = "Verificación del Centro de Costo " + _s + " con Facturación desde " + _a.ToShortDateString() + " hasta " + _b.ToShortDateString();
                dataGridView5.BringToFront();
                dataGridView5.AutoGenerateColumns = false;
                Column2.Visible = false;
                dataGridView5.DataSource = op_salidasmat.GellAllsalidasmatver(_a, _b, _s, _c, 5);
                this.toolStripStatusLabel4.Text = dataGridView5.RowCount.ToString();
            }
            if (_o == 9)
            {
                this.Text = "Verificación del Centro de Costo " + _s + " con Facturación desde " + _a.ToShortDateString() + " hasta " + _b.ToShortDateString();
                dataGridView5.BringToFront();
                dataGridView5.AutoGenerateColumns = false;
                Column2.Visible = false;
                dataGridView5.DataSource = op_salidasmat.GellAllsalidasmatver(_a, _b, _s, _c, 6);
                this.toolStripStatusLabel4.Text = dataGridView5.RowCount.ToString();
            }
           
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (_o == 1 || _o == 3 || _o == 4 || _o == 5 || _o == 8)
            {
                if (this.dataGridView5.RowCount > 0)
                    op_exportacion.opcion(dataGridView5, "xls Excel (*.xls)|*.xls", 0, this.Text);
                else
                    MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              
            }
            else
            {
                if (this.dataGridView2.RowCount > 0)
                    op_exportacion.opcion(dataGridView2, "xls Excel (*.xls)|*.xls", 0, this.Text);
                else
                    MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              
            }

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (_o == 1 || _o == 3 || _o == 4 || _o == 5 || _o == 8)
            {
                if (this.dataGridView5.RowCount > 0)
                    op_exportacion.opcion(dataGridView5, "csv (*.csv)|*.csv", 1, this.Text);
                else
                    MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (this.dataGridView2.RowCount > 0)
                    op_exportacion.opcion(dataGridView2, "csv (*.csv)|*.csv", 1, this.Text);
                else
                    MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (_o == 1 || _o == 3 || _o == 4 || _o == 5 || _o == 8)
            {
                if (this.dataGridView5.RowCount > 0)
                    op_exportacion.opcion(dataGridView5, "html (*.html)|*.html", 0, this.Text);
                else
                    MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               
            }
            else
            {
                if (this.dataGridView2.RowCount > 0)
                    op_exportacion.opcion(dataGridView2, "html (*.html)|*.html", 0, this.Text);
                else
                    MessageBox.Show("La matriz no contiene datos", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
