using System.Collections.Generic;
using DG.Tweening;
using Runtime.Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Controllers.UI
{
    public class LevelPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<Image> _stageImages = new List<Image>();
        [SerializeField] private List<TextMeshProUGUI> _levelTexts = new List<TextMeshProUGUI>();
        
        
        
        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onSetLevelValue += OnSetLevelValue;
            UISignals.Instance.onSetStageColor += OnSetStageColor;
        }

        [Button("OnSetStageColor")]
        private void OnSetStageColor(byte stageValue)
        {
            _stageImages[stageValue].DOColor(new Color(0.9960785f,0.4156863f,0.08627451f), 0.5f);
        }

        private void OnSetLevelValue(byte levelValue)
        {
            var additionalValue = ++levelValue;
            _levelTexts[0].text = additionalValue.ToString();
            additionalValue++;
            _levelTexts[1].text = additionalValue.ToString();
        }

        private void UnSubscribeEvents()
        {
            UISignals.Instance.onSetLevelValue -= OnSetLevelValue;
            UISignals.Instance.onSetStageColor -= OnSetStageColor;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}