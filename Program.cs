﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_CS_Delegate
{
    class Program
    {
        static void Main(string[] args)
        {
            //ExecuteDemo01();
            ExecuteDemo02();
            ExecuteDemo03();

            Console.ReadLine();
        }

        static void ExecuteDemo01()
        {
            Calculator cal = new Calculator();
            int rs1 = cal.GetCalculate(100, 200, Items.Add);
            int rs2 = cal.GetCalculate(100, 200, Items.Sub);
            int rs3 = cal.GetCalculate(100, 200, Items.Mul);
            int rs4 = cal.GetCalculate(100, 200, Items.Div);
            Console.WriteLine("計算結果: {0}", rs1);
            Console.WriteLine("計算結果: {0}", rs2);
            Console.WriteLine("計算結果: {0}", rs3);
            Console.WriteLine("計算結果: {0}", rs4);

            Console.WriteLine("-------------------------");

            List<int> list = new List<int>() { 44, 5, 61, 32, 18 };
            int rs5 = cal.GetCalculate(list, Items.Sum);
            int rs6 = cal.GetCalculate(list, Items.Avg);
            int rs7 = cal.GetCalculate(list, Items.Max);
            int rs8 = cal.GetCalculate(list, Items.Min);
            Console.WriteLine("計算結果: {0}", rs5);
            Console.WriteLine("計算結果: {0}", rs6);
            Console.WriteLine("計算結果: {0}", rs7);
            Console.WriteLine("計算結果: {0}", rs8);

            Console.WriteLine("-------------------------");

            int rs9 = cal.GetCalculate(100, 200, cal.Add);
            int rs10 = cal.GetCalculate(100, 200, cal.Sub);
            int rs11 = cal.GetCalculate(100, 200, (x, y) => x * y);
            int rs12 = cal.GetCalculate(100, 200, (x, y) => x / y);
            Console.WriteLine("計算結果: {0}", rs9);
            Console.WriteLine("計算結果: {0}", rs10);
            Console.WriteLine("計算結果: {0}", rs11);
            Console.WriteLine("計算結果: {0}", rs12);

            Console.WriteLine("-------------------------");

            cal.GetCalculate(list, cal.Types2);
            //cal.GetCalculate(list, delegate (int x, int y) {return x > y;});
            cal.GetCalculate(list, (x, y) => x < y);
        }


        static void ExecuteDemo02()
        {
            Publisher publisher = new Publisher("C#出版社");
            Person p1 = new Person("p1");
            Person p2 = new Person("p2");
            Person p3 = new Person("p3");
            publisher.persons.Add(p1);
            publisher.persons.Add(p2);
            publisher.persons.Add(p3);

            Company c1 = new Company("c1");
            publisher.companies.Add(c1);


            //使用proxy
            Person p4 = new Person("p4");
            Company c2 = new Company("c2");
            publisher.Subscribers.Add(p4);
            publisher.Subscribers.Add(c2);

            publisher.SendNewspaper(new Newspaper() { Title = "Title", Content = "Content" });

            p1.ReadNewspaper();
            p2.ReadNewspaper();
            p3.ReadNewspaper();
            c1.ReadNewspaper();
            p4.ReadNewspaper();
            c2.ReadNewspaper();
        }


        static void ExecuteDemo03()
        {
            MulticastDelegate md = new MulticastDelegate();
            md.display();

            Console.WriteLine("----------多播委託應用----------");

            Publisher_MulticastDelegate publisher = new Publisher_MulticastDelegate("NET出版社");
            Person_MulticastDelegate p1 = new Person_MulticastDelegate("p1");
            Person_MulticastDelegate p2 = new Person_MulticastDelegate("p2");
            Company_MulticastDelegete c1 = new Company_MulticastDelegete("c1");
            Company_MulticastDelegete c2 = new Company_MulticastDelegete("c2");

            //<關鍵>透過多播委託方式讓訂閱者的訂閱方法指派給自定義委託函數
            publisher._Subscribers = p1.ReceiveNewspaper;
            publisher._Subscribers += p2.ReceiveNewspaper;

            //如果產生例外
            //publisher._Subscribers += delegate(Newspaper newspaper) { throw new ApplicationException("Current Error"); };
            publisher._Subscribers += newspaper => throw new ApplicationException("Current Error");


            publisher._Subscribers += c1.ReceiveNewspaper;
            publisher._Subscribers += c2.ReceiveNewspaper;

            //呼叫派送報紙方法時又去呼叫委託函數，而委託函數又指向訂閱報紙方法
            publisher.SendNewspaper(new Newspaper() { Title = "Title", Content = "Content" });

            p1.ReadNewspaper();
            p2.ReadNewspaper();
            c1.ReadNewspaper();
            c2.ReadNewspaper();
        }
    }
}
