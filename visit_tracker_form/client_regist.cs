using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace visit_tracker_form
{
    public partial class client_regist : Form
    {
        public client_regist()
        {
            InitializeComponent();
        }

        private void client_regist_Load(object sender, EventArgs e)
        {
            label9.Text = string.Empty;
            label9.Text = $"Olá Seja Bem Vindo {UserSession.Name}.";
        }

         private void UpdateDgvClient()
        {
            using (MySqlConnection conn = new MySqlConnection(Program.connect))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                try
                {

                }
                catch (Exception ex) 
                {
                    MessageBox.Show("",ex.Message);
                }
                finally
                {
                    if ( conn.State != ConnectionState.Closed )
                    {
                        conn.Close();
                    }
                }
            }
        }
        

        private void btnSaveClient_Click(object sender, EventArgs e)
        {

        }

        private void btnEditClient_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteClient_Click(object sender, EventArgs e)
        {

        }

        private void btnClearClient_Click(object sender, EventArgs e)
        {

        }
    }
}
