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

		private Texture2D asteroidTexture;
		private int asteroidCount;
		private List<AsteroidSprite> asteroids;
		private (int Width, int Height) screenSize;
		private float a = 0.0f;
		private Random rnd;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			rnd = new Random();
			asteroidCount = 10;
			//screenSize = (graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
			screenSize = (graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			asteroidTexture = Content.Load<Texture2D>("asteroid");
			asteroids = new List<AsteroidSprite>();

			
			Enumerable.Range(0, asteroidCount).ToList().ForEach(i =>
			  {
				  Console.WriteLine("Creating Asteroid");
				  float angle = rnd.Next(0, 359);
				  //Vector2 position = new Vector2(rnd.Next(0, graphics.PreferredBackBufferWidth - asteroidTexture.Width), rnd.Next(0, graphics.PreferredBackBufferHeight - asteroidTexture.Height));
				  Vector2 position = new Vector2(rnd.Next(0, screenSize.Width - asteroidTexture.Width), rnd.Next(0, screenSize.Height - asteroidTexture.Height));
				  var linearVelocity = new Vector2(rnd.Next(-150, 150), rnd.Next(-150, 150));
				  asteroids.Add(new AsteroidSprite(
					  asteroidTexture, angle,
					  position,
					  360.0f, 
					  linearVelocity));
			  });
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// Move the asteroids.
			float elapsedMilliseconds = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
			asteroids.ForEach(a =>
			{
				a.Angle = a.Angle + (float)((a.RotationalVelocityInDegreesPerSecond / 1000f * elapsedMilliseconds));
				a.Position = a.Position + new Vector2((a.LinearVelocityInPixelsPerSecond.X / 1000f) * elapsedMilliseconds, (a.LinearVelocityInPixelsPerSecond.Y / 1000f) * elapsedMilliseconds);

				// Make 'em bounce
				//if (a.Position.X < 0) a.LinearVelocityInPixelsPerSecond.X *= -1;
				if (a.Position.X < 0) a.LinearVelocityInPixelsPerSecond *= new Vector2(-1, 1);
				if (a.Position.Y < 0) a.LinearVelocityInPixelsPerSecond *= new Vector2(1, -1);
				if (a.Position.X + a.Texture.Width > screenSize.Width) a.LinearVelocityInPixelsPerSecond *= new Vector2(-1, 1);
				if (a.Position.Y + a.Texture.Height > screenSize.Height) a.LinearVelocityInPixelsPerSecond *= new Vector2(1, -1);
			});

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			asteroids.ForEach(a => spriteBatch.Draw(
				a.Texture,
				a.Position + a.Center,
				a.SourceRectangle,
				Color.White,
				a.Angle, //a.Angle,
				//new Vector2(0,0), 
				a.Center,
				0.25f,    // Scale
				SpriteEffects.None,
				1));
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
