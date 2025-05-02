using DG.Tweening;
using Runtime.Data.ValueObjects;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Controllers.Player
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [FormerlySerializedAs("_renderer")] [SerializeField] private new Renderer renderer;
        [SerializeField] private TextMeshPro _scaleText;
        [SerializeField] private ParticleSystem _confetti;

        #endregion

        #region Private Variables

        [ShowInInspector] private PlayerMeshData _data;

        #endregion

        #endregion

        internal void SetData(PlayerMeshData data)
        {
            _data = data;
        }

        internal void ScaleUpPlayer()
        {
            renderer.gameObject.transform.DOScaleX(_data.ScaleCounter, 1f).SetEase(Ease.Flash);
        }

        internal void ShowUpText()
        {
            _scaleText.DOFade(1, 0).SetEase(Ease.Flash).OnComplete(() =>
            {
                _scaleText.DOFade(0, 0.30f).SetDelay(0.35f);
                _scaleText.rectTransform.DOAnchorPosY(1f, .65f).SetEase(Ease.Linear);
            });
        }

        internal void PlayConfetti()
        { //emit confetti
            _confetti.Play();
        }

        internal void OnReset()
        {
            renderer.gameObject.transform.DOScaleX(1f,1f).SetEase(Ease.Linear);
        }
    }
}