using Game.Platform;
using UnityEngine;

namespace Game.StaticData
{
    [CreateAssetMenu(fileName = "PlatformsConfig", menuName = "Config/PLatforms")]
    public class PlatformsStaticData : ScriptableObject
    {
        public PlatformsMover.Settings Settings;
        public int Count;
        
        [Space(1f)]
        public Vector3 LaunchPlatformStartPosition;
    }
}