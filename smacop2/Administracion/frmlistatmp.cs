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
using smacop2.Operaciones;

namespace smacop2.Administracion
{
    public partial class frmlistatmp : Form
    {
        public frmlistatmp()
        {
            InitializeComponent();
            toolStripTextBox1.TextChanged+=new EventHandler(toolStripTextBox1_TextChanged);
            this.listView1.MouseDoubleClick+=new MouseEventHandler(listView1_MouseDoubleClick);
        }
        int  _i;
        public frmlistatmp(int i): this()
        {
            _i = i;
        }
        private void listView1_MouseDoubleClick(object sender, EventArgs e)
        {
            int i = listView1.SelectedIndices[0];
            op_var.cod = this.listView1.Items[i].Text;
            op_var.nom = this.listView1.Items[i].SubItems[1].Text;
            op_var.opc = _i;
            this.Close();
        }
        private void frmlistatmp_Load(object sender, EventArgs e)
        {
            if (_i == 1 || _i == 6 || _i == 8)
                lista(@"SELECT  codclie, nomclie FROM clientes ORDER BY nomclie ASC", "codclie", "nomclie");
            if (_i == 2 || _i==3 || _i==5 || _i==7)
                lista(@"SELECT  codempl, nomempl FROM empleados where activo=1 ORDER BY nomempl ASC", "codempl","nomempl");
            if (_i == 4 || _i == 9)
                  lista(@"SELECT p.`cod`, p.`nom` FROM logicop.proveedores p ORDER BY p.`nom` ASC", "cod", "nom");
            if (_i == 10)
            {
               
                    lista(@"SELECT
                                    salidasmat.ccos,
                                    proyectos.nomproy
                                    FROM
                                    salidasmat
                                    INNER JOIN proyectos ON proyectos.idproy = salidasmat.ccos
                                    GROUP BY
                                    salidasmat.ccos,
                                    proyectos.nomproy
                                    ORDER BY
                                    proyectos.nomproy ASC", "ccos", "nomproy");
                    //lista(@"SELECT cod, nom FROM proveedores WHERE COD LIKE '" + this.toolStripTextBox1.Text + "%' OR nom LIKE '" + this.toolStripTextBox1.Text + "%' ORDER BY nom ASC", "cod", "nom");
                    toolStripStatusLabel2.Text = listView1.Items.Count.ToString();
                
            }
             
        }
        private void lista(string sql, string c1 , string c2)
        {
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                this.listView1.Items.Clear();
                if (reader.HasRows == true)
                {
                    int i=0;
                    while (reader.Read())
                    {
                        this.listView1.Items.Add(reader[c1].ToString());
                        this.listView1.Items[i].SubItems.Add(reader[c2].ToString());
                        i++;
                    }
                }
            }
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (_i == 1 || _i == 6 || _i == 8)
            {
                if (!(string.IsNullOrEmpty(toolStripTextBox1.Text)) && this.listView1.Items.Count > 0)
                {
                    lista(@"SELECT codclie, nomclie FROM clientes WHERE CODCLIE LIKE '" + this.toolStripTextBox1.Text + "%' OR nomCLIE LIKE '" + this.toolStripTextBox1.Text + "%' ORDER BY nomclie ASC", "codclie", "nomclie");
                    toolStripStatusLabel2.Text = listView1.Items.Count.ToString();
                }
            }
            if (_i == 2 || _i == 3 || _i == 5 || _i == 7)
            {
                if (!(string.IsNullOrEmpty(toolStripTextBox1.Text)) && this.listView1.Items.Count > 0)
                {
                    lista(@"SELECT codempl, nomempl FROM empleados WHERE CODempl LIKE '" + this.toolStripTextBox1.Text + "%' OR nomempl LIKE '" + this.toolStripTextBox1.Text + "%' and activo=1 ORDER BY nomempl ASC", "codempl", "nomempl");
                    toolStripStatusLabel2.Text = listView1.Items.Count.ToString();
                }
            }
            if (_i == 4 || _i == 9)
            {
                if (!(string.IsNullOrEmpty(toolStripTextBox1.Text)) && this.listView1.Items.Count > 0)
                {
                    
                    lista(@"SELECT cod, nom FROM proveedores WHERE COD LIKE '" + this.toolStripTextBox1.Text + "%' OR nom LIKE '" + this.toolStripTextBox1.Text + "%' ORDER BY nom ASC", "cod", "nom");
                    toolStripStatusLabel2.Text = listView1.Items.Count.ToString();
                }
            }
            if (_i == 10)
            {
                if (!(string.IsNullOrEmpty(toolStripTextBox1.Text)) && this.listView1.Items.Count > 0)
                {
                    lista(@"SELECT
                            salidasmat.ccos as cod,
                            proyectos.nomproy as nom
                            FROM
                            salidasmat
                            INNER JOIN proyectos ON proyectos.idproy = salidasmat.ccos
                            WHERE salidasmat.ccos LIKE '" + this.toolStripTextBox1.Text + "%' OR proyectos.nomproy LIKE '" + this.toolStripTextBox1.Text +
                             "%' ORDER BY nom ASC GROUP BY salidasmat.ccos, proyectos.nomproy", "cod", "nom");               //lista(@"SELECT cod, nom FROM proveedores WHERE COD LIKE '" + this.toolStripTextBox1.Text + "%' OR nom LIKE '" + this.toolStripTextBox1.Text + "%' ORDER BY nom ASC", "cod", "nom");
                    toolStripStatusLabel2.Text = listView1.Items.Count.ToString();
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //lista(@"SELECT  tcodclie, codclie FROM clientes ORDER BY nomclie ASC");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (_i == 1 || _i == 6 || _i == 8)
                lista(@"SELECT  codclie, nomclie FROM clientes ORDER BY nomclie ASC", "codclie", "nomclie");
            if (_i == 2 || _i == 3 || _i == 5 || _i == 7)
                lista(@"SELECT  codempl, nomempl FROM empleados where activo=1 ORDER BY nomempl ASC", "codempl", "nomempl");
            if (_i == 4 || _i == 9)
                lista(@"SELECT p.`cod`, p.`nom` FROM logicop.proveedores p ORDER BY p.`nom` ASC", "cod", "nom");
            if (_i == 10)
            {
                if (!(string.IsNullOrEmpty(toolStripTextBox1.Text)) && this.listView1.Items.Count > 0)
                {
                    lista(@"SELECT
                            salidasmat.ccos as cod,
                            proyectos.nomproy as nom
                            FROM
                            salidasmat
                            INNER JOIN proyectos ON proyectos.idproy = salidasmat.ccos
                            WHERE salidasmat.ccos LIKE '" + this.toolStripTextBox1.Text + "%' OR proyectos.nomproy LIKE '" + this.toolStripTextBox1.Text +
                             "%' ORDER BY nom ASC GROUP BY salidasmat.ccos, proyectos.nomproy", "cod", "nom");                    //lista(@"SELECT cod, nom FROM proveedores WHERE COD LIKE '" + this.toolStripTextBox1.Text + "%' OR nom LIKE '" + this.toolStripTextBox1.Text + "%' ORDER BY nom ASC", "cod", "nom");
                    toolStripStatusLabel2.Text = listView1.Items.Count.ToString();
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (_i == 1 || _i == 6 || _i == 8)
            {
                Administracion.frmclientesdet f = new frmclientesdet();
                f.ShowDialog();
               
            }
            if (_i == 2 || _i == 3 || _i == 5 || _i == 7)
            {
                Administracion.frmempleadosdet f = new Administracion.frmempleadosdet();
                f.ShowDialog();

            }

            if (_i == 4 || _i == 9)
            {
                Administracion.frmprovedoresdet f = new Administracion.frmprovedoresdet();
                f.ShowDialog();
            }
            if (_i == 10)
            {
                Administracion.frmproyectosdet f = new frmproyectosdet();
                f.ShowDialog();
            }
        }
    }
}
