using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using System.Xml.Linq;
using visit_tracker_form;

namespace visit_tracker
{
    public partial class frm_Visit : Form
    {
        public frm_Visit()
        {
            InitializeComponent();
            //UpdateDgvClient(); dgvClient foi retirado do formulário por enquanto.
            LoadClients();
            ShowId();
        }

        private void clearAllVisits()
        {
            ShowId();
            loadDate();
            cbxIdClient.SelectedIndex = -1;
            txtNameClient.Text = string.Empty;
            txtResponsible.Text = string.Empty;
            txtResponsible.Text = UserSession.Name;
            txtTitle.Text = string.Empty;
            txtDescription.Text = string.Empty;
            listVisits.Items.Clear();
        }

        private void frm_Visit_Load(object sender, EventArgs e)
        {
            // Resgata o nome do usuário que foi logado no sistema
            txtResponsible.Text = UserSession.Name;
            // Carrega a data atual dentro do textbox 'txtDateVisit'
            loadDate();

            //evita que o evento SelectedIndexChanged dispare
            //logo no carregamento e tente buscar visitas sem cliente.
            cbxIdClient.SelectedIndex = -1; // nada selecionado inicialmente
            cbxIdClient.DropDownStyle = ComboBoxStyle.DropDown; // permite digitar para buscar

            listVisits.Items.Clear();
        }
        private void loadDate()
        {
            txtDateVisit.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
                string countId = "SELECT MAX(id) + 1 AS proximo_id FROM visits;";

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

        // -----------------------------
        // Carrega o ComboBox de clientes
        // -----------------------------
        private void LoadClients()
        {
            string query = "SELECT id, name FROM clients WHERE is_activated = 1";

            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(Program.connect))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
            {
                adapter.Fill(dt);
            }

            cbxIdClient.DataSource = dt;
            cbxIdClient.DisplayMember = "id"; // texto visível
            cbxIdClient.ValueMember = "id";   // valor real (ID)
            cbxIdClient.SelectedIndex = -1;   // nada selecionado inicialmente
        }

        // ----------------------------- DESTIVADO TEMPORARIAMENTE -----------------------------
        // Carrega o DataGridView de clientes
        // -----------------------------

        /*
        private void UpdateDgvClient()
        {
            string query = "SELECT id, name FROM clients WHERE is_activated = 1";

            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(Program.connect))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
            {
                adapter.Fill(dt);
            }
            // Ajuste nomes das colunas se quiser
            if (dt.Columns.Contains("id"))
                dt.Columns["id"].ColumnName = "Id";
            if (dt.Columns.Contains("name"))
                dt.Columns["name"].ColumnName = "Nome";

            dgvClient.DataSource = dt;
            dgvClient.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        */

        private void cbxIdClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxIdClient.SelectedIndex < 0 || cbxIdClient.SelectedItem == null)
            {
                txtNameClient.Text = string.Empty;
                listVisits.Items.Clear();
                return;
            }

            cbxIdClient.BackColor = ColorTranslator.FromHtml(default);

            txtTitle.Clear();
            txtDescription.Clear();

            // Obtém o DataRowView do item selecionado
            DataRowView drvClient = (DataRowView)cbxIdClient.SelectedItem;
            // Extrai o ID e o nome do cliente
            int clientId = Convert.ToInt32(drvClient["id"]);
            string clientName = drvClient["name"].ToString();
            // Preenche o TextBox com o nome do cliente
            txtNameClient.Text = clientName;

            // Carrega as visitas do cliente selecionado
            CarregarVisitas(clientId);

            loadDate();
            ShowId();
            txtResponsible.Text = UserSession.Name;
        }


        private void queryBudget(int visitId, ListBox listProp)
        {
            using (MySqlConnection conn = new MySqlConnection(Program.connect))
            {
                try
                {
                    conn.Open();
                    
                    string query = @"SELECT 
                        b.id, 
                        b.visit_id, 
                        b.user_id,
                        b.total_value,
                        b.description,
                        b.status
                        v.date_visit
                    FROM budgets b
                    INNER JOIN visits v sssON b.visit_id = v.id
                    WHERE v.id = @id_visit"; ;
                    
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_visit", visitId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            listProp.Items.Clear();

                            while (reader.Read())
                            {
                                // Processa os dados aqui.
                                // Cria um objeto anônimo para guardar os dados.
                                var BudgetQuery = new BudgetQuery
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    IdVisit = Convert.ToInt32(reader["visit_id"]),
                                    IdUser = Convert.ToInt32(reader["user_id"]),
                                    TotalValue = Convert.ToDouble(reader["total_value"]),
                                    Description = reader["description"].ToString(),
                                    Status = reader["status"].ToString(),
                                    VisitDate = Convert.ToDateTime(reader["date_visit"])
                                };
                                
                                // Adiciona o objeto, não a string!
                                listProp.Items.Add(BudgetQuery);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar orçamentos: " + ex.Message);
                }
            }
        }

        private void CarregarVisitas(int clientId)
        {
            using (MySqlConnection conn = new MySqlConnection(Program.connect))
            {
                try
                {
                    conn.Open();

                    string query = @"SELECT 
                                        v.id AS visit_id,
                                        v.title,
                                        v.description,
                                        v.id_client,
                                        c.name AS client_name,
                                        u.id AS responsible_id,
                                        u.name AS responsible,
                                        v.date_visit
                                    FROM visits v
                                    INNER JOIN users u ON v.responsible = u.id
                                    INNER JOIN clients c ON c.id = v.id_client
                                    WHERE v.id_client = @id_client";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_client", clientId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            listVisits.Items.Clear();

                            while (reader.Read())
                            {
                                // Cria um objeto anônimo para guardar os dados
                                var visita = new Visits
                                {
                                    Id = Convert.ToInt32(reader["visit_id"]),
                                    Titulo = reader["title"].ToString(),
                                    Descricao = reader["description"].ToString(),

                                    IdCliente = Convert.ToInt32(reader["id_client"]),
                                    NomeCliente = reader["client_name"].ToString(),

                                    Responsavel = reader["responsible"].ToString(),
                                    IdResponsavel = Convert.ToInt32(reader["responsible_id"]),
                                    DataVisita = Convert.ToDateTime(reader["date_visit"])
                                };

                                // Adiciona o objeto, não a string!
                                listVisits.Items.Add(visita);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar visitas: " + ex.Message);
                }
            }
        }

        private void txtDateVisit_TextChanged(object sender, EventArgs e)
        {
            /*FORMATAÇAO DO TEXTBOX txtBirth*/
            // Obtém o texto do TextBox
            string dateBirth = txtDateVisit.Text;

            // Filtra apenas os dígitos do texto, removendo qualquer caractere não numérico
            dateBirth = new string(dateBirth.Where(char.IsDigit).ToArray());

            // Limita o comprimento da data a no máximo 10 dígitos
            if (dateBirth.Length > 8)
                dateBirth = dateBirth.Substring(0, 8);

            // Variável para armazenar a data formatada
            string formattedBirth = string.Empty; // 00/00/0000

            // Adiciona os primeiros 2 dígitos
            if (dateBirth.Length > 0)
                formattedBirth += dateBirth.Substring(0, Math.Min(2, dateBirth.Length));

            // Adiciona o segundo grupo de 3 dígitos com um ponto na frente
            if (dateBirth.Length > 2)
                formattedBirth += "/" + dateBirth.Substring(2, Math.Min(2, dateBirth.Length - 2));

            // Adiciona o terceiro grupo de 3 dígitos com outro ponto na frente
            if (dateBirth.Length > 4)
                formattedBirth += "/" + dateBirth.Substring(4, Math.Min(4, dateBirth.Length - 4));

            // Define o texto do TextBox como o CPF formatado
            txtDateVisit.Text = formattedBirth;

            // Ajusta a posição do cursor para o final do texto
            txtDateVisit.SelectionStart = formattedBirth.Length;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // -----------------------------
        // Evento ao clicar em uma linha do DataGridView
        // -----------------------------
        /*
        private void dgvClient_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvClient.Rows[e.RowIndex];

                // Seleciona no ComboBox pelo ID
                cbxIdClient.SelectedValue = row.Cells["Id"].Value;

                // Preenche o TextBox com o nome do cliente
                txtNameClient.Text = row.Cells["Nome"].Value?.ToString();
            }
            loadDate();
            ShowId();
            txtResponsible.Text = UserSession.Name;
        }
        */
        private void dgvClient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvClient_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listVisits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listVisits.SelectedItem is Visits visita)
            {
                txtId.Text = visita.Id.ToString();
                txtResponsible.Tag = visita.IdResponsavel;
                txtResponsible.Text = visita.Responsavel;// mostra o nome
                txtDateVisit.Text = visita.DataVisita.ToString("dd/MM/yyyy");
                txtTitle.Text = visita.Titulo;
                txtDescription.Text = visita.Descricao;
            }
        }

        private void btnAddVisit_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(Program.connect))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                // Query para consultar se existem mais de um id especifico no banco de dados.
                string checkIdQuery = "SELECT COUNT(*) FROM visits WHERE id = @Id";
                // Cria comandos MySQL separados para cada query
                MySqlCommand checkIdCmd = new MySqlCommand(checkIdQuery, conn);
                // Adiciona os valores dos parâmetros @Id
                checkIdCmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = txtId.Text;
                // Executa as queries e converte o resultado para inteiros
                int idExists = Convert.ToInt32(checkIdCmd.ExecuteScalar());
                //
                if (idExists > 0) // Se "idExists" for maior que '0' indica que o id já existe no banco de dados.
                {
                    MessageBox.Show("Id da visita já cadastrado no sistema! \n " +
                        "Por favor verifique.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    string errorMessage = string.Empty;

                    if (string.IsNullOrWhiteSpace(txtId.Text))
                    {
                        errorMessage += "Id: \n";
                        txtId.BackColor = ColorTranslator.FromHtml("#FEC6C6");
                    }

                    if (string.IsNullOrWhiteSpace(cbxIdClient.Text))
                    {
                        errorMessage += "Id Cliente: \n";
                        cbxIdClient.BackColor = ColorTranslator.FromHtml("#FEC6C6");
                    }

                    if (string.IsNullOrWhiteSpace(txtNameClient.Text))
                    {
                        errorMessage += "Nome Cliente: \n";
                        txtNameClient.BackColor = ColorTranslator.FromHtml("#FEC6C6");
                    }
                    if (string.IsNullOrWhiteSpace(txtResponsible.Text))
                    {
                        errorMessage += "Responsavel: \n";
                        txtResponsible.BackColor = ColorTranslator.FromHtml("#FEC6C6");
                    }
                    if (string.IsNullOrWhiteSpace(txtDateVisit.Text))
                    {
                        errorMessage += "Data Visita: \n";
                        txtDateVisit.BackColor = ColorTranslator.FromHtml("#FEC6C6");
                    }

                    else if (!DateTime.TryParse(txtDateVisit.Text, out DateTime dataDigitada))
                    {
                        errorMessage += "Data Visita (inválida): \n";
                        txtDateVisit.BackColor = ColorTranslator.FromHtml("#FEC6C6");
                    }
                    else if (dataDigitada.Date != DateTime.Today)
                    {
                        errorMessage += "Data Visita (deve ser a data de hoje): \n";
                        txtDateVisit.BackColor = ColorTranslator.FromHtml("#FEC6C6");
                    }

                    if (string.IsNullOrWhiteSpace(txtTitle.Text))
                    {
                        errorMessage += "Titulo: \n";
                        txtTitle.BackColor = ColorTranslator.FromHtml("#FEC6C6");
                    }

                    if (string.IsNullOrWhiteSpace(txtDescription.Text))
                    {
                        errorMessage += "Descrição: \n";
                        txtDescription.BackColor = ColorTranslator.FromHtml("#FEC6C6");
                    }
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        MessageBox.Show($"Os seguintes campos são obrigatórios ou inválidos:\n\n{errorMessage}",
                            "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        try
                        {
                            // Obtem o id do usuario logado e armazena na variavel UserId
                            int UserId = UserSession.Id;

                            // Inicia a inseção na tabela 'visit'
                            string queryVisit = "INSERT INTO visits(id_client, responsible, date_visit, title, description, created_by, updated_by ) VALUES (@id_Client, @Responsible, @Date_Visit, @Title, @Description, @Created_by, @Updated_by)";

                            // Cria um novo comando MySqlCommand para inserir os dados na tabela 'clients'
                            using (MySqlCommand cmd = new MySqlCommand(queryVisit, conn))
                            {
                                cmd.Parameters.AddWithValue("@id_Client", cbxIdClient.Text);
                                cmd.Parameters.AddWithValue("@Responsible", UserId);
                                cmd.Parameters.AddWithValue("@Date_Visit", DateTime.Parse(txtDateVisit.Text));
                                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                                cmd.Parameters.AddWithValue("@Created_by", UserId);
                                cmd.Parameters.AddWithValue("@Updated_by", UserId);

                                // Executa o comando SQL para inserção na tabela 'users'
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Dados adicionados com Sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ShowId();
                                txtTitle.Text = string.Empty;
                                txtDescription.Text = string.Empty;


                                // Atualiza automaticamente a lista chamando o método
                                if (cbxIdClient.SelectedItem is KeyValuePair<int, string> cliente)
                                {
                                    CarregarVisitas(cliente.Key);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erro ao inserir no banco:\n" + ex.Message, "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Selecione uma visita para atualizar.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(Program.connect))
            {
                string errorMessage = string.Empty;

                if (string.IsNullOrWhiteSpace(txtId.Text))
                {
                    errorMessage += "Id: \n";
                    txtId.BackColor = ColorTranslator.FromHtml("#FEC6C6");
                }

                if (string.IsNullOrWhiteSpace(cbxIdClient.Text))
                {
                    errorMessage += "Id Cliente: \n";
                    cbxIdClient.BackColor = ColorTranslator.FromHtml("#FEC6C6");
                }

                if (string.IsNullOrWhiteSpace(txtNameClient.Text))
                {
                    errorMessage += "Nome Cliente: \n";
                    txtNameClient.BackColor = ColorTranslator.FromHtml("#FEC6C6");
                }
                if (string.IsNullOrWhiteSpace(txtResponsible.Text))
                {
                    errorMessage += "Responsavel: \n";
                    txtResponsible.BackColor = ColorTranslator.FromHtml("#FEC6C6");
                }
                if (string.IsNullOrWhiteSpace(txtDateVisit.Text))
                {
                    errorMessage += "Data Visita: \n";
                    txtDateVisit.BackColor = ColorTranslator.FromHtml("#FEC6C6");
                }

                else if (!DateTime.TryParse(txtDateVisit.Text, out DateTime dataDigitada))
                {
                    errorMessage += "Data Visita (inválida): \n";
                    txtDateVisit.BackColor = ColorTranslator.FromHtml("#FEC6C6");
                }
                else if (dataDigitada.Date != DateTime.Today)
                {
                    errorMessage += "Data Visita (deve ser a data de hoje): \n";
                    txtDateVisit.BackColor = ColorTranslator.FromHtml("#FEC6C6");
                }

                if (string.IsNullOrWhiteSpace(txtDescription.Text))
                {
                    errorMessage += "Id Cliente: \n";
                    txtDescription.BackColor = ColorTranslator.FromHtml("#FEC6C6");
                }
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    MessageBox.Show($"Os seguintes campos são obrigatórios ou inválidos:\n\n{errorMessage}",
                        "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    try
                    {
                        conn.Open();

                        string query = @"UPDATE visits 
                             SET title = @title,
                                 description = @description,
                                 responsible = @responsible,
                                 date_visit = @date_visit
                             WHERE id = @id";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                            cmd.Parameters.AddWithValue("@title", txtTitle.Text);
                            cmd.Parameters.AddWithValue("@description", txtDescription.Text);
                            cmd.Parameters.AddWithValue("@responsible", (int)txtResponsible.Tag); // ID do responsável
                            cmd.Parameters.AddWithValue("@date_visit", DateTime.Parse(txtDateVisit.Text));

                            int linhasAfetadas = cmd.ExecuteNonQuery();

                            if (linhasAfetadas > 0)
                                MessageBox.Show("Visita atualizada com sucesso!");
                            else
                                MessageBox.Show("Não foi possível atualizar a visita.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao atualizar visita: " + ex.Message);
                    }
                }
            }

            // Recarrega a lista para atualizar o ListBox
            if (cbxIdClient.SelectedItem is KeyValuePair<int, string> cliente)
            {
                CarregarVisitas(cliente.Key);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearAllVisits();
            ShowId();
        }

        private void btnDate_Click(object sender, EventArgs e)
        {
            loadDate();
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            txtId.BackColor = ColorTranslator.FromHtml(default);
        }

        private void txtNameClient_TextChanged(object sender, EventArgs e)
        {
            txtNameClient.BackColor = ColorTranslator.FromHtml(default);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnCadLogin_Click(object sender, EventArgs e)
        {
            new frm_Login().Show();
            this.Hide();
        }

        private void btnCadUser_Click(object sender, EventArgs e)
        {
            new frm_User().Show();
            this.Hide();
        }

        private void btnCadClient_Click(object sender, EventArgs e)
        {
            new frm_Client().Show();
            this.Hide();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void BuscarClientePorId()
        {
            if (string.IsNullOrWhiteSpace(cbxIdClient.Text))
                return;

            if (!int.TryParse(cbxIdClient.Text, out int idDigitado))
            {
                MessageBox.Show("Digite um ID válido.");
                return;
            }

            for (int i = 0; i < cbxIdClient.Items.Count; i++)
            {
                DataRowView drv = (DataRowView)cbxIdClient.Items[i];

                if (Convert.ToInt32(drv["id"]) == idDigitado)
                {
                    cbxIdClient.SelectedIndex = i;
                    return; // achou, sai
                }
            }

            MessageBox.Show("Cliente não encontrado.");
        }


        // O método cbxIdClient_KeyDown está correto, pois KeyEventArgs possui KeyCode.
        // Não é necessário alterar esse método.

        private void cbxIdClient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                BuscarClientePorId();
                e.SuppressKeyPress = true; // evita beep ou comportamento estranho
            }
        }

        // Substitua o método cbxIdClient_Leave para usar EventArgs corretamente.
        // O evento Leave não possui KeyCode, então remova a verificação de tecla.

        private void cbxIdClient_Leave(object sender, EventArgs e)
        {
            BuscarClientePorId();
        }

        private void btnNewProp_Click(object sender, EventArgs e)
        {
            Visits visita = listVisits.SelectedItem as Visits;

            if (visita == null)
            {
                MessageBox.Show("Selecione uma visita.");
                return;
            }

            frm_Prop frm = new frm_Prop(visita);
            frm.ShowDialog();

            /*
            Visits visit = listVisits.SelectedItem as Visits;

            if (listVisits.SelectedItem is Visits visita)
            {
                new frm_Prop().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Selecione uma visita para ver os orçamentos.");
                return;
            }*/
        }

        private void btnCadProduct_Click(object sender, EventArgs e)
        {
            new frm_Prod().Show();
            this.Hide();
        }
    }
}
