using Assets.Scripts.Extensions;
using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Player.Components.Base;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player.Components
{
    public sealed class CollisionComponent : ComponentBase, IFixedTickable
    {
        public CollisionComponent(
            IEventBus<PlayerStates> eventBus,
            PlayerSettings settings) : base(eventBus)
        {
            _settings = settings ?? throw new NullReferenceException("PlayerSettings is null"); ;
        }

        public void Start(
            Transform bottomRightRayPoint,
            Transform bottomLeftRayPoint,
            Transform topLeftRayPoint,
            Transform topRightRayPoint)
        {
            _bottomRightRayPoint = bottomRightRayPoint.IfNullThrowExceptionOrReturn();
            _bottomLeftRayPoint = bottomLeftRayPoint.IfNullThrowExceptionOrReturn();
            _topLeftRayPoint = topLeftRayPoint.IfNullThrowExceptionOrReturn();
            _topRightRayPoint = topRightRayPoint.IfNullThrowExceptionOrReturn();
        }

        public void FixedTick()
        {
            Debug.Log("Fixed tick");

            ThrowRay(_bottomRightRayPoint.position, _bottomRightRayPoint.right, _settings.RightRayLength);
            ThrowRay(_bottomRightRayPoint.position, _bottomRightRayPoint.up * -1, _settings.BottomRayLength);
            ThrowRay(_bottomLeftRayPoint.position, _bottomLeftRayPoint.right * -1, _settings.LeftRayLength);
            ThrowRay(_bottomLeftRayPoint.position, _bottomLeftRayPoint.up * -1, _settings.BottomRayLength);
            ThrowRay(_topLeftRayPoint.position, _topLeftRayPoint.right * -1, _settings.LeftRayLength);
            ThrowRay(_topLeftRayPoint.position, _topLeftRayPoint.up, _settings.TopRayLength);
            ThrowRay(_topRightRayPoint.position, _topRightRayPoint.right, _settings.RightRayLength);
            ThrowRay(_topRightRayPoint.position, _topRightRayPoint.up, _settings.TopRayLength);
        }

        private void ThrowRay(Vector3 point, Vector3 direction, float distance)
        {
            Ray2D ray = new Ray2D(point, direction);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, distance);

            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);

            if (hit.collider != null && hit.collider.name == "Ground")
            {
                Debug.Log("Хвала Небесам!!! Луч надежды снизошел на нас и соприкоснулся с LayerPlus!");
            }
        }


        private Transform _bottomRightRayPoint, _bottomLeftRayPoint, _topLeftRayPoint, _topRightRayPoint;

        private readonly PlayerSettings _settings;
    }
}