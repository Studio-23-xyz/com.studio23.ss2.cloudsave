
using Cysharp.Threading.Tasks;
using Studio23.SS2.CloudSave.Core;
using UnityEngine;


namespace Studio23.SS2.CloudSave.Data
{
    public class DummyProvider : AbstractCloudSaveProvider
    {
      

        protected internal override UniTask<byte[]> DownloadFromCloud(string key)
        {
            Debug.Log($"Dummy Provider : {key} downloaded");
            return new UniTask<byte[]>();
        }

        protected internal override UniTask<int> Initialize()
        {
            Debug.Log($"Dummy Provider Initialized, Please replace with proper provider");
            return new UniTask<int>(0);
        }

        protected internal override UniTask<int> UploadToCloud(string key, byte[] data)
        {
            Debug.Log($"Dummy Provider : {data.Length} bytes uploaded to {key}");
            return new UniTask<int>(0);
        }
    }
}