using Studio23.SS2.AuthSystem.Core;
using Studio23.SS2.CloudSave.Core;
using UnityEngine;
using UnityEngine.Events;

public class CloudSaveManager : MonoBehaviour
{
    [SerializeField] private bool _registerOnStart = true;
    [SerializeField] private AbstractCloudSaveProvider _provider;

    public UnityEvent OnInitializationSucess;
    public UnityEvent OnInitializationFail;
    public UnityEvent OnUploadSuccess;
    public UnityEvent OnUploadFail;
    public UnityEvent OnDownloadSuccess;
    public UnityEvent OnDownloadFail;


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
    public void Initialize()
    {
        _provider = GetComponent<AbstractCloudSaveProvider>();
        if (_provider == null)
        {
            Debug.LogError("Cloud Provider not found. Cloud Save will not work!");
            OnInitializationFail?.Invoke();
            return;
        }

        RegisterEvents();

        _provider.Initialize();
    }

    /// <summary>
    /// You should fire OnUploadSuccess in the implementation.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="filepath"></param>
    public void UploadToCloud(string key, string filepath)
    {
        _provider.UploadToCloud(key, filepath);
    }

    /// <summary>
    /// You should fire OnDownloadSuccess in the implementation
    /// </summary>
    /// <param name="key"></param>
    /// <param name="downloadLocation"></param>
    public void DownloadFromCloud(string key, string downloadLocation)
    {
        _provider.DownloadFromCloud(key, downloadLocation);
    }


    private void RegisterEvents()
    {
        _provider.OnInitializationSucess.AddListener(()=>OnInitializationSucess?.Invoke());
        _provider.OnInitializationFail.AddListener(() => OnInitializationFail?.Invoke());

        _provider.OnUploadSuccess.AddListener(() => OnUploadSuccess?.Invoke());
        _provider.OnUploadFail.AddListener(() => OnUploadFail?.Invoke());

        _provider.OnDownloadSuccess.AddListener(() => OnDownloadSuccess?.Invoke());
        _provider.OnDownloadFail.AddListener(() => OnDownloadFail?.Invoke());

    }


}
