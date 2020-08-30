using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_CS_Delegate
{
    class Person : INewspaper
    {
        public string Name { get; set; }
        public Newspaper Newspaper { get; set; }
        public Person()
        {

        }
        public Person(string name)
        {
            this.Name = name;
        }

        //接收報紙
        public void ReceiveNewspaper(Newspaper newspaper)
        {
            this.Newspaper = newspaper;
        }


        //閱讀報紙
        public void ReadNewspaper()
        {
            Console.WriteLine("Person:{0}在讀, 出版社: {1}, 報紙標題: {2}, 報紙內容: {3}", this.Name, this.Newspaper.Name, this.Newspaper.Title, this.Newspaper.Content);
        }
    }
}
