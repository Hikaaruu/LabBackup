namespace AntAlg
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph(200); //200
            graph.RandomFill(50);  //50
            graph.Prepeare();
            (bool found,double minLen ,List<int> path) = AntSearch.Search(graph, 100, 0, 199);  // 300 0 199
            Console.WriteLine();
            if (found)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("-------------------------------------------------------------------------------");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("result: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                foreach (var item in path)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Length: " + minLen);
                Console.ForegroundColor = ConsoleColor.Gray;
            }

        }
    }
}