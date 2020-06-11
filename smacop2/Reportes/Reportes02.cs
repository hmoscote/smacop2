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
    public partial class Reportes02 : Form
    {
        public Reportes02()
        {
            InitializeComponent();
        }
        int _opc;
        public Reportes02(int opc)
            : this()
        {
            _opc = opc;
        }
        private void Reportes02_Load(object sender, EventArgs e)
        {
        

            if (_opc == 1)
                this.lstsalidasmatTableAdapter.FillBy2(this.logicopDataSet.lstsalidasmat, op_var.a, op_var.b);
                
            if (_opc == 2)
                this.lstsalidasmatTableAdapter.FillBy2(this.logicopDataSet.lstsalidasmat, op_var.a, op_var.a);

            if (_opc == 3)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "smacop2.Reportes.R0000009.rdlc";
                this.reportViewer1.Name = "reportViewer1";
                this.logicopDataSet.DataSetName = "logicopDataSet";
                this.lstsalidasmatBindingSource.DataMember = "lstsalidasmat";
                this.lstsalidasmatBindingSource.DataSource = this.logicopDataSet;
                this.lstsalidasmatTableAdapter.FillBy(this.logicopDataSet.lstsalidasmat, op_var.c, op_var.a, op_var.b);
            }
            if (_opc ==9)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "smacop2.Reportes.R0000009.rdlc";
                this.reportViewer1.Name = "reportViewer1";
                this.logicopDataSet.DataSetName = "logicopDataSet";
                this.lstsalidasmatBindingSource.DataMember = "lstsalidasmat";
                this.lstsalidasmatBindingSource.DataSource = this.logicopDataSet;
                this.lstsalidasmatTableAdapter.FillBy3(this.logicopDataSet.lstsalidasmat, op_var.c, op_var.a, op_var.b);
            }
            if (_opc == 4)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "smacop2.Reportes.R0000009.rdlc";
                this.reportViewer1.Name = "reportViewer1";
                this.logicopDataSet.DataSetName = "logicopDataSet";
                this.lstsalidasmatBindingSource.DataMember = "lstsalidasmat";
                this.lstsalidasmatBindingSource.DataSource = this.logicopDataSet;
                this.lstsalidasmatTableAdapter.FillBy2(this.logicopDataSet.lstsalidasmat, op_var.a, op_var.b);
            
            }
            if (_opc == 5)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "smacop2.Reportes.R0000010.rdlc";
                //this.reportViewer1.Name = "reportViewer1";
                //this.logicopDataSet.DataSetName = "logicopDataSet";
                //this.lstsalidasmatBindingSource.DataMember = "lstsalidasmat";
                //this.lstsalidasmatBindingSource.DataSource = this.logicopDataSet;
                this.lstsalidasmatTableAdapter.FillBy2(this.logicopDataSet.lstsalidasmat, op_var.a, op_var.b);
            }
            if (_opc == 6)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "smacop2.Reportes.R0000011.rdlc";
                //this.reportViewer1.Name = "reportViewer1";
                //this.logicopDataSet.DataSetName = "logicopDataSet";
                //this.lstsalidasmatBindingSource.DataMember = "lstsalidasmat";
                //this.lstsalidasmatBindingSource.DataSource = this.logicopDataSet;
                this.lstsalidasmatTableAdapter.FillBy2(this.logicopDataSet.lstsalidasmat, op_var.a, op_var.b);
            }

            if (_opc == 7)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "smacop2.Reportes.R0000012.rdlc";
                //this.reportViewer1.Name = "reportViewer1";
                //this.logicopDataSet.DataSetName = "logicopDataSet";
                //this.lstsalidasmatBindingSource.DataMember = "lstsalidasmat";
                //this.lstsalidasmatBindingSource.DataSource = this.logicopDataSet;
                this.lstsalidasmatTableAdapter.FillBy2(this.logicopDataSet.lstsalidasmat, op_var.a, op_var.b);
            }
            if (_opc == 8)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "smacop2.Reportes.R0000016.rdlc";
                //this.reportViewer1.Name = "reportViewer1";
                //this.logicopDataSet.DataSetName = "logicopDataSet";
                //this.lstsalidasmatBindingSource.DataMember = "lstsalidasmat";
                //this.lstsalidasmatBindingSource.DataSource = this.logicopDataSet;
                this.lstsalidasmatTableAdapter.FillBy2(this.logicopDataSet.lstsalidasmat, op_var.a, op_var.b);
            }
            if (_opc == 10)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "smacop2.Reportes.R0000008.rdlc";
                this.reportViewer1.Name = "reportViewer1";
                this.logicopDataSet.DataSetName = "logicopDataSet";
                this.lstsalidasmatBindingSource.DataMember = "lstsalidasmat";
                this.lstsalidasmatBindingSource.DataSource = this.logicopDataSet;
                this.lstsalidasmatTableAdapter.FillBy4(this.logicopDataSet.lstsalidasmat,op_var.c, op_var.a, op_var.b);
            }
            this.reportViewer1.RefreshReport();
        }

        }

    }

