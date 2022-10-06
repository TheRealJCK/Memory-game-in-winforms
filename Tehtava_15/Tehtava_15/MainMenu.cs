using System.Windows.Forms;
using Tehtava_15.GameStructure; 
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;

namespace Tehtava_15
{
    public partial class MainMenu : Form
    {

        public Button Layout4x4 = new Button();
        public Button Layout6x6 = new Button();

        public static Controller _controller;
       

        public MainMenu()
        {

            BackGround.Scene(this, "Menu");
            CreateDifficultyButtons();
            InitializeComponent();

        }

        public void SetController(Controller contrroller)
        {
            _controller = contrroller;
        }


        // 1920; 1080

        //Button size 3x6 = 280; 280 and move 40 px 
        // 1. row 20; 50
        // 2. row 20, 385
        // 3. row 20; 730

        //Button size 6x6 = 175; 240 and move 210 px to right
        // 1. row 15, 35
        // 2. row 15, 285
        // 3. row 15, 540
        // 4. row 15, 790

        //  Makes the game with the 3x6 values.

        private async void Easy(object sender, EventArgs e)   
        {
            string dif = ((Button)sender).Name;
            _controller.GetInfo(dif);

            Controls.Remove(Layout6x6);
            Controls.Remove(Layout4x4);

            _controller.MakeFundamentals();

            await Task.Delay(800);
            _controller.ShowAllAnswer();
            await Task.Delay(2250);
            _controller.HideAllAnswer();
            _controller.Timer(true);
        }

        //  Makes the game with the 6x6 values.
        private void Hard(object sender, EventArgs e)
        {
            string dif = ((Button)sender).Name;

            _controller.GetInfo(dif);

            Controls.Remove(Layout6x6);
            Controls.Remove(Layout4x4);

            _controller.MakeFundamentals();
            _controller.Timer(true);

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1920, 1080);

        }


       public Controller ReturnControllerObject()
       {
            return _controller;
       }

        public void CreateDifficultyButtons()
        {
            // Button sizes and values
            //3x6
            Layout4x4.Size = new Size(300, 200);
            Layout4x4.FlatAppearance.MouseDownBackColor = Color.Transparent;
            Layout4x4.FlatAppearance.MouseOverBackColor = Color.Transparent;
            Layout4x4.FlatAppearance.BorderSize = 0;
            Layout4x4.FlatStyle = FlatStyle.Flat;
            Layout4x4.BackColor = Color.Transparent;
            Layout4x4.Visible = true;
            Layout4x4.Click += new EventHandler(Easy);
            Layout4x4.Location = new Point(460, 540);
            Layout4x4.UseVisualStyleBackColor = true;

            //6x6
            Layout6x6.Visible = true;
            Layout6x6.Size = new Size(300, 200);
            Layout6x6.Name = "Hard";
            Layout6x6.Location = new Point(1260, 540);
            Layout6x6.UseVisualStyleBackColor = true;
            Layout6x6.FlatAppearance.MouseDownBackColor = Color.Transparent;
            Layout6x6.FlatAppearance.MouseOverBackColor = Color.Transparent;
            Layout6x6.FlatStyle = FlatStyle.Flat;
            Layout6x6.Click += new EventHandler(Hard);
            Layout6x6.FlatAppearance.BorderSize = 0;
            Layout6x6.BackColor = Color.Transparent;

            //Texts and its values
            //3x6
            Layout4x4.Text = "EASY";
            Layout4x4.ForeColor = Color.LimeGreen;
            Layout4x4.Font = new Font("Stencil", 16.2F, FontStyle.Bold, GraphicsUnit.Point);

            //6x6
            Layout6x6.Text = "HARD";
            Layout6x6.ForeColor = Color.Red;
            Layout6x6.Font = new Font("Stencil", 16.2F, FontStyle.Bold, GraphicsUnit.Point);

            //Add things?
            Controls.Add(Layout4x4);
            Controls.Add(Layout6x6);

        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();

        }
    }
}