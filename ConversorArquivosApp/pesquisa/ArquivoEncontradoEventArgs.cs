using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Olvebra.ConversorArquivosApp.pesquisa
{
    public class ArquivoEncontradoEventArgs : EventArgs
    {
        public ArquivoEncontradoEventArgs(EntradaEncontrada entrada)
        {
            this.Entrada = entrada;
        }

        public EntradaEncontrada Entrada { get; protected set;}

        public override string ToString()
        {
            return Entrada.ToString();
        }
    }
}
