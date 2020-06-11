using System;
using System.Windows.Forms;
using smacop2.Operaciones;

namespace smacop2.Administracion
{
    public partial class frmcambiocod : Form
    {
        public frmcambiocod()
        {
            InitializeComponent();
        }
        bool existe = false;
        private void button1_Click(object sender, EventArgs e)
        {
            //CLIENTES/CONTRATISTAS
            //EMPLEADOS
            //CANTERAS
            //CENTRO DE COSTOS
            //RECIBO DE SALIDAS
            //RECIBO DE ENTRADAS
            //EQUIPOS EN RECIBO DE SALIDAS
            //EQUIPOS EN RECIBO DE ENTRADAS
           
            if (string.IsNullOrEmpty(this.comboBox1.Text))
            {
                errorProvider1.SetError(comboBox1, "Seleccione el Objeto a diligenciarse");
                return;
            }
            else errorProvider1.SetError(comboBox1, null);
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Digite el Código a diligenciarse");
                return;
            }
            else errorProvider1.SetError(textBox1, null);

            switch(comboBox1.SelectedIndex)
            {
                case 0:
                    existe = op_sql.comprobar("select codclie from clientes where codclie='"+this.textBox1.Text.Trim()+"'");
                    break;
                case 1:
                    existe = op_sql.comprobar("select codempl from empleados where codempl='" + this.textBox1.Text.Trim() + "'");
                    break;
            
                case 2:
                    existe = op_sql.comprobar("select cod from proveedores where cod='" + this.textBox1.Text.Trim() + "'");
                    break;

                case 3:
                    existe = op_sql.comprobar("select idproy from proyectos where idproy='" + this.textBox1.Text.Trim() + "'");
                    break;

                case 4:
                    existe = op_sql.comprobar("select nrec from salidasmat where nrec='" + this.textBox1.Text.Trim() + "'");
                    break;

                case 5:
                    existe = op_sql.comprobar("select nrec from entradasmat where nrec='" + this.textBox1.Text.Trim() + "'");
                    break;

            }


            if (existe)
            {
                errorProvider1.SetError(comboBox1, null);
                errorProvider1.SetError(textBox1, null);
                this.button1.SendToBack();
                this.textBox2.Visible = true;
                this.button2.Visible = true;
                this.label4.Visible = true;
                this.comboBox1.Enabled = false;
            }
            else
            {
                errorProvider1.SetError(textBox1, "No existe el código: " + this.textBox1.Text);
                this.button1.BringToFront();
                this.textBox2.Visible = false;
                this.button2.Visible = false;
                this.label4.Visible = false;
                this.comboBox1.Enabled = true;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            errorProvider1.SetError(comboBox1, null);
            errorProvider1.SetError(textBox1, null);
            this.button1.BringToFront();
            this.textBox2.Visible = false;
            this.button2.Visible = false;
            this.label4.Visible = false;
            this.textBox1.Text = null;
            this.comboBox1.Enabled = true;

            this.comboBox1.Text = null;
            //this.textBox3.Text = null;
            this.textBox2.Text = null;
            this.textBox1.Text = null;
            //this.textBox4.Text = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox2.Text))
                errorProvider1.SetError(textBox2, "Digite el Código a diligenciarse");
            else
            {
                errorProvider1.SetError(textBox2, null);
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        op_sql.parametro2("update clientes set codclie ='" + this.textBox2.Text.Trim() + "' where codclie='" + this.textBox1.Text.Trim() + "'");
                        break;
                    case 1:
                        op_sql.parametro2("update empleados set codempl ='" + this.textBox2.Text.Trim() + "' where codempl='" + this.textBox1.Text.Trim() + "'");
                        break;
                    case 2:
                        op_sql.parametro2("update proveedores set cod ='" + this.textBox2.Text.Trim() + "' where cod='" + this.textBox1.Text.Trim() + "'");
                        break;
                    case 3:
                        op_sql.parametro2("update proyectos set idproy ='" + this.textBox2.Text.Trim() + "' where idproy='" + this.textBox1.Text.Trim() + "'");
                        break;
                    case 4:
                        op_sql.parametro2("update salidasmat set nrec ='" + this.textBox2.Text.Trim() + "' where nrec='" + this.textBox1.Text.Trim() + "'");
                        break;
                    case 5:
                        op_sql.parametro2("update entradasmat set nrec ='" + this.textBox2.Text.Trim() + "' where nrec='" + this.textBox1.Text.Trim() + "'");
                        break;
                }
            }
        }

        //private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    switch (comboBox1.SelectedIndex)
        //    {
        //        case 8:
        //        case 9:
        //            this.textBox3.Visible = true;
        //            break;
        //    }
        //}

        //private void button5_Click(object sender, EventArgs e)
        //{
        //    panel2.Visible = true;
        //}

        //private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (comboBox2.Text == "EQUIPOS EN RECIBO DE ENTRADAS")
        //    {
        //        comboBox6.DataSource = op_combos.FillCombo(@"SELECT DISTINCT entradasmat.equ as cod, entradasmat.equ as nom FROM `entradasmat` order by 1");
        //        comboBox6.DisplayMember = "cod";
        //        comboBox6.ValueMember = "nom";
        //        comboBox6.Text = null;
        //    }

        //    if (comboBox2.Text == "EQUIPOS EN RECIBO DE SALIDAS")
        //    {
        //        comboBox6.DataSource = op_combos.FillCombo(@"SELECT DISTINCT salidasmat.equ as cod, salidasmat.equ as nom FROM `salidasmat` order by 1");
        //        comboBox6.DisplayMember = "cod";
        //        comboBox6.ValueMember = "nom";
        //        comboBox6.Text = null;
        //    }
        //}

        private void frmcambiocod_Load(object sender, EventArgs e)
        {
            


        }
    }
}
