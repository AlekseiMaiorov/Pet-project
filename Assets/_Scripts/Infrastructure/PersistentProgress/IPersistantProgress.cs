using Infrastructure.States;

namespace Infrastructure.PersistentProgress
{
    public interface IPersistantProgress
    {
        PlayerProgress Progress { get; set; }
    }
}