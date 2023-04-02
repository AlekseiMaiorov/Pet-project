using System;
using Infrastructure.Extensions;
using Infrastructure.PersistentProgress;
using Infrastructure.States;
using UnityEngine;

namespace Infrastructure.SaveLoad
{
    internal class SaveLoadService : ISaveLoadService
    {
        public event Action<PlayerProgress> OnLoadData;
        public event Action<PlayerProgress> OnSaveData;
        private const string PROGRESS_KEY = "PlayerProgress";

        private IPersistantProgress _persistantProgress;

        public SaveLoadService(IPersistantProgress persistantProgress)
        {
            _persistantProgress = persistantProgress;
        }

        public void InformLoadDataListeners()
        {
            OnLoadData?.Invoke(_persistantProgress.Progress);
        }

        public PlayerProgress LoadSavedData()
        {
            return PlayerPrefs.GetString(PROGRESS_KEY)?.ToDeserialize<PlayerProgress>();
        }

        public void Save()
        {
            OnSaveData?.Invoke(_persistantProgress.Progress);
            PlayerPrefs.SetString(PROGRESS_KEY, _persistantProgress.Progress.ToJson());
        }
    }
}