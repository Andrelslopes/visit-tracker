using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace visit_tracker
{
    internal class AppEnums
    {
        public enum UserType
        {
            [Description("Operador")]
            Operador,
            [Description("Administrador")]
            Administrador
        }

        public enum ContactType
        {
            [Description("Telefone")]
            Telefone,
            [Description("Celular")]
            Celular,
            [Description("Email")]
            Email
        }

        public enum VisitStatus
        {
            [Description("Agendado")]
            Agendado,
            [Description("Em Andamento")]
            EmAndamento,
            [Description("Concluído")]
            Concluido,
            [Description("Cancelado")]
            Cancelado
        }

        public enum VisitType
        {
            [Description("Residencial")]
            Residencial,
            [Description("Comercial")]
            Comercial,
            [Description("Industrial")]
            Industrial
        }

        public enum FilterProduct
        {
            [Description("Id")]
            Id,
            [Description("Fabricante")]
            Fabricante,
            [Description("Modelo")]
            Modelo,
            [Description("Descrição")]
            Descricao
        }
    }
}
