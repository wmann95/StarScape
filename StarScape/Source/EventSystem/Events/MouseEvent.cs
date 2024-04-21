using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using StarScape.Source.Input;

namespace StarScape.Source.EventSystem.Events
{
    public class MouseEvent
	{

		public enum Type
		{
			Pressed,
			Down,
			Released,
			Clicked,
		}

		public readonly Type type;

		public readonly MouseButton mouseButton;

		public readonly Vector2 position;

		public readonly long time;

		public bool handled = false;

		public MouseEvent(Type type, MouseButton mouseButton, Vector2 position)
		{
			this.type = type;
			this.mouseButton = mouseButton;
			this.position = position;
			time = Time.gameTime;
		}

	}
}
