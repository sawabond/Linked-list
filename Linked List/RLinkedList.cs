using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISD_Lab2
{
    /* 1.	Создать функции для работы с односвязным списком: 
     * добавление элемента после заданного, в начало и в конец, 
     * удаление элемента по значению, поиск элемента по значению, 
     * обмен двух элементов списка местами, распечатка списка. 
     * Список представлен в динамической памяти.
        */
    public class RLinkedList : IEnumerable
    {
        /// <summary>
        /// Односвязные список
        /// </summary>
        /// <remarks>
        /// Имеет функции добавление элемента после заданного, в начало и в конец, 
        /// удаление элемента по значению, поиск элемента по значению, 
        /// обмен двух элементов списка местами, распечатка списка.
        /// Список представлен в динамической памяти.
        /// </remarks>
        private Node _head;
        private Node _tail;
        private int _count;
        public Node Head
        {
            get => _head;
            set { _head = value; }
        }
        public Node Tail
        {
            get => _tail;
            set { _tail = value; }
        }
        public int Count
        {
            get => _count;
            private set { _count = value; }
        }
        public RLinkedList()
        {
            Head = null;
            Tail = Head;
            Count = 0;
        }
        public RLinkedList(int info)
        {
            Head = new Node(info);
            Tail = Head;
            Count = 1;
        }
        public RLinkedList(int info, Node next)
        {
            Head = new Node(info);
            Tail = next;
            Count = 2;
        }
        /// <summary>
        /// Добавляет узел со значением инфо в конец списка
        /// </summary>
        public void PushBack(int info)
        {
            var newNode = new Node(info);
            if (Count == 0)
            {
                Head = newNode;
                Tail = Head;
                Count = 1;
                return;
            }
            Tail.Next = newNode;
            Tail = Tail.Next;
            Count++;
        }
        /// <summary>
        /// Добавляет узел со значением info в начало списка
        /// </summary>
        public void PushFront(int info)
        {
            var newNode = new Node(info);
            if (Count == 0)
            {
                Head = newNode;
                Tail = Head;
                Count = 1;
                return;
            }
            newNode.Next = Head;
            Head = newNode;
            Count++;
        }
        /// <summary>
        /// Добавляет узел со значением info на позицию position
        /// </summary>
        public void Add(int info, int position)
        {
            if (position > Count || position < 0)
                throw new IndexOutOfRangeException();
            var newNode = new Node(info);
            if (position == 0)
            {
                
                newNode.Next = Head;
                Head = newNode;
                Count++;
                return;
            }
            Node currentNode = this[position - 1];
            newNode.Next = currentNode.Next;
            currentNode.Next = newNode;
            Count++;
        }
        public bool AddAfter(int info, int afterInfo)
        {
            var node = FindNode(afterInfo);
            var newNode = new Node(info);
            if (node.Equals(Tail))
            {
                Tail.Next = newNode;
                Tail = newNode;
                Count++;
                return true;
            }
            if (node != null)
            {
                newNode.Next = node.Next;
                node.Next = newNode;
                Count++;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Выводит список в консоль
        /// </summary>
        public void Print()
        {
            var node = Head;
            while (node.Equals(Tail) == false)
            {
                Console.Write($"{node.Info} ");
                node = node.Next;
            }
            Console.WriteLine(node.Info);
        }
        /// <summary>
        /// Удаляет все узлы со значением info
        /// </summary>
        
        /// <summary>
        /// Инвертирует порядок элементов в списке
        /// </summary>
        public void Reverse()
        {
            if (Count <= 1) return;
            Node prev = null;
            Node current = Head;
            Node prevHead = Head;
            while (current.Next != null)
            {
                var temp = current.Next;
                current.Next = prev;
                prev = current;
                current = temp;
            }
            Head = current;
            current.Next = prev;
            Tail = prevHead;
        }
        public void EraseAll(int info)
        {
            while (Head.Info.Equals(info))
            {
                Head = Head.Next;
            }
            Node currentNode = Head;
            while (currentNode.Next != null)
            {
                if (currentNode.Next.Info.Equals(info))
                {
                    currentNode.Next = currentNode.Next.Next;
                    Count--;
                    continue;
                }
                currentNode = currentNode.Next;
            }
            Tail = currentNode;
        }
        /// <summary>
        /// Удаляет все узлы с чётным значением
        /// </summary>
        public void EraseAllEven()
        {
            if (Head == null) return;
            Node temporaryNode = new Node();
            while (Head.Info % 2 == 0)
            {
                temporaryNode = Head.Next;
                Head.Next = null;
                Head = temporaryNode;
                Count--;
                if (Count == 0)
                {
                    this.Clear();
                    return;
                }
            }
            var currentNode = Head;
            while (currentNode.Next?.Equals(Tail) == false)
            {
                var tempoNode = currentNode;
                while (currentNode?.Next?.Info % 2 == 0)
                {
                    currentNode = currentNode.Next;
                    Count--;
                }
                if (currentNode.Next == null)
                {
                    Tail = tempoNode;
                    return;
                }
                tempoNode.Next = currentNode.Next;
                currentNode = currentNode.Next;
            }
            if (Tail.Info % 2 == 0)
            {
                Tail = currentNode;
                currentNode.Next = null;
                Count--;
            }
        }
        /// <summary>
        /// Удаляет узел со значением info
        /// </summary>
        public void Erase(int info)
        {
            if (Head.Info.Equals(info))
            {
                Head = Head.Next;
                Count--;
                return;
            }

            var previousNode = FindPreviousNode(info);
            if (previousNode == null) return;

            if (previousNode.Next.Equals(Tail))
            {
                previousNode.Next = null;
                Tail = previousNode;
                Count--;
                return;
            }
            previousNode.Next = previousNode.Next.Next;
            Count--;
        }
        /// <summary>
        /// Находит узел, который стоит перед узлом со значением info
        /// </summary>
        public Node FindPreviousNode(int info)
        {
            var currentNode = Head;
            if (currentNode.Info.Equals(info)) return null;
            while (currentNode.Next.Equals(Tail) == false)
            {
                if (currentNode.Next.Info.Equals(info))
                    return currentNode;
                currentNode = currentNode?.Next;
            }
            if (currentNode.Next.Info.Equals(info))
                return currentNode;
            return null;
        }
        public void Swap(int firstIndex, int secondIndex)
        {
            if (firstIndex < 0 || firstIndex >= Count
                || secondIndex < 0 || secondIndex >= Count)
            {
                throw new IndexOutOfRangeException();
            }

            int t = firstIndex;
            firstIndex = Math.Min(t, secondIndex); 
            secondIndex = Math.Max(t, secondIndex); 

            // if we swap head and tail
            if (firstIndex == 0 && secondIndex == Count - 1)
            {
                SwapHeadAndTail();
                return;
            }
            // if we swap Head
            if (Math.Min(firstIndex, secondIndex) == 0)
            {
                SwapHead(secondIndex);
                return;
            }
            // if we swap Tail
            if (Math.Max(firstIndex, secondIndex) == Count - 1)
            {
                SwapTail(firstIndex);
                return;
            }
            var firstPrevious= this[firstIndex - 1];
            var secondPrevious = this[secondIndex - 1];

            var firstNode = firstPrevious.Next;
            var secondNode = secondPrevious.Next;

            Node tFirst = firstNode.Next, tSecond = secondNode.Next;
            firstNode.Next = tSecond;
            secondNode.Next = tFirst;

            firstPrevious.Next = secondNode;
            secondPrevious.Next = firstNode;
        }
        public void SwapHeadAndTail()
        {
            var head = Head;
            var headNext = head.Next;
            var previousTail = this[Count - 2];
            var tail = Tail;

            previousTail.Next = head;
            head.Next = null;
            tail.Next = headNext;
            Tail = head;
            Head = tail;
            return;
        }
        public void SwapHead(int secondIndex)
        {
            var previous = this[secondIndex - 1];
            var second = previous.Next;
            var secondNext = second.Next;
            var t = Head;
            var tNext = Head.Next;

            Head = second;
            second.Next = tNext;
            previous.Next = t;
            t.Next = secondNext;
            return;
        }
        public void SwapTail(int firstIndex)
        {
            var previous = this[firstIndex - 1];
            var second = previous.Next;
            var secondNext = second.Next;
            var t = Tail;
            var previousTail = this[Count - 2];

            Tail = second;
            previousTail.Next = second;
            Tail.Next = null;
            t.Next = secondNext;
            previous.Next = t;
            return;
        }
        /// <summary>
        /// Находит узел со значением info
        /// </summary>
        public Node FindNode(int info)
        {
            var currentNode = Head;
            while (currentNode.Equals(Tail) == false)
            {
                if (currentNode.Info.Equals(info))
                    return currentNode;
                currentNode = currentNode?.Next;
            }
            if (currentNode.Info.Equals(info))
                return currentNode;
            return null;
        }
        /// <summary>
        /// Очищает список
        /// </summary>
        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }
        public IEnumerator GetEnumerator()
        {
            var currentNode = Head;
            while (currentNode != null)
            {
                yield return currentNode;
                currentNode = currentNode.Next;
            }
        }
        public Node this[int position]
        {
            get
            {
                if (position < 0 || position >= Count)
                    throw new IndexOutOfRangeException();
                Node currentNode = Head;
                for (var i = 0; i < position; i++)
                {
                    currentNode = currentNode.Next;
                }
                return currentNode;
            }
            set
            {
                if (position < 0 || position >= Count)
                    throw new IndexOutOfRangeException();
                Node currentNode = Head;
                for (var i = 0; i < position; i++)
                {
                    currentNode = currentNode.Next;
                }
                currentNode = value;
            }
        }
    }
}
