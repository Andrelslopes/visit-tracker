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
    public partial class frm_Visit : Form
    {
        public frm_Visit()
        {
            InitializeComponent();
        }

        private void frm_Visit_Load(object sender, EventArgs e)
        {

        }

        private void txtCod_TextChanged(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(Program.connect))
            {
                // Abre a conexão com o banco de dados MySQL
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string query = "SELECT id, name FROM clients WHERE id =  @Codigo";

                try
                {
                    // Cria um MySqlCommand para executar a consulta 
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Codigo", txtCod.Text);

                        using(MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtClient.Text = reader["name"].ToString();
                            }
                            else
                            {
                                txtClient.Text = string.Empty;

                                MessageBox.Show("Codigo não encontrado");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro:\n" + ex.Message + "\n" + ex.StackTrace, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        private void txtClient_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
