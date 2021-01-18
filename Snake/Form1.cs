using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class SnakeGame : Form
    {
        private static Snake snake = new Snake();
        private Square food = new Square();
        public SnakeGame()
        {
            InitializeComponent();

            new Settings(); // linking the Settings Class to this Form

            gameTimer.Interval = 1000 / Settings.Speed; // Changing the game time to settings speed
            gameTimer.Tick += updateSreen; // linking a updateScreen function to the timer
            gameTimer.Start(); // starting the timer

            startGame(); // running the start game function
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            Input.changeState(e.KeyCode, true);
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            Input.changeState(e.KeyCode, false);
        }

        private void updateGraphics(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics; // create a new graphics class called canvas

            if (Settings.GameOver == false)
            {
                // if the game is not over then we do the following

                Brush snakeColour; // create a new brush called snake colour

                for (int i = 0; i < snake.Length(); i++)
                {
                    snakeColour = Brushes.Green;

                    canvas.FillRectangle(snakeColour, new Rectangle(snake.GetSquare(i).X * Settings.Width, snake.GetSquare(i).Y * Settings.Height, Settings.Width, Settings.Height));
                }

                canvas.FillRectangle(Brushes.Red, new Rectangle(food.X * Settings.Width, food.Y * Settings.Height, Settings.Width, Settings.Height));
            }
            else
            {
                string gameOver = "Game Over \n Du hast " + Settings.Score + " Punkte erreicht. \n Drücke ENTER um erneut zu spielen.";
                label3.Text = gameOver;
                label3.Visible = true;
            }
        }

        private void startGame()
        {
            label3.Visible = false;
            new Settings();
            snake.Clear();
            snake.Add(new Square { X = 10, Y = 5 });

            label2.Text = Settings.Score.ToString();

            generateFood();
        }

        private void movePlayer()
        {
            for (int i = snake.Length()-1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (Settings.direction)
                    {
                        case Directions.Down:
                            snake.GetSquare(0).Y++;
                            break;
                        case Directions.Up:
                            snake.GetSquare(0).Y--;
                            break;
                        case Directions.Left:
                            snake.GetSquare(0).X--;
                            break;
                        case Directions.Right:
                            snake.GetSquare(0).X++;
                            break;

                    }

                    int maxXpos = pbCanvas.Size.Width / Settings.Width;
                    int maxYpos = pbCanvas.Size.Height / Settings.Height;

                    if (snake.GetSquare(i).X < 0 || snake.GetSquare(i).Y < 0 || snake.GetSquare(i).X > maxXpos || snake.GetSquare(i).Y > maxYpos)
                        die();

                    if (snake.GetSquare(0).X == food.X && snake.GetSquare(0).Y == food.Y)
                        eat();
                }
                else
                {
                    snake.GetSquare(i).X = snake.GetSquare(i - 1).X;
                    snake.GetSquare(i).Y = snake.GetSquare(i - 1).Y;
                }
            }
        }

        private void generateFood()
        {
            int maxXpos = pbCanvas.Size.Width / Settings.Width;
            int maxYpos = pbCanvas.Size.Height / Settings.Height;

            Random rnd = new Random();
            int tempX = rnd.Next(0, maxXpos);
            int tempY = rnd.Next(0, maxYpos);

            Console.WriteLine(snake.Length());

            for(int i = 0; i < snake.Length(); i++)
            {
                Square tempSquare = snake.GetSquare(i);

                if (tempSquare.X == tempX && tempSquare.Y == tempY)
                {
                    generateFood();
                    return;
                }
            }

            food = new Square { X = tempX, Y = tempY };
        }
        private void eat()
        {
            Settings.Score += Settings.Points;
            Square body = new Square { X = snake.GetSquare(snake.Length() - 1).X, Y = snake.GetSquare(snake.Length() - 1).Y };
            snake.Add(body);
            label2.Text = Settings.Score.ToString();
            generateFood();
        }
        private void die()
        {
            Settings.GameOver = true;
        }

        private void updateSreen(object sender, EventArgs e)
        {
            // this is the Timers update screen function. 
            // each tick will run this function

            if (Settings.GameOver == true)
            {

                // if the game over is true and player presses enter
                // we run the start game function

                if (Input.KeyPress(Keys.Enter))
                {
                    startGame();
                }
            }
            else
            {
                //if the game is not over then the following commands will be executed

                // below the actions will probe the keys being presse by the player
                // and move the accordingly

                if (Input.KeyPress(Keys.Right) && Settings.direction != Directions.Left)
                {
                    Settings.direction = Directions.Right;
                }
                else if (Input.KeyPress(Keys.Left) && Settings.direction != Directions.Right)
                {
                    Settings.direction = Directions.Left;
                }
                else if (Input.KeyPress(Keys.Up) && Settings.direction != Directions.Down)
                {
                    Settings.direction = Directions.Up;
                }
                else if (Input.KeyPress(Keys.Down) && Settings.direction != Directions.Up)
                {
                    Settings.direction = Directions.Down;
                }

                movePlayer(); // run move player function
            }

            pbCanvas.Invalidate(); // refresh the picture box and update the graphics on it

        }
    }
}
