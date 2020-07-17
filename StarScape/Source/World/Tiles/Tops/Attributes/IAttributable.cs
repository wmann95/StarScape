using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarScape.Source.World.Tiles.Tops.Attributes
{
	/// <summary>
	/// Blueprint for all attributes. It's intentionally a very simple class.
	/// </summary>
	public interface IAttributable
	{
		List<IAttribute> Attributes { get; }

		IAttribute GetAttribute<IAttribute>();
		bool HasAttribute<IAttribute>();
		void AddAttribute<IAttribute>(IAttribute attribute);
	}
}
