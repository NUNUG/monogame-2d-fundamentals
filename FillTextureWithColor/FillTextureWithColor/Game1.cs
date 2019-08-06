using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace FillTextureWithColor
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Texture2D whiteBox;
		Texture2D grumpyCat;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// Create a 1x1 pixel white texture.
			whiteBox = new Texture2D(graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			whiteBox.SetData<Color>(new[] { Color.White });
			using (FileStream fs = new FileStream(@"..\..\..\..\..\..\assets\grumpycat.png", FileMode.Open))
			{
				grumpyCat = Texture2D.FromStream(graphics.GraphicsDevice, fs);
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
			spriteBatch.Begin();

			// Draw the white pixel onto the render target but stretch it to fit a specified rectangle.
			spriteBatch.Draw(whiteBox, new Rectangle(50, 50, 100, 100), Color.White);

			// Skew the grumpy cat!
			spriteBatch.Draw(grumpyCat, new Rectangle(170, 170, 640, 100), Color.White);

			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
