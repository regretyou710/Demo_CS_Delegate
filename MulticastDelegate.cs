using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_CS_Delegate
{
    class MulticastDelegate
    {
        //宣告Action()委託函數
        Action<int> all;
        //Action<int> Print = delegate(int i){Console.WriteLine(i);};
        Action<int> Print = i => Console.WriteLine(i);
        Action<int> AddThenPring = i => { i++; Console.WriteLine(i); };

        public void display()
        {
            Print(1);
            AddThenPring(1);

            //由方法簽章相同關係下指派給all變數
            all = Print;
            all(1);
            Console.WriteLine("----------多播委託----------");
            all = Print + AddThenPring;
            all = all + Print;
            all(1);

            //清空多波委託
            all = null;
            // all(1);// exception
        }
    }


    // 委託應用，與觀察者模式不同，沒有使用統一介面方式操作
    class Person_MulticastDelegate
    {
        public string Name { get; set; }
        public Newspaper_MulticastDelegete Newspaper { get; set; }

        public Person_MulticastDelegate()
        {

        }

        public Person_MulticastDelegate(string name)
        {
            this.Name = name;
        }


        //接收報紙
        public void ReceiveNewspaper(Newspaper_MulticastDelegete newspaper)
        {
            this.Newspaper = newspaper;
        }


        //接收報紙(多家報社，想知道是來自哪家報社報紙)
        public void ReceiveNewspaper2(Object sender, PublisherArgs newspaper)
        {
            if (sender is Publisher_MulticastDelegate)
            {
                newspaper.Newspaper.PublisherName = (sender as Publisher_MulticastDelegate).Name;
            }

            if (sender is Publisher_MulticastDelegate2)
            {
                newspaper.Newspaper.PublisherName = (sender as Publisher_MulticastDelegate2).Name;
            }
            this.Newspaper = newspaper.Newspaper;
        }


        //閱讀報紙
        public void ReadNewspaper()
        {
            Console.WriteLine("Person:{0}在讀, 出版社: {1}, 報紙標題: {2}, 報紙內容: {3}", this.Name, this.Newspaper.PublisherName, this.Newspaper.Title, this.Newspaper.Content);
        }

    }

    //將委託的Subscribers(Newspaper_MulticastDelegete newspaper)參數封裝
    class PublisherArgs : System.EventArgs
    {
        public Newspaper_MulticastDelegete Newspaper { get; set; }
        public PublisherArgs(Newspaper_MulticastDelegete newspaper)
        {
            this.Newspaper = newspaper;
        }
    }


    class Publisher_MulticastDelegate
    {
        public string Name { get; set; }
        //自定義委託類型，訂閱者的接收報紙方法與委託簽章相同
        public delegate void Subscribers(Newspaper_MulticastDelegete newspaper);
        //宣告委託變數
        public Subscribers _Subscribers = null;
        //(封裝委託)
        //public event Subscribers _Subscribers = null;


        //多家報社
        //自定義委託類型，訂閱者的接收報紙方法與委託簽章相同
        //public delegate void Subscribers2(Object sender, Newspaper_MulticastDelegete newspaper);
        //封裝委託參數
        public delegate void Subscribers2(Object sender, PublisherArgs newspaper);
        //宣告委託變數
        public event Subscribers2 _Subscribers2 = null;


        public Publisher_MulticastDelegate(string name)
        {
            this.Name = name;
        }

        //寄送報紙
        public void SendNewspaper(Newspaper_MulticastDelegete newspaper)
        {
            newspaper.PublisherName = this.Name;
            //使用委託
            if (_Subscribers != null)
            {
                //_Subscribers(newspaper);

                //如果在多播委託時產生例外:
                //將_Subscribers中的調用委託函數列表迭代出來
                foreach (Subscribers hander in _Subscribers.GetInvocationList())
                {
                    try
                    {
                        //每一個元素都是Subscribers類型函數，調用hander(參數);
                        hander(newspaper);
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }
                }

            }

        }


        //寄送報紙(多家報社)
        public void SendNewspaper2(Newspaper_MulticastDelegete newspaper)
        {
            //使用委託
            if (_Subscribers2 != null)
            {
                //_Subscribers(newspaper);

                //如果在多播委託時產生例外:
                //將_Subscribers中的調用委託函數列表迭代出來
                foreach (Subscribers2 hander in _Subscribers2.GetInvocationList())
                {
                    try
                    {
                        //每一個元素都是Subscribers類型函數，調用hander(調用Subscribers2()的當前類,參數);
                        hander(this, new PublisherArgs(new Newspaper_MulticastDelegete()));
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }
                }

            }

        }
    }



    class Newspaper_MulticastDelegete
    {
        public string PublisherName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }

    class Company_MulticastDelegete
    {
        public string Name { get; set; }
        public Newspaper_MulticastDelegete Newspaper { get; set; }
        public Company_MulticastDelegete()
        {

        }
        public Company_MulticastDelegete(string name)
        {
            this.Name = name;
        }

        public void ReceiveNewspaper(Newspaper_MulticastDelegete newspaper)
        {
            this.Newspaper = newspaper;
        }

        public void ReadNewspaper()
        {
            Console.WriteLine("Company:{0}在讀, 出版社: {1}, 報紙標題: {2}, 報紙內容: {3}", this.Name, this.Newspaper.PublisherName, this.Newspaper.Title, this.Newspaper.Content);
        }

        //接收報紙(多家報社，想知道是來自哪家報社報紙)
        public void ReceiveNewspaper2(Object sender, PublisherArgs newspaper)
        {

            if (sender is Publisher_MulticastDelegate)
            {
                newspaper.Newspaper.PublisherName = (sender as Publisher_MulticastDelegate).Name;
            }

            if (sender is Publisher_MulticastDelegate2)
            {
                newspaper.Newspaper.PublisherName = (sender as Publisher_MulticastDelegate2).Name;
            }
            this.Newspaper = newspaper.Newspaper;

        }
    }

    class Publisher_MulticastDelegate2
    {
        public string Name { get; set; }

        //多家報社
        //自定義委託類型，訂閱者的接收報紙方法與委託簽章相同
        //public delegate void Subscribers2(Object sender, Newspaper_MulticastDelegete newspaper);
        //封裝委託參數
        public delegate void Subscribers2(Object sender, PublisherArgs newspaper);
        //宣告委託變數
        public event Subscribers2 _Subscribers2 = null;


        public Publisher_MulticastDelegate2(string name)
        {
            this.Name = name;
        }

        //寄送報紙(多家報社)
        public void SendNewspaper2(Newspaper_MulticastDelegete newspaper)
        {
            //使用委託
            if (_Subscribers2 != null)
            {
                //_Subscribers(newspaper);

                //如果在多播委託時產生例外:
                //將_Subscribers中的調用委託函數列表迭代出來
                foreach (Subscribers2 hander in _Subscribers2.GetInvocationList())
                {
                    try
                    {
                        //每一個元素都是Subscribers類型函數，調用hander(調用Subscribers2()的當前類,參數);
                        hander(this, new PublisherArgs(new Newspaper_MulticastDelegete()));
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }
                }

            }

        }
    }
}
