using Runtime.Commands.Player;
using Runtime.Controllers.Player;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public byte StageValue;

        internal ForceBallsToPoolCommand ForceCommand;

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerMovementController _movementController;
        [SerializeField] private PlayerMeshController _meshController;
        [SerializeField] private PlayerPhysicsController _physicsController;

        #endregion

        #region Private Variables

        private PlayerData _data;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetPlayerData();
            SendDataToControllers();
            Init();
        }


        private PlayerData GetPlayerData()
        {
            return Resources.Load<CD_Player>("Data/CD_Player").Data;
        }


        private void SendDataToControllers()
        {
            // _movementController.SetData(_data.MovementData);
            // _meshController.SetData(_data.MeshData);
        }


        private void Init()
        {
            ForceCommand = new ForceBallsToPoolCommand(this, _data.ForceData);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += OnInputTaken;
            InputSignals.Instance.onInputTaken += OnInputReleased;
            InputSignals.Instance.onInputDragged += OnInputDragged;
            UISignals.Instance.onPlay += OnPlay; 
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onStageAreaEntered += OnStageAreaEntered;
            CoreGameSignals.Instance.onStageAreaSuccessful += OnStageAreaSuccessful;
            CoreGameSignals.Instance.onFinishAreaEntered += OnFinishAreaEntered;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void OnPlay()
        {
            _movementController.IsReadyToPlay(true);
        }

        private void OnInputTaken()
        {
            _movementController.IsReadyToMove(true);
        }

        private void OnInputDragged(HorizontalInputParams inputParams)
        {
            _movementController.UpdateInputParams(inputParams);
        }

        private void OnInputReleased()
        {
            _movementController.IsReadyToMove(false);
        }


        private void OnStageAreaEntered()
        {
            _movementController.IsReadyToPlay(false);
        }

        private void OnStageAreaSuccessful(byte value)
        {
            StageValue = (byte)++value;
        }

        private void OnFinishAreaEntered()
        {
            CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
            //Mini Game Yazilacak!
        }

        private void OnLevelFailed()
        {
            _movementController.IsReadyToPlay(false);
        }

        private void OnLevelSuccessful()
        {
            _movementController.IsReadyToPlay(false);
        }
        
        private void OnReset()
        {
            StageValue = 0;
            _movementController.OnReset();
            _physicsController.OnReset();
            _meshController.OnReset();
        }

        private void UnSubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnInputTaken;
            InputSignals.Instance.onInputTaken -= OnInputReleased;
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            UISignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onStageAreaEntered -= OnStageAreaEntered;
            CoreGameSignals.Instance.onStageAreaSuccessful -= OnStageAreaSuccessful;
            CoreGameSignals.Instance.onFinishAreaEntered -= OnFinishAreaEntered;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}