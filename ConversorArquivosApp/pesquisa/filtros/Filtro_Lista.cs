using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olvebra.ConversorArquivosApp.pesquisa.filtros
{
    public class Filtro_Lista : Filtro
    {
        protected List<Filtro> m_filtros;
        protected bool m_sorted;

        public Filtro_Lista()
        {
            m_sorted = true;
            m_filtros = new List<Filtro>();
        }

        public void AddFiltro(Filtro filtro)
        {
            m_filtros.Add(filtro);
            m_sorted = false;
        }

        public override int GetCusto()
        {
            int custo = 0;
            foreach (Filtro filtro in m_filtros)
            {
                int custoFiltro = filtro.GetCusto();
                if (custoFiltro > custo) custo = custoFiltro;
            }
            return custo;
        }

        public override void Preparar()
        {
            foreach (Filtro filtro in m_filtros) filtro.Preparar();
            if (!m_sorted)
            {
                m_sorted = true;
                List<Filtro> lista = m_filtros;
                lista.Sort(new Comparison<Filtro>(FiltroCustoComparer));
            }
        }

        public override bool Filtrar(Pesquisa pesquisa, ContextoPesquisa contexto, EntradaEncontrada entrada)
        {
            foreach (Filtro filtro in m_filtros)
            {
                if (!filtro.Filtrar(pesquisa, contexto, entrada)) return false;
            }
            return true;
        }

        private int FiltroCustoComparer(Filtro a, Filtro b)
        {
            int a1 = a.GetCusto();
            int b1 = b.GetCusto();
            if (a1 == b1) return 0;
            if (a1 < b1) return 1;
            return -1;
        }

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            Preparar();
            foreach (Filtro filtro in m_filtros)
            {
                ret.Append(" AND ");
                ret.Append(filtro.ToString());
            }
            return "(" + ret.ToString() + ")";
        }
    }
}
