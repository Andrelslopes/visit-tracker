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


            btnShowPass.Image = Properties.Resources.olho2;
            pictureBox1.Image = Properties.Resources.login_da_conta;
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtLogin.Text.Trim();
            string senhaDigitada = txtPass.Text.Trim();

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(senhaDigitada))
            {
                MessageBox.Show("Todos os campos devem ser preenchidos!",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(Program.connect))
                {
                    conn.Open();

                    string query = "SELECT id, name, username, password, is_admin, is_activated, is_blocked FROM users WHERE username = @Username";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", usuario);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int userId = reader.GetInt32("id"); // Obtém o valor da coluna especicada.

                                string fullName = reader.GetString("name"); 

                                string hashSalvo = reader.GetString("password");

                                bool senhaCorreta = BCrypt.Net.BCrypt.Verify(senhaDigitada, hashSalvo);

                                bool isAdmin = reader.GetBoolean("is_admin");

                                bool isActivated = reader.GetBoolean("is_activated");

                                bool isBlocked = reader.GetBoolean("is_blocked");

                                if (isBlocked)
                                {
                                    MessageBox.Show("Este Usuário está bloqueado. \nPor favor entre em contato com o administrador.", "Erro",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    if (senhaCorreta)
                                    {
                                        if (isAdmin)
                                        {
                                            MessageBox.Show("Login Bem-sucedido! \n Seja Bem Vindo Usuário Administrador.", "Sucesso",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            // Exemplo: abrir tela principal
                                            new user_regist().Show();
                                            this.Hide();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Login Bem-sucedido! \n Seja Bem Vindo Usuário  Operador.", "Sucesso",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            // Exemplo: abrir tela principal
                                            new client_regist().Show();
                                            this.Hide();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Senha incorreta.", "Erro",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Usuário não encontrado.", "Erro",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro ao conectar com o banco de dados: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
