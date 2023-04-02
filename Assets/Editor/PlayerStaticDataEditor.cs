using Game.StaticData;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(PlayerStaticData))]
    public class PlayerStaticDataEditor : UnityEditor.Editor
    {
        private const string PlayerSpawnPoint = "PlayerSpawnPoint";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            PlayerStaticData playerData = (PlayerStaticData) target;

            if (GUILayout.Button("Find spawn point"))
            {
                playerData.SpawnPoint = GameObject.FindWithTag(PlayerSpawnPoint).transform.position;
            }

            EditorUtility.SetDirty(target);
        }
    }
}