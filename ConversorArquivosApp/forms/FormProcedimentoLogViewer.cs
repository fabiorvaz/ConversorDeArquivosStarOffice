using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Olvebra.ConversorArquivosApp.logger;

namespace Olvebra.ConversorArquivosApp.forms
{
	public partial class FormProcedimentoLogViewer : Form
	{
		public FormProcedimentoLogViewer()
		{
			InitializeComponent();
		}

		private void FormProcedimentoLogViewer_Load(object sender, EventArgs e)
		{
		}

		public void AtualizarLista()
		{
			try
			{
				AtualizarListaInt();
			}
			catch (Exception) { }
		}

		private void AtualizarListaInt()
		{
			StringBuilder htmlSb = new StringBuilder();
			htmlSb.Append(
@"<html>
<head>
<style type='text/css'>
 html, body {
    Font-family: 'Arial';
 }
 p { margin: 5px; }
 .msgOk    { Color: Green; Font-weight: bold; }
 .msgInfo  { Color: Black; }
 .msgAviso { Color: GoldenRod; }
 .msgErro  { Color: Red; Font-weight: bold; }
</style>
</head>
<body>"
);
			foreach (MensagemLogger mensagem in ProcedimentoLogger.Default.Mensagens)
				htmlSb.Append(Mensagem2Html(mensagem));

			htmlSb.Append("</body></html>");

			webMensagens.DocumentText = "";
			webMensagens.Document.OpenNew(true);
			webMensagens.DocumentText = htmlSb.ToString();

			webMensagens.Invalidate();
			webMensagens.Update();

			int maxLoop = 10;
			Application.DoEvents();
			while ((webMensagens.Document.Body == null) && (maxLoop > 0))
			{
				Application.DoEvents();
				maxLoop--;
			}
			if (webMensagens.Document.Body != null)
				webMensagens.Document.Window.ScrollTo(0, webMensagens.Document.Body.ClientRectangle.Height * 2);
		}

		private string Mensagem2Html(MensagemLogger mensagem)
		{
			return String.Format("<p class='{2}'>{0}{1}</p>", NivelMensagemToTag(mensagem.Tipo), mensagem.Mensagem, NivelMensagemToCss(mensagem.Tipo));
		}

		private string NivelMensagemToTag(TipoMsgLogger nivel)
		{
			switch (nivel)
			{
				case TipoMsgLogger.Info: return "";
				case TipoMsgLogger.Ok: return "OK: ";
				case TipoMsgLogger.Aviso: return "AVISO: ";
				case TipoMsgLogger.Erro: return "ERRO: ";
			}
			return "";
		}

		private string NivelMensagemToCss(TipoMsgLogger nivel)
		{
			switch (nivel)
			{
				case TipoMsgLogger.Info: return "msgInfo";
				case TipoMsgLogger.Ok: return "msgOk";
				case TipoMsgLogger.Aviso: return "msgAviso";
				case TipoMsgLogger.Erro: return "msgErro";
			}
			return "";
		}

		private void buttonFechar_Click(object sender, EventArgs e)
		{
			Hide();
		}

		private void Form_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Hide();
				e.Handled = true;
				return;
			}
		}

		private void Form_Shown(object sender, EventArgs e)
		{
			AtualizarLista();
		}

		private void buttonLimpar_Click(object sender, EventArgs e)
		{
			ProcedimentoLogger.Default.Clear();
		}
	}
}
