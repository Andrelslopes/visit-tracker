using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using visit_tracker;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;
using WinFormsTextBox = System.Windows.Forms.TextBox;

namespace visit_tracker_form
{
    public partial class frm_Client : Form
    {
        public frm_Client()
        {
            InitializeComponent();
            UpdateDgvClient();
            ShowId();
            enumContactType();
        }

        private void client_regist_Load(object sender, EventArgs e)
        {
            label9.Text = string.Empty;
            label9.Text = $"Olá, Seja Bem Vindo {UserSession.Name}.";
        }

        private void ShowId()
        {
            //Cria uma nova conexão como o banco de dados
            using (MySqlConnection conn = new MySqlConnection(Program.connect))
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    // Define a consulta SQL para obter o próximo id
                    string countId = "SELECT MAX(id) + 1 AS proximo_id FROM clients;";

                    // Cria um comando MySQL com a consulta SQL
                    using (MySqlCommand cmd = new MySqlCommand(countId, conn))
                    {
                        // Executa a consulta e obtém o resultado
                        object result = cmd.ExecuteScalar();

                        // Verifica se o resultado não é nulo
                        if (result != null)
                        {
                            // Converte o resultado para string e define o texto no controle TxtIdUser
                            txtCod.Text = result.ToString();
                        }
                        else
                        {
                            // Se não houver resultado, define o texto como "1"
                            txtCod.Text = "1";
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
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
            }
        }

        private void ClearAll()
        {
            txtCod.Text = string.Empty;
            txtName.Text = string.Empty;
            txtResponsible.Text = string.Empty;
            cbxTypeContact.SelectedIndex = -1;
            txtValueContact.Enabled = false;
            txtCEP.Text = string.Empty;
            txtStreet.Text = string.Empty;
            txtNumber.Text = string.Empty;
            txtNeighborhood.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtState.Text = string.Empty;
            btnSaveClient.Enabled = true;
            ShowId();
        }
        
        private void UpdateDgvClient()
        {
            using (MySqlConnection conn = new MySqlConnection(Program.connect))
            {
                // Abre a conexão com o banco de dados MySQL
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                try
                {
                    // Define a consulta SQL para selecionar os dados desejados da tabela 'usuario'
                    string query = @"
                        SELECT 
                            c.id, 
                            c.name, 
                            p.responsible, 
                            p.type, 
                            p.value_type, 
                            a.postal_code, 
                            a.street, 
                            a.number, 
                            a.neighborhood, 
                            a.city, 
                            a.state 
                        FROM clients c
                        JOIN addresses a ON a.fk_id_client = c.id
                        JOIN client_contacts p ON p.fk_client_id = c.id
                        WHERE c.is_activated = 1;";


                    // Cria um MySqlCommand para executar a consulta 
                    using (MySqlCommand cmd = new MySqlCommand (query, conn))
                    {
                        // Usa um MySqlAdapter para preencher um DataTable com os resultados da consulta
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter (cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill (dt);

                            // Renomeia as colunas conforme necessário, verificando se cada coluna exite antes de renomar
                            if (dt.Columns.Contains("id"))
                                dt.Columns["id"].ColumnName = "Id";

                            if (dt.Columns.Contains ("name"))
                                dt.Columns["name"].ColumnName = "Nome";

                            if (dt.Columns.Contains("responsible"))
                                dt.Columns["responsible"].ColumnName = "Responsável";

                            if (dt.Columns.Contains("type"))
                                dt.Columns["type"].ColumnName = "Tipo";

                            if (dt.Columns.Contains("value_type"))
                                dt.Columns["value_type"].ColumnName = "Contato";

                            if (dt.Columns.Contains("postal_code"))
                                dt.Columns["postal_code"].ColumnName = "Cep";

                            if (dt.Columns.Contains("street"))
                                dt.Columns["street"].ColumnName = "Rua";

                            if (dt.Columns.Contains("number"))
                                dt.Columns["number"].ColumnName = "Nº";

                            if (dt.Columns.Contains("neighborhood"))
                                dt.Columns["neighborhood"].ColumnName = "Bairro";

                            if (dt.Columns.Contains("city"))
                                dt.Columns["city"].ColumnName = "Cidade";

                            if (dt.Columns.Contains("state"))
                                dt.Columns["state"].ColumnName = "Estado";

                            // Define o DataTable como a fonte de dados do DataGridView chamado "dgvClient"
                            dgvClient.DataSource = dt;

                            // Define o tamanho de cada coluna como automático
                            dgvClient.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        }
                    }
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

        /// <summary>
        /// Popula o ComboBox 'cbxTypeContact' com os valores do enum 'AppEnums.ContactType',
        /// exibindo a descrição para o usuário e mantendo o valor real do enum internamente.
        /// </summary>
        private void enumContactType()
        {
            // Obtém todos os valores do enum 'AppEnums.ContactType' e faz cast para o tipo correto.
            var values = Enum.GetValues(typeof(AppEnums.ContactType)).Cast<AppEnums.ContactType>();

            // Percorre cada valor do enum
            foreach (var value in values)
            {
                // Obtém a descrição do valor (provavelmente definida com [Description("Texto")])
                string description = EnumHelper.GetDescription(value);

                // Adiciona um item ao ComboBox como objeto anônimo
                //     Text  = descrição amigável (o que o usuário vê na tela)
                //     Value = valor do enum (o que você usará no código)
                cbxTypeContact.Items.Add(new { Text = description, Value = value });
            }

            // Define que o texto visível no ComboBox será a propriedade "Text"
            cbxTypeContact.DisplayMember = "Text";

            // Define que o valor associado ao item será a propriedade "Value"
            cbxTypeContact.ValueMember = "Value";

            // Garante que nenhum item esteja selecionado inicialmente
            cbxTypeContact.SelectedIndex = -1;
        }

        private void btnSaveClient_Click(object sender, EventArgs e)
        {
            string errorMessage = "";

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                errorMessage += "Nome: \n";
                txtName.BackColor = ColorTranslator.FromHtml("#FEC6C6");
            }

            if (string.IsNullOrWhiteSpace(txtCEP.Text))
            {
                errorMessage += "CEP: \n";
                txtCEP.BackColor = ColorTranslator.FromHtml("#FEC6C6");
            }

            if (string.IsNullOrWhiteSpace(txtStreet.Text))
            {
                errorMessage += "Rua: \n";
                txtStreet.BackColor = ColorTranslator.FromHtml("#FEC6C6");
            }

            if (string.IsNullOrWhiteSpace(txtNumber.Text))
            {
                errorMessage += "Número: \n";
                txtNumber.BackColor = ColorTranslator.FromHtml("#FEC6C6");
            }

            if (string.IsNullOrWhiteSpace(txtNeighborhood.Text))
            {
                errorMessage += "Bairro: \n";
                txtNeighborhood.BackColor = ColorTranslator.FromHtml("#FEC6C6");
            }

            if (string.IsNullOrWhiteSpace(txtCity.Text))
            {
                errorMessage += "Cidade: \n";
                txtCity.BackColor = ColorTranslator.FromHtml("#FEC6C6");
            }

            if (string.IsNullOrWhiteSpace(txtState.Text))
            {
                errorMessage += "Estado: \n";
                txtState.BackColor = ColorTranslator.FromHtml("#FEC6C6");
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show($"Os seguintes campos são obrigatórios:\n\n{errorMessage}",
                    "Campos Obrigatórios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                using (MySqlConnection conn = new MySqlConnection(Program.connect))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    using (var transation = conn.BeginTransaction())
                    {
                        try
                        {
                            // Obtem o id do usuario logado e armazena na variavel UserId
                            int UserId = UserSession.Id;

                            // Inicia a inseção na tabela 'client'
                            string queryClients = "INSERT INTO clients(name, created_by, updated_by ) VALUES (@Name, @Created_by, @Updated_by)";

                            // Cria um novo comando MySqlCommand para inserir os dados na tabela 'clients'
                            using (MySqlCommand cmd = new MySqlCommand(queryClients, conn, transation))
                            {
                                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                                cmd.Parameters.AddWithValue("@Created_by", UserId);
                                cmd.Parameters.AddWithValue("@Updated_by", UserId);

                                // Executa o comando SQL para inserção na tabela 'users'
                                cmd.ExecuteNonQuery();
                            }

                            // Obtém o texto do TextBox
                            string cep = txtCEP.Text;

                            // Filtra apenas os dígitos do texto, removendo qualquer caractere não numérico
                            cep = new string(cep.Where(char.IsDigit).ToArray());

                            // Obter o valor da chave primária da tabela 'Clients'
                            int fkClietId;

                            string lastId = "SELECT LAST_INSERT_ID()";

                            using (MySqlCommand cmd = new MySqlCommand(lastId, conn, transation))
                            {
                                fkClietId = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            string queryContacts = "INSERT INTO client_contacts (fk_client_id, type, value_type,responsible, created_by, updated_by ) VALUES (@fk_client_id, @type, @value_type, @responsible, @created_by, @updated_by)";

                            // Cria um novo comando MySqlCommanda para inserir os dados na tabela "client_contacts"
                            using (MySqlCommand cmd = new MySqlCommand(queryContacts, conn, transation))
                            {
                                cmd.Parameters.AddWithValue("@fk_client_id",fkClietId);
                                cmd.Parameters.AddWithValue("@type", cbxTypeContact.Text); // Criar uma classe para enum.
                                cmd.Parameters.AddWithValue("@value_type", txtValueContact.Text);
                                cmd.Parameters.AddWithValue("@responsible",txtResponsible.Text);
                                cmd.Parameters.AddWithValue("@Created_by", UserId);
                                cmd.Parameters.AddWithValue("@Updated_by", UserId);

                                // Executa o comando SQL para inserção na tabela 'users'
                                cmd.ExecuteNonQuery();
                            }

                            string queryAddresses = "INSERT INTO addresses (postal_code, street, number, neighborhood, city, state, fk_id_client, created_by, updated_by)" +
                                " VALUES (@Postal_code, @Street, @Number, @Neighborhood, @City, @State, @Fk_id_client, @Created_by, @Updated_by)";

                            // Cria um novo comando MySqlCommand para inserir os dados na tabela 'Addresses'
                            using (MySqlCommand cmd = new MySqlCommand(queryAddresses, conn, transation))
                            {
                                cmd.Parameters.AddWithValue("@Postal_code", cep);
                                cmd.Parameters.AddWithValue("@Street", txtStreet.Text);
                                cmd.Parameters.AddWithValue("@Number", txtNumber.Text);
                                cmd.Parameters.AddWithValue("@Neighborhood", txtNeighborhood.Text);
                                cmd.Parameters.AddWithValue("@City", txtCity.Text);
                                cmd.Parameters.AddWithValue("@State", txtState.Text);
                                cmd.Parameters.AddWithValue("@Fk_id_client", fkClietId);
                                cmd.Parameters.AddWithValue("@Created_by", UserId);
                                cmd.Parameters.AddWithValue("@Updated_by", UserId);

                                // Executa o comando SQL para inserção na tabela 'users'.
                                cmd.ExecuteNonQuery();
                            }

                            // Se tudo correr bem, commit a transação.
                            transation.Commit();

                            // Exibe uma mensagem de sucesso
                            MessageBox.Show("Dados criados com Sucesso!", "Sucesso", MessageBoxButtons.OK);

                            // Chama o método para atualizar o próximo Id em txtCod.
                            ShowId();

                            // Chama o método para atualizar o dataGridView.
                            UpdateDgvClient();

                            // Chama o método para limpar os campos textBox.
                            ClearAll();

                        }
                        catch (Exception ex)
                        {
                            transation.Rollback();
                            MessageBox.Show("Erro ao inserir no banco:\n" + ex.Message + "\n" + ex.StackTrace, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            // Fecha a conexão com o banco de dados
                            if (conn.State != ConnectionState.Closed)
                            {
                                conn.Close();
                            }
                        }
                    }
                }
            }
        }

        private void btnEditClient_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(Program.connect))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int userId = UserSession.Id;
                        int clientId = Convert.ToInt32(txtCod.Text); // ID do cliente

                        // Atualiza os dados do cliente
                        string queryClients = "UPDATE clients SET name=@Name, updated_by=@Updated_by WHERE id=@Id";

                        using (MySqlCommand cmd = new MySqlCommand(queryClients, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Name", txtName.Text);
                            cmd.Parameters.AddWithValue("@Updated_by", userId);
                            cmd.Parameters.AddWithValue("@Id", clientId);

                            cmd.ExecuteNonQuery();
                        }

                        string queryContacts = "UPDATE client_contacts SET fk_client_id=@Fk_client_id, responsible=@Responsible, type=@Type, value_type=@Value_type, created_by=@Created_by, updated_by=@Updated_by WHERE id=@Id";

                        using (MySqlCommand cmd = new MySqlCommand(queryContacts, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Fk_client_id", clientId);
                            cmd.Parameters.AddWithValue("@Responsible",txtResponsible.Text);
                            cmd.Parameters.AddWithValue("@Type", cbxTypeContact.Text);
                            cmd.Parameters.AddWithValue("@Value_type", txtValueContact.Text); // <-- CORRIGIDO
                            cmd.Parameters.AddWithValue("@Created_by", userId);
                            cmd.Parameters.AddWithValue("@Updated_by", userId);

                            // Aqui você precisa passar o ID do contato que está sendo atualizado
                            int contactId = Convert.ToInt32(idClient); // ou de onde você pega esse ID
                            cmd.Parameters.AddWithValue("@Id", contactId); // <-- ADICIONADO

                            cmd.ExecuteNonQuery();
                        }

                        // Atualiza o endereço relacionado ao cliente
                        string queryAddresses = @"UPDATE addresses SET postal_code=@Postal_code, street=@Street, number=@Number, neighborhood=@Neighborhood, city=@City, state=@State, updated_by=@Updated_by WHERE fk_id_client=@Fk_id_client";

                        // Obtém o texto do TextBox
                        string cep = txtCEP.Text;

                        // Filtra apenas os dígitos do texto, removendo qualquer caractere não numérico
                        cep = new string(cep.Where(char.IsDigit).ToArray());

                        using (MySqlCommand cmd = new MySqlCommand(queryAddresses, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Postal_code", cep);
                            cmd.Parameters.AddWithValue("@Street", txtStreet.Text);
                            cmd.Parameters.AddWithValue("@Number", txtNumber.Text);
                            cmd.Parameters.AddWithValue("@Neighborhood", txtNeighborhood.Text);
                            cmd.Parameters.AddWithValue("@City", txtCity.Text);
                            cmd.Parameters.AddWithValue("@State", txtState.Text);
                            cmd.Parameters.AddWithValue("@Updated_by", userId);
                            cmd.Parameters.AddWithValue("@Fk_id_client", clientId); // usa o ID do cliente

                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        MessageBox.Show("Dados editados com sucesso!", "Sucesso", MessageBoxButtons.OK);
                        UpdateDgvClient();
                        ClearAll();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Erro ao alterar dados no banco:\n" + ex.Message + "\n" + ex.StackTrace, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }

        private int idClient;

        private void btnDeleteClient_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Deseja realmente excluir este cliente?",
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
                        ("UPDATE clients SET is_activated=@IsActivated WHERE id=@IdClient", conn);

                    insertDB.Parameters.Add("@IsActivated", MySqlDbType.Int32).Value = valueActive;
                    insertDB.Parameters.Add("@IdClient", MySqlDbType.Int32).Value = Convert.ToInt32(idClient);

                    // Executa o comando de inserção
                    insertDB.ExecuteNonQuery();

                    // Exibe uma mensagem de sucesso
                    MessageBox.Show("Dados excluidos com sucesso!",
                        "Sucesso", MessageBoxButtons.OK);

                    UpdateDgvClient();
                    ClearAll();

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

        private void btnClearClient_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void txtCod_TextChanged(object sender, EventArgs e)
        {
            txtCod.BackColor = ColorTranslator.FromHtml(default);
        }

        private void FormatToTitleCase(TextBox textBox)
        {
            if (textBox == null) return;

            // Reseta a cor de fundo para o padrão
            textBox.BackColor = ColorTranslator.FromHtml(default);

            // Guarda a posição atual do cursor
            int selectionStart = textBox.SelectionStart;
            int selectionLength = textBox.SelectionLength;

            // Converte para Title Case respeitando a cultura atual
            textBox.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo
                           .ToTitleCase(textBox.Text.ToLower());

            // Restaura a seleção original
            textBox.Select(selectionStart, selectionLength);
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            txtName.BackColor = ColorTranslator.FromHtml(default);

            if (sender is TextBox textBox)
            {
                FormatToTitleCase(textBox);
            }
        }

        private void txtResponsible_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                FormatToTitleCase(textBox);
            }
        }

        private void txtCEP_TextChanged_1(object sender, EventArgs e)
        {
            txtCEP.BackColor = ColorTranslator.FromHtml(default);

            /* FORMATAÇÃO DO TEXTBOX CEP */
            // Obtém o texto do TextBox
            string cep = txtCEP.Text;

            // Filtra apenas os dígitos do texto, removendo qualquer caractere não numérico
            cep = new string(cep.Where(char.IsDigit).ToArray());

            // Limita o comprimento do CEP a no máximo 8 dígitos
            if (cep.Length > 8)
                cep = cep.Substring(0, 8);

            // Variável para armazenar o CEP formatado
            string formattedCep = string.Empty;

            // Adiciona os primeiros 5 dígitos
            if (cep.Length > 0)
                formattedCep += cep.Substring(0, Math.Min(5, cep.Length));

            // Adiciona o traço e os últimos 3 dígitos
            if (cep.Length > 5)
                formattedCep += "-" + cep.Substring(5, Math.Min(3, cep.Length - 5));

            // Define o texto do TextBox como o CEP formatado
            txtCEP.Text = formattedCep;

            // Ajusta a posição do cursor para o final do texto
            txtCEP.SelectionStart = formattedCep.Length;

        }

        private void txtStreet_TextChanged(object sender, EventArgs e)
        {
            txtStreet.BackColor = ColorTranslator.FromHtml(default);
        }

        private void txtNumber_TextChanged(object sender, EventArgs e)
        {
            txtNumber.BackColor = ColorTranslator.FromHtml(default);
        }

        private void txtNeighborhood_TextChanged(object sender, EventArgs e)
        {
            txtNeighborhood.BackColor = ColorTranslator.FromHtml(default);
        }

        private void txtCity_TextChanged(object sender, EventArgs e)
        {
            txtCity.BackColor = ColorTranslator.FromHtml(default);
        }

        private void txtState_TextChanged(object sender, EventArgs e)
        {
            txtState.BackColor = ColorTranslator.FromHtml(default);
        }

        private void dgvClient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvClient_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Obtendo o ID da linha quando clicado duas vezes dentro do dgvClient.
            int Selected_Id = Convert.ToInt32(dgvClient.Rows[e.RowIndex].Cells[0].Value);

            // Atribui o valor de 'Selected_Id' a variavel global 'idClient'
            idClient = Selected_Id;

            // Ao clicar duas vezes na info da lista irá subir as informações  para as textBox.
            txtCod.Text = Selected_Id.ToString();
            txtName.Text = dgvClient.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtResponsible.Text = dgvClient.Rows[e.RowIndex].Cells[2].Value.ToString();
            cbxTypeContact.Text = dgvClient.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtValueContact.Text = dgvClient.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtCEP.Text = dgvClient.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtStreet.Text = dgvClient.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtNumber.Text = dgvClient.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtNeighborhood.Text = dgvClient.Rows[e.RowIndex].Cells[8].Value.ToString();
            txtCity.Text = dgvClient.Rows[e.RowIndex].Cells[9].Value.ToString();
            txtState.Text = dgvClient.Rows[e.RowIndex].Cells[10].Value.ToString();
            btnSaveClient.Enabled = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new frm_Login().Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSearchCEP_Click(object sender, EventArgs e)
        {
            
        }

        private async void btnSearchCEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCEP.Text))
            {
                txtStreet.Text = string.Empty;
                txtNeighborhood.Text = string.Empty;
                txtCity.Text = string.Empty;
                txtState.Text = string.Empty;
            }
            else
            {
                string cep = txtCEP.Text.Replace("-", "").Trim();

                if (cep.Length == 8)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            string url = $"https://viacep.com.br/ws/{cep}/json/";
                            HttpResponseMessage response = await client.GetAsync(url);

                            if (response.IsSuccessStatusCode)
                            {
                                var json = await response.Content.ReadAsStringAsync();
                                var endereco = JsonConvert.DeserializeObject<Address>(json);

                                if (endereco != null && endereco.cep != null)
                                {
                                    txtStreet.Text = endereco.logradouro;
                                    txtNeighborhood.Text = endereco.bairro;
                                    txtCity.Text = endereco.localidade;
                                    txtState.Text = endereco.estado;
                                }
                                else
                                {
                                    MessageBox.Show("CEP não encontrado.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Erro ao buscar o CEP.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erro: {ex.Message}");
                        }
                    }
                }
            }
        }

        private void btnCadVisit_Click(object sender, EventArgs e)
        {
            new frm_Visit().Show();
            this.Hide();
        }

        public string clientId;

        private void btnMoreContacts_Click(object sender, EventArgs e)
        {

        }

        private void cbxTypeContact_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Limpa o texto sempre que mudar o tipo
            txtValueContact.Clear();

            // Opcional: você pode mudar o MaxLength conforme o tipo
            switch (cbxTypeContact.SelectedIndex)
            {
                case 0:
                    txtValueContact.MaxLength = 14; // (99) 9999-9999
                    break;
                case 1:
                    txtValueContact.MaxLength = 15; // (99) 99999-9999
                    break;
                case 2:
                    txtValueContact.MaxLength = 100;
                    break;
            }
        }

        private void txtValueContact_TextChanged(object sender, EventArgs e)
        {
            int selectedValue = cbxTypeContact.SelectedIndex;

            if (selectedValue == 0 || selectedValue == 1)
            {
                string apenasNumeros = new string(txtValueContact.Text.Where(char.IsDigit).ToArray());

                if (selectedValue == 0)
                {
                    // Formata como (99) 9999-9999
                    if (apenasNumeros.Length <= 10)
                    {
                        txtValueContact.Text = FormatPhone(apenasNumeros);
                    }
                }
                else if (selectedValue == 1)
                {
                    // Formata como (99) 99999-9999
                    if (apenasNumeros.Length <= 11)
                    {
                        txtValueContact.Text = FormatCell(apenasNumeros);
                    }
                }

                // Mantém o cursor no final
                txtValueContact.SelectionStart = txtValueContact.Text.Length;
            }
            else if (selectedValue == 2)
            {
                // Não formata, apenas deixa livre
            }
        }
        
        // Funções auxiliares
        private string FormatPhone(string numbers)
        {
            if (numbers.Length < 3)
                return numbers;
            if (numbers.Length <= 6)
                return $"({numbers.Substring(0, 2)}) {numbers.Substring(2)}";
            if (numbers.Length <= 10)
                return $"({numbers.Substring(0, 2)}) {numbers.Substring(2, 4)}-{numbers.Substring(6)}";

            return numbers;
        }

        private string FormatCell(string numbers)
        {
            if (numbers.Length < 3)
                return numbers;
            if (numbers.Length <= 7)
                return $"({numbers.Substring(0, 2)}) {numbers.Substring(2)}";
            if (numbers.Length <= 11)
                return $"({numbers.Substring(0, 2)}) {numbers.Substring(2, 5)}-{numbers.Substring(7)}";

            return numbers;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            new frm_Login().Show();
            this.Hide();
        }

        private void btnCadUser_Click(object sender, EventArgs e)
        {
            new frm_User().Show();
            this.Hide();
        }
    }
}
