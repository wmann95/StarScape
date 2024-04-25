
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Microsoft.Xna.Framework.Graphics;

using Newtonsoft.Json.Converters;

using StarScape.Source.TileSystem;
using StarScape.Source.Types.Attributes;
using StarScape.Source.World;

namespace StarScape.Source.Types {

    public class Tile : GameObject
	{

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public TileLayer layer { get; set; }

	}
}
