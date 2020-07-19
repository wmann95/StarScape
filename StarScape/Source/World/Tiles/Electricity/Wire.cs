using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarScape.Source.World.Tiles.Electricity
{
	public class Wire : Tile, IElectricity
	{
		public override Texture2D TileTexture { get { return null; } }
		public override bool DoesTextureHaveTransparency { get { return true; } }

		private int wireState = 0;

		public Wire(int xPos, int yPos) : base(xPos, yPos, 4)
		{

		}

		public override void Draw(SpriteBatch batch)
		{
			base.Draw(batch);
		}
		
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}
	}
}
