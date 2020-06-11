using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using smacop2.Entity;
using smacop2.Administracion;
using smacop2.Operaciones;
using AutoHidePanels;

using System.Reflection;
using System.Runtime.InteropServices;


namespace smacop2
{
    public partial class frmmenu : Form
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

        public frmmenu()
        {
            InitializeComponent();
            this.Activated += new EventHandler(frmmenu_Activated);
          
            this.FormClosing += new FormClosingEventHandler(frmmenu_FormClosing);
            this.treeView1.NodeMouseDoubleClick+=new TreeNodeMouseClickEventHandler(treeView1_NodeMouseDoubleClick);
           //axTaskPanel1.ItemClick+=new AxXtremeTaskPanel._DTaskPanelEvents_ItemClickEventHandler(axTaskPanel1_ItemClick);
            
        }

        private bool a;

        public frmmenu(bool aa):this()
        {
            a = aa;

        }

        

        private void frmmenu_FormClosing(object sender, EventArgs e)
        {
            
            Operaciones.op_sql.parametro1(" insert into logaccesos values (null, now(),concat('" + Operaciones.op_var.usu + "',' sale de la aplicación')); ");
        }


        private void frmmenu_Activated(object sender, EventArgs e)
        {
            //if (this.axTaskPanel1.Visible == false)
            //{
            //    if (op_var.forma == true)
            //    {
            //        this.axTaskPanel1.Visible = true;
            //        op_var.forma = false;
            //    }
            //}
            //if (op_var.opc == 1 || op_var.opc == 2 || op_var.opc == 3)
            //{
               
                //if (frm1 != null)
                //{

                    //datos.con = 0;


            //switch (op_var.opc)
            //{
            //    case 1:
            //        frmproyectosdet frm1 = (frmproyectosdet)this.ActiveMdiChild;
            //        frm1.textBox8.Text = op_var.cod;
            //        frm1.textBox9.Text = op_var.nom;
            //        break;
            //    case 2:
            //        frmproyectosdet frm2 = (frmproyectosdet)this.ActiveMdiChild;
            //        frm2.textBox11.Text = op_var.cod;
            //        frm2.textBox10.Text = op_var.nom;
            //        break;
            //    case 3:
            //        frmproyectosdet frm3 = (frmproyectosdet)this.ActiveMdiChild;
            //        frm3.textBox13.Text = op_var.cod;
            //        frm3.textBox12.Text = op_var.nom;
            //        break;
            //    case 4:
            //        Movimientos.frmmovimientosdetEXT frm4 = (Movimientos.frmmovimientosdetEXT)this.ActiveMdiChild;
            //        //frm4.textBox8.Text = op_var.cod;
            //        //frm4.textBox9.Text = op_var.nom;
            //        break;
            //    case 5:
            //        Movimientos.frmmovimientosdetEXT frm5 = (Movimientos.frmmovimientosdetEXT)this.ActiveMdiChild;
            //        frm5.textBox14.Text = op_var.cod;
            //        frm5.textBox13.Text = op_var.nom;
            //        break;
            //    case 6:
            //        Movimientos.frmmovimientosdetINT frm6 = (Movimientos.frmmovimientosdetINT)this.ActiveMdiChild;
            //        frm6.textBox8.Text = op_var.cod;
            //        frm6.textBox9.Text = op_var.nom;
            //        break;
            //    case 7:
            //        Movimientos.frmmovimientosdetINT frm7 = (Movimientos.frmmovimientosdetINT)this.ActiveMdiChild;
            //        frm7.textBox14.Text = op_var.cod;
            //        frm7.textBox13.Text = op_var.nom;
            //        break;
            //    case 8:
            //    case 9:
            //    case 10:
            //        Consultas.frmfacturacion frm8 = (Consultas.frmfacturacion)this.ActiveMdiChild;
            //        frm8.textBox2.Text = op_var.cod;
            //        frm8.textBox1.Text = op_var.nom;
            //        frm8.toolStripButton4.Visible = false;
            //        break;
            //}
                


                //frm1 = null;
                op_var.cod = null;
                op_var.nom = null;
                op_var.opc = 0;
            //}

            //if (op_var.opc == 4 )
            //{
                
            //    switch (op_var.opc)
            //    {
                    
                   
            //    }
            //    frm1 = null;
            //    op_var.cod = null;
            //    op_var.nom = null;
            //    op_var.opc = 0;
            //}
           
        }
            

        

      

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmcargosdet frm = new frmcargosdet();
            frm.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmempleadosdet frm = new frmempleadosdet();
            frm.ShowDialog();
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        

            //try
            //{
            //    frmmenu frmp = this;
            //    Administracion.frmempleados fr = new Administracion.frmempleados();
            //    fr.WindowState = FormWindowState.Normal;

            //    foreach (Form f in frmp.MdiChildren)
            //    {
            //        if (f.Name == fr.Name)
            //        {
            //            f.Select();
            //            f.WindowState = FormWindowState.Normal;
            //            f.BringToFront();
            //            return;
            //        }
            //    }


            //    fr.MdiParent = this;
            //    fr.BringToFront();
            //    fr.Show();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        //private void cargosToolStripMenuItem1_Click(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        frmmenu frmp = this;
        //        frmcargos fr = new frmcargos();
        //        fr.WindowState = FormWindowState.Normal;

        //        foreach (Form f in frmp.MdiChildren)
        //        {
        //            if (f.Name == fr.Name)
        //            {
        //                f.Select();
        //                f.WindowState = FormWindowState.Normal;
        //                f.BringToFront();
        //                return;
        //            }
        //        }


        //        fr.MdiParent = this;
        //        fr.BringToFront();
        //        fr.Show();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void empleadosToolStripMenuItem2_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        frmmenu frmp = this;
        //        Administracion.frmempleados fr = new Administracion.frmempleados();
        //        fr.WindowState = FormWindowState.Normal;

        //        foreach (Form f in frmp.MdiChildren)
        //        {
        //            if (f.Name == fr.Name)
        //            {
        //                f.Select();
        //                f.WindowState = FormWindowState.Normal;
        //                f.BringToFront();
        //                return;
        //            }
        //        }


        //        fr.MdiParent = this;
        //        fr.BringToFront();
        //        fr.Show();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void proveedoresToolStripMenuItem2_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        frmmenu frmp = this;
        //        frmproveedores fr = new frmproveedores();
        //        fr.WindowState = FormWindowState.Normal;

        //        foreach (Form f in frmp.MdiChildren)
        //        {
        //            if (f.Name == fr.Name)
        //            {
        //                f.Select();
        //                f.WindowState = FormWindowState.Normal;
        //                f.BringToFront();
        //                return;
        //            }
        //        }


        //        fr.MdiParent = this;
        //        fr.BringToFront();
        //        fr.Show();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    //frmclientesdet frm = new frmclientesdet();
        //    //frm.ShowDialog();
        //    try
        //    {
        //        frmmenu frmp = this;
        //        frmclientes fr = new frmclientes();
        //        fr.WindowState = FormWindowState.Normal;

        //        foreach (Form f in frmp.MdiChildren)
        //        {
        //            if (f.Name == fr.Name)
        //            {
        //                f.Select();
        //                f.WindowState = FormWindowState.Normal;
        //                f.BringToFront();
        //                return;
        //            }
        //        }


        //        fr.MdiParent = this;
        //        fr.BringToFront();
        //        fr.Show();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void clientesToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //   frmclientesdet frm = new frmclientesdet();
        //    frm.ShowDialog();
        //}

        //private void proyectosToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        frmmenu frmp = this;
        //        frmproyectos fr = new frmproyectos();
        //        fr.WindowState = FormWindowState.Normal;

        //        foreach (Form f in frmp.MdiChildren)
        //        {
        //            if (f.Name == fr.Name)
        //            {
        //                f.Select();
        //                f.WindowState = FormWindowState.Normal;
        //                f.BringToFront();
        //                return;
        //            }
        //        }


        //        fr.MdiParent = this;
        //        fr.BringToFront();
        //        fr.Show();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void equiposToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        frmmenu frmp = this;
        //        frmequipos fr = new frmequipos();
        //        fr.WindowState = FormWindowState.Normal;

        //        foreach (Form f in frmp.MdiChildren)
        //        {
        //            if (f.Name == fr.Name)
        //            {
        //                f.Select();
        //                f.WindowState = FormWindowState.Normal;
        //                f.BringToFront();
        //                return;
        //            }
        //        }


        //        fr.MdiParent = this;
        //        fr.BringToFront();
        //        fr.Show();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void frmmenu_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = op_combos.FillCombo(@"SELECT codigo as cod, nombre as nom FROM modulo");
            comboBox1.DisplayMember = "cod";
            comboBox1.ValueMember = "cod";
            comboBox1.Text = null;
            this.toolStripStatusLabel2.Text = op_var.usu;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
        }
      
        private void departamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmmenu frmp = this;
                frmlugares fr = new frmlugares();
                fr.WindowState = FormWindowState.Normal;

                foreach (Form f in frmp.MdiChildren)
                {
                    if (f.Name == fr.Name)
                    {
                        f.Select();
                        f.WindowState = FormWindowState.Normal;
                        f.BringToFront();
                        return;
                    }
                }


                fr.MdiParent = this;
                fr.BringToFront();
                fr.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    

        private void materialesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                frmmenu frmp = this;
                frmmateriales fr = new frmmateriales();
                fr.WindowState = FormWindowState.Normal;

                foreach (Form f in frmp.MdiChildren)
                {
                    if (f.Name == fr.Name)
                    {
                        f.Select();
                        f.WindowState = FormWindowState.Normal;
                        f.BringToFront();
                        return;
                    }
                }


                fr.MdiParent = this;
                fr.BringToFront();
                fr.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        private void entradasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmmenu frmp = this;
                Movimientos.frmmovimientos fr = new Movimientos.frmmovimientos();
                fr.WindowState = FormWindowState.Normal;

                foreach (Form f in frmp.MdiChildren)
                {
                    if (f.Name == fr.Name)
                    {
                        f.Select();
                        f.WindowState = FormWindowState.Normal;
                        f.BringToFront();
                        return;
                    }
                }


                fr.MdiParent = this;
                fr.BringToFront();
                fr.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void salidasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmmenu frmp = this;
                Movimientos.frmmovimientosINT fr = new Movimientos.frmmovimientosINT();
                fr.WindowState = FormWindowState.Normal;

                foreach (Form f in frmp.MdiChildren)
                {
                    if (f.Name == fr.Name)
                    {
                        f.Select();
                        f.WindowState = FormWindowState.Normal;
                        f.BringToFront();
                        return;
                    }
                }


                fr.MdiParent = this;
                fr.BringToFront();
                fr.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void configuraciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmmenu frmp = this;
                Movimientos.frmcalendario fr = new Movimientos.frmcalendario();
                fr.WindowState = FormWindowState.Normal;

                foreach (Form f in frmp.MdiChildren)
                {
                    if (f.Name == fr.Name)
                    {
                        f.Select();
                        f.WindowState = FormWindowState.Normal;
                        f.BringToFront();
                        return;
                    }
                }


                fr.MdiParent = this;
                fr.BringToFront();
                fr.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

       

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void crearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmmenu frmp = this;
                frmregusuario fr = new frmregusuario(1);
                fr.WindowState = FormWindowState.Normal;

                foreach (Form f in frmp.MdiChildren)
                {
                    if (f.Name == fr.Name)
                    {
                        f.Select();
                        f.WindowState = FormWindowState.Normal;
                        f.BringToFront();
                        return;
                    }
                }


                fr.MdiParent = this;
                fr.BringToFront();
                fr.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmmenu frmp = this;
                frmregusuario fr = new frmregusuario(2);
                fr.WindowState = FormWindowState.Normal;

                foreach (Form f in frmp.MdiChildren)
                {
                    if (f.Name == fr.Name)
                    {
                        f.Select();
                        f.WindowState = FormWindowState.Normal;
                        f.BringToFront();
                        return;
                    }
                }


                fr.MdiParent = this;
                fr.BringToFront();
                fr.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ajusteInventarioToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                frmmenu frmp = this;
                Consultas.frmfacturacion fr = new Consultas.frmfacturacion();
                fr.WindowState = FormWindowState.Normal;

                foreach (Form f in frmp.MdiChildren)
                {
                    if (f.Name == fr.Name)
                    {
                        f.Select();
                        f.WindowState = FormWindowState.Normal;
                        f.BringToFront();
                        return;
                    }
                }


                fr.MdiParent = this;
                fr.BringToFront();
                fr.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void centroDeCostosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmmenu frmp = this;
                Consultas.frmCcentrocostos fr = new Consultas.frmCcentrocostos();
                fr.WindowState = FormWindowState.Normal;

                foreach (Form f in frmp.MdiChildren)
                {
                    if (f.Name == fr.Name)
                    {
                        f.Select();
                        f.WindowState = FormWindowState.Normal;
                        f.BringToFront();
                        return;
                    }
                }


                fr.MdiParent = this;
                fr.BringToFront();
                fr.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void producciónEquiposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmmenu frmp = this;
                Consultas.frmliquidacionope fr = new Consultas.frmliquidacionope();
                fr.WindowState = FormWindowState.Normal;

                foreach (Form f in frmp.MdiChildren)
                {
                    if (f.Name == fr.Name)
                    {
                        f.Select();
                        f.WindowState = FormWindowState.Normal;
                        f.BringToFront();
                        return;
                    }
                }


                fr.MdiParent = this;
                fr.BringToFront();
                fr.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
                frmmenu frmp = this;
                frmcargosdet fr = new frmcargosdet();
                fr.WindowState = FormWindowState.Normal;

                foreach (Form f in frmp.MdiChildren)
                {
                    if (f.Name == fr.Name)
                    {
                        f.Select();
                        f.WindowState = FormWindowState.Normal;
                        f.BringToFront();
                        return;
                    }
                }


                fr.MdiParent = this;
                fr.BringToFront();
                fr.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            try
            {
                frmmenu frmp = this;
                frmclientesdet fr = new frmclientesdet();
                fr.WindowState = FormWindowState.Normal;

                foreach (Form f in frmp.MdiChildren)
                {
                    if (f.Name == fr.Name)
                    {
                        f.Select();
                        f.WindowState = FormWindowState.Normal;
                        f.BringToFront();
                        return;
                    }
                }


                fr.MdiParent = this;
                fr.BringToFront();
                fr.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                frmmenu frmp = this;
                frmempleadosdet fr = new frmempleadosdet();
                fr.WindowState = FormWindowState.Normal;

                foreach (Form f in frmp.MdiChildren)
                {
                    if (f.Name == fr.Name)
                    {
                        f.Select();
                        f.WindowState = FormWindowState.Normal;
                        f.BringToFront();
                        return;
                    }
                }


                fr.MdiParent = this;
                fr.BringToFront();
                fr.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            try
            {
                frmmenu frmp = this;
                frmprovedoresdet fr = new frmprovedoresdet();
                fr.WindowState = FormWindowState.Normal;

                foreach (Form f in frmp.MdiChildren)
                {
                    if (f.Name == fr.Name)
                    {
                        f.Select();
                        f.WindowState = FormWindowState.Normal;
                        f.BringToFront();
                        return;
                    }
                }


                fr.MdiParent = this;
                fr.BringToFront();
                fr.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            try
            {
                frmmenu frmp = this;
                frmequiposdet fr = new frmequiposdet();
                fr.WindowState = FormWindowState.Normal;

                foreach (Form f in frmp.MdiChildren)
                {
                    if (f.Name == fr.Name)
                    {
                        f.Select();
                        f.WindowState = FormWindowState.Normal;
                        f.BringToFront();
                        return;
                    }
                }


                fr.MdiParent = this;
                fr.BringToFront();
                fr.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            try
            {
                frmmenu frmp = this;
                frmproyectosdet fr = new frmproyectosdet();
                fr.WindowState = FormWindowState.Normal;

                foreach (Form f in frmp.MdiChildren)
                {
                    if (f.Name == fr.Name)
                    {
                        f.Select();
                        f.WindowState = FormWindowState.Normal;
                        f.BringToFront();
                        return;
                    }
                }


                fr.MdiParent = this;
                fr.BringToFront();
                fr.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            try
            {
                frmmenu frmp = this;
                frmmaterialesdet fr = new frmmaterialesdet();
                fr.WindowState = FormWindowState.Normal;

                foreach (Form f in frmp.MdiChildren)
                {
                    if (f.Name == fr.Name)
                    {
                        f.Select();
                        f.WindowState = FormWindowState.Normal;
                        f.BringToFront();
                        return;
                    }
                }


                fr.MdiParent = this;
                fr.BringToFront();
                fr.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            try
            {
                frmmenu frmp = this;
                frmlugares fr = new frmlugares();
                fr.WindowState = FormWindowState.Normal;

                foreach (Form f in frmp.MdiChildren)
                {
                    if (f.Name == fr.Name)
                    {
                        f.Select();
                        f.WindowState = FormWindowState.Normal;
                        f.BringToFront();
                        return;
                    }
                }


                fr.MdiParent = this;
                fr.BringToFront();
                fr.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void entradaDeMaterialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmmenu frmp = this;
                Movimientos.frmmovimientosdetEXT fr = new Movimientos.frmmovimientosdetEXT();
                fr.WindowState = FormWindowState.Normal;

                foreach (Form f in frmp.MdiChildren)
                {
                    if (f.Name == fr.Name)
                    {
                        f.Select();
                        f.WindowState = FormWindowState.Normal;
                        f.BringToFront();
                        return;
                    }
                }


                fr.MdiParent = this;
                fr.BringToFront();
                fr.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void salidaDeMaterialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmmenu frmp = this;
                Movimientos.frmmovimientosdetINT fr = new Movimientos.frmmovimientosdetINT();
                fr.WindowState = FormWindowState.Normal;

                foreach (Form f in frmp.MdiChildren)
                {
                    if (f.Name == fr.Name)
                    {
                        f.Select();
                        f.WindowState = FormWindowState.Normal;
                        f.BringToFront();
                        return;
                    }
                }


                fr.MdiParent = this;
                fr.BringToFront();
                fr.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\r\nDescripción: " + ex.Message.ToString(), "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
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
        //    if (string.IsNullOrEmpty(this.textBox1.Text))
        //    {
        //        errorProvider1.SetError(textBox2, "La contraseña debe ser digitada");
        //        o = false;
        //    }
        //    return o;
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            //if (!validacion()) return;

            //bool p = Operaciones.op_sql.comprobar("select usu, AES_DECRYPT(con,'84089809011053') as con from usuarios where usu='" + this.textBox1.Text + "' and AES_DECRYPT(con,'84089809011053')='" + this.textBox2.Text.Trim() + "';");
            //if (p == true)
            //{
            //    this.Hide();
            //    frmmenu frm = new frmmenu();
            //    Operaciones.op_var.usu = this.textBox1.Text;
            //    Operaciones.op_sql.parametro1(" insert into logaccesos values (null, now(),concat('" + Operaciones.op_var.usu + "',' ingresa a la aplicación')); ");
            //    //if(Operaciones.op_sql.comprobar("select * from configuracion")==false);
            //    // Operaciones.op_sql.parametro1(" update configuracion set fecha=CURDATE();");

            //    frm.Show();
            //    //menuStrip1.Enabled = true;
            //    //this.toolStripButton5.Visible = false;
            //    //this.toolStripButton4.Enabled = true;
            //    //this.toolStripButton3.Enabled = true;
            //    //this.toolStripButton2.Enabled = true;
            //    //this.toolStripComboBox1.Enabled = false;
            //    //this.textBox1.Enabled = false;
            //    //CLS.operaciones_SQL.sentencia("INSERT into logsman (usuario, fechent, accion)VALUES ('" + this.toolStripComboBox1.Text + "', current_date(),CONCAT('Ingreso a la aplicación ','" + DateTime.Now.ToShortTimeString() + "'));", "El Usuario autentificado", 0);
            //    //CLS.varent.emp = this.toolStripComboBox1.Text;
            //    //CLS.varent.user = this.toolStripComboBox1.Text;
            //    //this.toolStripStatusLabel1.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
            //    //CreaAutoHidePanels();

            //    //MessageBox.Show("El Usuario autentificado", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //    MessageBox.Show("El Usuario y/o contraseña son incorrecta", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

          
           
        
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    Application.Exit();
        //}

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void cargosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            frmcargosdet fr1 = new frmcargosdet();
            fr1.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr1.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr1.MdiParent = this;
            fr1.BringToFront();
            fr1.Show();
        }

        private void clientesYoContratistasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;


            frmclientesdet fr2 = new frmclientesdet();
            fr2.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr2.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr2.MdiParent = this;
            fr2.BringToFront();
            fr2.Show();
        }

        private void empleadosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            frmempleadosdet fr3 = new frmempleadosdet();
            fr3.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr3.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr3.MdiParent = this;
            fr3.BringToFront();
            fr3.Show();
        }

        private void propietariosDeCanterasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            frmprovedoresdet fr4 = new frmprovedoresdet();
            fr4.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr4.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr4.MdiParent = this;
            fr4.BringToFront();
            fr4.Show();
        }

        private void cargosToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            frmcargos fr5 = new frmcargos();
            fr5.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr5.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr5.MdiParent = this;
            fr5.BringToFront();
            fr5.Show();
        }

        private void clientesYoContratistasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            frmclientes fr6 = new frmclientes();
            fr6.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr6.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr6.MdiParent = this;
            fr6.BringToFront();
            fr6.Show();
        }

        private void empleadosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            frmempleados fr7 = new frmempleados();
            fr7.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr7.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr7.MdiParent = this;
            fr7.BringToFront();
            fr7.Show();
        }

        private void propietariosDeCanterasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            frmproveedores fr8 = new frmproveedores();
            fr8.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr8.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr8.MdiParent = this;
            fr8.BringToFront();
            fr8.Show();
        }

        private void equiposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void materialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            frmmateriales fr11 = new frmmateriales();
            fr11.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr11.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr11.MdiParent = this;
            fr11.BringToFront();
            fr11.Show();
        }

        private void centroDeCostosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            frmproyectos fr11 = new frmproyectos();
            fr11.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr11.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr11.MdiParent = this;
            fr11.BringToFront();
            fr11.Show();
        }

        private void distanciasDeLugaresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            Movimientos.frmdefdist fr11 = new Movimientos.frmdefdist();
            fr11.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr11.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr11.MdiParent = this;
            fr11.BringToFront();
            fr11.Show();
        }

        private void lugaresToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            frmlugares fr11 = new frmlugares();
            fr11.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr11.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr11.MdiParent = this;
            fr11.BringToFront();
            fr11.Show();
        }

        private void modificarValoresClavesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            frmcambiocod fr11 = new frmcambiocod();
            fr11.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr11.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr11.MdiParent = this;
            fr11.BringToFront();
            fr11.Show();
        }

        private void entradasDeMaterialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmmenu frmp = this;
            //Movimientos.frmmovimientos fr11 = new Movimientos.frmmovimientos();
            //fr11.WindowState = FormWindowState.Normal;
            //foreach (Form f in frmp.MdiChildren)
            //{
            //    if (f.Name == fr11.Name)
            //    {
            //        f.Select();
            //        f.WindowState = FormWindowState.Normal;
            //        f.BringToFront();
            //        return;
            //    }
            //}
            //fr11.MdiParent = this;
            //fr11.BringToFront();
            //fr11.Show();
        }

        private void salidasDeMaterialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

        private void liquidaciónDeOperadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmmenu frmp = this;
            //Consultas.frmliquidacionope fr11 = new Consultas.frmliquidacionope();
            //fr11.WindowState = FormWindowState.Normal;
            //foreach (Form f in frmp.MdiChildren)
            //{
            //    if (f.Name == fr11.Name)
            //    {
            //        f.Select();
            //        f.WindowState = FormWindowState.Normal;
            //        f.BringToFront();
            //        return;
            //    }
            //}
            //fr11.MdiParent = this;
            //fr11.BringToFront();
            //fr11.Show();
        }

        private void liquidaciónDeEquiposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            Consultas.frmresumenmov fr11 = new Consultas.frmresumenmov();
            fr11.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr11.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr11.MdiParent = this;
            fr11.BringToFront();
            fr11.Show();
        }

        private void porClientesContratistasYCanterasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            Consultas.frmfacturacion fr11 = new Consultas.frmfacturacion();
            fr11.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr11.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr11.MdiParent = this;
            fr11.BringToFront();
            fr11.Show();
        }

        private void porCentroDeCostosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            Consultas.frmCcentrocostos fr11 = new Consultas.frmCcentrocostos();
            fr11.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr11.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr11.MdiParent = this;
            fr11.BringToFront();
            fr11.Show();
        }

        private void parámetrosDeCohortesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            Movimientos.frmcalendario fr11 = new Movimientos.frmcalendario();
            fr11.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr11.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr11.MdiParent = this;
            fr11.BringToFront();
            fr11.Show();
        }

        private void valoresDeCargasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            Movimientos.frmconfig2 fr11 = new Movimientos.frmconfig2();
            fr11.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr11.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr11.MdiParent = this;
            fr11.BringToFront();
            fr11.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void porProducciónDeEntradasYSalidasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            Consultas.frmproduccionmespar fr11 = new Consultas.frmproduccionmespar();
            fr11.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr11.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr11.MdiParent = this;
            fr11.BringToFront();
            fr11.Show();
        }

        private void tecnologicosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            frmequipos fr11 = new frmequipos();
            fr11.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr11.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr11.MdiParent = this;
            fr11.BringToFront();
            fr11.Show();
        }

        private void pesadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            frmequipos fr11 = new frmequipos();
            fr11.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr11.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr11.MdiParent = this;
            fr11.BringToFront();
            fr11.Show();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            frmregusuario fr11 = new frmregusuario();
            fr11.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr11.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr11.MdiParent = this;
            fr11.BringToFront();
            fr11.Show();
        }

        private void salirToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void potMaterialesEnCentroDeCostosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            Consultas.frmproduccionmespar fr11 = new Consultas.frmproduccionmespar();
            fr11.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr11.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr11.MdiParent = this;
            fr11.BringToFront();
            fr11.Show();
        }

        private void entradasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            Movimientos.frmmovimientos fr11 = new Movimientos.frmmovimientos();
            fr11.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr11.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr11.MdiParent = this;
            fr11.BringToFront();
            fr11.Show();
        }

        private void salidasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            Movimientos.frmmovimientosINT fr11 = new Movimientos.frmmovimientosINT();
            fr11.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr11.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr11.MdiParent = this;
            fr11.BringToFront();
            fr11.Show();
        }

        private void operadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            Consultas.frmliquidacionope fr11 = new  Consultas.frmliquidacionope();
            fr11.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr11.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr11.MdiParent = this;
            fr11.BringToFront();
            fr11.Show();
        }

        private void equiposToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmmenu frmp = this;
            Consultas.frmresumenmov fr11 = new Consultas.frmresumenmov();
            fr11.WindowState = FormWindowState.Normal;
            foreach (Form f in frmp.MdiChildren)
            {
                if (f.Name == fr11.Name)
                {
                    f.Select();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            fr11.MdiParent = this;
            fr11.BringToFront();
            fr11.Show();
        }

        private void pToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ShowForm();
        }
        int u=0;
        //private void ShowForm()
        //{
        //    u+=1;
        //    string key = "f"+ u.ToString();
        //    Movimientos.frmmovimientosINT fr11 = new Movimientos.frmmovimientosINT();
        //    this.tabControl1.TabPages.Add(key,fr11.Text);
        //    fr11.TopLevel=false;

        //    fr11.FormBorderStyle =FormBorderStyle.None;
        //fr11.Dock = DockStyle.Fill;
        //       this.tabControl1.TabPages[key].Controls.Add(fr11);
         
        //    this.tabControl1.TabPages[key].Tag = fr11;

        //    fr11.Tag = this.tabControl1.TabPages[key];
        //fr11.Show();


        //}
        string key;
        private void ShowForm(int i)
        {

            switch (i)
            {
                case 20:
                    u += 1;
                    key = "f" + u.ToString();
                    Consultas.frmproduccionmescm fr00 = new Consultas.frmproduccionmescm();
                    this.tabControl1.TabPages.Add(key, fr00.Text);
                    fr00.TopLevel = false;
                    fr00.FormBorderStyle = FormBorderStyle.None;
                    fr00.Dock = DockStyle.Fill;
                    this.tabControl1.TabPages[key].Controls.Add(fr00);
                    this.tabControl1.TabPages[key].Tag = fr00;
                    fr00.Tag = this.tabControl1.TabPages[key];
                    this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.TabPages[key].ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[key];
                    fr00.Show();
                    break;
                case 1:
                    u += 1;
                    key = "f" + u.ToString();
                    frmclientes fr01 = new frmclientes();
                    this.tabControl1.TabPages.Add(key, fr01.Text);
                    fr01.TopLevel = false;
                    fr01.FormBorderStyle = FormBorderStyle.None;
                    fr01.Dock = DockStyle.Fill;
                    this.tabControl1.TabPages[key].Controls.Add(fr01);
                    this.tabControl1.TabPages[key].Tag = fr01;
                    fr01.Tag = this.tabControl1.TabPages[key];
                    this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.TabPages[key].ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[key];
                    fr01.Show();
                    break;

                case 2:
                    u += 1;
                    key = "f" + u.ToString();
                    frmproveedores fr02 = new frmproveedores();
                    this.tabControl1.TabPages.Add(key, fr02.Text);
                    fr02.TopLevel = false;
                    fr02.FormBorderStyle = FormBorderStyle.None;
                    fr02.Dock = DockStyle.Fill;
                    this.tabControl1.TabPages[key].Controls.Add(fr02);
                    this.tabControl1.TabPages[key].Tag = fr02;
                    fr02.Tag = this.tabControl1.TabPages[key];
                    this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.TabPages[key].ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[key];
                    fr02.Show();
                    break;

                case 3:
                    u += 1;
                    key = "f" + u.ToString();
                    frmempleados fr03 = new frmempleados();
                    this.tabControl1.TabPages.Add(key, fr03.Text);
                    fr03.TopLevel = false;
                    fr03.FormBorderStyle = FormBorderStyle.None;
                    fr03.Dock = DockStyle.Fill;
                    this.tabControl1.TabPages[key].Controls.Add(fr03);
                    this.tabControl1.TabPages[key].Tag = fr03;
                    fr03.Tag = this.tabControl1.TabPages[key];
                    this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.TabPages[key].ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[key];
                    fr03.Show();
                    break;


                case 4:
                    u += 1;
                    key = "f" + u.ToString();
                    frmusuarios fr04 = new frmusuarios();
                    this.tabControl1.TabPages.Add(key, fr04.Text);
                    fr04.TopLevel = false;
                    fr04.FormBorderStyle = FormBorderStyle.None;
                    fr04.Dock = DockStyle.Fill;
                    this.tabControl1.TabPages[key].Controls.Add(fr04);
                    this.tabControl1.TabPages[key].Tag = fr04;
                    fr04.Tag = this.tabControl1.TabPages[key];
                    this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.TabPages[key].ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[key];
                    fr04.Show();
                    break;

                case 5:
                    u += 1;
                    key = "f" + u.ToString();
                    frmequipos fr05 = new frmequipos();
                    this.tabControl1.TabPages.Add(key, fr05.Text);
                    fr05.TopLevel = false;
                    fr05.FormBorderStyle = FormBorderStyle.None;
                    fr05.Dock = DockStyle.Fill;
                    this.tabControl1.TabPages[key].Controls.Add(fr05);
                    this.tabControl1.TabPages[key].Tag = fr05;
                    fr05.Tag = this.tabControl1.TabPages[key];
                    this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.TabPages[key].ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[key];
                    fr05.Show();
                    break;

                case 6:
                    u += 1;
                    key = "f" + u.ToString();
                    frmproyectos fr06 = new frmproyectos();
                    this.tabControl1.TabPages.Add(key, fr06.Text);
                    fr06.TopLevel = false;
                    fr06.FormBorderStyle = FormBorderStyle.None;
                    fr06.Dock = DockStyle.Fill;
                    this.tabControl1.TabPages[key].Controls.Add(fr06);
                    this.tabControl1.TabPages[key].Tag = fr06;
                    fr06.Tag = this.tabControl1.TabPages[key];
                    this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.TabPages[key].ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[key];
                    fr06.Show();
                    break;

                case 7:
                    u += 1;
                    key = "f" + u.ToString();
                    frmmateriales fr07 = new frmmateriales();
                    this.tabControl1.TabPages.Add(key, fr07.Text);
                    fr07.TopLevel = false;
                    fr07.FormBorderStyle = FormBorderStyle.None;
                    fr07.Dock = DockStyle.Fill;
                    this.tabControl1.TabPages[key].Controls.Add(fr07);
                    this.tabControl1.TabPages[key].Tag = fr07;
                    fr07.Tag = this.tabControl1.TabPages[key];
                    this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.TabPages[key].ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[key];
                    fr07.Show();
                    break;

                case 8:
                    u += 1;
                    key = "f" + u.ToString();
                    Movimientos.frmmovimientos fr08 = new Movimientos.frmmovimientos();
                    this.tabControl1.TabPages.Add(key, fr08.Text);
                    fr08.TopLevel = false;
                    fr08.FormBorderStyle = FormBorderStyle.None;
                    fr08.Dock = DockStyle.Fill;
                    this.tabControl1.TabPages[key].Controls.Add(fr08);
                    this.tabControl1.TabPages[key].Tag = fr08;
                    fr08.Tag = this.tabControl1.TabPages[key];
                    this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.TabPages[key].ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[key];
                    fr08.Show();
                    break;

                case 9:
                    u += 1;
                    key = "f" + u.ToString();
                    Movimientos.frmmovimientosINT fr09 = new Movimientos.frmmovimientosINT();
                    this.tabControl1.TabPages.Add(key, fr09.Text);
                    fr09.TopLevel = false;
                    fr09.FormBorderStyle = FormBorderStyle.None;
                    fr09.Dock = DockStyle.Fill;
                    this.tabControl1.TabPages[key].Controls.Add(fr09);
                    this.tabControl1.TabPages[key].Tag = fr09;
                    fr09.Tag = this.tabControl1.TabPages[key];
                    this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.TabPages[key].ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[key];
                    fr09.Show();
                    break;

                case 10:
                    u += 1;
                    key = "f" + u.ToString();
                    Consultas.frmproduccionmespar fr10 = new Consultas.frmproduccionmespar();
                    this.tabControl1.TabPages.Add(key, fr10.Text);
                    fr10.TopLevel = false;
                    fr10.FormBorderStyle = FormBorderStyle.None;
                    fr10.Dock = DockStyle.Fill;
                    this.tabControl1.TabPages[key].Controls.Add(fr10);
                    this.tabControl1.TabPages[key].Tag = fr10;
                    fr10.Tag = this.tabControl1.TabPages[key];
                    this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.TabPages[key].ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[key];
                    fr10.Show();
                    break;
                case 11:
                    u += 1;
                    key = "f" + u.ToString();
                    Consultas.frmfacturacion fr11 = new Consultas.frmfacturacion();
                    this.tabControl1.TabPages.Add(key, fr11.Text);
                    fr11.TopLevel = false;
                    fr11.FormBorderStyle = FormBorderStyle.None;
                    fr11.Dock = DockStyle.Fill;
                    this.tabControl1.TabPages[key].Controls.Add(fr11);
                    this.tabControl1.TabPages[key].Tag = fr11;
                    fr11.Tag = this.tabControl1.TabPages[key];
                    this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.TabPages[key].ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[key];
                    fr11.Show();
                    break;

                case 12:
                    u += 1;
                    key = "f" + u.ToString();
                    Consultas.frmCcentrocostos fr12 = new Consultas.frmCcentrocostos();
                    this.tabControl1.TabPages.Add(key, fr12.Text);
                    fr12.TopLevel = false;
                    fr12.FormBorderStyle = FormBorderStyle.None;
                    fr12.Dock = DockStyle.Fill;
                    this.tabControl1.TabPages[key].Controls.Add(fr12);
                    this.tabControl1.TabPages[key].Tag = fr12;
                    fr12.Tag = this.tabControl1.TabPages[key];
                    this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.TabPages[key].ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[key];
                    fr12.Show();
                    break;

                case 13:
                    u += 1;
                    key = "f" + u.ToString();
                    frmcargos fr13 = new frmcargos();
                    this.tabControl1.TabPages.Add(key, fr13.Text);
                    fr13.TopLevel = false;
                    fr13.FormBorderStyle = FormBorderStyle.None;
                    fr13.Dock = DockStyle.Fill;
                    this.tabControl1.TabPages[key].Controls.Add(fr13);
                    this.tabControl1.TabPages[key].Tag = fr13;
                    fr13.Tag = this.tabControl1.TabPages[key];
                    this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.TabPages[key].ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[key];
                    fr13.Show();
                    break;

                case 14:
                    u += 1;
                    key = "f" + u.ToString();
                    Movimientos.frmdefdist fr14 = new Movimientos.frmdefdist();
                    this.tabControl1.TabPages.Add(key, fr14.Text);
                    fr14.TopLevel = false;
                    fr14.FormBorderStyle = FormBorderStyle.None;
                    fr14.Dock = DockStyle.Fill;
                    this.tabControl1.TabPages[key].Controls.Add(fr14);
                    this.tabControl1.TabPages[key].Tag = fr14;
                    fr14.Tag = this.tabControl1.TabPages[key];
                    this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.TabPages[key].ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[key];
                    fr14.Show();
                    break;

                case 15:
                    u += 1;
                    key = "f" + u.ToString();
                    Administracion.frmlugares fr15 = new Administracion.frmlugares();
                    this.tabControl1.TabPages.Add(key, fr15.Text);
                    fr15.TopLevel = false;
                    fr15.FormBorderStyle = FormBorderStyle.None;
                    fr15.Dock = DockStyle.Fill;
                    this.tabControl1.TabPages[key].Controls.Add(fr15);
                    this.tabControl1.TabPages[key].Tag = fr15;
                    fr15.Tag = this.tabControl1.TabPages[key];
                    this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.TabPages[key].ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[key];
                    fr15.Show();
                    break;


                case 19:
                    u += 1;
                    key = "f" + u.ToString();
                    Consultas.frmliquidacionope fr19 = new Consultas.frmliquidacionope();
                    this.tabControl1.TabPages.Add(key, fr19.Text);
                    fr19.TopLevel = false;
                    fr19.FormBorderStyle = FormBorderStyle.None;
                    fr19.Dock = DockStyle.Fill;
                    this.tabControl1.TabPages[key].Controls.Add(fr19);
                    this.tabControl1.TabPages[key].Tag = fr19;
                    fr19.Tag = this.tabControl1.TabPages[key];
                    this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.TabPages[key].ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[key];
                    fr19.Show();
                    break;


                case 16:
                    u += 1;
                    key = "f" + u.ToString();
                    Movimientos.frmcalendario fr16 = new Movimientos.frmcalendario();
                    this.tabControl1.TabPages.Add(key, fr16.Text);
                    fr16.TopLevel = false;
                    fr16.FormBorderStyle = FormBorderStyle.None;
                    fr16.Dock = DockStyle.Fill;
                    this.tabControl1.TabPages[key].Controls.Add(fr16);
                    this.tabControl1.TabPages[key].Tag = fr16;
                    fr16.Tag = this.tabControl1.TabPages[key];
                    this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.TabPages[key].ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[key];
                    fr16.Show();
                    break;


                case 17:
                    u += 1;
                    key = "f" + u.ToString();
                    Movimientos.frmconfig2 fr17 = new Movimientos.frmconfig2();
                    this.tabControl1.TabPages.Add(key, fr17.Text);
                    fr17.TopLevel = false;
                    fr17.FormBorderStyle = FormBorderStyle.None;
                    fr17.Dock = DockStyle.Fill;
                    this.tabControl1.TabPages[key].Controls.Add(fr17);
                    this.tabControl1.TabPages[key].Tag = fr17;
                    fr17.Tag = this.tabControl1.TabPages[key];
                    this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.TabPages[key].ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[key];
                    fr17.Show();
                    break;

                case 18:
                    u += 1;
                    key = "f" + u.ToString();
                    Consultas.frmresumenmov fr18 = new Consultas.frmresumenmov();
                    this.tabControl1.TabPages.Add(key, fr18.Text);
                    fr18.TopLevel = false;
                    fr18.FormBorderStyle = FormBorderStyle.None;
                    fr18.Dock = DockStyle.Fill;
                    this.tabControl1.TabPages[key].Controls.Add(fr18);
                    this.tabControl1.TabPages[key].Tag = fr18;
                    fr18.Tag = this.tabControl1.TabPages[key];
                    this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.TabPages[key].ContextMenuStrip = this.contextMenuStrip1;
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[key];
                    fr18.Show();
                    break;
            }


        }

        private void treeView1_NodeMouseDoubleClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Text == "Clientes y/o Contratistas") ShowForm(1);
            if (e.Node.Text == "Propietarios y Canteras") ShowForm(2);
            if (e.Node.Text == "Empleados") ShowForm(3);
            if (e.Node.Text == "Usuarios") ShowForm(4);
            if (e.Node.Text == "Equipos") ShowForm(5);
            if (e.Node.Text == "Centro de Costos") ShowForm(6);
            if (e.Node.Text == "Materiales") ShowForm(7);
            if (e.Node.Text == "Compra de Materiales (Entradas)") ShowForm(8);
            if (e.Node.Text == "Venta de Materiales (Salidas)") ShowForm(9);
            if (e.Node.Text == "Movimiento de Material por Centros") ShowForm(10);
            if (e.Node.Text == "Movimiento de Centro por Materiales") ShowForm(20);
            if (e.Node.Text == "Movimiento de Clientes y/o Contratistas") ShowForm(11);
            if (e.Node.Text == "Movimiento de Centros") ShowForm(12);
            if (e.Node.Text == "Cargos") ShowForm(13);
            if (e.Node.Text == "Distancias") ShowForm(14);
            if (e.Node.Text == "Lugares") ShowForm(15);

            if (e.Node.Text == "Parametro de Cohorte") ShowForm(16);
            if (e.Node.Text == "Parametro de Flete") ShowForm(17);
            if (e.Node.Text == "Por Equipos") ShowForm(18);
            if (e.Node.Text == "Por Operadores") ShowForm(19);
         


               
           
        }

        private void cerrarVentanaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.TabPages.Count == 0) return;
            //this.tabControl1.SelectedTab.Tag.Close();
            this.tabControl1.TabPages.Remove(this.tabControl1.SelectedTab);

        //     If Me.Tabs.TabPages.Count = 0 Then Exit Sub
        //CType(Me.Tabs.SelectedTab.Tag, Form).Close()
        //Me.Tabs.TabPages.Remove(Me.Tabs.SelectedTab)
        }

        private void cerrarTodasLasVentanasToolStripMenuItem_Click(object sender, EventArgs e)
        {


            foreach (TabPage n in this.tabControl1.TabPages)
            {
                if (this.tabControl1.SelectedTab != n)
                    this.tabControl1.TabPages.Remove(n);
            }

        //     For Each t As TabPage In Me.Tabs.TabPages
        //    If Not Me.Tabs.SelectedTab Is t Then
        //        CType(t.Tag, Form).Close()
        //        Me.Tabs.TabPages.Remove(t)
        //    End If
        //Next
        }
        private bool validacion()
        {
            bool o = true;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "El usuario debe ser digitado");
                o = false;
            }
            if (string.IsNullOrEmpty(this.textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "La contraseña debe ser digitada");
                o = false;
            }
            if (string.IsNullOrEmpty(this.comboBox1.Text))
            {
                errorProvider1.SetError(comboBox1, "El Módulo debe ser seleccionado");
                o = false;
            }
            return o;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!validacion()) return;

            bool p1 = Operaciones.op_sql.comprobar("select usu, AES_DECRYPT(con,'84089809011053') as con from usuarios where usu='" + this.textBox1.Text + "' and AES_DECRYPT(con,'84089809011053')='" + this.textBox2.Text.Trim() + "' and act=1;");
            bool p2 = Operaciones.op_sql.comprobar("select usu, AES_DECRYPT(con,'84089809011053') as con from usuarios where usu='" + this.textBox1.Text + "' and AES_DECRYPT(con,'84089809011053')='" + this.textBox2.Text.Trim() + "' and act=0;");
            if (p1 == true)
            {


                frmmenu frm = new frmmenu();
                //frm.label6.Text = "MODULO DE " + comboBox1.Text;
                Operaciones.op_var.usu = this.textBox1.Text;
                Operaciones.op_sql.parametro1(" insert into logaccesos values (null, now(),concat('" + Operaciones.op_var.usu + "',' ingresa a la aplicación')); ");
                //if(Operaciones.op_sql.comprobar("select * from configuracion")==false);
                // Operaciones.op_sql.parametro1(" update configuracion set fecha=CURDATE();");
                panel2.SendToBack();
                this.label10.Text = this.textBox1.Text;
                this.label12.Text = DateTime.Now.ToShortDateString();
                this.label13.Text = DateTime.Now.ToShortTimeString();
                panel3.Visible = true;

                //this.Hide();
                //Operaciones.op_var.forma = true;
                //frm.Show();
                //menuStrip1.Enabled = true;
                //this.toolStripButton5.Visible = false;
                //this.toolStripButton4.Enabled = true;
                //this.toolStripButton3.Enabled = true;
                //this.toolStripButton2.Enabled = true;
                //this.toolStripComboBox1.Enabled = false;
                //this.textBox1.Enabled = false;
                //CLS.operaciones_SQL.sentencia("INSERT into logsman (usuario, fechent, accion)VALUES ('" + this.toolStripComboBox1.Text + "', current_date(),CONCAT('Ingreso a la aplicación ','" + DateTime.Now.ToShortTimeString() + "'));", "El Usuario autentificado", 0);
                //CLS.varent.emp = this.toolStripComboBox1.Text;
                //CLS.varent.user = this.toolStripComboBox1.Text;
                //this.toolStripStatusLabel1.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
                //CreaAutoHidePanels();

                MessageBox.Show("El Usuario está autentificado", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (p2 == true)
            {
                MessageBox.Show("El Usuario no ha sido aprobado", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("El Usuario y/o contraseña son incorrecta", "SMACOP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

          
           
          
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
             
                this.label6.Text = op_sql.parametro1(@"SELECT nombre as nom FROM modulo where codigo='" + this.comboBox1.Text + "'");
        }

        private void cod(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //u += 1;
            //key = "f" + u.ToString();
            frmregusuario fr04 = new frmregusuario(1); 
            fr04.Text = "Nuevo Usuario";
            //this.tabControl1.TabPages.Add(key, fr04.Text);
            //fr04.TopLevel = false;
            //fr04.FormBorderStyle = FormBorderStyle.None;
            //fr04.Dock = DockStyle.Fill;
            //this.tabControl1.TabPages[key].Controls.Add(fr04);
            //this.tabControl1.TabPages[key].Tag = fr04;
            //fr04.Tag = this.tabControl1.TabPages[key];
            //this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
            //this.tabControl1.TabPages[key].ContextMenuStrip = this.contextMenuStrip1;
            //this.tabControl1.SelectedTab = this.tabControl1.TabPages[key];
            fr04.ShowDialog();

        }

        private void cod1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmregusuario fr04 = new frmregusuario(2);
            fr04.Text = "Cambiar Contraseña";
            //this.tabControl1.TabPages.Add(key, fr04.Text);
            //fr04.TopLevel = false;
            //fr04.FormBorderStyle = FormBorderStyle.None;
            //fr04.Dock = DockStyle.Fill;
            //this.tabControl1.TabPages[key].Controls.Add(fr04);
            //this.tabControl1.TabPages[key].Tag = fr04;
            //fr04.Tag = this.tabControl1.TabPages[key];
            //this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
            //this.tabControl1.TabPages[key].ContextMenuStrip = this.contextMenuStrip1;
            //this.tabControl1.SelectedTab = this.tabControl1.TabPages[key];
            fr04.ShowDialog();

        }

        

    //
  
    }
}
