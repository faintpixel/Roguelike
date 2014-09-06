using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using xnaHelper.Helpers;
using xnaHelper.Primitives;
using xnaHelper.Input;
using DodongosQuest.Screens.Gameplay;
using DodongosQuest.Screens;
using DodongosQuest.Screens.Game_Over;
using System.Configuration;
using DodongosQuest.ParticleSystem;

namespace DodongosQuest
{

    public class GameReference
    {
        private static Game1 game;

        public static Game1 Game
        {
            get { return game; }
            set { game = value; }
        }

    }
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;

        private IScreen _currentScreen;
        private MouseState _previousMouseState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            ContentHelper.Content = Content;
            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 768;
            if (ConfigurationSettings.AppSettings["RunInFullScreenMode"] == "1")
                _graphics.IsFullScreen = true;
            GameReference.Game = this;
        }

        protected override void Initialize()
        {
            this.Window.Title = "Dodongo's Quest v0.0.0";
            this.IsMouseVisible = true;
            _previousMouseState = Mouse.GetState();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            GraphicsHelper.spriteBatch = new SpriteBatch(GraphicsDevice);
            GraphicsHelper.primitiveBatch = new PrimitiveBatch(GraphicsDevice);
            GraphicsHelper.graphicsDevice = GraphicsDevice;

            GameplayScreen gameplayScreen = new GameplayScreen();
            gameplayScreen.GameOver += new EventHandler(gameplayScreen_GameOver);
            _currentScreen = gameplayScreen;
        }

        void gameplayScreen_GameOver(object sender, EventArgs e)
        {
            _currentScreen = new GameOverScreen();
        }



        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            _currentScreen.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GraphicsHelper.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            _currentScreen.Draw(gameTime);

            GraphicsHelper.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
