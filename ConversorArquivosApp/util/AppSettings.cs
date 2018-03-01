using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Reflection;

namespace Olvebra.ConversorArquivosApp.util
{
    public class AppSettings
    {
        private static AppSettings m_default;

        public static AppSettings Default
        {
            get
            {
                if (m_default == null)
                {
                    m_default = new AppSettings();
                }
                return m_default;
            }
        }

        protected string GetNomeChaveUser()
        {
            Assembly ass = Assembly.GetExecutingAssembly();
            AssemblyName asn = ass.GetName();
            return String.Format("HKEY_CURRENT_USER\\Software\\Olvebra\\{0}\\Settings",
                asn.Name);
        }

        protected string GetNomeChaveGlobal()
        {
            Assembly ass = Assembly.GetExecutingAssembly();
            AssemblyName asn = ass.GetName();
            return String.Format("HKEY_LOCAL_MACHINE\\Software\\Olvebra\\{0}\\Settings",
                asn.Name);
        }


        public string User_LoadString(string nomeValor)
        {
            return Registry.GetValue(GetNomeChaveUser(), nomeValor, null) as string;
        }

        public string User_LoadString(string nomeValor, string valorDefault)
        {
            return Registry.GetValue(GetNomeChaveUser(), nomeValor, valorDefault) as string;
        }

        public void User_SaveString(string nomeValor, string valor)
        {
            Registry.SetValue(GetNomeChaveUser(), nomeValor, valor);
        }

        public string[] User_LoadParams(string nomeValor)
        {
            string ret = Registry.GetValue(GetNomeChaveUser(), nomeValor, "") as string;
            return Params_FromStr(ret);
        }


        public string Global_LoadString(string nomeValor)
        {
            return Registry.GetValue(GetNomeChaveGlobal(), nomeValor, null) as string;
        }

        public string Global_LoadString(string nomeValor, string valorDefault)
        {
            return Registry.GetValue(GetNomeChaveGlobal(), nomeValor, valorDefault) as string;
        }

        public void Global_SaveString(string nomeValor, string valor)
        {
            Registry.SetValue(GetNomeChaveGlobal(), nomeValor, valor);
        }


        public static string Params_ToStr(params string[] lista)
        {
            return String.Join("|", lista);
        }

        public static string[] Params_FromStr(string str)
        {
            if (str != null)
                return str.Split('|');
            else
                return (new string[0]);
        }

        public static string Params_GetString(string[] parametros, int pos, string valorDefault)
        {
            if (parametros.Length > pos) return parametros[pos];
            return valorDefault;
        }

        public static int Params_GetInt(string[] parametros, int pos, int valorDefault)
        {
            if (parametros.Length <= pos) return valorDefault;
            int valor;
            if (!Int32.TryParse(parametros[pos], out valor)) return valorDefault;
            return valor;
        }

        public static int Params_GetInt(string[] parametros, int pos, int valorDefault, int minimo, int maximo)
        {
            if (parametros.Length <= pos) return valorDefault;
            int valor;
            if (!Int32.TryParse(parametros[pos], out valor)) return valorDefault;
            if (valor < minimo || valor > maximo) return valorDefault;
            return valor;
        }

        public static bool Params_GetBool(string[] parametros, int pos, bool valorDefault)
        {
            if (parametros.Length <= pos) return valorDefault;
            string ret = parametros[pos].Substring(0, 1).ToLower();
            if (ret.IndexOfAny(new char[] { 's', 'y', 't', '1' }) >= 0)
                return true;
            if (ret.IndexOfAny(new char[] { 'n', 'f', '0' }) >= 0)
                return false;
            return valorDefault;
        }


        public static string BoolToStr(bool valor)
        {
            if (valor) return "1";
            return "0";
        }


        public void SaveFormState(Form form)
        {
            if (form.WindowState == FormWindowState.Minimized) return;
            string str = Params_ToStr(
                ((int)form.WindowState).ToString(),
                form.Top.ToString(),
                form.Left.ToString(),
                form.Height.ToString(),
                form.Width.ToString());
            User_SaveString(form.GetType().Name, str);
        }

        public void LoadFormState(Form form)
        {
            string ret = User_LoadString(form.GetType().Name, null);
            if (ret == null) return;
            string[] parametros = Params_FromStr(ret);

            int height = Params_GetInt(parametros, 3, form.Height, 128, form.Height * 3);
            int width = Params_GetInt(parametros, 4, form.Width, 160, form.Width * 3);

            form.Top = Params_GetInt(parametros, 1, form.Top, SystemInformation.VirtualScreen.Top, SystemInformation.VirtualScreen.Bottom);
            form.Left = Params_GetInt(parametros, 2, form.Left, SystemInformation.VirtualScreen.Left, SystemInformation.VirtualScreen.Right);

            FormWindowState state = (FormWindowState)Params_GetInt(parametros, 0, ((int)form.WindowState), 0, 2);
            if (state != FormWindowState.Minimized) form.WindowState = state;

            if (form.WindowState == FormWindowState.Normal)
            {
                form.Height = height;
                form.Width = width;
            }
        }

    }
}
