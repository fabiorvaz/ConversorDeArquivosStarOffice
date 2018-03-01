using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Security.Permissions;
using Olvebra.ConversorArquivosApp.Properties;
using System.Windows.Forms;

[assembly: RegistryPermissionAttribute(SecurityAction.RequestMinimum,
    ViewAndModify = "HKEY_LOCAL_MACHINE")]

[assembly: RegistryPermissionAttribute(SecurityAction.RequestMinimum,
    ViewAndModify = "HKEY_CLASSES_ROOT")]

namespace Olvebra.ConversorArquivosApp.util
{
    /// <summary>
    /// Utilitário localizador do executável do Soffice.
    /// 
    /// Leitura/escrita do registro:
    /// http://www.codeproject.com/Articles/3389/Read-write-and-delete-from-registry-with-C
    /// </summary>
    public static class SofficeFinder2
    {
        private const string P_NOME_PROGRAMA = "soffice.exe";
        private const string P_NOME_PASTA = "LibreOffice 3";

        private const string P_HKEY_LOCAL_MACHINE = "HKEY_LOCAL_MACHINE";
        private const string P_HKEY_CLASSES_ROOT = "HKEY_CLASSES_ROOT";


        private static void SalvarCaminhoTightVnc(string nomeArquivo)
        {
            // TODO: coloque aqui a linha para salvar o caminho
            Properties.Settings.Default.soffice_executavel = nomeArquivo;
        }

        /// <summary>
        /// Retorna o caminho salvo, ou null caso não tenha um
        /// caminho salvo.
        /// Use LocalizarCaminho() para procurar pelo caminho.
        /// </summary>
        /// <returns>Retorna o caminho salvo, ou null caso não tenha um
        /// caminho salvo.</returns>
        public static string ObterCaminho()
        {
            return Properties.Settings.Default.soffice_executavel;
        }

        /// <summary>
        /// Procura o caminho para o executavel.
        /// Retorna o caminho/nome arquivo encontrado, ou null caso não encontre.
        /// </summary>
        /// <returns>Retorna o caminho/nome arquivo encontrado, ou null caso não encontre.</returns>
        public static string ProcurarCaminho()
        {
            string ret = null;

            ret = ProcurarNoRegistro();
            if (String.IsNullOrEmpty(ret)) ret = ProcurarEmProgramFiles();

            // salva caso encontre
            if (!String.IsNullOrEmpty(ret)) SalvarCaminhoTightVnc(ret);
            return ret;
        }

        /// <summary>
        /// Procura o programa do soffice nos subdiretórios de 
        /// Program Files.
        /// Retorna o caminho/nome arquivo, ou null caso não encontre.
        /// </summary>
        /// <returns>Retorna o caminho/nome arquivo, ou null caso não encontre.</returns>
        public static string ProcurarEmProgramFiles()
        {
            Queue<string> filaDiretorios = new Queue<string>();
            Queue<string> filaArquivos = new Queue<string>();
            filaDiretorios.Enqueue(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

            while (filaDiretorios.Count > 0)
            {
                string diretorioAtual = filaDiretorios.Dequeue();

                try
                {
                    string[] dirList = Directory.GetDirectories(diretorioAtual);
                    string[] fileList = Directory.GetFiles(diretorioAtual);
                    foreach (string dirEntry in dirList)
                        if (!dirEntry.StartsWith(".")) filaDiretorios.Enqueue(Path.Combine(diretorioAtual, dirEntry));
                    foreach (string fileEntry in fileList)
                        if (!fileEntry.StartsWith(".")) filaArquivos.Enqueue(Path.Combine(diretorioAtual, fileEntry));
                }
                catch (Exception) { }

                while (filaArquivos.Count > 0)
                {
                    string arquivoAtual = filaArquivos.Dequeue();
                    string nomeArquivo = Path.GetFileName(arquivoAtual);
                    if ((nomeArquivo.ToLower().Equals(P_NOME_PROGRAMA.ToLower())))
                    {
                        SalvarCaminhoTightVnc(arquivoAtual);
                        return arquivoAtual;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Procurar o caminho pro executavel no registro do Windows.
        /// </summary>
        /// <returns>O caminho/nome arquivo encontrado, ou null caso não encontre.</returns>
        private static string ProcurarNoRegistro()
        {
            string[] listaChaves = new string[] {
				@"HKEY_LOCAL_MACHINE\SOFTWARE\LibreOffice\LibreOffice\4.2\Path"
			};

            foreach (string nomeChave in listaChaves)
            {
                string valorChave = LerChaveRegistro(nomeChave);
                if (!String.IsNullOrEmpty(valorChave))
                {
                    string nome = ProcessarValorChave(valorChave);
                    if (!String.IsNullOrEmpty(nome))
                    {
                        string arquivo = Path.Combine(nome, P_NOME_PROGRAMA);
                        if (File.Exists(arquivo))
                        {
                            SalvarCaminhoTightVnc(arquivo);
                            return arquivo;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Remove da linha de comando os parâmetros, aspas, e outros lixos.
        /// Retorna o nome do diretório da linha de comando.
        /// </summary>
        /// <param name="valorChave">Linha de comando.</param>
        /// <returns>Retorna o nome do diretório da linha de comando.</returns>
        private static string ProcessarValorChave(string valorChave)
        {
            if (!valorChave.StartsWith("\""))
                valorChave = "\"" + valorChave;
            string nome = valorChave.Split('"')[1].Trim();
            if (File.Exists(nome))
                return Path.GetDirectoryName(nome);
            if (Directory.Exists(nome))
                return nome;
            return null;
        }

        /// <summary>
        /// Lê a chave do registro do Windows.
        /// Exemplo de chave: HKEY_LOCAL_MACHINE\SOFTWARE\TightVNC\Path
        /// </summary>
        /// <param name="nomeChave">Nome da chave</param>
        /// <returns>Valor da chave.</returns>
        public static string LerChaveRegistro(string nomeChave)
        {
            RegistryKey hive;
            if (nomeChave.StartsWith(P_HKEY_LOCAL_MACHINE))
            {
                hive = Registry.LocalMachine;
            }
            else if (nomeChave.StartsWith(P_HKEY_CLASSES_ROOT))
            {
                hive = Registry.ClassesRoot;
            }
            else
            {
                throw new Exception("Registry hive não programada: " +
                    nomeChave.Substring(0, (nomeChave + '/').IndexOf('/')));
            }

            int pos;
            string subCaminho;
            string caminhoValor;
            string nomeValor;
            pos = nomeChave.IndexOf('\\');
            subCaminho = nomeChave.Substring(pos + 1);
            caminhoValor = Path.GetDirectoryName(subCaminho);
            nomeValor = Path.GetFileName(subCaminho);

            try
            {
                // lê do registro
                RegistryKey rk = hive.OpenSubKey(caminhoValor);
                object objRet = rk.GetValue(nomeValor, null);

                string ret;
                if (objRet == null)
                    ret = "";
                else
                    ret = objRet.ToString();

                return ret;
            }
            catch (Exception) { return ""; }
        }

    }
}
