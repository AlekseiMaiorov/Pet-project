namespace Infrastructure.UI
{
    public interface ILoadingScreen
    {
        void DestroyLoadingScreen();
        void TrackLoadingProgress(ITraceableLoadProgress traceableLoadProgress);
    }
}