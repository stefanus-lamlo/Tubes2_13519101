using System;
using System.Collections.Generic;
using System.Linq;
public class main
{
	static void MutualFriends(List<int>[] adjacentList, List<string> nodes, string node1, string node2){
		foreach( int a in adjacentList[nodes.IndexOf(node1)]){
			foreach(int b in adjacentList[nodes.IndexOf(node2)]){
				if (a == b){
					Console.WriteLine(nodes[a]);
				}
			}
		}

		//INI ERROR JUGA NJIR
		// for (int i =0; i < adjacentList[nodes.IndexOf(node1)].Capacity; i++){
		// 	for (int j =0; j< adjacentList[nodes.IndexOf(node2)].Capacity; j++){
		// 		if(adjacentList[nodes.IndexOf(node1)][i] == adjacentList[nodes.IndexOf(node2)][j]){
		// 			Console.WriteLine(nodes[adjacentList[nodes.IndexOf(node1)][i]]);
		// 		}
		// 	}
		// }
	}
	static void FriendRecommendation(List<int>[] adjacentList, List<string> nodes, string node ){
		Dictionary<int,int> tabOfFreqMutual = new Dictionary<int, int>();
		int indeks = nodes.IndexOf(node);
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

		// for (int i =0; i < adjacentList[indeks].Capacity; i++){
		// 	Console.WriteLine("Masuk {0}", i);
		// 	int dummy = adjacentList[indeks][i];
		// 	Console.WriteLine("Kapasitasnya adalah {0}", adjacentList[dummy].Capacity);
		// 	for(int j =0; j < adjacentList[dummy].Capacity; j++){
				
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
		List<int>[] adjacentList = new List<int>[nodes.Capacity]; //sebenernya array of list string bagus soalnya c# punya banyak method buat nanganin pencarian string tapi lebih nyaman pake int
		//kalau pake int ya konsekuensinya harus make referensi indexof dari nodes
		//misal pengen akses adjacentlist dari node "A", nah cara aksesnya yaitu adjacentlist[nodes.IndexOf("A")]

		for(int i = 0; i < nodes.Capacity; i++){
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
		for (int i =0; i < nodes.Capacity; i++){
			adjacentList[i].Sort((x,y) => nodes[x].CompareTo(nodes[y]));
			//sort boy
		}
		for (int i = 0; i < nodes.Capacity; i++){
			Console.WriteLine("Adjacent node dari node {0} adalah", nodes[i]);
			foreach(int b in adjacentList[i]){
				Console.Write(" {0}",nodes[b]);
			}
			Console.WriteLine("");
			//ini buat ngecek aja bener apa gak.
		}

		
		
		FriendRecommendation(adjacentList,nodes,"E");

		
	}
}
