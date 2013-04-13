using System;
using NSH.Core.Domain;

namespace NSH.Core.WPF
{
    public interface ISelectView<T>
        : IView where T : EntityBase
    {
        T Selected { get; set; }
    }
}
