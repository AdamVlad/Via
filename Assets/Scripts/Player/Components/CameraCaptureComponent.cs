using UnityEngine;
using Cinemachine;
using Zenject;

using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Utils.EventBus;
using Assets.Scripts.Extensions;

namespace Assets.Scripts.Player.Components
{
    public class CameraCaptureComponent : ComponentBase, ITickable 
    {
        public CameraCaptureComponent(
            IEventBus<PlayerEvents> eventBus,
            PlayerSettings settings) : base(eventBus, settings)
        {
        }

        protected override void StartInternal()
        {
            base.StartInternal();

            _cinemachineBrain = Camera
                .main
                .GetComponent<CinemachineBrain>()
                .IfNullThrowExceptionOrReturn();

            _dirty = true;
        }

        public void Tick()
        {
            if (_dirty)
            {
                if (_cinemachineBrain.ActiveVirtualCamera == null) return;

                _cinemachineBrain.ActiveVirtualCamera.Follow = 
                    _player.gameObject.transform;

                _dirty = false;
            }
        }

        private CinemachineBrain _cinemachineBrain;
        private bool _dirty;
    }
}