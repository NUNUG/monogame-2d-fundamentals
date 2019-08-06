using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KeyboardDemo
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		SpriteFont displayFont;
		string keyboardStatusText;
		KeyboardState oldKbdState;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			keyboardStatusText = string.Empty;
			oldKbdState = Keyboard.GetState();

			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			displayFont = Content.Load<SpriteFont>("DisplayText");
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// Take a snapshot of the state of every key on the keyboard.  
			KeyboardState kbdState = Keyboard.GetState();

			// Use old keyboard state to determine key change events such as "keyup" and "keydown".
			// Useful when accepting typed input or for actions that require a repeated keypress such as a jump action.
			// Watch for this in the debug OUTPUT toolwindow.

			if (kbdState.IsKeyDown(Keys.Space) && !oldKbdState.IsKeyDown(Keys.Space))
				Console.WriteLine("Space was pressed");
			if (!kbdState.IsKeyDown(Keys.Space) && oldKbdState.IsKeyDown(Keys.Space))
				Console.WriteLine("Space was released");


			// Use the current keyboard state only to determine key status, such as when constantly holding down an arrow key for movement or holding down a rapid-fire button.
			keyboardStatusText = string.Join(", ", new[] {
				kbdState.IsKeyDown(Keys.Up) ? "Up" : "",
				kbdState.IsKeyDown(Keys.Down) ? "Down" : "",
				kbdState.IsKeyDown(Keys.Left) ? "Left" : "",
				kbdState.IsKeyDown(Keys.Right) ? "Right" : ""
			}.Where(s => !string.IsNullOrEmpty(s)));

			// Save this state for next time.  
			oldKbdState = kbdState;

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			spriteBatch.Begin();
			spriteBatch.DrawString(displayFont, keyboardStatusText, Vector2.Zero, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 1);
			spriteBatch.DrawString(displayFont, "Hello World", Vector2.Zero, Color.White, 0f, Vector2.Zero, 0.25f, SpriteEffects.None, 1);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
