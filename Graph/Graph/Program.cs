using System;

namespace Graph {
    class Program {
        static void Main(string[] args) {
            /*拓扑测试
            Graph g = new Graph();
            g.AddVertex("A");
            g.AddVertex("B");
            g.AddVertex("C");
            g.AddVertex("D");
            g.AddEdge(0, 1);
            g.AddEdge(1, 2);
            g.AddEdge(2, 3);
            g.AddEdge(3, 4);
            g.TopSort();
            Console.ReadKey();
            */
            

            // 遍历测试
            Graph g = new Graph();
            g.AddVertex("A");
            g.AddVertex("B");
            g.AddVertex("C");
            g.AddVertex("D");
            g.AddVertex("E");
            g.AddVertex("F");
            g.AddVertex("G");
            g.AddEdge(0, 1);
            g.AddEdge(0, 2);
            g.AddEdge(1, 3);
            g.AddEdge(2, 4);
            g.AddEdge(3, 5);
            g.AddEdge(4, 6);
            Console.WriteLine("\n深度优先遍历");
            g.DepthFirstSearch();
            Console.WriteLine("\n广度优先遍历");
            g.BreadthFirstSearch();
            
            Console.ReadKey();
        }
    }
}
