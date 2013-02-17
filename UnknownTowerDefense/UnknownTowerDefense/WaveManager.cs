using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace UnknownTowerDefense
{
    class WaveManager
    {

        private int numberOfWaves; 

        private float timeSinceLastWave; 
        private Queue<Wave> waves = new Queue<Wave>(); 
        private Texture2D enemyTexture; 
        private bool waveFinished = false; 
        private Level level; 

        public Wave CurrentWave 
        {
            get { return waves.Peek(); }
        }

        public List<Enemy> Enemies 
        {
            get { return CurrentWave.Enemies; }
        }

        public int Round 
        {
            get { return CurrentWave.RoundNumber + 1; }
        }

        public WaveManager(Player player, Level level, int numberOfWaves, Texture2D enemyTexture)
        {

            this.numberOfWaves = numberOfWaves;

            this.enemyTexture = enemyTexture;

            this.level = level;

            for (int i = 0; i < numberOfWaves; i++)
            {
                int initialNumerOfEnemies = 6;

                int numberModifier = (i / 6) + 1;
                Wave wave = new Wave(i, initialNumerOfEnemies * numberModifier, player, level, enemyTexture);

                waves.Enqueue(wave);
            }
            StartNextWave();
        }
        
        private void StartNextWave()
        {
            if (waves.Count > 0) 
            {
                waves.Peek().start(); 
                timeSinceLastWave = 0; 
                waveFinished = false;
            }
        }
        public void Update(GameTime gameTime)
        {
            CurrentWave.Update(gameTime); 

            if (CurrentWave.RoundOver) 
            {
                waveFinished = true;
            }

            if (waveFinished) 
            {
                timeSinceLastWave += (float)gameTime.ElapsedGameTime.TotalSeconds; 
            }

            if (timeSinceLastWave > 10.0f) 
            {
                waves.Dequeue(); 
                StartNextWave();
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentWave.Draw(spriteBatch);
        }
    }

}
