using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DisplayText
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		SpriteFont scoreFont;
		int score;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			score = 0;
			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
		
			// Load the spritefont as content.
			scoreFont = Content.Load<SpriteFont>("score");
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			score = Convert.ToInt32(gameTime.TotalGameTime.TotalSeconds);

			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			// Without PointClamp SamplerState. (default seems to be AnisotropicClamp)
			spriteBatch.Begin();
			spriteBatch.DrawString(scoreFont, score.ToString(), new Vector2(100, 50), Color.White, 0f, new Vector2(0, 0), 3.0f, SpriteEffects.None, 1);
			spriteBatch.End();

			// With PointClamp SamplerState.
			spriteBatch.Begin(samplerState: SamplerState.PointClamp);
			spriteBatch.DrawString(scoreFont, score.ToString(), new Vector2(100, 200), Color.White, 0f, new Vector2(0, 0), 3.0f, SpriteEffects.None, 1);
			spriteBatch.End();

			// Right now, it looks better with PointClamp.  
			// Now go in and change the font size from 12 to 72 (6x) and scale it down to 1/6th of the 3.0f scale which is 0.5f.  
			// Find the file by opening the Pipeline tool, doing a rebuild, then look at the output for the spritefont.  It shows the filename.  
			// You can also right-click on the node in the pipeline and say OPEN WITH.
			// Now it looks better without PointClamp.

			base.Draw(gameTime);
		}
	}
}