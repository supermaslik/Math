using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMod_D_A
{
    static class DijkstraAlgorithm
    {
        static readonly int unsettedPrice = int.MaxValue;
        static int[,] _matrix;
        static int _startPoint;
        static int _endPoint;

        private static void DefineElements(int[,] matrix, int startPoint, int endPoint)
        {
            _matrix = matrix;
            _startPoint = startPoint;
            _endPoint = endPoint;
        }
        static public void FindShortestWayByMatrixDesirialize(int[,] matrix, int startPoint, int endPoint)
        {
            DefineElements(matrix, startPoint, endPoint);
            CheckStartAndEndPoints();
            _startPoint = (--startPoint);
            _endPoint = (--endPoint);

            List<int> unsettedNodes = new List<int>();
            for (int i = 0; i < matrix.GetLength(0); i++)
                unsettedNodes.Add(i);

            int[] previousNode = new int[matrix.GetLength(0)];
            SetValieToAllElementsOfArray(previousNode, startPoint);
            int[] reachNodeCost = new int[matrix.GetLength(0)];
            SetValieToAllElementsOfArray(reachNodeCost, int.MaxValue);
            reachNodeCost[startPoint] = 0;

            SearchOneRow(previousNode, reachNodeCost, startPoint);
            unsettedNodes.Remove(startPoint);

            while (unsettedNodes.Count() != 0)
            {
                int row = FindMinRow(reachNodeCost, unsettedNodes);
                SearchOneRow(previousNode, reachNodeCost, row);
                unsettedNodes.Remove(row);
            }

            Console.WriteLine("Сперва цена потом пути");
            GlobalFunctions.ShowArray<int>(reachNodeCost);
            GlobalFunctions.ShowArray<int>(previousNode);

        }

        private static int FindMinRow(int[] reachNodeCost, List<int> unsettedNodes)
        {
            int row = -1;
            int minCost = int.MaxValue;
            for(int i = 0; i < unsettedNodes.Count(); i++)
            {
                if(reachNodeCost[unsettedNodes[i]] < minCost)
                {
                    minCost = reachNodeCost[unsettedNodes[i]];
                    row = unsettedNodes[i];
                }
            }
            return row;
        }

        private static void SetValieToAllElementsOfArray(int[] array, int value)
        {
            for (int i = 0; i < array.GetLength(0); i++)
                array[i] = value;
        }

        private static void SearchOneRow(int[] previousNode, int[] reachNodeCost, int row)
        {
            for(int i = 0; i < _matrix.GetLength(0);i++)
            {
                if((_matrix[row,i] + reachNodeCost[row]) < reachNodeCost[i])
                {
                    previousNode[i] = row;
                    reachNodeCost[i] = _matrix[row, i] + reachNodeCost[row];
                }
            }
        }

        static public void FindShortestWay(int[,] matrix, int startPoint, int endPoint)
        {
            DefineElements(matrix, startPoint, endPoint);
            CheckStartAndEndPoints();
            _startPoint = (--startPoint);
            _endPoint = (--endPoint);
            List<Node> existingNodes = new List<Node>();


            for (int i = 0; i < matrix.GetLength(0); i++)
                existingNodes.Add(new Node() { Id = i });

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.WriteLine($"Node {i}");
                existingNodes[i].Price = unsettedPrice;
                existingNodes[i].Checked = false;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i == j)
                        continue;
                    if (matrix[i, j] > -1)
                        existingNodes[i].NodeYouMayGo.Add(existingNodes[j]);
                }
                Console.WriteLine("Node you may go:");
                foreach (var node in existingNodes[i].NodeYouMayGo)
                    Console.Write($"{node.Id}");
                Console.WriteLine("-----");

            }

            //тут уже определены node's

                existingNodes[startPoint].Price = 0;
                foreach(var Node in existingNodes[startPoint].NodeYouMayGo)
                {
                    Node.Price = matrix[startPoint, Node.Id.Value];
                Node.backNode = existingNodes[startPoint];
                }
            existingNodes[startPoint].NodeYouMayGo = null;

            //выставил цены из начальной точки
            existingNodes[startPoint].Checked = true;
                foreach (var Node in existingNodes)
                    Console.WriteLine(Node.Price);

            List<Node> trackedAndUncheckedNodes = existingNodes.Where(x => x.Price != unsettedPrice && x.Checked != true).ToList();
            //так нащёл те, откуда можно идти, далее найду минимальное расстояние

            Node nodeWithLowestCostToGo = FindLowestConstToGoNode(trackedAndUncheckedNodes, startPoint);

            if (nodeWithLowestCostToGo.NodeYouMayGo.Count == 0)
            {
                nodeWithLowestCostToGo.Checked = true;
                nodeWithLowestCostToGo.Price = matrix[startPoint, nodeWithLowestCostToGo.Id.Value];
            }
        }

        private static Node FindLowestConstToGoNode(List<Node> trackedAndUncheckedNodes, int nodeId)
        {
            int lowestPrice = int.MaxValue;
            Node nodeToReturn = null;
            for(int i = 0; i < trackedAndUncheckedNodes.Count; i++)
            {
                if(_matrix[nodeId,trackedAndUncheckedNodes[i].Id.Value] < lowestPrice)
                {
                    lowestPrice = _matrix[nodeId, trackedAndUncheckedNodes[i].Id.Value];
                    nodeToReturn = trackedAndUncheckedNodes[i];
                }
            }
            return nodeToReturn;
        }

        static private void CheckStartAndEndPoints()
        {
            if (_startPoint > _matrix.GetLength(0) || _endPoint > _matrix.GetLength(0) ||
                _startPoint < 1 || _endPoint < 1)
                GlobalFunctions.Error("Error points declaration");
        }
    }
}
