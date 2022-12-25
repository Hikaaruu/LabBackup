using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring
{
    public class GreedyColoring
    {


        public static List<int>? Start(Graph graph, int maxClrCount)
        {
            List<int> colors = new List<int>();
            for (int i = 0; i < graph.Count; i++)
            {
                colors.Add(-1);
            }

            for (int i = 0; i < graph.Count; i++)
            {
                List<int> adjColors = new List<int>();
                List<int>? adj = graph.GetAdjacent(i);
                foreach (var a in adj)
                {
                    adjColors.Add(colors[a]);
                }


                bool success = false;
                for (int j = 0; j < maxClrCount; j++)
                {
                    if (!adjColors.Contains(j))
                    {
                        colors[i] = j;
                        success = true;
                        break;
                    }
                }

                if (!success)
                {
                    return null;
                }
            }

            return colors;

           
        }
    }
}
