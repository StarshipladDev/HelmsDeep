using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public static class Globals
    {
        public static int height = 20;
        public static SiegeFunctions.SiegeCell[,] siegeCells = new SiegeFunctions.SiegeCell[height,height];
        public static int[] wallHeight = new int[height];
        public static  int[] cityHeight = new int[height];
        public static bool print = false;
        public static void PrintCells()
        {
            for(int i=0; i < height; i++)
            {
                for (int f = 0; f < height; f++)
                {
                    if (print)
                    {
                        Debug.Write("[" + siegeCells[i, f].GetNextDirection() + "," + siegeCells[i, f].GetCellType());

                    }
                }
                Debug.Write("\n");
            }
        }
    }

}
