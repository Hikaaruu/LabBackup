//200 vertex, degree 1-30, 40 bea, 2 - scouts 
namespace GraphColoring
{
    class Program
    {
        public static void Test()
        {
            Console.WriteLine("Enter vertex count and max degree");
            string b = Console.ReadLine();
            Console.WriteLine();
            int vertex = int.Parse(b.Split(' ')[0]);
            int maxDegInp = int.Parse(b.Split(' ')[1]);

            Graph graph = new Graph(vertex);
            graph.RandomFill(maxDegInp);

            for (int i = 0; i < graph.Count; i++)
            {
                for (int j = 0; j < graph.Count; j++)
                {
                    Console.Write(graph.Adjacent[i, j] + " ");
                }
                Console.WriteLine();
            }
            bool coloring = true;

            List<int> colors = GreedyColoring.Start(graph, graph.Count);

            Console.WriteLine();

            foreach (var item in colors)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

            int maxDeg = 0;
            int minDeg = graph.Count;
            int count;
            bool semetric = true;
            bool own = false;
            for (int i = 0; i < graph.Count; i++)
            {
                count = 0;
                for (int j = 0; j < graph.Count; j++)
                {
                    if ((bool)graph.IsAdjacent(i, j))
                    {
                        count++;
                    }
                    if (graph.Adjacent[i, j] != graph.Adjacent[j, i])
                    {
                        semetric = false;
                    }
                    if (i == j && graph.Adjacent[i, j] == 1)
                    {
                        own = true;
                    }
                }
                if (count < minDeg)
                {
                    minDeg = count;
                }
                if (count > maxDeg)
                {
                    maxDeg = count;
                }
            }



            for (int i = 0; i < graph.Count; i++)
            {
                List<int> adj = graph.GetAdjacent(i);
                foreach (var a in adj)
                {
                    if (colors[i] == colors[a])
                    {
                        coloring = false;
                    }
                }
            }

            if (maxDeg <= maxDegInp)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Test passed.   Max degree <= " + maxDegInp + " (" + maxDeg + ")");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Test failed.   Max degree > " + maxDegInp + " (" + maxDeg + ")");
            }

            if (minDeg >= 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Test passed.   Min degree > 0 " + " (" + minDeg + ")");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Test failed.   Min degree = " + minDeg);
            }

            if (semetric)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Test passed.   Graph is semetric");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Test failed.  Graph is not semetric");
            }

            if (!own)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Test passed.   There are no edges between same vertex");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Test failed.  There are edges between same vertex");
            }


            if (coloring)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Test passed.   Coloring is correct");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Test failed. Coloring is not correct");
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void Main(string[] args)
        {
            Graph graph = new Graph(200);
            graph.RandomFill(30);
            List<int> colors = GreedyColoring.Start(graph, graph.Count);
            List<int> GreedyColors = new List<int>();
            foreach (var item in colors)
            {
                GreedyColors.Add(item);
            }
            List<int> newClr = BeaAlg.Start(graph, colors, 38, 100);

            bool coloring = true;
            for (int i = 0; i < graph.Count; i++)
            {
                foreach (var adj in graph.GetAdjacent(i))
                {
                    if (newClr[i] == newClr[adj])
                    {
                        coloring = false;
                    }
                }
            }

            if (coloring)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Coloring are correct");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Coloring are incorrect");
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            Console.WriteLine();
            Console.WriteLine("Greedy colors: ");
            foreach (var clr in GreedyColors)
            {
                Console.Write(clr + " ");
            }
            Console.WriteLine();
            Console.WriteLine("colors count = " + GreedyColors.Distinct().Count());

            Console.WriteLine();
            Console.WriteLine("Bea colors: ");
            foreach (var clr in newClr)
            {
                Console.Write(clr + " ");
            }
            Console.WriteLine();
            Console.WriteLine("colors count = " + newClr.Distinct().Count());
            Console.WriteLine();
            bool equal = true;
            for (int i = 0; i < GreedyColors.Count; i++)
            {
                if (GreedyColors[i] != newClr[i])
                {
                    equal = false;
                    break;
                }
            }
            Console.WriteLine("Lists equal - "+ equal);
            
        }
    }
}