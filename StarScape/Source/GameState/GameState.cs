using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StarScape.Source.Rendering;

namespace StarScape.Source.GameState
{
	public abstract class GameState
	{
		static Camera2D activeCamera;
		public static Camera2D ActiveCamera { get { return activeCamera; } }

		public abstract void Draw(SpriteBatch spriteBatch);
		public abstract void Update(GameTime gameTime);
	}
}
