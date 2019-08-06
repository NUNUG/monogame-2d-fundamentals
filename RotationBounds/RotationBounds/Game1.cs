using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpriteRotation
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		private (int Width, int Height) screenSize;

		private Texture2D asteroid;
		private Vector2 position;
		private float angle;
		private float scale;
		private float scaleSpeed;
		private float rotationalVelocityInDegreesPerSecond;
		private Vector2 screenCenter;
		private Vector2 asteroidCenter;
		private Vector2 screenOrigin;
		private Vector2 asteroidOrigin;
		private Vector2 asteroidOriginalSize;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			screenSize = (640, 480);
			graphics.PreferredBackBufferWidth = screenSize.Width;
			graphics.PreferredBackBufferHeight = screenSize.Height;
			graphics.ApplyChanges();

			// Asteroid initial state
			angle = 0.0f;
			scale = 1.0f;
			scaleSpeed = 0.0f;
			rotationalVelocityInDegreesPerSecond = 90f;

			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			//asteroid = Content.Load<Texture2D>("asteroid");
			asteroid = Content.Load<Texture2D>("asteroid-withbackground");
			asteroidOriginalSize = new Vector2(asteroid.Width, asteroid.Height);

			// Screen center. (Must be here.  Can't be in Initialize() because asteroid is not initialized yet.)
			screenCenter = new Vector2(screenSize.Width / 2, screenSize.Height / 2);
			asteroidCenter = new Vector2(asteroid.Width / 2, asteroid.Height / 2);
			screenOrigin = screenCenter - asteroidCenter;
		}

		protected override void UnloadContent()
		{
		}

		// Visual helpers ------------------------
		protected void DrawBigPixel(Vector2 point, Texture2D texture, SpriteBatch sb)
		{
			DrawRect(point, texture, sb, new Vector2(6, 6));
		}

		protected void DrawBigPixel(Vector2 position, Texture2D texture, SpriteBatch sb, Vector2 size)
		{
			sb.Draw(texture, new Rectangle(((int)position.X) - ((int)size.X / 2), ((int)position.Y) - ((int)size.X / 2), (int)size.X, (int)size.X), Color.White);
		}

		protected void DrawRect(Vector2 position, Texture2D texture, SpriteBatch sb, Vector2 size)
		{
			sb.Draw(texture, new Rectangle(((int)position.X), ((int)position.Y), (int)size.X, (int)size.X), Color.White);
		}

		protected void DrawRect(Rectangle rect, Texture2D texture, SpriteBatch sb)
		{
			sb.Draw(texture, rect, Color.White);
		}

		protected Texture2D ColorPixel(Color color)
		{
			var result = new Texture2D(graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			result.SetData<Color>(new[] { color });
			return result;
		}
		// End visual helpers ------------------------


		// Rotation  functions ------------------------
		protected float DegToRad(float degrees)
		{
			return degrees * (float)Math.PI / 180;
		}

		protected Vector2[] RotateRect(Rectangle rect, Vector2 rotationOrigin, Vector2 worldTranslation, float angle, float scale)
		{
			return RotatePoints(
				new Vector2[] {
					new Vector2(rect.Left, rect.Top),
					new Vector2(rect.Right * scale, rect.Top),
					new Vector2(rect.Right * scale, rect.Bottom * scale),
					new Vector2(rect.Left, rect.Bottom * scale)
				},
				rotationOrigin,
				worldTranslation,
				angle);
		}

		protected Vector2[] RotatePoints(Vector2[] points, Vector2 rotationOrigin, Vector2 worldTranslation, float angle)
		{
			//List<Vector2> newPoints = new List<Vector2>();
			//var count = points.Length;
			//for (int j = 0; j < count; j++)
			//{
			//	newPoints.Add(RotatePoint(points[j], rotationOrigin, worldTranslation, angle));
			//}
			//return newPoints.ToArray();

			return points
				.Select(p => RotatePoint(p, rotationOrigin, worldTranslation, angle))
				.ToArray();
		}

		// This guy is the only real work horse here to rotate points.  Those above are just convenience functions.
		protected Vector2 RotatePoint(Vector2 point, Vector2 rotationOrigin, Vector2 worldTranslation, float angle)
		{
			var m =
				Matrix.CreateTranslation(-rotationOrigin.X, -rotationOrigin.Y, 0)
				* Matrix.CreateRotationZ(angle)
				* Matrix.CreateTranslation(worldTranslation.X, worldTranslation.Y, 0);

			var result = Vector2.Transform(point, m);
			return result;
		}

		// This determines the minimum bounding rect of a set of points.
		protected Rectangle GetBoundsFromPoints(Vector2[] points)
		{
			var x = (int)points.Min(p => p.X);
			var y = (int)points.Min(p => p.Y);
			return new Rectangle(
				x,
				y,
				(int)points.Max(p => p.X) - x,
				(int)points.Max(p => p.Y) - y
			);
		}
		// End rotation  functions ------------------------

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// Angle
			float elapsedMilliseconds = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
			var radsPerSecond = DegToRad(rotationalVelocityInDegreesPerSecond);
			angle = angle + (float)((radsPerSecond / 1000f * elapsedMilliseconds));

			// Scale
			scale = scale + scaleSpeed / 1000f * elapsedMilliseconds;
			if ((scale > 2.0f) || (scale < 0.5))
				scaleSpeed = -scaleSpeed;
			//scale = 2.0f;

			// Position
			position = screenCenter;
			asteroidOrigin = asteroidCenter;


			base.Update(gameTime);
		}


		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			// Determine bounding rectangle for rotated sprite.
			var rotatedCornerPoints = RotateRect(asteroid.Bounds, asteroidOrigin * scale, screenCenter, angle, scale);
			var rotatedBoundsRect = GetBoundsFromPoints(rotatedCornerPoints);

			// Draw the bounding rectangle.
			DrawRect(rotatedBoundsRect, ColorPixel(Color.SlateGray), spriteBatch);

			// Draw a shadow of the original size.
			DrawRect(screenCenter - asteroidOrigin, ColorPixel(Color.DarkSeaGreen), spriteBatch, asteroidOriginalSize);
			
			// Draw the sprite.
			spriteBatch.Draw(
				asteroid,
				position,
				null,
				Color.White,
				angle,
				asteroidOrigin,
				scale,
				SpriteEffects.None,
				1.0f);

			// Draw a point in each corner of the asteroid texture.  These points were used to create the bounding rectangle for the rotated image.
			rotatedCornerPoints
				.ToList()
				.ForEach(p => DrawBigPixel(p, ColorPixel(Color.Black), spriteBatch));

			// Draw a dot at the upper-left point on the original asteroid
			DrawBigPixel(screenCenter - asteroidOrigin, ColorPixel(Color.Blue), spriteBatch);
			
			// Draw center indicator on asteroid.  This is also the center of the screen.
			DrawBigPixel(screenCenter, ColorPixel(Color.Red), spriteBatch);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
