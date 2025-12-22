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
            txtClient.Text = _visita.NomeCliente;
            txtIdClient.Text = _visita.IdCliente.ToString();
            txtVisitTitle.Text = _visita.Titulo;
            txtVisitDate.Text = _visita.DataVisita.ToString("dd/MM/yyyy");

            lblVisitId.Text = _visita.Id.ToString();
        }
    }
}
