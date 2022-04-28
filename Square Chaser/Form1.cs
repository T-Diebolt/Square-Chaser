using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace Square_Chaser
{
    public partial class Form1 : Form
    {
        Rectangle player1 = new Rectangle(226, 226, 24, 24);
        Rectangle player2 = new Rectangle(250, 226, 24, 24);
        Rectangle objective = new Rectangle();
        Rectangle boost = new Rectangle();

        Random random = new Random();
        
        int player1Speed = 6;
        int player2Speed = 6;
        int player1Score = 0;
        int player2Score = 0;

        int boost1Count = 0;
        int boost2Count = 0;
        int boostMoveCount = 0;

        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;

        SolidBrush greenBrush = new SolidBrush(Color.Lime);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush cyanBrush = new SolidBrush(Color.Cyan);
        SolidBrush pinkBrush = new SolidBrush(Color.Magenta);
        SolidBrush purpleBrush = new SolidBrush(Color.DarkViolet);

        SoundPlayer pointSound = new SoundPlayer(Properties.Resources.Point_Get);
        SoundPlayer winSound = new SoundPlayer(Properties.Resources.Win);
        SoundPlayer boostSound = new SoundPlayer(Properties.Resources.Boost);

        public Form1()
        {
            InitializeComponent();
            winLabel.Text = "PRESS ANY KEY";
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Player start function
            if (gameEngine.Enabled == false && player1Score == 0 && player2Score == 0)
            {
                gameEngine.Enabled = true;
                winLabel.Text = "";
                objective = new Rectangle(random.Next(40, 480), random.Next(40, 480), 20, 20);
                boost = new Rectangle(random.Next(40, 490), random.Next(1, 490), 10, 10);
            }

            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }
        }
    

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
        switch (e.KeyCode)
        {
            case Keys.W:
                wDown = false;
                break;
            case Keys.S:
                sDown = false;
                break;
            case Keys.A:
                aDown = false;
                break;
            case Keys.D:
                dDown = false;
                break;
            case Keys.Up:
                upArrowDown = false;
                break;
            case Keys.Down:
                downArrowDown = false;
                break;
            case Keys.Left:
                leftArrowDown = false;
                break;
            case Keys.Right:
                rightArrowDown = false;
                break;
        }
    }

        private void gameEngine_Tick(object sender, EventArgs e)
        {
            
            //move player 1 
            if (wDown == true && player1.Y > 40)
            {
                player1.Y -= player1Speed;
            }

            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += player1Speed;
            }

            if (aDown == true && player1.X > 0)
            {
                player1.X -= player1Speed;
            }

            if (dDown == true && player1.X < this.Width - player1.Width)
            {
                player1.X += player1Speed;
            }

            //move player 2 
            if (upArrowDown == true && player2.Y > 40)
            {
                player2.Y -= player2Speed;
            }

            if (downArrowDown == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += player2Speed;
            }

            if (leftArrowDown == true && player2.X > 0)
            {
                player2.X -= player2Speed;
            }

            if (rightArrowDown == true && player2.X < this.Width - player2.Width)
            {
                player2.X += player2Speed;
            }

            //boost
            if(boost1Count == 1 || boost2Count == 1)
            {
                boost = new Rectangle(random.Next(40, 490), random.Next(1, 490), 10, 10);
            }
            
            if(boost1Count == 0)
            {
                player1Speed = 6;
            }
            else if(boost1Count > 0)
            {
                boost1Count--;
            }
            
            if(boost2Count == 0)
            {
                player2Speed = 6;
            }
            else if (boost2Count > 0)
            {
                boost2Count--;
            }
            
            if (player1.IntersectsWith(boost))
            {
                player1Speed = player1Speed + 2;
                boost1Count = 100;
                boost.X = boost.X - 500;
                boostSound.Play();
            }
            else if (player2.IntersectsWith(boost))
            {
                player2Speed = player2Speed + 2;
                boost2Count = 100;
                boost.X = boost.X - 500;
                boostSound.Play();
            }

            

            //player reachees objective and/or wins
            if (player1.IntersectsWith(objective))
            {
                player1Score++;
                p1ScoreLabel.Text += "I";
                objective = new Rectangle(random.Next(40, 480), random.Next(40, 480), 20, 20);
                pointSound.Play();
            }
            else if (player2.IntersectsWith(objective))
            {
                player2Score++;
                p2ScoreLabel.Text += "I";
                objective = new Rectangle(random.Next(40, 480), random.Next(40, 480), 20, 20);
                pointSound.Play();
            }

            if(player1Score == 5)
            {
                winLabel.Text = "P1 WINS!!!";
                winSound.Play();
                gameEngine.Enabled = false;
                Refresh();
                Restart();
                
            }
            else if(player2Score == 5)
            {
                winLabel.Text = "P2 WINS!!!";
                winSound.Play();
                gameEngine.Enabled = false;
                Refresh();
                Restart();
            }

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(pinkBrush, player1);
            e.Graphics.FillRectangle(purpleBrush, player2);
            e.Graphics.FillRectangle(greenBrush, objective);
            e.Graphics.FillRectangle(cyanBrush, boost);
        }

        private void Restart()
        {
            Thread.Sleep(3000);
            winLabel.Text = "PRESS ANY KEY";
            player1Score = 0;
            player2Score = 0;
            player1Speed = 6;
            player2Speed = 6;
            p1ScoreLabel.Text = "";
            p2ScoreLabel.Text = "";
            player1 = new Rectangle(226, 226, 24, 24);
            player2 = new Rectangle(250, 226, 24, 24);
            objective = new Rectangle();
            boost = new Rectangle();
            Refresh();
            Thread.Sleep(1000);
        }

    }

}
