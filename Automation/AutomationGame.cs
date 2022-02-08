using System;
using Automation.Core;
using Automation.Entities;
using Automation.Graphics;
using Automation.Interface;
using Automation.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Automation {
    public class AutomationGame : Game {
        public static AutomationGame Game { get; private set; }
        
        public Registry<TileType, Tile> TileRegistry { get; } = new Registry<TileType, Tile>();
        public Registry<TextureName, Texture2D> TextureRegistry { get; } = new Registry<TextureName, Texture2D>();
        
        public World.World World { get; private set; }
        public Player Player { get; private set; }
        public Camera Camera { get; private set; }
        public Hud Hud { get; private set; }
        
        private GraphicsDeviceManager _graphics;

        public AutomationGame() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Game = this;
        }

        protected override void Initialize() {
            // Spawn player
            Player = new Player();
            Hud = new Hud(Player);
            
            // Generate world
            World = new World.World();
            World.SpawnEntity(Player);
            World.ForceChunkUpdate();

            // Create Camera
            Camera = new Camera(Player);

            base.Initialize();
        }

        protected override void LoadContent() {
            TextureLoader.LoadTextures();
            TileLoader.LoadTiles();
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var deltaTime = Math.Round(Time.GetDelta(gameTime.ElapsedGameTime), 4);
            Camera.PollUpdate(deltaTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            World.Draw(Camera);
            Hud.Draw(Camera);

            base.Draw(gameTime);
        }
    }
}