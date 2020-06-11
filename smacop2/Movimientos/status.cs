using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace smacop2
{
    public partial class status : Form
    {
        public status()
        {
            InitializeComponent();
            //this.FormClosed+=new FormClosedEventHandler(status_FormClosed);
        }

     
        public void Show(string Message)
        {
            label2.Text = Message;
            this.Show();
            Application.DoEvents();
            this.Close();
        }
      

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void status_Load(object sender, EventArgs e)
        {

        }

       
    }
}
