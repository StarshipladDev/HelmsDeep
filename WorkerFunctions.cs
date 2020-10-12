using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
        public static void SetCell(int i, int f, Random rand)
        {
            int[] wallHeight = Globals.wallHeight;
            int[] cityHeight = Globals.cityHeight;

            if (wallHeight[i] == f)
            {
                Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.WallTop);
                if (rand.Next(5) == 0)
                {
                    Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.Rohan);
                }
                if (i > 0)
                {
                    if (wallHeight[i - 1] == wallHeight[i] - 1 || wallHeight[i - 1] == wallHeight[i]+1)
                    {
                        Globals.siegeCells[i - 1, f].SetCellType(SiegeFunctions.CellTypes.WallTop);
                    }

                }
            }
            if (wallHeight[i] + 1 == f || wallHeight[i] + 2 == f)
            {
                Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.Wall);
            }
            if (cityHeight[i] > f)
            {

                Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.Night);
            }
            if (cityHeight[i] <= f && wallHeight[i] > f)
            {

                Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.CityProper);
                if (rand.Next(30) == 0 && f < wallHeight[i] - 2)
                {
                    Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.Torch);
                }
            }
            if (f > wallHeight[i] + 2)
            {
                Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.Ground);
            }
            if (f > wallHeight[i] + 4) { 

                bool placeSoldier = false;
                if (Globals.siegeCells[i, f - 1].GetCellType() == SiegeFunctions.CellTypes.LadderStart)
                {

                    Globals.siegeCells[i, f - 1].SetCellType(SiegeFunctions.CellTypes.Ladder);
                    Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.Ladder);
                }
                else if (i > 4 && i < Globals.siegeCells.GetLength(0) - 3)
                {
                    placeSoldier = true;
                }
                else if (rand.Next(5) > 1)
                {
                    placeSoldier = true;
                }
                if (placeSoldier)
                {
                    Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.Urkhai);
                    if (rand.Next(30) == 0)
                    {
                        Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.UrkhaiElite);
                    }
                    if (rand.Next(30) == 0)
                    {
                        Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.LadderStart);
                    }
                    if (rand.Next(30) == 0)
                    {
                        Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.Torch);
                    }
                }
            }
        }
        public static void FillCells()
        {
            Random rand = new Random();
            //The height of the Top of the wall
            int height = 20;
            int[] wallHeight = Globals.wallHeight;
            int[] cityHeight = Globals.cityHeight;
            for (int i = 0; i < height; i++) {
                if (i == 0) {
                    wallHeight[i] = 7;
                    cityHeight[i] = 2;
                }
                else {
                    wallHeight[i] = wallHeight[i - 1];
                    cityHeight[i] = cityHeight[i - 1];
                }
                int wallHeightChange = rand.Next(3);
                int cityHeightChange = rand.Next(3);
                if (wallHeight[i] > 10)
                {
                    wallHeightChange = rand.Next(2);
                }
                if (wallHeight[i] < 5)
                {
                    wallHeightChange = rand.Next(2) + 1;
                }
                if (cityHeight[i] > 4) {

                    cityHeightChange = rand.Next(2);

                }
                if (cityHeight[i] < 2)
                {
                    cityHeightChange = rand.Next(2) + 1;
                }
                if (cityHeightChange == 0)
                {
                    cityHeight[i]--;
                }
                if (cityHeightChange == 2)
                {
                    cityHeight[i]++;
                }
                if (wallHeightChange == 0)
                {
                    wallHeight[i]--;
                }
                if (wallHeightChange == 2)
                {
                    wallHeight[i]++;
                }
                Debug.WriteLine("Wall:" + wallHeight[i] + ", city:" + cityHeight[i]);
            }
            //X axis
            for (int i = 0; i < Globals.siegeCells.GetLength(0); i++) {
                //Y axis
                for (int f = 0; f < Globals.siegeCells.GetLength(0); f++)
                {
                    Globals.siegeCells[i, f] = new SiegeFunctions.SiegeCell(SiegeFunctions.Direction.None, SiegeFunctions.CellTypes.Empty);
                    SetCell(i, f, rand);

                    /*
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
                    */
                    Debug.Write(Globals.siegeCells[i, f].GetCellType().ToString());
                }
                Debug.Write("\n");
            }
        }
        public static void HelmsDeepButton(Form1 f)
        {
            Stream s = File.Create("Output.gif");
            StreamWriter sw = new StreamWriter(s);
            GifWriter gw = new GifWriter(s,350,1) ;
            Random rand = new Random();
            FillCells();

            Bitmap m = new Bitmap(600, 600);
            //Graphics g = Graphics.FromImage(m);
            DisplayCells(f,rand,m);

            for (int i=0; i<15; i++)
            {
                Debug.WriteLine(" I is "+i);
                Globals.PrintCells();
                SetUpSiegeAnimation(rand);
                RunAnimation();
                DisplayCells(f,rand,m);
                m.Save("temp.png", ImageFormat.Png);
                Globals.PrintCells();
                //Image netFrame = Image.FromFile("temp.png"); ;
                gw.WriteFrame(m);
                Thread.Sleep(1000);
            }
            //g.Dispose();
            m.Dispose();
            gw.Dispose();
            s.Close();

        }
        public static bool GetNearbyCells(int x, int y, SiegeFunctions.CellTypes search)
        {
            if (x > 0)
            {

                if (Globals.siegeCells[x - 1, y].GetCellType() == search)
                {
                    return true;
                }
                if (y > 0 && Globals.siegeCells[x - 1, y].GetCellType() == search)
                {
                    return true;
                }
                if (y < Globals.siegeCells.GetLength(0) - 1 && Globals.siegeCells[x - 1, y + 1].GetCellType() == search)
                {
                    return true;
                }
            }
            if (y > 0 && Globals.siegeCells[x, y - 1].GetCellType() == search)
            {
                return true;
            }
            if (y < Globals.siegeCells.GetLength(0) - 1 && Globals.siegeCells[x, y + 1].GetCellType() == search)
            {
                return true;
            }
            if (x < Globals.siegeCells.GetLength(0) - 1)
            {

                if (Globals.siegeCells[x + 1, y].GetCellType() == search)
                {
                    return true;
                }
                if (y > 0 && Globals.siegeCells[x + 1, y - 1].GetCellType() == search)
                {
                    return true;
                }
                if (y < Globals.siegeCells.GetLength(0) - 1 && Globals.siegeCells[x + 1, y + 1].GetCellType() == search)
                {
                    return true;
                }
            }
            return false;
        }
        public static void RunAnimation()
        {
            for (int i = 0; i < Globals.siegeCells.GetLength(1); i++)
            {
                for (int f = 1; f < Globals.siegeCells.GetLength(1); f++)
                {
                    if (Globals.siegeCells[i, f].GetNextDirection() == SiegeFunctions.Direction.Up)
                    {

                        Globals.siegeCells[i, f - 1].SetCellType(Globals.siegeCells[i, f].GetCellType());
                        
                    }
                    else if (Globals.siegeCells[i, f].GetNextDirection() == SiegeFunctions.Direction.Left)
                    {

                        Globals.siegeCells[i-1,f].SetCellType(Globals.siegeCells[i, f].GetCellType());

                    }
                    else if (Globals.siegeCells[i, f].GetNextDirection() == SiegeFunctions.Direction.Right)
                    {

                        Globals.siegeCells[i+1,f].SetCellType(Globals.siegeCells[i, f].GetCellType());

                    }
                    //RESET AREA BELOW
                    if(Globals.siegeCells[i, f].GetNextDirection() != SiegeFunctions.Direction.None)
                    {
                        if (f > Globals.wallHeight[i] + 2)
                        {
                            Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.Ground);
                        }
                        else if (f > Globals.wallHeight[i])
                        {
                            Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.Ladder);
                        }
                        else if (f <= Globals.wallHeight[i])
                        {
                            Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.WallTop);
                        }
                    }
                    
                }
            }
            int newUnitCounter=0;
            Random rand = new Random();
            for (int i = 0; i < Globals.height - 1; i++)
            {
                //If unit ahead is Ladder start, make that ladder and make current unit ladder
                if (Globals.siegeCells[i, Globals.height - 1].GetCellType() == SiegeFunctions.CellTypes.LadderStart)
                {
                    Globals.siegeCells[i, Globals.height - 2].SetCellType(SiegeFunctions.CellTypes.Ladder);
                    Globals.siegeCells[i, Globals.height-1].SetCellType(SiegeFunctions.CellTypes.Ladder);

                }
                // Otherwise replace 3 ground tiles with new units
                else if (newUnitCounter<5 && Globals.siegeCells[i, Globals.height-1].GetCellType()==(SiegeFunctions.CellTypes.Ground))
                {
                    if (rand.Next(2) > 0)
                    {
                        SetCell(i, Globals.height-1,rand);
                        newUnitCounter++;
                    }
                }
            }
        }
        public static void SetUpSiegeAnimation(Random rand){
            for(int i=0; i< Globals.siegeCells.GetLength(1); i++)
            {
                for (int f = 1; f < Globals.siegeCells.GetLength(1); f++)
                {
                    //Default-> set direction as none
                    Globals.siegeCells[i, f].SetNextDirection(SiegeFunctions.Direction.None);
                    Globals.siegeCells[i, f].wounded = false;
                    //Urkhai , Elites and Torches
                    if (Globals.siegeCells[i, f].GetCellType() == SiegeFunctions.CellTypes.Urkhai || Globals.siegeCells[i, f].GetCellType() == SiegeFunctions.CellTypes.Torch || Globals.siegeCells[i, f].GetCellType() == SiegeFunctions.CellTypes.UrkhaiElite)
                    {
                        //If touching Rohan Soldier, set one as wounded
                        if (GetNearbyCells(i, f, SiegeFunctions.CellTypes.Rohan))
                        {
                            if (rand.Next(3) > 1)
                            {
                                Globals.siegeCells[i, f].wounded = true;
                            }
                        }

                        //If below Wallbase, and cell in front is free or will be free, move foward
                        else if (f > Globals.wallHeight[i] + 3 && (Globals.siegeCells[i,f-1].GetNextDirection()== SiegeFunctions.Direction.Up || Globals.siegeCells[i, f - 1].GetCellType()== SiegeFunctions.CellTypes.Ground))
                        {
                            Globals.siegeCells[i, f].SetNextDirection(SiegeFunctions.Direction.Up);
                        }
                        //Move up if ladder on wall above
                        else if (f >= Globals.wallHeight[i] && (Globals.siegeCells[i, f - 1].GetCellType()== SiegeFunctions.CellTypes.Ladder || Globals.siegeCells[i, f - 1].GetCellType() == SiegeFunctions.CellTypes.WallTop))
                        {
                            Globals.siegeCells[i, f].SetNextDirection(SiegeFunctions.Direction.Up);
                        }
                        //If on wall and Left or Right Wall free && WallTop or Ground, move there
                        else if (rand.Next(2) > 0)
                        {
                            if ((i > 1 && (( Globals.siegeCells[i - 1, f].GetCellType() == SiegeFunctions.CellTypes.WallTop)|| (Globals.siegeCells[i - 1, f].GetCellType() == SiegeFunctions.CellTypes.Ground))))
                            {
                                if(i>2 &&Globals.siegeCells[i-2, f].GetNextDirection()!= SiegeFunctions.Direction.Right)
                                {
                                    Globals.siegeCells[i, f].SetNextDirection(SiegeFunctions.Direction.Left);
                                }
                            }
                        }
                        else if((i <Globals.height-2 && ( Globals.siegeCells[i + 1, f].GetCellType() == SiegeFunctions.CellTypes.WallTop || Globals.siegeCells[i + 1, f].GetCellType() == SiegeFunctions.CellTypes.Ground )) )
                        {
                            Globals.siegeCells[i, f].SetNextDirection(SiegeFunctions.Direction.Right);
                        }
                    }
                    //Ladders
                    if(Globals.siegeCells[i, f].GetCellType() == SiegeFunctions.CellTypes.Ladder)
                    {
                        //If below Wall, keep moving
                        if (f > Globals.wallHeight[i] + 3)
                        {
                            Globals.siegeCells[i, f].SetNextDirection(SiegeFunctions.Direction.Up);
                        }
                        //If at Wall and ladder following, stop
                        else if (f == Globals.wallHeight[i] + 3 && Globals.siegeCells[i, f + 1].GetCellType() == SiegeFunctions.CellTypes.Ladder)
                        {
                            Globals.siegeCells[i, f].SetNextDirection(SiegeFunctions.Direction.None);
                        }
                        //If at wall and behind isn't Urkahai
                        else if(f == Globals.wallHeight[i] + 3 && Globals.siegeCells[i, f + 1].GetCellType() != SiegeFunctions.CellTypes.Ladder)
                        {
                            Globals.siegeCells[i, f - 1].SetCellType(SiegeFunctions.CellTypes.Ladder);
                            Globals.siegeCells[i, f - 2].SetCellType(SiegeFunctions.CellTypes.Ladder);
                        }
                    }
                    //Rohan
                    if (Globals.siegeCells[i, f].GetCellType() == SiegeFunctions.CellTypes.Rohan)
                    {
                        if (GetNearbyCells(i, f, SiegeFunctions.CellTypes.Urkhai) || GetNearbyCells(i, f, SiegeFunctions.CellTypes.UrkhaiElite))
                        {
                            if (rand.Next(3) > 1)
                            {
                                Globals.siegeCells[i, f].wounded = true;
                            }
                        }
                    }
                }
            }
        }
        public static void DisplayCells(Form1 f,Random rand,Bitmap m)
        {
            Form1 form1 = f;
            SolidBrush brush  = new SolidBrush(Color.Transparent);
            Graphics g = Graphics.FromImage(m);
            for (int r = 0; r < Globals.siegeCells.GetLength(0); r++)
            {
                for (int i = 0; i < Globals.siegeCells.GetLength(0); i++)
                {
                    switch(Globals.siegeCells[r, i].GetCellType()){
                        case (SiegeFunctions.CellTypes.Ground):
                            brush.Color = Color.FromArgb(255,178,123,89);
                            break;
                        case (SiegeFunctions.CellTypes.Wall):
                            brush.Color = Color.FromArgb(255, 128, 128, 128);
                            break;
                        case (SiegeFunctions.CellTypes.WallTop):
                            brush.Color = Color.FromArgb(255, 89, 89, 89);
                            break;
                        case (SiegeFunctions.CellTypes.Night):
                            brush.Color = Color.FromArgb(255, 0, 0, 0);
                            break;
                        case (SiegeFunctions.CellTypes.CityProper):
                            brush.Color = Color.FromArgb(255, 64, 64, 64);
                            break;
                        case (SiegeFunctions.CellTypes.Rohan):

                            brush.Color = Color.FromArgb(255, 165, 255, 127);
                            break;
                        case (SiegeFunctions.CellTypes.Torch):

                            brush.Color = Color.FromArgb(255, 255, 216, 0);
                            break;
                        case (SiegeFunctions.CellTypes.Urkhai):

                            brush.Color = Color.FromArgb(255, 48, 48, 48);
                            if(GetNearbyCells(r,i, SiegeFunctions.CellTypes.Torch))
                            {

                                brush.Color = Color.FromArgb(255, 71, 71, 71);
                            }
                            break;
                        case (SiegeFunctions.CellTypes.UrkhaiElite):

                            brush.Color = Color.FromArgb(255, 192, 192, 192);
                            break;
                       case (SiegeFunctions.CellTypes.Ladder):
                             brush.Color = Color.FromArgb(255, 127, 63, 63);
                            break;
                        default:
                                brush.Color = Color.FromArgb(165, 255, 0, 0);
                                break;


                    }
                    //If WOunded, change color to red
                    if(Globals.siegeCells[r, i].wounded)
                    {
                        brush.Color = Color.FromArgb(255, 255, 125, 125);
                    }
                    g.FillRectangle(brush,new Rectangle(new Point((20*r)+50,(20*i)+100),new Size(20,20)));
                    if (Globals.siegeCells[r,i].GetCellType() == SiegeFunctions.CellTypes.Urkhai)
                    {
                        Pen smotherPen = new Pen(Color.LightGray);
                        for (int z = 0; z < 1; z++)
                        {
                            if (rand.Next(3) > 1)
                            {
                                int one = rand.Next(20) + (20 * r) + 50;
                                int two = rand.Next(20) + (20 * i) + 100;
                                int three = one + (rand.Next(10) - 5);
                                int four = two + (rand.Next(10) - 5);
                                if (three < 0)
                                {
                                    three = 0;
                                }
                                if (four < 0)
                                {
                                    four = 0;
                                }
                                g.DrawLine(smotherPen, one, two, three, four);
                            }
                        }
                    }
                    
                }
            }
            
            Graphics gg = f.CreateGraphics();
            gg = g;
            f.Update();

            //Thread.Sleep(1000);
            gg.Dispose();
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
