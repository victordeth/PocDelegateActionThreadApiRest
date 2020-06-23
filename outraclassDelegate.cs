using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes_Delegate
{
    public class outraclassDelegate
    {
        public delegate void DelegateEscreveLinha(string messageLog);
        public event DelegateEscreveLinha EscreveLinha;

        private void OnEscreveLinha(string messageLog)
        {
            if (EscreveLinha != null)
                EscreveLinha(messageLog);
        }


        public void UsaDelegateAqui(int vezes)
        {

            for (int i = 0; i < vezes; i++)
            {
                EscreveLinha("Delegate aqui por uma terceira camada " + i.ToString());
            }

        }

    }
}
