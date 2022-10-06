using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tehtava_15.GameStructure
{
    public class Card : Button
    {
        GameCards _fromGameCardsClass;
        Controller _contreoller = new();
        MainMenu _mainMenu = new();

        public Point location;
        public bool firstPressedOrSecond = true;

        Bitmap cardsBackSideImg;

        Button[] card;
        Button answer;
        Button firsPressed;
        Button secondPressed;
        Button tryAgainButton;
        Button leaveButton;

        TextBox endSceneTextBox;

        public Card()
        {

        }

        public int GameCardPreSets(Button theCard, Form form, Image cardsBackSideImg , Point location, Size size, int nextButtonLocation)
        {

            theCard.FlatAppearance.MouseDownBackColor = Color.Transparent;
            theCard.FlatAppearance.MouseOverBackColor = Color.Transparent;
            theCard.FlatAppearance.BorderSize = 0;
            theCard.FlatStyle = FlatStyle.Flat;
            theCard.BackColor = Color.Transparent;
            theCard.UseVisualStyleBackColor = true;

            theCard.TabStop = false;
            theCard.FlatAppearance.BorderSize = 0;
            theCard.Visible = true;

            theCard.Image = cardsBackSideImg;

            theCard.Size = size;
            theCard.Location = location;


            theCard.Click += new EventHandler(CardClicked);

            return location.X;
        }

        public void EndGameButtonPreSets(Button endButton , Point location, Size size, string option, Color color)
        {
            endButton.Size = size;
            endButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            endButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            endButton.FlatAppearance.BorderSize = 0;
            endButton.FlatStyle = FlatStyle.Flat;
            endButton.BackColor = Color.Transparent;
            endButton.Visible = true;
            endButton.Click += new EventHandler(EndSceneClick);
            endButton.Location = location;
            endButton.UseVisualStyleBackColor = true;
            endButton.Text = option;
            endButton.ForeColor = color;
            endButton.Font = new Font("Stencil", 16.2F, FontStyle.Bold, GraphicsUnit.Point);

        }

        /// <summary>
        /// /Todo: split to smaller methods
        /// </summary>
        /// <param name="form"></param>
        /// <param name="round"></param>
        public void CreateButtons(Form form, int round)
        {
            int hardButtonAmount = 36;
            while (true)
            {
                card = new Button[round];
                //Makes 36 buttons for the memory game.
                if (round == hardButtonAmount)
                {
                    cardsBackSideImg = new Bitmap(Properties.Resources.CardBG, new Size(175, 240));
                    location = new Point(20, 50); 
                    int nextButton = 210;

                    for (int i = 0; i < round; i++)
                    {
                        var buttonValues = new Button();
                        int locationXDefault = 15;

                        int x = location.X;
                        int y = location.Y;
                        if (i == 9 || i == 27)
                        {
                            location.Y = y + 250;
                            location.X = locationXDefault;
                        }
                        else if (i == 18)
                        {
                            location.Y = y + 255;
                            location.X = locationXDefault;
                        }
                        
                        int pastLocation = GameCardPreSets(buttonValues, form, cardsBackSideImg, location, new Size(175, 240) , nextButton);
                        location.X  = pastLocation + nextButton;

                        card[i] = buttonValues;
                       form.Controls.Add(card[i]);
                    };
                }

                //Makes 16 buttons for the memory game.
                else
                {
                    cardsBackSideImg = new Bitmap(Properties.Resources.CardBG, new Size(280, 280));
                    location = new Point(15, 35);
                    int nextButton = 317;

                    for (int i = 0; i < round; i++)
                    {
                        var buttonValues = new Button();
                        int x = location.X;
                        int y = location.Y;
                        if (i == 6)
                        {
                            location.Y = y = 385;
                            x = 20;
                            location.X = x;
                        }
                        else if (i == 12)
                        {
                            location.Y = y = 730;
                            x = 20;
                            location.X = x;
                        }

                        int pastLocation = GameCardPreSets(buttonValues, form, cardsBackSideImg, location, new Size(280, 280), nextButton);
                        location.X = pastLocation + nextButton;

                        card[i] = buttonValues;
                        form.Controls.Add(card[i]);
                    };
                }              
                break;  
            }
        }

        public async void CardClicked(object sender, EventArgs e)
        {
            _contreoller = _mainMenu.ReturnControllerObject();
            _fromGameCardsClass = _contreoller.ReturnGameCardsObject();

            if (firstPressedOrSecond == true)
            {
                firsPressed = sender as Button;

                Image frontImageOne = _fromGameCardsClass._memoryCards[firsPressed];
                firsPressed.Image = frontImageOne;
                firstPressedOrSecond = false;
            }
            else
            {
                secondPressed = sender as Button;

                Image frontImageTwo = _fromGameCardsClass._memoryCards[secondPressed];
                secondPressed.Image = frontImageTwo;

                if (firsPressed.Name == secondPressed.Name)
                {
                    MessageBox.Show("You Pressed the same button TWICE");
                    ChangeImageToBG(firsPressed, secondPressed);
                }
                else
                {
                    await Task.Delay(200);
                    _contreoller.CheckIfCardsAreSame(secondPressed, firsPressed);
                    _contreoller.CheckIfGameHaveEnded();
                }
                firstPressedOrSecond = true;
            }
        }

        public void ChangeImageToBG(Button fistButton, Button secondButton)
        {
            fistButton.Image = cardsBackSideImg;
            secondButton.Image = cardsBackSideImg;
        }

        public void CreateEndScene(string time, MainMenu mainForm)
        {
            endSceneTextBox = new TextBox();
            endSceneTextBox.ForeColor = Color.Black;
            endSceneTextBox.BorderStyle = BorderStyle.None;
            endSceneTextBox.Font = new Font("Italianate", 26.25F, FontStyle.Bold, GraphicsUnit.Point);
            endSceneTextBox.Location = new Point(400, 210);
            endSceneTextBox.Size = new Size(1200, 51);
            endSceneTextBox.Text = $"Congratulations! Your time of completion is {time}";

            tryAgainButton = new Button();
            leaveButton = new Button();
            location = new Point(400, 800);
            EndGameButtonPreSets(tryAgainButton, location, new Size(365, 225), "Try again", Color.LimeGreen);
            location = new Point(1240, 800);
            EndGameButtonPreSets(leaveButton, location, new Size(365, 225), "Leave", Color.Red);

            mainForm.Controls.Add(tryAgainButton);
            mainForm.Controls.Add(leaveButton);
            mainForm.Controls.Add(endSceneTextBox);
        }

        public void EndSceneClick(object sender, EventArgs e)
        {
            answer = sender as Button;

            if (answer.Text == "Leave")
            {
                Application.Exit();
            }
            else
            {
                Application.Restart();
            }
        }
    }
}