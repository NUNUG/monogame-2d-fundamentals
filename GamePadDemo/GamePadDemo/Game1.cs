using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GamePadDemo
{
	public class DisplayFlags
	{
		public string Connected { get; set; }
		public string ShowA { get; set; }
		public string ShowB { get; set; }
		public string ShowX { get; set; }
		public string ShowY { get; set; }
		public string ShowLeftShoulder { get; set; }
		public string ShowRightShoulder { get; set; }
		public string ShowLeftStickButton { get; set; }
		public string ShowRightStickButton { get; set; }
		public string dPadDirection { get; set; }
		public string LeftStick { get; set; }
		public string RightStick { get; set; }
	}

	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		SpriteFont font;
		DisplayFlags flags;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			flags = new DisplayFlags();
			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			font = Content.Load<SpriteFont>("Caption");
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			// Took the gamepad button out of this "quit" logic for the purpose of this demo.
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();


			// Read the gamepad.
			var gpstate = GamePad.GetState(PlayerIndex.One);

			// Determine what to show on the screen.
			flags.Connected = gpstate.IsConnected ? "Connected: True" : "Connected: False";
			flags.ShowA = gpstate.Buttons.A == ButtonState.Pressed ? "A" : "";
			flags.ShowB = gpstate.Buttons.B == ButtonState.Pressed ? "B" : "";
			flags.ShowX = gpstate.Buttons.X == ButtonState.Pressed ? "X" : "";
			flags.ShowY = gpstate.Buttons.Y == ButtonState.Pressed ? "Y" : "";
			flags.ShowLeftShoulder = gpstate.Buttons.LeftShoulder == ButtonState.Pressed ? "Left Shoulder" : "";
			flags.ShowRightShoulder = gpstate.Buttons.RightShoulder == ButtonState.Pressed ? "Right Shoulder" : "";
			flags.ShowLeftStickButton = gpstate.Buttons.LeftStick == ButtonState.Pressed ? "Left Stick" : "";
			flags.ShowRightStickButton = gpstate.Buttons.RightStick == ButtonState.Pressed ? "Right Stick" : "";
			flags.dPadDirection = string.Join(", ", new string[] {
				gpstate.DPad.Up == ButtonState.Pressed ? "DPad Up" : "",
				gpstate.DPad.Down == ButtonState.Pressed ? "DPad Down" : "",
				gpstate.DPad.Left == ButtonState.Pressed ? "DPad Left" : "",
				gpstate.DPad.Right == ButtonState.Pressed ? "DPad Right" : ""
			});
			flags.LeftStick = string.Concat("(", gpstate.ThumbSticks.Left.X.ToString(), ", ", gpstate.ThumbSticks.Left.Y.ToString(), ")");
			flags.RightStick = string.Concat("(", gpstate.ThumbSticks.Right.X.ToString(), ", ", gpstate.ThumbSticks.Right.Y.ToString(), ")");
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.DarkBlue);

			int rowNum = 0;
			int rowHeight = 30;
			var textColor = Color.White;
			float rotation = 0.0f;
			float scale = 0.25f;

			spriteBatch.Begin();
			new List<string> {
				flags.Connected,
				flags.dPadDirection,
				flags.ShowA,
				flags.ShowB,
				flags.ShowX,
				flags.ShowY,
				flags.ShowLeftShoulder,
				flags.ShowRightShoulder,
				flags.ShowLeftStickButton,
				flags.ShowRightStickButton,
				flags.LeftStick,
				flags.RightStick
			}.ForEach(s => spriteBatch.DrawString(font, s, new Vector2(10, rowNum++ * rowHeight), textColor, rotation, Vector2.Zero, scale, SpriteEffects.None, 0f));
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
