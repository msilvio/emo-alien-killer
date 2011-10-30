using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

namespace Emo
{
    class Health
    {
        public int _health;
        Texture2D mHealthBar;
        Viewport viewport;
        Vector2 Position;

        public int Healths{
            get { return _health; }
            set {_health = value; }
        }

        public Health(Texture2D mhealthBar, Viewport viewport, Vector2 position) 
        {
            this.mHealthBar = mhealthBar;
            this.viewport = viewport;
            this.Position = position;
        }

        public void Init() {
        }

        public void Update(GameTime gameTime, Vector2 position) 
        {
            KeyboardState mKeys = Keyboard.GetState();
            if (mKeys.IsKeyDown(Keys.Up) == true)
            {
                _health += 1;
            }

            if (mKeys.IsKeyDown(Keys.Down) == true)
            {
                _health -= 1;
            }

            _health = (int)MathHelper.Clamp(_health, 0, 100);

            this.Position = position;
            
        }

        public void Draw(SpriteBatch spriteBatch) 
        {

            //spriteBatch.Draw(mHealthBar, new Rectangle(this.viewport.Width / 2 - mHealthBar.Width / 2,
            //    30, mHealthBar.Width, 44), new Rectangle(0, 45, mHealthBar.Width, 44), Color.Red);

            //spriteBatch.Draw(mHealthBar, new Rectangle(this.viewport.Width / 2 - mHealthBar.Width / 2,
            //    30, mHealthBar.Width, 44), new Rectangle(0, 0, mHealthBar.Width, 44), Color.White);

            //spriteBatch.Draw(mHealthBar, new Rectangle(100, 100, (_health*mHealthBar.Width)/100, mHealthBar.Height), Color.White);

            spriteBatch.Draw(mHealthBar, 
                            new Rectangle((int)this.Position.X +20, // ajustado a largura do heroi
                                          (int)this.Position.Y-100, // ajustado a altura
                                          (_health * mHealthBar.Width/2) / 100, // reduzida metade do tamanho
                                           mHealthBar.Height), 
                                           Color.White);

        }
    }
}
