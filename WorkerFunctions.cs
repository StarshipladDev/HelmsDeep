using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class WorkerFunctions
    {
        public static void RefillCells()
        {
            Globals.siegeCells = new SiegeFunctions.SiegeCell[20, 20];
            for (int i = 0; i < Globals.siegeCells.GetLength(0); i++)
            {
                for (int f = 0; f < Globals.siegeCells.GetLength(0); f++)
                {
                    Globals.siegeCells[i, f] = new SiegeFunctions.SiegeCell(SiegeFunctions.Direction.None, SiegeFunctions.CellTypes.Empty);
                }
            }
        }
        public static void FillCells()
        {
                Random rand = new Random();
                for (int i = 0; i < Globals.siegeCells.GetLength(0); i++)
                {
                for (int f = 0; f < Globals.siegeCells.GetLength(0); f++)
                {
                    Globals.siegeCells[i, f] = new SiegeFunctions.SiegeCell(SiegeFunctions.Direction.None, SiegeFunctions.CellTypes.Empty);
                    switch (rand.Next(8))
                    {
                        case (1 | 2 | 3):
                            Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.Urkhai);
                            break;

                        case (4):
                            Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.Rohan);
                            break;
                        default:
                            break;
                    }
                }
        }
        }
        public static void HelmsDeepButton(Form1 f)
        {
            FillCells();
            DisplayCells(f);

        }
        public static void DisplayCells(Form1 f)
        {
            Form1 form1 = f;
            SolidBrush brush  = new SolidBrush(Color.Transparent);
            Graphics g = f.CreateGraphics();
            for (int i = 0; i < Globals.siegeCells.GetLength(0); i++)
            {
                for (int r = 0; r < Globals.siegeCells.GetLength(0); r++)
                {
                    switch(Globals.siegeCells[i, r].GetCellType()){
                        case (SiegeFunctions.CellTypes.Empty):
                            brush.Color = Color.LightCoral;
                            break;
                        case (SiegeFunctions.CellTypes.Urkhai):
                            brush.Color = Color.Black;
                            break;
                        case (SiegeFunctions.CellTypes.Rohan):
                            brush.Color = Color.DarkGreen;
                            break;


                    }
                    g.FillRectangle(brush,new Rectangle(new Point((20*r)+50,(20*i)+100),new Size(20,20)));
                }
            }
            f.Update();
            g.Dispose();
        }
        public static void HelmsDeepFunction(object sender,EventArgs e)
        {
            Button senderButton = (Button)sender;
            Form1 f = (Form1)senderButton.Parent;
            HelmsDeepButton(f);
        }
    }
}
