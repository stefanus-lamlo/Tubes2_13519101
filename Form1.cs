using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Algorithm;

namespace TryGUI
{
	public partial class Form1 : Form
    {

		List<string> nodes = new List<string>();
		List<int>[] adjacentList;
		//sebenernya array of list string bagus soalnya c# punya banyak method buat nanganin pencarian string tapi lebih nyaman pake int
		//kalau pake int ya konsekuensinya harus make referensi indexof dari nodes
		//misal pengen akses adjacentlist dari node "A", nah cara aksesnya yaitu adjacentlist[nodes.IndexOf("A")]
		//nanti isinya indeks dari nodes mana aja yang bertetanggaan sama A
		bool DFSMode;
        string user = "none";
        string dest = "none";
        string file;
        string path;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBox1.Enabled = false;
			comboBox2.Enabled = false;

            label4.Text = "";
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        // DFS
        {
            DFSMode = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        // BFS
        {
            DFSMode = false;
        }

        private string suffix(int length)
        {
            if (length % 10 == 1)
            {
                return "st";
            } 
            else if(length % 10 == 2)
            {
                return "nd";
            }
            else if (length % 10 == 3)
            {
                return "rd";
            }
            else
            {
                return "th";
            }
        }
        private void button1_Click(object sender, EventArgs e)
		// Submit button (process)
        {


            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                user = comboBox1.SelectedItem.ToString();
                dest = comboBox2.SelectedItem.ToString();
                List<string> output = new List<string>();
                List<string> path = new List<string>();
                string result = "";
                result += "Nama akun: " + user + " dan " + dest + "\n";
                if (DFSMode)
                {

                    output = Algorithm.Algorithm.DepthFirstSearch(adjacentList, nodes, user);
                    path = Algorithm.Algorithm.getPathDFS(adjacentList, output, dest, nodes);

                }
                else
                // BFS Mode
                {
                    path = Algorithm.Algorithm.BreadthFirstSearch(adjacentList, nodes, user, dest);

                }
                if (path.Count >= 2)
                {
                    result += (path.Count - 2) + suffix(path.Count - 2) + " - degree connection\n";
                }
                else
                {
                    MessageBox.Show("Tidak ada jalur koneksi yang tersedia\nAnda harus memulai koneksi baru itu sendiri.");
                }
                //Nama akun: A dan H
                //2nd - degree connection
                //A → B → E → H
                foreach (string c in path)
                {
                    if (c != path[0])
                    {
                        result += " -> " + c;
                    }
                    else
                    {
                        result += c;
                    }

                }
                MessageBox.Show(result);

            }

        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label4.Text = "";
			Algorithm.Algorithm.FriendRecommendation(adjacentList, nodes, comboBox1.SelectedItem.ToString(), label4);
		}

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
            // Browse
        {
           
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select graph file";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            panel1.Controls.Clear();
            

            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();


                nodes.Clear();
                string[] edge;
                
                textBox1.Text = filterFileName(fdlg.FileName);
                path = fdlg.ToString();
                var fileStream = fdlg.OpenFile();

                

				string namaFile = fdlg.FileName;
				edge = System.IO.File.ReadAllLines(namaFile); //ini buat nama file yang diinput di dalem program

				
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

				adjacentList =  new List<int>[nodes.Count]; //sebenernya array of list string bagus soalnya c# punya banyak method buat nanganin pencarian string tapi lebih nyaman pake int
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

				comboBox1.Enabled = true;
				comboBox2.Enabled = true;
				comboBox1.Items.AddRange(nodes.ToArray());
				comboBox2.Items.AddRange(nodes.ToArray());

                //create a viewer object 
                Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
                //create a graph object 
                Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
                //create the graph content 

                
                foreach (string each in edge)
                {
                    if (each != edge[0])
                    {
                        int space = each.IndexOf(" ");
                        string elm1 = each.Substring(0, space);
                        string elm2 = each.Substring(space + 1);

                        var t = graph.AddEdge(elm1, elm2);
                        t.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                    }
                    
                }

                //bind the graph to the viewer 
                viewer.Graph = graph;
                //associate the viewer with the form 
                panel1.SuspendLayout();
                viewer.Dock = System.Windows.Forms.DockStyle.Fill;
                panel1.Controls.Add(viewer);
                panel1.ResumeLayout();

            }


            




        }

        static string filterFileName(string fileName)
        {
            string result;
            int i = fileName.Length - 1;
            while (i >= 0)
            {
                if (fileName[i] == '\\')
                {
                    break;
                }
                i--;
            }
            i++;
            result = fileName.Substring(i, fileName.Length - i);


            return result;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }


}






