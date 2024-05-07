using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrediariaSoChorei
{
    public class Transacao
    {
        public double? valor;
        public char? tipo;
        public Transacao? duplicata;
        public Transacao(double valores, char tipo)
        {
            this.valor = valores;
            this.tipo = tipo;
        }
    }
}
