using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularityTestingApp.model
{
    public class Data
    {
        [Name("Number")]
        public string Number { get; set; }

        [Name("Object Name")]
        public string Objects { get; set; }

        [Name("Usage")]
        public string Usage { get; set; }

        [Name("Comment")]
        public string Comment { get; set; }

        [Name("Occurance")]
        public string Occurance { get; set; }
        public string Simplicity { get; set; }
    }
}
