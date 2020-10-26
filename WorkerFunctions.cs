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
    ///<summary>
    ///
    /// WorkerFunctions is a static class, comprising a suite of 
    /// It primarily interacts with the <see cref="Globals"/>Globals</see> class to produce a drawable
    /// 2D array 'gridlist' in the globals class.
    /// 
    ///</summary>

    class WorkerFunctions
    {



        /// <summary>
        /// HelmsDeepFunction is a listner Function that cna be assigned to a button.
        /// It runs the function <see cref="HelmsDeepButton"/>
        /// </summary>
        /// <param name="sender">The object that made the request</param>
        /// <param name="e">The parameters of the context this function was called</param>
        public static void HelmsDeepFunction(object sender, EventArgs e)
        {
            Button senderButton = (Button)sender;
            Form1 f = (Form1)senderButton.Parent;
            HelmsDeepButton(f);
        }
        /// <summary>
        /// HelmsDeepButton is a Static method that resets the etire GIF drawing process,
        /// resets all cells, redraws each cell, and prints a specified ammount of frames,
        /// both to a form passed and to an output 'Output.gif' file.
        /// </summary>
        /// <param name="f">The form to draw the new frames to</param>
        /// <param name="frames">The ammount of frames to print</param>
        public static void HelmsDeepButton(Form1 f, int frames = 20)
        {
            Stream s = File.Create("Output.gif");
            StreamWriter sw = new StreamWriter(s);
            GifWriter gw = new GifWriter(s, 350, 1);
            Random rand = new Random();
            FillCells();
            Bitmap m = new Bitmap(400, 400);
            //Graphics g = Graphics.FromImage(m);
            DisplayCells(f, rand, m);
            Graphics gg = f.CreateGraphics();
            gg.DrawImage(new Bitmap("Help.png"), 400, 50);
            gg.Dispose();
            for (int i = 0; i < frames; i++)
            {
                Debug.WriteLine(" I is " + i);
                Globals.PrintCells();
                SetUpSiegeAnimation(rand);
                RunAnimation();
                DisplayCells(f, rand, m, i);
                m.Save("temp.png", ImageFormat.Png);
                Globals.PrintCells();
                gw.WriteFrame(m);
                Thread.Sleep(1000);
            }
            //g.Dispose();
            m.Dispose();
            gw.Dispose();
            s.Close();

        }
        /// <summary>
        /// RefillCells sets all of GLobal's 'gridlist' to be empty
        /// </summary>
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
        /// <summary>
        /// Sets a specific cell as a random cell proceduarly based on its location.
        /// It does this by changing a cell in Global's 'Siegecells' array
        /// </summary>
        /// <param name="i">The'x' value of the cell to set</param>
        /// <param name="f">The 'y' value of the cell to set</param>
        /// <param name="rand">An instance of <see cref="Random"> Random</see> to proceduarly generate a celltype.</param>
        public static void SetCell(int i, int f, Random rand)
        {
            int[] wallHeight = Globals.wallHeight;
            int[] cityBackgroundHeight = Globals.cityBackgroundHeight;
            int[] cityHeight = Globals.cityHeight;


            //SECTION wall height  Ground setup
            //Wall Top
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
            //Regular Wall
            if (wallHeight[i] + 1 == f || wallHeight[i] + 2 == f)
            {
                Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.Wall);
            }
            //Night
            if (cityBackgroundHeight[i] > f)
            {

                Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.Night);
            }
            //Background CIty
            if (cityBackgroundHeight[i] <= f && cityHeight[i] > f)
            {
                Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.BackgroundCity);
                if (rand.Next(20) == 0  && f<cityHeight[i]-1)
                {
                    Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.Torch);
                }
            }
            if (cityHeight[i] <= f && wallHeight[i] > f)
            {

                Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.CityProper);
                
            }
            if (f > wallHeight[i] + 2)
            {
                Globals.siegeCells[i, f].SetCellType(SiegeFunctions.CellTypes.Ground);
            }
            //END SECTION
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
        /// <summary>
        /// FillCells is a function that fills all cells in Globals.Siegegrid with the default 'ground'
        /// tile. It also randomly generates the 'horizon' height of the various background layers such as <see cref="SiegeFunctions.CellTypes.CityProper"/>
        /// </summary>
        public static void FillCells()
        {
            Random rand = new Random();
            //The height of the Top of the wall
            int height = 20;
            int[] wallHeight = Globals.wallHeight;
            int[] cityHeight = Globals.cityHeight;
            int[] cityBackgroundHeight = Globals.cityBackgroundHeight;
            //SECTION (1) Setup city/backgroundCIty/Wall height Arrays
            // Y Values | Content
            //0->2 Night
            // 3->5 Background CIty
            // 4->6 City
            // 6->10 WallTop
            for (int i = 0; i < height; i++) {
                if (i == 0) {
                    wallHeight[i] = 9;
                    cityBackgroundHeight[i] = 2;
                    cityHeight[i] = 6;
                }
                else {
                    wallHeight[i] = wallHeight[i - 1];

                    cityBackgroundHeight[i] = cityBackgroundHeight[i-1];
                    cityHeight[i] = cityHeight[i - 1];
                }
                int wallHeightChange = rand.Next(3);
                int cityBackgroundHeightChange = rand.Next(3);
                int cityHeightChange = rand.Next(3);
                // Section(2) Walls and city only move in direction within their 'Limits'

                if (cityBackgroundHeight[i] < 1)
                {
                    cityBackgroundHeightChange = rand.Next(2) + 1;
                }
                if (cityBackgroundHeight[i] >4 )
                {
                    cityBackgroundHeightChange = rand.Next(2);
                }
                if (wallHeight[i] > 12)
                {
                    wallHeightChange = rand.Next(2);
                }
                if (wallHeight[i] < 8)
                {
                    wallHeightChange = rand.Next(2) + 1;
                }
                if (cityHeight[i] >7) {

                    cityHeightChange = rand.Next(2);

                }
                if (cityHeight[i] <5)
                {
                    cityHeightChange = rand.Next(2) + 1;
                }
                if (cityBackgroundHeight[i] > cityHeight[i])
                {
                    cityBackgroundHeightChange = rand.Next(2);
                }
                // End setion(2)
                //Section(2) change various heights based on their respective random
                if (cityHeightChange == 0)
                {
                    cityHeight[i]--;
                }
                if (cityHeightChange == 2)
                {
                    cityHeight[i]++;
                }
                /*
                 * REMOVED 26/10/2020 due to customer feedback 
                if (wallHeightChange == 0)
                {
                    wallHeight[i]--;
                }
                if (wallHeightChange == 2)
                {
                    wallHeight[i]++;
                }
                */
                if (cityBackgroundHeightChange == 0)
                {
                    cityBackgroundHeight[i]--;
                }
                if (cityBackgroundHeightChange == 2)
                {
                    cityBackgroundHeight[i]++;
                }
                //End section(2)
                Debug.WriteLine("Wall:" + wallHeight[i] + ", city:" + cityHeight[i]);
            }
            //End SECTION(1)
            //X axis
            for (int i = 0; i < Globals.siegeCells.GetLength(0); i++) {
                //Y axis
                for (int f = 0; f < Globals.siegeCells.GetLength(0); f++)
                {
                    Globals.siegeCells[i, f] = new SiegeFunctions.SiegeCell(SiegeFunctions.Direction.None, SiegeFunctions.CellTypes.Empty);
                    SetCell(i, f, rand);
                    Debug.Write(Globals.siegeCells[i, f].GetCellType().ToString());
                }
                Debug.Write("\n");
            }
        }

        /// <summary>
        /// SetUpSiegeAnimation is a function to iteratively decide the next state a cell will be in based
        /// on gamelogic and the cellType. These next steps are carried out in <see cref="RunAnimation"/>
        /// </summary>
        /// <param name="rand">An instance of <see cref="Random"/></param>
        public static void SetUpSiegeAnimation(Random rand)
        {
            for (int i = 0; i < Globals.siegeCells.GetLength(1); i++)
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
                        else if (f > Globals.wallHeight[i] + 3 && (Globals.siegeCells[i, f - 1].GetNextDirection() == SiegeFunctions.Direction.Up || Globals.siegeCells[i, f - 1].GetCellType() == SiegeFunctions.CellTypes.Ground))
                        {
                            Globals.siegeCells[i, f].SetNextDirection(SiegeFunctions.Direction.Up);
                        }
                        //Move up if ladder on wall above
                        else if (f >= Globals.wallHeight[i] && (Globals.siegeCells[i, f - 1].GetCellType() == SiegeFunctions.CellTypes.Ladder || Globals.siegeCells[i, f - 1].GetCellType() == SiegeFunctions.CellTypes.WallTop))
                        {
                            Globals.siegeCells[i, f].SetNextDirection(SiegeFunctions.Direction.Up);
                        }
                        //If on wall and Left or Right Wall free && WallTop or Ground, move there
                        else if (rand.Next(2) > 0)
                        {
                            if ((i > 1 && ((Globals.siegeCells[i - 1, f].GetCellType() == SiegeFunctions.CellTypes.WallTop) || (Globals.siegeCells[i - 1, f].GetCellType() == SiegeFunctions.CellTypes.Ground))))
                            {
                                if (i > 2 && Globals.siegeCells[i - 2, f].GetNextDirection() != SiegeFunctions.Direction.Right)
                                {
                                    Globals.siegeCells[i, f].SetNextDirection(SiegeFunctions.Direction.Left);
                                }
                            }
                        }
                        else if ((i < Globals.height - 2 && (Globals.siegeCells[i + 1, f].GetCellType() == SiegeFunctions.CellTypes.WallTop || Globals.siegeCells[i + 1, f].GetCellType() == SiegeFunctions.CellTypes.Ground)))
                        {
                            Globals.siegeCells[i, f].SetNextDirection(SiegeFunctions.Direction.Right);
                        }
                    }
                    //Ladders
                    if (Globals.siegeCells[i, f].GetCellType() == SiegeFunctions.CellTypes.Ladder)
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
                        else if (f == Globals.wallHeight[i] + 3 && Globals.siegeCells[i, f + 1].GetCellType() != SiegeFunctions.CellTypes.Ladder)
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
                    //If wounded destroy on 33%chance
                    if (Globals.siegeCells[i, f].wounded)
                    {
                        if (rand.Next(4) == 0)
                        {
                            Globals.siegeCells[i, f].SetNextDirection(SiegeFunctions.Direction.Destroy);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// RunAnimation is a static porcessor function used to change the state of Globals.Siegecells to create the next
        /// 'frame'.
        /// RunAnimation is where the actions decided in <see cref="SetUpSiegeAnimation(Random)"/> are put into effect in the form
        /// of a state change or 'new frame'
        /// </summary>
        public static void RunAnimation()
        {
            for (int i = 0; i < Globals.siegeCells.GetLength(1); i++)
            {
                for (int f = 0; f < Globals.siegeCells.GetLength(1); f++)
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
                    if (Globals.siegeCells[i, f].GetNextDirection() != SiegeFunctions.Direction.None)
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
            //Create new units and place them in the bottom row
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
        /// <summary>
        /// DispalyCells draws <see cref="Globals"/>'s SiegeCells array.
        /// Dispaly cell, for eachcell Type, decide a Solidbrush color, and then draws a 
        /// 20x20px square in that color, modiefied by nearby lighting. This drawing takes place
        /// in both an external bitmap and on a refrenced <see cref="Form"/>.
        /// Square are drawn left to right, top to bottom
        /// </summary>
        /// <param name="f">The Windows Form to Draw on</param>
        /// <param name="rand">An instance of <see cref="Random"/> to generate random objects</param>
        /// <param name="m">The bitmap to draw to</param>
        /// <param name="frame">The number 'frame' that is being drawn so the Form 'f' can have it's title edited to show progress</param>
        public static void DisplayCells(Form1 f,Random rand,Bitmap m,int frame=0)
        {

            int xOffset = 0;
            int yOffset = 0;
            Form1 form1 = f;
            form1.Text="SiegeGen Rendering Frame "+frame;
            SolidBrush brush  = new SolidBrush(Color.Transparent);
            Graphics g = Graphics.FromImage(m);

            Graphics gg = f.CreateGraphics();
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
                            brush.Color = Color.FromArgb(255, 100, 100, 100);
                            break;
                        case (SiegeFunctions.CellTypes.Night):
                            brush.Color = Color.FromArgb(255, 0, 0, 0);
                            break;
                        case (SiegeFunctions.CellTypes.CityProper):
                            brush.Color = Color.FromArgb(255, 72, 72, 72);
                            break;
                        case (SiegeFunctions.CellTypes.Rohan):

                            brush.Color = Color.FromArgb(255, 165, 255, 127);
                            break;
                        case (SiegeFunctions.CellTypes.Torch):

                            brush.Color = Color.FromArgb(255, 255, 216, 0);
                            break;
                        case (SiegeFunctions.CellTypes.BackgroundCity):

                            brush.Color = Color.FromArgb(255, 54, 54, 54);
                            break;
                        case (SiegeFunctions.CellTypes.Urkhai):

                            brush.Color = Color.FromArgb(255, 48, 48, 48);
                            
                            break;
                        case (SiegeFunctions.CellTypes.UrkhaiElite):

                            brush.Color = Color.FromArgb(255, 192, 192, 192);
                            break;
                       case (SiegeFunctions.CellTypes.Ladder):
                             brush.Color = Color.FromArgb(255, 127, 63, 63);
                            break;
                        case (SiegeFunctions.CellTypes.LadderStart):
                            brush.Color = Color.FromArgb(255, 127, 63, 63);
                            break;
                        default:
                                brush.Color = Color.FromArgb(165, 255, 0, 0);
                                break;


                    }
                    //Add Light between 10&&15 if torch nearby Non-ground,non-torch
                    if (GetNearbyCells(r, i, SiegeFunctions.CellTypes.Torch) && Globals.siegeCells[r, i].GetCellType()!=SiegeFunctions.CellTypes.Torch && Globals.siegeCells[r, i].GetCellType() != SiegeFunctions.CellTypes.Ground && Globals.siegeCells[r, i].GetCellType() != SiegeFunctions.CellTypes.BackgroundCity)
                    {
                        int Lightchange = rand.Next(10) +5;
                        brush.Color = Color.FromArgb(255, (brush.Color.R+ Lightchange)%255, (brush.Color.B+ Lightchange)%255, (brush.Color.G+ Lightchange)%255);
                    }
                    //If torch and nearbycell cityBackground, randomize brightness
                    if (GetNearbyCells(r, i, SiegeFunctions.CellTypes.BackgroundCity) && Globals.siegeCells[r, i].GetCellType() == SiegeFunctions.CellTypes.Torch)
                    {
                        int Lightchange = rand.Next(100) + 155;
                        brush.Color = Color.FromArgb(255,Lightchange,Lightchange,0);
                    }
                    //If WOunded, change color to red
                    if (Globals.siegeCells[r, i].wounded)
                    {
                        brush.Color = Color.FromArgb(255, (brush.Color.R+100)%255, brush.Color.G%100, brush.Color.B%100);
                    }
                    g.FillRectangle(brush,new Rectangle(new Point((20*r)+xOffset,(20*i)+yOffset),new Size(20,20)));
                    gg.FillRectangle(brush, new Rectangle(new Point((20 * r) + xOffset, (20 * i) + yOffset), new Size(20, 20)));
                    if (Globals.siegeCells[r, i].GetCellType() == SiegeFunctions.CellTypes.Urkhai || Globals.siegeCells[r, i].GetCellType() == SiegeFunctions.CellTypes.Rohan)
                    {
                        Pen smotherPen = new Pen(Color.LightGray);
                        for (int z = 0; z < 1; z++)
                        {
                            if (rand.Next(3) > 1)
                            {
                                int one = rand.Next(20) + (20 * r) + xOffset;
                                int two = rand.Next(20) + (20 * i) + yOffset;
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
                                gg.DrawLine(smotherPen, one, two, three, four);
                            }
                        }
                    }
                }
            }
            
            f.Update();

            //Thread.Sleep(1000);
            gg.Dispose();
            g.Dispose();
        }

        /// <summary>
        /// A utility function to see if a cell of type 'search' is adjacent to the specified co-ords
        /// </summary>
        /// <param name="x">The X value of the search cell</param>
        /// <param name="y">The Y value of the search cell</param>
        /// <param name="search">The 'cellType' to be searched for</param>
        /// <returns>Returns true if cell of type 'search' is present in Globals.siegecells[x,y]</returns>
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
    }
}
