using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace smacop2.Reportes
{
    public partial class Reportes03 : Form
    {
        public Reportes03()
        {
            InitializeComponent();
        }

         int _opc;
        public Reportes03(int opc)
            : this()
        {
            _opc = opc;
        }
         
          
        private void Reporte03_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'logicopDataSet.resumen_comision' Puede moverla o quitarla según sea necesario.
            //this.resumen_comisionTableAdapter.Fill(this.logicopDataSet.resumen_comision);
            if (_opc == 1)
            {
                this.resumen_comisionTableAdapter.FillBy1(this.logicopDataSet.resumen_comision,Operaciones.op_var.a,Operaciones.op_var.b);
            }

            if (_opc == 2)
            {
                this.resumen_comisionTableAdapter.FillBy2(this.logicopDataSet.resumen_comision, Operaciones.op_var.c, Operaciones.op_var.a, Operaciones.op_var.b);
            }
            // TODO: esta línea de código carga datos en la tabla 'logicopDataSet.resumen_comision' Puede moverla o quitarla según sea necesario.
            //this.resumen_comisionTableAdapter.Fill(this.logicopDataSet.resumen_comision);

            this.reportViewer1.RefreshReport();

        }
    }
}
