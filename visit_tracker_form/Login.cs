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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("Todos os campos devem ser preenchidos!",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cria uma nova conexão com o banco de dados.
            MySqlConnection conn = new MySqlConnection(Program.connect);
            // Abra a conexão
            conn.Open();

            try
            {
                // Cria um objeto MySqlCommand para executar a consulta SQL
                MySqlCommand cmd = new MySqlCommand();

                string passEncrypt = txtPass.Text;


            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro ao conectar com banco de dados:" +  ex.Message,"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
