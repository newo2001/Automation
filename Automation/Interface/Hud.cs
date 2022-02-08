using Automation.Entities;
using Automation.Graphics;
using Automation.Tiles;
using Automation.Core.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Automation.Interface {
    public class Hud {
        private readonly Player _player;
        private readonly SpriteBatch _spriteBatch;

        public Hud(Player player) {
            _player = player;
            _spriteBatch = new SpriteBatch(AutomationGame.Game.GraphicsDevice);
        }
        
       private void DrawCursor(Camera camera) {
           // Calculate snapped cursor position
           var camOffset = new Vector2((int) camera.Position.X % Tile.Size, (int) camera.Position.Y % Tile.Size).Abs();
           var tileCoords = (Mouse.GetState().Position.ToVector2() - camOffset) / Tile.Size;
           tileCoords.Floor();
           var cursorSnapped = (tileCoords * Tile.Size + camOffset).ToPoint();
            
           // Determine cursor color
           var screenSize = AutomationGame.Game.Window.ClientBounds.Size.ToVector2() / 2f;
           var distance = (screenSize - Mouse.GetState().Position.ToVector2()).Length();
           var color = distance < 5 * Tile.Size ? Color.SteelBlue : Color.Red;
           var texture = AutomationGame.Game.TextureRegistry.Get(TextureName.Cursor);
           
           _spriteBatch.Draw(texture, new Rectangle(cursorSnapped, new Point(32)), color);
       }

        public void Draw(Camera camera) {
            _spriteBatch.Begin();
            
            DrawCursor(camera);
            
            _spriteBatch.End();
        }
    }
}