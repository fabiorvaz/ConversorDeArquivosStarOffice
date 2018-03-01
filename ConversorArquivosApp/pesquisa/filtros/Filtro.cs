using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olvebra.ConversorArquivosApp.pesquisa.filtros
{
    public abstract class Filtro
    {
        /// <summary>
        /// Custo de processamento do filtro, para permitir processar os filtros
        /// mais leves antes.
        /// Quanto menor o custo, mas leve o filtro.
        /// </summary>
        /// <returns>Custo de processamento do filtro.</returns>
        public abstract int GetCusto();

        /// <summary>
        /// Valida/filtra a entrada de arquivo/diretório.
        /// Retorna true se validada com sucesso no filtro, e false caso contrário.
        /// </summary>
        /// <param name="pesquisa">Engine de pesquisa executando o filtro.</param>
        /// <param name="contexto">Contexto de pesquisa atual.</param>
        /// <param name="entrada">Entrada a ser validada.</param>
        /// <returns>Retorna true se passou no filtro, e false caso não tenha passado pelo filtro.</returns>
        public abstract bool Filtrar(Pesquisa pesquisa, ContextoPesquisa contexto, EntradaEncontrada entrada);

        public virtual void Preparar() { }
    }
}
