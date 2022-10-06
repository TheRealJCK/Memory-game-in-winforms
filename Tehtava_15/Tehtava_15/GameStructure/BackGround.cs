using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tehtava_15.GameStructure
{
    public class BackGround
    {
        public  BackGround()
        {

        }

        /// <summary>
        /// Changes Scene when needed
        /// </summary>
        public static void Scene(MainMenu key, string sceneName)
        {
            if (sceneName == "Game")
            {
                key.BackgroundImage = Properties.Resources.GameBG;
            }
            if (sceneName == "Victory")
            {
                key.BackgroundImage = Properties.Resources.EndBG;                
            }
            else
            {
                key.BackgroundImage = Properties.Resources.MenuBG;
            }

            key.BackgroundImageLayout = ImageLayout.Stretch;
        }

    }
}
