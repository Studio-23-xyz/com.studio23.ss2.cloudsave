
using Studio23.SS2.CloudSave.Core;
using UnityEngine;


namespace Studio23.SS2.CloudSave.Data
{
    public class DummyProvider : AbstractCloudSaveProvider
    {
        protected internal override void DownloadFromCloud(string key, string downloadLocation)
        {
            Debug.Log($"Dummy Provider : {key} downloaded to {downloadLocation}");
        }

        protected internal override void Initialize()
        {
            Debug.Log($"Dummy Provider Initialized, Please replace with proper provider");
        }

        protected internal override void UploadToCloud(string key, string filepath)
        {
            Debug.Log($"Dummy Provider : {key} uploaded from {filepath}");
        }

    }
}