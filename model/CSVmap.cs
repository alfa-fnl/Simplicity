using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularityTestingApp.model
{
    public sealed class csvMap : CsvHelper.Configuration.ClassMap<Data>
    {
        public csvMap()
        {
            // Menggunakan Map method untuk meng-map properties ke kolom-kolom CSV
            Map(m => m.Number).Name("Number");
            Map(m => m.Objects).Name("Class Name/Object");
            Map(m => m.Usage).Name("Usage");
            Map(m => m.Comment).Name("Fan In");
            Map(m => m.Occurance).Name("Occurance");
            Map(m => m.Simplicity).Name("Modularity");

        }
    }
}
