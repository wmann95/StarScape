using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StarScape.Source;
using StarScape.Source.GameState;
using StarScape.Source.JSON;
using StarScape.Source.Rendering;
using StarScape.Source.World;

using Keyboard = StarScape.Source.Input.Keyboard;
using Mouse = StarScape.Source.Input.Mouse;

namespace StarScape
{
    public class MainGame : Game
	{
		static Camera2D activeCamera;

		public static int screenWidth = 800;
		public static int screenHeight = 600;
		public static Mouse MouseManager = new Mouse();

        public static GraphicsDeviceManager graphics { get; private set; }
		public static Camera2D ActiveCamera { get { return activeCamera; } }

		Camera2D cam;
		ResolutionIndependentRenderer resolutionIndependentRenderer;
		SpriteBatch spriteBatch;
		GameState state;

		
		public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);

			graphics.PreferredBackBufferWidth = screenWidth;
			graphics.PreferredBackBufferHeight = screenHeight;
			graphics.ApplyChanges();
			
            Content.RootDirectory = "Content";


			LoadHelper.Content = this.Content;
			
        }

        protected override void Initialize()
        {
			resolutionIndependentRenderer = new ResolutionIndependentRenderer(this);
			cam = new Camera2D(resolutionIndependentRenderer);
			cam.Zoom = 1f;

			activeCamera = cam;

			//InitializeResolutionIndependence(screenWidth, screenHeight);

			Window.AllowUserResizing = true;
			Window.ClientSizeChanged += Window_ClientSizeChanged;

			state = new ShipBuildState();

			this.IsMouseVisible = true;
			
            base.Initialize();
        }

		private void Window_ClientSizeChanged(object sender, System.EventArgs e)
		{
			
		}

		private void InitializeResolutionIndependence(int realScreenWidth, int realScreenHeight)
		{
			resolutionIndependentRenderer.virtualWidth = 800;
			resolutionIndependentRenderer.virtualHeight = 600;
			resolutionIndependentRenderer.screenWidth = realScreenWidth;
			resolutionIndependentRenderer.screenHeight = realScreenHeight;

			ActiveCamera.RecalculateTransformationMatrices();
		}
		
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

			JsonHandler.LoadData("installables/tile.json");

			GameObjects.PrintRegistered();
        }

        protected override void UnloadContent() { }
		
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

			Time.gameTime += gameTime.ElapsedGameTime.Milliseconds;

			MouseManager.Update();

			state.Update(gameTime);

            base.Update(gameTime);
        }

		protected override void Draw(GameTime gameTime)
        {
			resolutionIndependentRenderer.BeginDraw();

			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, ActiveCamera.GetViewTransformationMatrix());

			state.Draw(spriteBatch);

			spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
