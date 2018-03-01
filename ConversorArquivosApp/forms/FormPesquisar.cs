using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Olvebra.ConversorArquivosApp.pesquisa;
using Olvebra.ConversorArquivosApp.pesquisa.filtros;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Win32;
using Olvebra.ConversorArquivosApp.util;
using Olvebra.ConversorArquivosApp.logger;
using System.Threading;
using Olvebra.ConversorArquivosApp.conversores;

namespace Olvebra.ConversorArquivosApp.forms
{
	public partial class FormPesquisar : Form
	{
		private DateTime m_Inicio;
		private DateTime m_Fim;
		private TimeSpan m_Duracao;

		public FormPesquisar()
		{
			InitializeComponent();
		}

		private void FormPesquisar_Load(object sender, EventArgs e)
		{            
            ProcedimentoLogger.Default.Inicializar();

			listResultado.SmallImageList = shellIconImageList1.SmallImageList;
			listResultado.LargeImageList = shellIconImageList1.LargeImageList;

			Program.control = this;

			ShowFileVersion();

			InicializarForm();

			Program.MainForm = this;

			ProcedimentoLogger.Default.Inicializar();
			ProcedimentoLogger.Default.AutoShowLog = true;
		}

		private void InicializarForm()
		{
			AppSettings.Default.LoadFormState(this);

			if (Program.Args.Length > 0)
				SetLocalPesquisa(Program.Args[0]);
			else
				textLocalPesquisa.Text = AppSettings.Default.User_LoadString("LocalPesquisa");

			textNomeArquivo.Text = AppSettings.Default.User_LoadString("NomeDoArquivo", "*.txt");

			textConteudoArquivo.Text = AppSettings.Default.User_LoadString("ConteudoDoArquivo", "");

			string[] checkParams = AppSettings.Default.User_LoadParams("PesquisaChecks");
			checkIgnoreCase.Checked = AppSettings.Params_GetBool(checkParams, 0, true);
			checkIgnoreHiddenFolders.Checked = AppSettings.Params_GetBool(checkParams, 1, true);
			checkPesquisarSubdiretorios.Checked = AppSettings.Params_GetBool(checkParams, 2, true);
			checkUseThreadPool.Checked = AppSettings.Params_GetBool(checkParams, 3, true);

			foreach (object item in ConversorFactory.Conversores())
				comboTipoConversor.Items.Add(item);
		}

		private void EncerrarForm()
		{
			AppSettings.Default.SaveFormState(this);

			if (textLocalPesquisa.Text != "")
				AppSettings.Default.User_SaveString("LocalPesquisa", textLocalPesquisa.Text);

			if (textNomeArquivo.Text != "")
				AppSettings.Default.User_SaveString("NomeDoArquivo", textNomeArquivo.Text);

			AppSettings.Default.User_SaveString("ConteudoDoArquivo", textConteudoArquivo.Text);

			AppSettings.Default.User_SaveString("PesquisaChecks", AppSettings.Params_ToStr(
				 AppSettings.BoolToStr(checkIgnoreCase.Checked),
				 AppSettings.BoolToStr(checkIgnoreHiddenFolders.Checked),
				 AppSettings.BoolToStr(checkPesquisarSubdiretorios.Checked),
				 AppSettings.BoolToStr(checkUseThreadPool.Checked)
				 ));

		}

		private void ShowFileVersion()
		{
			Assembly ass = Assembly.GetExecutingAssembly();
			object[] list = ass.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
			if (list.Length >= 0)
			{
				AssemblyFileVersionAttribute version1 = (list[0] as AssemblyFileVersionAttribute);
				this.Text += String.Format(" v.{0}", version1.Version);
			}
		}
        
		private void textLocalPesquisa_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetFormats().Contains<string>(System.Windows.Forms.DataFormats.FileDrop))
			{
				string[] lista = (string[])e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop);
				if (lista.Length < 1) return;
				SetLocalPesquisa(lista[0]);
			}
		}

		private void SetLocalPesquisa(string caminho)
		{
			if (File.Exists(caminho))
			{
				caminho = Path.GetDirectoryName(caminho);
			}
			else
			{
				if (!Directory.Exists(caminho)) return;
			}
			textLocalPesquisa.Text = caminho;
		}

		private void Evento_DragEnter_Arquivo(object sender, DragEventArgs e)
		{
			if (e.Data.GetFormats().Contains<string>(System.Windows.Forms.DataFormats.FileDrop))
				e.Effect = DragDropEffects.Link;
			else
				e.Effect = DragDropEffects.None;
		}

		private void textNomeArquivo_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetFormats().Contains<string>(System.Windows.Forms.DataFormats.FileDrop))
			{
				string[] lista = (string[])e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop);
				if (lista.Length < 1) return;
				string caminho = lista[0];
				string nomeArquivo = "";
				if (File.Exists(caminho))
				{
					nomeArquivo = "*" + Path.GetExtension(caminho);
					caminho = Path.GetDirectoryName(caminho);
				}
				else
				{
					if (!Directory.Exists(caminho)) return;
				}
				if (!String.IsNullOrEmpty(caminho))
					textLocalPesquisa.Text = caminho;
				if (!String.IsNullOrEmpty(nomeArquivo))
					textNomeArquivo.Text = nomeArquivo;
			}
		}

		private void pesquisa1_MensagemProgresso(object sender, MensagemProgressoEventArgs args)
		{
			labelProgresso.Text = args.Mensagem;
		}

		private void buttonPesquisar_Click(object sender, EventArgs e)
		{
			if (!pesquisa1.Rodando)
			{
				pesquisa1.Clear();

				pesquisa1.IgnorarPastasOcultas = checkIgnoreHiddenFolders.Checked;
				pesquisa1.DiretorioOrigem = textLocalPesquisa.Text;
				pesquisa1.NomeArquivo = textNomeArquivo.Text;
				pesquisa1.PesquisarSubdiretorios = checkPesquisarSubdiretorios.Checked;
				pesquisa1.UseThreads = checkUseThreadPool.Checked;

				if (textConteudoArquivo.Text != "")
					pesquisa1.FiltroArquivo = new Filtro_LocalizarTexto(textConteudoArquivo.Text, checkIgnoreCase.Checked);

				listResultado.Items.Clear();

				m_Inicio = DateTime.Now;

				pesquisa1.Iniciar();
				buttonPesquisar.Text = "Cancelar";
			}
			else
			{
				pesquisa1.Cancelar();
				buttonPesquisar.Text = "Pesquisar";
			}
		}

		private void pesquisa1_PesquisaConcluida(object sender, EventArgs e)
		{
			buttonPesquisar.Text = "Pesquisar";
			m_Fim = DateTime.Now;
			m_Duracao = m_Fim - m_Inicio;
			labelProgresso.Text = labelProgresso.Text + String.Format(" Tempo decorrido: {0}", m_Duracao);
			listResultado.Focus();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
		}

		private void FormPesquisar_FormClosed(object sender, FormClosedEventArgs e)
		{
			pesquisa1.Cancelar();
		}

		private void ListaAdicionarEntrada(EntradaEncontrada entrada)
		{
			ListViewItem lvi = new ListViewItem();
			lvi.Text = entrada.FileSystemInfo.Name;
			lvi.SubItems.Add(entrada.FileSystemInfo.LastWriteTime.ToString());
			lvi.SubItems.Add(Path.GetDirectoryName(entrada.CaminhoCompleto));
			lvi.Tag = entrada;
			if (entrada.TipoEntrada == EntradaEncontrada.eTipoEntrada.eTipoDiretorio)
				lvi.ImageIndex = shellIconImageList1.GetFolderSmallIcon(entrada.CaminhoCompleto);
			else
				lvi.ImageIndex = shellIconImageList1.GetSmallIcon(entrada.CaminhoCompleto);
			listResultado.Items.Add(lvi);
		}

		private void pesquisa1_ArquivoEncontrado(object sender, ArquivoEncontradoEventArgs args)
		{
			ListaAdicionarEntrada(args.Entrada);
		}

		private void FormPesquisar_Resize(object sender, EventArgs e)
		{
			int listWidth, listHeight;

			listWidth = this.ClientSize.Width - (2 * listResultado.Left);
			listHeight = this.ClientSize.Height - (listResultado.Left + listResultado.Top);

			if (listWidth < 100) listWidth = 100;
			if (listHeight < 100) listHeight = 100;

			listResultado.Width = listWidth;
			listResultado.Height = listHeight;
		}

		private void contextMenuItemEncontrado_Opening(object sender, CancelEventArgs e)
		{
			bool selecionado = (listResultado.SelectedItems.Count > 0);
			abrirToolStripMenuItem.Enabled = selecionado;
			abrirLocalToolStripMenuItem.Enabled = selecionado;
			propriedadesToolStripMenuItem.Enabled = selecionado;
		}

		private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				AbrirItem(ItemSelecionado());
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void abrirLocalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				AbrirLocalItem(ItemSelecionado());
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void propriedadesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				AbrirPropriedadesItem(ItemSelecionado());
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private EntradaEncontrada ItemSelecionado()
		{
			if (listResultado.SelectedItems.Count < 1) return null;
			return listResultado.SelectedItems[0].Tag as EntradaEncontrada;
		}

		private void AbrirItem(EntradaEncontrada item)
		{
			if (item == null) return;
			ProcessStartInfo psi = new ProcessStartInfo(item.CaminhoCompleto);
			psi.UseShellExecute = true;
			Process.Start(psi);
		}

		private void AbrirLocalItem(EntradaEncontrada item)
		{
			if (item == null) return;
			util.ShellHelper.ExploreSelect(item.CaminhoCompleto);
		}

		private void AbrirPropriedadesItem(EntradaEncontrada item)
		{
			if (item == null) return;
			util.ShellHelper.ShowFileProperties(item.CaminhoCompleto);
		}

		private void listResultado_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				AbrirItem(ItemSelecionado());
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void buttonSelecionarLocal_Click(object sender, EventArgs e)
		{
			folderBrowserDialog1.Description = "Selecione o local de busca";
			if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				textLocalPesquisa.Text = folderBrowserDialog1.SelectedPath;
			}
		}

		private void FormPesquisar_FormClosing(object sender, FormClosingEventArgs e)
		{
			EncerrarForm();
		}

		private void linkSelecionarTodos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			foreach (ListViewItem item in listResultado.Items)
				item.Checked = true;
		}

		private void linkSelecionarInverter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			foreach (ListViewItem item in listResultado.Items)
				item.Checked = !item.Checked;
		}

		private void buttonConverter_Click(object sender, EventArgs e)
		{
            if (Properties.Settings.Default.soffice_executavel == null)
            {
                MessageBox.Show("Selecione o executavel do LibreOffice versão 3.6.", "Libre Office");
                return;
            }
            else if (Properties.Settings.Default.soffice_executavel == "")
            {
                MessageBox.Show("Selecione o executavel do LibreOffice versão 3.6.", "Libre Office");
                return;
            }
			if (listResultado.CheckedItems.Count == 0)
			{
				MessageBox.Show("Selecione os arquivos a serem convertidos.", "Conversor");
				comboTipoConversor.Focus();
				return;
			}
			if (comboTipoConversor.SelectedItem == null)
			{
				MessageBox.Show("Selecione o tipo de conversor a ser usado", "Conversor");
				comboTipoConversor.Focus();
				return;
			}
			if (MessageBox.Show(
				String.Format("Converter os arquivos selecionados para o formato '{0}'?",
				comboTipoConversor.SelectedItem), "Conversor", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
				return;

            ProcedimentoLogger.Default.Clear();
			ProcedimentoLogger.Default.Inicializar();
			ProcedimentoLogger.Default.AutoShowLog = true;
            
			ProcedimentoLogger.Default.LogInfo("Conversão iniciada");

			BackgroundWorker bw = new BackgroundWorker();
			bw.DoWork += new DoWorkEventHandler(bw_DoWork);
			bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

			List<EntradaEncontrada> lista = new List<EntradaEncontrada>();
			foreach(ListViewItem item in listResultado.CheckedItems)
				lista.Add(item.Tag as EntradaEncontrada);

			this.Enabled = false;
			bw.RunWorkerAsync(new object[] { comboTipoConversor.SelectedItem, lista});
		}

		void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			ProcedimentoLogger.Default.LogInfo("Conversão finalizada");
			this.Enabled = true;
		}

		void bw_DoWork(object sender, DoWorkEventArgs e)
		{
            List<EntradaEncontrada> lista = (List<EntradaEncontrada>)((object[])(e.Argument))[1];
            ConversorLibreOffice35 conversor = new ConversorLibreOffice35();
			if (conversor == null) return;
            conversor.ConvertTo =((ConversorLibreOfficeListItemAdapter)(((object[])(e.Argument))[0])).ConvertTo.ToString();
            conversor.ConverterBatch(lista);
		}

        private void LinkInformarLibreOffice_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Executavel (*.exe)|*.exe";
            if (Properties.Settings.Default.soffice_executavel == null)
                openFileDialog1.InitialDirectory = "c:\\";
            else if (Properties.Settings.Default.soffice_executavel == "")
                openFileDialog1.InitialDirectory = "c:\\";
            else if (!File.Exists(Properties.Settings.Default.soffice_executavel))
                openFileDialog1.InitialDirectory = "c:\\";
            else
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(Properties.Settings.Default.soffice_executavel);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                Properties.Settings.Default.soffice_executavel = openFileDialog1.FileName;
        }


	}
}
