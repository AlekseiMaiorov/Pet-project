using Infrastructure.States;

namespace Infrastructure.PersistentProgress
{
    public interface ISaveLoadData : ILoadData
    {
        void SaveData(PlayerProgress progress);
    }
}