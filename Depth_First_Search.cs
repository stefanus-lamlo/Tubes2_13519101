using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depth_First_Search
{
    class DFS
    {
    
        // Core procedure for DFS
        static void DepthFirstSearchHelp(List<int>[] adjacentList, int idx, ref List<int> output)
        {
            List<int> temp = adjacentList[idx];
            output.Add(idx);

            for (int i = 1; i < temp.Count; i++)
            {
                if (!output.Contains(temp[i]))
                {
                    DepthFirstSearchHelp(adjacentList, temp[i], ref output);
                }
            }


        }

        // Depth First Search, char "user" is the starting node
        // This procedure initiate all components needed for DFS, then call DepthFirstSearchHelp
        static List<string> DepthFirstSearch(List<int>[] adjacentList, List<string> nodes, string user)
        {
            int head = nodes.IndexOf(user);
            List<int> output = new List<int>();

            List<string> outputasli = new List<string>();
            if (head == -1)
            {
                Console.Out.WriteLine("Failed to locate user");
                return outputasli;
            }
            DepthFirstSearchHelp(adjacentList, head, ref output);

            

            foreach(int o in output){
                outputasli.Add(nodes[o]);
            }


            return outputasli;
        }

        // Function to return path of the starting node (user) to "end"
        // Starting node based on output's head (head[0])
        static List<string> getPathDFS(List<int>[] adjacentList, List<string> output, string end, List<string> nodes)
        {
            int endIdx = output.IndexOf(end);

            List<string> path = new List<string>(output.Count);
            if (endIdx == -1)
            {
                Console.Write("Node can't be reached");
                return path;
            }
            // int endIdx = output.FindIndex(i => output[i] == end);
            path.Add(output[endIdx]);
            string curr = end;
            for (int j = endIdx - 1; j >= 0; j--)
            {
                if (adjacentList[nodes.IndexOf(curr)].IndexOf(j) != -1 )
                {
                    curr = output[j];
                    path.Add(output[j]);
                }
            }
            path.Reverse();
            return path;
        }
    }
}
