using UnityEngine;

namespace Siccity.GLTFUtility
{
	public class GLTFTextureOptimizer
	{
		/// <summary>
		/// Resizes and compresses texure with the according texture resolutions.
		/// </summary>
		/// <param name="tex">referred Texture2D</param>
		/// <param name="type">texture type</param>
		public Texture2D GetResizedTextureByTextureType(Texture2D tex, TextureType type)
		{
			RenderTexture renderTexture;
			switch (type)
			{
				case TextureType.Diffuse:
					renderTexture = new RenderTexture(1024, 1024, 0);
					break;
				case TextureType.Normal:
				case TextureType.ORM:
					renderTexture = new RenderTexture(512, 512, 0);
					break;
				default:
					renderTexture = new RenderTexture(512, 512, 0);
					break;
			}

			renderTexture.enableRandomWrite = true;
			RenderTexture.active = renderTexture;
			Graphics.Blit(tex, renderTexture);

			Texture2D retTex = new Texture2D(renderTexture.width, renderTexture.width, TextureFormat.RGB24, false);
			retTex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
			retTex.Apply();
			retTex.Compress(true);
			return retTex;

		}
	}

	public enum TextureType
	{
		Diffuse = 1,
		Normal = 2,
		ORM = 3
	}
}