using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StarScape.Source;
using StarScape.Source.Rendering;
using StarScape.Source.World;
using Keyboard = StarScape.Source.World.Keyboard;
using Mouse = StarScape.Source.Input.Mouse;

namespace StarScape
{
    public class MainGame : Game
    {
        public static GraphicsDeviceManager graphics { get; private set; }
        SpriteBatch spriteBatch;
		public static Mouse MouseManager = new Mouse();
		
		ShipBuildState world;

		ResolutionIndependentRenderer resolutionIndependentRenderer;

		Camera2D cam;

		static Camera2D activeCamera;
		public static Camera2D ActiveCamera { get { return activeCamera; } }

		public static int screenWidth = 800;
		public static int screenHeight = 600;
		
		public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);

			graphics.PreferredBackBufferWidth = screenWidth;
			graphics.PreferredBackBufferHeight = screenHeight;
			graphics.ApplyChanges();
			
            Content.RootDirectory = "Content";


			LoadHelper.contentManager = this.Content;
			
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

			world = new ShipBuildState();

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

        }

        protected override void UnloadContent() { }
		
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

			Time.gameTime += gameTime.ElapsedGameTime.Milliseconds;

			MouseManager.Update();

			world.Update(gameTime);

            base.Update(gameTime);
        }

		protected override void Draw(GameTime gameTime)
        {
			resolutionIndependentRenderer.BeginDraw();

			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, ActiveCamera.GetViewTransformationMatrix());

			world.Draw(spriteBatch);

			spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
