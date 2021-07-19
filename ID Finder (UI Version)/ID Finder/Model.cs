using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID_Finder
{
    class Model
    {

        private long id, inicio, fim;
        private int dia;
        private string aula, presenca;

        public Model(long id,
                     string aula,
                     int dia,
                     long inicio,
                     long fim,
                     string presenca)
        {
            this.id = id;
            this.aula = aula;
            this.dia = dia;
            this.inicio = inicio;
            this.fim = fim;
            this.presenca = presenca;
        }

        public long ID { get => id; set => id = value; }

        public string Aula { get => aula; set => aula = value; }

        public string Presenca { get => presenca; set => presenca = value; }

        public int Dia { get => dia; set => dia = value; }

        public long Inicio { get => inicio; set => inicio = value; }

        public long Fim { get => fim; set => fim = value; }

    }
}
