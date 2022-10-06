using System.Diagnostics;

namespace Tehtava_15.GameStructure
{
    public class Controller 
    {
        MainMenu _mainMenu;
        Card _card;
        GameCards _gameCards = new GameCards();

        Stopwatch timer = new();
        static string difficulty;

        public Controller()
        {

        }

        public Controller(MainMenu form)
        {
            _mainMenu = form;
            _mainMenu.SetController(this);
        }
        
        public void GetInfo(string diff)
        {
            difficulty = diff;
            BackGround.Scene(_mainMenu, "Game");
        }

        public  void MakeFundamentals()
        {
            _card = new();
            if (difficulty == "Hard")
            {
                int hardGameDifficulty = 36;
                _card.CreateButtons(_mainMenu, hardGameDifficulty);
                _gameCards.CreateCards(_mainMenu, false);
            }
            else
            {
                int easyGameDifficulty = 18;
                _card.CreateButtons(_mainMenu, easyGameDifficulty);
                _gameCards.CreateCards(_mainMenu, true);
            }
        }

        public  void ShowAllAnswer()
        {
            foreach (var button in _mainMenu.Controls.OfType<Button>())
            {
                Image frontImg = _gameCards._memoryCards[button];

                button.Image = frontImg;
            }
        }

        public  void HideAllAnswer()
        {
            foreach (var button in _mainMenu.Controls.OfType<Button>())
            {
                Image backImage;

                if (_gameCards._memoryCards.Count() == 36)
                {
                    backImage = new Bitmap(Properties.Resources.CardBG, new Size(175, 240));
                }
                else
                {
                    backImage = new Bitmap(Properties.Resources.CardBG, new Size(280, 280));
                }
                button.Image = backImage;
            }
        }

        public  GameCards ReturnGameCardsObject()
        {
            return _gameCards;
        }
        
        public  void CheckIfCardsAreSame(Button firstOne, Button SecondOne)
        {

            if (firstOne.Image == SecondOne.Image)
            {
                _mainMenu.Controls.Remove(firstOne);
                _mainMenu.Controls.Remove(SecondOne);
            }
            else
            {
                _card.ChangeImageToBG(SecondOne, firstOne);
            }
        }

        public void Timer(bool startOrEnd)
        {
            if(startOrEnd == true)
            {
                timer.Start();

            }
            else
            {
                timer.Stop();

                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    timer.Elapsed.Hours,
                    timer.Elapsed.Minutes, timer.Elapsed.Seconds,
                    timer.Elapsed.Milliseconds / 10);
                _card.CreateEndScene(elapsedTime, _mainMenu);

            }
        }
        /// <summary>
        /// Check if game have ended and stops the time
        /// </summary>
        public void CheckIfGameHaveEnded()
        {
            if (_mainMenu.Controls.OfType<Button>().Count() == 0)
            {
                BackGround.Scene(_mainMenu, "Victory");
                Timer(false);
            }
        }

    }
}