using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Resources.ResXFileRef;
using WinFormsTextBox = System.Windows.Forms.TextBox;
using static visit_tracker.Properties.Resources;
using visit_tracker;

namespace visit_tracker_form
{
    public partial class user_regist : Form
    {
        private int idUser;
        private bool visiblePass = false;

        public user_regist()
        {
            InitializeComponent();
            ResetColor();
            UpdateDgvUsers();
            ClearTextbox();
            ShowId();

            txtId.Enabled = false;
            txtId.TextAlign = HorizontalAlignment.Center;
            
            btnShowPass.Image = olho2;
            btnShowPass2.Image = olho2;

            txtPass.UseSystemPasswordChar = true;
            txtConfPass.UseSystemPasswordChar = true;
        }

        private void ResetColor()
        {
            txtName.BackColor = ColorTranslator.FromHtml(default);
            txtCpf.BackColor = ColorTranslator.FromHtml(default);
            txtEmail.BackColor = ColorTranslator.FromHtml(default);
            txtUser.ForeColor = ColorTranslator.FromHtml(default);
            txtPass.ForeColor = ColorTranslator.FromHtml(default);
        }
        private void ClearTextbox()
        {
            txtId.Text = string.Empty;
            txtName.Text = "";
            txtCpf.Text = "";
            txtEmail.Text = "";
            txtUser.Text = "";
            txtPass.Text = "";
            txtConfPass.Text = "";
        }

        public static class CpfValidator
        {
            public static bool IsValid(string cpf)
            {
                // Remova quaisquer caracteres não numéricos
                cpf = Regex.Replace(cpf, @"[^0-9]", "");

                // Verifique se o comprimento tem 11 dígitos
                if (cpf.Length != 11)
                    return false;

                // Verifique se há padrões de CPF inválidos
                if (new string(cpf[0], 11) == cpf)
                    return false;

                // Calcular a verificação do primeiro dígito
                int[] multiplier1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplier2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                string tempCpf = cpf.Substring(0, 9);
                int sum = 0;

                for (int i = 0; i < 9; i++)
                    sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];

                int remainder = sum % 11;
                if (remainder < 2)
                    remainder = 0;
                else
                    remainder = 11 - remainder;

                string digit = remainder.ToString();
                tempCpf += digit;
                sum = 0;

                for (int i = 0; i < 10; i++)
                    sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];

                remainder = sum % 11;
                if (remainder < 2)
                    remainder = 0;
                else
                    remainder = 11 - remainder;

                digit += remainder.ToString();

                return cpf.EndsWith(digit);
            }
        }

        private void UpdateDgvUsers()
        {
            using (MySqlConnection conn = new MySqlConnection(Program.connect))
            {
                // Abre a conexão com o banco de dados MySQL
                conn.Open();
                try
                {
                    // Define a consulta SQL para selecionar os dados desejados da tabela 'usuario'
                    string query = "SELECT id, name, cpf, email, username FROM users WHERE is_activated = 1";

                    // Cria um MySqlCommand para executar a consulta
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        // Usa um MySqlDataAdapter para preencher um DataTable com os resultados da consulta
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Renomeia as colunas conforme necessário, verificando se cada coluna existe antes de renomear
                            if (dt.Columns.Contains("id"))
                                dt.Columns["id"].ColumnName = "ID";
                            if (dt.Columns.Contains("name"))
                                dt.Columns["name"].ColumnName = "NOME";
                            if (dt.Columns.Contains("cpf"))
                                dt.Columns["cpf"].ColumnName = "CPF";
                            if (dt.Columns.Contains("email"))
                                dt.Columns["email"].ColumnName = "EMAIL";
                            if (dt.Columns.Contains("username"))
                                dt.Columns["username"].ColumnName = "USUÁRIO";

                            // Define o DataTable como a fonte de dados do DataGridView chamado 'dgvUsers'
                            dgvUsers.DataSource = dt;
                            // Define o tamanho de cada coluna como automático
                            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Exibe uma mensagem de erro em um MessageBox se ocorrer uma exceção
                    MessageBox.Show($"Erro!\nNão foi possível carregar as informações.\nDetalhes: {ex.Message}",
                        "Erro de Carregamento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Verifica se aconexão está fechada
                    // Fecha a conexão com o banco de dados,
                    // garantindo que seja fechada mesmo se ocorrer uma exceção
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
            }
        }

        private string RemoveAcentos(string texto)
        {
            string textoNormalizado = texto.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in textoNormalizado)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        private void nameForUser()
        {
            //Formata a cor do texto dentro do textBox para default
            txtUser.BackColor = ColorTranslator.FromHtml(default);


            // Adquirir o nome completo do TextBox
            string fullName = txtName.Text.Trim();

            // Divide o nome completo em partes
            string[] nameParts = fullName.Split(new char[] { ' ' });

            if (txtName.Text != string.Empty)
            {
                if (nameParts.Length > 1)
                {
                    // Obter o primeiro nome
                    string firstName = nameParts[0];

                    // Obter o último Sobrenome
                    string lastName = nameParts[nameParts.Length - 1];

                    // Define os valores para txtUser com o devido nome e sobrenome.
                    txtUser.Text = $"{firstName}.{lastName}";

                    // Obter o nome do textBox
                    string textOriginal = txtUser.Text;

                    //Retorna o mesmo valor contido em txtUser em minúsculo e sem acentos.
                    string textProcessado = RemoveAcentos(textOriginal.ToLower());

                    // Verifica se o texto contido em txtUser está em minusculo, caso não, será convertido.
                    if (txtUser.Text != textProcessado)
                    {
                        txtUser.Text = textProcessado;
                        txtUser.SelectionStart = textProcessado.Length;
                    }
                }
                else
                {
                    MessageBox.Show("Favor Digite o nome completo.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtName.Select(); // Coloca o foco no campo de CPF caso seja inválido
                    return;
                }
            }
        }



        private void ShowId()
        {
            // Cria uma nova conexão com o banco de dados
            MySqlConnection conn = new MySqlConnection(Program.connect);

            try
            {
                // Abre a conexão
                conn.Open();

                // Define a consulta SQL para obter o próximo id
                string countId = "SELECT MAX(id) + 1 AS proximo_id FROM users;";

                // Cria um comando MySQL com a consulta SQL
                using (MySqlCommand cmd = new MySqlCommand(countId, conn))
                {
                    // Executa a consulta e obtém o resultado
                    object result = cmd.ExecuteScalar();

                    // Verifica se o resultado não é nulo
                    if (result != DBNull.Value)
                    {
                        // Converte o resultado para string e define o texto no controle TxtIdUser
                        txtId.Text = result.ToString();
                    }
                    else
                    {
                        // Se não houver resultado, define o texto como "1"
                        txtId.Text = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                // Exibe uma mensagem de erro caso ocorra uma exceção
                MessageBox.Show("Erro ao carregar id: " + ex.Message);
            }
            finally
            {
                // Fecha a conexão
                conn.Close();
            }
        }

        private void user_regist_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string errorMessage = "";

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                errorMessage += "Nome: \n";
                txtName.BackColor = ColorTranslator.FromHtml("#FEC6C6");
            }

            if (string.IsNullOrWhiteSpace(txtCpf.Text))
            {
                errorMessage += "CPF: \n";
                txtCpf.BackColor = ColorTranslator.FromHtml("#FEC6C6");
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                errorMessage += "Email: \n";
                txtEmail.BackColor = ColorTranslator.FromHtml("#FEC6C6");
            }

            if (string.IsNullOrWhiteSpace(txtUser.Text))
            {
                errorMessage += "Usuário: \n";
                txtUser.BackColor = ColorTranslator.FromHtml("#FEC6C6");
            }

            if (string.IsNullOrWhiteSpace(txtPass.Text))
            {
                errorMessage += "Senha: \n";
                txtPass.BackColor = ColorTranslator.FromHtml("#FEC6C6");
            }

            if (string.IsNullOrWhiteSpace(txtConfPass.Text))
            {
                errorMessage += "Senha: \n";
                txtConfPass.BackColor = ColorTranslator.FromHtml("#FEC6C6");
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show($"Os seguintes campos são obrigatórios:\n\n{errorMessage}",
                    "Campos Obrigatórios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                // Verifica se o CPF é válido
                if (!CpfValidator.IsValid(txtCpf.Text))
                {
                    MessageBox.Show("CPF inválido. Por favor, insira um CPF válido.",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtCpf.Select(); // Coloca o foco no campo de CPF caso seja inválido
                    return;
                }

                MySqlConnection conn = new MySqlConnection(Program.connect);
                conn.Open();

                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    int UserId = UserSession.Id; // Substituir pelo valor real da sessão quando aplicável

                    // Remover os caracteres não numéricos do TextBox
                    string cleanCpf = new string(txtCpf.Text.Where(char.IsDigit).ToArray());

                    // Define as queries SQL para verificar o login,CPF e email individualmente
                    string checkIdQuery = "SELECT COUNT(*) FROM users WHERE id = @Id";
                    string checkLoginQuery = "SELECT COUNT(*) FROM users WHERE username = @Username";
                    string checkCpfQuery = "SELECT COUNT(*) FROM users WHERE cpf = @CPF";
                    string checkEmailQuery = "SELECT COUNT(*) FROM users WHERE email = @Email";

                    // Cria comandos MySQL separados para cada query
                    MySqlCommand checkIdCmd = new MySqlCommand(checkIdQuery, conn);
                    MySqlCommand checkLoginCmd = new MySqlCommand(checkLoginQuery, conn);
                    MySqlCommand checkCpfCmd = new MySqlCommand(checkCpfQuery, conn);
                    MySqlCommand checkEmailCmd = new MySqlCommand(checkEmailQuery, conn);

                    // Adiciona os valores dos parâmetros @Login, @CPF e @Email
                    checkIdCmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = txtId.Text;
                    checkLoginCmd.Parameters.Add("@Username", MySqlDbType.VarChar).Value = txtUser.Text;
                    checkCpfCmd.Parameters.Add("@Cpf", MySqlDbType.VarChar).Value = cleanCpf;
                    checkEmailCmd.Parameters.Add("Email", MySqlDbType.VarChar).Value = txtEmail.Text;

                    // Executa as queries e converte o resultado para inteiros
                    int idExists = Convert.ToInt32(checkIdCmd.ExecuteScalar());
                    int loginExists = Convert.ToInt32(checkLoginCmd.ExecuteScalar());
                    int cpfExists = Convert.ToInt32(checkCpfCmd.ExecuteScalar());
                    int emailExists = Convert.ToInt32(checkEmailCmd.ExecuteScalar());


                    if (idExists > 0)
                    {
                        MessageBox.Show("Id já cadastrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (loginExists > 0)
                    {
                        MessageBox.Show("Usuário já cadastrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (cpfExists > 0)
                    {
                        MessageBox.Show("CPF já cadastrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (emailExists > 0)
                    {
                        MessageBox.Show("Email já cadastrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (passForce <= 80)
                    {
                        MessageBox.Show("Senha não possui os requisitos mínimos de seguraça, Por favor verifique!", "Aviso", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        MySqlCommand insertDB = new MySqlCommand(
                            "INSERT INTO users (name, cpf, email, username, password, created_by, updated_by) " +
                            "VALUES (@Name, @Cpf, @Email, @Username, @Password, @Created_by, @Updated_by);", conn);

                        insertDB.Parameters.Add("@Name", MySqlDbType.VarChar).Value = txtName.Text;
                        insertDB.Parameters.Add("@Cpf", MySqlDbType.VarChar).Value = cleanCpf;
                        insertDB.Parameters.Add("@Email", MySqlDbType.VarChar).Value = txtEmail.Text;
                        insertDB.Parameters.Add("@Username", MySqlDbType.VarChar).Value = txtUser.Text;
                        insertDB.Parameters.Add("@Password", MySqlDbType.VarChar).Value = BCrypt.Net.BCrypt.HashPassword(txtPass.Text);
                        insertDB.Parameters.Add("@Created_by", MySqlDbType.Int32).Value = UserId;
                        insertDB.Parameters.Add("@Updated_by", MySqlDbType.Int32).Value = UserId;

                        insertDB.ExecuteNonQuery();

                        MessageBox.Show("Usuário cadastrado com sucesso!",
                            "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearTextbox(); // Apenas aqui
                        UpdateDgvUsers();
                        ShowId();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar as informações: " + ex.Message,
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string errorMessage = "";

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                errorMessage += "Nome: \n";
                txtName.BackColor = ColorTranslator.FromHtml("#FEC6C6");
            }

            if (string.IsNullOrWhiteSpace(txtCpf.Text))
            {
                errorMessage += "CPF: \n";
                txtCpf.BackColor = ColorTranslator.FromHtml("#FEC6C6");
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                errorMessage += "Email: \n";
                txtEmail.BackColor = ColorTranslator.FromHtml("#FEC6C6");
            }

            if (string.IsNullOrWhiteSpace(txtUser.Text))
            {
                errorMessage += "Usuário: \n";
                txtUser.BackColor = ColorTranslator.FromHtml("#FEC6C6");
            }

            if (string.IsNullOrWhiteSpace(txtPass.Text))
            {
                errorMessage += "Senha: \n";
                txtPass.BackColor = ColorTranslator.FromHtml("#FEC6C6");
            }

            if (string.IsNullOrWhiteSpace(txtConfPass.Text))
            {
                errorMessage += "Senha: \n";
                txtConfPass.BackColor = ColorTranslator.FromHtml("#FEC6C6");
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show($"Os seguintes campos são obrigatórios:\n\n{errorMessage}",
                    "Campos Obrigatórios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                MySqlConnection conn = new MySqlConnection(Program.connect);

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                int UserId = UserSession.Id; // Substituir pelo valor real da sessão quando aplicável

                // Remover os caracteres não numéricos do TextBox
                string cleanCpf = new string(txtCpf.Text.Where(char.IsDigit).ToArray());

                // Verifica se o CPF é válido
                if (!CpfValidator.IsValid(txtCpf.Text))
                {
                    MessageBox.Show("CPF inválido. Por favor, insira um CPF válido.",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtCpf.Select(); // Coloca o foco no campo de CPF caso seja inválido
                    return;
                }

                try
                {
                    MySqlCommand insertDB = new MySqlCommand("UPDATE users SET name=@Name, cpf=@Cpf, email=@Email, username=@Username, password=@Password, updated_by=@Updated_by WHERE id=@Id", conn);

                    insertDB.Parameters.Add("@Name", MySqlDbType.VarChar).Value = txtName.Text;
                    insertDB.Parameters.Add("@Cpf", MySqlDbType.VarChar).Value = cleanCpf;
                    insertDB.Parameters.Add("@Email", MySqlDbType.VarChar).Value = txtEmail.Text;
                    insertDB.Parameters.Add("@Username", MySqlDbType.VarChar).Value = txtUser.Text;
                    insertDB.Parameters.Add("@Password", MySqlDbType.VarChar).Value = BCrypt.Net.BCrypt.HashPassword(txtPass.Text);
                    insertDB.Parameters.Add("@Updated_by", MySqlDbType.Int32).Value = UserId;
                    insertDB.Parameters.Add("@Id", MySqlDbType.Int32).Value = Convert.ToInt32(txtId.Text);
                    
                    insertDB.ExecuteNonQuery();

                    MessageBox.Show("Dados Alterados com Sucesso.",
                        "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearTextbox(); // Apenas aqui
                    UpdateDgvUsers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao editar as informações: " + ex.Message,
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
            }
        }

        private void btnDelet_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Deseja realmente excluir este usuário?",
                "Excluir Usuário.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No)
            {
                return;
            }
            else
            {
                MySqlConnection conn = new MySqlConnection(Program.connect);

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                try
                {
                    string valueActive = "0";

                    MySqlCommand insertDB = new MySqlCommand
                        ("UPDATE users SET is_activated=@IsActivated WHERE id=@IdUser", conn);

                    insertDB.Parameters.Add("@IsActivated", MySqlDbType.Int32).Value = valueActive;
                    insertDB.Parameters.Add("@IdUser", MySqlDbType.Int32).Value = Convert.ToInt32(idUser);

                    // Executa o comando de inserção
                    insertDB.ExecuteNonQuery();

                    // Exibe uma mensagem de sucesso
                    MessageBox.Show("Dados excluidos com sucesso!",
                        "Sucesso", MessageBoxButtons.OK);

                    ClearTextbox();
                    ResetColor();
                    UpdateDgvUsers();
                    ClearTextbox();
                    ShowId();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao apagar as informações: " + ex.Message,
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearTextbox();
            ShowId();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            txtName.BackColor = ColorTranslator.FromHtml(default);

            // Converte o texto digitado para Title Case
            // (onde a primeira letra de cada palavra fica maiúscula).
            if (sender is WinFormsTextBox textBox)
            {
                // Guarda a posição atual do cursor dentro do TextBox
                int selectionStart = textBox.SelectionStart;

                // Guarda o tamanho do texto selecionado (se houver).
                int selectionLength = textBox.SelectionLength;

                // Use a versão específica do "ToTitleCase" para respeitar a cultura atual
                textBox.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(textBox.Text.ToLower());

                // Restaurar a seleção original
                textBox.Select(selectionStart, selectionLength);
            }
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            nameForUser();
        }

        private void txtCpf_TextChanged(object sender, EventArgs e)
        {
            txtCpf.BackColor = ColorTranslator.FromHtml(default);

            /* FORMATAÇAO DO TEXTBOX CPF */
            // Obtém o texto do TextBox
            string cpf = txtCpf.Text;

            // Filtra apenas os dígitos do texto, removendo qualquer caractere não numérico
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            // Limita o comprimento do CPF a no máximo 11 dígitos
            if (cpf.Length > 11)
                cpf = cpf.Substring(0, 11);

            // Variável para armazenar o CPF formatado
            string formattedCpf = string.Empty;

            // Adiciona os primeiros 3 dígitos
            if (cpf.Length > 0)
                formattedCpf += cpf.Substring(0, Math.Min(3, cpf.Length));

            // Adiciona o segundo grupo de 3 dígitos com um ponto na frente
            if (cpf.Length > 3)
                formattedCpf += "." + cpf.Substring(3, Math.Min(3, cpf.Length - 3));

            // Adiciona o terceiro grupo de 3 dígitos com outro ponto na frente
            if (cpf.Length > 6)
                formattedCpf += "." + cpf.Substring(6, Math.Min(3, cpf.Length - 6));

            // Adiciona os últimos 2 dígitos com um traço na frente
            if (cpf.Length > 9)
                formattedCpf += "-" + cpf.Substring(9, Math.Min(2, cpf.Length - 9));

            // Define o texto do TextBox como o CPF formatado
            txtCpf.Text = formattedCpf;

            // Ajusta a posição do cursor para o final do texto
            txtCpf.SelectionStart = formattedCpf.Length;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            txtEmail.BackColor = ColorTranslator.FromHtml(default);

            // Converte o texto digitado para minúsculas.
            if (sender is WinFormsTextBox textBox)
            {
                // Guarda a posição atual do cursor dentro do TextBox
                int selectionStart = textBox.SelectionStart;

                // Guarda o tamanho do texto selecionado (se houver).
                int selectionLength = textBox.SelectionLength;

                // Converte o texto digitado para minúsculo
                textBox.Text = textBox.Text.ToLower();

                // Restaura a seleção original
                textBox.Select(selectionStart, selectionLength);
            }
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            txtUser.BackColor = ColorTranslator.FromHtml(default);
        }

        private int CalcularForcaSenha(string senha)
        {
            int pontuacao = 0;

            if (senha.Length >= 8)
                pontuacao += 20;
            if (Regex.IsMatch(senha, @"[a-z]"))
                pontuacao += 20;
            if (Regex.IsMatch(senha, @"[A-Z]"))
                pontuacao += 20;
            if (Regex.IsMatch(senha, @"[0-9]"))
                pontuacao += 20;
            if (Regex.IsMatch(senha, @"[\W_]"))
                pontuacao += 20;

            return pontuacao;
        }

        int passForce = 0;

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            txtPass.BackColor = ColorTranslator.FromHtml(default);

            string senha = txtPass.Text;
            int forca = CalcularForcaSenha(senha);
            passForce = forca;

            progressBar1.Value = forca;
            progressBar1.Invalidate();  // Força a atualização da barra de progresso

            // Muda cor da barra — você precisa criar uma ProgressBar customizada, pois a padrão não permite mudar cor diretamente.

            if (forca == 0)
            {
                progressBar1.ForeColor = Color.Red;
            }
            else if (forca <= 40)
            {
                progressBar1.ForeColor = Color.Red;
            }
            else if (forca <= 80)
            {
                progressBar1.ForeColor = Color.Orange;
            }
            else
            {
                progressBar1.ForeColor = Color.Green;
            }
        }

        private void txtConfPass_TextChanged(object sender, EventArgs e)
        {
            txtConfPass.BackColor = ColorTranslator.FromHtml(default);

            string senha = txtConfPass.Text;
            int forca = CalcularForcaSenha(senha);

            progressBar2.Value = forca;
            progressBar2.Invalidate();  // Força a atualização da barra de progresso

            // Muda cor da barra — você precisa criar uma ProgressBar customizada, pois a padrão não permite mudar cor diretamente.

            if (forca == 0)
            {
                progressBar2.ForeColor = Color.Red;
            }
            else if (forca <= 40)
            {
                progressBar2.ForeColor = Color.Red;
            }
            else if (forca <= 80)
            {
                progressBar2.ForeColor = Color.Orange;
            }
            else
            {
                progressBar2.ForeColor = Color.Green;
            }
        }

        private void checkPasswords()
        {
            if(txtConfPass.Text != string.Empty)
            {
                if (txtConfPass.Text != txtPass.Text)
                {
                    MessageBox.Show("Senhas não conferem. Por favor verifique.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtConfPass.Select();
                    txtConfPass.Text = string.Empty;
                    return;
                }
            }
        }

        private void txtConfPass_Leave(object sender, EventArgs e)
        {
            checkPasswords();
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

        private void UpdateButtonView2()
        {
            if (visiblePass)
            {
                // Se a senha estiver visível, mostra a imagem de olho fechado
                btnShowPass2.Image = olho1;
                txtConfPass.UseSystemPasswordChar = false;
            }
            else
            {
                // Se a senha estiver oculta, mostra a imagem de olho aberto
                btnShowPass2.Image = olho2;
                txtConfPass.UseSystemPasswordChar = true;
            }
        }

        private void btnShowPass2_Click(object sender, EventArgs e)
        {
            // Alterar a visibilidade da senha
            visiblePass = !visiblePass;
            UpdateButtonView2();
        }

        private void dgvUsers_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Obtendo o ID da linha quando clicado duas vezes dentro do dgvUser.
            int Selected_Id = Convert.ToInt32(dgvUsers.Rows[e.RowIndex].Cells[0].Value);

            // Atribui o valor de 'Selected_Id' a variavel global 'idUser'
            idUser = Selected_Id;

            txtId.Text = Selected_Id.ToString();
            txtName.Text = dgvUsers.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCpf.Text = dgvUsers.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvUsers.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtUser.Text = dgvUsers.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string filter = txtSearch.Text.Trim(); // Remove espaços desnecessários

            if (string.IsNullOrWhiteSpace(filter))
            {
                UpdateDgvUsers(); // Recarrega todos os dados se o campo estiver vazio
                return;
            }

            string query = "SELECT id, name, cpf, email, username FROM users WHERE is_activated = 1 AND cpf LIKE @Filter";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(Program.connect))
                {
                    await conn.OpenAsync();

                    using (MySqlCommand comando = new MySqlCommand(query, conn))
                    {
                        // Busca pelo início do CPF
                        comando.Parameters.AddWithValue("@Filter", filter + "%");

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(comando))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Renomeia as colunas conforme necessário, verificando se cada coluna existe antes de renomear
                            if (dt.Columns.Contains("id"))
                                dt.Columns["id"].ColumnName = "ID";

                            if (dt.Columns.Contains("name"))
                                dt.Columns["name"].ColumnName = "NOME";

                            if (dt.Columns.Contains("cpf"))
                                dt.Columns["cpf"].ColumnName = "CPF";

                            if (dt.Columns.Contains("email"))
                                dt.Columns["email"].ColumnName = "EMAIL";

                            if (dt.Columns.Contains("username"))
                                dt.Columns["username"].ColumnName = "USUÁRIO";

                            // Define o DataTable como a fonte de dados do DataGridView chamado 'dgvUsers'
                            // Atualiza o DataGridView com os dados filtrados
                            dgvUsers.DataSource = dt;

                            // Define o tamanho de cada coluna como automático
                            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao buscar os dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void progressBar2_Click(object sender, EventArgs e)
        {

        }

        private void btnCadClient_Click(object sender, EventArgs e)
        {
            new client_regist().Show();
            this.Hide();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnVisit_Click(object sender, EventArgs e)
        {
            new frm_Visit().Show();
            this.Hide();
        }
    }
}
