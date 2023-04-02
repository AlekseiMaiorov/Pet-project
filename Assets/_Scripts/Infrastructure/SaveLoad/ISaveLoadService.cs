using System;
using Infrastructure.States;

namespace Infrastructure.SaveLoad
{
    public interface ISaveLoadService
    {
        event Action<PlayerProgress> OnLoadData;
        event Action<PlayerProgress> OnSaveData;
        void InformLoadDataListeners();
        PlayerProgress LoadSavedData();
        void Save();
    }
}