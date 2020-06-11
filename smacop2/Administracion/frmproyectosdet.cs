using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using smacop2.Entity;
using smacop2.Operaciones;
using System.Runtime.InteropServices;
namespace smacop2.Administracion
{
    public partial class frmproyectosdet : Form
    {
        public frmproyectosdet()
        {
            InitializeComponent();
            this.textBox8.KeyDown+=new KeyEventHandler(textBox8_KeyDown);
            this.textBox11.KeyDown += new KeyEventHandler(textBox11_KeyDown);
            this.textBox13.KeyDown += new KeyEventHandler(textBox13_KeyDown);
            this.textBox11.MouseDoubleClick+=new MouseEventHandler(textBox11_MouseDoubleClick);
            this.textBox13.MouseDoubleClick += new MouseEventHandler(textBox13_MouseDoubleClick);
            this.textBox8.MouseDoubleClick += new MouseEventHandler(textBox8_MouseDoubleClick);
        }

        //private string _id;
        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        private static extern IntPtr GetSystemMenu(IntPtr hwnd, int revert);
        [DllImport("user32.dll", EntryPoint = "GetMenuItemCount")]
        private static extern int GetMenuItemCount(IntPtr hmenu);
        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        private static extern int RemoveMenu(IntPtr hmenu, int npos, int wflags);
        [DllImport("user32.dll", EntryPoint = "DrawMenuBar")]
        private static extern int DrawMenuBar(IntPtr hwnd);
        private const int MF_BYPOSITION = 0x0400;
        private const int MF_DISABLED = 0x0002;

        private string _a, _b;

        public frmproyectosdet(string a, string b):this()
        {
            _a = a;
            _b = b;

        }

        public void textBox11_MouseDoubleClick(object sender, EventArgs e)
        {
            frmlistatmp fr = new frmlistatmp(2);
            fr.ShowDialog();
        }
        public void textBox13_MouseDoubleClick(object sender, EventArgs e)
        {
            frmlistatmp fr = new frmlistatmp(3);
            fr.ShowDialog();
        }
        public void textBox8_MouseDoubleClick(object sender, EventArgs e)
        {
            frmlistatmp fr = new frmlistatmp(1);
            fr.ShowDialog();
        }
        public void textBox11_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F5)
            {
                frmlistatmp fr = new frmlistatmp(2);
                fr.ShowDialog();
            }

        }
        public void textBox8_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F5) {
                frmlistatmp fr = new frmlistatmp(1);
                fr.ShowDialog();
            }
                
        }

        public void textBox13_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F5)
            {
                frmlistatmp fr = new frmlistatmp(3);
                fr.ShowDialog();
            }

        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            
            frmlistatmp fr = new frmlistatmp();
            fr.ShowDialog();
        }

        private void frmproyectosdet_Load(object sender, EventArgs e)
        {

            IntPtr hmenu = GetSystemMenu(this.Handle, 0);
            int cnt = GetMenuItemCount(hmenu);
            // remove 'close' action
            RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            // remove extra menu line
            RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            DrawMenuBar(this.Handle);


            //comboBox1.DataSource = op_combos.FillCombo(@"SELECT cod, nom FROM dpto ORDER BY nom ASC");
            //comboBox1.DisplayMember = "nom";
            //comboBox1.ValueMember = "cod";
            //comboBox1.Text = null;

            comboBox3.DataSource = op_documentos.documentos1();
            comboBox3.DisplayMember = "nom";
            comboBox3.ValueMember = "cod";
            comboBox3.Text = null;

            if (!(string.IsNullOrEmpty(_a) && string.IsNullOrEmpty(_b)))
            {
                this.textBox8.Text = _a;
                this.textBox9.Text = _b;
                this.textBox8.Enabled = false;
            }
            if (!string.IsNullOrEmpty(_a) && string.IsNullOrEmpty(_b))
            {
               
                proyectos c = op_proyectos.GellIdproyectos(_a, 1);
                if (c != null)
                {
                    this.Text = "Editando Proyecto: " + c.idproy + "-" + c.nomproy;
                    this.comboBox3.SelectedValue = c.tipproy;

                    this.textBox1.Text = c.idproy;
                    this.textBox2.Text = c.nomproy;
                    this.textBox7.Text = c.desproy;
                    this.textBox8.Text = c.contproy;
                    this.textBox11.Text = c.dirproy;
                    this.textBox13.Text = c.respproy;
                    this.checkBox1.Checked = c.actproy;
                 
                    this.textBox10.Text= op_sql.parametro("select nomempl from empleados where codempl=@cod","@cod",c.dirproy);
                    this.textBox12.Text = op_sql.parametro("select nomempl from empleados where codempl=@cod", "@cod", c.respproy);
                    this.textBox9.Text = op_sql.parametro("select nomclie from clientes where codclie=@cod", "@cod", c.contproy);
                    toolStripButton5.Visible = true;
                    toolStripButton1.Visible = false;
                  
                    operacion();
                    toolStripButton3.Visible = false;
                    foreach (Control u in this.Controls)
                    {
                        if (u is Button)
                            u.Enabled = true;
                    }
                }
            }
            else
            {
                foreach (Control u in this.Controls)
                {
                    if (u is Button)
                        u.Enabled = false;
                }
                this.Text = "Nuevo Registro Proyecto";
                toolStripButton3.Visible = false;
                toolStripButton5.Visible = false;
                toolStripButton1.Visible = true;
                
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //this.label10.Text = "EDITANDO CLIENTE/CONTRATISTA...";
            //_id = null;
            toolStripButton3.Visible = false;
            toolStripButton5.Visible = false;
            toolStripButton1.Visible = true;
           
            foreach (Control i in this.Controls)
            {
                if (i is TextBox || i is ComboBox )
                {
                    i.Enabled = true;
                    i.Text = null;
                }


            }
            this.checkBox1.Checked = false;
            this.checkBox1.Enabled = true;
            this.textBox1.ReadOnly = true;
            textBox1.Focus();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //this.label10.Text = "EDITANDO CLIENTE/CONTRATISTA...";
            //_id = null;
            toolStripButton3.Visible = false;
            toolStripButton5.Visible = false;
            toolStripButton1.Visible = true;
           
            foreach (Control i in this.Controls)
            {
                if (i is TextBox || i is ComboBox || i is CheckBox)
                {
                    i.Enabled = true;
                    //i.Text = null;
                }


            }
            this.textBox1.ReadOnly = true;
            textBox1.Focus();
        }

        private bool validacion()
        {
            bool o = true;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(this.comboBox3.Text))
            {
                errorProvider1.SetError(comboBox3, "El Tipo de Proyecto es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "El ID del Proyecto es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "La Nombre es Obligatoria");
                o = false;
            }
           
            if (string.IsNullOrEmpty(this.textBox8.Text))
            {
                errorProvider1.SetError(textBox8, "El Contratista del Proyecto es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.textBox11.Text))
            {
                errorProvider1.SetError(textBox11, "El Director del Proyecto es Obligatorio");
                o = false;
            }
            if (string.IsNullOrEmpty(this.textBox13.Text))
            {
                errorProvider1.SetError(textBox13, "El Responsable del Proyecto es Obligatorio");
                o = false;
            }
            return o;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!validacion()) return;
            proyectos c = new proyectos();

            c.tipproy = this.comboBox3.SelectedValue.ToString();
            c.idproy = this.textBox1.Text;
            c.nomproy = this.textBox2.Text;
            c.desproy = this.textBox7.Text;
            c.contproy = this.textBox8.Text;
            c.dirproy = this.textBox11.Text;
            c.respproy = this.textBox13.Text;
            c.actproy = this.checkBox1.Checked;
           


            //this.label10.Text = "CLIENTE/CONTRATISTA: " + c.NombreCompleto;

            int rowsAffected = 0;
            if (string.IsNullOrEmpty(_a))
            {
                if (this.checkBox1.Checked == false)
                {
                    int a = Convert.ToInt32(MessageBox.Show("El Proyecto está inactivo, por lo tanto no podrá operar con este" + Environment.NewLine +"¿Desea activarlo?", Application.ProductName.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Information));
                    if (a == 6)
                    {
                        c.actproy = true;
                        this.checkBox1.Checked = true;
                    }
                }
                rowsAffected = op_proyectos.accion(c, 1);
            }
            else
            {
                this.textBox1.ReadOnly = true;
                rowsAffected = op_proyectos.accion(c, 2);
            }
            if (rowsAffected > 0)
            {
                if (rowsAffected == 1)
                {
                    MessageBox.Show(mensajes.MsjProc1, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    operacion();
                    op_var.boton1 = true;
                    op_var.boton2 = true;
                    foreach (Control u in this.Controls)
                    {
                        if (u is Button)
                            u.Enabled = true;
                    }
                    this.toolStripButton3.Visible = true;
                }
                if (rowsAffected == 2)
                {
                    MessageBox.Show(mensajes.MsjProc2, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    operacion();
                    foreach (Control u in this.Controls)
                    {
                        if (u is Button)
                            u.Enabled = true;
                    }
                    this.toolStripButton3.Visible = true;
                }
                if (rowsAffected == 3)
                {
                    MessageBox.Show(mensajes.MsjProc3, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    operacion();
                    foreach (Control u in this.Controls)
                    {
                        if (u is Button)
                            u.Enabled = true;
                    }
                    this.toolStripButton3.Visible = true;
                }
                if (rowsAffected == 4)
                {
                    if (MessageBox.Show(mensajes.MsjProc4 + " " + mensajes.MsjProc5, Application.ProductName.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        rowsAffected = op_proyectos.accion(c, 2);
                    }

                }
            }
            else
            {
                MessageBox.Show(mensajes.MsjProc0, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }
        private void operacion()
        {
            foreach (Control e in this.Controls)
            {
                if (e is TextBox || e is ComboBox || e is CheckBox)
                    e.Enabled = false;
            }


            this.toolStripButton3.Visible = true;
            this.toolStripButton1.Visible = false;
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
           
        }

        private void contratistasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmlistatmp fr = new frmlistatmp(1);
            fr.ShowDialog();
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmlistatmp fr = new frmlistatmp(2);
            fr.ShowDialog();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox1.Focus();
        }

        private void activated(object sender, EventArgs e)
        {
            switch (op_var.opc)
            {
                case 1:
                    //frmproyectosdet frm1 = (frmproyectosdet)this.ActiveMdiChild;
                    textBox8.Text = op_var.cod;
                   textBox9.Text = op_var.nom;
                    break;
                case 2:
                    //frmproyectosdet frm2 = (frmproyectosdet)this.ActiveMdiChild;
                   textBox11.Text = op_var.cod;
                    textBox10.Text = op_var.nom;
                    break;
                case 3:
                    //frmproyectosdet frm3 = (frmproyectosdet)this.ActiveMdiChild;
                   textBox13.Text = op_var.cod;
                    textBox12.Text = op_var.nom;
                    break;
            }
        }

        
    }
}
