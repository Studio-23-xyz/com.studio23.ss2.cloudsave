using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using Studio23.SS2.AuthSystem.Core;
using UnityEngine;
using UnityEngine.Events;

[assembly: InternalsVisibleTo("com.studio23.ss2.savesystem.xboxcorepc")]
namespace Studio23.SS2.CloudSave.Core
{
    public class CloudSaveManager : MonoBehaviour
    {
        public static CloudSaveManager Instance;

        [SerializeField] private bool _registerOnStart = true;
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
        /// Calls Registration on start if it's true
        /// </summary>
        public virtual void Start()
        {
            if (_registerOnStart)
            {
                RegisterWithAuthSystem();
            }
        }

        /// <summary>
        /// Registers to auth system however it can be overriden
        /// </summary>
        public virtual void RegisterWithAuthSystem()
        {
            if (AuthenticationManager.instance == null)
            {
                Debug.Log("AuthManager wasn't initialized");
                return;
            }
            AuthenticationManager.instance.OnAuthSuccess.AddListener(Initialize);
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

            while (_uploadState == API_States.Process_Started)
            {
                await UniTask.Yield();
                await UniTask.NextFrame();
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

            while (_downloadState == API_States.Process_Started)
            {
                await UniTask.Yield();
                await UniTask.NextFrame();
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

    public enum API_States
    {
        None,
        Process_Started,
        Success,
        Failed
    }

}


