using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Olvebra.ConversorArquivosApp.pesquisa
{
    public class BinaryStreamSearcher
    {
        private const int P_BUFFER_SIZE = 4 * 1024;

        public bool IgnoreCase;
        public string FileName;
        public Stream BaseStream;
        public string TextBusca;
        public byte[] busca;

        public BinaryStreamSearcher(Stream stream)
        {
        }

        public BinaryStreamSearcher(string fileName)
        {
            FileName = fileName;
        }

        public void SetBusca(string conteudo, Encoding encoding, bool ignoreCase)
        {
            if (ignoreCase)
                busca = encoding.GetBytes(conteudo.ToLower());
            else
                busca = encoding.GetBytes(conteudo);
            IgnoreCase = ignoreCase;
        }

        private bool Buscar(Stream stream)
        {
            int readPos;
            int buscaPos = 0;
            byte[] buffer = new byte[P_BUFFER_SIZE];
            while (stream.CanRead)
            {
                int count = stream.Read(buffer, 0, P_BUFFER_SIZE);
                if (count == 0) break;
                readPos = 0;
                while (count > 0)
                {
                    byte b1 = buffer[readPos];   // arquivo
                    byte b2 = busca[buscaPos];   // busca
                    if (IgnoreCase && (b1 >= 'A' && b1 <= 'Z')) b1 = (byte)(int)(b1 + 32);
                    if (b1 == b2)
                    {
                        buscaPos++;
                        if (buscaPos == busca.Length) return true;
                    }
                    else
                    {
                        if (buscaPos != 0)
                        {
                            // reseta a busca para o byte corrente
                            buscaPos = 0;
                            continue;
                        }
                    }
                    readPos++;
                    count--;
                }
            }
            return false;
        }

        public bool Buscar()
        {
            if (BaseStream != null)
            {
                return Buscar(BaseStream);
            }
            else
            {
                try
                {
                    using (FileStream stream = new FileStream(FileName, FileMode.Open))
                    {
                        return Buscar(stream);
                    }
                }
                catch (Exception ex)
                {
                    // HACK: acertar a mensagem de erro
                    return false;
                }
            }
        }
    }
}
