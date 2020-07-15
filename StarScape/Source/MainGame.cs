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
		public static Camera2D cam;

		public static readonly int screenWidth = 800;
		public static readonly int screenHeight = 600;

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
			cam.Zoom = 4/5f;

			world = new World();

			this.IsMouseVisible = true;
			
            base.Initialize();
        }

		private void InitializeResolutionIndependence(int realScreenWidth, int realScreenHeight)
		{
			resolutionIndependentRenderer.virtualWidth = 1536;
			resolutionIndependentRenderer.virtualHeight = 864;
			resolutionIndependentRenderer.screenWidth = realScreenWidth;
			resolutionIndependentRenderer.screenHeight = realScreenHeight;

			cam.RecalculateTransformationMatrices();
		}
		
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

			world.Load();
        }
		
        protected override void UnloadContent()
        {

        }
		
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
			
			Time.gameTime += gameTime.ElapsedGameTime.Milliseconds;

			world.Update(ref cam, gameTime);

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
