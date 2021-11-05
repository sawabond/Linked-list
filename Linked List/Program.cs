using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISD_Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Различные элементы 09
            // 09 Найти количество различных элементов в списке целых чисел. в списке 1 2 2 1 всего 2 различных элемента
            var list = new List<int>{ 1, 2, 2, 1 };
            var set = new HashSet<int>(list);
            foreach (int i in set)
                Console.WriteLine(i);
            Console.WriteLine($"В списке всего {set.Count} различных элемента");
            #endregion
            #region Максимальный и минимальный. Свап 05
            // 05 максимальный и минимальный элементы списка и поменять их местами.
            var list2 = new RLinkedList();
            for (var i = 0; i < 4; i++)
                list2.PushBack(4);
            for (var i = 0; i <= 10; i++)
                list2.PushBack(i);
            list2.Add(-3, 5);
            list2.Add(24, 8);
            int minIndex = 0, maxIndex = 0;
            int min = list2.Head.Info;
            int max = list2.Head.Info;
            int j = 0;
            foreach (Node node in list2)
            {
                if (min < node.Info)
                {
                    min = node.Info;
                    minIndex = j;
                }
                else if (max > node.Info)
                {
                    max = node.Info;
                    maxIndex = j;
                }
                j++;
            }
            list2.Print();
            list2.Swap(maxIndex, minIndex);
            list2.Print();
            #endregion
            #region Исключить все четные 01


            // 01 Исключить из списка целых чисел все элементы с четными значениями
            list2.Add(4, 10);
            list2.Add(4, 10);
            list2.Add(4, 10);
            list2.Add(4, 10);
            list2.PushBack(2);
            list2.PushBack(2);
            list2.PushBack(2);
            list2.PushBack(7);
            list2.Print();
            list2.EraseAllEven();

            list2.Print();
            #endregion
            #region Реверс списка 07
            // 07.	Дан список элементов. Переставить элементы списка в обратном порядке.
            Console.WriteLine("============");
            var rlist = new RLinkedList();
            for (int i = 0; i < 9; i++)
                rlist.PushBack(i);
            rlist.Reverse();
            rlist.Print();
            Console.ReadLine();
            #endregion
        }
    }
}
