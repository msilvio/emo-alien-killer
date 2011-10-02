using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Emo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D edKillers;
        BaseHero _eddie;
        Vector2 _posicaoPlayer;
        KeyboardState tecladoAnterior;
        Texture2D fundo;

        Animation machado;

        enum Telas 
        {
            INTRO, MENU, FASE1
        };

        Telas tela_atual = Telas.FASE1; //Telas tela_atual = Telas.INTRO;
        Texture2D telaIntro;
        Texture2D telaMenu;
        //Texture2D seta, telaIntroducao, telaMenu;
        //Rectangle seta_destino, seta_origem;
        //int largura, altura, pos_origemX, pos_origemY, pos_destinoX, pos_destinoY;
        KeyboardState teclado;

        //enum Telas { INTRO, MENU, FASE1, };

        //Telas tela_atual;

        //int frame;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //frame = 0;
            //tela_atual = Telas.INTRO;

            Window.Title = "Emo Alien Killer";
            IsMouseVisible = false;

            _eddie = new BaseHero();

            machado = new Animation();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //edKillers = Content.Load<Texture2D>("eddiekillers");

            edKillers = Content.Load<Texture2D>("eddie1");
            _posicaoPlayer = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            _eddie.Initialize(
                500,
                edKillers,
                Content.Load<Texture2D>("eddie1"),
                _posicaoPlayer, 6);

            telaIntro = Content.Load<Texture2D>("telaintro");
            telaMenu = Content.Load<Texture2D>("telamenu");
            fundo = Content.Load<Texture2D>("Fundo_Fase01");

            Texture2D textura_Axe = Content.Load<Texture2D>("axe-sprite");
            // machado.Initialize(textura_Axe, new Vector2(150.0f, 230.0f), 42, 42, 4, 90, Color.White, 1.0f, true);
            machado.Initialize(textura_Axe, new Vector2(100.0f, 100.0f), 42, 42, 4, 90, Color.White, 1.0f, true);


            
            /*
            seta = Content.Load<Texture2D>("setas");
            largura = seta.Width / 4;
            altura = seta.Height / 2;
            seta_destino = new Rectangle(0, 0, largura, altura);
            pos_origemX = 0;
            pos_origemY = 0;
            seta_origem = new Rectangle(pos_origemX, pos_origemY, largura, altura);

            telaIntroducao = Content.Load<Texture2D>("telaintro");
            telaMenu = Content.Load<Texture2D>("telamenu");
             * */
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            teclado = Keyboard.GetState();
            
            if (teclado.IsKeyDown(Keys.Escape))
            {
                this.Exit(); // Teclar Escape para sa�da do jogo
            }
           
            // TODO: Add your update logic here
            
            switch (tela_atual)
            {
                case Telas.INTRO:
                    if (teclado.IsKeyDown(Keys.Enter))
                    {
                        tela_atual = Telas.MENU;
                    }
                    break;
                case Telas.MENU:
                    if (teclado.IsKeyDown(Keys.Enter) && !tecladoAnterior.IsKeyDown(Keys.Enter))
                    {
                        tela_atual = Telas.FASE1;
                    }
                    break;
                case Telas.FASE1:
                    _eddie.update(teclado, gameTime);
                    machado.Update(gameTime);
            
                    break;
            }
            tecladoAnterior = teclado;
            

            base.Update(gameTime);
        }
/*
        public void Fase1()
        {
            if (teclado.IsKeyDown(Keys.Left))
            {
                pos_origemX = 0;
                pos_origemY = altura;
                pos_destinoX -= 2;
            }

            if (teclado.IsKeyDown(Keys.Right))
            {
                pos_origemX = frame * largura;
                pos_origemY = 0;
                pos_destinoX += 2;
                if (frame < 4)
                    frame++;
                else
                    frame = 0;
            }
            seta_destino.X = pos_destinoX;
            seta_origem.X = pos_origemX;
            seta_origem.Y = pos_origemY;
             * */

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            
            switch (tela_atual)
            {
                case Telas.INTRO:
                    spriteBatch.Draw(telaIntro, Vector2.Zero, Color.White);
                    break;
                case Telas.MENU:
                    spriteBatch.Draw(telaMenu, Vector2.Zero, Color.White);
                    break;
                case Telas.FASE1:
                    //spriteBatch.Draw(fundo, Vector2.Zero, Color.White);
                    spriteBatch.Draw(fundo, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, 2.1f, SpriteEffects.None, 1.0f);
                    _eddie.Draw(spriteBatch);
                    machado.Draw(spriteBatch);


                    break;
            }

            

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}