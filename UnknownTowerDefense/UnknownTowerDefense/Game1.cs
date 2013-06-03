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

namespace UnknownTowerDefense
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Level level = new Level();
       // Wave wave;
        //Tower tower;
        WaveManager waveManager;
        Player player;
        Toolbar toolBar;
        Button arrowButton;
        SpriteFont font;
        ParticleEngine particleEngine;
        Texture2D   MainMenuTexture;
        Rectangle RecMainMenu;
        Texture2D LoadGameTexture;
        Rectangle RecLoadGameTexture;
        Texture2D AboutTexture;
        Rectangle RecAbout;
        Texture2D SettingsTexture;
        Rectangle RecSettings;
        Texture2D FutureDevelopmentTexture;
        Rectangle RecDevelopment;
        Rectangle RecLoadGame;
        Rectangle RecFutureDevelopment;
        
        bool Mainmenubool;
        bool LoadGamebool;
        bool FutureDevelopmentbool;
        bool Settingsbool;
        bool Aboutbool;
        
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //Set fullscreen
            graphics.IsFullScreen = false;
            if (graphics.IsFullScreen == false)
            {
                graphics.PreferredBackBufferWidth = level.Width * 32;
                graphics.PreferredBackBufferHeight = 32 + level.Height * 32;
            }
            graphics.ApplyChanges();
           
            graphics.ApplyChanges();
            IsMouseVisible = true;
            this.Window.Title = "UnknownTD";
            Mainmenubool = false;
            LoadGamebool = false;
            FutureDevelopmentbool = false;
            Settingsbool = false;
            Aboutbool = false;

            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
          //  graphics.PreferredBackBufferWidth = level.Width * 32;
          //  graphics.PreferredBackBufferHeight = level.Height * 32;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D grass = Content.Load<Texture2D>("grass");
            Texture2D path = Content.Load<Texture2D>("dirt");
            Texture2D bar = Content.Load<Texture2D>("bar");
            Texture2D arrowNormal = Content.Load<Texture2D>("tower");
            Texture2D arrowHover = Content.Load<Texture2D>("towerhover");
            Texture2D arrowPressed = Content.Load<Texture2D>("towerpressed");
            MainMenuTexture = Content.Load<Texture2D>("LoadGame");
            SettingsTexture = Content.Load<Texture2D>("LoadGame");
            LoadGameTexture = Content.Load<Texture2D>("LoadGame");
            AboutTexture = Content.Load<Texture2D>("LoadGame");


            
            RecSettings = new Rectangle (30, 30, SettingsTexture.Width, SettingsTexture.Height);
            RecAbout = new Rectangle(60,60, AboutTexture.Width, AboutTexture.Height);
            RecLoadGame = new Rectangle (90,90, LoadGameTexture.Width, LoadGameTexture.Height);
            RecMainMenu = new Rectangle(0, 0, MainMenuTexture.Width, MainMenuTexture.Height);

            arrowButton = new Button(arrowNormal, arrowHover, arrowPressed, new Vector2(0, level.Height * 32 + 2));
            arrowButton.Clicked += new EventHandler(arrowButton_Clicked);

            SpriteFont font = Content.Load<SpriteFont>("Arial");

            level.AddTexture(grass);
            level.AddTexture(path);

            Texture2D enemyTexture = Content.Load<Texture2D>("nerd");

            toolBar = new Toolbar(bar, font, new Vector2(0, level.Height * 32));
            //enemy1 = new Enemy(enemyTexture, Vector2.Zero, 100, 10, 0.5f);
           // enemy1.SetWaypoints(level.Waypoints);
           // wave = new Wave(0, 10, level, enemyTexture);
           // wave.start();
            List<Texture2D> particleTextures = new List<Texture2D>();
            Texture2D towerTexture = Content.Load<Texture2D>("tower_normal");
            Texture2D bulletTexture = Content.Load<Texture2D>("bullet");
            Texture2D healthBar = Content.Load<Texture2D>("healthbar");
            Texture2D blood = Content.Load<Texture2D>("blood");
            particleTextures.Add(blood);
            particleEngine = new ParticleEngine(particleTextures, new Vector2(400, 240), 10);

            player = new Player(level, towerTexture, bulletTexture);
            waveManager = new WaveManager(player, level, 24, enemyTexture, healthBar);

            //tower = new Tower(towerTexture, Vector2.Zero);
           
        }
      
        private void arrowButton_Clicked(object sender, EventArgs e)
        {
            player.NewTowerType = "Arrow Tower";
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            if (RecMainMenu.Contains(Mouse.GetState().X, Mouse.GetState().Y) &&(Mouse.GetState().LeftButton == ButtonState.Pressed ))
            {
                Mainmenubool = true;

            }
            if (RecFutureDevelopment.Contains(Mouse.GetState().X, Mouse.GetState().Y) &&(Mouse.GetState().LeftButton == ButtonState.Pressed))
            {
                FutureDevelopmentbool = true;
            }
            if (RecAbout.Contains(Mouse.GetState().X, Mouse.GetState().Y) && (Mouse.GetState().LeftButton == ButtonState.Pressed))
            {
                Aboutbool = true;
            }
            if (RecLoadGame.Contains(Mouse.GetState().X, Mouse.GetState().Y) && (Mouse.GetState().LeftButton == ButtonState.Pressed))
            {
                LoadGamebool = true;
            }
            if (RecSettings.Contains(Mouse.GetState().X, Mouse.GetState().Y) && (Mouse.GetState().LeftButton == ButtonState.Pressed))
            {
                Settingsbool = true;
            }
            if (Mainmenubool == true)
            {
                List<Enemy> enemies = new List<Enemy>();
                waveManager.Update(gameTime);
                arrowButton.Update(gameTime);
                player.Update(gameTime, waveManager.Enemies);
                // enemies.Add(wave);
            }
            
            if (LoadGamebool == true)
            {
              List<Enemy> enemies = new List<Enemy>();
                waveManager.Update(gameTime);
                arrowButton.Update(gameTime);
                player.Update(gameTime, waveManager.Enemies);
            }
            if (Settingsbool == true)
            {
                //put code here for settings
            }
            if (FutureDevelopmentbool == true)
            {
                //put code here fore future development
            }
            if (Aboutbool == true)
            {
                //put code here for about
            }
            


            drawParticle(Mouse.GetState().X, Mouse.GetState().Y);
                particleEngine.Update();


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 

        public void drawParticle(int X, int Y)
        {
            particleEngine.EmitterLocation = new Vector2(X, Y);
        }
        protected override void Draw(GameTime gameTime)
        {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                float frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (Mainmenubool == false)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(MainMenuTexture, RecMainMenu, Color.White);
                    particleEngine.Draw(spriteBatch);
                    spriteBatch.End();
                   
                }

                if (Mainmenubool == true)
                {
                    
                    spriteBatch.Begin();

                    SpriteFont font = Content.Load<SpriteFont>("Arial");

                    level.Draw(spriteBatch);

                    waveManager.Draw(spriteBatch);


                    toolBar.Draw(spriteBatch, player);
                    arrowButton.Draw(spriteBatch);
                    player.Draw(spriteBatch);

                    spriteBatch.DrawString(font, "FPS: " + (int)frameRate, new Vector2(10, 10), Color.Black);

                    particleEngine.Draw(spriteBatch);

                    spriteBatch.End();
                }
                if (Aboutbool == true)
                {
                    //put code here for drawing the about text
                }
                if (Settingsbool == true)
                {
                    //put code here for drawing the settings text
                }
                if (FutureDevelopmentbool == true)
                {
                    //put code here for drwaing futuredevelopment text
                }
                if (LoadGamebool == true)
                {
                    //put code here for drawing the loaded game.
                }


    base.Draw(gameTime);
        }
    }
}
