using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSH.Core.Domain
{
    public interface ITreeEntity<T>
    {
        bool IsRoot { get; set; }

        T Parent { get; set; }

        IList<T> Children { get; set; }

    }
}
