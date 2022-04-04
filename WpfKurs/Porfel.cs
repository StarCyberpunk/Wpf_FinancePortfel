using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfKurs
{
    public enum Type
    {
        agresive1 = 0, agresive2 = 1, agresive3 = 3,
        balance1 = 4, balance2 = 5, balance3 = 6,
        konserv1 = 7, konserv2 = 8, konserv3 = 9
    }
    internal class Porfel
    {
        public static List<Akcia> AkciaList;
        public static List<Kripta> RiskZero;
        public static List<Akcia> ResultAkcii;
        public static List<Kripta> ResultRisk;
        public static double ResRisk;
        public static Type tipPortfel;

        public Porfel(List<Akcia> Akcii,  List<Kripta> Zerorisk,string name ,
        
        int money ,
        Type tipPot,
        int srok)
        {
            tipPortfel = tipPot;
            AkciaList = new List<Akcia>();
            RiskZero = new List<Kripta>();
            ResultAkcii=new List<Akcia>();
            ResultRisk = new List<Kripta>();
            ResRisk = 0;
            double procAkc = 0;
            double procZero = 0;

            
            switch (tipPortfel)
            {
                case Type.agresive1:
                    {
                        procAkc = 100;
                    }
                    break;
                case Type.agresive2:
                    {
                        procAkc = 85;
                        procZero = 15;
                    }
                    break;
                case Type.agresive3:
                    {
                        procAkc = 70;
                        procZero = 30;
                    }
                    break;
                case Type.balance1:
                    {
                        procAkc = 60;
                        procZero = 30;
                    }
                    break;
                case Type.balance2:
                    {
                        procAkc = 50;
                        procZero = 50;
                    }
                    break;
                case Type.balance3:
                    {
                        procAkc = 40;
                        procZero = 60;
                    }
                    break;
                case Type.konserv1:
                    {
                        procAkc = 30;
                        procZero = 70;
                    }
                    break;
                case Type.konserv2:
                    {
                        procAkc = 20;
                        procZero = 80;
                    }
                    break;
                case Type.konserv3:
                    {
                        procZero = 100;
                    }
                    break;
            }
            
            procAkc/=100;
            procZero/=100;

            foreach(Akcia a in Akcii)
            {
                if (a.Raznica > 0) { 
                if (a.CurrentPrice * 3 < money* procAkc)
                { if(!HaveType(a,AkciaList))
                    AkciaList.Add(a);
                        else
                        {
                            FindBetter(a, AkciaList);
                        }
                       
                }
                }
            }
            foreach (Kripta k in Zerorisk)
            {
                if (k.Raznicaa > 0)
                {
                    if (k.CurrentPrice * 3 < money * procZero)
                    {
                        RiskZero.Add(k);
                    }
                }
            }
            double mone = money * procAkc;
            int i = 0;
            SortForBetter(AkciaList);
            while (mone > 0)
            {
                ResultAkcii.Add(AkciaList[i]);
                i++;
                if (i >= AkciaList.Count ) i = 0;
                mone -= AkciaList[i].CurrentPrice;
            }
           

        }
        private void SortForBetter(List<Akcia> list)
        {

            for (int i = 0; i < list.Count - 1; ++i)
            { 
                for (int j = i + 1; j < list.Count; ++j)
                { 
                    if (list[i].Raznica / list[i].CurrentPrice < list[j].Raznica / list[j].CurrentPrice)
                    { 
                       Akcia tmp = list[i];
                        list[i] = list[j];
                        list[j] = tmp;
                    }
                }
            }
        }
        private void FindBetter(Akcia a,List<Akcia> list)
        {
            Akcia b=null;
            foreach (Akcia ak in list)
            {
                if (ak.peergroups == a.peergroups)
                {

                    b = ak;
                }
            }
            if (a.Raznica / a.CurrentPrice > b.Raznica / a.CurrentPrice)
            {
                list.Add(a);
                list.Remove(b);
            }
            
        }
        private bool HaveType(Akcia a,List<Akcia> list)
        {
            foreach(Akcia ak in list)
            {
                if (ak.peergroups == a.peergroups)
                {
                    return true;
                    
                }
            }
            return false;
        }
        private int Kol_voAkcia(Akcia a)
        {
            int k = 0;
            int i = 0;
            while (i <ResultAkcii.Count-1) { 
                if(ResultAkcii[i].symbol == a.symbol)
                {
                    k++;
                    ResultAkcii.RemoveAt(i);
                    
                }
                i++;
                
            }

            return k;
        }

        public  string GetPortfel()
        {
            string portfel2 = "";
            int i = 0;
            while(i < ResultAkcii.Count-1) {
            
                Akcia a=ResultAkcii[i];
                ResRisk += a.RiskScore;
                 portfel2 += String.Format(a.symbol+" "+Kol_voAkcia(a)+"шт "+"По цене {0} "+"Прибыль(%){1}"+"\n",a.CurrentPrice,Math.Round( a.Raznica/a.CurrentPrice*100,2));
                i++;
            }
            ResRisk /= ResultAkcii.Count;
            portfel2 += String.Format("\n{0}",ResRisk);
            return portfel2;
        }
    }
}
