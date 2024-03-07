using Cysharp.Threading.Tasks;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using Studio23.SS2.CloudSave.Data;

namespace Studio23.SS2.CloudSave.Core
{
    [RequireComponent(typeof(CloudSaveProviderFactory))]
    public class CloudSaveManager : MonoBehaviour
    {
        public static CloudSaveManager Instance;

        private CloudSaveProviderFactory _factory;

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

            _factory = GetComponent<CloudSaveProviderFactory>();
            _factory.Initialize();

            _provider = _factory.GetProvider();
        }



        /// <summary>
        /// This is responsible for initialization of platform cloud save feature if needed
        /// </summary>
        public async UniTask Initialize()
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
        public async UniTask UploadToCloud(string slotName, string key, string filepath)
        {
            byte[] data = await File.ReadAllBytesAsync(filepath);
            int result = await _provider.UploadToCloud(slotName,key, data);


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
        public async UniTask DownloadFromCloud(string slotName, string key, string downloadLocation)
        {
            if (_provider == null)
            {
                Debug.Log("Provider can't be null");
                return;
            }

            byte[] data = await _provider.DownloadFromCloud(slotName,key);
            if(data == null)
            {
                Debug.LogWarning($"Slot: {slotName} and key: {key} has no data");
                return;
            }
            await File.WriteAllBytesAsync(downloadLocation, data);

            OnDownloadSuccess?.Invoke();

        }

        
        public async UniTask DeleteContainerFromCloud(string slotName)
        {
            if (_provider == null)
            {
                Debug.Log("Provider can't be null");
                return;
            }

            await _provider.DeleteSlotFromCloud(slotName);
        }


    }


}


