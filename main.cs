﻿using System;
using System.Collections.Generic;
using System.Linq;
public class main
{
	static List<string> BreadthFirstSearch(List<int>[] adjacentList, List<string> nodes, string start, string end)
	{
		List<int> output = new List<int>();
		List<int> temp = new List<int>();
		temp = adjacentList[nodes.IndexOf(start)];
		Boolean found = false;
		output.Add(nodes.IndexOf(start));
		int[] predator = new int[nodes.Count];
		for (int i = 0 ; i < nodes.Count; i++){
			predator[i] = -1;
		}
		bool[] visited = new bool[nodes.Count];
		for  (int i = 0; i < nodes.Count; i++){
			visited[i] = false;
		}  
		visited[nodes.IndexOf(start)] = true;
		while(output.Count != 0 && !found){
			Console.WriteLine("tes");
			int i = output[0];
			output.RemoveAt(0);
			temp = adjacentList[i];
			foreach (int val in temp){
				if (!visited[val]){
					if (val == nodes.IndexOf(end)){
						found = true;
					}
					visited[val] = true;
					predator[val] = i;
					output.Add(val);
				}
			}
		}

		for(int i = 0 ; i < nodes.Count; i++){
			if (predator[i] != -1){
				Console.WriteLine("{0} -> {1}", nodes[predator[i]], nodes[i]);
			}
		}
		Console.WriteLine("Ini debugging");

		List<string> hasil = new List<string>();
		if(!output.Contains(nodes.IndexOf(end))){
			Console.WriteLine("Failed to locate user");
			return hasil;
		}
		else{
			int i = nodes.IndexOf(end);
			hasil.Add(end);
			while(predator[i]!=nodes.IndexOf(start)){
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
                if (adjacentList[nodes.IndexOf(curr)].IndexOf(nodes.IndexOf(output[j])) != -1 )
                {
                    curr = output[j];
                    path.Add(output[j]);
                }
            }
            path.Reverse();
            return path;
        }
	static void MutualFriends(List<int>[] adjacentList, List<string> nodes, string node1, string node2){
		//TETEP PAKE INI SOALNYA NYAMAN UNTUK DIBACA
		foreach( int a in adjacentList[nodes.IndexOf(node1)]){
			foreach(int b in adjacentList[nodes.IndexOf(node2)]){
				if (a == b){
					Console.WriteLine(nodes[a]);
				}
			}
		}

		//INI ERROR JUGA NJIR
		//TERNYATA HARUS PAKE COUNT BUKAN CAPACITY, CAPACITY ITU MAKSNYA DAN PASTI DBIKIN PANGKAT 2, PANTES LOLOS KALAU ISINYA 4

		// for (int i =0; i < adjacentList[nodes.IndexOf(node1)].Count; i++){
		// 	for (int j =0; j< adjacentList[nodes.IndexOf(node2)].Count; j++){
		// 		if(adjacentList[nodes.IndexOf(node1)][i] == adjacentList[nodes.IndexOf(node2)][j]){
		// 			Console.WriteLine(nodes[adjacentList[nodes.IndexOf(node1)][i]]);
		// 		}
		// 	}
		// }
	}
	static void FriendRecommendation(List<int>[] adjacentList, List<string> nodes, string node ){
		Dictionary<int,int> tabOfFreqMutual = new Dictionary<int, int>();
		int indeks = nodes.IndexOf(node);
		//PAKAI INI AJA SOALNYA INDAH UNTUK DIBACA

		foreach( int a in adjacentList[indeks]){
			// Console.WriteLine("Kapasitasnya adalah {0}", adjacentList[a].Capacity);
			foreach(int b in adjacentList[a]){
				if (b != indeks && adjacentList[indeks].IndexOf(b) == -1 ){
					if ( tabOfFreqMutual.ContainsKey(b)){
						// Console.WriteLine("Ada");
						tabOfFreqMutual[b]++;
					}else{
						tabOfFreqMutual.Add(b,1);
						// Console.WriteLine("Ga ada");
					}
				}
				// else{
				// 	// Console.WriteLine("Ga cocok");
				// }
			}
			// Console.WriteLine("Next");
		}
		
		//ANJIR OGEB BANGET MASAK PAKE FORLOOP BIASA MALAH BIKIN ERROR

		// for (int i =0; i < adjacentList[indeks].Count; i++){
		// 	Console.WriteLine("Masuk {0}", i);
		// 	int dummy = adjacentList[indeks][i];
		// 	Console.WriteLine("Kapasitasnya adalah {0}", adjacentList[dummy].Count);
		// 	for(int j =0; j < adjacentList[dummy].Count; j++){
				
		// 		if (adjacentList[dummy][j] != indeks && adjacentList[indeks].IndexOf(adjacentList[dummy][j]) == -1){
		// 			if (tabOfFreqMutual.ContainsKey(adjacentList[dummy][j])){
		// 				Console.WriteLine("Ada");
		// 				tabOfFreqMutual[adjacentList[dummy][j]]++;
		// 				Console.WriteLine("Masaka?");
		// 			}else{
		// 				Console.WriteLine("Ga Ada");
		// 				tabOfFreqMutual.Add(adjacentList[dummy][j],1);
		// 			}
		// 		}else{
		// 			Console.WriteLine("ga cocok");
		// 		}
		// 		Console.WriteLine("yeet");
		// 	}
		// 	Console.WriteLine("Keluar");
		// }

		// Console.WriteLine("Sampai sini");

		List<KeyValuePair<int,int>> sortedTable =  tabOfFreqMutual.ToList();
		sortedTable.Sort((y,x) => x.Value.CompareTo(y.Value));
		Console.WriteLine("Daftar rekomendasi teman untuk akun {0} :", node);

		foreach(KeyValuePair<int,int> a in sortedTable){
			Console.WriteLine("Nama akun: {0}", nodes[a.Key]);
			Console.WriteLine("{0} mutual friends:", a.Value);
			MutualFriends(adjacentList,nodes,node,nodes[a.Key]);
			Console.WriteLine("");
		}


	}
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
			edge = System.IO.File.ReadAllLines(namaFile); //ini buat nama file yang diinput di dalem program
		}else{
			edge = System.IO.File.ReadAllLines(args[0]); //ini khusus buat aku soalnya males nulis hehe
		}
		List<string> nodes = new List<string>();
		foreach (string line in edge)
		{
			if (line != edge[0]){
				int space = line.IndexOf(" ");
				string elm1 = line.Substring(0,space);
				string elm2 = line.Substring(space+1);
				if (nodes.IndexOf(elm1) == -1){ //kalau belum ada di daftar nodes, tambahin
					nodes.Add(elm1);
				}
				if (nodes.IndexOf(elm2) == -1){ //kalau belum ada tambahin juga
					nodes.Add(elm2);
				}
			}
			
		}
		List<int>[] adjacentList = new List<int>[nodes.Count]; //sebenernya array of list string bagus soalnya c# punya banyak method buat nanganin pencarian string tapi lebih nyaman pake int
		//kalau pake int ya konsekuensinya harus make referensi indexof dari nodes
		//misal pengen akses adjacentlist dari node "A", nah cara aksesnya yaitu adjacentlist[nodes.IndexOf("A")]
		//nanti isinya indeks dari nodes mana aja yang bertetanggaan sama A

		for(int i = 0; i < nodes.Count; i++){
			adjacentList[i] = new List<int>();
		}
		foreach (string line in edge)
		{
			if (line != edge[0]){
				int space = line.IndexOf(" ");
				string elm1 = line.Substring(0,space);
				string elm2 = line.Substring(space+1);
				adjacentList[nodes.IndexOf(elm1)].Add(nodes.IndexOf(elm2)); //adjacentlist[elm1] ditambahin index elm2 di list nodesnya
				//misal ada edge A B, maka dan nodes isinya {A, B} maka adjacentlist[0].Add(1)
				// 0 didapet dari indeksnya nodes yang isinya "A" dan 1 dari indeksnya node yang isinya "B"
				adjacentList[nodes.IndexOf(elm2)].Add(nodes.IndexOf(elm1)); //masing masing dimasukkin ke list adjacency tetangganya
			}
			
		}
		for (int i =0; i < nodes.Count; i++){
			adjacentList[i].Sort((x,y) => nodes[x].CompareTo(nodes[y]));
			//sort boy
		}
		for (int i = 0; i < nodes.Count; i++){
			Console.WriteLine("Adjacent node dari node {0} adalah", nodes[i]);
			foreach(int b in adjacentList[i]){
				Console.Write(" {0}",nodes[b]);
			}
			Console.WriteLine("");
			//ini buat ngecek aja bener apa gak.
		}

		List<string> tes = new List<string>();
		tes = DepthFirstSearch(adjacentList,nodes,"A");

		List<string> testing = new List<string>();
		testing = getPathDFS(adjacentList,tes, "H", nodes);

		tes = BreadthFirstSearch(adjacentList, nodes, "A", "H");
		foreach( string s in tes){
			Console.WriteLine(s);
		}

		// foreach(string s in tes){
		// 	Console.WriteLine(s);
		// }
		
		foreach(string s in testing){
			Console.WriteLine(s);
		}

		FriendRecommendation(adjacentList,nodes,"E");

		
	}
}