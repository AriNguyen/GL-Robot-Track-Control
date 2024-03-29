﻿using System;
using OpenTK;
using GLTrackControl.Graphics;

namespace GLTrackControl
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            GameWindow window = new GameWindow(800, 800);
            Game game = new Game(window, 20, 20, 20, 4);
            game.Start();
        }
    }
}
