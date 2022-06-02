using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfKurs
{
    internal interface IGetData
    {
        
        int AddNewAddToList();
        Akcia GetADataFromYahoo(string name);
        Kripta GetBDataFromYahoo(string name);
         
        bool FindInString(List<string> list, string s);


    }
}
