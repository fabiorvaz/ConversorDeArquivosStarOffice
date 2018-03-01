using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;

namespace Olvebra.ConversorArquivosApp.util
{
    public static class ShellHelper
    {
        public const int SW_SHOW = 5;
        public const uint SEE_MASK_INVOKEIDLIST = 12;

        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LARGEICON = 0x0; // 'Large icon
        public const uint SHGFI_SMALLICON = 0x1; // 'Small icon

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHELLEXECUTEINFO
        {
            public int cbSize;
            public uint fMask;
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpVerb;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpFile;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpParameters;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpDirectory;
            public int nShow;
            public IntPtr hInstApp;
            public IntPtr lpIDList;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpClass;
            public IntPtr hkeyClass;
            public uint dwHotKey;
            public IntPtr hIcon;
            public IntPtr hProcess;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        /// <summary>
        /// Shows the File Properties Shell dialog box.
        /// 
        /// Source: http://stackoverflow.com/questions/1936682/how-do-i-display-a-files-properties-dialog-from-c
        /// Author: feedwall http://stackoverflow.com/users/1462094/feedwall
        /// Date..: 15/09/2014 11:43
        /// </summary>
        /// <param name="Filename">File name to show the properties</param>
        /// <returns>True/false if the dialog was shown.</returns>
        public static bool ShowFileProperties(string Filename)
        {
            SHELLEXECUTEINFO info = new SHELLEXECUTEINFO();
            info.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(info);
            info.lpVerb = "properties";
            info.lpFile = Filename;
            info.nShow = SW_SHOW;
            info.fMask = SEE_MASK_INVOKEIDLIST;
            return ShellExecuteEx(ref info);
        }

        public static void ExploreSelect(string Filename)
        {
            string arguments = String.Format(
                "/select,\"{0}\"", 
                Filename);
            ProcessStartInfo psi = new ProcessStartInfo("explorer.exe", arguments);
            Process.Start(psi);
        }

        public static void GetIcon(string Filename, out Icon SmallIcon, out Icon LargeIcon)
        {
            IntPtr hImgSmall;
            IntPtr hImgLarge;

            SHFILEINFO shinfo = new SHFILEINFO();

            /*
             * uFlags:
             * SHGFI_ADDOVERLAYS (0x000000020)
             * */

            //The icon is returned in the hIcon member of the shinfo struct
            hImgSmall = SHGetFileInfo(Filename, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_SMALLICON);
            SmallIcon = Icon.FromHandle(shinfo.hIcon);

            hImgLarge = SHGetFileInfo(Filename, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_LARGEICON);
            LargeIcon = Icon.FromHandle(shinfo.hIcon);
        }
    }
}
