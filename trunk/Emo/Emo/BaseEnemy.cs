using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Emo
{
    class BaseEnemy : Sprite
    {
        
        protected float startHealth;
        protected float currentHealth;

        protected bool alive = true;

        protected float speed = 0.5f;

        public float CurrentHealth
        {
            get { return currentHealth; }
            set { currentHealth = value; }
        }

        public float HealthPercentage
        {
            get { return currentHealth / startHealth; }
        }

        public bool IsDead
        {
            get { return currentHealth <= 0; }
        }

        public BaseEnemy(Texture2D texture, Vector2 position, float health, float speed)
    : base(texture, position)
{
    this.startHealth = health;
    this.currentHealth = startHealth;
    this.speed = speed;
}

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (currentHealth <= 0)
                alive = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                // Muda a cor do inimigo até ele sumir/morrer
                float healthPercentage = (float)currentHealth / (float)startHealth;

                Color color = new Color(new Vector3(1 - healthPercentage,
                    1 - healthPercentage, 1 - healthPercentage));

                base.Draw(spriteBatch, color);
            }
        }
        /*private Health _heroHealth;
        private Texture2D _textura;
        private int _moveSpeed;
        public Vector2 _posicao;
        public void Initialize(int hp, Texture2D textura, Vector2 posicao, int ms)
        {

            //_heroHealth = new Health();
            //_heroHealth.Healths = hp;
            _textura = textura;
            _posicao = posicao;
            _moveSpeed = ms;


        }
        public void Draw(SpriteBatch sb)
        {

            sb.Draw(_textura, _posicao, null, Color.White);
        }
        public void update(GameTime gt)
        {


            _posicao.X -= _moveSpeed;


            _posicao.X = MathHelper.Clamp(_posicao.X, 0, 800 - _textura.Width);
            _posicao.Y = MathHelper.Clamp(_posicao.Y, 0, 600 - _textura.Height);
        }*/

    }
}
