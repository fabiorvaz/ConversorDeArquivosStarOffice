using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Olvebra.ConversorArquivosApp.pesquisa;
using System.IO;
using Olvebra.ConversorArquivosApp.logger;

namespace Olvebra.ConversorArquivosApp.conversores
{
	public class ConversorLibreOffice35 : IConversor
	{
		public string ConvertTo;

		public void ConverterBatch(IEnumerable<pesquisa.EntradaEncontrada> listaArquivos)
		{
            if (!EncontraSoffice())
            {
                return;
            }
            string command = "\""+Properties.Settings.Default.soffice_executavel+"\"";
            foreach (EntradaEncontrada entrada in listaArquivos)
            {
                int i = System.Diagnostics.Process.GetProcessesByName("soffice.exe").Count();
                string parametros="--headless -convert-to "+ConvertTo+" \""+entrada.CaminhoCompleto+"\" --outdir \""+Path.GetDirectoryName(entrada.CaminhoCompleto)+"\"";
                string temp = command + parametros;
                System.Diagnostics.Process proc = System.Diagnostics.Process.Start(command, parametros);
                proc.WaitForExit();
                //while (System.Diagnostics.Process.GetProcessesByName("soffice.exe").Count()>i);
                string arquivo_final = Path.Combine(Path.GetDirectoryName(entrada.CaminhoCompleto),(Path.GetFileNameWithoutExtension(entrada.FileInfo.Name)+"."+ConvertTo.Split(':')[0]));
                if(File.Exists(arquivo_final))
                {
                    ProcedimentoLogger.Default.LogInfo("Arquivo convertido com sucesso: " + entrada.FileInfo.Name);
                }
                else
                {
                    ProcedimentoLogger.Default.LogInfo("Falha ao converter arquivo: " + entrada.FileInfo.Name);
                }
            }
		}

        private bool EncontraSoffice()
        {
            if (!String.IsNullOrEmpty(Properties.Settings.Default.soffice_executavel))
            {
                return true;
            }
            return false;
        }
	}
}
