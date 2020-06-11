using System;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;
using smacop2.Entity;
using smacop2.Operaciones;
using System.Runtime.InteropServices;
namespace smacop2
{
    public partial class frmconflistados : Form
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
        public frmconflistados()
        {
            InitializeComponent();
           
        }


       int _id;
        public frmconflistados(int id): this()
        {

           _id = id;
        }

       
        private void frmcargosconf_Load(object sender, EventArgs e)
        {
            IntPtr hmenu = GetSystemMenu(this.Handle, 0);
            int cnt = GetMenuItemCount(hmenu);
            // remove 'close' action
            RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            // remove extra menu line
            RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            DrawMenuBar(this.Handle);

                conf c;

                switch (_id)
                {
                    case 1:
                        c = opconf.GellIdConf("cargos");
                        if (c != null)
                        {
                            this.comboBox1.Text = c.campo;
                            criterio(c.valor);
                            this.checkBox1.Checked = c.cargar;
                        }
                        break;
                    case 2:
                        c = opconf.GellIdConf("clientes");
                        if (c != null)
                        {
                            this.comboBox1.Text = c.campo;
                            criterio(c.valor);
                            this.checkBox1.Checked = c.cargar;
                        }
                        break;
                    case 3:
                        c = opconf.GellIdConf("empleados");
                        if (c != null)
                        {
                            this.comboBox1.Text = c.campo;
                            criterio(c.valor);
                            this.checkBox1.Checked = c.cargar;
                        }
                        break;
                    case 4:
                        c = opconf.GellIdConf("proveedores");
                        if (c != null)
                        {
                            this.comboBox1.Text = c.campo;
                            criterio(c.valor);
                            this.checkBox1.Checked = c.cargar;
                        }
                        break;
                    case 5:
                        c = opconf.GellIdConf("equipos");
                        if (c != null)
                        {
                            this.comboBox1.Text = c.campo;
                            criterio(c.valor);
                            this.checkBox1.Checked = c.cargar;
                        }
                        break;
                    case 6:
                        c = opconf.GellIdConf("MATERIALES");
                        if (c != null)
                        {
                            this.comboBox1.Text = c.campo;
                            criterio(c.valor);
                            this.checkBox1.Checked = c.cargar;
                        }
                        break;
                    case 7:
                        c = opconf.GellIdConf("proyectos");
                        if (c != null)
                        {
                            this.comboBox1.Text = c.campo;
                            criterio(c.valor);
                            this.checkBox1.Checked = c.cargar;
                        }
                        break;
                }
                

               
        }

        private void criterio(string f)
        {
            if (f == "C") radioButton1.Checked = true;
            if (f == "E") radioButton2.Checked = true;
            if (f == "T") radioButton3.Checked = true;
        }

        private string criterio2()
        {
            string f = null;
            if (radioButton1.Checked == true) f = "C";
            if (radioButton2.Checked == true) f = "E";
            if (radioButton3.Checked == true) f = "T";
            return f;
        }
        //private Entity.cargos c { get; set; }


        private void button1_Click(object sender, EventArgs e)
        {

            conf car = new conf();
            int recibe = 0;
            switch (_id)
            {
                case 1:
                    car.tabla = "CARGOS";
                    car.campo = this.comboBox1.Text;
                    car.valor = criterio2();
                    car.cargar = checkBox1.Checked;
                    recibe = opconf.accion(car);
                    if (recibe == 1)
                        this.button1.Enabled = false;
                    break;
                case 2:
                    car.tabla = "CLIENTES";
                    car.campo = this.comboBox1.Text;
                    car.valor = criterio2();
                    car.cargar = checkBox1.Checked;
                    recibe = opconf.accion(car);
                    if (recibe == 1)
                        this.button1.Enabled = false;
                    break;
                case 3:
                    car.tabla = "EMPLEADOS";
                    car.campo = this.comboBox1.Text;
                    car.valor = criterio2();
                    car.cargar = checkBox1.Checked;
                    recibe = opconf.accion(car);
                    if (recibe == 1)
                        this.button1.Enabled = false;
                    break;
                case 4:
                    car.tabla = "PROVEEDORES";
                    car.campo = this.comboBox1.Text;
                    car.valor = criterio2();
                    car.cargar = checkBox1.Checked;
                    recibe = opconf.accion(car);
                    if (recibe == 1)
                        this.button1.Enabled = false;
                    break;
                case 5:
                    car.tabla = "equipos";
                    car.campo = this.comboBox1.Text;
                    car.valor = criterio2();
                    car.cargar = checkBox1.Checked;
                    recibe = opconf.accion(car);
                    if (recibe == 1)
                        this.button1.Enabled = false;
                    break;
                case 6:
                    car.tabla = "MATERIALES";
                    car.campo = this.comboBox1.Text;
                    car.valor = criterio2();
                    car.cargar = checkBox1.Checked;
                    recibe = opconf.accion(car);
                    if (recibe == 1)
                        this.button1.Enabled = false;
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
