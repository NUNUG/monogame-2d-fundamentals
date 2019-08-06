using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteRotation
{
	public class AsteroidSprite
	{
		public AsteroidSprite(Texture2D texture, float angle, Vector2 position,
			float rotationalVelocityInDegreesPerSecond, 
			Vector2 linearVelocityInPixelsPerSecond)
		{
			Texture = texture;
			Angle = angle;
			Position = position;
			RotationalVelocityInDegreesPerSecond = rotationalVelocityInDegreesPerSecond;
			LinearVelocityInPixelsPerSecond = linearVelocityInPixelsPerSecond;
		}

		public Texture2D Texture { get; set; }
		public float Angle { get; set; }
		public Vector2 Position { get; set; }
		public float _rotationalVelocityInDegreesPerSecond;
		public float RotationalVelocityInDegreesPerSecond { get => _rotationalVelocityInDegreesPerSecond; set => _rotationalVelocityInDegreesPerSecond = value * 3.14159f / 180; }
		public Vector2 LinearVelocityInPixelsPerSecond { get; set; }
		public Rectangle SourceRectangle => new Rectangle(0, 0, Texture.Width, Texture.Height);
		public Rectangle DestRectangle => new Rectangle(
			Convert.ToInt32(Position.X),
			Convert.ToInt32(Position.Y),
			Convert.ToInt32(Position.X + Texture.Width - 1),
			Convert.ToInt32(Position.Y + Texture.Height - 1));
		public Vector2 Center => new Vector2(Texture.Width / 2.0f, Texture.Height / 2.0f);
		public Vector2 Origin => new Vector2(Texture.Width / 2.0f + Position.X, Texture.Height / 2.0f + Position.Y);
	}
}
