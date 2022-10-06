using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tehtava_15.GameStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tehtava_15.GameStructure.Tests
{
    [TestClass()]
    public class ControllerTests
    {
        [TestMethod()]
        public void CheckIfGameHaveEndedTest()
        {
            // Arrange

            MainMenu view = new MainMenu();
            Controller _controller = new(view);
            Application.Run(view);
            for (int i = 0; i < 20; i++)
            {
                Button b = new();
                view.Controls.Add(b);
            }

            // Act

            //view.Controls.OfType<Button>().Count     == 0;
            
           //   Assert(view, false);
            
            
     
        }
    }
}