using Cysharp.Threading.Tasks;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace Studio23.SS2.CloudSave.Core
{
    public class CloudSaveManager : MonoBehaviour
    {
        public static CloudSaveManager Instance;

        [SerializeField] private AbstractCloudSaveProvider _provider;

        public UnityEvent OnInitializationSucess;
        public UnityEvent OnInitializationFail;
        public UnityEvent OnUploadSuccess;
        public UnityEvent OnUploadFail;
        public UnityEvent OnDownloadSuccess;
        public UnityEvent OnDownloadFail;

        void Awake()
        {
            Instance = this;
            _provider = GetComponent<AbstractCloudSaveProvider>();
        }



        /// <summary>
        /// This is responsible for initialization of platform cloud save feature if needed
        /// </summary>
        public async void Initialize()
        {

            if (_provider == null)
            {
                Debug.LogError("Cloud Provider not found. Cloud Save will not work!");
                OnInitializationFail?.Invoke();
                return;
            }

            int result = await _provider.Initialize();

            if (result == 0)
            {
                OnInitializationSucess?.Invoke();
            }
            else
            {
                OnInitializationFail?.Invoke();
            }

        }

        /// <summary>
        /// You should fire OnUploadSuccess in the implementation.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="filepath"></param>
        public async UniTask UploadToCloud(string key, string filepath)
        {
            byte[] data = await File.ReadAllBytesAsync(filepath);
            int result = await _provider.UploadToCloud(key, data);


            if (result == 0)
            {
                OnUploadSuccess?.Invoke();
            }
            else
            {
                OnUploadFail?.Invoke();
            }
        }

        /// <summary>
        /// You should fire OnDownloadSuccess in the implementation
        /// </summary>
        /// <param name="key"></param>
        /// <param name="downloadLocation"></param>
        public async UniTask DownloadFromCloud(string key, string downloadLocation)
        {
            if (_provider == null)
            {
                Debug.Log("Provider can't be null");
                return;
            }

            byte[] data = await _provider.DownloadFromCloud(key);

            await File.WriteAllBytesAsync(downloadLocation, data);

            OnDownloadSuccess?.Invoke();


        }
    }


}


