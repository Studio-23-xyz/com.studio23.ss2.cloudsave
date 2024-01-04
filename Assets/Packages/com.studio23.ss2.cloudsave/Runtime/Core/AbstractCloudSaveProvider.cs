using Studio23.SS2.CloudSave.Core;
using UnityEngine;
using UnityEngine.Events;


namespace Studio23.SS2.CloudSave.Core
{
    [RequireComponent(typeof(CloudSaveManager))]
    public abstract class AbstractCloudSaveProvider : MonoBehaviour
    {

        /// <summary>
        /// This is responsible for initialization of platform cloud save feature if needed
        /// </summary>
        protected internal abstract void Initialize();

        /// <summary>
        /// You should fire OnUploadSuccess in the implementation.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="filepath"></param>
        protected internal abstract void UploadToCloud(string key, string filepath);

        /// <summary>
        /// You should fire OnDownloadSuccess in the implementation
        /// </summary>
        /// <param name="key"></param>
        /// <param name="downloadLocation"></param>
        protected internal abstract void DownloadFromCloud(string key, string downloadLocation);


    }

}
