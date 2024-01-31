using Cysharp.Threading.Tasks;
using System.Collections.Generic;
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

        protected internal override UniTask<Dictionary<string, byte[]>> DownloadFromCloud(string slotName, string[] keys)
        {
            Debug.Log($"<color=yellow>Dummy Provider</color>:\nFile Downloaded from container:<color=white>{slotName}</color> with Keys Count:<color=white>{keys.Length}</color>");
            return new UniTask<Dictionary<string, byte[]>>();
        }

        protected internal override UniTask<int> DeleteSlotFromCloud(string slotName)
        {
            Debug.Log($"<color=yellow>Dummy Provider</color> :\n<color=white>{slotName}</color> deleted from the cloud");
            return new UniTask<int>(0);
        }
    }
}