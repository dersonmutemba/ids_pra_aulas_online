using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID_Finder
{

    public delegate void Notificar();

    class Events
    {

        public event Notificar ProcessoCompleto;
        
        public void IniciarProcesso()
        {
            Console.WriteLine("Processo iniciou.");

        }

    }
}
