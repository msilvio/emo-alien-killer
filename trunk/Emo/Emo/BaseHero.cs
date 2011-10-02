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
    class BaseHero
    {
        private Health _heroHealth;
        private Texture2D _textura;
        private int _moveSpeed;
        Rectangle seta_destino, seta_origem;
        public Vector2 _posicao;
        public bool Tiro;

        //public int largura, altura, pos_origem
        public int largura, altura, pos_origemX, pos_origemY, pos_destinoX, pos_destinoY, frame;

        public void Initialize(int hp,Texture2D textura, Texture2D _healthBar, Vector2 posicao, int ms)
        {

            _heroHealth = new Health(_healthBar);
            _heroHealth.Healths = hp;
            _textura = textura;
            _posicao = posicao;
            _moveSpeed = ms;

            largura = _textura.Width / 4;
            altura = _textura.Height / 2;
            pos_origemX = 0;
            pos_origemY = 0;
            pos_destinoX = 0;
            pos_destinoY = 0;
            frame = 0;

            seta_destino = new Rectangle(0, 200, largura, altura);
            seta_origem = new Rectangle(pos_origemX, pos_origemY, largura, altura);
        }

        public void Draw(SpriteBatch sb) 
        {

            sb.Draw(_textura, seta_destino, seta_origem, Color.White);
        }

        public void Fire(bool onShot)
        {
            Tiro = onShot;
        }

        private void HandleGamepadInput(GamePadState gamePadState)
        {
            this._posicao +=
                new Vector2(
                    gamePadState.ThumbSticks.Left.X * 4,
                    -gamePadState.ThumbSticks.Left.Y * 4);
            if (gamePadState.Buttons.A == ButtonState.Pressed)
            {
                Fire(true);
            }
            //if (gamePadState.Buttons.LeftShoulder == ButtonState.Pressed) { _moveSpeed = 5; }
        }


        public void update(KeyboardState kbs,GameTime gt) 
        {

            if (kbs.IsKeyDown(Keys.Left))
            {
                _posicao.X -= _moveSpeed;

                pos_origemX = frame * largura;
                pos_origemY = altura;
                //pos_destinoX -= 2;
                if (frame < 3)
                    frame++;
                else
                    frame = 0;
               
            }

            if (kbs.IsKeyDown(Keys.Right))
            {
                _posicao.X += _moveSpeed;

                pos_origemX = frame * largura;
                pos_origemY = 0;
                //pos_destinoX += 2;
                if (frame < 3)
                    frame++;
                else
                    frame = 0;
            }

            if (kbs.IsKeyDown(Keys.Space)) { Fire(true); }
            if (kbs.IsKeyUp(Keys.Space)) { Fire(false); }

            //_posicao.X = MathHelper.Clamp(_posicao.X, 0, 800 - _textura.Width);
            //_posicao.Y = MathHelper.Clamp(_posicao.Y, 0, 600 - _textura.Height);

            _posicao.X = MathHelper.Clamp(_posicao.X, 0, 800 - largura);
            _posicao.Y = MathHelper.Clamp(_posicao.Y, 0, 600 - altura);

            //seta_destino.X = (int)MathHelper.Clamp(_posicao.X, 0, 800 - largura);

            seta_destino.X = (int)_posicao.X;

            //seta_destino.X = pos_destinoX;
            //seta_destino.X = pos_destinoX;
            seta_origem.X = pos_origemX;
            seta_origem.Y = pos_origemY;

            HandleGamepadInput(GamePad.GetState(PlayerIndex.One));

        }
    }
}
