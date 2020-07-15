using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StarScape.Source.Rendering;

namespace StarScape.Source.World
{
	public class Player
	{

		public int xPos { get; private set; }
		public int yPos { get; private set; }
		public int textureID { get; private set; }

		int speed = 1; //tiles per second
		
		public Player(int x, int y)
		{
			xPos = x;
			yPos = y;
		}

		public void Update(GameTime gameTime)
		{
			
			
			if (Keyboard.WasKeyTyped(Keys.W))
			{
				xPos += speed;
			}
			if (Keyboard.WasKeyTyped(Keys.A))
			{
				xPos -= speed;
			}
			if (Keyboard.WasKeyTyped(Keys.W))
			{
				yPos -= speed;
			}
			if (Keyboard.WasKeyTyped(Keys.S))
			{
				yPos += speed;
			}
		}

		public void Load()
		{
			//textureID = LoadHelper.Load<Texture2D>("CharacterTest");
		}
	}
}
