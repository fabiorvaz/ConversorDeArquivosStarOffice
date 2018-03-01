using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Olvebra.ConversorArquivosApp.forms;
using System.Windows.Forms;

namespace Olvebra.ConversorArquivosApp.logger
{
	public class ProcedimentoLogger
	{
		private static ProcedimentoLogger m_default = null;

		public static ProcedimentoLogger Default
		{
			get
			{
				if (m_default == null) m_default = new ProcedimentoLogger();
				return m_default;
			}
		}


		// -------------------------------------------------


		private FormProcedimentoLogViewer m_formLogger;

		public List<MensagemLogger> Mensagens;
		public TipoMsgLogger NivelMaximo;
		public bool AutoShowLog;


		private ProcedimentoLogger()
		{
			AutoShowLog = false;
			Mensagens = new List<MensagemLogger>();
		}


		private FormProcedimentoLogViewer GetFormLogger()
		{
			if (m_formLogger != null) return m_formLogger;
			throw new InvalidOperationException("Logger não inicializado / faltando chamada a Inicializar");
		}


		public void Inicializar()
		{
			m_formLogger = new FormProcedimentoLogViewer();
		}


		public void LogMsg(TipoMsgLogger tipo, string mensagem)
		{
			Mensagens.Add(new MensagemLogger(tipo, mensagem));
			if (tipo > NivelMaximo) NivelMaximo = tipo;
			if (m_formLogger.Visible)
			{
				FormAtualizarLista();
			}
			if (AutoShowLog) ShowForm();
		}

		private void FormAtualizarLista()
		{
			if (m_formLogger.InvokeRequired)
			{
				m_formLogger.BeginInvoke(new MethodInvoker(m_formLogger.AtualizarLista));
			}
			else
			{
				m_formLogger.AtualizarLista();
			}
		}

		private void ShowForm()
		{
			GetFormLogger();
			if (m_formLogger.Visible) return;			
			if (Program.MainForm.InvokeRequired)
			{
				Program.MainForm.BeginInvoke((MethodInvoker)delegate
				{
					if (m_formLogger.IsDisposed)
						m_formLogger = new FormProcedimentoLogViewer();
					m_formLogger.Owner = Program.MainForm;
					m_formLogger.Show();
				});
			}
			else
			{
				m_formLogger.Owner = Program.MainForm;
				m_formLogger.Show();
			}
		}

		private void HideForm()
		{
			GetFormLogger();
			if (!m_formLogger.Visible) return;
			if (m_formLogger.InvokeRequired)
			{
				m_formLogger.BeginInvoke(new MethodInvoker(m_formLogger.Hide));
			}
			else
				m_formLogger.Hide();
		}


		public void LogOk(string mensagem) { LogMsg(TipoMsgLogger.Ok, mensagem); }
		public void LogInfo(string mensagem) { LogMsg(TipoMsgLogger.Info, mensagem); }
		public void LogAviso(string mensagem) { LogMsg(TipoMsgLogger.Aviso, mensagem); }
		public void LogErro(string mensagem) { LogMsg(TipoMsgLogger.Erro, mensagem); }


		public void Clear()
		{
			Mensagens = new List<MensagemLogger>();
			NivelMaximo = TipoMsgLogger.Ok;
			if (m_formLogger.Visible == true)
				HideForm();
		}
	}

	public enum TipoMsgLogger
	{
		Ok = 0,
		Info = 1,
		Aviso = 2,
		Erro = 3
	}
}
