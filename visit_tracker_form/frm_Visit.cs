using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
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
    public partial class frm_Visit : Form
    {
        public frm_Visit()
        {
            InitializeComponent();
            UpdateDgvClient();
            Load_Clients();
            ShowId();
        }

        private void frm_Visit_Load(object sender, EventArgs e)
        {
            txtResponsible.Text = UserSession.Name;
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

        private void Load_Clients()
        {
            string query = "SELECT id, name FROM clients WHERE is_activated = 1";

            using (MySqlConnection conn = new MySqlConnection(Program.connect))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Configura para mostrar apenas o ID
                        cbxIdClient.DisplayMember = "Key"; // exibe o ID
                        cbxIdClient.ValueMember = "Value"; // guarda o Nome

                        while (reader.Read())
                        {
                            cbxIdClient.Items.Add(
                                new KeyValuePair<int, string>(
                                    reader.GetInt32("id"),    // Key → ID
                                    reader.GetString("name")  // Value → Nome
                                )
                            );
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar informações em campo Cliente: " + ex.Message);
                }
            }
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
                    string query = "SELECT id, name FROM clients WHERE is_activated = 1;";

                    // Cria um MySqlCommand para executar a consulta 
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Usa um MySqlAdapter para preencher um DataTable com os resultados da consulta
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Renomeia as colunas conforme necessário, verificando se cada coluna exite antes de renomar
                            if (dt.Columns.Contains("id"))
                                dt.Columns["id"].ColumnName = "Id";
                            if (dt.Columns.Contains("name"))
                                dt.Columns["name"].ColumnName = "Nome";

                            // Define o DataTable como a fonte de dados do DataGridView chamado "dgvClient"
                            dgvClient.DataSource = dt;

                            // Define o tamanho de cada coluna como automático
                            dgvClient.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("", ex.Message);
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

        private void btnAddVisit_Click(object sender, EventArgs e)
        {

        }

        private void cbxIdClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxIdClient.SelectedItem is KeyValuePair<int, string> cliente)
            {
                txtNameClient.Text = cliente.Value; // Nome do cliente
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

            // Variável para armazenar o CPF formatado
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
    }
}
