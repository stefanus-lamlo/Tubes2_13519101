using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Algorithm
{
	public class Algorithm
	{

		public static List<string> BreadthFirstSearch(List<int>[] adjacentList, List<string> nodes, string start, string end)
		{
			List<int> output = new List<int>();
			List<int> temp = new List<int>();
			temp = adjacentList[nodes.IndexOf(start)];
			Boolean found = false;
			output.Add(nodes.IndexOf(start));
			int[] predator = new int[nodes.Count];
			for (int i = 0; i < nodes.Count; i++)
			{
				predator[i] = -1;
			}
			bool[] visited = new bool[nodes.Count];
			for (int i = 0; i < nodes.Count; i++)
			{
				visited[i] = false;
			}
			visited[nodes.IndexOf(start)] = true;
			while (output.Count != 0 && !found)
			{
				int i = output[0];
				output.RemoveAt(0);
				temp = adjacentList[i];
				foreach (int val in temp)
				{
					if (!visited[val])
					{
						if (val == nodes.IndexOf(end))
						{
							found = true;
						}
						visited[val] = true;
						predator[val] = i;
						output.Add(val);
					}
				}
			}


			List<string> hasil = new List<string>();
			if (!output.Contains(nodes.IndexOf(end)))
			{
				MessageBox.Show("Failed to locate user");
				return hasil;
			}
			else
			{
				int i = nodes.IndexOf(end);
				hasil.Add(end);
				while (predator[i] != nodes.IndexOf(start))
				{
					hasil.Add(nodes[predator[i]]);
					i = predator[i];
				}
				hasil.Add(start);
				hasil.Reverse();
			}
			return hasil;
		}
		static void DepthFirstSearchHelp(List<int>[] adjacentList, int idx, ref List<int> output)
		{
			List<int> temp = adjacentList[idx];
			output.Add(idx);

			for (int i = 0; i < temp.Count; i++)
			{
				if (!output.Contains(temp[i]))
				{
					DepthFirstSearchHelp(adjacentList, temp[i], ref output);
				}
			}


		}

		// Depth First Search, char "user" is the starting node
		// This procedure initiate all components needed for DFS, then call DepthFirstSearchHelp
		public static List<string> DepthFirstSearch(List<int>[] adjacentList, List<string> nodes, string user)
		{
			int head = nodes.IndexOf(user);
			List<int> output = new List<int>();

			List<string> outputasli = new List<string>();
			if (head == -1)
			{
				MessageBox.Show("Failed to locate user");
				return outputasli;
			}
			DepthFirstSearchHelp(adjacentList, head, ref output);



			foreach (int o in output)
			{
				outputasli.Add(nodes[o]);
			}


			return outputasli;
		}

		// Function to return path of the starting node (user) to "end"
		// Starting node based on output's head (head[0])
		public static List<string> getPathDFS(List<int>[] adjacentList, List<string> output, string end, List<string> nodes)
		{
			int endIdx = output.IndexOf(end);

			List<string> path = new List<string>(output.Count);
			if (endIdx == -1)
			{

				return path;
			}
			// int endIdx = output.FindIndex(i => output[i] == end);
			path.Add(output[endIdx]);
			string curr = end;
			for (int j = endIdx - 1; j >= 0; j--)
			{
				if (adjacentList[nodes.IndexOf(curr)].IndexOf(nodes.IndexOf(output[j])) != -1)
				{
					curr = output[j];
					path.Add(output[j]);
				}
			}
			path.Reverse();
			return path;
		}
		static void MutualFriends(List<int>[] adjacentList, List<string> nodes, string node1, string node2, Label l4)
		{
			//TETEP PAKE INI SOALNYA NYAMAN UNTUK DIBACA
			foreach (int a in adjacentList[nodes.IndexOf(node1)])
			{
				foreach (int b in adjacentList[nodes.IndexOf(node2)])
				{
					if (a == b)
					{
						l4.Text += nodes[a] +"\n";
					}
				}
			}

		}
		public static void FriendRecommendation(List<int>[] adjacentList, List<string> nodes, string node, Label l4)
		{
			Dictionary<int, int> tabOfFreqMutual = new Dictionary<int, int>();
			int indeks = nodes.IndexOf(node);
			//PAKAI INI AJA SOALNYA INDAH UNTUK DIBACA

			foreach (int a in adjacentList[indeks])
			{
				foreach (int b in adjacentList[a])
				{
					if (b != indeks && adjacentList[indeks].IndexOf(b) == -1)
					{
						if (tabOfFreqMutual.ContainsKey(b))
						{
							tabOfFreqMutual[b]++;
						}
						else
						{
							tabOfFreqMutual.Add(b, 1);
						}
					}

				}

			}


			List<KeyValuePair<int, int>> sortedTable = tabOfFreqMutual.ToList();
			sortedTable.Sort((y, x) => x.Value.CompareTo(y.Value));
			l4.Text += "Daftar rekomendasi teman untuk akun " + node + ":\n";


			foreach (KeyValuePair<int, int> a in sortedTable)
			{
				l4.Text += "Nama akun: " + nodes[a.Key] + "\n";
				l4.Text += a.Value + " mutual friends:\n";
				MutualFriends(adjacentList, nodes, node, nodes[a.Key], l4);
				l4.Text += "\n";
			}


		}
		/*
		static void Main(string[] args)
		{
			string[] edge;
			if (args.Length == 0)
			{
				string namaFile = Console.ReadLine();
				edge = System.IO.File.ReadAllLines(namaFile); //ini buat nama file yang diinput di dalem program
			}
			else
			{
				edge = System.IO.File.ReadAllLines(args[0]); //ini khusus buat aku soalnya males nulis hehe
			}
			List<string> nodes = new List<string>();
			foreach (string line in edge)
			{
				if (line != edge[0])
				{
					int space = line.IndexOf(" ");
					string elm1 = line.Substring(0, space);
					string elm2 = line.Substring(space + 1);
					if (nodes.IndexOf(elm1) == -1)
					{ //kalau belum ada di daftar nodes, tambahin
						nodes.Add(elm1);
					}
					if (nodes.IndexOf(elm2) == -1)
					{ //kalau belum ada tambahin juga
						nodes.Add(elm2);
					}
				}

			}
			adjacentList = new List<int>[nodes.Count]; //sebenernya array of list string bagus soalnya c# punya banyak method buat nanganin pencarian string tapi lebih nyaman pake int
																	//kalau pake int ya konsekuensinya harus make referensi indexof dari nodes
																	//misal pengen akses adjacentlist dari node "A", nah cara aksesnya yaitu adjacentlist[nodes.IndexOf("A")]
																	//nanti isinya indeks dari nodes mana aja yang bertetanggaan sama A

			for (int i = 0; i < nodes.Count; i++)
			{
				adjacentList[i] = new List<int>();
			}
			foreach (string line in edge)
			{
				if (line != edge[0])
				{
					int space = line.IndexOf(" ");
					string elm1 = line.Substring(0, space);
					string elm2 = line.Substring(space + 1);
					adjacentList[nodes.IndexOf(elm1)].Add(nodes.IndexOf(elm2)); //adjacentlist[elm1] ditambahin index elm2 di list nodesnya
																				//misal ada edge A B, maka dan nodes isinya {A, B} maka adjacentlist[0].Add(1)
																				// 0 didapet dari indeksnya nodes yang isinya "A" dan 1 dari indeksnya node yang isinya "B"
					adjacentList[nodes.IndexOf(elm2)].Add(nodes.IndexOf(elm1)); //masing masing dimasukkin ke list adjacency tetangganya
				}

			}
			for (int i = 0; i < nodes.Count; i++)
			{
				adjacentList[i].Sort((x, y) => nodes[x].CompareTo(nodes[y]));
				//sort boy
			}
			for (int i = 0; i < nodes.Count; i++)
			{
				Console.WriteLine("Adjacent node dari node {0} adalah", nodes[i]);
				foreach (int b in adjacentList[i])
				{
					Console.Write(" {0}", nodes[b]);
				}
				Console.WriteLine("");
				//ini buat ngecek aja bener apa gak.
			}

			List<string> tes = new List<string>();
			tes = DepthFirstSearch(adjacentList, nodes, "A");

			List<string> testing = new List<string>();
			testing = getPathDFS(adjacentList, tes, "H", nodes);

			tes = BreadthFirstSearch(adjacentList, nodes, "A", "H");
			foreach (string s in tes)
			{
				Console.WriteLine(s);
			}

			// foreach(string s in tes){
			// 	Console.WriteLine(s);
			// }

			foreach (string s in testing)
			{
				Console.WriteLine(s);
			}

			FriendRecommendation(adjacentList, nodes, "E");


		} */
	}


}
