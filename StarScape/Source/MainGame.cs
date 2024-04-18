using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StarScape.Source;
using StarScape.Source.Rendering;
using StarScape.Source.World;
using Keyboard = StarScape.Source.World.Keyboard;
using Mouse = StarScape.Source.World.Mouse;

namespace StarScape
{
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
		
		public static ContentManager contentManager { get; private set; }

		World world;

		ResolutionIndependentRenderer resolutionIndependentRenderer;
		Camera2D cam;

		public static int screenWidth = 800;
		public static int screenHeight = 600;
		
		public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);

			graphics.PreferredBackBufferWidth = screenWidth;
			graphics.PreferredBackBufferHeight = screenHeight;
			graphics.ApplyChanges();
			
            Content.RootDirectory = "Content";

			contentManager = this.Content;

			
        }

        protected override void Initialize()
        {
			resolutionIndependentRenderer = new ResolutionIndependentRenderer(this);
			cam = new Camera2D(resolutionIndependentRenderer);
			cam.Zoom = 1f;

			//InitializeResolutionIndependence(screenWidth, screenHeight);

			Window.AllowUserResizing = true;
			Window.ClientSizeChanged += Window_ClientSizeChanged;

			world = new World(ref cam);

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

			cam.RecalculateTransformationMatrices();
		}
		
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

			world.Load();
        }

        protected override void UnloadContent() { }
		
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

			Time.gameTime += gameTime.ElapsedGameTime.Milliseconds;

			world.Update(gameTime);

            base.Update(gameTime);
        }

		protected override void Draw(GameTime gameTime)
        {
			resolutionIndependentRenderer.BeginDraw();

			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, cam.GetViewTransformationMatrix());

			world.Draw(gameTime, spriteBatch);

			spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
