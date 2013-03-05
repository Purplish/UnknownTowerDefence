using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UnknownTowerDefense
{
    class Enemy : Sprite
    {
        protected float startHealth;
        protected float currentHealth;
        protected bool alive = true;
        protected float speed = 0.5F;
        protected float speedModifier;
        protected float modifierDuration;
        protected float modifierCurrentTime;
        protected int bountyGiven;

        private Queue<Vector2> waypoints = new Queue<Vector2>();

        public void SetWaypoints(Queue<Vector2> waypoints)
        {
            foreach (Vector2 waypoint in waypoints)
                this.waypoints.Enqueue(waypoint);
            this.position = this.waypoints.Dequeue();
        }

        public float DistanceToDestination
        {

            get { return Vector2.Distance(position, waypoints.Peek()); }

        }

        public float CurrentHealth
        {
            get { return currentHealth; }
            set { currentHealth = value; }
        }

        public float SpeedModifier
        {
            get { return speedModifier; }
            set { speedModifier = value; }
        }

        public float ModifierDuration
        {
            get { return modifierDuration; }
            set 
            {
                modifierDuration = value;
                modifierCurrentTime = 0;
            }
        }

        public bool IsDead
        {
            get { return !alive; }
        }

        public float HealthPercentage
        {
            get { return currentHealth / startHealth; }
        }

        public int BountyGiven
        {
            get { return bountyGiven; }
        }

        public Enemy(Texture2D texture, Vector2 position, float health, int bountyGiven, float speed)
            : base(texture, position)
        {
            this.startHealth = health;
            this.currentHealth = startHealth;
            this.bountyGiven = bountyGiven;
            this.speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (waypoints.Count > 0)
            {
                if (DistanceToDestination < speed)
                {
                    position = waypoints.Peek();
                    waypoints.Dequeue();
                }
                else
                {
                    Vector2 direction = waypoints.Peek() - position;
                    direction.Normalize();

                    float temporarySpeed = speed;

                    if (modifierCurrentTime > modifierDuration)
                    {
                        speedModifier = 0;
                        modifierCurrentTime = 0;
                    }

                    if (speedModifier != 0 && modifierCurrentTime < modifierDuration)
                    {
                        temporarySpeed *= speedModifier;
                        modifierCurrentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    
                    velocity = Vector2.Multiply(direction, temporarySpeed);
                    position += velocity;
                }
            }
            else
                alive = false;
            if (currentHealth <= 0)
                alive = false;
            
            
                
            

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                Color color;
                float healthPercentage = (float)currentHealth / (float)startHealth;
                if (this.speedModifier > 0)
                {
                    color = new Color(new Vector3(0, 255, 255));
                }
                else
                {
                    color = new Color(new Vector3(0, 0, 0));
                }
                    base.Draw(spriteBatch, color);

            }
        }

    }
}
