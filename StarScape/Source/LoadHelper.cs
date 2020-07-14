using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace StarScape.Source
{
	public static class LoadHelper
	{

		public static T Load<T>(String name)
		{
			
			T t = MainGame.contentManager.Load<T>(name);
			return t;
		}

	}
}
