using Microsoft.Win32;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace prjWPFBuild
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        clsText Txt;
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

                foreach (string file in filepath)
                {
                    if (file.Contains(".sln"))
                    {
                        isTrueFile = true;
                    }

                    if (isTrueFile == false)
                    {
                        
                        filepath = null;
                        fileName = "Dosyanizi buraya surukleyin";
                        lblFileName.Content = fileName;
                        files = null;
                        MessageBox.Show("Dosya tipi desteklenmiyor!");
                        break;
                    }
                }
            }
        }
        private void btnCalistir_Click(object sender, RoutedEventArgs e)
        {
            if(txtOld.Text == "" || txtNew.Text == "")
            {
                MessageBox.Show("Yeni ve eski bölümlerinin ikisi de dolu olmalı");
            }
            else
            {
                Txt = new clsText(txtOld, txtNew);
                foreach (string file in Filecls.filepath)
                {
                    Console.WriteLine(file);
                    
                    using (var stream = System.IO.File.Open(file, FileMode.Open, FileAccess.ReadWrite))
                    {

                        StreamReader sr = new StreamReader(stream);
                        StreamWriter sw = new StreamWriter(stream);
                        string contents;
                        string pattern = String.Format(@"\b{0}\b", txtOld.Text); ;
                        //string replace = txtNew.Text;
                        while((contents = sr.ReadLine()) != null)
                        {
                            Console.WriteLine(pattern);
                            Console.WriteLine(contents);
                            if (Regex.IsMatch(contents,pattern))
                            {
                                
                                sw.WriteLine(Regex.Replace(contents, pattern, txtNew.Text));
                                //sw.WriteLine(contents.Replace(txtOld.Text, txtNew.Text));
                                contents = Regex.Replace(contents, pattern, txtNew.Text);

                                




                                Console.WriteLine(contents);
                                
                                //Regex.Replace(contents, pattern, replace);
                                break;
                            }
                            
                        }


                    }

                }
                MessageBox.Show("İslem tamamlandi!");
            }
        }
    }
}
