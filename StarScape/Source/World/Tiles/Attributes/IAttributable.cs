using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarScape.Source.World.Tiles.Attributes
{
	/// <summary>
	/// Blueprint for all attributes. It's intentionally a very simple class.
	/// </summary>
	public interface IAttributable
	{
		public HashSet<IAttribute> Attributes { get; }

		public void AddAttributes(IAttribute[] attributes)
		{
			Attributes.UnionWith(attributes);
		}
	}
}
