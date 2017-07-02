using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public interface IToolProperty
    {
        string SourceFile { get; set; }
        string FinalFile { get; set; }
        IDictionary<string, string> ReplacingCharactarsDictionary { get; set; }
        void Initial();
    }
}
