using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using visit_tracker_form;
using static System.Windows.Forms.LinkLabel;

namespace visit_tracker
{
    public partial class frm_Prod : Form
    {
        public frm_Prod()
        {
            InitializeComponent();
            ShowId();
            UpdateDataGrid();
        }

        private void frm_Prod_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            txtId.BackColor = ColorTranslator.FromHtml(default);
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            txtDescription.BackColor = ColorTranslator.FromHtml(default);
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            txtAmount.BackColor = ColorTranslator.FromHtml(default);
        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            txtValue.BackColor = ColorTranslator.FromHtml(default);
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvProduct_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //evita erro se clicar no cabeçalho ou fora de uma linha válida
            if (e.RowIndex < 0) return;

            try
            {
                // Preenche os TextBoxes com os dados da linha selecionada
                txtId.Text = dgvProduct.CurrentRow.Cells["ID"].Value.ToString();
                txtManufacturer.Text = dgvProduct.CurrentRow.Cells["Fabricante"].Value.ToString();
                txtModel.Text = dgvProduct.CurrentRow.Cells["Modelo"].Value.ToString();
                txtDescription.Text = dgvProduct.CurrentRow.Cells["Descrição"].Value.ToString();
                txtValue.Text = dgvProduct.CurrentRow.Cells["Valor Unit."].Value.ToString();
                txtAmount.Text = dgvProduct.CurrentRow.Cells["Quantidade"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao selecionar a linha: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string errorMessage = "";

            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                errorMessage += "O campo Id é obrigatório.\n";
                txtId.BackColor = Color.LightYellow;
            }

            if (string.IsNullOrWhiteSpace(txtManufacturer.Text))
            {
                errorMessage += "O campo Fabricante é obrigatório.\n";
                txtManufacturer.BackColor = Color.LightYellow;
            }

            if (string.IsNullOrEmpty(txtModel.Text))
            {
                errorMessage += "O campo Modelo é obrigatório. \n";
                txtModel.BackColor = Color.LightYellow;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                errorMessage += "O campo Descrição é obrigatório.\n";
                txtDescription.BackColor = Color.LightYellow;
            }

            if (string.IsNullOrWhiteSpace(txtValue.Text))
            {
                errorMessage += "O campo Valor Unit. é obrigatório.\n";
                txtValue.BackColor = Color.LightYellow;
            }

            if (string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                errorMessage += "O campo Quantidade é obrigatório.\n";
                txtAmount.BackColor = Color.LightYellow;
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage, "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MySqlConnection conn = new MySqlConnection(Program.connect);
                try
                {
                    int UserId = UserSession.Id;

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    string checkIdQuery = "SELECT COUNT(*) FROM products WHERE id = @id";

                    MySqlCommand checkCmd = new MySqlCommand(checkIdQuery, conn);

                    checkCmd.Parameters.AddWithValue("@id", txtId.Text);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("O ID já existe. Por favor, insira um ID único.",
                            "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtId.BackColor = Color.LightYellow;
                        return;
                    }
                    else
                    {
                        txtId.BackColor = ColorTranslator.FromHtml(default);

                        MySqlCommand insertCmd = new MySqlCommand(
                            "INSERT INTO products (manufacturer, model, description, unit_value, amount, created_by, updated_by) VALUES (@manufacturer, @model, @description, @unit_value, @amount, @created_by, @updated_by)", conn);

                        insertCmd.Parameters.Add("@manufacturer", MySqlDbType.VarChar).Value = txtManufacturer.Text;
                        insertCmd.Parameters.Add("@model", MySqlDbType.VarChar).Value = txtModel.Text;
                        insertCmd.Parameters.Add("@description", MySqlDbType.VarChar).Value = txtDescription.Text;
                        insertCmd.Parameters.Add("@unit_value", MySqlDbType.Decimal).Value = Convert.ToDecimal(txtValue.Text);
                        insertCmd.Parameters.Add("@amount", MySqlDbType.Int32).Value = Convert.ToInt32(txtAmount.Text);
                        insertCmd.Parameters.Add("@created_by", MySqlDbType.Int32).Value = UserId;
                        insertCmd.Parameters.Add("@updated_by", MySqlDbType.Int32).Value = UserId;

                        //insertCmd.Parameters.AddWithValue("@updated_by", Environment.UserName); pesquisar como pegar o id do usuario logado
                        insertCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Dados salvos com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    cleartextbox();

                    UpdateDataGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao salvar os dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cleartextbox();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnSearchFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                MessageBox.Show("Arquivo selecionado: " + filePath, "Arquivo Selecionado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            string imagePath = openFileDialog1.FileName;

            txtFilePath.Text = imagePath;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShowId()
        {
            // Cria uma nova conexão com o banco de dados
            MySqlConnection conn = new MySqlConnection(Program.connect);

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    // Abre a conexão
                    conn.Open();
                }

                // Define a consulta SQL para obter o próximo id
                string countId = "SELECT MAX(id) + 1 AS proximo_id FROM products;";

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
                if (conn.State == ConnectionState.Open)
                {
                    // Fecha a conexão
                    conn.Close();
                }
            }
        }

        private void cleartextbox()
        {
            txtId.Text = string.Empty;
            txtManufacturer.Text = string.Empty;
            txtModel.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtValue.Text = string.Empty;
            txtAmount.Text = string.Empty;
            
            ShowId();
        }

        private void UpdateDataGrid()
        {
            using (MySqlConnection conn = new MySqlConnection(Program.connect))
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    string query = "SELECT id AS 'ID', manufacturer AS 'Fabricante', model AS 'Modelo', description AS 'Descrição', unit_value AS 'Valor Unit.', amount AS 'Quantidade' FROM products";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);

                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    dgvProduct.DataSource = dataTable;

                    dgvProduct.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao atualizar o DataGridView: " + ex.Message);
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
    }
}
