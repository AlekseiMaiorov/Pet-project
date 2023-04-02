using Infrastructure.States;

namespace Infrastructure.PersistentProgress
{
    public class PersistantProgress : IPersistantProgress
    {
        public PlayerProgress Progress { get; set; }
    }
}