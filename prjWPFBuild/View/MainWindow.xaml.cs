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

                string fileExt;

                foreach (string file in filepath)
                {
                    //yanlislik cikiyor burada .sln dosyasi bulamiyor file'da
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
                Txt = new clsText(txtOld, txtNew);
                string allText = "";
                foreach (string file in Filecls.filepath)
                {
                    Console.WriteLine(file);

                    using (var stream = System.IO.File.Open(file, FileMode.Open, FileAccess.ReadWrite))
                    {

                        StreamReader sr = new StreamReader(stream);
                        StreamWriter sw = new StreamWriter(stream);
                        string contents;
                        string pattern = String.Format(@"\b{0}\b", txtOld.Text);
                        string oldContents = "";
                        //string replace = txtNew.Text;
                        while ((contents = sr.ReadLine()) != null)
                        {
                            Console.WriteLine(pattern);
                            Console.WriteLine(contents);

                            allText = allText + "\n" + contents;
                            /*
                            if (Regex.IsMatch(contents, pattern))
                            {

                                contents = Regex.Replace(contents, pattern, txtNew.Text);


                                Console.WriteLine(contents);

                                //Regex.Replace(contents, pattern, replace);

                                
                            }*/
                            if (contents == oldContents)
                                break;
                            oldContents = contents;
                        }
                        Console.WriteLine(allText);


                        if (Regex.IsMatch(allText, pattern))
                        {
                            //buraya silme işlemi yapılcak

                            //file.Replace()




                            //sw.Write(Regex.Replace(allText, pattern, txtNew.Text));
                            // sw.Write(file, Regex.Replace(allText, pattern, txtNew.Text));
                            //File.WriteAllText(file, allText);
                        }


                        allText = "";
                    }

                }
                MessageBox.Show("İslem tamamlandi!");
            }
        }
    }
}
