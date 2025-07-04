using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAppPlayground.DIExample.medianB
{
    public interface IDataSource
    {
        string[] ReadData();
    }
}
