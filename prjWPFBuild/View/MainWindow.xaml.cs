using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace prjWPFBuild
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        clsFile Filecls;

        private void FileDropPanel_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //File path alıyor
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                string fileName = Path.GetFileName(files[0]);

                lblFileName.Content = fileName;

                string[] filepath = Directory.GetFiles(files[0], "*.*", SearchOption.AllDirectories);

                bool isTrueFile = false;

                Filecls = new clsFile(files, filepath);

                string fileExt;

                foreach (string file in filepath)
                {
                    fileExt = System.IO.Path.GetExtension(file);
                    if (fileExt == ".sln")
                    {
                        isTrueFile = true;
                        break;
                    }
                    else
                    {
                        isTrueFile = false;
                    }
                    Console.WriteLine(file);
                }
                if (isTrueFile == false)
                {
                    filepath = null;
                    fileName = "Dosyanizi buraya surukleyin";
                    lblFileName.Content = fileName;
                    files = null;
                    MessageBox.Show("Dosya tipi desteklenmiyor!");
                }
                if(isTrueFile)
                {
                    string sourcepath = files[0];
                    string targetpath = files[0] + "Copied";

                    foreach (string dirPath in Directory.GetDirectories(sourcepath, "*", SearchOption.AllDirectories))
                    {
                        Directory.CreateDirectory(dirPath.Replace(sourcepath, targetpath));
                    }

                    //Copy all the files & Replaces any files with the same name
                    foreach (string newPath in Directory.GetFiles(sourcepath, "*.*", SearchOption.AllDirectories))
                    {
                        File.Copy(newPath, newPath.Replace(sourcepath, targetpath), true);
                    }
                    string[] copiedfilepath = Directory.GetFiles(targetpath, "*.*", SearchOption.AllDirectories);
                    Filecls.filepath = copiedfilepath;
                }
            }
        }

        private void btnCalistir_Click(object sender, RoutedEventArgs e)
        {

            if (txtOld.Text == "" || txtNew.Text == "")
            {
                MessageBox.Show("Yeni ve eski bölümlerinin ikisi de dolu olmalı");
            }
            else
            {
                string text;
                string pattern = String.Format(@"\b{0}\b", txtOld.Text);
                foreach (string file in Filecls.filepath)
                {
                    text = File.ReadAllText(file);

                    if(Regex.IsMatch(text, pattern))
                    {
                        text = text.Replace(txtOld.Text, txtNew.Text);
                        File.WriteAllText(file, text);
                    }
                }
                MessageBox.Show("İslem tamamlandi!");
            }
        }
    }
}
