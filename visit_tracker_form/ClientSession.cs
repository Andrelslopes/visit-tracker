using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace visit_tracker
{
    internal class ClientSession
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        // Isso ajuda a exibir o nome no ListBox, mas manter o Id "escondido"
        public override string ToString()
        {
            return $"{Id} | {Nome}";
        }
    }
}
