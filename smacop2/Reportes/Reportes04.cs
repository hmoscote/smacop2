using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using smacop2.Operaciones;

namespace smacop2.Reportes
{
    public partial class Reportes04 : Form
    {
        public Reportes04()
        {
            InitializeComponent();
        }
        int _opc;
        public Reportes04(int opc)
            : this()
        {
            _opc = opc;
        }

        private void Reportes04_Load(object sender, EventArgs e)
        {
            if (_opc == 1)
            this.vista_viajesTableAdapter.FillBy3(this.logicopDataSet.vista_viajes,"");
            //if (_opc == 2)
            //    this.vista_viajesTableAdapter.FillBy2(this.logicopDataSet.lstsalidasmat, Operaciones.op_var.a, Operaciones.op_var.a);
            if (_opc == 2)
           
                this.vista_viajesTableAdapter.FillBy2(this.logicopDataSet.vista_viajes, op_var.ope);

            if (_opc == 3)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "smacop2.Reportes.R0000014.rdlc";
                this.reportViewer1.Name = "reportViewer1";
                this.logicopDataSet.DataSetName = "logicopDataSet";
                this.vistaviajesBindingSource.DataMember = "dsentradas";
                this.vistaviajesBindingSource.DataSource = this.logicopDataSet;
                this.vista_viajesTableAdapter.FillBy(this.logicopDataSet.vista_viajes, op_var.ope, op_var.aa,op_var.yy);
            }
            // TODO: esta línea de código carga datos en la tabla 'logicopDataSet.vista_viajes' Puede moverla o quitarla según sea necesario.
            //this.vista_viajesTableAdapter.Fill(this.logicopDataSet.vista_viajes);
            //Operaciones.op_var.ope=null;
            //Operaciones.op_var.aa=0;
            //Operaciones.op_var.yy =0;
            if (this.logicopDataSet.vista_viajes.Count > 0)
                this.reportViewer1.RefreshReport();
            else
                MessageBox.Show("No existe información correspondiente a los parámetros", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
