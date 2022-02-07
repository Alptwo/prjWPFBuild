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

public class clsText
{
    private static string _oldText;
    private static string _newText;

    public clsText(TextBox txtOldText, TextBox txtNewText)
    {
        _newText = txtNewText.Text;
        _oldText = txtOldText.Text;
    }

    public static string oldText
    {
        get
        {
            return _oldText;
        }
        set
        {
            _oldText = value;
        }
    }
    public static string NewText
    {
        get
        {
            return _newText;
        }
        set
        {
            _newText = value;
        }
    }



}
