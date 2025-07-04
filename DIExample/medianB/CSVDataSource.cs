using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAppPlayground.DIExample.medianB
{
    public class CSVDataSource: IDataSource
    {
        /// <summary>
        /// Faking it for example purposes
        /// </summary>

        //private readonly string _filePath;
        //public CSVDataSource(string filePath)
        //{
        //    _filePath = filePath;
        //}

        public string[] ReadData()
        {
            Debug.Print("CSVDataSource.ReadData called.");
            return new string[]
            {
                "Name,Value",
                "A,10",
                "B,20",
                "C,30",
                "D,40"
            };
            //if (!System.IO.File.Exists(_filePath))
            //{
            //    throw new FileNotFoundException($"The file {_filePath} does not exist.");
            //}
            //return System.IO.File.ReadAllLines(_filePath);
        }
    }
}
