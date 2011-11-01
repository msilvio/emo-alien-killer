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
    class Shoot : Animation
    {
        #region Declarations

        private Texture2D texture;
        public int largura, altura, damage;
        float bulletMoveSpeed;
        private Vector2 position;
        bool DIREITA;
        Viewport viewport;

        #endregion

        #region Constructor

        public Shoot(Texture2D texture, Vector2 position,
                                int frameWidth, int frameHeight, int frameCount,
                               int frametime, Color color, float scale, bool looping, bool DIREITA, Viewport viewport)
            : base(texture, position,
                                frameWidth, frameHeight, frameCount,
                                frametime, color, scale, looping)
        {

            this.texture = texture;
            Position = position;
            Active = true;
            damage = 5;
            this.altura = texture.Width / 4;
            this.largura = texture.Height;
            bulletMoveSpeed = 20.0f;
            color = Color.White;
            this.DIREITA = DIREITA;
            this.viewport = viewport;

        }

        #endregion

        #region Update and Draw

        public void Update(GameTime gameTime)
        {
            if (DIREITA)
            {
                Position.X += bulletMoveSpeed;
            }
            else
            {
                Position.X -= bulletMoveSpeed;
            }

            if(Position.X + texture.Width /2 > viewport.Width + 100)
                Active = false;

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        #endregion

    }
}
