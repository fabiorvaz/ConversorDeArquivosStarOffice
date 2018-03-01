using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olvebra.ConversorArquivosApp.pesquisa.filtros
{
    public class Filtro_Nome : Filtro
    {
        public const int P_CUSTO_FILTRO = 1000;

        public string SubString { get; set; }
        public bool IgnoreCase { get; set; }

        public Filtro_Nome(string subString, bool ignoreCase)
        {
            this.SubString = subString;
            this.IgnoreCase = ignoreCase;
        }

        public override int GetCusto()
        {
            return P_CUSTO_FILTRO;
        }

        public override bool Filtrar(Pesquisa pesquisa, ContextoPesquisa contexto, EntradaEncontrada entrada)
        {
            if (String.IsNullOrWhiteSpace(SubString)) return false;
            return (entrada.FileSystemInfo.Name.IndexOf(SubString, (IgnoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture)) >= 0);
        }
    }
}
