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
    public partial class frm_Add_Contacts : Form
    {
        public frm_Add_Contacts()
        {
            InitializeComponent();
            enumContactType();
            //UpdateDgvContact();
        }

        private void UpdateDgvContact()
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
                            cl.id,
                            cl.name,
                            ct.responsible,
                            ct.type,
                            ct.value_type
                        FROM clients cl
                        JOIN client_contacts ct ON cl.id = fk_client_id
                        WHERE is_activated = 1;";

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
                                dt.Columns["id"].ColumnName = "Id Cliente";

                            if (dt.Columns.Contains("name"))
                                dt.Columns["name"].ColumnName = "Nome Cliente";

                            if (dt.Columns.Contains("responsible"))
                                dt.Columns["responsible"].ColumnName = "Responsável";

                            if (dt.Columns.Contains("type"))
                                dt.Columns["type"].ColumnName = "Tipo";

                            if (dt.Columns.Contains("value_type"))
                                dt.Columns["value_type"].ColumnName = "Contato";
                            
                            // Define o DataTable como a fonte de dados do DataGridView chamado "dgvContacts"
                            dgvContacts.DataSource = dt;

                            // Define o tamanho de cada coluna como automático
                            dgvContacts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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

        private void frm_Add_Contacts_Load(object sender, EventArgs e)
        {

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

        private void btnSaveContact_Click(object sender, EventArgs e)
        {

        }

        private void btnEditContact_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteContact_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            
        }

        private void AtualizarLista()
        {

        }
    }
}
