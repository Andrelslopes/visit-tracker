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
using visit_tracker_form;

namespace visit_tracker
{
    public partial class frm_Prop : Form
    {
        private Visits _visita;

        // 🔹 construtor com parâmetro
        public frm_Prop(Visits visita)
        {
            InitializeComponent();
            _visita = visita;
        }

        private void frm_Prop_Load(object sender, EventArgs e)
        {
            // Preenche os campos com os dados da visita recebida
            txtClient.Text = _visita.NomeCliente;
            txtIdClient.Text = _visita.IdCliente.ToString();
            txtVisitTitle.Text = _visita.Titulo;
            txtVisitDate.Text = _visita.DataVisita.ToString("dd/MM/yyyy");
            lblVisitId.Text = _visita.Id.ToString();

            ShowId();

            // Deixa os campos de cliente e visita como somente leitura
            txtClient.ReadOnly = true;
            txtIdClient.ReadOnly = true;
            txtVisitTitle.ReadOnly = true;
            txtVisitDate.ReadOnly = true;

            txtDateProp.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void txtIdClient_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtClient_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtVisitDate_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtVisitTitle_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtIdProp_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDateProp_TextChanged(object sender, EventArgs e)
        {
            /*FORMATAÇAO DO TEXTBOX txtBirth*/
            // Obtém o texto do TextBox
            string dateBirth = txtDateProp.Text;

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
            txtDateProp.Text = formattedBirth;

            // Ajusta a posição do cursor para o final do texto
            txtDateProp.SelectionStart = formattedBirth.Length;
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 1. Validação de Dados (UI) antes de abrir a conexão
            string errorMessage = "";
            DateTime parsedDate = DateTime.MinValue;

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
                errorMessage += "- O campo 'Título' é obrigatório.\n";
            txtTitle.BackColor = string.IsNullOrWhiteSpace(txtTitle.Text) ? Color.LightYellow : Color.White;

            // Validação da Data
            if (!DateTime.TryParseExact(txtDateProp.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate))
                errorMessage += "- Data inválida. Use o formato dd/MM/yyyy.\n";

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
                errorMessage += "- O campo 'Descrição' é obrigatório.\n";
            txtDescription.BackColor = string.IsNullOrWhiteSpace(txtDescription.Text) ? Color.LightYellow : Color.White;

            // Se houver erros, mostra tudo de uma vez e para
            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show("Corrija os seguintes erros:\n\n" + errorMessage, "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Definir o Valor do Status via RadioButtons
            // Usando a técnica de Inteiros para o Banco de Dados
            int statusValor = 1; // Padrão: Pendente
            if (rbProgress.Checked) statusValor = 2;
            else if (rbApproved.Checked) statusValor = 3;
            else if (rbRejected.Checked) statusValor = 4;

            // 3. Persistência no Banco
            // Abrir conexão e inserir os dados
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Program.connect))
                {
                    conn.Open();

                    // Verificar duplicidade
                    string checkQuery = "SELECT COUNT(*) FROM budgets WHERE id = @id";
                    using (MySqlCommand cmdCheck = new MySqlCommand(checkQuery, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@id", txtIdProp.Text);
                        if (Convert.ToInt32(cmdCheck.ExecuteScalar()) > 0)
                        {
                            MessageBox.Show("Registro já existe com este ID.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Inserção
                    string insertQuery = @"INSERT INTO budgets (visit_id, user_id, title, description, budget_date, status_id, total_value, created_by, updated_by)
                                 VALUES (@visit_id, @user_id, @title, @description, @budget_date, @status_id, @total_value, @created_by, @updated_by)";

                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@visit_id", _visita.Id);
                        insertCmd.Parameters.AddWithValue("@user_id", UserSession.Id);
                        insertCmd.Parameters.AddWithValue("@title", txtTitle.Text);
                        insertCmd.Parameters.AddWithValue("@description", txtDescription.Text);

                        // A 'parsedDate' já é um objeto DateTime, o Driver do MySQL converte para o formato correto automaticamente
                        insertCmd.Parameters.AddWithValue("@budget_date", parsedDate);

                        // Salvando o valor numérico do RadioButton
                        insertCmd.Parameters.AddWithValue("@status_id", statusValor);

                        insertCmd.Parameters.AddWithValue("@total_value", 0);
                        insertCmd.Parameters.AddWithValue("@created_by", UserSession.Id);
                        insertCmd.Parameters.AddWithValue("@updated_by", UserSession.Id);

                        insertCmd.ExecuteNonQuery();
                        MessageBox.Show("Orçamento salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message, "Erro no Banco", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ShowId();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ShowId();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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
                string countId = "SELECT MAX(id) + 1 AS proximo_id FROM budgets;";

                // Cria um comando MySQL com a consulta SQL
                using (MySqlCommand cmd = new MySqlCommand(countId, conn))
                {
                    // Executa a consulta e obtém o resultado
                    object result = cmd.ExecuteScalar();

                    // Verifica se o resultado não é nulo
                    if (result != DBNull.Value)
                    {
                        // Converte o resultado para string e define o texto no controle TxtIdUser
                        txtIdProp.Text = result.ToString();
                    }
                    else
                    {
                        // Se não houver resultado, define o texto como "1"
                        txtIdProp.Text = "1";
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

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
