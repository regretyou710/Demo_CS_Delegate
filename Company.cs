using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_CS_Delegate
{
    class Company : INewspaper
    {
        public string Name { get; set; }
        public Newspaper Newspaper { get; set; }
        public Company()
        {

        }
        public Company(string name)
        {
            this.Name = name;
        }

        public void ReceiveNewspaper(Newspaper newspaper)
        {
            this.Newspaper = newspaper;
        }

        public void ReadNewspaper()
        {
            Console.WriteLine("Company:{0}在讀, 出版社: {1}, 報紙標題: {2}, 報紙內容: {3}", this.Name, this.Newspaper.Name, this.Newspaper.Title, this.Newspaper.Content);
        }
    }
}
