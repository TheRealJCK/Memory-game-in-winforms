using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using Tehtava_15.Properties;
using System.Drawing;
using System.IO;
using System.Text;

namespace Tehtava_15.GameStructure
{
    public class GameCards
    {

        public List<Image> _allImages = new List<Image>();
        public List<Image> _gameImages = new List<Image>();
        
        public Dictionary<Button, Image> _memoryCards = new Dictionary<Button, Image>();

        public bool evenOrUneven = true;
        public int id = 1;
        public Size size;
        public Image frontImage;
        public Random rand = new Random();
        public int count;


        public GameCards()
        {

        }


        /// <summary>
        /// Resize the image to right size
        /// </summary>
        /// <param name="imgToResize"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Image ResizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }


        /// <summary>
        /// Easy way to call random method.
        /// </summary>
        /// <returns></returns>
        public int Randomiser<T>(List<T> list)
        {
            int randomOrder = rand.Next(list.Count);
            return randomOrder;
        }


        /// <summary>
        /// Creates cards from images and buttons
        /// </summary>
        /// <param name="_this"></param>
        /// <param name="numberOfCards"></param>
        public void CreateCards(Form _this, bool easyGmae)
        {

            if (easyGmae == true)
            {
                size = new Size(280, 280);
            }
            else
            {
                size = new(175, 240);
            }

            // Adds "Card1-18" named images from Resources file to a list

            var resources = Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
            foreach (DictionaryEntry myResource in resources)
            {

                string name = myResource.Key.ToString();
                Image Image = myResource.Value as Image;

                if (name.Contains("CardBG"))
                {
                    continue;

                }
                else if (name.Contains("Card"))
                {
                   
                    Image fImg = ResizeImage(Image, size);
                    _allImages.Add(fImg);
                    count++;
                }
            }

            int number = Randomiser(_allImages);

            // make duplicates
            foreach (var button in _this.Controls.OfType<Button>())
            {
                frontImage = _allImages[number];

                if (evenOrUneven == true)
                {
                    _gameImages.Add(frontImage);
                    evenOrUneven = false;
                }
                else
                {
                    _gameImages.Add(frontImage);
                    _allImages.RemoveAt(number);
                    number = Randomiser(_allImages);

                    evenOrUneven = true;
                }
            }

            // Makes cards by adding button as a key and image as a value
            foreach (var button in _this.Controls.OfType<Button>())
            {
                int key = Randomiser(_gameImages);


                if (key == _gameImages.Count)
                {
                    key--;
                }
                Image frontImg = _gameImages[key];


                // set name as an id for the future comparison
                SetId(button, frontImg, key);

            }

        }

        public void SetId(Button buttonThatNeedToBeIded, Image frontImage, int Key)
        {
            buttonThatNeedToBeIded.Name = $"{id}";
            _memoryCards.Add(buttonThatNeedToBeIded, frontImage);
            _gameImages.Remove(_gameImages[Key]);
            id++;
        }

    }
}



