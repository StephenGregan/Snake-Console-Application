using System;
using System.Drawing;
using System.Windows;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SnakeConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            game:
            {
                Console.CursorVisible = (false);
                Console.Title = "Snake";

                Console.SetWindowSize(56, 38);

                Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();

                ConsoleColor bgColor = Console.BackgroundColor;
                ConsoleColor fgColor = Console.ForegroundColor;
                int delay = 100;
                string direction = "right";

                int snakeLength = 0;
                Random random = new Random();

                int score = 0;
                int x = 20;
                int y = 20;
                int colourTog = 1;
                bool alive = true;
                bool pelletOn = false;
                int pelletX = 0;
                int pelletY = 0;

                int[] xPoints;
                xPoints = new int[8] { 20, 19, 18, 17, 16, 15, 14, 13 };
                int[] yPoints = new int[8] { 20, 20, 20, 20, 20, 20, 20, 20 };

                while (alive)
                {
                    if (pelletOn == false)
                    {
                        bool collide = false;
                        pelletOn = true;
                        pelletX = random.Next(4, Console.WindowWidth -4);
                        pelletY = random.Next(4, Console.WindowWidth -4);

                        for (int l = (xPoints.Length -1); l > 1; l--)
                        {
                            if (xPoints[l] == pelletX & yPoints[l] == pelletY)
                            {
                                collide = true;
                            }
                        }
                        if (collide == true)
                        {
                            pelletOn = false;
                            break;
                        }
                        else
                        {
                            Console.SetCursorPosition(pelletX, pelletY);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.BackgroundColor = bgColor;
                            Console.WriteLine("#");
                            pelletOn = true;
                        }
                    }
                    Array.Resize<int>(ref xPoints, snakeLength);
                    Array.Resize<int>(ref yPoints, snakeLength);

                    Thread.Sleep(delay);
                    colourTog++;
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        switch (key.Key)
                        {
                            case ConsoleKey.RightArrow:
                                if (direction != "left")
                                {
                                    direction = "right";
                                }
                                break;
                            case ConsoleKey.LeftArrow:
                                if (direction != "right")
                                {
                                    direction = "left";
                                }
                                break;
                            case ConsoleKey.UpArrow:
                                if (direction != "down")
                                {
                                    direction = "up";
                                }
                                break;
                            case ConsoleKey.DownArrow:
                                if (direction != "up")
                                {
                                    direction = "down";
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    if (direction == "right")
                    {
                        x += 1;
                    }
                    else if (direction == "left")
                    {
                        x -= 1;
                    }
                    else if (direction == "down")
                    {
                        y += 1;
                    }
                    else if (direction == "up")
                    {
                        y -= 1;
                    }

                    xPoints[0] = x;
                    yPoints[0] = y;

                    for (int l = (xPoints.Length - 1); l > 0; l--)
                    {
                        xPoints[l] = xPoints[l - 1];
                        yPoints[l] = yPoints[l - 1];
                    }

                    try
                    {
                        Console.SetCursorPosition(xPoints[0], yPoints[0]);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        alive = false;
                    }
                    if (colourTog == 2)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                    }
                    else
                    {
                        colourTog = 1;
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    Console.ForegroundColor = fgColor;
                    Console.Write("*");

                    try
                    {
                        Console.SetCursorPosition(xPoints[xPoints.Length - 1], yPoints[yPoints.Length - 1]);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        alive = false;
                    }

                    Console.BackgroundColor = bgColor;
                    Console.Write(" ");

                    if (x == pelletX & y == pelletY)
                    {
                        pelletOn = false;
                        snakeLength += 1;
                        delay -= delay / 16;
                        new Thread(() => Console.Beep(320, 250)).Start();
                    }

                    for (int l = (xPoints.Length - 1); l > 1; l++)
                    {
                        if (xPoints[1] == xPoints[0] & yPoints[1] == yPoints[0])
                        {
                            alive = false;
                        }
                    }
                    score = ((snakeLength) - 8);
                    Console.SetCursorPosition(2, 2);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write("Score: {0} ", score);
                }
                new Thread(() => Console.Beep(37, 1)).Start();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
                Console.Beep(831, 250);
                Console.Beep(785, 250);

                ConsoleColor[] colours = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));

                for (int i = 0; i < 1; i++)
                {
                    foreach (var colour in colours)
                    {
                        Console.SetCursorPosition(0, 0);
                        Console.ForegroundColor = colour;
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\n");
                        Console.WriteLine("\n           Game Over: :(");
                        Console.WriteLine("\n\n         Your score was: {0} !", score);
                        Thread.Sleep(100);
                    }
                }
                Thread.Sleep(1000);
                Console.WriteLine("\n\n\n\n\n\n         -- Press any key to continue --");
                Thread.Sleep(500);
                Console.ReadKey(true);
                Console.ReadKey(true);
                goto game;
            }
        }
    }
}
