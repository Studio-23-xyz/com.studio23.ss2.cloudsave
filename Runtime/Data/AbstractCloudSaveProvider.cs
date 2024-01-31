using Cysharp.Threading.Tasks;
using System.Collections.Generic;
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
        /// Uploads a save file to the cloud to a slot with the key name as byte[].
        /// </summary>
        /// <param name="slotName">Name of the slot</param>
        /// <param name="key">Unique ID for the file</param>
        /// <param name="data">Raw Byte data</param>
        /// <returns>Status code as int</returns>
        protected internal abstract UniTask<int> UploadToCloud(string slotName,string key, byte[] data);

       
        /// <summary>
        /// Download one file from the cloud from the selected slot with a key. Get byte[] as the file data.
        /// </summary>
        /// <param name="slotName">Name of the slot</param>
        /// <param name="key">Unique ID for the file</param>
        /// <returns>Raw Byte[] as the data</returns>
        protected internal abstract UniTask<byte[]> DownloadFromCloud(string slotName,string key);

        /// <summary>
        /// Download multiple files from the cloud
        /// </summary>
        /// <param name="slotName">Name of the slot</param>
        /// <param name="keys">List of uniqueIDs for each file</param>
        /// <returns>A Dictionary of KeyValue pair of file name key and it's respective data byte[]</returns>
        protected internal abstract UniTask<Dictionary<string, byte[]>> DownloadFromCloud(string slotName, string[] keys);

        /// <summary>
        /// Delete slot data from cloud. Mostly used for debug purposes
        /// </summary>
        /// <param name="slotName">Name of the slot</param>
        /// <returns>Status code as int</returns>
        protected internal abstract UniTask<int> DeleteSlotFromCloud(string slotName);



    }

}
