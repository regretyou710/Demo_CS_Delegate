using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_CS_Delegate
{
    class Publisher
    {
        public string Name { get; set; }
        public List<Person> persons = new List<Person>();
        //當現在又增加公司的訂報者
        public List<Company> companies = new List<Company>();
        //多型
        public List<INewspaper> Subscribers = new List<INewspaper>();


        public Publisher(string name)
        {
            this.Name = name;
        }

        //寄送報紙
        public void SendNewspaper(Newspaper newspaper)
        {
            newspaper.Name = this.Name;
            /*
            //此寫法代碼不封閉，雖然報紙寄送給Person，但對於閱讀報紙，
            //在程式執行時還要再建立一次Pereson物件，變成接收和閱讀的Person是不同的物件
            Person p1 = new Person("p1");
            Person p2 = new Person("p2");
            Person p3 = new Person("p3");

            p1.ReceiveNewspaper(newspaper);
            p2.ReceiveNewspaper(newspaper);
            p3.ReceiveNewspaper(newspaper);
            */

            // 使用委託，由程式執行時創建Person物件決定寄送報紙對象
            //persons.ForEach(delegate (Person p) { p.ReceiveNewspaper(newspaper); });
            persons.ForEach(p => p.ReceiveNewspaper(newspaper));
            companies.ForEach(c => c.ReceiveNewspaper(newspaper));


            //面向接口編程(觀察者模式又稱訂閱發布模式):將公共操作統一到接口
            //此時因為又增加對象，變成代碼不封閉，於是讓訂報者實現有接收報紙方法的介面
            Subscribers.ForEach(objs => objs.ReceiveNewspaper(newspaper));
        }
    }
}
