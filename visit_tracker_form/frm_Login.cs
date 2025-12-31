using MySql.Data.MySqlClient;
using static visit_tracker.Properties.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using visit_tracker;


namespace visit_tracker_form
{
    public partial class frm_Login : Form
    {
        private bool visiblePass = false;

        public frm_Login()
        {
            InitializeComponent();

            btnShowPass.Image = olho2;
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        //----------------------------------------------------------
        // Botão de Login
        //----------------------------------------------------------
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

                    string query = @"SELECT 
                                        id,
                                        name,
                                        username,
                                        password,
                                        is_admin,
                                        is_activated,
                                        attempts,
                                        is_blocked
                                    FROM users 
                                    WHERE username = @Username";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", usuario);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Lê o resultado da consulta
                            if (reader.Read()) // Se o usuário foi encontrado
                            {
                                int userId = reader.GetInt32("id"); // Obtém o ID do usuário

                                int attempts = reader.GetInt32("attempts"); // Obtém o número de tentativas

                                string fullName = reader.GetString("name"); // Obtém o nome completo do usuário

                                string hashSalvo = reader.GetString("password"); // Obtém o hash da senha salva no banco

                                bool senhaCorreta = BCrypt.Net.BCrypt.Verify(senhaDigitada, hashSalvo); // Verifica a senha digitada com o hash salvo

                                bool isAdmin = reader.GetBoolean("is_admin"); // Verifica se o usuário é admin

                                bool isActivated = reader.GetBoolean("is_activated");   // Verifica se o usuário está ativado

                                bool isBlocked = reader.GetBoolean("is_blocked"); // Verifica se o usuário está bloqueado

                                UserSession.Id = userId; // Armazena o ID do usuário na sessão
                                
                                UserSession.Name = fullName; // Armazena o nome completo do usuário na sessão

                                
                                if (isBlocked) // Verifica se o usuário está bloqueado
                                {
                                    MessageBox.Show("Este Usuário está bloqueado. \nPor favor entre em contato com o administrador.", "Erro",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    if (senhaCorreta) // Se a senha estiver correta
                                    {
                                        reader.Close(); // precisa fechar antes de fazer outro comando!
                                        
                                        if (attempts != 0) // Reseta as tentativas se estiver diferente de 0
                                        {
                                            string updateQuery = "UPDATE users SET attempts = 0, is_blocked = 0 WHERE id=@IdUser";
                                            
                                            using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                                            {
                                                updateCmd.Parameters.AddWithValue("@IdUser", userId);
                                                
                                                updateCmd.ExecuteNonQuery();
                                            }
                                        }

                                        if (isAdmin) // Verifica se o usuário é admin
                                        {
                                            // Abrir tela principal
                                            new frm_User().Show();
                                            this.Hide();
                                        }
                                        else
                                        {
                                            //Abrir tela Visitas
                                            new frm_Visit().Show();
                                            this.Hide();
                                        }
                                    }
                                    else
                                    {
                                        // Se o usuário digitar a senha errada
                                        MessageBox.Show($"Senha inválida.\n Restam {5 - attempts} tentativas", "Erro",
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
                btnShowPass.Image = olho1;
                txtPass.UseSystemPasswordChar = false;
            }
            else
            {
                // Se a senha estiver oculta, mostra a imagem de olho aberto
                btnShowPass.Image = olho2;
                txtPass.UseSystemPasswordChar = true;
            }
        }

        private void btnShowPass_Click(object sender, EventArgs e)
        {
            // Alterar a visibilidade da senha
            visiblePass = !visiblePass;
            UpdateButtonView();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            // Permitir o login ao pressionar Enter
            if (e.KeyCode == Keys.Enter)
            {
                // Simula o clique no botão de login
                btnLogin.PerformClick();
            }
        }
    }
}
