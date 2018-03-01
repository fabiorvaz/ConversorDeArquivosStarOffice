using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olvebra.ConversorArquivosApp.logger
{
	public class Logger
	{
		public static ProcedimentoLogger Default
		{
			get
			{
				return ProcedimentoLogger.Default;
			}
		}
	}
}
