using System;
using System.Collections.Generic;
using System.Text;

namespace Graph {
    class Vertex {
        public string Data; //节点数据
        public bool IsVisited; //是否访问
        public Vertex(string Vertexdata) {
            Data = Vertexdata;
        }
    }
}
