using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Olvebra.ConversorArquivosApp.pesquisa.filtros;

namespace Olvebra.ConversorArquivosApp.pesquisa
{
    public class ContextoPesquisa
    {
        public string DiretorioOrigem { get; set; }
        public string NomeArquivo { get; set; }
        public bool PesquisarSubdiretorios { get; set; }
        public bool IgnorarPastasOcultas { get; set; }
        public bool UseThreads { get; set; }
        public Filtro FiltroArquivo { get; set; }
        public Filtro FiltroDiretorio { get; set; }
        public int MaximoDeResultados { get; set; }
    }
}
