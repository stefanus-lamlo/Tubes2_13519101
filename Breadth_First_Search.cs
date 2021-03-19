using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breadth_First_Search
{
    class BFS
    {
    
        // Core procedure for BFS
        static list<string> BreadthFirstSearchHelp(List<int>[] adjacentList, List<string> nodes, string start, start end)
        {
            List<int> output = new List<int>();
            List<int> temp = new List<int>();
            temp = adjacentList[nodes.IndexOf(start)];
            
            output.Add(nodes.IndexOf(start));
            int[] predator = new int[adjacentList.count];
            while(output.Any()){
                int i = output[0];
                output.RemoveFirst();
                temp = adjancentList[i];
                foreach (int val in temp){
                    if (!output.Contains(val)){
                        predator[val] = i;
                        output.AddLast(val)
                    }
                }
            }
            List<string> hasil = new List<string>()
            if(!output.Contains(nodes.IndexOf(end))){
                Console.WriteLine("Failed to locate user");
                return hasil;
            }
            else{
                i = nodes.IndexOf(end);
                hasil.Add(end);
                while(predator[i]!=nodes.IndexOf(start)){
                      hasil.Add(predator[i]);
                      i = predator[i];
                }
                hasil.Add(start);
                hasil.Reverse()
            }
            return hasil;
        }
    }
}