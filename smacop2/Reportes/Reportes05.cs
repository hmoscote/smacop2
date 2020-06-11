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
    public partial class Reportes05 : Form
    {
        public Reportes05()
        {
            InitializeComponent();
        }
         int _opc;
        public Reportes05(int opc)
            : this()
        {
            _opc = opc;
        }

        private void Reportes05_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'logicopDataSet.redimiento_por_equipo' Puede moverla o quitarla según sea necesario.
            if (_opc==1)
            this.redimiento_por_equipoTableAdapter.Fill(this.logicopDataSet.redimiento_por_equipo);
            if (_opc == 2)
                this.redimiento_por_equipoTableAdapter.FillBy(this.logicopDataSet.redimiento_por_equipo, Operaciones.op_var.a, Operaciones.op_var.b);

            this.reportViewer1.RefreshReport();
        }
    }
}
