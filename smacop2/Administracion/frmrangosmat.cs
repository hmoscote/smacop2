using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using smacop2.Entity;
using smacop2.Operaciones;
using System.Runtime.InteropServices;

namespace smacop2.Administracion
{
    public partial class frmrangosmat : Form
    {
        public frmrangosmat()
        {
            InitializeComponent();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool validacion()
        {
            bool o = true;
            errorProvider1.Clear();


            if (string.IsNullOrEmpty(this.textBox1.Text) || this.numericUpDown1.Text == "0")
            {
                errorProvider1.SetError(textBox1, "El precio es Obligatorio");
                o = false;
            }

            if (string.IsNullOrEmpty(this.numericUpDown1.Text) || this.numericUpDown1.Text == "0" || int.Parse(this.numericUpDown1.Text) <= int.Parse(this.numericUpDown2.Text))
            {
                errorProvider1.SetError(numericUpDown1, "Verifique el rango...");
                o = false;
            }

            if (string.IsNullOrEmpty(this.numericUpDown2.Text) || this.numericUpDown2.Text == "0" || int.Parse(this.numericUpDown2.Text) >= int.Parse(this.numericUpDown1.Text))
            {
                errorProvider1.SetError(numericUpDown2, "Verifique el rango...");
                o = false;
            }

          

            return o;
        }
         private string _id;
        public frmrangosmat(string id)
            : this()
        {
            _id = id;
           
        }

        private void operacion()
        {
           
            foreach (Control e in this.groupBox1.Controls)
            {
                if (e is TextBox || e is ComboBox || e is Button || e is CheckBox || e is NumericUpDown)
                    e.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!validacion()) return;

            rango c = new rango();
            c.mat = _id;
            c.rini = Convert.ToInt32(this.numericUpDown2.Text);
            c.rfin = Convert.ToInt32(this.numericUpDown1.Text);
            c.precio =Convert.ToDecimal(this.textBox1.Text);
            int recibe = 0;
            //if (string.IsNullOrEmpty(_id))
                recibe = op_rangos.accion(c, 1);
            //else
            //{
            //    this.textBox1.ReadOnly = true;
            //    recibe = op_rangos.accion(c, 2);
            //}

            if (recibe > 0)
            {
                MessageBox.Show("Registro Grabado");
                operacion();
                button1.Enabled = false;
               
                //this.label10.Text = "EMPLEADO: " + c.NombreCompleto;
            }
        }
    }
}
