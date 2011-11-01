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
    class Background
    {
        // controle de rolagem de cenário
        public Texture2D fundo_imagem;
        public Vector2 fundo_quadro;
        int moveSpeed;
        int X;
        bool DIREITA = true;
        Viewport viewport;

        public Background(Viewport viewport, Texture2D fundo_imagem, Vector2 fundo_quadro, int moveSpeed, bool DIREITA)
        {
            this.viewport = viewport;
            this.fundo_imagem = fundo_imagem;
            this.fundo_quadro = fundo_quadro;
            this.moveSpeed = moveSpeed;
            this.DIREITA = DIREITA;
        }

        public void MoveBackgroundRight()
        {
            this.fundo_quadro.X += moveSpeed;
        }

        public void MoveBackgroundLeft()
        {
            this.fundo_quadro.X -= moveSpeed;
        }

        public void Update(GameTime gameTime, Vector2 heroPosition)
        {
            if (DIREITA)
            {
                if (heroPosition.X > 600)
                {
                    this.fundo_quadro.X += moveSpeed;
                    //X += moveSpeed;
                }
               
            }
            else
            {
                if (heroPosition.X < 100)
                {
                    this.fundo_quadro.X -= moveSpeed;
                    //X += moveSpeed;
                }
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(fundo_imagem,
                                    new Rectangle(0, 0,
                                    fundo_imagem.Width * 2, fundo_imagem.Height * 2),
                                    new Rectangle((int)fundo_quadro.X, (int)fundo_quadro.Y,
                                    fundo_imagem.Width, fundo_imagem.Height),
                                    Color.White);

            //spriteBatch.Draw(fundo_imagem, new Vector2(X, 0), null, Color.White,
            //                0.0f, Vector2.Zero, 2.1f, SpriteEffects.None, 1.0f);


        }

    }
}
