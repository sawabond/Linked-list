using System;

namespace AISD_Lab2
{
    public class Node
    {
        public int Info { get; set; }
        public Node Next { get; set; }
        public Node(int info, Node next)
        {
            if (next == null)
                throw new ArgumentNullException();
            Info = info;
            Next = next;
        }
        public Node(int info)
        {
            Info = info;
            Next = null;
        }
        public Node()
        {
            Info = default;
            Next = null;
        }
        public override string ToString()
        {
            return $"{Info}";
        }
    }
}
