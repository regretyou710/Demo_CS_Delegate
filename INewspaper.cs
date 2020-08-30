using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_CS_Delegate
{
    interface INewspaper
    {
        //接收報紙
        void ReceiveNewspaper(Newspaper newspaper);


        //閱讀報紙
        void ReadNewspaper();
    }
}
