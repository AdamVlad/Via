using UnityEngine;
using Zenject;

using Assets.Scripts.Extensions;

namespace Assets.Scripts
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private Transform _playerSpawnPoint;

        [Inject]
        private void Construct(Player.Factory playerFactory)
        {
            _playerFactory = playerFactory;
        }

        private void Awake()
        {
            _playerSpawnPoint.IfNullThrowException();
        }

        private void Start()
        {
            var player = _playerFactory.Create();
            player.transform.position = _playerSpawnPoint.position;

        }

        private Player.Factory _playerFactory;
    }
}