namespace Olvebra.ConversorArquivosApp.forms
{
    partial class FormPesquisar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.textNomeArquivo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textLocalPesquisa = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textConteudoArquivo = new System.Windows.Forms.TextBox();
            this.checkPesquisarSubdiretorios = new System.Windows.Forms.CheckBox();
            this.buttonPesquisar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.listResultado = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuItemEncontrado = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirLocalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propriedadesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelProgresso = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkIgnoreCase = new System.Windows.Forms.CheckBox();
            this.checkIgnoreHiddenFolders = new System.Windows.Forms.CheckBox();
            this.checkUseThreadPool = new System.Windows.Forms.CheckBox();
            this.buttonSelecionarLocal = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.linkSelecionarTodos = new System.Windows.Forms.LinkLabel();
            this.linkSelecionarInverter = new System.Windows.Forms.LinkLabel();
            this.buttonConverter = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.comboTipoConversor = new System.Windows.Forms.ComboBox();
            this.LinkInformarLibreOffice = new System.Windows.Forms.LinkLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pesquisa1 = new Olvebra.ConversorArquivosApp.pesquisa.Pesquisa(this.components);
            this.shellIconImageList1 = new Olvebra.ConversorArquivosApp.util.ShellIconImageList();
            this.contextMenuItemEncontrado.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nome do arquivo:";
            // 
            // textNomeArquivo
            // 
            this.textNomeArquivo.AllowDrop = true;
            this.textNomeArquivo.Location = new System.Drawing.Point(12, 25);
            this.textNomeArquivo.Name = "textNomeArquivo";
            this.textNomeArquivo.Size = new System.Drawing.Size(190, 20);
            this.textNomeArquivo.TabIndex = 1;
            this.textNomeArquivo.Text = "*.txt";
            this.textNomeArquivo.DragDrop += new System.Windows.Forms.DragEventHandler(this.textNomeArquivo_DragDrop);
            this.textNomeArquivo.DragEnter += new System.Windows.Forms.DragEventHandler(this.Evento_DragEnter_Arquivo);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Local de pesquisa:";
            // 
            // textLocalPesquisa
            // 
            this.textLocalPesquisa.AllowDrop = true;
            this.textLocalPesquisa.Location = new System.Drawing.Point(12, 66);
            this.textLocalPesquisa.Name = "textLocalPesquisa";
            this.textLocalPesquisa.Size = new System.Drawing.Size(365, 20);
            this.textLocalPesquisa.TabIndex = 5;
            this.textLocalPesquisa.Text = "c:\\Programacao";
            this.textLocalPesquisa.DragDrop += new System.Windows.Forms.DragEventHandler(this.textLocalPesquisa_DragDrop);
            this.textLocalPesquisa.DragEnter += new System.Windows.Forms.DragEventHandler(this.Evento_DragEnter_Arquivo);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(208, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Conteúdo do arquivo:";
            // 
            // textConteudoArquivo
            // 
            this.textConteudoArquivo.Location = new System.Drawing.Point(208, 25);
            this.textConteudoArquivo.Name = "textConteudoArquivo";
            this.textConteudoArquivo.Size = new System.Drawing.Size(207, 20);
            this.textConteudoArquivo.TabIndex = 3;
            // 
            // checkPesquisarSubdiretorios
            // 
            this.checkPesquisarSubdiretorios.AutoSize = true;
            this.checkPesquisarSubdiretorios.Checked = true;
            this.checkPesquisarSubdiretorios.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkPesquisarSubdiretorios.Location = new System.Drawing.Point(431, 48);
            this.checkPesquisarSubdiretorios.Name = "checkPesquisarSubdiretorios";
            this.checkPesquisarSubdiretorios.Size = new System.Drawing.Size(154, 17);
            this.checkPesquisarSubdiretorios.TabIndex = 9;
            this.checkPesquisarSubdiretorios.Text = "Pesquisar nos subdiretórios";
            this.checkPesquisarSubdiretorios.UseVisualStyleBackColor = true;
            // 
            // buttonPesquisar
            // 
            this.buttonPesquisar.Location = new System.Drawing.Point(12, 92);
            this.buttonPesquisar.Name = "buttonPesquisar";
            this.buttonPesquisar.Size = new System.Drawing.Size(75, 23);
            this.buttonPesquisar.TabIndex = 20;
            this.buttonPesquisar.Text = "Pesquisar";
            this.buttonPesquisar.UseVisualStyleBackColor = true;
            this.buttonPesquisar.Click += new System.EventHandler(this.buttonPesquisar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Resultado:";
            // 
            // listResultado
            // 
            this.listResultado.CheckBoxes = true;
            this.listResultado.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listResultado.ContextMenuStrip = this.contextMenuItemEncontrado;
            this.listResultado.Location = new System.Drawing.Point(12, 138);
            this.listResultado.Name = "listResultado";
            this.listResultado.Size = new System.Drawing.Size(682, 234);
            this.listResultado.TabIndex = 23;
            this.listResultado.UseCompatibleStateImageBehavior = false;
            this.listResultado.View = System.Windows.Forms.View.Details;
            this.listResultado.DoubleClick += new System.EventHandler(this.listResultado_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Nome";
            this.columnHeader1.Width = 227;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Data";
            this.columnHeader2.Width = 105;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Local";
            this.columnHeader3.Width = 321;
            // 
            // contextMenuItemEncontrado
            // 
            this.contextMenuItemEncontrado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.abrirLocalToolStripMenuItem,
            this.propriedadesToolStripMenuItem});
            this.contextMenuItemEncontrado.Name = "contextMenuItemEncontrado";
            this.contextMenuItemEncontrado.Size = new System.Drawing.Size(149, 70);
            this.contextMenuItemEncontrado.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuItemEncontrado_Opening);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // abrirLocalToolStripMenuItem
            // 
            this.abrirLocalToolStripMenuItem.Name = "abrirLocalToolStripMenuItem";
            this.abrirLocalToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.abrirLocalToolStripMenuItem.Text = "Abrir local";
            this.abrirLocalToolStripMenuItem.Click += new System.EventHandler(this.abrirLocalToolStripMenuItem_Click);
            // 
            // propriedadesToolStripMenuItem
            // 
            this.propriedadesToolStripMenuItem.Name = "propriedadesToolStripMenuItem";
            this.propriedadesToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.propriedadesToolStripMenuItem.Text = "Propriedades";
            this.propriedadesToolStripMenuItem.Click += new System.EventHandler(this.propriedadesToolStripMenuItem_Click);
            // 
            // labelProgresso
            // 
            this.labelProgresso.Location = new System.Drawing.Point(93, 92);
            this.labelProgresso.Name = "labelProgresso";
            this.labelProgresso.Size = new System.Drawing.Size(601, 23);
            this.labelProgresso.TabIndex = 22;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkIgnoreCase
            // 
            this.checkIgnoreCase.AutoSize = true;
            this.checkIgnoreCase.Checked = true;
            this.checkIgnoreCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkIgnoreCase.Location = new System.Drawing.Point(431, 12);
            this.checkIgnoreCase.Name = "checkIgnoreCase";
            this.checkIgnoreCase.Size = new System.Drawing.Size(145, 17);
            this.checkIgnoreCase.TabIndex = 7;
            this.checkIgnoreCase.Text = "Ignore case no conteúdo";
            this.checkIgnoreCase.UseVisualStyleBackColor = true;
            // 
            // checkIgnoreHiddenFolders
            // 
            this.checkIgnoreHiddenFolders.AutoSize = true;
            this.checkIgnoreHiddenFolders.Checked = true;
            this.checkIgnoreHiddenFolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkIgnoreHiddenFolders.Location = new System.Drawing.Point(431, 30);
            this.checkIgnoreHiddenFolders.Name = "checkIgnoreHiddenFolders";
            this.checkIgnoreHiddenFolders.Size = new System.Drawing.Size(130, 17);
            this.checkIgnoreHiddenFolders.TabIndex = 8;
            this.checkIgnoreHiddenFolders.Text = "Ignorar pastas ocultas";
            this.checkIgnoreHiddenFolders.UseVisualStyleBackColor = true;
            // 
            // checkUseThreadPool
            // 
            this.checkUseThreadPool.AutoSize = true;
            this.checkUseThreadPool.Checked = true;
            this.checkUseThreadPool.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkUseThreadPool.Location = new System.Drawing.Point(431, 66);
            this.checkUseThreadPool.Name = "checkUseThreadPool";
            this.checkUseThreadPool.Size = new System.Drawing.Size(101, 17);
            this.checkUseThreadPool.TabIndex = 10;
            this.checkUseThreadPool.Text = "Use thread pool";
            this.checkUseThreadPool.UseVisualStyleBackColor = true;
            // 
            // buttonSelecionarLocal
            // 
            this.buttonSelecionarLocal.Location = new System.Drawing.Point(383, 65);
            this.buttonSelecionarLocal.Name = "buttonSelecionarLocal";
            this.buttonSelecionarLocal.Size = new System.Drawing.Size(31, 22);
            this.buttonSelecionarLocal.TabIndex = 6;
            this.buttonSelecionarLocal.Text = "...";
            this.buttonSelecionarLocal.UseVisualStyleBackColor = true;
            this.buttonSelecionarLocal.Click += new System.EventHandler(this.buttonSelecionarLocal_Click);
            // 
            // linkSelecionarTodos
            // 
            this.linkSelecionarTodos.AutoSize = true;
            this.linkSelecionarTodos.Location = new System.Drawing.Point(76, 122);
            this.linkSelecionarTodos.Name = "linkSelecionarTodos";
            this.linkSelecionarTodos.Size = new System.Drawing.Size(33, 13);
            this.linkSelecionarTodos.TabIndex = 24;
            this.linkSelecionarTodos.TabStop = true;
            this.linkSelecionarTodos.Text = "todos";
            this.linkSelecionarTodos.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSelecionarTodos_LinkClicked);
            // 
            // linkSelecionarInverter
            // 
            this.linkSelecionarInverter.AutoSize = true;
            this.linkSelecionarInverter.Location = new System.Drawing.Point(115, 122);
            this.linkSelecionarInverter.Name = "linkSelecionarInverter";
            this.linkSelecionarInverter.Size = new System.Drawing.Size(42, 13);
            this.linkSelecionarInverter.TabIndex = 25;
            this.linkSelecionarInverter.TabStop = true;
            this.linkSelecionarInverter.Text = "inverter";
            this.linkSelecionarInverter.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSelecionarInverter_LinkClicked);
            // 
            // buttonConverter
            // 
            this.buttonConverter.Location = new System.Drawing.Point(12, 386);
            this.buttonConverter.Name = "buttonConverter";
            this.buttonConverter.Size = new System.Drawing.Size(154, 23);
            this.buttonConverter.TabIndex = 26;
            this.buttonConverter.Text = "Converter selecionados";
            this.buttonConverter.UseVisualStyleBackColor = true;
            this.buttonConverter.Click += new System.EventHandler(this.buttonConverter_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(173, 391);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "para tipo";
            // 
            // comboTipoConversor
            // 
            this.comboTipoConversor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTipoConversor.FormattingEnabled = true;
            this.comboTipoConversor.Location = new System.Drawing.Point(227, 388);
            this.comboTipoConversor.Name = "comboTipoConversor";
            this.comboTipoConversor.Size = new System.Drawing.Size(334, 21);
            this.comboTipoConversor.TabIndex = 29;
            // 
            // LinkInformarLibreOffice
            // 
            this.LinkInformarLibreOffice.AutoSize = true;
            this.LinkInformarLibreOffice.Location = new System.Drawing.Point(506, 122);
            this.LinkInformarLibreOffice.Name = "LinkInformarLibreOffice";
            this.LinkInformarLibreOffice.Size = new System.Drawing.Size(188, 13);
            this.LinkInformarLibreOffice.TabIndex = 30;
            this.LinkInformarLibreOffice.TabStop = true;
            this.LinkInformarLibreOffice.Text = "Informar localização do LibreOffice 3.6";
            this.LinkInformarLibreOffice.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkInformarLibreOffice_LinkClicked);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pesquisa1
            // 
            this.pesquisa1.Cancelado = false;
            this.pesquisa1.DiretorioOrigem = null;
            this.pesquisa1.FiltroArquivo = null;
            this.pesquisa1.IgnorarPastasOcultas = true;
            this.pesquisa1.MaximoDeResultados = 1000;
            this.pesquisa1.NomeArquivo = null;
            this.pesquisa1.PesquisarSubdiretorios = false;
            this.pesquisa1.Tag = null;
            this.pesquisa1.UseThreads = false;
            this.pesquisa1.ArquivoEncontrado += new Olvebra.ConversorArquivosApp.pesquisa.Pesquisa.ArquivoEncontradoDelegate(this.pesquisa1_ArquivoEncontrado);
            this.pesquisa1.MensagemProgresso += new Olvebra.ConversorArquivosApp.pesquisa.Pesquisa.MensagemProgressoDelegate(this.pesquisa1_MensagemProgresso);
            this.pesquisa1.PesquisaConcluida += new System.EventHandler(this.pesquisa1_PesquisaConcluida);
            // 
            // shellIconImageList1
            // 
            this.shellIconImageList1.IndividualFolderIcons = false;
            // 
            // FormPesquisar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 421);
            this.Controls.Add(this.LinkInformarLibreOffice);
            this.Controls.Add(this.comboTipoConversor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonConverter);
            this.Controls.Add(this.linkSelecionarInverter);
            this.Controls.Add(this.linkSelecionarTodos);
            this.Controls.Add(this.checkUseThreadPool);
            this.Controls.Add(this.buttonSelecionarLocal);
            this.Controls.Add(this.checkIgnoreHiddenFolders);
            this.Controls.Add(this.checkIgnoreCase);
            this.Controls.Add(this.labelProgresso);
            this.Controls.Add(this.listResultado);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonPesquisar);
            this.Controls.Add(this.checkPesquisarSubdiretorios);
            this.Controls.Add(this.textConteudoArquivo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textLocalPesquisa);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textNomeArquivo);
            this.Controls.Add(this.label1);
            this.Name = "FormPesquisar";
            this.ShowIcon = false;
            this.Text = "Conversor de Arquivos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPesquisar_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormPesquisar_FormClosed);
            this.Load += new System.EventHandler(this.FormPesquisar_Load);
            this.Resize += new System.EventHandler(this.FormPesquisar_Resize);
            this.contextMenuItemEncontrado.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textNomeArquivo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textLocalPesquisa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textConteudoArquivo;
        private System.Windows.Forms.CheckBox checkPesquisarSubdiretorios;
        private System.Windows.Forms.Button buttonPesquisar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView listResultado;
        private pesquisa.Pesquisa pesquisa1;
        private System.Windows.Forms.Label labelProgresso;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.CheckBox checkIgnoreCase;
        private System.Windows.Forms.CheckBox checkIgnoreHiddenFolders;
        private System.Windows.Forms.CheckBox checkUseThreadPool;
        private System.Windows.Forms.Button buttonSelecionarLocal;
        private System.Windows.Forms.ContextMenuStrip contextMenuItemEncontrado;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirLocalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propriedadesToolStripMenuItem;
        private util.ShellIconImageList shellIconImageList1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.LinkLabel linkSelecionarTodos;
        private System.Windows.Forms.LinkLabel linkSelecionarInverter;
		private System.Windows.Forms.Button buttonConverter;
		  private System.Windows.Forms.Label label5;
		  private System.Windows.Forms.ComboBox comboTipoConversor;
          private System.Windows.Forms.LinkLabel LinkInformarLibreOffice;
          private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

