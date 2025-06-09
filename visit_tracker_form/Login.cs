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
        private bool visiblePass = false;

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
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    string query = "SELECT id, name, username, password, is_admin, is_activated, attempts, is_blocked FROM users WHERE username = @Username";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", usuario);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int userId = reader.GetInt32("id"); // Obtém o valor da coluna especicada.

                                int attempts = reader.GetInt32("attempts");

                                string fullName = reader.GetString("name");

                                string hashSalvo = reader.GetString("password");

                                bool senhaCorreta = BCrypt.Net.BCrypt.Verify(senhaDigitada, hashSalvo);

                                bool isAdmin = reader.GetBoolean("is_admin");

                                bool isActivated = reader.GetBoolean("is_activated");

                                bool isBlocked = reader.GetBoolean("is_blocked");

                                UserSession.Id = userId;
                                UserSession.Name = fullName;

                                if (isBlocked)
                                {
                                    MessageBox.Show("Este Usuário está bloqueado. \nPor favor entre em contato com o administrador.", "Erro",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    if (senhaCorreta)
                                    {
                                        reader.Close(); // precisa fechar antes de fazer outro comando!
                                        if (attempts != 0)
                                        {
                                            string updateQuery = "UPDATE users SET attempts = 0, is_blocked = 0 WHERE id=@IdUser";
                                            using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                                            {
                                                updateCmd.Parameters.AddWithValue("@IdUser", userId);
                                                updateCmd.ExecuteNonQuery();
                                            }
                                        }

                                        if (isAdmin)
                                        {
                                            MessageBox.Show($"Login Bem-sucedido! \n Seja Bem Vindo {fullName}.", "Sucesso",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            // Exemplo: abrir tela principal
                                            new user_regist().Show();
                                            this.Hide();
                                        }
                                        else
                                        {
                                            MessageBox.Show($"Login Bem-sucedido! \n Seja Bem Vindo {fullName}.", "Sucesso",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            new client_regist().Show();
                                            this.Hide();
                                        }
                                    }
                                    else
                                    {
                                        // Se o usuário digitar a senha errada
                                        MessageBox.Show($"Senha inválidos.\n Restam {5 - attempts} tentativas", "Erro",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        attempts++;
                                        reader.Close(); // precisa fechar antes de fazer outro comando!
                                        try
                                        {
                                            MySqlCommand insertDB = new MySqlCommand
                                                ("UPDATE users SET attempts=@Attempts WHERE id=@IdUser", conn);

                                            insertDB.Parameters.Add("@Attempts", MySqlDbType.Int32).Value = attempts;
                                            insertDB.Parameters.Add("@IdUser", MySqlDbType.Int32).Value = Convert.ToInt32(userId);

                                            // Executa o comando de inserção
                                            insertDB.ExecuteNonQuery();

                                            if (attempts >= 5)
                                            {
                                                try
                                                {
                                                    string updateQuery = "UPDATE users SET is_blocked = 1 WHERE id=@IdUser";
                                                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                                                    {
                                                        updateCmd.Parameters.AddWithValue("@IdUser", userId);
                                                        updateCmd.ExecuteNonQuery();
                                                    }

                                                    MessageBox.Show("Sua conta foi bloqueada.\nVocê excedeu o número máximo de tentativas.", "Conta Bloqueada",
                                                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show("Erro Interno ao alterar as informações: " + ex.Message,
                                                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                            }
                                            else
                                            {
                                                return;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Erro Interno ao alterar as informações: " + ex.Message,
                                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //Se o usuário não foi localizado no banco.
                                MessageBox.Show("Usuário não foi localizado.", "Erro",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro ao conectar com o banco de dados: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateButtonView()
        {
            if (visiblePass)
            {
                // Se a senha estiver visível, mostra a imagem de olho fechado
                btnShowPass.Image = Properties.Resources.olho1;
                txtPass.UseSystemPasswordChar = false;
            }
            else
            {
                // Se a senha estiver oculta, mostra a imagem de olho aberto
                btnShowPass.Image = Properties.Resources.olho2;
                txtPass.UseSystemPasswordChar = true;
            }
        }

        private void btnShowPass_Click(object sender, EventArgs e)
        {
            // Alterar a visibilidade da senha
            visiblePass = !visiblePass;
            UpdateButtonView();
        }
    }
}
