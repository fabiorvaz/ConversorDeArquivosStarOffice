namespace Olvebra.ConversorArquivosApp.forms
{
    partial class FormProcedimentoLogViewer
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.webMensagens = new System.Windows.Forms.WebBrowser();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonLimpar = new System.Windows.Forms.Button();
            this.buttonFechar = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.webMensagens, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(740, 334);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // webMensagens
            // 
            this.webMensagens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webMensagens.Location = new System.Drawing.Point(3, 3);
            this.webMensagens.MinimumSize = new System.Drawing.Size(20, 20);
            this.webMensagens.Name = "webMensagens";
            this.webMensagens.Size = new System.Drawing.Size(734, 278);
            this.webMensagens.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonLimpar);
            this.panel1.Controls.Add(this.buttonFechar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(416, 287);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(321, 44);
            this.panel1.TabIndex = 2;
            // 
            // buttonLimpar
            // 
            this.buttonLimpar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonLimpar.Image = global::Olvebra.ConversorArquivosApp.Properties.Resources.door_in;
            this.buttonLimpar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLimpar.Location = new System.Drawing.Point(118, 3);
            this.buttonLimpar.Name = "buttonLimpar";
            this.buttonLimpar.Size = new System.Drawing.Size(97, 37);
            this.buttonLimpar.TabIndex = 2;
            this.buttonLimpar.Text = "Limpar";
            this.buttonLimpar.UseVisualStyleBackColor = true;
            this.buttonLimpar.Click += new System.EventHandler(this.buttonLimpar_Click);
            // 
            // buttonFechar
            // 
            this.buttonFechar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonFechar.Image = global::Olvebra.ConversorArquivosApp.Properties.Resources.door_in;
            this.buttonFechar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonFechar.Location = new System.Drawing.Point(221, 3);
            this.buttonFechar.Name = "buttonFechar";
            this.buttonFechar.Size = new System.Drawing.Size(97, 37);
            this.buttonFechar.TabIndex = 1;
            this.buttonFechar.Text = "Fechar\r\n";
            this.buttonFechar.UseVisualStyleBackColor = true;
            this.buttonFechar.Click += new System.EventHandler(this.buttonFechar_Click);
            // 
            // FormProcedimentoLogViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 354);
            this.Controls.Add(this.tableLayoutPanel1);
            this.KeyPreview = true;
            this.Name = "FormProcedimentoLogViewer";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Registro de Informações";
            this.Shown += new System.EventHandler(this.Form_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

		  private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		  private System.Windows.Forms.WebBrowser webMensagens;
		  private System.Windows.Forms.Panel panel1;
		  private System.Windows.Forms.Button buttonFechar;
		  private System.Windows.Forms.Button buttonLimpar;


	 }
}