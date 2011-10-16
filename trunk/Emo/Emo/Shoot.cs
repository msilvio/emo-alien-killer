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
        public int Damage;
        float bulletMoveSpeed;
        private Vector2 position;
        bool DIREITA;

        #endregion

        #region Constructor

        public Shoot(Texture2D texture, Vector2 position,
                                int frameWidth, int frameHeight, int frameCount,
                               int frametime, Color color, float scale, bool looping, bool DIREITA)
            : base(texture, position,
                                frameWidth, frameHeight, frameCount,
                                frametime, color, scale, looping)
        {

            this.texture = texture;
            Position = position;
            Active = true;
            Damage = 5;
            bulletMoveSpeed = 20.0f;
            color = Color.White;
            this.DIREITA = DIREITA;

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

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        #endregion

    }
}
