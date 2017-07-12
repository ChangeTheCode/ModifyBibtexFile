using System;
using System.Collections.Generic;
using System.Text;

namespace ModifyBibtex
{
    public interface IBibItem
    {
        string Typ { get; set; }
        string Title { get;  set; }
        string URL { get;  set; }
        string Tail { get;  set; }
        string MiddleSection { get; set; }
    }
}
