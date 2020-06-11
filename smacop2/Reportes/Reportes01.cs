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
    public partial class Reportes01 : Form
    {
        public Reportes01()
        {
            InitializeComponent();
        }
        int _opc;

        public Reportes01(int opc):this()
        {
            _opc = opc;
        }

        private void Reportes01_Load(object sender, EventArgs e)
        {
            if (_opc == 1)
                this.lstentradasmatTableAdapter.reporte_general(this.logicopDataSet.lstentradasmat, op_var.a,op_var.b);
            if (_opc == 2)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "smacop2.Reportes.R0000001.rdlc";
                this.reportViewer1.Name = "reportViewer1";
                this.logicopDataSet.DataSetName = "logicopDataSet";
                this.lstentradasmatBindingSource.DataMember = "lstentradasmat";
                this.lstentradasmatBindingSource.DataSource = this.logicopDataSet;
                this.lstentradasmatTableAdapter.reporte_fecha(this.logicopDataSet.lstentradasmat, Operaciones.op_var.a);
            }
            if (_opc == 9)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "smacop2.Reportes.R0000002.rdlc";
                this.reportViewer1.Name = "reportViewer1";
                this.logicopDataSet.DataSetName = "logicopDataSet";
                this.lstentradasmatBindingSource.DataMember = "lstentradasmat";
                this.lstentradasmatBindingSource.DataSource = this.logicopDataSet;
                this.lstentradasmatTableAdapter.FillBy1(this.logicopDataSet.lstentradasmat, op_var.c,op_var.a, op_var.b);
                // 
                //this.lstentradasmatTableAdapter.FillBy1(this.logicopDataSet.lstentradasmat, Operaciones.op_var.c, Operaciones.op_var.a, Operaciones.op_var.b);
            }
            if (_opc == 3)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "smacop2.Reportes.R0000003.rdlc";
                this.reportViewer1.Name = "reportViewer1";
                this.logicopDataSet.DataSetName = "logicopDataSet";
                this.lstentradasmatBindingSource.DataMember = "lstentradasmat";
                this.lstentradasmatBindingSource.DataSource = this.logicopDataSet;
                this.lstentradasmatTableAdapter.reporte_general(this.logicopDataSet.lstentradasmat, op_var.a, op_var.b);
            }
            if (_opc == 4)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "smacop2.Reportes.R0000004.rdlc";
                this.reportViewer1.Name = "reportViewer1";
                this.logicopDataSet.DataSetName = "logicopDataSet";
                this.lstentradasmatBindingSource.DataMember = "lstentradasmat";
                this.lstentradasmatBindingSource.DataSource = this.logicopDataSet;
                this.lstentradasmatTableAdapter.reporte_general(this.logicopDataSet.lstentradasmat, op_var.a, op_var.b);
            }
            if (_opc == 5)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "smacop2.Reportes.R0000005.rdlc";
                this.reportViewer1.Name = "reportViewer1";
                this.logicopDataSet.DataSetName = "logicopDataSet";
                this.lstentradasmatBindingSource.DataMember = "lstentradasmat";
                this.lstentradasmatBindingSource.DataSource = this.logicopDataSet;
                this.lstentradasmatTableAdapter.reporte_general(this.logicopDataSet.lstentradasmat, op_var.a, op_var.b);
            }
            if (_opc == 6)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "smacop2.Reportes.R0000006.rdlc";
                this.reportViewer1.Name = "reportViewer1";
                this.logicopDataSet.DataSetName = "logicopDataSet";
                this.lstentradasmatBindingSource.DataMember = "lstentradasmat";
                this.lstentradasmatBindingSource.DataSource = this.logicopDataSet;
                this.lstentradasmatTableAdapter.reporte_general(this.logicopDataSet.lstentradasmat, op_var.a, op_var.b);
            }
            if (_opc == 7)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "smacop2.Reportes.R0000006.rdlc";
                this.reportViewer1.Name = "reportViewer1";
                this.logicopDataSet.DataSetName = "logicopDataSet";
                this.lstentradasmatBindingSource.DataMember = "lstentradasmat";
                this.lstentradasmatBindingSource.DataSource = this.logicopDataSet;
                this.lstentradasmatTableAdapter.reporte_general(this.logicopDataSet.lstentradasmat, op_var.a, op_var.b);
            }

            if (_opc == 8)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "smacop2.Reportes.R0000014.rdlc";
                this.reportViewer1.Name = "reportViewer1";
                this.logicopDataSet.DataSetName = "logicopDataSet";
                this.lstentradasmatBindingSource.DataMember = "lstentradasmat";
                this.lstentradasmatBindingSource.DataSource = this.logicopDataSet;
                this.lstentradasmatTableAdapter.reporte_general(this.logicopDataSet.lstentradasmat, op_var.a, op_var.b);
            }

            //if (_opc == 10)
            //{
            //    this.reportViewer1.LocalReport.ReportEmbeddedResource = "smacop2.Reportes.R0000014.rdlc";
            //    this.reportViewer1.Name = "reportViewer1";
            //    this.logicopDataSet.DataSetName = "logicopDataSet";
            //    this.lstentradasmatBindingSource.DataMember = "lstentradasmat";
            //    this.lstentradasmatBindingSource.DataSource = this.logicopDataSet;
            //    this.lstentradasmatTableAdapter.reporte_general(this.logicopDataSet.lstentradasmat, op_var.a, op_var.b);
            //}
            //this.lstentradasmatTableAdapter.reporte_fecha(this.logicopDataSet.lstentradasmat, DateTime.Parse(Operaciones.op_var.c));
            //this.lstentradasmatTableAdapter.Fill(this.logicopDataSet.lstentradasmat);

            this.reportViewer1.RefreshReport();
        }
    }
}
