using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Infrastructure.AssetManagment
{
    public class AddressableProvider
    {
        private readonly List<AsyncOperationHandle> _assetHandles = new List<AsyncOperationHandle>();
        private readonly Dictionary<string, object> _assets = new Dictionary<string, object>();
        private readonly Dictionary<string, string> _assetsName = new Dictionary<string, string>();

        public T Get<T>(string assetKey)
        {
            string assetGuid = assetKey;

            if (!_assets.ContainsKey(assetKey))
            {
                if (!_assetsName.TryGetValue(assetKey, out assetGuid))
                {
                    Debug.LogError($"Asset {assetKey}, don't loaded");
                    return default;
                }
            }

            if (_assets.TryGetValue(assetGuid, out object asset))
            {
                return
                    asset is T type
                        ? type
                        : default;
            }

            Debug.LogError($"Asset {assetGuid}, don't loaded");
            return default;
        }

        public T Get<T>(AssetReference assetReference)
        {
            string assetKey = assetReference.RuntimeKey.ToString();
            return Get<T>(assetKey);
        }

        public async UniTask Load<T>(string assetKey)
        {
            if (_assets.ContainsKey(assetKey))
            {
                return;
            }

            if (_assetsName.ContainsKey(assetKey))
            {
                return;
            }

            try
            {
                AsyncOperationHandle<T> assetHandle = Addressables.LoadAssetAsync<T>(assetKey);
                _assetHandles.Add(assetHandle);

                T asset = await assetHandle.ToUniTask();

                _assets.Add(assetKey, asset);

                var assetName = asset.ToString().Split(' ')[0];

                if (assetName != assetKey)
                {
                    _assetsName.Add(assetName, assetKey);
                }
            }
            catch (InvalidKeyException e)
            {
                Debug.LogException(e);
            }
        }

        public async UniTask Load<T>(AssetReference assetReference)
        {
            string assetKey = assetReference.RuntimeKey.ToString();
            await Load<T>(assetKey);
        }

        public void ReleaseAssets()
        {
            foreach (AsyncOperationHandle asyncOperationHandle in _assetHandles)
            {
                Addressables.Release(asyncOperationHandle);
            }

            _assetsName.Clear();
            _assets.Clear();
        }
    }
}