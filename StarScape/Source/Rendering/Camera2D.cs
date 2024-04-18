using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarScape.Source.Rendering
{
	/// <summary>
	/// Magic
	/// </summary>
	public class Camera2D
	{
		public readonly float MaxZoom = 0.1f;

		private float zoom;
		private float rotation;
		private Vector2 position;
		private Matrix transform = Matrix.Identity;
		private bool isViewTransformationDirty = true;
		private Matrix camTranslationMatrix = Matrix.Identity;
		private Matrix camRotationMatrix = Matrix.Identity;
		private Matrix camScaleMatrix = Matrix.Identity;
		private Matrix resTranslationMatrix = Matrix.Identity;

		protected ResolutionIndependentRenderer resIndRenderer;
		private Vector3 camTranslationVector = Vector3.Zero;
		private Vector3 camScaleVector = Vector3.Zero;
		private Vector3 resTranslationVector = Vector3.Zero;

		public Camera2D(ResolutionIndependentRenderer renderer)
		{
			resIndRenderer = renderer;

			zoom = 4/5f;
			rotation = 0f;
			position = Vector2.Zero;
		}

		public Vector2 Position
		{
			get
			{
				return position;
			}
			set
			{
				position = value;
				isViewTransformationDirty = true;
			}
		}

		public float Rotation
		{
			get
			{
				return rotation;
			}
			set
			{
				rotation = value;
				isViewTransformationDirty = true;
			}
		}

		public float Zoom
		{
			get
			{
				return zoom;
			}
			set
			{
				zoom = value;
				if (zoom < MaxZoom)
					zoom = MaxZoom;
				isViewTransformationDirty = true;
			}
		}

		public void Move(Vector2 amount)
		{
			Position += amount;
		}

		public void SetPosition(Vector2 pos)
		{
			Position = pos;
		}

		/// <summary>
		/// This method tells the camera that something about the graphics device has been changed and the camera needs to be updated. Things like resolution changes should subsequently call this method.
		/// </summary>
		/// <returns></returns>
		public Matrix GetViewTransformationMatrix()
		{
			if (isViewTransformationDirty)
			{
				camTranslationVector.X = -position.X;
				camTranslationVector.Y = -position.Y;

				Matrix.CreateTranslation(ref camTranslationVector, out camTranslationMatrix);
				Matrix.CreateRotationZ(rotation, out camRotationMatrix);

				camScaleVector.X = zoom;
				camScaleVector.Y = zoom;
				camScaleVector.Z = 1;

				Matrix.CreateScale(ref camScaleVector, out camScaleMatrix);

				resTranslationVector.X = resIndRenderer.virtualWidth * 0.5f;
				resTranslationVector.Y = resIndRenderer.virtualHeight * 0.5f;
				resTranslationVector.Z = 0;

				Matrix.CreateTranslation(ref resTranslationVector, out resTranslationMatrix);

				transform = camTranslationMatrix *
							camRotationMatrix *
							camScaleMatrix *
							resIndRenderer.GetTransformationMatrix();
				isViewTransformationDirty = false;
			}

			return transform;
		}

		/// <summary>
		/// A long winded way to say "Update the camera"
		/// </summary>
		public void RecalculateTransformationMatrices()
		{
			isViewTransformationDirty = true;
		}


		/// <summary>
		/// Returns the current bounds of the camera. If the camera moves, the bound also moves, and this reflects that.
		/// </summary>
		/// <returns></returns>
		public Vector2 GetCameraBounds()
		{
			return new Vector2(MainGame.screenWidth / Zoom, MainGame.screenHeight / Zoom) + Position;
		}

	}
}
