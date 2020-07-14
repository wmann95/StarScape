using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StarScape.Source;
using StarScape.Source.Rendering;
using StarScape.Source.World;

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

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
			
            Content.RootDirectory = "Content";

			contentManager = this.Content;

			
        }

        protected override void Initialize()
        {
			resolutionIndependentRenderer = new ResolutionIndependentRenderer(this);
			cam = new Camera2D(resolutionIndependentRenderer);
			cam.Zoom = 1/5f;

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Source.World.Keyboard.GetState().IsKeyDown(Keys.Escape))
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
