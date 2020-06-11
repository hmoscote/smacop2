using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace smacop2.Administracion
{
    public partial class frmdefusuario : Form
    {
        public frmdefusuario()
        {
            InitializeComponent();
        }

        private string _usu;
        //private int _opc;
        private bool _activo;

        public frmdefusuario(string usu, bool activo):this()
        {
            _usu = usu;
            _activo = activo;
            //_opc = opc;
        }

        private void frmdefusuario_Load(object sender, EventArgs e)
        {
            comboBox2.DataSource = Operaciones.op_combos.FillCombo(@"SELECT codigo as cod, nombre as nom FROM modulo ORDER BY nom ASC");
            comboBox2.DisplayMember = "nom";
            comboBox2.ValueMember = "cod";
            comboBox2.Text = null;

            //comboBox4.DataSource = Operaciones.op_combos.FillCombo(@"SELECT usu as cod, usu as nom FROM usuarios ORDER BY nom ASC");
            //comboBox4.DisplayMember = "nom";
            //comboBox4.ValueMember = "cod";
            //comboBox4.Text = null;

            this.textBox2.Text= _usu;
            checkBox1.Checked = _activo;

            //if (_opc==1)
            //{


            //}

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
               int rowsAffected = 0;
             try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
                {
                    con.Open();

                    MySqlCommand command = new MySqlCommand("sp_gestion_usuario", con);
                    command.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramId = new MySqlParameter("msj", MySqlDbType.Int32);
                    paramId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(paramId);

                    command.Parameters.AddWithValue("modulo1",  comboBox2.SelectedValue.ToString());
                    command.Parameters.AddWithValue("usuario1", this.textBox2.Text);
                    command.Parameters.AddWithValue("activo1", checkBox1.Checked);
                    command.Parameters.AddWithValue("tipusuario1",comboBox1.Text);
              
                    command.ExecuteNonQuery();
                    rowsAffected = int.Parse(command.Parameters["msj"].Value.ToString());
                    if (rowsAffected==1)
                        MessageBox.Show("El usuario ya tiene registrado el módulo, desea cambiar el tipo de administración debe eliminar el registro del módulo y crearlo nuevamente", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (rowsAffected == 2)
                        MessageBox.Show("El Registro fue grabado", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de acceso a datos: " + ex.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex1)
            {
                MessageBox.Show("Error: " + ex1.Message.ToString(), Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
