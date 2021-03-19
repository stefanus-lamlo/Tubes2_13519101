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
                    DepthFirstSearchHelp(adjacentList, adjacentList[i], ref output);
                }
            }


        }

        // Depth First Search, char "user" is the starting node
        // This procedure initiate all components needed for DFS, then call DepthFirstSearchHelp
        static List<string> DepthFirstSearch(List<int>[] adjacentList, List<string> nodes, string user)
        {
            int head = nodes.indexof(user);
            List<int> output = new List<int>();

            if (head == -1)
            {
                Console.Out.WriteLine("Failed to locate user");
                return output;
            }
            DepthFirstSearchHelp(graph, head, ref output);

            List<string> out = new List<string>();
            foreach(int o: output){
                out.Add(nodes[o]);
            }


            return out;
        }

        // Function to determine whether "a" has connection (edge) to "b" or not
        static bool isEdge(List<char>[] graph, char a, char b)
        {
            // Returns true if b is an edge of a
            foreach (char c in graph[getIdx(graph, a)])
            {
                if (c == b)
                {
                    return true;
                }
            }
            return false;
        }

        // Function to return path of the starting node (user) to "end"
        // Starting node based on output's head (head[0])
        static List<char> getPathDFS(List<char>[] graph, List<char> output, char end)
        {
            int endIdx = -1;
            for (int i = 0; i < output.Count; i++)
            {
                if (output[i] == end)
                {
                    endIdx = i;
                    break;
                }
            }
            List<char> path = new List<char>(output.Count);
            if (endIdx == -1)
            {
                Console.Write("Node can't be reached");
                return path;
            }
            // int endIdx = output.FindIndex(i => output[i] == end);

            path.Add(output[endIdx]);
            char curr = end;
            for (int j = endIdx - 1; j >= 0; j--)
            {
                if (isEdge(graph, output[j], curr))
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
