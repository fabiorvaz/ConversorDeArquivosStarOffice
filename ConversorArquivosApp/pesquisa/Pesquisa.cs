using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Olvebra.ConversorArquivosApp.logger;
using Olvebra.ConversorArquivosApp.pesquisa.filtros;
using Olvebra.ConversorArquivosApp.components;
using System.Diagnostics;
using System.Drawing;

namespace Olvebra.ConversorArquivosApp.pesquisa
{
    public class Pesquisa : Component
    {
        private const int P_TEMPO_CANCELA_MS = 200;

        // Contexto da pesquisa
        public string DiretorioOrigem { get; set; }
        public string NomeArquivo { get; set; }
        public bool PesquisarSubdiretorios { get; set; }
        public Filtro FiltroArquivo { get; set; }
        public bool IgnorarPastasOcultas { get; set; }
        public int MaximoDeResultados { get; set; }
        public bool UseThreads { get; set; }
        // -----


        public bool Cancelado { get; set; }
        public object Tag { get; set; }
        public List<EntradaEncontrada> Resultado { get; protected set; }

        public delegate void MensagemProgressoDelegate(object sender, MensagemProgressoEventArgs args);
        public delegate void ArquivoEncontradoDelegate(object sender, ArquivoEncontradoEventArgs args);

        public event ArquivoEncontradoDelegate ArquivoEncontrado;
        public event MensagemProgressoDelegate MensagemProgresso;
        public event EventHandler PesquisaConcluida;

        protected Thread m_threadPesquisa;
        protected ContextoPesquisa Contexto;

        protected CountZeroEvent m_countZeroEvent;

        protected bool InternalCancelado
        {
            get
            {
                // return (Cancelado || (!Thread.CurrentThread.Equals(m_threadPesquisa)));
                return (Cancelado);
            }
        }

        public Pesquisa()
        {
            m_countZeroEvent = new CountZeroEvent();
            Resultado = new List<EntradaEncontrada>();
            Clear();
        }

        public Pesquisa(IContainer container)
        {
            m_countZeroEvent = new CountZeroEvent();
            Resultado = new List<EntradaEncontrada>();
            Clear();
        }


        public bool Rodando { get { return (m_threadPesquisa != null && m_threadPesquisa.IsAlive); } }

        public void Iniciar()
        {
            Cancelar();

            if (String.IsNullOrEmpty(this.DiretorioOrigem))
                throw new ArgumentException("Diretório de origem não informado.");

            if (!Directory.Exists(this.DiretorioOrigem))
                throw new DirectoryNotFoundException("Diretório não encontrado: " + this.DiretorioOrigem);

            Contexto = new ContextoPesquisa();
            Contexto.DiretorioOrigem = this.DiretorioOrigem;
            Contexto.NomeArquivo = this.NomeArquivo;
            Contexto.PesquisarSubdiretorios = this.PesquisarSubdiretorios;
            Contexto.FiltroArquivo = this.FiltroArquivo;
            Contexto.IgnorarPastasOcultas = this.IgnorarPastasOcultas;
            Contexto.MaximoDeResultados = this.MaximoDeResultados;
            Contexto.UseThreads = this.UseThreads;
            if (this.NomeArquivo.IndexOfAny(new char[] { '?', '*' }) < 0)
            {
                Contexto.FiltroDiretorio = new Filtro_Nome(this.NomeArquivo, true);
            }
            

            Thread t = new Thread(ThreadPesquisa);

            Resultado = new List<EntradaEncontrada>();
            Cancelado = false;
            m_countZeroEvent.Clear();

            try
            {
                ThreadPool.SetMaxThreads(16, 16);
                ThreadPool.SetMinThreads(4, 4);
            }
            catch (Exception) { }

            t.Start();
            m_threadPesquisa = t;
        }

        public void Cancelar()
        {
            if (!Rodando) return;
            Cancelado = true;
            Thread t = m_threadPesquisa;
            if (t != null)
            {
                m_countZeroEvent.Clear();
                t.Join(P_TEMPO_CANCELA_MS);
                if (t.IsAlive) t.Interrupt();
            }
            m_threadPesquisa = null;
        }

        protected void OnMensagemProgresso(string mensagem)
        {
            if (InternalCancelado) return;
            if (MensagemProgresso != null)
            {
                MensagemProgressoEventArgs args = new MensagemProgressoEventArgs(mensagem);
                OnMensagemProgresso(args);
            }
        }

        private void OnMensagemProgresso(MensagemProgressoEventArgs args)
        {
            MensagemProgressoDelegate d = MensagemProgresso;
            if (InternalCancelado) return;
            if (d == null) return;
            Control c = d.Target as Control;
            if (c != null && c.InvokeRequired)
            {
                c.BeginInvoke(d, new object[] { this, args });
            }
            else
            {
                d.Invoke(this, args);
            }
        }

        private void OnPesquisaConcluida(EventArgs args)
        {
            EventHandler d = PesquisaConcluida;
            if (InternalCancelado) return;
            if (d == null) return;
            Control c = d.Target as Control;
            if (c != null && c.InvokeRequired)
            {
                c.BeginInvoke(d, new object[] { this, args });
            }
            else
            {
                d.Invoke(this, args);
            }
        }

        protected void OnArquivoEncontrado(EntradaEncontrada entrada)
        {
            if (InternalCancelado) return;
            if (ArquivoEncontrado != null)
            {
                ArquivoEncontradoEventArgs args = new ArquivoEncontradoEventArgs(entrada);
                OnArquivoEncontrado(args);
            }
        }

        protected void OnArquivoEncontrado(ArquivoEncontradoEventArgs args)
        {
            ArquivoEncontradoDelegate d = ArquivoEncontrado;
            if (InternalCancelado) return;
            if (d == null) return;
            Control c = d.Target as Control;
            if (c != null && c.InvokeRequired)
            {
                c.BeginInvoke(d, new object[] { this, args });
            }
            else
            {
                d.Invoke(this, args);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Cancelar();
        }

        protected void ThreadPesquisa()
        {
            if (InternalCancelado) return;
            try
            {
                PesquisarDiretorio(Contexto.DiretorioOrigem);
                m_countZeroEvent.Wait();
            }
            catch (ThreadInterruptedException) { }
            if (!InternalCancelado)
            {
                OnMensagemProgresso("concluído");
                OnPesquisaConcluida(new EventArgs());
            }
        }

        protected void PesquisarDiretorio(string diretorioPai)
        {
            if (InternalCancelado) return;

            OnMensagemProgresso(diretorioPai);

            if (InternalCancelado) return;

            string[] listaArquivos;

            try
            {
                listaArquivos = Directory.GetFiles(diretorioPai, Contexto.NomeArquivo);
            }
            catch (Exception ex)
            {
                Logger.Default.LogAviso(String.Format(
                    "Diretório {0}. Erro: {1}", diretorioPai, ex.Message));
                listaArquivos = null;
            }

            if (listaArquivos != null)
            {
                foreach (string arquivo in listaArquivos)
                {
                    if (InternalCancelado) return;
                    if (Resultado.Count == Contexto.MaximoDeResultados) return;

                    try
                    {
                        EntradaEncontrada entrada = new EntradaEncontrada(arquivo, EntradaEncontrada.eTipoEntrada.eTipoArquivo);
                        if (Contexto.FiltroArquivo != null)
                        {
                            if (!Contexto.FiltroArquivo.Filtrar(this, Contexto, entrada))
                                entrada = null;
                        }
                        if (entrada != null)
                        {
                            Resultado.Add(entrada);
                            OnArquivoEncontrado(entrada);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Default.LogAviso(String.Format(
                            "Arquivo {0}. Erro: {1}", arquivo, ex.Message));
                        return;
                    }
                }
            }

            if (InternalCancelado) return;

            string[] listaDiretorios;

            try
            {
                listaDiretorios = Directory.GetDirectories(diretorioPai);
            }
            catch (Exception ex)
            {
                Logger.Default.LogAviso(String.Format(
                    "Diretório {0}. Erro: {1}", diretorioPai, ex.Message));
                return;
            }

            foreach (string diretorio in listaDiretorios)
            {
                if (InternalCancelado) return;
                if (Resultado.Count == MaximoDeResultados) return;

                if (Contexto.IgnorarPastasOcultas)
                {
                    DirectoryInfo info = new DirectoryInfo(diretorio);
                    if ((info.Attributes & FileAttributes.Hidden) > 0)
                        continue;
                }

                // Usa o filtro de diretório para retorno o diretório como um resultado
                // de pesquisa.
                if (Contexto.FiltroDiretorio != null)
                {
                    try
                    {
                        EntradaEncontrada entrada = new EntradaEncontrada(diretorio, EntradaEncontrada.eTipoEntrada.eTipoDiretorio);
                        if (Contexto.FiltroDiretorio.Filtrar(this, Contexto, entrada))
                        {
                            Resultado.Add(entrada);
                            OnArquivoEncontrado(entrada);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Default.LogAviso(String.Format(
                            "Diretório {0}. Erro: {1}", diretorio, ex.Message));
                        return;
                    }
                }

                // pesquisa em todos os diretórios independente do filtro de diretório
                if (Contexto.UseThreads)
                    QueuePesquisarDiretorio(diretorio);
                else
                    PesquisarDiretorio(diretorio);
            }
        }

        private void QueuePesquisarDiretorio(string diretorio)
        {
            m_countZeroEvent.Add();
            ThreadPool.QueueUserWorkItem(new WaitCallback(PoolWorkItem), diretorio);
        }

        private void PoolWorkItem(object item)
        {
            try
            {
                if (!InternalCancelado)
                {

                    string diretorio = (string)item;
                    PesquisarDiretorio(diretorio);
                }
            }
            finally
            {
                m_countZeroEvent.Remove();
            }
        }

        public void Clear()
        {
            DiretorioOrigem = "";
            NomeArquivo = "";
            PesquisarSubdiretorios = false;
            FiltroArquivo = null;
            IgnorarPastasOcultas = true;
            MaximoDeResultados = 1000;
            UseThreads = true;
        }
    }
}
