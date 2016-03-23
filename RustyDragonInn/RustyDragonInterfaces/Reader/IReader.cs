using RustyDragonBasesAndInterfaces.Models;
using System.Collections.Generic;

namespace RustyDragonBasesAndInterfaces.Reader
{
    public interface IReader
    {
        IList<ICheese> Load(string filePath);
    }
}