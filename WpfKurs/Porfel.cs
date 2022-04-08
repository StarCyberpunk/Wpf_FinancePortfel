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
        public static string Name;

        //TODO сроки и доходность

        public Porfel(List<Akcia> Akcii,  List<Kripta> Zerorisk,string name ,
        int zadol,
        int money ,
        Type tipPot,
        int srok,int doxod)
        {
            tipPortfel = tipPot;
            AkciaList = new List<Akcia>();
            RiskZero = new List<Kripta>();
            ResultAkcii = new List<Akcia>();
            ResultRisk = new List<Kripta>();
            ResRisk = 0;
            Name = name;
            double procAkc = 0;
            double procZero = 0;
            

            switch (tipPortfel)
            {
                case Type.agresive1:
                    {
                        procAkc = 1;
                        Filter(Akcii, Zerorisk, money, procAkc, procZero);
                        SortForBetterMaxPribal(AkciaList);

                    }
                    break;
                case Type.agresive2:
                    {
                        procAkc = 0.85;
                        procZero = 0.15;
                        Filter(Akcii, Zerorisk, money, procAkc, procZero);
                        SortForBetterMaxPribal(AkciaList);
                        SortForBetterMaxPribal(RiskZero);
                    }
                    break;
                case Type.agresive3:
                    {
                        procAkc = 0.70;
                        procZero = 0.30;
                        Filter(Akcii, Zerorisk, money, procAkc, procZero);
                        SortForBetterMaxPribal(AkciaList);
                        SortForBetterMaxPribal(RiskZero);
                    }
                    break;
                case Type.balance1:
                    {
                        procAkc = 0.60;
                        procZero = 0.30;
                        Filter(Akcii, Zerorisk, money, procAkc, procZero);
                        SortForBetterMinRisk(AkciaList);
                        SortForBetterMaxPribal(RiskZero);
                    }
                    break;
                case Type.balance2:
                    {
                        procAkc = 0.50;
                        procZero = 0.50;
                        Filter(Akcii, Zerorisk, money, procAkc, procZero);
                        SortForBetterMinRisk(AkciaList);
                        SortForBetterMaxPribal(RiskZero);
                    }
                    break;
                case Type.balance3:
                    {
                        procAkc = 0.40;
                        procZero = 0.60;
                        Filter(Akcii, Zerorisk, money, procAkc, procZero);
                        SortForBetterMinRisk(AkciaList);
                        SortForBetterMaxPribal(RiskZero);
                    }
                    break;
                case Type.konserv1:
                    {
                        procAkc = 0.30;
                        procZero = 0.70;
                        Filter(Akcii, Zerorisk, money, procAkc, procZero);
                        SortForBetterMinRisk(AkciaList);
                        SortForBetterMaxPribal(RiskZero);
                    }
                    break;
                case Type.konserv2:
                    {
                        procAkc = 0.20;
                        procZero = 0.80;
                        Filter(Akcii, Zerorisk, money, procAkc, procZero);
                        SortForBetterMinRisk(AkciaList);
                        SortForBetterMaxPribal(RiskZero);
                    }
                    break;
                case Type.konserv3:
                    {
                        procZero = 1;
                        Filter(Akcii, Zerorisk, money, procAkc, procZero);
                        SortForBetterMaxPribal(RiskZero);
                    }
                    break;
            }

            


            double moneyAkcii = money * procAkc;
            int i = 0;
            //доходность либо по raznica либо по норме прибыли
            double obdoxod = 0;
            double countdoxod = 0;
            while (moneyAkcii > 0)
            {
                if (AkciaList.Count == 0) break;
                if (AkciaList[i].TargetPriceMean*100/ AkciaList[i].CurrentPrice-100 > doxod) {
                    if (moneyAkcii - AkciaList[i].CurrentPrice < 0) break;
                ResultAkcii.Add(AkciaList[i]);
                    moneyAkcii -= AkciaList[i].CurrentPrice;
                    obdoxod += AkciaList[i].TargetPriceMean * 100 / AkciaList[i].CurrentPrice - 100;
                    countdoxod += 1;
                }
                i++;
                if (i >= AkciaList.Count) i = 0;
                
            }
             
            i = 0;
            double moneyZero = money * procZero;
            while (moneyZero > 0)
            {
                if (RiskZero.Count == 0) break;
                if(moneyZero - RiskZero[i].CurrentPrice<0) break;
                ResultRisk.Add(RiskZero[i]);
                moneyZero -= RiskZero[i].CurrentPrice;
                    
                
                i++;
                if (i >= RiskZero.Count) i = 0;
                
            }
            obdoxod /= countdoxod;

        }

        private void Filter(List<Akcia> Akcii, List<Kripta> Zerorisk, int money, double procAkc, double procZero)
        {
            foreach (Akcia a in Akcii)
            {
                if (a.Raznica > 0)
                {
                    if (a.CurrentPrice * 3 < money * procAkc)
                    {
                        if (!HaveType(a, AkciaList))
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
                    if (k.CurrentPrice * 4 < money * procZero)
                    {
                        RiskZero.Add(k);
                    }
                }
            }
        }

        private void SortForBetterMaxMinrisk(List<Akcia> list)
        {

            for (int i = 0; i < list.Count - 1; ++i)
            {
                for (int j = i + 1; j < list.Count; ++j)
                {
                    if (list[i].Raznica / list[i].CurrentPrice < list[j].Raznica / list[j].CurrentPrice&&list[i].RiskScore>list[j].RiskScore)
                    {
                        Akcia tmp = list[i];
                        list[i] = list[j];
                        list[j] = tmp;
                    }
                }
            }
        }
        private void SortForBetterMaxPribal(List<Akcia> list)
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
        private void SortForBetterMaxPribal(List<Kripta> list)
        {

            for (int i = 0; i < list.Count - 1; ++i)
            {
                for (int j = i + 1; j < list.Count; ++j)
                {
                    if (list[i].Raznicaa / list[i].CurrentPrice > list[j].Raznicaa / list[j].CurrentPrice)
                    {
                        Kripta tmp = list[i];
                        list[i] = list[j];
                        list[j] = tmp;
                    }
                }
            }
        }
        private void SortForBetterMinRisk(List<Akcia> list)
        {

            for (int i = 0; i < list.Count - 1; ++i)
            {
                for (int j = i + 1; j < list.Count; ++j)
                {
                    if (list[i].RiskScore > list[j].RiskScore)
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
            foreach (Akcia ak in ResultAkcii)
            {
                if (ak.symbol == a.symbol)
                {
                    k++;
                }
             }
            while (i <ResultAkcii.Count-1) { 
                if(ResultAkcii[i].symbol == a.symbol)
                {
                    
                    ResultAkcii.RemoveAt(i);
                    i = -1;
                }
                i++;
                
            }

            return k;
        }
        private int Kol_voZero(Kripta a)
        {
            int k = 0;
            int i = 0;
            foreach(Kripta z in ResultRisk)
            {
                if (z.symbol == a.symbol)
                {
                    k++;
                }
            }
            while (i < ResultRisk.Count - 1)
            {
                if (ResultRisk[i].symbol == a.symbol)
                {
                    
                    ResultRisk.RemoveAt(i);
                    i = -1;
                }
                i++;


            }

            return k;
        }

        public  string GetPortfel()
        {
            string portfel2 = "";
            int i = 0;
            if (ResultAkcii.Count != 0) { 
            portfel2 += String.Format("Имя:{0}\n Советуем купить акции данных компаний:\n",Name);
            while(i < ResultAkcii.Count-1) {
            
                Akcia a=ResultAkcii[i];
                ResRisk += a.RiskScore;
                 portfel2 += String.Format(a.symbol+" "+Kol_voAkcia(a)+"шт "+"По цене {0} "+"Максимальная прибыль(%){1}"+"\n"+"Минимальная прибыль:{2}\n", Math.Round(a.CurrentPrice,2),Math.Round( a.TargetPriceMean * 100 / a.CurrentPrice - 100, 2),Math.Round(a.NormaPriboli,2));
                i++;
            }
            portfel2 += String.Format("\n Примерный риск:{0}",Math.Round( ResRisk/i,2));
            portfel2 += "\n\n";
            }
            else { portfel2 += String.Format("Нет акций с данной доходностью"); }
            if (ResultRisk.Count!=0) { 
                portfel2 += String.Format(" Советуем купить эти фонды/металлы :\n");
            i = 0;
            while (i < ResultRisk.Count - 1)
            {

                Kripta a = ResultRisk[i];
                ResRisk = 5;
                portfel2 += String.Format(a.symbol + " " + Kol_voZero(a) + "шт " + "По цене {0} " + " Примерная прибыль(%):{1}" + "\n", Math.Round(a.CurrentPrice,2), Math.Round(a.TargetMedian * 100 / a.CurrentPrice - 100, 2));
                i++;
            }

            ResRisk /= ResultRisk.Count;
            
            portfel2 += String.Format("\n Примерный риск:{0}",ResRisk/i);
            }
            
            return portfel2;
        }
    }
}
