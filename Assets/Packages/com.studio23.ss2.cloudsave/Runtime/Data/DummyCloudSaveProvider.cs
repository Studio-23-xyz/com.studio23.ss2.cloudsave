using Cysharp.Threading.Tasks;
using UnityEngine;


namespace Studio23.SS2.CloudSave.Data
{
    [CreateAssetMenu(fileName = "Dummy Cloud Save Provider", menuName = "Studio-23/SaveSystem/Cloud Provider/Dummy Provider", order = 1)]
    public class DummyCloudSaveProvider : AbstractCloudSaveProvider
    {

        protected internal override UniTask<int> Initialize()
        {
            Debug.Log($"<color=yellow>Dummy Provider</color>:\n Initialized,<color=red> Please replace with proper provider</color>");
            return new UniTask<int>(0);
        }

        protected internal override UniTask<int> UploadToCloud(string slotName, string key, byte[] data)
        {
            Debug.Log($"<color=yellow>Dummy Provider</color> :\n{data.Length} bytes uploaded to container:<color=white>{slotName}</color> with Key:<color=white>{key}</color>");
            return new UniTask<int>(0);
        }

        protected internal override UniTask<byte[]> DownloadFromCloud(string slotName, string key)
        {
            Debug.Log($"<color=yellow>Dummy Provider</color>:\nFile Downloaded from container:<color=white>{slotName}</color> with Key:<color=white>{key}</color>");
            byte[] data = null;
            return new UniTask<byte[]>(data);
        }

    }
}