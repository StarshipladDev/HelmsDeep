using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class SiegeFunctions
    {
        public enum CellTypes
        {
            Ladder,
            Urkhai,
            Torch,
            Wall,
            LadderStart,
            Grate,
            Rohan,
            CityProper,
            Empty,
            WallTop,
            UrkhaiElite,
            Night,
            Ground,
            BackgroundCity


        }
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right,
            None
        }
        public class SiegeCell
        {
            public bool wounded = false;
            CellTypes cellType;
            Direction lastDirection;
            Direction nextDirection;
            public SiegeCell(Direction d,CellTypes c)
            {
                cellType = c;
                lastDirection = d;
                nextDirection = Direction.None;

            }
            public CellTypes GetCellType()
            { 
                return cellType;
            }
            public Direction GetLastDirection()
            {
                return lastDirection;
            }
            public Direction GetNextDirection()
            {
                return nextDirection;
            }
            public void SetNextDirection(Direction d)
            {
                this.nextDirection = d;
            }
            public void SetCellType(CellTypes c)
            {
                this.cellType = c;
            }
        }
    }
}
