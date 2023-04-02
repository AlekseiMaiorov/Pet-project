using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.UI
{
    public class LoadingScreen : MonoBehaviour, ILoadingScreen
    {
        
        [SerializeField]
        private float _porgressBarFillDuration = 1f;
        [SerializeField]
        private Image _progressBar;
        [SerializeField]
        private Ease _progressBarFillEase = Ease.Linear;

        private Tweener _tween;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void DestroyLoadingScreen()
        {
            Destroy(this, 1f);
        }

        public void TrackLoadingProgress(ITraceableLoadProgress traceableLoadProgress)
        {
            ResetProgressBar();
            traceableLoadProgress.OnLoaded += UpdateProgressBar;
        }

        private void UpdateProgressBar(float progressValue)
        {
            _tween?.Kill();

            _tween = DOVirtual.Float(_progressBar.fillAmount,
                                     progressValue,
                                     _porgressBarFillDuration,
                                     FillProgressBar)
                              .SetEase(_progressBarFillEase)
                              .SetLink(gameObject).OnKill(CheckProgressBarFill().Forget);
        }

        private async UniTaskVoid CheckProgressBarFill()
        {
            if (_progressBar.fillAmount >= 1f)
            {
                await UniTask.Delay(200,
                                    DelayType.DeltaTime,
                                    PlayerLoopTiming.Update,
                                    this.GetCancellationTokenOnDestroy());
                gameObject.SetActive(false);
                ResetProgressBar();
            }
        }

        private void FillProgressBar(float value)
        {
            _progressBar.fillAmount = value;
        }

        private void ResetProgressBar()
        {
            FillProgressBar(0);
        }
    }
}