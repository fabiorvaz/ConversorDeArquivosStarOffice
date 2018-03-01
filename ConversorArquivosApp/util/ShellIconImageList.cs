using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.ComponentModel;

namespace Olvebra.ConversorArquivosApp.util
{
    public class ShellIconImageList : Component
    {
        public const string P_FOLDER_ICON_KEY = "{folder}";

        public ImageList SmallImageList { get; protected set; }
        public ImageList LargeImageList { get; protected set; }

        public bool IndividualFolderIcons { get; set; }

        public ShellIconImageList()
        {
            this.SmallImageList = new ImageList();
            this.LargeImageList = new ImageList();
            IndividualFolderIcons = false;
        }

        public int GetSmallIcon(string fileName)
        {
            string iconKey = GetFileNameKey(fileName);

            if (SmallImageList.Images.ContainsKey(iconKey))
                return SmallImageList.Images.IndexOfKey(iconKey);

            LoadIcon(iconKey, fileName);

            return SmallImageList.Images.IndexOfKey(iconKey);
        }        

        public int GetFolderSmallIcon(string fileName)
        {
            string iconKey = P_FOLDER_ICON_KEY;
            if (IndividualFolderIcons) iconKey = fileName.ToLower();

            if (SmallImageList.Images.ContainsKey(iconKey))
                return SmallImageList.Images.IndexOfKey(iconKey);

            LoadIcon(iconKey, fileName);

            return SmallImageList.Images.IndexOfKey(iconKey);
        }

        private string GetFileNameKey(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();
            if ("|.exe|.ico|.ani|.cur|".Contains("|" + extension + "|"))
                return fileName.ToLower();
            if (Directory.Exists(fileName))
            {
                if (IndividualFolderIcons)
                    return fileName.ToLower();
                else
                    return P_FOLDER_ICON_KEY;
            }
            return extension.ToLower();
        }

        public int GetLargeIcon(string fileName)
        {
            string iconKey = GetFileNameKey(fileName);

            if (LargeImageList.Images.ContainsKey(iconKey))
                return LargeImageList.Images.IndexOfKey(iconKey);

            LoadIcon(iconKey, fileName);

            return LargeImageList.Images.IndexOfKey(iconKey);
        }

        private void LoadIcon(string iconKey, string fileName)
        {
            Icon smallIcon, largeIcon;

            util.ShellHelper.GetIcon(fileName, out smallIcon, out largeIcon);

            if (!SmallImageList.Images.ContainsKey(iconKey))
                SmallImageList.Images.Add(iconKey, smallIcon);

            if (!LargeImageList.Images.ContainsKey(iconKey))
                LargeImageList.Images.Add(iconKey, largeIcon);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            ImageList obj;
            if (SmallImageList != null)
            {
                obj = SmallImageList;
                SmallImageList = null;
                obj.Dispose();
            }
            if (LargeImageList != null)
            {
                obj = LargeImageList;
                LargeImageList = null;
                obj.Dispose();
            }
        }

    }
}
