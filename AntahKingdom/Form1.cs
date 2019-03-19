using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Glee;
using System.Drawing.Imaging;
using Microsoft.Glee.Drawing;

namespace AntahKingdom
{
    public partial class Form1 : Form
    {
        //KELOMPOK VARIABLE STATIC INTERAKSI DENGAN TOMBOL

        static string mapLoc1;

        static string questLoc1;
        
        static bool mapLoaded = false;

        static System.IO.StreamReader file2; //menyimpan quest

        static int Q; //Banyaknya question

        static int countQ; //Banyaknya question yang sudah diproses

        /*  
         * BAGIAN WINDOW DAN TOMBOL
         * */

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Next_Click(object sender, EventArgs e)
        {
            if (countQ < Q)
            {
                string str = file2.ReadLine();
                countQ++;
                if (countQ == Q)
                {
                    Next.Visible = false;
                    NextLabel.Visible = false;
                }
                solveFromGUI(str);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Next.Visible = false;
            NextLabel.Visible = false;
            label5.Visible = false;
        }

        private void browseMap_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
           

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                mapLoc1 = dlg.FileName;

                MessageBox.Show("Map loaded : " + mapLoc1);
                mapLoaded = true;
                //Buat representasi map
                buatMap(mapLoc1);
                //Siapkan dan tampilkan graf
                resetWarna();
                TampilkanGraph();
            }
            
        }

        private void browseQuest_Click(object sender, EventArgs e)
        {
            if (mapLoaded)
            {
                OpenFileDialog dlg = new OpenFileDialog();
               
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    questLoc1 = dlg.FileName;

                    MessageBox.Show("Question loaded : " + questLoc1);
                    countQ = 0;
                    //Memunculkan tombol next dan keterangan (label5)
                    Next.Visible = true;
                    NextLabel.Visible = true;
                    label5.Visible = true;
                    solveFromFile(questLoc1);
                }
            }
            else
            {
                MessageBox.Show("Input map first!!");
            }
        }

        private void SubmitQuest_Click(object sender, EventArgs e)
        {
            if (mapLoaded)
            {
                string str2 = pertanyaan.Text;
                solveFromGUI(str2);
            }
        }

        /*  
         * BAGIAN BACK-END
         * */

        static System.IO.StreamReader file;

        static int N; //Banyaknya simpul Rumah

        static int[] nodeVal;//nilai dari setiap node

        static bool[] visited; //suatu node sudah dikunjungi atau belum

        static LinkedList<int>[] myList; 
        //Array of LinkedList untuk merepresentasikan graf rumah-rumah

        static int[] pathArr; //Array of path

        static int countPath; //banyaknya path untuk sebuah solusi

        static bool solved;

        static bool solvable;

        static int firstStart;

        static void buatMap(string mapLoc) //Membuat representasi peta dalam graf dan linkedlist
        {
            //Inisialisasi Graph
            //create a form 
            form = new System.Windows.Forms.Form();
            //create a viewer object 
            viewer = new Microsoft.Glee.GraphViewerGdi.GViewer();
            //create a graph object 
            graph = new Microsoft.Glee.Drawing.Graph("graph");

            //Inisialisasi variable static
            file = new System.IO.StreamReader(@mapLoc);
            N = Convert.ToInt32(file.ReadLine());
            nodeVal = new int[N + 1];
            visited = new bool[N + 1];
            pathArr = new int[N + 1];
            //ALGORITMA

            //inisialiasi ketetanggaan seluruh node dan nilai visited
            myList = new LinkedList<int>[N + 1];
            for (int i = 1; i <= N; i++)
            {
                visited[i] = false;
                myList[i] = new LinkedList<int>();
            }

            //Input
            string[] inp = new string[2];
            int temp1, temp2;
            for (int i = 0; i < N - 1; i++)
            {
                inp = file.ReadLine().Split();
                temp1 = int.Parse(inp[0]);
                temp2 = int.Parse(inp[1]);

                //Mengisi ketetanggaan kedua node
                myList[temp1].AddLast(temp2);
                myList[temp2].AddLast(temp1);

                //Membuat Graph
                string tempst1 = temp1.ToString();
                string tempst2 = temp2.ToString();
                graph.AddEdge(tempst1, tempst2);
            }
            //Inisialisasi nilai awal simpul dengan nol
            for (int i = 1; i <= N; i++)
            {
                nodeVal[i] = 0;
            }
            giveNodeValue(1, 0);

        }
        
        public void SolveIt(bool goAway, int Goal, int Start)
        {
            //Algoritma utama dalam menyelesaikan masalah
            //ALGORITMA
            pathArr[countPath] = Start;
            countPath++;

            //PERBAIKAN ALGORITMA
            solvable = (Goal == Start);
            solvable = solvable || ((goAway) && (nodeVal[Goal] > nodeVal[Start]));
            solvable = solvable || ((!goAway) && (nodeVal[Goal] < nodeVal[Start]));
            //CEK SOLVABILITAS
            if (solvable)
            {
                if (Start == Goal)
                {
                    //Ketemu lokasi Jose
                    solved = true;
                    int a;
                    a = goAway ? 1 : 0;
                    printPath(a,Goal,firstStart);
                }
                else
                {
                    foreach (int i in myList[Start])
                    {
                        bool canGo = goAway && (nodeVal[i] > nodeVal[Start]);
                        canGo = canGo || (!goAway && (nodeVal[i] < nodeVal[Start]));
                        //Hanya bisa DFS jika : ada node yang bisa ditelusuri dan belum mendapatkan solusi
                        if ((canGo) && (!solved))
                        {
                            //DO DFS HERE
                            SolveIt(goAway, Goal, i);
                            countPath--;
                        }
                    }
                }
            }
        }

        public void printPath(int a, int b, int c)
        {
            //Print ke layar seluruh jalur yang dilewati
            string str1 = "";
            
            str1 = a + " " + b + " " + c + " YA\n";
            str1 = str1 + "Ferdiant dapat menemukan Hose\n";
            str1 = str1 + "Ferdiant melalui jalur yang digambarkan";
            label5.Text = str1;
            
            warnaiGraph();
            TampilkanGraph();
            resetWarna();
            //MessageBox.Show(str1);
        }
        
        static public void giveNodeValue(int idx, int val)
        {
            // Memberi nilai pada setiap node
            visited[idx] = true;
            nodeVal[idx] = val;
            foreach (int i in myList[idx])
            {
                if (!visited[i])
                {
                    giveNodeValue(i, val + 1);
                }
            }
        }

        public void solveFromGUI(string str2)
        {
            int a, b, c;
            string[] str3 = new string[3];
            str3 = str2.Split();
            a = int.Parse(str3[0]);
            b = int.Parse(str3[1]);
            c = int.Parse(str3[2]);

            solved = false;
            countPath = 0;
            firstStart = c;
            SolveIt((a == 1), b, c);

            if (!solved)
            {
                string str5;
                str5 = "Ferdiant tidak akan menemukan\n";
                str5 = str5 + "Jose (pada titik "+b+")\n";
                if(a == 1)
                    str5 = str5 + "jika Ferdiant bergerak menjauhi \n";
                else
                    str5 = str5 + "jika Ferdiant bergerak mendekati \n";
                str5 = str5 + "rumah raja (titik 1) ";
                str5 = str5 + "dari titik " + c;
                label5.Text = (a + " " + (b + " " + (c + " TIDAK\n"))) + str5;
                
                TampilkanGraph();
                //MessageBox.Show(str3 + " TIDAK\n");
            }
        }

        public void solveFromFile(string questLoc)
        {
            // Memecahkan masalah dari pertanyaan pada file eksternal
            // KAMUS
            string str;

            file2 = new System.IO.StreamReader(@questLoc);

            //ALGORITMA
            Q = Convert.ToInt32(file2.ReadLine());
            //for (int i = 0; i < Q; i++)
            countQ++;
            str = file2.ReadLine();
            //Lakukan hal yang sama dengan solve problem dari GUI
            solveFromGUI(str);
        }
        
        /*  
         * BAGIAN GRAPH
         * */

        static Microsoft.Glee.Drawing.Graph graph;

        static System.Windows.Forms.Form form;

        static Microsoft.Glee.GraphViewerGdi.GViewer viewer;

        public void TampilkanGraph()
        {
            graph.GraphAttr.EdgeAttr.ArrowHeadAtTarget = Microsoft.Glee.Drawing.ArrowStyle.None;
            graph.GraphAttr.Orientation = Microsoft.Glee.Drawing.Orientation.Landscape;

            //contoh input
            //bind the graph to the viewer 
            viewer.Graph = graph;
            //associate the viewer with the form 
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            //show the form 
            form.ShowDialog();
        }

        public void warnaiGraph()
        {
            for (int i = 0; i < countPath; i++)
            {
                string temp = pathArr[i].ToString();
                graph.FindNode(temp).Attr.Fillcolor = Microsoft.Glee.Drawing.Color.OrangeRed;
            }
        }

        public void resetWarna()
        {
            for (int i = 1; i <= N; i++)
            {
                string temp = i.ToString();
                graph.FindNode(temp).Attr.Fillcolor = Microsoft.Glee.Drawing.Color.Azure;
            }
        }
    }
    
}
