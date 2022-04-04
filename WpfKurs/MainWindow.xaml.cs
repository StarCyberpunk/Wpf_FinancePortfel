using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace WpfKurs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
     
public partial class MainWindow : Window
    {
        int SrokInvest = 0;
        public static double KursDollara = GetDataUSD();
        List<Akcia> Akcii = new List<Akcia>();
        List<Kripta> Kripta = new List<Kripta>();
        List<Kripta> KriptaRisk = new List<Kripta>();
        List<Kripta> Zerorisk = new List<Kripta>();
        public static Type tipPortfel;
        public static string cel { get; set; }
        public static string social { get; set; }
        public static string maried { get; set; }
        //Курс доллара
        public MainWindow()
        {
            InitializeComponent();
            CeLInvest.Items.Add("Выберете...");
            CeLInvest.Items.Add("Сбережение");
            CeLInvest.Items.Add("Максимальный процент дохода");
            CeLInvest.Items.Add("Стабильный рост");
            SocialStatus.Items.Add("Выберете...");
            SocialStatus.Items.Add("Высокий достаток");
            SocialStatus.Items.Add("Средний достаток");
            SocialStatus.Items.Add("Низкий достаток");
            Maried.Items.Add("Выберете...");
            Maried.Items.Add("Замужем/Женат");
            Maried.Items.Add("Имеется отношения");
            Maried.Items.Add("Одинок");
        }

        private void GoProgress_Click(object sender, RoutedEventArgs e)
        {
            Output.Text = "";
            string name = FIO.Text;
            int money = 0;
            int old = 0;
            int zadol = 0;
            int cell = 0;
            int socia = 0;
            int marie = 0;
            bool correct = true;
            
            int doxod = Int32.Parse(Doxod.Text);
            int srok = Int32.Parse(Srok.Text);
            
            try
            {
                old = Int32.Parse(Old.Text);
                money = Int32.Parse(Sum.Text);
                zadol = Int32.Parse(Zadol.Text);
            }
            catch
            {
                correct = false;
            }

            if (old < 18 || old > 99)
            {
                correct = false;
                MessageBox.Show("Введите корректный возраст");
            }

            if(money < 0||money>Int32.MaxValue)
            {
                correct=false;
                MessageBox.Show("Введите корректную сумму");
            }
            if(doxod == 0)
            {
                correct = false;
                MessageBox.Show("Введите корректную процент доходности");
            }
            if (srok == 0)
            {
                correct = false;
                MessageBox.Show("Введите корректную срочность");
            }



            if (cel == "Выберете..." || cel == null)
            {
                correct = false;
            }
            

            if (social == "Выберете..." || social == null)
            {
                correct = false;
            }
            

            if (maried == "Выберете..." || maried == null)
            {
                correct = false;
            }

            if (cel == "Сбережение") { cell = 1; }
            else if (cel == "Максимальный процент дохода") { cell = 3; }
            else if (cel == "Стабильный рост") { cell = 2; }


            if (social == "Высокий достаток") { socia = 3; }
            else if (social == "Средний достаток") { socia = 2; }
            else if (social == "Низкий достаток") { socia = 1; }


            if (maried == "Замужем/Женат") { marie = 3; }
            else if (maried == "Имеется отношения") { marie = 2; }
            else if (maried == "Одинок") { marie = 1; }

            if (old < 50) { 
            if (cell == 1)
            {
                if (socia == 1)
                {
                    if (marie == 1) { tipPortfel = Type.konserv2; }
                    else if (marie == 2) { tipPortfel = Type.konserv2; }
                    else if (marie == 3) { tipPortfel = Type.konserv3; }
                }
                else if (socia == 2)
                {
                    if (marie == 1) { tipPortfel = Type.konserv1; }
                    else if (marie == 2) { tipPortfel = Type.konserv1; }
                    else if (marie == 3) { tipPortfel = Type.konserv2; }
                }
                else if (socia == 3)
                {
                    if (marie == 1) { tipPortfel = Type.konserv1; }
                    else if (marie == 2) { tipPortfel = Type.konserv1; }
                    else if (marie == 3) { tipPortfel = Type.konserv1; }
                }
            }
            else if (cell == 2)
            {
                if (socia == 1)
                {
                    if (marie == 1) { tipPortfel = Type.balance2; }
                    else if (marie == 2) { tipPortfel = Type.balance2; }
                    else if (marie == 3) { tipPortfel = Type.balance3; }
                }
                else if (socia == 2)
                {
                    if (marie == 1) { tipPortfel = Type.balance1; }
                    else if (marie == 2) { tipPortfel = Type.balance1; }
                    else if (marie == 3) { tipPortfel = Type.balance2; }
                }
                else if (socia == 3)
                {
                    if (marie == 1) { tipPortfel = Type.balance1; }
                    else if (marie == 2) { tipPortfel = Type.balance1; }
                    else if (marie == 3) { tipPortfel = Type.balance1; }
                }
            }
            else if (cell == 3)
            {
                if (socia == 1)
                {
                    if (marie == 1) { tipPortfel = Type.agresive2; }
                    else if (marie == 2) { tipPortfel = Type.agresive2; }
                    else if (marie == 3) { tipPortfel = Type.agresive3; }
                }
                else if (socia == 2)
                {
                    if (marie == 1) { tipPortfel = Type.agresive1; }
                    else if (marie == 2) { tipPortfel = Type.agresive1; }
                    else if (marie == 3) { tipPortfel = Type.agresive2; }
                }
                else if (socia == 3)
                {
                    if (marie == 1) { tipPortfel = Type.agresive1; }
                    else if (marie == 2) { tipPortfel = Type.agresive1; }
                    else if (marie == 3) { tipPortfel = Type.agresive1; }
                }
            }
            }
            else
            {
                tipPortfel = Type.konserv3;
            }


            if ( correct)
            {
                Porfel na = new Porfel(Akcii, Zerorisk, name, money,tipPortfel ,srok);
                Output.Text = na.GetPortfel();
            }
            else
            {
                MessageBox.Show("Проверьте корректность данных");
            }
            
        }
        //TODO Проверка на нулевые и на неправильные данные 

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            
            List<string> BlueAct = new List<string>();
            BlueAct.Add("AAPL");
            BlueAct.Add("JNJ");
            BlueAct.Add("INTC");
            BlueAct.Add("MRK");
            BlueAct.Add("NVDA");
            List<string> RiskAct = new List<string>();
            RiskAct.Add("SBER.ME");
            RiskAct.Add("ROSN.ME");
            RiskAct.Add("BPE5.DU");
            RiskAct.Add("YCP.DE");
            List<string> Kripto = new List<string>();
            Kripto.Add("BTC-USD");
            Kripto.Add("ETH-USD");
            Kripto.Add("SOL-USD");
            List<string> KriptoHighRisk=    new List<string>();
            KriptoHighRisk.Add("DOGE-USD");
            KriptoHighRisk.Add("CVX-USD");
            KriptoHighRisk.Add("LUNA1-USD");
            List<string> ZeroRisk=new List<string>();
            ZeroRisk.Add("GC=F");
            ZeroRisk.Add("SI=F");
            ZeroRisk.Add("^N225");
            ZeroRisk.Add("^RUT");
            ZeroRisk.Add("^GSPC");
            ZeroRisk.Add("^DJI");
            int z = 0;
            
            while (true) {
                if (z==20) { break; }
                string s = BlueAct[z];
                Akcia temp = GetDataFromYahooFinAKCIA(s);
                if (temp.RiskScore != 0&temp.TargetPriceMedian!=0) {
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
            z = 0;
            while (true)
            {
                if (z == 20) { break; }
                string s = RiskAct[z];
                Akcia temp = GetDataFromYahooFinAKCIA(s);
                if (temp.RiskScore != 0 & temp.TargetPriceMedian != 0)
                {
                    Akcii.Add(temp);
                }
                if (temp.recommendedSymbols != null & (RiskAct.Count < 20))
                {
                    for (int i = 0; i < temp.recommendedSymbols.Length; i++)
                    {
                        if (!FindInString(RiskAct, temp.recommendedSymbols[i].symbol))
                        {
                            RiskAct.Add(temp.recommendedSymbols[i].symbol);
                        }

                    }
                }
                z++;
            }

            z = 0;
           /* while (true)
            {
                if (z == 20) { break; }
                string s = Kripto[z];
                Kripta temp = GetDataFromYahooFinZero(s);
                Kripta.Add(temp);
                if (temp.recommendedSymbols != null & (Kripto.Count < 20))
                {
                    for (int i = 0; i < temp.recommendedSymbols.Length; i++)
                    {
                        if (!FindInString(Kripto, temp.recommendedSymbols[i].symbol))
                        {
                            Kripto.Add(temp.recommendedSymbols[i].symbol);
                        }

                    }
                }
                z++;
            }
            z= 0;
            while (true)
            {
                if (z == 20) { break; }
                string s = KriptoHighRisk[z];
                Kripta temp = GetDataFromYahooFinZero(s);
                KriptaRisk.Add(temp);
                if (temp.recommendedSymbols != null & (KriptoHighRisk.Count < 20))
                {
                    for (int i = 0; i < temp.recommendedSymbols.Length; i++)
                    {
                        if (!FindInString(KriptoHighRisk, temp.recommendedSymbols[i].symbol))
                        {
                            KriptoHighRisk.Add(temp.recommendedSymbols[i].symbol);
                        }

                    }
                }
                z++;
            }*/
            z = 0;
            while (true)
            {
                if (z == 20) { break; }
                string s = ZeroRisk[z];
                Kripta temp = GetDataFromYahooFinZero(s);
                if(temp.indicators!=null) Zerorisk.Add(temp);
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
            //TODOУбрать повторение
            //сроки разные 


            

            Update.Content = "Обновлено";
            Update.IsEnabled = false;


        }
       private static Akcia GetDataFromYahooFinAKCIA(string name)
        {
            string predict = String.Format("https://query1.finance.yahoo.com/v6/finance/recommendationsbysymbol/{0}", name);
            string recomendation = String.Format("https://query1.finance.yahoo.com/v11/finance/quoteSummary/{0}?lang=en&region=US&modules=financialData", name);
            string insaiders = String.Format("https://query1.finance.yahoo.com/v11/finance/quoteSummary/{0}?lang=en&region=US&modules=recommendationTrend", name);
            string risks = String.Format("https://query1.finance.yahoo.com/v11/finance/quoteSummary/{0}?lang=en&region=US&modules=esgScores", name);
            string charts = String.Format("https://query1.finance.yahoo.com/v8/finance/chart/{0}?range=6mo&region=US&interval=1d&lang=en&events=div%2Csplit",name);
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
                                    ak.NormaPriboli = otv.quoteSummary.result[0].financialData.profitMargins.raw*100;
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
                                    
                                        ak.RiskScore = otv.quoteSummary.result[0].esgScores.totalEsg.raw/2;
                                    
                                    ak.peergroups = otv.quoteSummary.result[0].esgScores.peerGroup;
                                }
                                break;
                            case 4:
                                {
                                    ak.indicators = otv.chart.result[0].indicators;
                                    ak.meta = otv.chart.result[0].meta;
                                    ak.timestamp = otv.chart.result[0].timestamp;
                                    
                                } break;

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
            ak.Raznica = ak.TargetPriceMean-ak.CurrentPrice;
            ak.symbol = name;
            return(ak);
        }
        private static double GetDataUSD()
        {
            string name = "RUB=X";
            string recomendation = String.Format("https://query1.finance.yahoo.com/v11/finance/quoteSummary/{0}?lang=en&region=US&modules=summaryDetail", name);

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(recomendation);
                httpWebRequest.ContentType = "text/json";
                httpWebRequest.Method = "GET";//Можно GET
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    //ответ от сервера
                    var result = streamReader.ReadToEnd();

                    //Сериализация
                    Rootobject otv = JsonConvert.DeserializeObject<Rootobject>(result);


                    return(otv.quoteSummary.result[0].summaryDetail.previousClose.raw);
                   



                    
                }
            }catch (Exception ex) { throw new Exception(); }
            }
        private static Kripta GetDataFromYahooFinZero(string name)
        {
            string predict = String.Format("https://query1.finance.yahoo.com/v6/finance/recommendationsbysymbol/{0}", name);
            string recomendation = String.Format("https://query1.finance.yahoo.com/v11/finance/quoteSummary/{0}?lang=en&region=US&modules=summaryDetail", name);
            string charts = String.Format("https://query1.finance.yahoo.com/v8/finance/chart/{0}?comparisons=MSFT%2C%5EVIX&range=1mo&region=US&interval=1d&lang=en&events=div%2Csplit",name);
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
                                    kr.CurrentPrice =  otv.quoteSummary.result[0].summaryDetail.open.raw;
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
                                    for(int j = 0; j < otv.chart.result[0].indicators.quote[0].close.Length; j++)
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
                    kr.TargetMedian= kr.TargetMedian * KursDollara;
                }
            }
            kr.Raznicaa =kr.TargetMedian- kr.CurrentPrice;
            kr.symbol = name;
            return (kr);
        }
        private static bool FindInString(List<string> list,string s)
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

        private void ComboBox_SelectionChangedCel(object sender, SelectionChangedEventArgs e)
        {
            cel = e.AddedItems[0].ToString();
            
        }
        private void ComboBox_SelectionChangedSocial(object sender, SelectionChangedEventArgs e)
        {
            social = e.AddedItems[0].ToString();
        }
        private void ComboBox_SelectionChangedMaried(object sender, SelectionChangedEventArgs e)
        {

            maried = e.AddedItems[0].ToString();
        }

        
    }
}
