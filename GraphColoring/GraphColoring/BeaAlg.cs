using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring
{
    public class BeaAlg
    {
        class Vertex
        {
            public int VertexNum { get; set; }
            public int Degree { get; set; }

            public Vertex(int num, int deg)
            {
                VertexNum = num;
                Degree = deg;
            }

            public static bool operator ==(Vertex left, Vertex right)
            {
                return (left.VertexNum == right.VertexNum);
            }

            public static bool operator !=(Vertex left, Vertex right)
            {
                return !(left.VertexNum == left.VertexNum);
            }
        }

        public static List<int> Start(Graph graph, List<int> colors, int furCount, int iter)
        {
            var rnd = new Random();
            int ranNum;
            int halfPrior;
            int halfRan;
            List<int> randomAdj;
            List<int> priorAdj;
            List<int> randomNums;
            List<int> priorNums;
            List<Vertex> priority = new List<Vertex>();            
            for (int i = 0; i < graph.Count; i++)
            {
                priority.Add(new Vertex(i, graph.GetAdjacent(i).Count));
            }
            priority.Sort((p, q) => p.Degree.CompareTo(q.Degree));
            priority.Reverse();

            for (int y = 0; y < iter; y++)
            {
                for (int i = 0; i < graph.Count; i++)
                {
                    do
                    {
                        ranNum = rnd.Next(0, graph.Count);
                    } while (ranNum == priority.First().VertexNum);

                    Vertex prior = priority.First();
                    Vertex rand = new Vertex(ranNum, graph.GetAdjacent(ranNum).Count);
                    priority.RemoveAll(x => x.VertexNum == prior.VertexNum);
                    priority.RemoveAll(x => x.VertexNum == rand.VertexNum);
                    priority.Add(prior);
                    priority.Add(rand);
                    halfPrior = furCount - furCount / 2;
                    halfRan = furCount / 2;

                    if (prior.Degree < halfPrior)
                    {
                        halfRan += halfPrior - prior.Degree;
                        halfPrior = prior.Degree;
                    }

                    if (rand.Degree < halfRan)
                    {
                        halfPrior += halfRan - rand.Degree;
                        halfRan = rand.Degree;
                    }

                    priorAdj = graph.GetAdjacent(prior.VertexNum);
                    randomAdj = graph.GetAdjacent(rand.VertexNum);

                    priorNums = new List<int>();
                    randomNums = new List<int>();

                    while (halfPrior > 0 && priorNums.Count < priorAdj.Count)
                    {
                        int index = rnd.Next(0, priorAdj.Count);
                        if (!priorNums.Contains(priorAdj[index]))
                        {
                            priorNums.Add(priorAdj[index]);
                            halfPrior--;
                        }
                    }


                    while (halfRan > 0 && randomNums.Count < randomAdj.Count)
                    {
                        int index = rnd.Next(0, randomAdj.Count);
                        if (!randomNums.Contains(randomAdj[index]))
                        {
                            randomNums.Add(randomAdj[index]);
                            halfRan--;
                        }
                    }


                    foreach (var ver in priorNums)
                    {
                        bool possible = true;
                        int priorClr = colors[prior.VertexNum];
                        int testClr;
                        int verClr = colors[ver];

                        foreach (var adj in priorAdj)
                        {
                            if (adj == ver)
                            {
                                continue;
                            }
                            if (colors[adj] == verClr)
                            {
                                possible = false;
                            }
                        }

                        foreach (var adj in graph.GetAdjacent(ver))
                        {
                            if (adj == prior.VertexNum)
                            {
                                continue;
                            }

                            if (colors[adj] == priorClr)
                            {
                                possible = false;
                            }
                        }

                        if (!possible)
                        {
                            continue;
                        }

                        colors[ver] = priorClr;
                        colors[prior.VertexNum] = verClr;


                        List<int> uniqClr = colors.Distinct().ToList();

                        uniqClr.Sort();






                        int k = 0;
                        while (k < colors[ver])
                        {
                            testClr = uniqClr[k];
                            bool noConflict = true;
                            foreach (var adj in graph.GetAdjacent(ver))
                            {
                                if (testClr == colors[adj])
                                {
                                    noConflict = false;
                                }
                            }
                            if (noConflict)
                            {
                                colors[ver] = testClr;
                                break;
                            }
                            k++;
                        }





                        k = 0;
                        while (k < colors[prior.VertexNum])
                        {
                            testClr = uniqClr[k];
                            bool noConflict = true;
                            foreach (var adj in priorAdj)
                            {
                                if (testClr == colors[adj])
                                {
                                    noConflict = false;
                                }
                            }
                            if (noConflict)
                            {
                                colors[prior.VertexNum] = testClr;
                                break;
                            }
                            k++;
                        }




                    }


                    foreach (var ver in randomNums)
                    {
                        bool possible = true;
                        int priorClr = colors[rand.VertexNum];
                        int testClr;
                        int verClr = colors[ver];

                        foreach (var adj in randomAdj)
                        {
                            if (adj == ver)
                            {
                                continue;
                            }
                            if (colors[adj] == verClr)
                            {
                                possible = false;
                            }
                        }

                        foreach (var adj in graph.GetAdjacent(ver))
                        {
                            if (adj == prior.VertexNum)
                            {
                                continue;
                            }
                            if (colors[adj] == priorClr)
                            {
                                possible = false;
                            }
                        }

                        if (!possible)
                        {
                            continue;
                        }

                        colors[ver] = priorClr;
                        colors[rand.VertexNum] = verClr;


                        List<int> uniqClr = colors.Distinct().ToList();

                        uniqClr.Sort();






                        int k = 0;
                        while (k < colors[ver])
                        {
                            testClr = uniqClr[k];
                            bool noConflict = true;
                            foreach (var adj in graph.GetAdjacent(ver))
                            {
                                if (testClr == colors[adj])
                                {
                                    noConflict = false;
                                }
                            }
                            if (noConflict)
                            {
                                colors[ver] = testClr;
                                break;
                            }
                            k++;
                        }





                        k = 0;
                        while (k < colors[rand.VertexNum])
                        {
                            testClr = uniqClr[k];
                            bool noConflict = true;
                            foreach (var adj in randomAdj)
                            {
                                if (testClr == colors[adj])
                                {
                                    noConflict = false;
                                }
                            }
                            if (noConflict)
                            {
                                colors[rand.VertexNum] = testClr;
                                break;
                            }
                            k++;
                        }




                    }

                }
                priority.Sort((p, q) => p.Degree.CompareTo(q.Degree));
                priority.Reverse();
            }

            

            return colors;
        }
    }
}
