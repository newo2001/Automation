using System;
using Automation.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Automation.Entities {
    public class Player : Entity {
        private Vector2 _position;
        public override Vector2 Position {
            get => _position;
            set {
                PlayerMoveEvent?.Invoke(this, new PlayerMoveEventData {
                    Old = Position,
                    New = value
                });
                _position = value;
            }
        }

        public override Texture2D Texture => AutomationGame.Game.TextureRegistry.Get(TextureName.TextureVoid);
        
        // Pixels per tick
        public Vector2 MovementSpeed { get; set; }
        
        public event EventHandler<PlayerMoveEventData> PlayerMoveEvent;

        public struct PlayerMoveEventData {
            public Vector2 Old { get; set; }
            public Vector2 New { get; set; }
        }

        public Player(Vector2 pos) {
            _position = pos;
            MovementSpeed = new Vector2(8);
        }
        
        public Player() : this(new Vector2(0, 0)) { }
    }
}