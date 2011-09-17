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
    class Shoot
    {
        public Vector2 _position;
        public bool _active;
        public int _damage;
        public Animation _tiro;
        Viewport _viewport;
        float _projectileMoveSpeed;



         public void Initialize(Viewport viewport, Vector2 position, Animation tiroani)
         {

             _tiro = tiroani;
            _position = position;
           this._viewport = viewport;

           _active = true;
           _damage = 50;

           _projectileMoveSpeed = 20f;
        }
        public void Update(GameTime gt)
        {
            _position.X += _projectileMoveSpeed;
            _tiro.Update(gt);
            if (_position.X + 75 > _viewport.Width)
                _active = false;

        }
        public void Draw(SpriteBatch sb)
        {
            
        }
    }
}
