using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfKurs
{
    internal class Porfel
    {
        public static List<Akcia> AkciaList;
        public Porfel(List<Akcia> Akcii, List<Kripta> Kripta, List<Kripta> KriptaRisk, List<Kripta> Zerorisk,string name ,
        int old ,
        int money ,
        int risk ,
        int doxod, 
        int srok,double usd)
        {
            AkciaList = new List<Akcia>();

            foreach (Akcia a in Akcii)
            {
                
                double riskAkcii = a.RiskScore;
                double stoimosti = 0;
                if(a.meta != null) { 
                if (a.meta.currency == "USD")
                {
                    stoimosti = a.CurrentPrice*usd;
                    a.CurrentPrice *= usd;
                }
                }
                else
                {
                     stoimosti = a.CurrentPrice;
                }
                
                double recomdation = a.recomendationMean;

                if (riskAkcii < risk&money>stoimosti*2&recomdation<2.5)
                {
                    AkciaList.Add(a);
                }

            }
           


        }
        public  string GetPortfel()
        {
            string portfel2 = "";
            foreach (Akcia a in AkciaList) { 
                 portfel2 += String.Format(a.symbol+"\n");
            }
            return portfel2;
        }
    }
}
