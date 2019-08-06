using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace Blending
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Texture2D grumpyCat;
		Texture2D nunugLogo;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// Load 2 images.
			using (FileStream fs = new FileStream(@"..\..\..\..\..\..\Assets\grumpycat.png", FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				grumpyCat = Texture2D.FromStream(graphics.GraphicsDevice, fs);
			}
			using (FileStream fs = new FileStream(@"..\..\..\..\..\..\Assets\nunuglogo.png", FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				nunugLogo = Texture2D.FromStream(graphics.GraphicsDevice, fs);
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
			GraphicsDevice.Clear(Color.Black);

			// Set up this spritebatch to use AlphaBlending.
			spriteBatch.Begin(blendState: BlendState.AlphaBlend);
			spriteBatch.Draw(grumpyCat, Vector2.Zero, Color.White);
			spriteBatch.Draw(nunugLogo, new Vector2(200, 200), new Color(Color.White, 0.5f));	// 0.5 for the alpha channel specifies 50% transparency.
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
