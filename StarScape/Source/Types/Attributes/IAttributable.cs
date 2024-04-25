using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarScape.Source.Types.Attributes
{
	public interface IAttributable
	{
		public virtual HashSet<Attribute> attributes => new HashSet<Attribute>();

		public virtual void AddAttributes(Attribute[] attributes)
		{
			this.attributes.UnionWith(attributes);
		}
	}
}
