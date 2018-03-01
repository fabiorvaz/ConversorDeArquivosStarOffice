using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olvebra.ConversorArquivosApp.pesquisa.filtros
{
    public class Filtro_LocalizarTexto : Filtro
    {
        public const int P_CUSTO_FILTRO = 1000;

        public string TextoLocalizar;
        private bool IgnoreCase;

        public Filtro_LocalizarTexto()
        {
            TextoLocalizar = "";
        }

        public Filtro_LocalizarTexto(string textoLocalizar)
        {
            this.TextoLocalizar = textoLocalizar;    
        }

        public Filtro_LocalizarTexto(string textoLocalizar, bool ignoreCase)
        {
            this.TextoLocalizar = textoLocalizar;
            this.IgnoreCase = ignoreCase;
        }

        public override int GetCusto()
        {
            return P_CUSTO_FILTRO;
        }

        public override bool Filtrar(Pesquisa pesquisa, ContextoPesquisa contexto, EntradaEncontrada entrada)
        {
            if (entrada.TipoEntrada != EntradaEncontrada.eTipoEntrada.eTipoArquivo) return true;
            BinaryStreamSearcher searcher = new BinaryStreamSearcher(entrada.CaminhoCompleto);
            searcher.SetBusca(TextoLocalizar, ASCIIEncoding.ASCII, IgnoreCase);
            return searcher.Buscar();
        }

        public override string ToString()
        {
            return String.Format("LocalizarTexto({0})", TextoLocalizar);
        }
    }
}
