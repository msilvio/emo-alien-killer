﻿using System;
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
        public Health _heroHealth;
        private Texture2D _textura;
        public int _moveSpeed;
        Rectangle seta_destino, seta_origem;
        public Vector2 _posicao;
        public bool alive = true;
        public bool Tiro;
        public bool DIREITA = true;
        Viewport viewport;
        public int largura, altura, pos_origemX, pos_origemY, pos_destinoX, pos_destinoY, frame, hp;

        public void Initialize(int hp,Texture2D textura, Texture2D _healthBar, Vector2 posicao, int ms, Viewport viewport)
        {

            _heroHealth = new Health(_healthBar, viewport, posicao);
            _heroHealth.Healths = hp;
            _textura = textura;
            _posicao = posicao;
            _moveSpeed = ms;
            this.hp = hp;

            largura = _textura.Width / 4;
            altura = _textura.Height / 2;
            pos_origemX = 0;
            pos_origemY = 0;
            pos_destinoX = 0;
            pos_destinoY = 0;
            frame = 0;
            this.viewport = viewport;
            this.alive = true;

            //seta_destino = new Rectangle(0, 200, largura, altura);
            seta_destino = new Rectangle(0, 170, largura, altura);
            seta_origem = new Rectangle(pos_origemX, pos_origemY, largura, altura);
        }

        public void Draw(SpriteBatch sb) 
        {

            sb.Draw(_textura, seta_destino, seta_origem, Color.White);
            _heroHealth.Draw(sb);
        }

        public void Fire(bool onShot)
        {
            Tiro = onShot;
        }

        private void HandleGamepadInput(GamePadState gamePadState)
        {
            this._posicao +=
                new Vector2(gamePadState.ThumbSticks.Left.X * _moveSpeed, 0); // controle do GamePad travado em Y

            if (gamePadState.Buttons.A == ButtonState.Pressed)
            {
                Fire(true);
            }
            //if (gamePadState.Buttons.LeftShoulder == ButtonState.Pressed) { _moveSpeed = 5; }
        }


        public void update(KeyboardState kbs,GameTime gt) 
        {
            _heroHealth.Healths = this.hp;
            _heroHealth.Update(gt, this._posicao);

            if (kbs.IsKeyDown(Keys.Left))
            {
                DIREITA = false;
                _posicao.X -= _moveSpeed;
                //if (_posicao.X > 400)
                //{
                //    MoveBackgroundRight();
                //    //X += moveSpeed;
                //}

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
                DIREITA = true;
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

            _posicao.X = MathHelper.Clamp(_posicao.X, 0, 800 - largura);
            _posicao.Y = MathHelper.Clamp(_posicao.Y, 0, 600 - altura);

            seta_destino.X = (int)_posicao.X;

            seta_origem.X = pos_origemX;
            seta_origem.Y = pos_origemY;

            HandleGamepadInput(GamePad.GetState(PlayerIndex.One));

        }

        private void MoveBackgroundRight()
        {
            throw new NotImplementedException();
        }
    }
}
