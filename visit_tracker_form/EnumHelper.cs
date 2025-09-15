using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace visit_tracker
{
    internal class EnumHelper
    {
        // Método que retorna a descrição de um valor enum, se existir
        public static string GetDescription(Enum value)
        {
            // Pega as informações do campo do enum (por exemplo: "Masculino", "Gerente", etc.)
            FieldInfo fi = value.GetType().GetField(value.ToString());

            // Procura pelo atributo [Description("texto")] nesse campo
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            // Se encontrou a descrição, retorna ela
            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                // Se não encontrou, retorna o nome original do enum (ex: "Masculino")
                return value.ToString();
        }
    }
}
