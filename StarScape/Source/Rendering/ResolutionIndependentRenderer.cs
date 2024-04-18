using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarScape.Source.Rendering
{
	/// <summary>
	/// Magic... Like actually magic. I don't understand much of anything in here, at least on the matrix math side of things.
	/// </summary>
	public class ResolutionIndependentRenderer
	{
		private readonly MainGame game;
		private Viewport viewport;
		private float xRatio;
		private float yRatio;
		private Vector2 virtualMousePos = new Vector2();

		public Color BackgroundColor = Color.Black;

		public int virtualHeight;
		public int virtualWidth;
		public int screenHeight;
		public int screenWidth;
		public bool isRenderingFinished;
		private static Matrix scaleMatrix;
		private bool isMatrixDirty = true;

		public ResolutionIndependentRenderer(MainGame mainGame)
		{
			game = mainGame;

			virtualWidth = 800;
			virtualHeight = 600;

			screenWidth = 800;
			screenHeight = 600;
		}

		public void Initialize()
		{
			SetupVirtualScreenViewport();

			xRatio = (float)viewport.Width / virtualWidth;
			yRatio = (float)viewport.Height / virtualHeight;

			isMatrixDirty = true;
		}

		public void SetupFullViewport()
		{
			var vp = new Viewport();
			vp.X = vp.Y = 0;
			vp.Width = screenWidth;
			vp.Height = screenHeight;
			game.GraphicsDevice.Viewport = vp;
			isMatrixDirty = true;
		}

		public void BeginDraw()
		{
			SetupFullViewport();
			game.GraphicsDevice.Clear(BackgroundColor);
			SetupVirtualScreenViewport();
		}

		public Matrix GetTransformationMatrix()
		{
			if (isMatrixDirty) RecreateScaleMatrix();

			return scaleMatrix;
		}

		public Vector2 ScaleMouseToScreenCoordinates(Vector2 screenPosition)
		{
			var realX = screenPosition.X - viewport.X;
			var realY = screenPosition.Y - viewport.Y;

			virtualMousePos.X = realX / xRatio;
			virtualMousePos.Y = realY / yRatio;

			return virtualMousePos;
		}

		private void RecreateScaleMatrix()
		{
			Matrix.CreateScale((float)screenWidth / virtualWidth, (float)screenHeight / virtualHeight, 1f, out scaleMatrix);
			isMatrixDirty = false;
		}

		public void SetupVirtualScreenViewport()
		{
			var targetAspectRatio = virtualWidth / (float)virtualHeight;
			var width = screenWidth;
			var height = (int)(width / targetAspectRatio + 0.5f);

			if(height > screenHeight)
			{
				height = screenHeight;
				width = (int)(height * targetAspectRatio + 0.5f);
			}

			viewport = new Viewport
			{
				X = (screenWidth / 2) - (width / 2),
				Y = (screenHeight / 2) - (height / 2),
				Width = width,
				Height = height
			};

			game.GraphicsDevice.Viewport = viewport;
		}
	}
}
