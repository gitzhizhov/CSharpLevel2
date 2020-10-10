using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralizedList
{
    //2.	Дана коллекция List<T>.Требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции:
    //      a.  для целых чисел;
    //      b.  * для обобщенной коллекции;
    //      c.  ** используя Linq.

    // Андрей Жижов

    class Program
    {
        static void Main(string[] args)
        {
            #region List без использования ling
            List<char> list0 = new List<char> {'g', 'g', 'g' , 'r', 'r', 'p' };

            var keyValues = Statictic(list0);
            Print(keyValues);

            Console.ReadKey();
            #endregion

            Console.WriteLine();

            #region List<int>
            List<int> list = new List<int>();
            list.AddRange(new int[] { 20, 1, 4, 8, 9, 44, 4, 5, 9, 2, 32, 3, 3, 6, 1, 1 });
            
            var count = list.GroupBy(i => i);
            foreach (var grp in count)
            {
                Console.WriteLine($"{grp.Key} - {grp.Count()}");
            }

            Console.ReadKey();
            #endregion

            Console.WriteLine();

            #region List<T>
            GenList<string> list2 = new GenList<string>();
            list2.AddRange(new string[] { "20", "1", "4", "8", "9", "44", "4", "5", "9", "2", "32", "3", "3", "6", "1", "1" });
            //list2.Print();
            var count2 = list2.GroupBy();
            list2.PrintGroupBy(count2);

            Console.ReadKey();
            #endregion
        }

        /// <summary>
        /// Метод подсчета количества вхождений элемента в список
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        static Dictionary<char, int> Statictic(List<char> list)
        {
            Dictionary<char, int> keyValues = new Dictionary<char, int>();

            for (int i = 0; i < list.Count; i++)
            {
                if (keyValues.ContainsKey(list[i]))
                    keyValues[list[i]]++;
                else
                    keyValues.Add(list[i], 1);
            }
            return keyValues;
        }

        /// <summary>
        /// Метод выводит на консоль коллекцию ключей и значений
        /// </summary>
        /// <param name="d"></param>
        static void Print(Dictionary<char, int> d) 
        {
            foreach (var grp in d)
            {
                Console.WriteLine($"{grp.Key} - {grp.Value}");
            }
        }
    }
}
