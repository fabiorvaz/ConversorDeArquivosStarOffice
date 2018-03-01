using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Olvebra.ConversorArquivosApp.pesquisa;

namespace Olvebra.ConversorArquivosApp.conversores
{
	public interface IConversor
	{
        void ConverterBatch(IEnumerable<EntradaEncontrada> listaArquivos);
	}
}
