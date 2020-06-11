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

namespace smacop2.Movimientos
{
    public partial class frmrangosfle : Form
    {
        public frmrangosfle()
        {
            InitializeComponent();
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
                errorProvider1.SetError(textBox1, "El Costo es Obligatorio");
                o = false;
            }

            if (string.IsNullOrEmpty(this.numericUpDown1.Text) || this.numericUpDown1.Text == "0" || decimal.Parse(this.numericUpDown1.Text) <= decimal.Parse(this.numericUpDown2.Text))
            {
                errorProvider1.SetError(numericUpDown1, "Verifique el rango...");
                o = false;
            }

            if (string.IsNullOrEmpty(this.numericUpDown2.Text) || this.numericUpDown2.Text == "0" || decimal.Parse(this.numericUpDown2.Text) >= decimal.Parse(this.numericUpDown1.Text))
            {
                errorProvider1.SetError(numericUpDown2, "Verifique el rango...");
                o = false;
            }

            return o;
        }

        private string _id;
        public frmrangosfle(string id)
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

            rangofle c = new rangofle();
            //c.cod= _id;
            c.vini = Convert.ToDecimal(this.numericUpDown2.Text);
            c.vfin = Convert.ToDecimal(this.numericUpDown1.Text);
            c.precio =Convert.ToDecimal(this.textBox1.Text);
            int recibe = 0;
            //if (string.IsNullOrEmpty(_id))
                recibe = op_rangos.accion1(c, 1);
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
