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
        SpriteFont arial;

        Texture2D edKillers;
        BaseHero _eddie;
        Vector2 _posicaoPlayer;
        KeyboardState tecladoAnterior;
        Texture2D fundo;
        bool onPlay = false;
        int moveSpeed = 2;
        Texture2D mHealthBar;

        TimeSpan previusSpawnTime;
        TimeSpan fireTime;
        TimeSpan previousFireTime;

        private float titleScreenTimer = 0.0f;
        private float titleScreenDelayTime = 1.0f;

        List<Shoot> shoots;

        Song gameplayMusic, menuMusic;
        SoundEffect laserSound;

        enum Telas 
        {
            INTRO, MENU, FASE1, FASE2, FASE3
        };

        Telas tela_atual = Telas.FASE1; //Telas tela_atual = Telas.INTRO; Telas tela_atual = Telas.FASE1;
        Texture2D telaIntro;
        Texture2D telaMenu;
        Background backGround;

        KeyboardState teclado;

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
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 460;
            graphics.ApplyChanges();

            _eddie = new BaseHero();

            shoots = new List<Shoot>();

            fireTime = TimeSpan.FromSeconds(.15f);
            previusSpawnTime = TimeSpan.Zero;

            

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

            edKillers = Content.Load<Texture2D>("eddie1");
            _posicaoPlayer = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X,
                                        GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

            _eddie.Initialize(
                100,
                edKillers,
                Content.Load<Texture2D>("eddie1"),
                _posicaoPlayer, moveSpeed, GraphicsDevice.Viewport);

            mHealthBar = Content.Load<Texture2D>("HealthBar");

            telaIntro = Content.Load<Texture2D>("telaintro");
            telaMenu = Content.Load<Texture2D>("telamenu");
            //fundo = Content.Load<Texture2D>("Fundo_Fase01"); // Trocar o fundo conforme a fase

            arial = Content.Load<SpriteFont>("arial");

            // Audio
            gameplayMusic = Content.Load<Song>("Sounds/446285_A_Necessary_Evil");
            menuMusic = Content.Load<Song>("Sounds/450042_01___Resting_Place");
            laserSound = Content.Load<SoundEffect>("Sounds/laserFire");

            PlayMusic(menuMusic); // colocar em funciomento depois de selecionada as musicas

            tela_atual = Telas.INTRO;

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

            // Teclar Escape para saída do jogo
            teclado = Keyboard.GetState();

            if (teclado.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
           
            // TODO: Add your update logic here
            
            switch (tela_atual)
            {
                case Telas.INTRO:
                    // incluido para controla a execução inicial
                    titleScreenTimer +=
                        (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (titleScreenTimer >= titleScreenDelayTime)
                    {
                        if ((Keyboard.GetState().IsKeyDown(Keys.Enter)) ||
                            (GamePad.GetState(PlayerIndex.One).Buttons.Start ==
                            ButtonState.Pressed))
                        {
                            tela_atual = Telas.MENU;
                        }
                    }
                    //if (teclado.IsKeyDown(Keys.Enter))
                    //{
                    //    tela_atual = Telas.MENU;
                    //}
                    break;
                case Telas.MENU:
                     titleScreenTimer +=
                        (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (titleScreenTimer >= titleScreenDelayTime)
                    {
                        if ((teclado.IsKeyDown(Keys.Enter) && !tecladoAnterior.IsKeyDown(Keys.Enter)) ||
                            (GamePad.GetState(PlayerIndex.One).Buttons.X ==
                            ButtonState.Pressed))
                        {
                            tela_atual = Telas.FASE1;
                            PlayMusic(gameplayMusic);
                            // carregar fundo correspondente a Fase 1
                            fundo = Content.Load<Texture2D>("Fundo_Fase03");
                            backGround = new Background(GraphicsDevice.Viewport,
                                            fundo, new Vector2(0, 0), _eddie._moveSpeed, _eddie.DIREITA);
                        }
                    }

                    break;
                case Telas.FASE1:
                    
                    _eddie.update(teclado, gameTime);

                    onPlay = true;
                    // carregar fundo correspondente a fase - retirar
                    //fundo = Content.Load<Texture2D>("Fundo_Fase03");
                    //backGround = new Background(GraphicsDevice.Viewport,
                    //                fundo, new Vector2(0, 0), _eddie._moveSpeed, _eddie.DIREITA);
                    //PlayMusic(gameplayMusic);

                    if (gameTime.TotalGameTime - previousFireTime > fireTime)
                    {
                        previousFireTime = gameTime.TotalGameTime;
                        if (_eddie.Tiro == true && onPlay == true) // if (player.Tiro == true) 
                        {
                            laserSound.Play();
                            AddBullet(_eddie._posicao);
                        }
                    }
                    backGround.Update(gameTime, _eddie._posicao);
                    break;

                case Telas.FASE2:

                    break;

                case Telas.FASE3:

                    break;
                    
            }
            tecladoAnterior = teclado;
            UpdateBullet(gameTime);
            //backGround.Update(gameTime, _eddie._posicao);

            base.Update(gameTime);
        }

        private void AddBullet(Vector2 position)
        {
            Texture2D textura_Axe = Content.Load<Texture2D>("axe-sprite");

            float meio = _eddie.largura / 2;
            Shoot shoot = new Shoot(textura_Axe, 
                                    _eddie._posicao + new Vector2(meio, -30), 
                                    42, 42, 4, 90, Color.White, 1.0f, true, 
                                    _eddie.DIREITA, GraphicsDevice.Viewport); 
            shoots.Add(shoot);
        }

        private void UpdateBullet(GameTime gameTime)
        {
            for (int i = shoots.Count - 1; i >= 0; i--)
            {
                shoots[i].Update(gameTime);

                if (shoots[i].Active == false)
                {
                    shoots.RemoveAt(i);
                }
            }
        }

        private void PlayMusic(Song song)
        {
            try
            {
                MediaPlayer.Play(song);
                MediaPlayer.IsRepeating = true;
            }
            catch { }
        }


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
                    // retirar apos concluída a class Background
                    //spriteBatch.Draw(fundo, Vector2.Zero, null, Color.White, 
                    //                0.0f, Vector2.Zero, 2.1f, SpriteEffects.None, 1.0f);
                    // desenhar fundo conforme a fase
                    backGround.Draw(spriteBatch);

                    _eddie.Draw(spriteBatch);

                    spriteBatch.DrawString(arial,
                                        "eddie_width: " + _eddie.largura +
                                        "  eddie_posicao: " + _eddie._posicao +
                                        "  eddie_health:  " + _eddie._heroHealth +
                                        "\nviewport" + GraphicsDevice.Viewport +
                                        "\nfundo_width " + backGround.fundo_imagem.Bounds +
                                        "\nquadro_lenth " + backGround.fundo_quadro.Length()
                                        ,
                                        Vector2.Zero,
                                        Color.White);

                    for (int i = 0; i < shoots.Count; i++)
                    {
                        shoots[i].Draw(spriteBatch);
                        
                    }
                    break;

                case Telas.FASE2:

                    break;

                case Telas.FASE3:

                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
