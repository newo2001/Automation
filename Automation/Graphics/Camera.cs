using System;
using Automation.Entities;
using Automation.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Automation.Graphics {
    public class Camera {
        public float ZoomSpeed { get; set; } = 0.0001f;
        public Matrix ViewMatrix { get; set; } = Matrix.CreateScale(1);

        public float CameraScale {
            get => _cameraScale;

            set {
                _cameraScale = value;
                RecalculateViewMatrix();
            }
        }

        private readonly Player _player;
        private int _lastScrollDistance = 0;
        private float _cameraScale = 1f;

        public Camera(Player player) {
            _player = player;
        }
        
        public Vector2 Position {
            get {
                var windowSize = AutomationGame.Game.Window.ClientBounds.Size.ToVector2();
                return _player.Position + new Vector2(16) - (windowSize/ 2f);
            }
            set {
                var windowSize = AutomationGame.Game.Window.ClientBounds.Size.ToVector2();
                _player.Position = value - new Vector2(16) + (windowSize / 2f);
            }
        }

        public Vector2 PlayerPosition => _player.Position;

        public void PollUpdate(double dt) {
            var newPos = Position;
            var movementSpeed = AutomationGame.Game.Player.MovementSpeed;
            var keyState = Keyboard.GetState();
            
            if (keyState.IsKeyDown(Keys.W))
                newPos.Y -= (float) (movementSpeed.Y * dt);
            if (keyState.IsKeyDown(Keys.S))
                newPos.Y += (float) (movementSpeed.Y * dt);
            if (keyState.IsKeyDown(Keys.A))
                newPos.X -= (float) (movementSpeed.X * dt);
            if (keyState.IsKeyDown(Keys.D))
                newPos.X += (float) (movementSpeed.X * dt);
            Position = newPos;

            var mouseState = Mouse.GetState();
            CameraScale += (mouseState.ScrollWheelValue - _lastScrollDistance) * ZoomSpeed;

            _lastScrollDistance = mouseState.ScrollWheelValue;
        }

        private void RecalculateViewMatrix() {
            ViewMatrix = Matrix.CreateTranslation(-new Vector3(Position, 0))
                        * Matrix.CreateScale(_cameraScale);
        }
    }
}