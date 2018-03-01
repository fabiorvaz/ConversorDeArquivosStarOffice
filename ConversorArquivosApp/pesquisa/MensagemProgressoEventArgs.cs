using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olvebra.ConversorArquivosApp.pesquisa
{
    public class MensagemProgressoEventArgs
    {
        public string Mensagem { get; set; }

        public MensagemProgressoEventArgs(string mensagem)
        {
            this.Mensagem = mensagem;
        }

        public override string ToString()
        {
            return Mensagem;
        }
    }
}
