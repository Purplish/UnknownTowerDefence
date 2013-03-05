using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UnknownTowerDefense
{
    class Tower : Sprite
    {
        protected int cost; // How much will the tower cost to make

        protected int damage; // The damage done to enemy's

        protected Texture2D bulletTexture;
        protected List<Bullet> bulletList = new List<Bullet>();
        protected float bulletTimer;
        protected Enemy enemy;
        protected float radius; // How far the tower can shoot
        protected String name = "";

        protected Enemy target;


        public Enemy Target
        {
            get { return target; }
            set { this.target = value; }
        }

        public int Cost
        {
            get { return cost; }
            set { this.cost = value; }
        }

        public int Damage
        {
            get { return damage; }
            set { this.damage = value; }
        }

        public float Radius
        {
            get { return radius; }
            set { this.radius = value; }
        }

        public String Name
        {
            get { return name; }
            set { this.name = value; }
        }

        public Tower(Texture2D texture, Texture2D bulletTexture, Vector2 position, String name)
            : base(texture, position)
        {
            this.bulletTexture = bulletTexture;
            this.name = name;
        }

        public bool IsInRange(Vector2 position)
        {
            if (Vector2.Distance(center, position) <= radius)
                return true;
            return false;
        }

        public void GetClosestEnemy(List<Enemy> enemies)
        {
            target = null;
            float smallestRange = radius;
            foreach (Enemy enemy in enemies)
            {
                if (Vector2.Distance(center, enemy.Center) < smallestRange)
                {
                    smallestRange = Vector2.Distance(center, enemy.Center);
                    target = enemy;
                }
            }
        }
        protected void FaceTarget()
        {

                Vector2 direction = center - target.Center;
                direction.Normalize();
                rotation = (float)Math.Atan2(-direction.X, direction.Y);
         }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


            bulletTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (target == null)
            {
            }
            if (target != null)
            {

                FaceTarget();


                if (!IsInRange(target.Center) || target.IsDead)
                {
                    //enemy.IsDead = true;                     
                    target = null;
                    bulletTimer = 0;

                }
               

            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            foreach (Bullet bullet in bulletList)

                bullet.Draw(spriteBatch);

            base.Draw(spriteBatch);

        }
    }
}
