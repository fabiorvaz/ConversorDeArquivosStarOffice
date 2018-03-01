using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Olvebra.ConversorArquivosApp.pesquisa
{
    public class EntradaEncontrada
    {
        public enum eTipoEntrada
        {
            eTipoNaoEncontrado,
            eTipoArquivo,
            eTipoDiretorio
        }

        public string CaminhoCompleto { get; protected set; }
        public eTipoEntrada TipoEntrada;

        protected FileSystemInfo m_cachedFileSystemInfo;

        public EntradaEncontrada(string caminhoCompleto, eTipoEntrada tipoEntrada)
        {
            this.CaminhoCompleto = caminhoCompleto;
            this.TipoEntrada = tipoEntrada;
            this.m_cachedFileSystemInfo = null;
        }

        public FileSystemInfo FileSystemInfo
        {
            get
            {
                if (TipoEntrada == eTipoEntrada.eTipoNaoEncontrado) return null;
                if (m_cachedFileSystemInfo != null) return m_cachedFileSystemInfo;
                if (Directory.Exists(CaminhoCompleto))
                {
                    TipoEntrada = eTipoEntrada.eTipoDiretorio;
                    m_cachedFileSystemInfo = new DirectoryInfo(CaminhoCompleto);
                }
                else if (File.Exists(CaminhoCompleto))
                {
                    TipoEntrada = eTipoEntrada.eTipoArquivo;
                    m_cachedFileSystemInfo = new FileInfo(CaminhoCompleto);
                }
                else
                {
                    TipoEntrada = eTipoEntrada.eTipoNaoEncontrado;
                    m_cachedFileSystemInfo = null;
                }
                return m_cachedFileSystemInfo;
            }
        }

        public FileInfo FileInfo { get { return FileSystemInfo as FileInfo; } }
        public DirectoryInfo DirectoryInfo { get { return FileSystemInfo as DirectoryInfo; } }

        public override string ToString()
        {
            return this.CaminhoCompleto;
        }
    }
}
