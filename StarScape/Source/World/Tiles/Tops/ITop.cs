using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarScape.Source.World.Tiles.Tops.Attributes;

namespace StarScape.Source.World.Tiles.Tops
{
	interface ITop : IUpdatable, IDrawable, IAttribute
	{
		Tile ParentTile { get; }
		int TextureID { get; }
		string TextureName { get; }
		string Name { get; }

		string GetTexture();

		void LoadContent();


	}
}
