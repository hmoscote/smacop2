using System;
using System.Windows.Forms;
using smacop2.Operaciones;

namespace smacop2
{
    public partial class frmprueba : Form
    {
        public frmprueba()
        {
            InitializeComponent();
        }

        private void frmprueba_Load(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource=op_combos.FillCombo("select cod, nom from proveedores order by 2");
            this.bindingNavigator1.BindingSource =this.bindingSource1 ;
            this.textBox1.DataBindings.Add("Text",bindingSource1,"cod");
            this.textBox2.DataBindings.Add("Text", bindingSource1, "nom");

            this.bindingSource2.DataSource = op_combos.FillCombo("select cod, nom from mpio order by 2");
            comboBox1.DisplayMember = "nom";
            comboBox1.ValueMember = "cod";
            comboBox1.DataSource = bindingSource2;
            this.comboBox1.DataBindings.Add("SelectedValue", bindingSource1, "ciud");
            //comboBox3.Text = null;
            //comboBox1.SelectedValue
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            bindingNavigatorAddNewItem.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(comboBox1.SelectedValue.ToString());
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }
    }
}
