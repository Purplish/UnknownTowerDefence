using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UnknownTowerDefense
{
    public class ParticleEngine
    {

        private Random random;
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> particles;
        private List<Texture2D> textures;
        private int life;


        public ParticleEngine(List<Texture2D> textures, Vector2 location, int life)
        {
            EmitterLocation = location;
            this.textures = textures;
            this.particles = new List<Particle>();
            this.life = life;
            random = new Random();
        }

        private Particle GenerateNewParticle(int a)
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                    1f * (float)(random.NextDouble() * 2 - 1),
                    1f * (float)(random.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
           // Color color = new Color(
          //          (float)random.NextDouble(),
           //         (float)random.NextDouble(),
            //        (float)random.NextDouble());
            Color color = Color.Red;
            float size = (float)random.NextDouble();
            int ttl = 10 + random.Next(a);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }

        public void Update()
        {
            int total = 1;

            for (int i = 0; i < total; i++)
            {
                particles.Add(GenerateNewParticle(life));
            }

            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if (particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

           // spriteBatch.Begin();
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
            //spriteBatch.End();
        }


    }
}
