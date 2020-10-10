using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralizedList
{
    class GenList<T>
    {
        public List<T> list = new List<T>();

        /// <summary>
        /// Метод подсчета количества вхождений в список
        /// </summary>
        /// <returns></returns>
        public Dictionary<T, int> Statistic()
        {
            Dictionary<T, int> keyValues = new Dictionary<T, int>();

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
        /// Метод добавление элементов в список
        /// </summary>
        /// <param name="v">Массив элементов</param>
        internal void AddRange(T[] v)
        {
            list.AddRange(v);
        }

        /// <summary>
        /// Метод группировки элементов
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<IGrouping<T, T>> GroupBy()
        {
            return (IEnumerable<IGrouping<T, T>>)this.list.GroupBy(e => e);
        }

        /// <summary>
        /// Метод вывода на консоль элементов списка
        /// </summary>
        public void Print()
        {
            foreach (T item in list)
            {
                Console.WriteLine($"{item}");
            }
        }

        /// <summary>
        /// Метод вывода на консоль коллекции ключей и значений
        /// </summary>
        /// <param name="dict"></param>
        public void PrintGroupBy(IEnumerable<IGrouping<T, T>> dict)
        {
            foreach (var grp in dict)
            {
                Console.WriteLine($"{grp.Key} - {grp.Count()}");
            }
        }
    }
}
