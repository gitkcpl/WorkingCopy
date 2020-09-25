using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.App.Shared
{
    public static class KontoHelp
    {
        public static string ListBarHelpMsg = "New=>Ins, Edit =>Ctrl+Enter, Delete=> Ctl+Del, Print=>Ctrl+P, Refresh=>Ctrl+R";
        public static string MainBarHelpMsg = "New=>Ins, Save =>F10, Print=>Ctrl+P, Find=>F5, List=>F12";
    }

    public class ComboBoxPairs
    {
        public string _Key { get; set; }
        public string _Value { get; set; }

        public ComboBoxPairs() { }
        public ComboBoxPairs(string _key, string _value)
        {
            _Key = _key;
            _Value = _value;
        }
    }
}
