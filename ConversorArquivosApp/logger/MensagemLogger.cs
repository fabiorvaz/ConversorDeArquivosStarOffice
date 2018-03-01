using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olvebra.ConversorArquivosApp.logger
{
	public class MensagemLogger
	{
		public string Mensagem;
		public TipoMsgLogger Tipo;
		public DateTime TimeStamp;

		public MensagemLogger(TipoMsgLogger tipo, string mensagem)
		{
			this.Tipo = tipo;
			this.Mensagem = mensagem;
			this.TimeStamp = DateTime.Now;
		}
	}
}
