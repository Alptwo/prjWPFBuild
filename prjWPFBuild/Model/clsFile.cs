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

class clsFile
{
    private static string[] _files;
    private static string[] _filepath;

    public clsFile(string[] files,string[] filepath)
    {
        _files = files;
        _filepath = filepath;
    }

    public string[] files
    {
        get
        {
            return _files;
        }
        set
        {
            _files = value;
        }
    }

    public string[] filepath
    {
        get
        {
            return _filepath;
        }
        set
        {
            _filepath = value;
        }
    }

}

