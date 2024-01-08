using Cysharp.Threading.Tasks;
using Studio23.SS2.CloudSave.Data;
using System;
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


        [SerializeField] public API_States _initializationState;
        [SerializeField] public API_States _downloadState;
        [SerializeField] public API_States _uploadState;
        [SerializeField] float waitTime = 4f;
        [SerializeField] float elapsedTime = 0f;

        void Awake()
        {
            Instance = this;
        }



        /// <summary>
        /// This is responsible for initialization of platform cloud save feature if needed
        /// </summary>
        public async void Initialize()
        {
            _provider = GetComponent<AbstractCloudSaveProvider>();
            if (_provider == null)
            {
                Debug.LogError("Cloud Provider not found. Cloud Save will not work!");
                OnInitializationFail?.Invoke();
                _initializationState = API_States.Failed;
                return;
            }

            _provider.Initialize();

            elapsedTime = 0;
            while (_initializationState == API_States.Process_Started && elapsedTime < waitTime)
            {
                await UniTask.Yield();
                await UniTask.NextFrame();
                elapsedTime += Time.deltaTime;
            }
            
            if (_initializationState == API_States.Process_Started)
            {
                Debug.LogWarning("Initialization took too long.");
            }

            if (_initializationState == API_States.Success)
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
        public async void UploadToCloud(string key, string filepath)
        {
            _provider.UploadToCloud(key, filepath);

            elapsedTime = 0;
            while (_uploadState == API_States.Process_Started && elapsedTime < waitTime)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(0.5), ignoreTimeScale: false);
                elapsedTime += Time.deltaTime;
            }

            if (_uploadState == API_States.Success)
            {
                OnUploadSuccess?.Invoke();
            }

            if (_uploadState == API_States.Failed)
            {
                OnUploadFail?.Invoke();
            }
        }

        /// <summary>
        /// You should fire OnDownloadSuccess in the implementation
        /// </summary>
        /// <param name="key"></param>
        /// <param name="downloadLocation"></param>
        public async void DownloadFromCloud(string key, string downloadLocation)
        {
            _provider.DownloadFromCloud(key, downloadLocation);

            elapsedTime = 0;
            while (_downloadState == API_States.Process_Started && elapsedTime < waitTime)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(0.5), ignoreTimeScale: false);
                elapsedTime += Time.deltaTime;
            }

            if (_downloadState == API_States.Success)
            {
                OnDownloadSuccess?.Invoke();
            }

            if (_downloadState == API_States.Failed)
            {
                OnDownloadFail?.Invoke();
            }

        }
    }


}


