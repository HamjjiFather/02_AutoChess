using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KKSFramework
{
    public static class SpritePivotChanger
    {
        [MenuItem ("Tools/SpritePivot")]
        public static void Change ()
        {
            var path = "_Image/Monster";
            var texture2Ds = Resources.LoadAll<Texture2D> (path);
            Debug.Log ($"loaded texture count : {texture2Ds.Length}");

            for (var i = 0; i < texture2Ds.Length; i++)
            {
                var texture = texture2Ds[i];
                var filePath = AssetDatabase.GetAssetPath (texture);
                var textureImporter = AssetImporter.GetAtPath (filePath) as TextureImporter;
                textureImporter.isReadable = true;
                var newMeta = new List<SpriteMetaData> ();
                for (var z = 0; z < textureImporter.spritesheet.Length; z++)
                {
                    var spriteMeta = textureImporter.spritesheet[z];
                    spriteMeta.alignment = 9;
                    spriteMeta.pivot = new Vector2 (0.5f, 0);
                    newMeta.Add (spriteMeta);
                }
                textureImporter.spritesheet = newMeta.ToArray ();
                textureImporter.isReadable = false;
                
                AssetDatabase.ImportAsset (filePath, ImportAssetOptions.ForceUpdate);
            }
        }
    }
}