using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Demo_CS_Delegate
{
    //列舉
    public enum Items
    {
        Add,
        Sub,
        Mul,
        Div,
        Sum,
        Avg,
        Min,
        Max
    }


    class Calculator
    {
        // 使用Func函數設定委託變數
        public Func<int, int, int> Add = delegate (int x, int y) { return x + y; };
        //public Func<int, int, int> Add = (x, y) => x - y;
        public Func<int, int, int> Sub = delegate (int x, int y) { return x - y; };
        public Func<int, int, int> Mul = delegate (int x, int y) { return x * y; };
        public Func<int, int, int> Div = delegate (int x, int y) { return x / y; };


        // 自訂委託類型
        public delegate bool Function(int x, int y);
        // 設定自訂委託變數
        public Function Types2 = delegate (int x, int y) { return x > y; };
        //public Function Types2 = (x, y) => x > y;
        public Function Types3 = delegate (int x, int y) { return x < y; };
        // 使用Func函數
        //public Func<int, int, bool> Types3 = delegate (int x, int y) { return x < y; };


        //列舉+switch()
        public int GetCalculate(int x, int y, Items items)
        {
            int sum = 0;
            try
            {
                switch (items)
                {
                    case Items.Add:
                        sum = x + y;
                        break;
                    case Items.Sub:
                        sum = x - y;
                        break;
                    case Items.Mul:
                        sum = x * y;
                        break;
                    case Items.Div:
                        sum = x / y;
                        break;
                    case Items.Sum:
                        sum = x / y;
                        break;
                    case Items.Avg:
                        sum = x / y;
                        break;
                    default:
                        throw new MyException("操作計算器異常");

                }
            }
            catch (MyException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return sum;
        }


        //列舉+switch()
        public int GetCalculate(List<int> list, Items items)
        {
            int rs = 0;
            try
            {
                switch (items)
                {
                    case Items.Sum:
                        rs = list.Sum();
                        break;
                    case Items.Avg:
                        rs = Convert.ToInt32(list.Average());
                        break;
                    case Items.Max:
                        rs = list.Max();
                        break;
                    case Items.Min:
                        rs = list.Min();
                        break;
                    default:
                        throw new MyException("操作計算器異常");

                }
            }
            catch (MyException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return rs;
        }


        //參數委託
        public int GetCalculate(int x, int y, Func<int, int, int> func)
        {
            int sum = 0;
            try
            {
                sum = func(x, y);
            }
            catch (Exception ex)
            {

                Console.Write(ex.Message);
            }
            return sum;
        }


        //參數委託
        public void GetCalculate(List<int> list, Function func)
        {
            int rs = list[0];
            try
            {
                foreach (int i in list)
                {
                    if (func(i, rs))
                    {
                        rs = i;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            Console.WriteLine("計算結果: " + rs);
        }


        //自訂例外
        class MyException : ApplicationException
        {
            public MyException(string message) : base(message)
            {

            }
        }


        /*
        public int Add(int x, int y)
        {
            int sum = 0;
            sum = x + y;
            return sum;
        }

        public int Sub(int x, int y)
        {
            int sum = 0;
            sum = x - y;
            return sum;
        }

        public int Mul(int x, int y)
        {
            int sum = 0;
            sum = x * y;
            return sum;
        }

        public int Div(int x, int y)
        {
            int sum = 0;
            sum = x / y;
            return sum;
        }
        */
    }
}
