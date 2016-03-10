using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RustyDragonBasesAndInterfaces.Models;

namespace RustyDragonBasesAndInterfaces.Reader
{
    public interface IReader
    {
        IList<ICheese> Load(string filePath);
    }
}
