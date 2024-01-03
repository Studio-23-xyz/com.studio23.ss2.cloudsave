using UnityEngine;
using UnityEngine.Events;


namespace Studio23.SS2.CloudSave.Core
{
    [RequireComponent(typeof(CloudSaveManager))]
    public abstract class AbstractCloudSaveProvider : MonoBehaviour
    {

        internal protected UnityEvent OnInitializationSucess;
        internal protected UnityEvent OnInitializationFail;
        internal protected UnityEvent OnUploadSuccess;
        internal protected UnityEvent OnUploadFail;
        internal protected UnityEvent OnDownloadSuccess;
        internal protected UnityEvent OnDownloadFail;


        /// <summary>
        /// This is responsible for initialization of platform cloud save feature if needed
        /// </summary>
        internal protected abstract void Initialize();

        /// <summary>
        /// You should fire OnUploadSuccess in the implementation.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="filepath"></param>
        internal protected abstract void UploadToCloud(string key, string filepath);

        /// <summary>
        /// You should fire OnDownloadSuccess in the implementation
        /// </summary>
        /// <param name="key"></param>
        /// <param name="downloadLocation"></param>
        internal protected abstract void DownloadFromCloud(string key, string downloadLocation);




    }

}
