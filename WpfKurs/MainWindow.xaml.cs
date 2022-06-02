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
        List<Kripta> Zerorisk = new List<Kripta>();
        public bool data_have=false;
        public int Srokk=1;
        public static Type tipPortfel;
        public static string cel { get; set; }
        public static string social { get; set; }
        public static string maried { get; set; }
        public static string range { get; set; }
        public static string interval { get; set; }
      

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
            int summator = 0;
            
            int doxod = Int32.Parse(Doxod.Text);
            int srok = Int32.Parse(Srok.Text);
            #region Check
            

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
            #endregion
            #region VVOD

            if (old >= 50)
            {
                summator += 1;
            }
            else if(old > 18 || old < 50)
            {
                summator += 5;
            }

            if (cel == "Сбережение") { summator -= 2; }
            else if (cel == "Максимальный процент дохода") { summator += 3; }
            else if (cel == "Стабильный рост") { summator += 1; }


            if (social == "Высокий достаток") { summator += 5; }
            else if (social == "Средний достаток") { summator += 2; }
            else if (social == "Низкий достаток") { summator += 1; }


            if (maried == "Замужем/Женат") { summator += 1; }
            else if (maried == "Имеется отношения") { summator += 3; }
            else if (maried == "Одинок") { summator += 5; }
            if (zadol != 0)
            {
                summator += 1;
            }
            else
            {
                summator += 5;
            }

            
            if (srok >= 1 && srok <= 3)
            {
                summator += 5;
            }
            else if (srok > 3 && srok <= 12)
            {
                summator += 3;
            }
            
            else if (srok > 12 && srok < 24)
            {
                summator += 1;
            }
            #endregion
            if (doxod / (summator * 2.5) <= 1.1)
            {
                if (MessageBox.Show("Проверьте риски", String.Format("Согласны ли вы с риском в {0} ?",summator*2.5), MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    MessageBox.Show("Поменяйте доходность или срок");
                    correct = false;
                }
                else
                {
                    if (summator >= 3 && summator <= 5) { tipPortfel = Type.konserv3; }
                    else if (summator >= 6 && summator <= 8) { tipPortfel = Type.konserv2; }
                    else if (summator >= 9 && summator <= 11) { tipPortfel = Type.konserv1; }
                    else if (summator >= 12 && summator <= 14) { tipPortfel = Type.balance3; }
                    else if (summator >= 15 && summator <= 17) { tipPortfel = Type.balance2; }
                    else if (summator >= 18 && summator <= 20) { tipPortfel = Type.balance1; }
                    else if (summator >= 21 && summator <= 23) { tipPortfel = Type.agresive3; }
                    else if (summator >= 24 && summator <= 26) { tipPortfel = Type.agresive2; }
                    else if (summator >= 26 && summator <= 28) { tipPortfel = Type.agresive1; }
                    MessageBox.Show("Тип портфеля " + tipPortfel.ToString());
                }
            }
            else
            {
                MessageBox.Show("Поменяйте доходность или срок");
                correct = false;
            }
            

           

            if ( correct)
            {
                Porfel na = new Porfel(Akcii, Zerorisk, name, zadol, money,tipPortfel ,srok,doxod);
                Output.Text = na.GetPortfel();
            }
            else
            {
                MessageBox.Show("Проверьте корректность данных");
            }
                
        }
       

        private void Update_Click(object sender, RoutedEventArgs e)
        {

            int srok = Int32.Parse(Srok.Text);
            Srokk = srok;

            if (srok == 0)
            {
                MessageBox.Show("Введите срочность");
            }
            else
            {
                if (srok == 1)
                {
                    range = "1mo";
                    interval = "1d";
                }
               else if (srok > 1 && srok <= 3)
                {
                    range = "3mo";
                    interval = "1d";
                }
                else if (srok > 3 && srok <= 6)
                {
                    range = "6mo";
                    interval = "1d";
                }
                else if (srok > 6 && srok <= 12)
                {
                    range = "1y";
                    interval = "1wk";
                }
                else if (srok > 12 && srok < 24)
                {
                    range = "5y";
                    interval = "1mo";
                }
                MessageBox.Show("Скачивание данных может занять до 5-х минут");
                List<string> BlueAct = new List<string>() { "AAPL","JNJ","INTC","MRK","NVDA", "SBER.ME", "ROSN.ME", "BPE5.DU", "YCP.DE" };
                List<string> Kripto = new List<string>() { "BTC-USD", "ETH-USD" , "SOL-USD", "DOGE-USD", "CVX-USD", "LUNA1-USD" };
                List<string> ZeroRisk = new List<string>() { "GC=F", "SI=F" , "^N225" , "^RUT" , "^GSPC" , "^DJI" };

                FinicialAkicas dir =new FinicialAkicas(BlueAct,KursDollara,range,interval);
                FinicialZeroRisk dir2 = new FinicialZeroRisk(ZeroRisk, KursDollara, range, interval);
                dir.AddNewAddToList();
                dir2.AddNewAddToList();
                Akcii = dir.GetList();
                Zerorisk =dir2.GetList();

                Update.Content = "Скачать";
                Srok_update.Visibility = Visibility.Visible;
                Update.Visibility = Visibility.Hidden;
                Srok_panel.IsEnabled = false;

            }


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

        private void slValue2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }

        private void Srok_update_Click(object sender, RoutedEventArgs e)
        {
            Update.Visibility = Visibility.Visible;
            Srok_panel.IsEnabled = true;
            Update.Content = "Обновить базу знаний";
            Srok_update.Visibility = Visibility.Hidden;
            //Можно обновлять только индексы
        }
    }
}
