using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Olvebra.ConversorArquivosApp.forms;

namespace Olvebra.ConversorArquivosApp
{
	static class Program
	{
		public static Control control;
		public static string[] Args;
		public static Form MainForm;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Args = args;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FormPesquisar());
		}
	}
}
