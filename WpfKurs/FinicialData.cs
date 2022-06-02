using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WpfKurs
{
    

    
    internal class FinicialAkicas : IGetData
    {
        List<Akcia> Akcii = new List<Akcia>();
        private List<string> Data { get; set; }
        public double KursDollara { get; set; }
        public static string Range { get; set; }
        public static string Interval { get; set; }
        public FinicialAkicas(List<string> data, double kurs, string range ,string interval) 
        {
            Data = data;
            KursDollara = kurs;
            Interval = interval;
            Range = range;
            
           
        }
        public List<Akcia> GetList()
        {
            return Akcii;
        }
        public  int AddNewAddToList()
        {
            List<string> BlueAct = Data;
                int z = 0;

                while (true)
                {
                    if (z == 20) { break; }
                    string s = BlueAct[z];
                    Akcia temp = GetADataFromYahoo(s);
                    if (temp.RiskScore != 0 & temp.TargetPriceMedian != 0)
                    {
                        Akcii.Add(temp);
                    }

                    if (temp.recommendedSymbols != null & (BlueAct.Count < 20))
                    {
                        for (int i = 0; i < temp.recommendedSymbols.Length; i++)
                        {
                            if (!FindInString(BlueAct, temp.recommendedSymbols[i].symbol))
                            {
                                BlueAct.Add(temp.recommendedSymbols[i].symbol);
                            }

                        }
                    }
                    z++;
                }
                return 0;
            
        }

        public Akcia GetADataFromYahoo(string name)
        {
            string predict = String.Format("https://query1.finance.yahoo.com/v6/finance/recommendationsbysymbol/{0}", name);
            string recomendation = String.Format("https://query1.finance.yahoo.com/v11/finance/quoteSummary/{0}?lang=en&region=US&modules=financialData", name);
            string insaiders = String.Format("https://query1.finance.yahoo.com/v11/finance/quoteSummary/{0}?lang=en&region=US&modules=recommendationTrend", name);
            string risks = String.Format("https://query1.finance.yahoo.com/v11/finance/quoteSummary/{0}?lang=en&region=US&modules=esgScores", name);
            string charts = String.Format("https://query1.finance.yahoo.com/v8/finance/chart/{0}?range={1}&region=US&interval={2}&lang=en&events=div%2Csplit", name, Range, Interval);
            //todo sroki
            string[] sites = new string[5];
            sites[0] = predict;
            sites[1] = recomendation;
            sites[2] = insaiders;
            sites[3] = risks;
            sites[4] = charts;
            Akcia ak = new Akcia();
            for (int i = 0; i < sites.Length; i++)
            {
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(sites[i]);
                    httpWebRequest.ContentType = "text/json";
                    httpWebRequest.Method = "GET";//Можно GET
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        //ответ от сервера
                        var result = streamReader.ReadToEnd();

                        //Сериализация
                        Rootobject otv = JsonConvert.DeserializeObject<Rootobject>(result);
                        switch (i)
                        {
                            case 0:
                                {
                                    ak.recommendedSymbols = otv.finance.result[0].recommendedSymbols;
                                }
                                break;
                            case 1:
                                {

                                    ak.CurrentPrice = otv.quoteSummary.result[0].financialData.currentPrice.raw;
                                    ak.recomendationMean = otv.quoteSummary.result[0].financialData.recommendationMean.raw;
                                    ak.NormaPriboli = otv.quoteSummary.result[0].financialData.profitMargins.raw * 100;
                                    ak.TargetPriceHigh = otv.quoteSummary.result[0].financialData.targetHighPrice.raw;
                                    ak.TargetPriceLow = otv.quoteSummary.result[0].financialData.targetLowPrice.raw;
                                    ak.TargetPriceMean = otv.quoteSummary.result[0].financialData.targetMeanPrice.raw;
                                    ak.TargetPriceMedian = otv.quoteSummary.result[0].financialData.targetMedianPrice.raw;

                                }
                                break;
                            case 2:
                                {
                                    ak.recommendationTrend = otv.quoteSummary.result[0].recommendationTrend;
                                }
                                break;
                            case 3:
                                {

                                    ak.RiskScore = otv.quoteSummary.result[0].esgScores.totalEsg.raw / 2;

                                    ak.peergroups = otv.quoteSummary.result[0].esgScores.peerGroup;
                                }
                                break;
                            case 4:
                                {
                                    ak.indicators = otv.chart.result[0].indicators;
                                    ak.meta = otv.chart.result[0].meta;
                                    ak.timestamp = otv.chart.result[0].timestamp;
                                    if (otv.chart.result[0].indicators.quote[0].close != null)
                                    {
                                        double median = 0;
                                        for (int j = 0; j < otv.chart.result[0].indicators.quote[0].close.Length; j++)
                                        {
                                            median += otv.chart.result[0].indicators.quote[0].close[j];
                                        }
                                        ak.TargetPriceMean = median / otv.chart.result[0].indicators.quote[0].close.Length;
                                    }

                                }
                                break;

                        }

                    }


                }
                catch (Exception ex)
                {
                    continue;
                }

            }
            if (ak.meta != null)
            {
                if (ak.meta.currency == "USD")
                {
                    ak.CurrentPrice = ak.CurrentPrice * KursDollara;
                    ak.TargetPriceHigh = ak.TargetPriceHigh * KursDollara;
                    ak.TargetPriceLow = ak.TargetPriceLow * KursDollara;
                    ak.TargetPriceMean = ak.TargetPriceMean * KursDollara;
                    ak.TargetPriceMedian = ak.TargetPriceMedian * KursDollara;

                }
            }
            ak.Raznica = ak.TargetPriceMean - ak.CurrentPrice;
            ak.symbol = name;
            return (ak);
        }
        public bool FindInString(List<string> list, string s)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == s)
                {
                    return true;
                }
            }
            return false;
        }

        public Kripta GetBDataFromYahoo(string name)
        {
            throw new NotImplementedException();
        }
    }
    internal class FinicialZeroRisk:IGetData
    {
        List<Kripta> Zerorisk = new List<Kripta>();
        private List<string> Data { get; set; }
        public double KursDollara { get; set; }
        public static string Range { get; set; }
        public static string Interval { get; set; }
        public FinicialZeroRisk(List<string> data, double kurs, string range, string interval)
        {
            Data = data;
            KursDollara = kurs;
            Interval = interval;
            Range = range;
            

        }

        public List<Kripta> GetList()
        {
            return Zerorisk;
        }
        public int AddNewAddToList()
        {
            List<string> ZeroRisk = Data;
            int z = 0;
            while (true)
            {
                if (z == 20) { break; }
                string s = ZeroRisk[z];
                Kripta temp = GetBDataFromYahoo(s);
                if (temp.indicators != null) Zerorisk.Add(temp);
                if (temp.recommendedSymbols != null & (ZeroRisk.Count < 20))
                {
                    for (int i = 0; i < temp.recommendedSymbols.Length; i++)
                    {
                        if (!FindInString(ZeroRisk, temp.recommendedSymbols[i].symbol))
                        {
                            ZeroRisk.Add(temp.recommendedSymbols[i].symbol);
                        }

                    }
                }
                z++;
            }
            return 0;
        }

        public Kripta GetBDataFromYahoo(string name)
        {
            string predict = String.Format("https://query1.finance.yahoo.com/v6/finance/recommendationsbysymbol/{0}", name);
            string recomendation = String.Format("https://query1.finance.yahoo.com/v11/finance/quoteSummary/{0}?lang=en&region=US&modules=summaryDetail", name);
            string charts = String.Format("https://query1.finance.yahoo.com/v8/finance/chart/{0}?comparisons=MSFT%2C%5EVIX&range=6mo&region=US&interval=1d&lang=en&events=div%2Csplit", name);
            string[] sites = new string[4];
            sites[0] = predict;
            sites[1] = recomendation;
            sites[2] = charts;
            Kripta kr = new Kripta();
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(sites[i]);
                    httpWebRequest.ContentType = "text/json";
                    httpWebRequest.Method = "GET";//Можно GET
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        //ответ от сервера
                        var result = streamReader.ReadToEnd();

                        //Сериализация
                        Rootobject otv = JsonConvert.DeserializeObject<Rootobject>(result);
                        switch (i)
                        {
                            case 0:
                                {
                                    kr.recommendedSymbols = otv.finance.result[0].recommendedSymbols;
                                }
                                break;
                            case 1:
                                {
                                    kr.summaryDetail = otv.quoteSummary.result[0].summaryDetail;
                                    kr.CurrentPrice = otv.quoteSummary.result[0].summaryDetail.open.raw;
                                }
                                break;
                            case 2:
                                {
                                    if (otv.chart.result == null) break;
                                    if (otv.chart.result[0].indicators == null) break;
                                    kr.indicators = otv.chart.result[0].indicators;
                                    kr.meta = otv.chart.result[0].meta;
                                    kr.timestamp = otv.chart.result[0].timestamp;
                                    kr.CurrentPrice = otv.chart.result[0].meta.chartPreviousClose;
                                    double median = 0;
                                    for (int j = 0; j < otv.chart.result[0].indicators.quote[0].close.Length; j++)
                                    {
                                        median += otv.chart.result[0].indicators.quote[0].close[j];
                                    }
                                    kr.TargetMedian = median / otv.chart.result[0].indicators.quote[0].close.Length;
                                }
                                break;

                        }
                    }

                }
                catch (Exception ex)
                {
                    continue;
                }

            }
            if (kr.summaryDetail != null)
            {
                if (kr.summaryDetail.currency == "USD")
                {

                    kr.CurrentPrice = kr.CurrentPrice * KursDollara;
                    kr.TargetMedian = kr.TargetMedian * KursDollara;
                }
            }
            kr.Raznicaa = kr.TargetMedian - kr.CurrentPrice;
            kr.symbol = name;
            return (kr);
        }
        public bool FindInString(List<string> list, string s)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == s)
                {
                    return true;
                }
            }
            return false;
        }

        public Akcia GetADataFromYahoo(string name)
        {
            throw new NotImplementedException();
        }
    }

}
