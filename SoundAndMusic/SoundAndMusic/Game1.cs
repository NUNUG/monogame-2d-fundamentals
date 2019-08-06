using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SoundAndMusic
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		private double lastPlayTicks;
		private SoundEffect lineClearSound;
		private Song backgroundMusic;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			lastPlayTicks = 0;
			base.Initialize();
		}

		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			lineClearSound = Content.Load<SoundEffect>("line-clear");
			backgroundMusic = Content.Load<Song>("backgroundMusic");
			MediaPlayer.Play(backgroundMusic);
		}

		protected override void UnloadContent()
		{
			MediaPlayer.Stop();
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// Play sound every 3 seconds.
			if (gameTime.TotalGameTime.TotalMilliseconds - lastPlayTicks > 3000)
			{
				lineClearSound.Play();
				lastPlayTicks = gameTime.TotalGameTime.TotalMilliseconds;
			}

			base.Draw(gameTime);
		}
	}
}
