using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MouseDemo
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Texture2D flower;
		Texture2D mousePointer;
		List<Point> flowers;
		MouseState oldMouseState;
		Point mousePosition;		

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			flowers = new List<Point>();

			base.Initialize();
		}

		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			flower = Content.Load<Texture2D>("flower");
			mousePointer = Content.Load<Texture2D>("MousePointer");
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// Read the mouse.
			var newMouseState = Mouse.GetState();
			mousePosition = newMouseState.Position;
					   
			// Look for mouse events.
			if ((newMouseState.LeftButton == ButtonState.Pressed) && (oldMouseState.LeftButton != ButtonState.Pressed))
			{
				// Left button clicked.  Plant a flower.
				flowers.Add(newMouseState.Position - new Point(flower.Width / 2, flower.Height /2));
			}
			else if ((newMouseState.RightButton == ButtonState.Pressed) && (oldMouseState.RightButton != ButtonState.Pressed))
			{
				flowers.Clear();
			}

			oldMouseState = newMouseState;
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.LawnGreen);

			spriteBatch.Begin();
			flowers.ForEach(f => spriteBatch.Draw(flower, f.ToVector2(), Color.White));
			spriteBatch.Draw(mousePointer, mousePosition.ToVector2(), Color.White);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
