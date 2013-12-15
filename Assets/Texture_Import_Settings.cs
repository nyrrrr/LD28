using UnityEngine;
using UnityEditor;

public class Texture_Import_Settings : AssetPostprocessor
{
	void OnPreprocessTexture()
	{
		TextureImporter importer = assetImporter as TextureImporter;
		Object asset = AssetDatabase.LoadAssetAtPath(importer.assetPath, typeof(Texture2D));
		if (!asset)
		{
			importer.textureType = TextureImporterType.Advanced;
			importer.spriteImportMode = SpriteImportMode.Single;
			importer.wrapMode = TextureWrapMode.Repeat;
			importer.filterMode = FilterMode.Point;
			importer.textureFormat = TextureImporterFormat.AutomaticTruecolor;
			importer.spritePixelsToUnits = 1;
			string resourcePath = "Assets/Resources/Textures/";
			if (importer.assetPath.Contains(resourcePath))
			{
				//string textureResourcePath = importer.assetPath.Substring(resourcePath.Length, importer.assetPath.Length - resourcePath.Length);
				//Material newMat = new Material(Shader.Find("Transparent/Cutout/Diffuse"));
				//newMat.mainTexture = (Texture2D)Resources.Load("Textures/" + textureResourcePath.Substring(0, textureResourcePath.Length - 4));
				//AssetDatabase.CreateAsset(newMat, "Assets/Resources/Materials/" + textureResourcePath.Substring(0, textureResourcePath.Length - 4) + ".mat");
			}
		}    
	}
}