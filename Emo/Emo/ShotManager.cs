using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Emo
{
    class ShotManager
    {
        public Texture2D Texture;
        public Vector2 Position;
        public bool Active;
        public int Damage;
        Viewport viewport;
        float bulletMoveSpeed;

        public int Width
        {
            get { return Texture.Width; }
        }

        public int Heigth
        {
            get { return Texture.Height; }
        }

        public void Init(Viewport viewport,Texture2D texture, Vector2 position) 
        {
            Texture = texture;
            Position = position;
            this.viewport = viewport;
            Active = true;
            Damage = 5;
            bulletMoveSpeed = 20.0f;
        }

        public void Update() 
        {
            Position.X += bulletMoveSpeed;

            if(Position.X + Texture.Width / 2 > viewport.Width)
                Active = false;
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f,
                new Vector2(Width / 2, Heigth / 2), 1f, SpriteEffects.None, 0f);
        }
    
    }
}
