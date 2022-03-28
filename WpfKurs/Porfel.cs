using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfKurs
{
    internal class Porfel
    {
        public Porfel(List<Akcia> Akcii, List<Kripta> Kripta, List<Kripta> KriptaRisk, List<Kripta> Zerorisk,string name ,
        int old ,
        int money ,
        int risk ,
        int doxod, 
        int srok)
        {
            List<Akcia> AkciaList = new List<Akcia>();
           foreach (Akcia a in Akcii)
            {
                double riskAkcii = a.RiskScore;
                double stoimosti = 0;
                double recomdation = a.financialData.recommendationMean.raw;

                if (riskAkcii < risk&money>stoimosti*2&recomdation<2.5)
                {
                    AkciaList.Add(a);
                }

            }

        }
    }
}
