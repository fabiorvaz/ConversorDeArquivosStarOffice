using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olvebra.ConversorArquivosApp.conversores
{
	public class ConversorFactory
	{
		public static IEnumerable<IConversor> Conversores()
		{
			List<IConversor> ret = new List<IConversor>();
			ret.Add(new ConversorLibreOfficeListItemAdapter("Planilha de cálculo LibreOffice 3", "ods:\"calc8\""));
            ret.Add(new ConversorLibreOfficeListItemAdapter("Planilha de cálculo Excel 97-2003", "xls:\"MS Excel 97\""));

			ret.Add(new ConversorLibreOfficeListItemAdapter("Documento de texto LibreOffice 3", "odt:\"writer8\""));
            ret.Add(new ConversorLibreOfficeListItemAdapter("Documento de texto Word 97-2003", "doc:\"MS Word 97\""));
            ret.Add(new ConversorLibreOfficeListItemAdapter("Documento de texto Word 2007", "docx:\"MS Word 2007 XML\""));

            ret.Add(new ConversorLibreOfficeListItemAdapter("Apresentação de slides LibreOffice 3", "odp:\"impress8\""));
            ret.Add(new ConversorLibreOfficeListItemAdapter("DApresentação de slides PowerPoint 97-2003", "ppt:\"MS PowerPoint 97\""));
			return ret;
		}
	}
}
