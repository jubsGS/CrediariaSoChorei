using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrediariaSoChorei
{
    public class CCorrente
    {
        public string? numero;
        public double saldo;
        public double limite;
        public bool status;
        public bool especial;

        public bool Sacar(double valores)
        {
            if (saldo - valores > -limite)
            {
                saldo -= valores;
                Transacoes.Add(new Transacao(valores, 'S'));
                return true;
            }
            return false;
        }
        public List<Transacao> Transacoes;
        public bool Depositar(double valores)
        {
            if (valores > 0)
            {
                saldo += valores;
                Transacoes.Add(new Transacao(valores, 'D'));
                return true;
            }
            return false;
        }

        public bool Transferir(CCorrente ContaDestino, double valores)
        {
            if (ContaDestino.status && Sacar(valores) && ContaDestino.Depositar(valores))
            {
                Transacoes[Transacoes.Count - 1].duplicata = ContaDestino.Transacoes[ContaDestino.Transacoes.Count - 1];

                ContaDestino.Transacoes[ContaDestino.Transacoes.Count - 1].duplicata = Transacoes[Transacoes.Count - 1];
                return true;
            }
            return false;
        }

        public CCorrente(string num, double limiteCC) : this()
        {
            this.numero = num;
            this.limite = limiteCC;
        }
        public CCorrente()
        {
            this.saldo = 0;
            this.status = true;
            Transacoes = new List<Transacao>();
        }

    }
}
