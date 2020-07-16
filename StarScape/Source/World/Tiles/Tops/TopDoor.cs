using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarScape.Source.World.Tiles.Tops
{
	/// <summary>
	/// This Class is one of my current projects. It essentially requires some hands-on work in the base Top class. This class will encompass all doors, from locked security doors to airlocks.
	/// </summary>
	public class TopDoor : Top
	{
		enum DoorType { BasicDoor }

		//Deprecated: When I wrote this, I realized I could do this in the LoadHelper class to help optimize loading, and this is now obsolete.
		//Dictionary<int, Texture2D> doorTextures = new Dictionary<int, Texture2D>();

		public TopDoor()
		{
			
		}
		
		public override string GetTexture()
		{
			return "TopFloor1";
		}

		public override void Draw(SpriteBatch batch)
		{
			throw new NotImplementedException();
		}

		public override void LoadContent()
		{
			throw new NotImplementedException();
		}

		public override void Update(GameTime gameTime)
		{
			throw new NotImplementedException();
		}
	}
}
