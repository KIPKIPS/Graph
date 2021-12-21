using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Graph {
    class Graph {
        //顶点数组
        //图中所能包含的点上限
        private const int Number = 10;
        //顶点数组
        private Vertex[] vertiexes;
        //邻接矩阵
        public int[,] adjmatrix;
        //统计当前图中有几个点
        int numVerts = 0;
        //初始化图
        public Graph() {
            //初始化邻接矩阵和顶点数组
            adjmatrix = new int[Number, Number];
            vertiexes = new Vertex[Number];
            //将代表邻接矩阵的表全初始化为0
            for (int i = 0; i < Number; i++) {
                for (int j = 0; j < Number; j++) {
                    adjmatrix[i, j] = 0;
                }
            }
        }

        //向图中添加节点
        public void AddVertex(string v) {
            vertiexes[numVerts] = new Vertex(v);
            numVerts++;
        }
        //向图中添加有向边
        public void AddEdge(int vertex1, int vertex2) {
            adjmatrix[vertex1, vertex2] = 1;
            //adjmatrix[vertex2, vertex1] = 1;
        }
        //显示点
        public void DisplayVert(int vertexPosition) {
            Console.Write(vertiexes[vertexPosition].Data + " ");
        }
        //寻找图中没有后继节点的点
        //具体表现为邻接矩阵中某一列全为0
        //此时返回行号，如果找不到返回-1
        private int FindNoSuccessor() {
            bool isEdge;
            //循环行
            for (int i = 0; i < numVerts; i++) {
                isEdge = false;
                //循环列，有一个1就跳出循环
                for (int j = 0; j < numVerts; j++) {
                    if (adjmatrix[i, j] == 1) {
                        isEdge = true;
                        break;
                    }
                }
                if (!isEdge) {
                    return i;
                }
            }
            return -1;
        }
        //删除图中的点
        //需要两个操作，分别从数组中删除点
        //从邻接矩阵中消去被删点的行和列
        private void DelVertex(int vert) {
            //保证不越界
            if (vert <= numVerts - 1) {
                //删除节点
                for (int i = vert; i < numVerts; i++) {
                    vertiexes[i] = vertiexes[i + 1];
                }
                //删除邻接矩阵的行
                for (int j = vert; j < numVerts; j++) {
                    MoveRow(j, numVerts);
                }
                //删除邻接矩阵中的列，因为已经删了行，所以少一列
                for (int k = vert; k < numVerts - 1; k++) {
                    MoveCol(k, numVerts - 1);
                }
                //删除后减少一个
                numVerts--;
            }
        }
        //辅助方法，移动邻接矩阵中的行
        private void MoveRow(int row, int length) {
            for (int col = row; col < numVerts; col++) {
                adjmatrix[row, col] = adjmatrix[row + 1, col];
            }
        }
        //辅助方法，移动邻接矩阵中的列
        private void MoveCol(int col, int length) {
            for (int row = col; row < numVerts; row++) {
                adjmatrix[row, col] = adjmatrix[row, col + 1];
            }
        }
        //拓扑排序
        //找到没有后继节点的节点，删除，加入一个栈，然后输出
        public void TopSort() {
            int origVerts = numVerts;
            //存放返回节点的栈
            Stack result = new Stack();
            while (numVerts > 0) {
                //找到第一个没有后继节点的节点
                int currVertex = FindNoSuccessor();
                if (currVertex == -1) {
                    Console.WriteLine("图为环路图无法进行拓扑排序");
                    return;
                }
                //如果找到，将其加入返回结果栈
                result.Push(vertiexes[currVertex].Data);
                //然后删除此节点
                DelVertex(currVertex);
            }
            //输出排序后的结果
            Console.Write("拓扑排序的顺序为:");
            while (result.Count > 0) {
                Console.Write(result.Pop() + " ");
            }
            //输出排序后的结果
        }

        //从邻接矩阵查找给定点第一个相邻且未被访问过的点
        //参数v是给定点在邻接矩阵的行
        private int GetAdjUnvisitedVertex(int v) {
            for (int j = 0; j < numVerts; j++) {
                if (adjmatrix[v, j] == 1 && vertiexes[j].IsVisited == false) {
                    return j;
                }
            }
            return -1;
        }
        //深度优先遍历
        public void DepthFirstSearch() {
            //声明一个存储临时结果的栈
            Stack s = new Stack();
            //先访问第一个节点
            vertiexes[0].IsVisited = true;
            DisplayVert(0);
            s.Push(0);
            int v;
            while (s.Count > 0) {
                //获得和当前节点连接的未访问过节点的序号
                v = GetAdjUnvisitedVertex((int)s.Peek());
                if (v == -1) {
                    s.Pop();
                } else {
                    //标记为已经被访问过
                    vertiexes[v].IsVisited = true;
                    DisplayVert(v);
                    s.Push(v);
                }
            }
            //重置所有节点为未访问过
            for (int u = 0; u < numVerts; u++){
                vertiexes[u].IsVisited = false;
            }
        }
        //广度优先遍历
        public void BreadthFirstSearch(){
            Queue q = new Queue();
            //首先访问第一个节点
            vertiexes[0].IsVisited = true;
            DisplayVert(0);
            q.Enqueue(0);
            //第一个节点访问结束

            int vert1, vert2;
            while (q.Count > 0){
                //首先访问同层级第一个节点
                vert1 = (int)q.Dequeue();
                vert2 = GetAdjUnvisitedVertex(vert1);
                //结束

                while (vert2 != -1){
                    //首先访问第二个节点
                    vertiexes[vert2].IsVisited = true;
                    DisplayVert(vert2);
                    q.Enqueue(vert2);
                    //寻找邻接的
                    vert2 = GetAdjUnvisitedVertex(vert1);
                }
            }
            //重置所有节点为未访问过
            for (int u = 0; u < numVerts; u++){
                vertiexes[u].IsVisited = false;
            }
        }
    }
}
