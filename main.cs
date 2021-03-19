using System;
using System.Collections.Generic;
public class main
{
	static void  Main(string[] args)
	{
		// string test = "halo bro";
		// string dummy1 = test.Substring(0, test.IndexOf(' '));
		// string dummy2 = test.Substring(test.IndexOf(' ')+1);
		// Console.WriteLine(dummy1);
		// Console.WriteLine(dummy2);
		// Console.WriteLine(args.Length);
		// Console.WriteLine("Hello world!");
		string[] edge;
		if (args.Length == 0){
			string namaFile = Console.ReadLine();
			edge = System.IO.File.ReadAllLines(namaFile);
		}else{
			edge = System.IO.File.ReadAllLines(args[0]);
		}
		List<string> node = new List<string>();
		foreach (string line in edge)
		{
			if (line != edge[0]){
				int space = line.IndexOf(" ");
				string elm1 = line.Substring(0,space);
				string elm2 = line.Substring(space+1);
				if (node.IndexOf(elm1) == -1){
					node.Add(elm1);
				}
				if (node.IndexOf(elm2) == -1){
					node.Add(elm2);
				}
			}
			
		}
		List<int>[] adjacentList = new List<int>[node.Capacity];
		for(int i = 0; i < node.Capacity; i++){
			adjacentList[i] = new List<int>();
		}
		foreach (string line in edge)
		{
			if (line != edge[0]){
				int space = line.IndexOf(" ");
				string elm1 = line.Substring(0,space);
				string elm2 = line.Substring(space+1);
				adjacentList[node.IndexOf(elm1)].Add(node.IndexOf(elm2));
				adjacentList[node.IndexOf(elm2)].Add(node.IndexOf(elm1));
			}
			
		}
		for (int i = 0; i < node.Capacity; i++){
			Console.WriteLine("Adjacent node dari node {0} adalah", node[i]);
			foreach(int b in adjacentList[i]){
				Console.Write(node[b] + " ");
			}
			Console.WriteLine("");
		}

		
	}
}
