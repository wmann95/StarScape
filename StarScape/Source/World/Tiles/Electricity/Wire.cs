using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StarScape.Source.World.Tiles.Electricity
{


	public class Wire : Tile, IElectricity
	{
		static Texture2D wireTextureAtlas = LoadHelper.LoadTexture("ElectricalCableGrid");
		static Rectangle[][] atlasRectReference;
		static int atlasColumnCount = 4;
		static int atlasRowCount = 4;
		static int textureSize = 64;

		Rectangle destinationRect;

		static Wire()
		{
			atlasRectReference = new Rectangle[atlasColumnCount][];

			for(int i = 0; i < atlasColumnCount; i++)
			{
				atlasRectReference[i] = new Rectangle[atlasRowCount];

				for(int j = 0; j < atlasRowCount; j++)
				{
					atlasRectReference[i][j] = new Rectangle(i * textureSize, j * textureSize, textureSize, textureSize);
				}
			}

		}

		public override Texture2D TileTexture {
			get
			{
				return wireTextureAtlas;
			}
		}

		public override bool DoesTextureHaveTransparency { get { return true; } }

		private int wireState = 0;

		public Wire(int xPos, int yPos, int state) : base(xPos, yPos, 3)
		{
			wireState = state;
			
			//Debug.Log(GetWireTextureRect());
		}

		Rectangle GetWireTextureRect()
		{
			int x = wireState % atlasColumnCount;
			int y = (int)Math.Floor((double)(wireState / atlasRowCount));

			return new Rectangle((int)Math.Round((float)(x * 64)), y * 64, textureSize, textureSize);
		}

		public override void Draw(SpriteBatch batch)
		{
			destinationRect = new Rectangle((int)(xPos * 64 + ParentTileMap.Position.X), (int)(yPos * 64 + ParentTileMap.Position.Y), 64, 64);
			//base.Draw(batch);
			batch.Draw(TileTexture, destinationRect/*(new Vector2(xPos, yPos) * 64 each tile texture is 64px wide.) + ParentTileMap.Position*/, GetWireTextureRect(), Color.White);
		}
		
		public override void Update(GameTime gameTime)
		{
			destinationRect = new Rectangle((int)(xPos * 64 + ParentTileMap.Position.X), (int)(yPos * 64 + ParentTileMap.Position.Y), 64, 64);


			if (Mouse.WasMouseButtonClicked(Mouse.MouseButton.Right) && Mouse.IsMouseInRect(destinationRect))//Mouse.WasMouseButtonClicked(Mouse.MouseButton.Right))
			{
				wireState++;
			}
		}

		int[] GetConnectedNeighbors()
		{
			switch (wireState)
			{
				case 0:
				{
					return new int[] { 0 };
				}
				case 1:
				{
					return new int[] { 2 };
				}
				case 2:
				{
					return new int[] { 4 };
				}
				case 3:
				{
					return new int[] { 6 };
				}
				case 4:
				{
					return new int[] { 0, 2 };
				}
				case 5:
				{
					return new int[] { 2, 4 };
				}
				case 6:
				{
					return new int[] { 4, 6 };
				}
				case 7:
				{
					return new int[] { 0, 6 };
				}
				default:
				{
					return null;
				}
			}


		}

	}
	
}
