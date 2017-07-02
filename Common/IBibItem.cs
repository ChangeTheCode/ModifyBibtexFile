using System;
using System.Collections.Generic;
using System.Text;

namespace ModifyBibtex
{
    public interface IBibItem
    {
        string Title { get;  set; }
        string URL { get;  set; }
        string Tail { get;  set; }

    }
}
