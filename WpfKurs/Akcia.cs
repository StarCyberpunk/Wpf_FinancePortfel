using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfKurs
{
    public class Akcia
    {
        
        public double CurrentPrice { get; set; }
        public double recomendationMean { get; set; }
        public double NormaPriboli { get; set; }
        public double TargetPriceHigh { get; set; }
        public double TargetPriceLow { get; set; }
        public double TargetPriceMean { get; set; }
        public double TargetPriceMedian { get; set; }
        public string symbol { get; set; }
        public Recommendedsymbol[] recommendedSymbols { get; set; }
        public Recommendationtrend recommendationTrend { get; set; }
        public string peergroups { get; set; }
        public double RiskScore { get; set; }
        public int[] timestamp { get; set; }
        public Meta meta { get; set; }
        public Indicators indicators { get; set; }
    }
    #region GetDate
    public class Rootobject
    {
        public Quotesummary quoteSummary { get; set; }
        public Finance finance { get; set; }
        public Chart chart { get; set; }
    }
    public class Chart
    {
        public Result[] result { get; set; }
        public object error { get; set; }
    }

    public class Quotesummary
    {
        public Result[] result { get; set; }
        public object error { get; set; }


    }

    public class Result
    {
        public Financialdata financialData { get; set; }
        public string symbol { get; set; }
        public Recommendedsymbol[] recommendedSymbols { get; set; }
        public Recommendationtrend recommendationTrend { get; set; }
        public Esgscores esgScores { get; set; }
        public Summarydetail summaryDetail { get; set; }
        public Meta meta { get; set; }
        public int[] timestamp { get; set; }
        public Events events { get; set; }
        public Indicators indicators { get; set; }
    }

    public class Financialdata
    {
        public int maxAge { get; set; }
        public Currentprice currentPrice { get; set; }
        public Targethighprice targetHighPrice { get; set; }
        public Targetlowprice targetLowPrice { get; set; }
        public Targetmeanprice targetMeanPrice { get; set; }
        public Targetmedianprice targetMedianPrice { get; set; }
        public Recommendationmean recommendationMean { get; set; }
        public string recommendationKey { get; set; }
        public Numberofanalystopinions numberOfAnalystOpinions { get; set; }
        public Totalcash totalCash { get; set; }
        public Totalcashpershare totalCashPerShare { get; set; }
        public Ebitda ebitda { get; set; }
        public Totaldebt totalDebt { get; set; }
        public Quickratio quickRatio { get; set; }
        public Currentratio currentRatio { get; set; }
        public Totalrevenue totalRevenue { get; set; }
        public Debttoequity debtToEquity { get; set; }
        public Revenuepershare revenuePerShare { get; set; }
        public Returnonassets returnOnAssets { get; set; }
        public Returnonequity returnOnEquity { get; set; }
        public Grossprofits grossProfits { get; set; }
        public Freecashflow freeCashflow { get; set; }
        public Operatingcashflow operatingCashflow { get; set; }
        public Earningsgrowth earningsGrowth { get; set; }
        public Revenuegrowth revenueGrowth { get; set; }
        public Grossmargins grossMargins { get; set; }
        public Ebitdamargins ebitdaMargins { get; set; }
        public Operatingmargins operatingMargins { get; set; }
        public Profitmargins profitMargins { get; set; }
        public string financialCurrency { get; set; }
    }
    public class Finance
    {
        public Result[] result { get; set; }
        public object error { get; set; }
    }
    #region charts
    public class Meta
    {
        public string currency { get; set; }
        public string symbol { get; set; }
        public string exchangeName { get; set; }
        public string instrumentType { get; set; }
        public int firstTradeDate { get; set; }
        public int regularMarketTime { get; set; }
        public int gmtoffset { get; set; }
        public string timezone { get; set; }
        public string exchangeTimezoneName { get; set; }
        public float regularMarketPrice { get; set; }
        public float chartPreviousClose { get; set; }
        public int priceHint { get; set; }
        public Currenttradingperiod currentTradingPeriod { get; set; }
        public string dataGranularity { get; set; }
        public string range { get; set; }
        public string[] validRanges { get; set; }
    }

    public class Currenttradingperiod
    {
        public Pre pre { get; set; }
        public Regular regular { get; set; }
        public Post post { get; set; }
    }

    public class Pre
    {
        public string timezone { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public int gmtoffset { get; set; }
    }

    public class Regular
    {
        public string timezone { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public int gmtoffset { get; set; }
    }

    public class Post
    {
        public string timezone { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public int gmtoffset { get; set; }
    }

    public class Events
    {
        public Dividends dividends { get; set; }
    }

    public class Dividends
    {
        public _1636119000 _1636119000 { get; set; }
        public _1643985000 _1643985000 { get; set; }
    }

    public class _1636119000
    {
        public float amount { get; set; }
        public int date { get; set; }
    }

    public class _1643985000
    {
        public float amount { get; set; }
        public int date { get; set; }
    }

    public class Indicators
    {
        public Quote[] quote { get; set; }
        public Adjclose[] adjclose { get; set; }
    }

    public class Quote
    {
        public float[] low { get; set; }
        public float[] open { get; set; }
        public float[] high { get; set; }
        public float[] close { get; set; }
        public int[] volume { get; set; }
    }

    public class Adjclose
    {
        public float[] adjclose { get; set; }
    }
    #endregion

    #region recomend
    public class Recommendedsymbol
    {
        public string symbol { get; set; }
        public float score { get; set; }
    }
    public class Currentprice
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Targethighprice
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Targetlowprice
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Targetmeanprice
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Targetmedianprice
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Recommendationmean
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Numberofanalystopinions
    {
        public int raw { get; set; }
        public string fmt { get; set; }
        public string longFmt { get; set; }
    }

    public class Totalcash
    {
        public long raw { get; set; }
        public string fmt { get; set; }
        public string longFmt { get; set; }
    }

    public class Totalcashpershare
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Ebitda
    {
    }

    public class Totaldebt
    {
        public long raw { get; set; }
        public string fmt { get; set; }
        public string longFmt { get; set; }
    }

    public class Quickratio
    {
    }

    public class Currentratio
    {
    }

    public class Totalrevenue
    {
        public long raw { get; set; }
        public string fmt { get; set; }
        public string longFmt { get; set; }
    }

    public class Debttoequity
    {
    }

    public class Revenuepershare
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Returnonassets
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Returnonequity
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Grossprofits
    {
        public long raw { get; set; }
        public string fmt { get; set; }
        public string longFmt { get; set; }
    }

    public class Freecashflow
    {
    }

    public class Operatingcashflow
    {
        public long raw { get; set; }
        public string fmt { get; set; }
        public string longFmt { get; set; }
    }

    public class Earningsgrowth
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Revenuegrowth
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Grossmargins
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Ebitdamargins
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Operatingmargins
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Profitmargins
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }
    #endregion
    #region trends
    public class Recommendationtrend
    {
        public Trend[] trend { get; set; }
        public int maxAge { get; set; }
    }

    public class Trend
    {
        public string period { get; set; }
        public int strongBuy { get; set; }
        public int buy { get; set; }
        public int hold { get; set; }
        public int sell { get; set; }
        public int strongSell { get; set; }
    }
    #endregion
    #region EGS
    public class Esgscores
    {
        public int maxAge { get; set; }
        public Totalesg totalEsg { get; set; }
        public Environmentscore environmentScore { get; set; }
        public Socialscore socialScore { get; set; }
        public Governancescore governanceScore { get; set; }
        public int ratingYear { get; set; }
        public int ratingMonth { get; set; }
        public float highestControversy { get; set; }
        public int peerCount { get; set; }
        public string esgPerformance { get; set; }
        public string peerGroup { get; set; }
        public string[] relatedControversy { get; set; }
        public Peeresgscoreperformance peerEsgScorePerformance { get; set; }
        public Peergovernanceperformance peerGovernancePerformance { get; set; }
        public Peersocialperformance peerSocialPerformance { get; set; }
        public Peerenvironmentperformance peerEnvironmentPerformance { get; set; }
        public Peerhighestcontroversyperformance peerHighestControversyPerformance { get; set; }
        public Percentile percentile { get; set; }
        public object environmentPercentile { get; set; }
        public object socialPercentile { get; set; }
        public object governancePercentile { get; set; }
        public bool adult { get; set; }
        public bool alcoholic { get; set; }
        public bool animalTesting { get; set; }
        public bool catholic { get; set; }
        public bool controversialWeapons { get; set; }
        public bool smallArms { get; set; }
        public bool furLeather { get; set; }
        public bool gambling { get; set; }
        public bool gmo { get; set; }
        public bool militaryContract { get; set; }
        public bool nuclear { get; set; }
        public bool pesticides { get; set; }
        public bool palmOil { get; set; }
        public bool coal { get; set; }
        public bool tobacco { get; set; }
    }

    public class Totalesg
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Environmentscore
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Socialscore
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Governancescore
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Peeresgscoreperformance
    {
        public float min { get; set; }
        public float avg { get; set; }
        public float max { get; set; }
    }

    public class Peergovernanceperformance
    {
        public float min { get; set; }
        public float avg { get; set; }
        public float max { get; set; }
    }

    public class Peersocialperformance
    {
        public float min { get; set; }
        public float avg { get; set; }
        public float max { get; set; }
    }

    public class Peerenvironmentperformance
    {
        public float min { get; set; }
        public float avg { get; set; }
        public float max { get; set; }
    }

    public class Peerhighestcontroversyperformance
    {
        public float min { get; set; }
        public float avg { get; set; }
        public float max { get; set; }
    }

    public class Percentile
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }
    #endregion
    #region Kripta
    public class Summarydetail
    {
        public int maxAge { get; set; }
        public Pricehint priceHint { get; set; }
        public Previousclose previousClose { get; set; }
        public Open open { get; set; }
        public Daylow dayLow { get; set; }
        public Dayhigh dayHigh { get; set; }
        public Regularmarketpreviousclose regularMarketPreviousClose { get; set; }
        public Regularmarketopen regularMarketOpen { get; set; }
        public Regularmarketdaylow regularMarketDayLow { get; set; }
        public Regularmarketdayhigh regularMarketDayHigh { get; set; }
        public Dividendrate dividendRate { get; set; }
        public Dividendyield dividendYield { get; set; }
        public Exdividenddate exDividendDate { get; set; }
        public Payoutratio payoutRatio { get; set; }
        public Fiveyearavgdividendyield fiveYearAvgDividendYield { get; set; }
        public Beta beta { get; set; }
        public Forwardpe forwardPE { get; set; }
        public Volume volume { get; set; }
        public Regularmarketvolume regularMarketVolume { get; set; }
        public Averagevolume averageVolume { get; set; }
        public Averagevolume10days averageVolume10days { get; set; }
        public Averagedailyvolume10day averageDailyVolume10Day { get; set; }
        public Bid bid { get; set; }
        public Ask ask { get; set; }
        public Bidsize bidSize { get; set; }
        public Asksize askSize { get; set; }
        public Marketcap marketCap { get; set; }
        public Yield yield { get; set; }
        public Ytdreturn ytdReturn { get; set; }
        public Totalassets totalAssets { get; set; }
        public Expiredate expireDate { get; set; }
        public Strikeprice strikePrice { get; set; }
        public Openinterest openInterest { get; set; }
        public Fiftytwoweeklow fiftyTwoWeekLow { get; set; }
        public Fiftytwoweekhigh fiftyTwoWeekHigh { get; set; }
        public Pricetosalestrailing12months priceToSalesTrailing12Months { get; set; }
        public Fiftydayaverage fiftyDayAverage { get; set; }
        public Twohundreddayaverage twoHundredDayAverage { get; set; }
        public Trailingannualdividendrate trailingAnnualDividendRate { get; set; }
        public Trailingannualdividendyield trailingAnnualDividendYield { get; set; }
        public Navprice navPrice { get; set; }
        public string currency { get; set; }
        public string fromCurrency { get; set; }
        public string toCurrency { get; set; }
        public string lastMarket { get; set; }
        public Volume24hr volume24Hr { get; set; }
        public Volumeallcurrencies volumeAllCurrencies { get; set; }
        public Circulatingsupply circulatingSupply { get; set; }
        public object algorithm { get; set; }
        public Maxsupply maxSupply { get; set; }
        public Startdate startDate { get; set; }
        public bool tradeable { get; set; }
    }

    public class Pricehint
    {
        public int raw { get; set; }
        public string fmt { get; set; }
        public string longFmt { get; set; }
    }

    public class Previousclose
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Open
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Daylow
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Dayhigh
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Regularmarketpreviousclose
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Regularmarketopen
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Regularmarketdaylow
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Regularmarketdayhigh
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Dividendrate
    {
    }

    public class Dividendyield
    {
    }

    public class Exdividenddate
    {
    }

    public class Payoutratio
    {
    }

    public class Fiveyearavgdividendyield
    {
    }

    public class Beta
    {
    }

    public class Forwardpe
    {
    }

    public class Volume
    {
        public long raw { get; set; }
        public string fmt { get; set; }
        public string longFmt { get; set; }
    }

    public class Regularmarketvolume
    {
        public long raw { get; set; }
        public string fmt { get; set; }
        public string longFmt { get; set; }
    }

    public class Averagevolume
    {
        public long raw { get; set; }
        public string fmt { get; set; }
        public string longFmt { get; set; }
    }

    public class Averagevolume10days
    {
        public long raw { get; set; }
        public string fmt { get; set; }
        public string longFmt { get; set; }
    }

    public class Averagedailyvolume10day
    {
        public long raw { get; set; }
        public string fmt { get; set; }
        public string longFmt { get; set; }
    }

    public class Bid
    {
    }

    public class Ask
    {
    }

    public class Bidsize
    {
    }

    public class Asksize
    {
    }

    public class Marketcap
    {
        public long raw { get; set; }
        public string fmt { get; set; }
        public string longFmt { get; set; }
    }

    public class Yield
    {
    }

    public class Ytdreturn
    {
    }

    public class Totalassets
    {
    }

    public class Expiredate
    {
    }

    public class Strikeprice
    {
    }

    public class Openinterest
    {
    }

    public class Fiftytwoweeklow
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Fiftytwoweekhigh
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Pricetosalestrailing12months
    {
    }

    public class Fiftydayaverage
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Twohundreddayaverage
    {
        public float raw { get; set; }
        public string fmt { get; set; }
    }

    public class Trailingannualdividendrate
    {
    }

    public class Trailingannualdividendyield
    {
    }

    public class Navprice
    {
    }

    public class Volume24hr
    {
        public long raw { get; set; }
        public string fmt { get; set; }
        public string longFmt { get; set; }
    }

    public class Volumeallcurrencies
    {
        public long raw { get; set; }
        public string fmt { get; set; }
        public string longFmt { get; set; }
    }

    public class Circulatingsupply
    {
        public long raw { get; set; }
        public string fmt { get; set; }
        public string longFmt { get; set; }
    }

    public class Maxsupply
    {
    }

    public class Startdate
    {
    }
    #endregion
    #endregion
}
