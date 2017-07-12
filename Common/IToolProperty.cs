using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public interface IToolProperty
    {
        string _sourceFile { get; set; }
        string _finalFile { get; set; }
        IDictionary<string, string> ReplacingCharactarsDictionary { get; set; }
        void Initial();
    }
}
