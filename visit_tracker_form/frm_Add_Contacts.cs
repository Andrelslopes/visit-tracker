using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace visit_tracker
{
    public partial class frm_Add_Contacts : Form
    {
        public frm_Add_Contacts()
        {
            InitializeComponent();
            enumContactType();
        }

        private void frm_Add_Contacts_Load(object sender, EventArgs e)
        {

        }

        private void enumContactType()
        {
            // Obtém todos os valores do enum Gender e os converte para uma coleção do tipo Gender
            var values = Enum.GetValues(typeof(AppEnums.ContactType)).Cast<AppEnums.ContactType>();

            // Para cada valor do enum (ex: Masculino, Feminino, Outros)
            foreach (var value in values)
            {
                // Usa o helper para pegar a descrição (ex: "Feminino" ao invés de "Feminino", se estiver com [Description])
                string description = EnumHelper.GetDescription(value);

                // Adiciona um objeto anônimo ao ComboBox com:
                // Text -> o que aparece para o usuário
                // Value -> o valor real do enum
                cbxTypeContact.Items.Add(new { Text = description, Value = value });
            }

            // Define que o ComboBox deve mostrar o campo "Text" dos itens
            cbxTypeContact.DisplayMember = "Text";

            // Define que o ComboBox deve considerar o campo "Value" como valor selecionado
            cbxTypeContact.ValueMember = "Value";

            // Nenhum item será selecionado inicialmente
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
            Application.Exit();
        }
    }
}
