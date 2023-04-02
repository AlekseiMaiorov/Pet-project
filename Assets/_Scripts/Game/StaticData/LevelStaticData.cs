using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.StaticData
{
    [CreateAssetMenu(menuName = "Config/Create LevelAssets", fileName = "DownloadableAssets")]
    public class LevelStaticData : ScriptableObject
    {
        public List<AssetReference> Assets => _assets;
        [SerializeField]
        private List<AssetReference> _assets;
    }
}