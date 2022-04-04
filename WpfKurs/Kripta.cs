using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfKurs
{

    

    public class Kripta
    {
        public Summarydetail summaryDetail { get; set; }
        public Recommendedsymbol[] recommendedSymbols { get; set; }
        public string symbol { get; set; }
        public int[] timestamp { get; set; }
        public Meta meta { get; set; }
        public Indicators indicators { get; set; }
        public double CurrentPrice { get; set; }
    }

    

}
