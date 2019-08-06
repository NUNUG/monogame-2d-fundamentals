using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace SetScreenResolution
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		// Some screen resolution stuff.
		Texture2D screenRuler;
		(int width, int height) screenSize640x480 = (640, 480);
		(int width, int height) screenSize800x600 = (800, 600);
		(int width, int height) screenSize;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			// Pick a screen size here.
			screenSize = screenSize800x600;

			// Set the screen size
			graphics.PreferredBackBufferWidth = screenSize.width;
			graphics.PreferredBackBufferHeight = screenSize.height;

			// If this is windowed, it seems any screen resolution is allowed.
			// If it is full screen, you will want to make sure it's a supported resolution.
			if (GraphicsAdapter.DefaultAdapter.SupportedDisplayModes.Any(mode => (mode.Width == screenSize.width) && (mode.Height == screenSize.height)))
				graphics.IsFullScreen = true;

			graphics.ApplyChanges();

			// Determine actual screen resolution and output it.
			(int w, int h) screenMode = (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
				GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
			System.Console.WriteLine(screenMode);

			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// Load an image from disk.  This circumvents the content pipeline and is a little improper.
			using (FileStream fs = new FileStream(@"..\..\..\..\..\..\assets\screenruler.png", FileMode.Open, FileAccess.Read))
			{
				screenRuler = Texture2D.FromStream(graphics.GraphicsDevice, fs);
			}
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// Draw the image with no scaling and it should fill the screen and how us the resolution.
			spriteBatch.Begin(samplerState: SamplerState.PointClamp);
			spriteBatch.Draw(screenRuler, Vector2.Zero, Color.White);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
