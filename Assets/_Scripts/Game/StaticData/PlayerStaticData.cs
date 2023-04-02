using Game.Player;
using UnityEngine;

namespace Game.StaticData
{
    [CreateAssetMenu(fileName = "Player Config", menuName = "Config/Player")]
    public class PlayerStaticData : ScriptableObject
    {
        [Space(1f)]
        public PlayerMove.Settings Settings;

        [Space(1f)]
        public PlayerSound.SoundClips SoundClips;
        
        [Space(1f)]
        public Vector3 SpawnPoint;
    }
}