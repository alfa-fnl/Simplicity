using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularityTestingApp.model
{
    internal class ObjectList
    {
		private string? className = "";

		public string? ClassName
		{
			get { return className; }
			set { className = value; }
		}

		private string? objectName = "";

		public string? ObjectName
		{
			get { return objectName; }
			set { objectName = value; }
		}

		private int? objectUsage = 0;

		public int? ObjectUsage
		{
			get { return objectUsage; }
			set { objectUsage = value; }
		}

		private int? comment = 0;

		public int? Comment
		{
			get { return comment; }
			set { comment = value; }
		}

        private int? occurance = 0;

        public int? Occurance
        {
            get { return occurance; }
            set { occurance = value; }
        }

        private int? simplicity = 0;

        public int? Simplicity
        {
            get { return simplicity; }
            set { simplicity = value; }
        }
    }
}
