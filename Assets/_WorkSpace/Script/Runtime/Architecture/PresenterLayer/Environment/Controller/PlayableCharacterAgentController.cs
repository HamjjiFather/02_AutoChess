using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
using Zenject;

namespace AutoChess.Presenter
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayableCharacterAgentController : MonoInstaller
    {
        #region Fields & Property

        private AdventureField _adventureField;

        public Camera targetCamera; 

        private NavMeshAgent _agent;

        private Rigidbody2D _rig2D;

        private PlayerInputAction _playerInputAction;

        [SerializeField]
        private float movementSpeed = 1f;

        #endregion


        #region Methods

        #region Override

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _rig2D = GetComponent<Rigidbody2D>();
            _playerInputAction = new PlayerInputAction();
        }


        private void OnEnable()
        {
            _playerInputAction.Enable();
        }


        private void OnDisable()
        {
            _playerInputAction.Disable();
        }


        private void LateUpdate()
        {
            var v2 = _playerInputAction.Player.Move.ReadValue<Vector2>();
            var v3 = new Vector3(v2.x, v2.y, v2.y) * movementSpeed;
            _agent.Move(v3);

            var camTransform = targetCamera.transform;
            var targetPos = transform.position;
            targetPos = new Vector3(targetPos.x, targetPos.y, camTransform.position.z);
            camTransform.position = targetPos;
        }

        #endregion


        #region This

        #endregion


        #region Event

        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log($"Enter: {col}" );

            var tile = col.GetComponent<Tilemap>();
            _adventureField.EnterToForest(tile);
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log($"exit: {other}" );
            _adventureField.ExitFromForest();
        }

        #endregion

        #endregion
    }
}