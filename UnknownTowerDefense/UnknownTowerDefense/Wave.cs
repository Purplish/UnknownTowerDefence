using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UnknownTowerDefense
{
    class Wave
    {

        private int numOfEnemies;
        private int waveNumber;
        private float spawnTimer = 0;
        private int enemiesSpawned = 0;

        private Texture2D healthTexture;
        private bool enemyAtEnd;
        private bool spawningEnemies;
        private Level level;
        private Texture2D enemyTexture;
        public List<Enemy> enemies = new List<Enemy>();
        private Player player;

        public bool RoundOver
        {
            get
            {
                return enemies.Count == 0 && enemiesSpawned == numOfEnemies;
            }
        }

        public int RoundNumber
        {
            get { return waveNumber; }
        }

        public bool EnemyAtEnd
        {
            get { return enemyAtEnd; }
            set { enemyAtEnd = value; }
        }

        public List<Enemy> Enemies
        {
            get { return enemies; }
        }

        public Wave(int waveNumber, int numOfEnemies, Player player, Level level, Texture2D enemyTexture, Texture2D healthtexture)
        {
            this.waveNumber = waveNumber;
            this.numOfEnemies = numOfEnemies;
            this.player = player;

            this.level = level;
            this.enemyTexture = enemyTexture;
            this.healthTexture = healthtexture;
        }

        private void AddEnemy()
        {
            Enemy enemy = new Enemy(enemyTexture, level.Waypoints.Peek(), 100, 3, 0.5f);
            enemy.SetWaypoints(level.Waypoints);
            enemies.Add(enemy);
            spawnTimer = 0;
            enemiesSpawned++;
        }

        public void start()
        {
            spawningEnemies = true;
        }

        public void Update(GameTime gameTime)
        {


            if (enemiesSpawned == numOfEnemies)
                spawningEnemies = false; // Enough enemies spawned
            if (spawningEnemies)
            {
                spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (spawnTimer > 2)
                    AddEnemy(); // Add enemy to level
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy enemy = enemies[i];
                enemy.Update(gameTime);

                if (enemy.IsDead)
                {

                    if (enemy.CurrentHealth > 0) // Enemy is at the end
                    {
                        enemyAtEnd = true;
                        player.Lives -= 1;
                    }
                    else
                    {
                        player.Money += enemy.BountyGiven;
                    }

                    enemies.Remove(enemy);
                    i--;
                }
            }
        }
    
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in enemies){
                
            Rectangle healthRectangle = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, healthTexture.Width, healthTexture.Height);
            spriteBatch.Draw(healthTexture, healthRectangle, Color.Gray);
            float healthPercentage = enemy.HealthPercentage;
            float visibleWidth = (float)healthTexture.Width * healthPercentage;

            healthRectangle = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, (int)(visibleWidth), healthTexture.Height);
            spriteBatch.Draw(healthTexture, healthRectangle, Color.Gold);
            enemy.Draw(spriteBatch);
            }
        }
    }
}
