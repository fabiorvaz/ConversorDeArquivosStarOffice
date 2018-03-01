using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olvebra.ConversorArquivosApp.conversores
{
	public class ConversorLibreOfficeListItemAdapter : IConversor
	{
		public string Descricao;
		public string ConvertTo;

		public ConversorLibreOfficeListItemAdapter(string descricao, string convertTo)
		{
			this.Descricao = descricao;
			this.ConvertTo = convertTo;
		}

		public override string ToString()
		{
			return String.Format("{0} - {1}", Descricao, ConvertTo);
		}

		public void ConverterBatch(IEnumerable<pesquisa.EntradaEncontrada> listaArquivos)
		{
			ConversorLibreOffice35 conversor = new ConversorLibreOffice35();
			conversor.ConvertTo = ConvertTo;
			conversor.ConverterBatch(listaArquivos);
		}
	}
}
