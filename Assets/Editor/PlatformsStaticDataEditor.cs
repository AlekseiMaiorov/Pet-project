using Game.StaticData;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class PlatformsStaticDataEditor
    {
        [CustomEditor(typeof(PlatformsStaticData))]
        public class PlayerStaticDataEditor : UnityEditor.Editor
        {
            private const string LAUNCH_PLATFORM_START_POSITION = "LaunchPlatformStartPosition";
            private const string PLATFORM_START_POSITION = "PlatformStartPosition";
            private const string TRIGGER_START_POSITION = "TriggerStartPosition";

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                PlatformsStaticData platformsStaticData = (PlatformsStaticData) target;

                if (GUILayout.Button("Find start points"))
                {
                    platformsStaticData.Settings.PlatformStartPosition =
                        GameObject.FindWithTag(PLATFORM_START_POSITION).transform.position;
                    platformsStaticData.Settings.TriggerStartPosition =
                        GameObject.FindWithTag(TRIGGER_START_POSITION).transform.position;
                    platformsStaticData.LaunchPlatformStartPosition =
                        GameObject.FindWithTag(LAUNCH_PLATFORM_START_POSITION).transform.position;
                }

                EditorUtility.SetDirty(target);
            }
        }
    }
}