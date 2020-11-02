using System;
using System.Diagnostics;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsApp1;

namespace HelmsDeepUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// CellsNotNull runs the initation script that sets all cells to a default, and
        /// then checks if all cells in <see cref="Globals.siegeCells"/> are not null.
        /// </summary>
        [TestMethod]

        public void CellsNotNull()
        {

            bool pass = true;
            if (true)
            {

                WorkerFunctions.HelmsDeepButton(new Form1(), 1);
                for (int i = 0; i < Globals.height; i++)
                {
                    for (int f = 0; f < Globals.height; f++)
                    {
                        if (Globals.siegeCells[0, 0] == null)
                        {
                            pass = false;
                        }
                    }
                }
            }

            if (pass)
            {
                Assert.AreNotEqual(pass, false);
            }
            else
            {

                Assert.Fail();
            }
        }
        /// <summary>
        /// WallHeightValidArray confirms that the 'wallheight' array is of valid data and size
        /// </summary>
        [TestMethod]
        public void WallHeightValidArray()
        {
            bool pass = true;
            if (Globals.height > Globals.wallHeight.Length)
            {
                Assert.Fail();
            }
            for (int i = 0; i < Globals.height; i++)
            {
                if (Globals.wallHeight[i] < 0 || Globals.wallHeight[i] > Globals.height)
                {
                    pass = false;
                }
            }
            Assert.IsTrue(pass);
        }
        /// <summary>
        /// Wall across takes the above pre-generated Globals and confirms all cells are either within the
        /// parameters set by wallheight, ot not walls
        /// </summary>
        [TestMethod]
        public void WallsAcross()
        {
            bool pass = true;
            for (int i = 0; i < Globals.height; i++)
            {
                for (int f = 0; f < Globals.height; f++)
                {
                    if (Globals.siegeCells[i, f].GetCellType() == SiegeFunctions.CellTypes.Wall)
                    {
                        if (f - Globals.wallHeight[i] < 0 || f - Globals.wallHeight[i] > 2)
                        {

                            Assert.Fail(String.Format("Failing on wall  in non-valid spot with x,y,wallheight : {0} {1} {2}", i, f, Globals.wallHeight[i]));
                            pass = false;
                        }
                    }
                    if (Globals.wallHeight[i] + 1 == f || Globals.wallHeight[i] + 2 == f)
                    {
                        if (Globals.siegeCells[i, f].GetCellType() != SiegeFunctions.CellTypes.Wall)
                        {
                            Assert.Fail("Failing on wallheight array not wall");
                            pass = false;
                        }
                    }
                }
                if (pass)
                {
                    Assert.AreEqual(pass, true);
                }
                else
                {

                    Assert.Fail();
                }

            }
        }
        /// <summary>
        /// AllCellsNotOneType checks to see if any cells in the <see cref="Globals.siegeCells"/> array are of the type 'Empty',
        /// As this celltype should never come up.
        /// </summary>
        [TestMethod]
        public void AllCellsNotNoneType()
        {
            bool pass = true;
            for (int i = 0; i < Globals.height; i++)
            {
                for (int f = 0; f < Globals.height; f++)
                {
                    if (Globals.siegeCells[i,f].GetCellType()==SiegeFunctions.CellTypes.Empty)
                    {
                        Assert.Fail(String.Format("Found and empty cell at x,y {0},{1}",i,f));
                        pass = false;
                    }
                }
            }
            Assert.IsTrue(pass);
        }

    }
}
