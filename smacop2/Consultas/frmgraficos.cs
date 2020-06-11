using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace smacop2.Consultas
{
    public partial class frmgraficos : Form
    {
        public frmgraficos()
        {
            InitializeComponent();
        }
        string _a;
        int _o;
        public frmgraficos(string a, int o):this()
        {
            _a = a;
            _o = o;
        }

        private void frmgraficos_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'logicopDataSet1.lstprov_salidas' Puede moverla o quitarla según sea necesario.
            this.lstprov_salidasTableAdapter.Fill(this.logicopDataSet1.lstprov_salidas);
            // TODO: esta línea de código carga datos en la tabla 'logicopDataSet1.lstsalidas_x_ccos' Puede moverla o quitarla según sea necesario.
          
            // TODO: esta línea de código carga datos en la tabla 'logicopDataSet1.lstentradas_x_canteras' Puede moverla o quitarla según sea necesario.
            //this.lstentradas_x_canterasTableAdapter1.Fill(this.logicopDataSet1.lstentradas_x_canteras);
            if (_o == 1)
                this.lstentradas_x_canterasTableAdapter1.FillBy(this.logicopDataSet1.lstentradas_x_canteras,_a);
            else if (_o == 2)
            {
                chart2.BringToFront();
                this.lstsalidas_x_ccosTableAdapter.FillBy(this.logicopDataSet1.lstsalidas_x_ccos, _a);
            }
            else if (_o == 3)
            {
                chart3.BringToFront();
                this.lstprov_salidasTableAdapter.FillBy(this.logicopDataSet1.lstprov_salidas,_a);
            }
            else
            {
                chart2.BringToFront();
                this.lstsalidas_x_ccosTableAdapter.FillBy(this.logicopDataSet1.lstsalidas_x_ccos, _a);

            }
            this.label5.Text = _a;
        }

    }
}
