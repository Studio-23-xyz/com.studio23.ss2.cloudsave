using Studio23.SS2.CloudSave.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Studio23.SS2.CloudSave.Core
{
    public class CloudSaveProviderFactory : MonoBehaviour
    {
        private Dictionary<PlatformProvider, AbstractCloudSaveProvider> _providers;


        internal void Initialize()
        {
            _providers=new Dictionary<PlatformProvider, AbstractCloudSaveProvider>();
            LoadProvidersFromResources();
        }

        internal void LoadProvidersFromResources()
        {
           AbstractCloudSaveProvider[] providers= Resources.LoadAll<AbstractCloudSaveProvider>("SaveSystem/CloudProviders");
            foreach (AbstractCloudSaveProvider provider in providers)
            {
                _providers[provider.PlatformProvider]= provider;
            }

        }


        internal AbstractCloudSaveProvider GetProvider(PlatformProvider provider)
        {
            return _providers[provider];
        }



    }
}