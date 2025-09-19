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
    }
}
