using Studio23.SS2.CloudSave.Data;
using UnityEngine;

namespace Studio23.SS2.CloudSave.Core
{
    public class CloudSaveProviderFactory : MonoBehaviour
    {
        private  AbstractCloudSaveProvider _dummyProvider;
        private  AbstractCloudSaveProvider _provider;


        internal void Initialize()
        {

            LoadProvidersFromResources();
        }

        private void LoadProvidersFromResources()
        {
           _dummyProvider= Resources.Load<AbstractCloudSaveProvider>("SaveSystem/CloudProviders/DummyCloudSaveProvider");
           _provider = Resources.Load<AbstractCloudSaveProvider>("SaveSystem/CloudProviders/CloudSaveProvider");

        }


        internal AbstractCloudSaveProvider GetProvider()
        {
            return _provider!= null ? _provider : _dummyProvider;
        }



    }
}