using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Globals is a static class that contains all other parameters and objects an instance of 'SiegeGenerator' should
    /// have access to.
    /// </summary>
    public static class Globals
    {
        public static int height = 20;
        public static SiegeFunctions.SiegeCell[,] siegeCells = new SiegeFunctions.SiegeCell[height,height];
        public static int[] wallHeight = new int[height];
        public static  int[] cityHeight = new int[height];
        public static int[] cityBackgroundHeight = new int[height];

        public static bool print = false;
        /// <summary>
        /// PrintCells prints out the state of each cell (It's direction and type) in order if the Globals.print bool is 'true'.
        /// It is for debugging purposes
        /// </summary>
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
