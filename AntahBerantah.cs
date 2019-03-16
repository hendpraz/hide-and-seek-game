using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 CREATED BY :
 Harry Rahmadi Munly
 Muhammad Hendry Prasetya
 Sekar Larasati Muslimah
 */

public class AntahBerantah
{
    //KAMUS GLOBAL
    //static System.IO.StreamReader file = new System.IO.StreamReader(@"d:\peta.txt");
    static int N = Convert.ToInt32(Console.ReadLine());
    //static int[] A = new int[N - 1];
    //static int[] B = new int[N - 1];

    static int[] nodeVal = new int[N + 1];

    static bool[] visited = new bool[N + 1];

    static LinkedList<int>[] myList;

    static int[] pathArr = new int[N + 1];

    static int countPath;

    static bool solved;

    static public void Main(string[] args)
    {
        // KAMUS
        string[] inp = new string[2];
        int temp1, temp2;

        //ALGORITMA

        //inisialiasi ketetanggaan seluruh node dan nilai visited
        myList = new LinkedList<int>[N + 1];
        for (int i = 1; i <= N; i++)
        {
            visited[i] = false;
            myList[i] = new LinkedList<int>();
        }

        //Input
        for (int i = 0; i < N - 1; i++)
        {
            inp = Console.ReadLine().Split();
            temp1 = int.Parse(inp[0]);
            temp2 = int.Parse(inp[1]);
            adjacent(temp1, temp2);
            adjacent(temp2, temp1);
        }
        //Inisialisasi nilai awal simpul dengan nol
        for (int i = 0; i < N + 1; i++)
        {
            nodeVal[i] = 0;
        }
        giveNodeValue(1, 0);

        //if () {
        solveFromFile();
        /*} else {
            readFromGUI();
        }
        */

        /* ADJACENCY DEBUGGING
        for (int j = 1; j <= N; j++)
        {
            Console.WriteLine("Node ke-" + j + "\nLinked list : ");
            foreach (int i in myList[j])
            {
                Console.WriteLine(i + " ");
            }
            Console.WriteLine();
        }
        */

        /* NODE VALUE DEBUGGING
        for(int i = 1; i <= N; i++)
        {
            Console.WriteLine(nodeVal[i] + " ");
        }
        */
    }

    static public void SolveIt(bool goAway, int Goal, int Start)
    {
            //Menyelesaikan masalah
            pathArr[countPath] = Start;
            countPath++;
            
            if (Start == Goal)
            {
                //Ketemu lokasi Jose
                solved = true;
                Console.WriteLine("YES");
                //printPath();
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

    static public void printPath()
    {
        Console.Write("YA\nAnda melewati jalur : ");
        for (int i = 0; i < countPath; i++)
        {
            Console.Write(pathArr[i] + " ");
        }
        Console.WriteLine("\n");
    }

    static public void adjacent(int x, int y)
    {
        //Mengisi ketetanggaan node x dengan y
        myList[x].AddLast(y);
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

    static public void solveFromFile()
    {
        // Memecahkan masalah dari pertanyaan pada file eksternal
        // KAMUS
        string[] str = new string[3];
        int Q;
        int a, b, c;
        //System.IO.StreamReader file = new System.IO.StreamReader(@"d:\question.txt");

        //ALGORITMA
        Q = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < Q; i++)
        {
            str = Console.ReadLine().Split();
            a = int.Parse(str[0]);
            b = int.Parse(str[1]);
            c = int.Parse(str[2]);
            solved = false;
            countPath = 0;
            SolveIt((a == 1), b, c);
            if (!solved)
            {
                Console.WriteLine("NO");
                //Console.WriteLine("TIDAK\n");
            }
        }
    }
    /*
    static public void readFromGUI()
    {
        SolveIt();
    }
    */
}

// Kumpulan sintaks C#
// Console.WriteLine("Hello");
// Console.ReadLine();
// float x = 1f;
// var x = 1;
// var y = x + 2; <- x & y defined as integer
// operator sama seperti C++ / Java
// int[] nums = new int[10]; banyak elemen = nums.Length
// int[,] nums2 = new int[2,2]; 
// sqr(ref a);
// getVal(out a);
