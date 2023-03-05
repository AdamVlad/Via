using UnityEngine;
using Zenject;

using Assets.Scripts.Extensions;
using Assets.Scripts.Utils.Factories;

namespace Assets.Scripts
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private Transform _playerSpawnPoint;

        [Inject]
        private void Construct(PlayerFactory playerPlayerFactory)
        {
            _playerPlayerFactory = playerPlayerFactory;
        }

        private void Awake()
        {
            _playerSpawnPoint.IfNullThrowException();
        }

        private void Start()
        {
            var player = _playerPlayerFactory.Create();
            player.transform.position = _playerSpawnPoint.position;

        }

        private PlayerFactory _playerPlayerFactory;
    }
}