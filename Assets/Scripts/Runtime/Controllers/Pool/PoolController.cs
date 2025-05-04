using System;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Runtime.Controllers.Pool
{
    public class PoolController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<DOTweenAnimation> tweens = new List<DOTweenAnimation>();
        [SerializeField] private TextMeshPro poolText;
        [SerializeField] private byte stageID;
        [SerializeField] private new Renderer renderer;

        #endregion

        #region Private Variables

        [ShowInInspector] private PoolData _data;
        [ShowInInspector] private byte _collectedCount;

        private readonly string _collectable = "Collectable";

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetPoolData();
        }

        private PoolData GetPoolData()
        {
            return Resources.Load<CD_Level>("Data/CD_Level")
                .Levels[(int)CoreGameSignals.Instance.onGetLevelValue?.Invoke()].Pools[stageID];
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onStageAreaSuccessful += OnActivateTweens;
            CoreGameSignals.Instance.onStageAreaSuccessful += OnChangedPoolColor;
        }


        private void OnActivateTweens(byte stageValue)
        {
            if (stageValue != stageID) return;
            foreach (var tween in tweens)
            {
                tween.DOPlay();
            }
        }

        private void OnChangedPoolColor(byte stageValue)
        {
            if (stageValue != stageID) return;
            renderer.material.DOColor(new Color(0.1607842f, 0.6039216f, 0.1766218f), 1f).SetEase(Ease.Linear);
        }

        private void Start()
        {
            SetRequiredAmountText();
        }

        private void SetRequiredAmountText()
        {
            poolText.text = $"0/{_data.RequiredObjectCount}";
        }

        public bool TakeResults(byte managerStageValue)
        {
            if (stageID == managerStageValue)
            {
                return _collectedCount >= _data.RequiredObjectCount;
            }

            return false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(_collectable)) return;
            IncreaseCollectedAmount();
            SetCollectedAmountToPool();
        }

        private void IncreaseCollectedAmount()
        {
            _collectedCount++;
        }

        private void SetCollectedAmountToPool()
        {
            poolText.text = $"{_collectedCount}/{_data.RequiredObjectCount}";
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(_collectable)) return;
            DecreaseCollectedAmount();
            SetCollectedAmountToPool();
        }

        private void DecreaseCollectedAmount()
        {
            _collectedCount--;
        }
    }
}