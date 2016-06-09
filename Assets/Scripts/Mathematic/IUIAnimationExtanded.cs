using Assets.Scripts.Animations.Scripts;

namespace Assets.Scripts.Mathematic
{
    public interface IUiAnimationExtanded: IUiAnimation
    {
        void ResetAnimation();
        void PauswAnimatio();
        void StopAnimation();
    }
}