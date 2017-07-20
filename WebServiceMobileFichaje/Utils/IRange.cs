using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebServiceMobileFichaje.Utils
{
    public interface IRange<T>
    {
        T Start { get; }
        T End { get; }
        bool Includes(T value);
        bool Includes(IRange<T> range);
    }
}
