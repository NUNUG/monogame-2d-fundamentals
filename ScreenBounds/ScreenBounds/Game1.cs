using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ScreenBounds
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Texture2D ballTexture;
		Vector2 ballPosition;
		float ballSpeed = 150.0f;
		(int MaxX, int MaxY) ScreenBounds;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			ballPosition = new Vector2(0, 0);
			ScreenBounds = (graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			ballTexture = Content.Load<Texture2D>("ball");
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			var kstate = Keyboard.GetState();

			if (kstate.IsKeyDown(Keys.Up))
				ballPosition.Y -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (kstate.IsKeyDown(Keys.Down))
				ballPosition.Y += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (kstate.IsKeyDown(Keys.Left))
				ballPosition.X -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (kstate.IsKeyDown(Keys.Right))
				ballPosition.X += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (ballPosition.X < 0) ballPosition.X = 0;
			if (ballPosition.X > ScreenBounds.MaxX - ballTexture.Width) ballPosition.X = ScreenBounds.MaxX - ballTexture.Width;
			if (ballPosition.Y < 0) ballPosition.Y = 0;
			if (ballPosition.Y > ScreenBounds.MaxY - ballTexture.Height) ballPosition.Y = ScreenBounds.MaxY - ballTexture.Height;

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			spriteBatch.Draw(ballTexture, ballPosition, Color.White);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
