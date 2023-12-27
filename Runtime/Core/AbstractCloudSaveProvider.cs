using Studio23.SS2.AuthSystem.Core;
using UnityEngine;
using UnityEngine.Events;


namespace Studio23.SS2.CloudSave.Core
{
    public abstract class AbstractCloudSaveProvider : MonoBehaviour
    {

        [SerializeField]internal bool _registerOnStart=true;

        public UnityEvent OnInitializationSucess;
        public UnityEvent OnUploadSuccess;
        public UnityEvent OnDownloadSuccess;


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
            AuthenticationManager.instance.OnAuthSuccess.AddListener(InitializePlatformService);
        }

        /// <summary>
        /// This is responsible for initialization of platform cloud save feature if needed
        /// </summary>
        public abstract void InitializePlatformService();

        /// <summary>
        /// You should fire OnUploadSuccess in the implementation.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="filepath"></param>
        public abstract void UploadToCloud(string key, string filepath);

        /// <summary>
        /// You should fire OnDownloadSuccess in the implementation
        /// </summary>
        /// <param name="key"></param>
        /// <param name="downloadLocation"></param>
        public abstract void DownloadFromCloud(string key, string downloadLocation);




    }

}
