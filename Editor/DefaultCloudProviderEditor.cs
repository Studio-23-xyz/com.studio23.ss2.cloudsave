using UnityEngine;
using UnityEditor;
using Studio23.SS2.CloudSave.Data;
using System.IO;

namespace Studio23.SS2.CloudSave.Editor
{

    public class DefaultCloudProviderEditor : EditorWindow
    {
        [MenuItem("Studio-23/Save System/CloudProviders/Create Default Provider")]
        static void CreateDefaultProvider()
        {
            DummyCloudSaveProvider providerSettings = ScriptableObject.CreateInstance<DummyCloudSaveProvider>();

            // Create the resource folder path
            string resourceFolderPath = "Assets/Resources/SaveSystem/CloudProviders";
            
            if(!Directory.Exists(resourceFolderPath))
            {
                Directory.CreateDirectory(resourceFolderPath);
            }

            // Create the ScriptableObject asset in the resource folder
            string assetPath = resourceFolderPath + "/DefaultProvider.asset";
            AssetDatabase.CreateAsset(providerSettings, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("Default Cloud Provider created at: " + assetPath);
        }
    }

}
