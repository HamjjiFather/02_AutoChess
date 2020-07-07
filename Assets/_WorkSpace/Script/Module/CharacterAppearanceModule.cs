using System;
using System.Threading;
using DG.Tweening;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class CharacterAppearanceModule : MonoBehaviour
    {
        #region Fields & Property

        public Animator characterAniamtor;

        public Image characterImage;

        public Image flashImage;

        public GageElement hpGageElement;

        public GageElement skillGageElement;

        public Transform directionTransform;

#pragma warning disable CS0649

        [Inject]
        private GameSetting _gameSetting;

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        /// <summary>
        /// Set Active.
        /// </summary>
        public void SetActive (bool isActive)
        {
            gameObject.SetActive (isActive);
        }


        /// <summary>
        /// 캐릭터 이미지.
        /// </summary>
        public void SetSprite (Sprite sprite)
        {
            characterImage.sprite = sprite;
        }


        /// <summary>
        /// 플래시 이미지 처리.
        /// </summary>
        public void DoFlashImageTween ()
        {
            flashImage.color = Color.white;
            flashImage.DOColor (new Color (1, 1, 1, 0), 0.1f);
        }


        /// <summary>
        /// 체력 게이지 세팅.
        /// </summary>
        public void SetHealthGageColor (CharacterSideType characterSideType)
        {
            hpGageElement.SetGageColor (characterSideType == CharacterSideType.Player
                ? _gameSetting.playerHealthGageColor : _gameSetting.aiHealthGageColor);
        }


        public void SetRuntimeAnimatorContoller (RuntimeAnimatorController animatorController)
        {
            characterAniamtor.runtimeAnimatorController = animatorController;
        }


        public void ChangeSide (bool isLeft)
        {
            characterImage.transform.localScale = isLeft ? Vector3.one : new Vector3 (-1, 1, 1);
        }

        public async UniTask PlayAnimation (string animationName, CancellationToken cancellationToken)
        {
            characterAniamtor.Play (animationName);
            await UniTask.Delay (TimeSpan.FromSeconds (0.5f), cancellationToken: cancellationToken);
        }


        public void SetValueOnlyHealthGageValue (float now, float max)
        {
            hpGageElement.SetValueOnlyGageValueAsync ((int) now, (int) max);
        }


        public void SetSkillSliderValue (float skillValue)
        {
            skillGageElement.SetSliderValue (skillValue);
        }


        public void SetActiveDirection (bool isActive)
        {
            directionTransform.gameObject.SetActive (isActive);
        }

        public void SetDirection (float rotationValue)
        {
            directionTransform.localEulerAngles = new Vector3 (0, 0, rotationValue);
        }
        

        #endregion


        #region EventMethods

        #endregion
    }
}