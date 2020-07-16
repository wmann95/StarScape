//using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StarScape.Source.World.Tiles.Tops.Attributes;

namespace StarScape.Source.World.Tiles.Tops
{
	/// <summary>
	/// This class is the top that adds a floor tile to the rooms hull.
	/// </summary>
	public class TopFloor : Top
	{

		public TopFloor()
		{

			
			
		}

		public override string GetTexture()
		{
			return "FloorTile1";
		}

		public new void LoadContent()
		{
			base.LoadContent();
			//Console.WriteLine("TopFloor Load");
		}

		public override void Update(GameTime gameTime)
		{
			//System.Console.WriteLine("TestTop");
			base.Update(gameTime);


			//Console.WriteLine("Test");
			/*if (Keyboard.WasKeyTyped(Keys.Space))
			{
				indexInt++;
				if (indexInt == 30) indexInt = 0;

				parentTile.xPos = indexInt % 5;
				parentTile.yPos = indexInt / 5;


			}
			*/


		}

		/// <summary>
		/// This is where I added some logic to tint the floor color based on the air pressure on that tile. Red is less than atmospheric pressure, blue is greater than atmospheric pressure.
		/// </summary>
		/// <param name="batch"></param>
		public override void Draw(SpriteBatch batch)
		{
			base.Draw(batch);
		}
	}
}
