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
        Texture2D MainMenuTexture;
        Rectangle RecMainMenu;
        bool isMainMenuClicked;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = level.Width * 32;
            graphics.PreferredBackBufferHeight = 32 + level.Height * 32;
            graphics.ApplyChanges();
            IsMouseVisible = true;
            isMainMenuClicked = false;
            



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

            Texture2D towerTexture = Content.Load<Texture2D>("tower_normal");
            Texture2D bulletTexture = Content.Load<Texture2D>("bullet");
            Texture2D healthBar = Content.Load<Texture2D>("healthbar");

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
            waveManager.Update(gameTime);


            List<Enemy> enemies = new List<Enemy>();

            arrowButton.Update(gameTime);

           // enemies.Add(wave);


            player.Update(gameTime, waveManager.Enemies);



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
                GraphicsDevice.Clear(Color.CornflowerBlue);


    spriteBatch.Begin(); 


    level.Draw(spriteBatch);

    waveManager.Draw(spriteBatch);
   
    player.Draw(spriteBatch);
    toolBar.Draw(spriteBatch, player);
    arrowButton.Draw(spriteBatch);

            
    spriteBatch.End();


    base.Draw(gameTime);
        }
    }
}
