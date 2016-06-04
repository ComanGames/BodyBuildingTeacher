using System;

namespace Assets.Scripts.Animations.Scripts
{
    public interface IUiAnimation
    {
        event Action AniamtionDone;
        void StartAnimation();
    }
}