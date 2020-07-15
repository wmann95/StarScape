using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarScape.Source.World.Tiles.Tops
{
	public class TopDoor : Top
	{
		enum DoorType { BasicDoor }

		Dictionary<int, Texture2D> doorTextures = new Dictionary<int, Texture2D>();

		public TopDoor() : base()
		{
			
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
