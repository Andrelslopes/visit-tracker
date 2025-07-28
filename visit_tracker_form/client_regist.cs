using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
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
            UpdateDgvClient();
        }

        private void client_regist_Load(object sender, EventArgs e)
        {
            label9.Text = string.Empty;
            label9.Text = $"Olá Seja Bem Vindo {UserSession.Name}.";
        }

        private void ClearAll()
        {
            txtCod.Text = string.Empty;
            txtName.Text = string.Empty;
            txtCEP.Text = string.Empty;
            txtStreet.Text = string.Empty;
            txtNumber.Text = string.Empty;
            txtNeighborhood.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtState.Text = string.Empty;
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
                    string query = "SELECT c. id, c.name , a.postal_code, a.street, a.number, a.neighborhood, a.city, a.state FROM addresses a LEFT JOIN clients c ON a.fk_id_client = c.id;";

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
        
        private void btnSaveClient_Click(object sender, EventArgs e)
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

                        int fkClietId;

                        string lastId = "SELECT LAST_INSERT_ID()";

                        using(MySqlCommand cmd = new MySqlCommand(lastId, conn, transation))
                        {
                            fkClietId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Obter o valor da chave primária da tabela 'Clients'

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
                            cmd.Parameters.AddWithValue("@Created_by",UserId);
                            cmd.Parameters.AddWithValue("@Updated_by",UserId);

                            // Executa o comando SQL para inserção na tabela 'users'.
                            cmd.ExecuteNonQuery();
                        }

                        // Se tudo correr bem, commit a transação.
                        transation.Commit();

                        // Exibe uma mensagem de sucesso
                        MessageBox.Show("Dados criados com Sucesso!", "Sucesso", MessageBoxButtons.OK);

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
                        string queryClients = "UPDATE clients SET name=@Name, created_by=@Created_by, updated_by=@Updated_by WHERE id=@Id";

                        using (MySqlCommand cmd = new MySqlCommand(queryClients, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Name", txtName.Text);
                            cmd.Parameters.AddWithValue("@Created_by", userId);
                            cmd.Parameters.AddWithValue("@Updated_by", userId);
                            cmd.Parameters.AddWithValue("@Id", clientId);

                            cmd.ExecuteNonQuery();
                        }

                        // Atualiza o endereço relacionado ao cliente
                        string queryAddresses = @"UPDATE addresses SET postal_code=@Postal_code, street=@Street, number=@Number, neighborhood=@Neighborhood, city=@City, state=@State, updated_by=@Updated_by WHERE fk_id_client=@Fk_id_client";

                        using (MySqlCommand cmd = new MySqlCommand(queryAddresses, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Postal_code", txtCEP.Text);
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

        private void btnDeleteClient_Click(object sender, EventArgs e)
        {

        }

        private void btnClearClient_Click(object sender, EventArgs e)
        {

        }

        private void txtCEP_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtCod_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCEP_TextChanged_1(object sender, EventArgs e)
        {
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

        }

        private void txtNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNeighborhood_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCity_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtState_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvClient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvClient_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Ao clicar duas vezes na info da lista irá subir as informações  para as textBox.
            txtCod.Text = dgvClient.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvClient.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCEP.Text = dgvClient.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtStreet.Text = dgvClient.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtNumber.Text = dgvClient.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtNeighborhood.Text = dgvClient.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtCity.Text = dgvClient.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtState.Text = dgvClient.Rows[e.RowIndex].Cells[7].Value.ToString();
            btnSaveClient.Enabled = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void btnSearchCEP_Click(object sender, EventArgs e)
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
    }
}
