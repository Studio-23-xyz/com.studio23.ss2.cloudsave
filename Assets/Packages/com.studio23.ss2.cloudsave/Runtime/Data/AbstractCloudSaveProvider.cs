using Cysharp.Threading.Tasks;
using UnityEngine;


namespace Studio23.SS2.CloudSave.Data
{
    public abstract class AbstractCloudSaveProvider : ScriptableObject
    {
        [SerializeField]
        protected PlatformProvider _platformProvider;

        public PlatformProvider PlatformProvider
        {
            get
            {
                return _platformProvider;
            }

            set
            {
                _platformProvider = value;
            }

        }
        /// <summary>
        /// This is responsible for initialization of platform cloud save feature if needed
        /// </summary>
        protected internal abstract UniTask<int> Initialize();
        /// <summary>
        /// You should fire OnUploadSuccess in the implementation.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="filepath"></param>
        protected internal abstract UniTask<int> UploadToCloud(string slotName,string key, byte[] data);

        /// <summary>
        /// You should fire OnDownloadSuccess in the implementation
        /// </summary>
        /// <param name="key"></param>
        /// <param name="downloadLocation"></param>
        protected internal abstract UniTask<byte[]> DownloadFromCloud(string slotName,string key);


    }

}
