using Assets.Scripts.Extensions;
using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Player.ComponentsData;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player.Components
{
    public sealed class GroundAndWallCheckerComponent : ObservableComponentDecorator, IFixedTickable
    {
        public GroundAndWallCheckerComponent(
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
            CheckGround(
                _bottomRightRayPoint.position,
                _bottomRightRayPoint.up * -1,
                _settings.BottomRayLength);

            CheckGroundIfIsOnGroundEqualsFalse(
                _bottomLeftRayPoint.position,
                _bottomLeftRayPoint.up * -1,
                _settings.BottomRayLength);
            CheckGroundIfIsOnGroundEqualsFalse(
                _topLeftRayPoint.position, 
                _topLeftRayPoint.up, 
                _settings.TopRayLength);
            CheckGroundIfIsOnGroundEqualsFalse(
                _topRightRayPoint.position, 
                _topRightRayPoint.up,
                _settings.TopRayLength);

            CheckWall(
                _bottomRightRayPoint.position,
                _bottomRightRayPoint.right,
                _settings.RightRayLength);

            CheckWallIfTouchedWallEqualsFalse(
                _bottomLeftRayPoint.position, 
                _bottomLeftRayPoint.right * -1, 
                _settings.LeftRayLength);
            CheckWallIfTouchedWallEqualsFalse(
                _topLeftRayPoint.position, 
                _topLeftRayPoint.right * -1, 
                _settings.LeftRayLength);
            CheckWallIfTouchedWallEqualsFalse(
                _topRightRayPoint.position, 
                _topRightRayPoint.right, 
                _settings.RightRayLength);

            if (DidOneParametersChange())
            {
                _isOnGroundOld = _isOnGround;
                _touchedWallOld = _touchedWall;

                Notify(new GroundAndWallCheckerData
                {
                    IsOnGround = _isOnGround,
                    TouchedWall = _touchedWall
                });
            }
        }

        private void CheckGroundIfIsOnGroundEqualsFalse(Vector3 point, Vector3 direction, float distance)
        {
            if (_isOnGround) return;

            CheckGround(point, direction, distance);
        }

        private void CheckWallIfTouchedWallEqualsFalse(Vector3 point, Vector3 direction, float distance)
        {
            if (_touchedWall) return;

            CheckWall(point, direction, distance);
        }

        private void CheckGround(Vector3 point, Vector3 direction, float distance)
        {
            var hit = ThrowRay(point, direction, distance);

            _isOnGround = hit.collider != null && hit.collider.name == "Ground";
        }

        private void CheckWall(Vector3 point, Vector3 direction, float distance)
        {
            var hit = ThrowRay(point, direction, distance);

            _touchedWall = hit.collider != null && hit.collider.name == "Wall";
        }

        private RaycastHit2D ThrowRay(Vector3 point, Vector3 direction, float distance)
        {
            Ray2D ray = new Ray2D(point, direction);

            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);

            return Physics2D.Raycast(ray.origin, ray.direction, distance);
        }

        private bool DidOneParametersChange()
        {
            return _isOnGround != _isOnGroundOld || _touchedWall != _touchedWallOld;
        }

        private Transform _bottomRightRayPoint, _bottomLeftRayPoint, _topLeftRayPoint, _topRightRayPoint;
        private readonly PlayerSettings _settings;

        private bool _isOnGround;
        private bool _touchedWall;

        private bool _isOnGroundOld;
        private bool _touchedWallOld;
    }
}