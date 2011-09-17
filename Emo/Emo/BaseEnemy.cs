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
    class BaseEnemy
    {
        private Health _heroHealth;
        private Texture2D _textura;
        private int _moveSpeed;
        public Vector2 _posicao;
        public void Initialize(int hp, Texture2D textura, Vector2 posicao, int ms)
        {

            _heroHealth = new Health();
            _heroHealth.Healths = hp;
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
        }

    }
}
