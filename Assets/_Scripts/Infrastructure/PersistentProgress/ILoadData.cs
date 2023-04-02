using Infrastructure.States;

namespace Infrastructure.PersistentProgress
{
    public interface ILoadData
    {
        void LoadData(PlayerProgress progress);
    }
}