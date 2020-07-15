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

		//Came in base Monogame project, without the public static and getter and setter.
		public static ContentManager contentManager { get; private set; }

		//Uninstantiated world
		World world;

		//The camera magic
		ResolutionIndependentRenderer resolutionIndependentRenderer;
		public static Camera2D cam;

		//hardcoded screen size that I can change to change the game window size at startup.
		public static readonly int screenWidth = 800;
		public static readonly int screenHeight = 600;
		
		/// <summary>
		///Stuff in here is pretty standard, really just made it so the games content manager can be seen by outsiders through the contentManager static variable. 
		/// </summary>
		public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);

			graphics.PreferredBackBufferWidth = screenWidth;
			graphics.PreferredBackBufferHeight = screenHeight;
			graphics.ApplyChanges();
			
            Content.RootDirectory = "Content";

			contentManager = this.Content;

			
        }

		///<summary>
		///This method is called after the MainGame is constructed. I could have instantiated the world in the constructor, but I felt it'd be better to keep objects related to the actual game
		///in the initialize phase.
		///</summary>
        protected override void Initialize()
        {
			resolutionIndependentRenderer = new ResolutionIndependentRenderer(this);
			cam = new Camera2D(resolutionIndependentRenderer);
			cam.Zoom = 4/5f;

			world = new World();

			this.IsMouseVisible = true;
			
            base.Initialize();
        }
		
		/// <summary>
		/// More camera magic.
		/// </summary>
		/// <param name="realScreenWidth"></param>
		/// <param name="realScreenHeight"></param>
		private void InitializeResolutionIndependence(int realScreenWidth, int realScreenHeight)
		{
			resolutionIndependentRenderer.virtualWidth = 1536;
			resolutionIndependentRenderer.virtualHeight = 864;
			resolutionIndependentRenderer.screenWidth = realScreenWidth;
			resolutionIndependentRenderer.screenHeight = realScreenHeight;

			cam.RecalculateTransformationMatrices();
		}
		
		/// <summary>
		/// LoadContent() gets called after the initilize call and before the update and draw loops begin. While I could load anything at any point in the game,
		/// it makes sense to me to have a load phase that gets most of this done, if not all.
		/// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

			world.Load();
        }
		
		/// <summary>
		/// This is for anything that gets loaded but isn't loaded via the Monogame Pipeline. I don't really know what stuff that'd be, but perhaps stuff like
		/// save files or things like that.
		/// </summary>
        protected override void UnloadContent() { }
		
		/// <summary>
		/// The Update game loop. It is essentially just an Update(gameTime) call that's inside of a while(true) loop. This is where the game physics and non-rendering things that happen over time should be.
		/// </summary>
		/// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
			//this came premade in the project, but it works, though I hardly ever use it.
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

			//I don't really like the XNA GameTime stuff so instead I made a Time class that functions similarly to the Time class in Unity.
			//This just adds the time since the last update in milliseconds to the Time.gameTime clock.
			Time.gameTime += gameTime.ElapsedGameTime.Milliseconds;

			//Call the worlds update method with the camera reference and the gametime parameters. The reference for the cam is necessary because
			//I don't want to make the camera a publicly available thing, but do want to control it inside of the update loops.
			world.Update(ref cam, gameTime);

            base.Update(gameTime);
        }

		/// <summary>
		/// The Draw loop. It is essentially just a Draw(gameTime) call that's inside of a while(true) loop. This is where the rendering should take place, inbetween the the spriteBatch.Begin(...) and End() calls.
		/// </summary>
		/// <param name="gameTime"></param>
		protected override void Draw(GameTime gameTime)
        {
			resolutionIndependentRenderer.BeginDraw(); // gets the rendering stuff for the camera sorted out.

			//gets the spriteBatch to be ready for the stuff being rendered to it. Most of this was added with the camera magic.
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, cam.GetViewTransformationMatrix());

			//Allows the world to go through its drawing phase.
			world.Draw(gameTime, spriteBatch);

			//ends the spriteBatch rendering and gets it ready to be drawn to the screen.
			spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
