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
	public class TopFloor : Top
	{

		public TopFloor() : base("FloorTile1")
		{

			this.AddAttribute(new AttAirPressure());
			
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

		public override void Draw(SpriteBatch batch)
		{
			//base.Draw(batch);

			//Debug.WriteLine("Test", true);
			//base.Draw(batch);

			float pressureColor = ((AttAirPressure)getAttribute<AttAirPressure>()).airPressure / AttAirPressure.AtmosphericPressure;

			Color color;
			//Console.WriteLine(pressureColor);

			if(pressureColor > 1)
			{
				color = new Color(1/pressureColor, 1/pressureColor, 1f);
			}
			else
			{
				color = new Color(1f, pressureColor, pressureColor);
			}
			

			batch.Draw(texture, new Vector2(parentTile.xPos, parentTile.yPos) * 64 + parentTile.parentTileMap.parentShip.Position, color);

		}

		public void setDebug(bool v)
		{
			((AttAirPressure)getAttribute<AttAirPressure>()).debugFlag = true;
		}
	}
}
