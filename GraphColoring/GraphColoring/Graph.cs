using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring
{
    public class Graph
    {
        public int[,] Adjacent { get; private set; }
        public int Count { get; private set; }

        public Graph(int count)
        {
            Count = count;
            Adjacent = new int[count, count];
        }


        public void AddEdge(int i, int j)
        {
            if (i < 0 || i >= Count || j < 0 || j >= Count)
            {
                return;
            }
            else
            {
                Adjacent[i, j] = 1;
                Adjacent[j, i] = 1;
            }
        }

        public bool? IsAdjacent(int i, int j)
        {
            if (i < 0 || i >= Count || j < 0 || j >= Count)
            {
                return null;
            }
            else
            {
                return Adjacent[i, j] == 1;
            }


        }

        public List<int>? GetAdjacent(int i)
        {
            if (i < 0 || i >= Count)
            {
                return null;
            }
            else
            {
                List<int>? result = new List<int>();
                for (int j = 0; j < Count; j++)
                {
                    if (Adjacent[i, j] == 1)
                    {
                        result.Add(j);
                    }
                }
                return result;
            }
        }

        public void RandomFill(int maxDegree)
        {
            if (maxDegree > Count)
            {
                return;
            }
            Random rnd = new Random();
            int[] aval = new int[Count];
            for (int i = 0; i < Count; i++)
            {
                aval[i] = maxDegree;
            }

            for (int i = 0; i < Count; i++)
            {
                int random = -1;
                if (GetAdjacent(i).Count > 0)
                {
                    continue;
                }
                else
                {

                    do
                    {
                        random = rnd.Next(0, Count);
                    } while (aval[random] <= 0 || random == i);


                    AddEdge(i, random);
                    aval[random]--;
                    aval[i]--;
                }

            }



            for (int i = 0; i < Count; i++)
            {
                int degree = rnd.Next(1, maxDegree + 1);
                if (GetAdjacent(i).Count>=degree)
                {
                    continue;
                }
                List<int> numbers = new List<int>();
                List<int>? adj = GetAdjacent(i);

                List<int> avaliableNums = new List<int>();
                for (int k = 0; k < Count; k++)
                {
                    if (aval[k]>0 && k!=i)
                    {
                        avaliableNums.Add(k);
                    }
                }
                var disAvaliableNums = avaliableNums.Except(adj);

                if (disAvaliableNums.Count() >= (degree - adj.Count))
                {
                    while (numbers.Count < degree - adj.Count)
                    {
                        int random = rnd.Next(0, disAvaliableNums.Count());
                        if (!numbers.Contains(disAvaliableNums.ElementAt(random)))
                        {
                            numbers.Add(disAvaliableNums.ElementAt(random));
                        }
                    }
                }
                else
                {
                    while (numbers.Count < disAvaliableNums.Count())
                    {
                        int random = rnd.Next(0, disAvaliableNums.Count());
                        if (!numbers.Contains(disAvaliableNums.ElementAt(random)))
                        {
                            numbers.Add(disAvaliableNums.ElementAt(random));
                        }
                    }
                }


                foreach (int number in numbers)
                {
                    AddEdge(i, number);
                    aval[number]--;
                    aval[i]--;
                }

            }
        }

    }
}
