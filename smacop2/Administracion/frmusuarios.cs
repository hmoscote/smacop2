using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using smacop2.Operaciones;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace smacop2.Administracion
{
    public partial class frmusuarios : Form
    {
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
        public frmusuarios()
        {
            InitializeComponent();
        
        }
     
        private void button2_Click(object sender, EventArgs e)
        {
           
            Application.Exit();
        }
        //private bool validacion()
        //{
        //    bool o = true;
        //    errorProvider1.Clear();
        //    if (string.IsNullOrEmpty(this.textBox1.Text))
        //    {
        //        errorProvider1.SetError(textBox1, "El usuario debe ser digitado");
        //        o = false;
        //    }
        //    //if (string.IsNullOrEmpty(this.textBox1.Text))
        //    //{
        //    //    errorProvider1.SetError(textBox2, "La contraseña debe ser digitada");
        //    //    o = false;
        //    //}
        //    return o;
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            //if (!validacion()) return;

            //bool p = Operaciones.op_sql.comprobar("select usu, AES_DECRYPT(con,'84089809011053') as con from usuarios where usu='" + this.textBox1.Text + "' and AES_DECRYPT(con,'84089809011053')='" + this.textBox2.Text.Trim() + "';");
        //    if (p == true)
        //    {


        //        frmmenu frm = new frmmenu();
        //        //frm.label6.Text = "MODULO DE " + comboBox1.Text;
        //        Operaciones.op_var.usu = this.textBox1.Text;
        //        Operaciones.op_sql.parametro1(" insert into logaccesos values (null, now(),concat('" + Operaciones.op_var.usu + "',' ingresa a la aplicación')); ");
        //       //if(Operaciones.op_sql.comprobar("select * from configuracion")==false);
        //       // Operaciones.op_sql.parametro1(" update configuracion set fecha=CURDATE();");
        //        this.Hide();
        //        Operaciones.op_var.forma = true;
        //        frm.Show();
        //        //menuStrip1.Enabled = true;
        //        //this.toolStripButton5.Visible = false;
        //        //this.toolStripButton4.Enabled = true;
        //        //this.toolStripButton3.Enabled = true;
        //        //this.toolStripButton2.Enabled = true;
        //        //this.toolStripComboBox1.Enabled = false;
        //        //this.textBox1.Enabled = false;
        //        //CLS.operaciones_SQL.sentencia("INSERT into logsman (usuario, fechent, accion)VALUES ('" + this.toolStripComboBox1.Text + "', current_date(),CONCAT('Ingreso a la aplicación ','" + DateTime.Now.ToShortTimeString() + "'));", "El Usuario autentificado", 0);
        //        //CLS.varent.emp = this.toolStripComboBox1.Text;
        //        //CLS.varent.user = this.toolStripComboBox1.Text;
        //        //this.toolStripStatusLabel1.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
        //        //CreaAutoHidePanels();

        //        //MessageBox.Show("El Usuario autentificado", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    else
        //        MessageBox.Show("El Usuario y/o contraseña son incorrecta", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

          
           
        

        }

        private void frmusuarios_Load(object sender, EventArgs e)
        {
            IntPtr hmenu = GetSystemMenu(this.Handle, 0);
            int cnt = GetMenuItemCount(hmenu);
            // remove 'close' action
            RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            // remove extra menu line
            RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            DrawMenuBar(this.Handle);

            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = Operaciones.op_usuarios.GellAllusu();

      
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
      
        }

        private void celda_doble_clic(object sender, DataGridViewCellMouseEventArgs e)
        {
             DataGridViewRow row = dataGridView3.CurrentRow as DataGridViewRow;
             dataGridView1.AutoGenerateColumns = false;
             dataGridView1.DataSource = Operaciones.op_combos.FillCombo3(@"SELECT
                                                                                menu_modulo.descripcion as nom,
                                                                                menu_modulo.descripcion2 as ndto1,
                                                                                menu_modulo.cod
                                                                           FROM
                                                                                definicion_menu_modulo
                                                                           INNER JOIN modulo_usuario ON modulo_usuario.modulo = definicion_menu_modulo.codmod
                                                                           INNER JOIN menu_modulo ON menu_modulo.cod = definicion_menu_modulo.codmen 
                                                                           WHERE modulo_usuario.usuario='"+usuarios+"' AND modulo_usuario.modulo='"+row.Cells[0].Value.ToString()+"'");
             modulo = row.Cells[0].Value.ToString();
             if (row.Cells[2].Value.ToString() == "ADMINISTRADOR")
             {
                 for (int i = 0; i <= this.dataGridView1.Rows.Count - 1; i++)
                     dataGridView1.Rows[i].Cells[3].Value = true;
                 dataGridView1.ReadOnly = true;
             }
             else
             {
                 string sql = @"SELECT * FROM OPCIONES_USUARIO WHERE MODULO='" + modulo + "' AND USUARIO='" + usuarios + "'";
                 
                 using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                 {
                     conn.Open();
                     MySqlCommand cmd = new MySqlCommand(sql, conn);
                     MySqlDataReader reader = cmd.ExecuteReader();
                     if (reader.HasRows == true)
                     {
                         while (reader.Read())
                             for (int k = 2; k <= dataGridView1.Rows.Count; k++)
                                dataGridView1.Rows[k-2].Cells[3].Value=Convert.ToBoolean(reader[k].ToString());
                     }
                 }
                           this.dataGridViewCheckBoxColumn1.ReadOnly = false;
             }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            Administracion.frmregusuario f = new Administracion.frmregusuario(1);
            f.ShowDialog();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
//            this.dataGridView3.DataSource = Operaciones.op_combos.FillCombo(@"SELECT
//                                                                                    modulo_usuario.modulo,
//                                                                                    modulo.nombre
//                                                                              FROM
//                                                                                    modulo_usuario
//                                                                              INNER JOIN modulo ON modulo.codigo = modulo_usuario.modulo
//                                                                              WHERE modulo_usuario.usuario='"+this.comboBox3.Text+"'");
//            comboBox4.DisplayMember = "nom";
//            comboBox4.ValueMember = "cod";
//            comboBox4.Text = null;
        }
        private string usuarios, modulo;
        private void cell_mouse_clic(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow row = dataGridView2.CurrentRow as DataGridViewRow;
                dataGridView3.AutoGenerateColumns = false;
                dataGridView3.DataSource = Operaciones.op_combos.FillCombo2(@"SELECT
                                                                                    modulo_usuario.modulo as cod,
                                                                                    modulo.nombre as nom,
                                                                                    modulo_usuario.acceso as ndto
                                                                            FROM
                                                                            modulo_usuario
                                                                            INNER JOIN modulo ON modulo.codigo = modulo_usuario.modulo
                                                                            WHERE modulo_usuario.usuario='" + row.Cells[0].Value.ToString() + "'");
              
                usuarios = row.Cells[0].Value.ToString();
                dataGridView1.DataSource = null;
               
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = Operaciones.op_usuarios.GellAllusu();
        }

        private void cell_mouse_doble_clic(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = dataGridView2.CurrentRow as DataGridViewRow;
            frmdefusuario f = new frmdefusuario(row.Cells[0].Value.ToString(), Convert.ToBoolean(row.Cells[1].Value));
            f.ShowDialog();
        }

       
        private void button1_Click_3(object sender, EventArgs e)
        {

             string[] a = {   "UPDATE OPCIONES_USUARIO SET o1",
                              "UPDATE OPCIONES_USUARIO SET o2",
                              "UPDATE OPCIONES_USUARIO SET o3",
                              "UPDATE OPCIONES_USUARIO SET o4",
                              "UPDATE OPCIONES_USUARIO SET o5",
                              "UPDATE OPCIONES_USUARIO SET o6",
                              "UPDATE OPCIONES_USUARIO SET o7",
                              "UPDATE OPCIONES_USUARIO SET o8",
                              "UPDATE OPCIONES_USUARIO SET o9",
                              "UPDATE OPCIONES_USUARIO SET o10",
                              "UPDATE OPCIONES_USUARIO SET o11",
                              "UPDATE OPCIONES_USUARIO SET o12",
                              "UPDATE OPCIONES_USUARIO SET o13",
                              "UPDATE OPCIONES_USUARIO SET o14",
                              "UPDATE OPCIONES_USUARIO SET o15",
                              "UPDATE OPCIONES_USUARIO SET o16",
                              "UPDATE OPCIONES_USUARIO SET o18",
                              "UPDATE OPCIONES_USUARIO SET o19",
                              "UPDATE OPCIONES_USUARIO SET o20"};
                for (int i = 0; i <= this.dataGridView1.Rows.Count - 1; i++)
                   Operaciones.op_sql.parametro1(@a[i]+"="+ Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value) + " WHERE MODULO='"+modulo+"' AND USUARIO='"+usuarios+"'");
               
        }

       
    }
}
