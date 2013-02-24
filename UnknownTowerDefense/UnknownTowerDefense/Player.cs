using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UnknownTowerDefense
{
    class Player
    {
        private int money = 30;

        private int lives = 30;

        private List<Tower> towers = new List<Tower>();
        private Texture2D bulletTexture;
        private MouseState mouseState; // Mouse state for the current frame
        private MouseState oldState; // Mouse state for the previous frame

        private Level level;

        private int cellX;
        private int cellY;
        private int tileX;
        private int tileY;
        private Texture2D towerTexture;
        private string newTowerType;

        public string NewTowerType
        {
            set { newTowerType = value; }
        }

        public void AddTower()
        {
            Tower towerToAdd = null;

            switch (newTowerType)
            {
                case "Arrow Tower":
                    {
                        towerToAdd = new ArrowTower(towerTexture,
                            bulletTexture, new Vector2(tileX, tileY));
                        break;
                    }
            }

            // Only add the tower if there is a space and if the player can afford it.
            if (IsCellClear() == true && towerToAdd.Cost <= money)
            {
                towers.Add(towerToAdd);
                money -= towerToAdd.Cost;

                // Reset the newTowerType field.
                newTowerType = string.Empty;
            }
        }

        public int Money
        {
            get { return money; }
            set { money = value; }
        }

        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }



        public Player(Level level, Texture2D towerTexture, Texture2D bulletTexture)
        {

            this.level = level;


            this.towerTexture = towerTexture;

            this.bulletTexture = bulletTexture;

        }
        public void Update(GameTime gameTime, List<Enemy> enemies)
        {
             mouseState = Mouse.GetState();

            cellX = (int)(mouseState.X / 32); // Convert the position of the mouse
            cellY = (int)(mouseState.Y / 32); // from array space to level space

            tileX = cellX * 32; // Convert from array space to level space
            tileY = cellY * 32; // Convert from array space to level space

            if (mouseState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed)
            {
                if (string.IsNullOrEmpty(newTowerType) == false)
                {
                    AddTower();
                }
            }

            foreach (Tower tower in towers)
            {
                if (tower.Target == null)
                {
                    tower.GetClosestEnemy(enemies);
                }

                tower.Update(gameTime);
            }

            oldState = mouseState;

             foreach (Tower tower in towers)
            {
                if (tower.Target == null)
                {
                    tower.GetClosestEnemy(enemies);
                }
                tower.Update(gameTime);
            }
        }

        private bool IsCellClear()
        {
            bool inBounds = cellX >= 0 && cellY >= 0 && // Make sure tower is within limits
                cellX < level.Width && cellY < level.Height;
            bool spaceClear = true;
            foreach (Tower tower in towers) // Check that there is no tower here
            {

                spaceClear = (tower.Position != new Vector2(tileX, tileY));

                if (!spaceClear)
                  break;
            }

            bool onPath = (level.GetIndex(cellX, cellY) != 1);

            return inBounds && spaceClear && onPath; // If both checks are true return true

        }
        Game1 t;
        public void Draw(SpriteBatch spriteBatch)
        {
            
            foreach (Tower tower in towers)
            {                
                tower.Draw(spriteBatch);
            }
        }
    }
}
