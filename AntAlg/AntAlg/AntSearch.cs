using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntAlg
{
    public class AntSearch
    {

        private static readonly int alpha = 3; //3
        private static readonly int beta = 4;  //4
        private static readonly double P = 0.1; //0.1
        private static readonly double Q = 10; //10

        private static double[,] LL;
        private static List<int> minPath;
        private static double minLength = double.MaxValue;

        public static (bool,double,List<int>) Search(Graph graph, int antCount, int startPoint, int endPoint)
        {
            List<int> path = new List<int>();
            LL = new double[graph.Count, graph.Count];
            for (int i = 0; i < graph.Count; i++)
            {
                for (int j = 0; j < graph.Count; j++)
                {
                    LL[i, j] = 0;
                }
            }
            Random rnd = new Random();
            bool found = false;
            for (int j = 0; j < antCount; j++)
            {
                (found,path) = FindPath(graph, startPoint, endPoint);
                double len = GetLength(graph, path);
                if (!found)
                {
                    Console.WriteLine("path doest exist");
                    return (false,minLength, path);
                }
                if (len<minLength)
                {
                    minLength = len;
                    minPath= path;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Ant number: " + j +  "      Current best path:");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    foreach (var item in minPath)
                    {
                        Console.Write(item + " ");
                    }
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Length: " + minLength);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                
                UpdatePheramone(graph, path);

            }



            return (true,minLength, minPath);
        }

        private static (bool,List<int>) FindPath(Graph graph, int startPoint, int endPoint)
        {
            Dictionary<int, double> probabilities = new Dictionary<int, double>();
            List<int> visited = new List<int>
            {
                startPoint
            };
            List<int> path = new List<int>
            {
                startPoint
            };

            bool found = false;
            int current = startPoint;
            while (!found && visited.Distinct().Count() < graph.Count && visited.Count < graph.Count * 4)
            {
                probabilities.Clear();
                List<int> adj = graph.GetAdjacent(current);
                adj = adj.Except(visited).ToList();
                if (adj.Count == 0)
                {
                    adj = graph.GetAdjacent(current);
                }
                double sum = 0;
                foreach (var a in adj)
                {
                    double ph = Math.Pow(graph.Pheromones[current, a], alpha);
                    double ds = Math.Pow(1 / graph.Distance[current, a], beta);
                    sum += ph * ds;
                }

                foreach (var a in adj)
                {
                    double propability = (Math.Pow(graph.Pheromones[current, a], alpha) * Math.Pow(1 / graph.Distance[current, a], beta)) / sum;
                    probabilities.Add(a, propability * 100);
                }
                probabilities = probabilities.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                int nextNode = GetNextNode(probabilities);

                visited.Add(nextNode);

                path.Add(nextNode);
                if (nextNode == endPoint)
                {
                    found = true;
                }

                current = nextNode;
            }
            return (found,path);

        }

        private static int GetNextNode(Dictionary<int, double> probabilities)
        {
            var rnd = new Random();
            int value = rnd.Next(0, 1000000);
            double val = (double)value / 10000;
            double current = 0;
            foreach (KeyValuePair<int, double> pair in probabilities)
            {
                current += pair.Value;
                if (val < current)
                {
                    return pair.Key;
                }
            }

            return probabilities.Last().Key;
        }

        private static void UpdatePheramone(Graph graph, List<int> path)
        {
            List<(int, int)> visited = new List<(int, int)>();
            double length = GetLength(graph, path);
            for (int i = 0; i < path.Count - 1; i++)
            {
                (int, int) edge = (path[i], path[i + 1]);
                visited.Add(edge);
                double temp = Q / length;
                LL[path[i], path[i + 1]] += temp;
                graph.Pheromones[path[i], path[i + 1]] = (1 - P) * graph.Pheromones[path[i], path[i + 1]] + LL[path[i], path[i + 1]];
            }

            for (int i = 0; i < graph.Count; i++)
            {
                for (int j = 0; j < graph.Count; j++)
                {
                    if ((bool)graph.IsAdjacent(i, j) && !visited.Contains((i, j)))
                    {
                        graph.Pheromones[i, j] = (1 - P) * graph.Pheromones[i, j] + LL[i, j];
                    }
                }
            }
        }

        private static double GetLength(Graph graph, List<int> path)
        {
            double length = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                length += graph.Distance[path[i], path[i + 1]];
            }
            return length;
        }

    }
}
