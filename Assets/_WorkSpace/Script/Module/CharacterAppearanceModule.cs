using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using UnityEngine;
using Zenject;

namespace AutoChess
{
    public class CharacterAppearanceModule : MonoBehaviour, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649
        
        [Resolver]
        private Animator _characterAniamtor;

        [Resolver]
        private SpriteRenderer _characterImage;

        [Resolver]
        private GageElement _hpGageElement;

        [Resolver]
        private GageElement _skillGageElement;

        [Resolver]
        private Transform _directionTransform;

        [Inject]
        private CommonColorSetting _commonColorSetting;

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
            _characterImage.sprite = sprite;
        }


        /// <summary>
        /// 플래시 이미지 처리.
        /// </summary>
        public void DoFlashImageTween ()
        {
            // _flashImage.color = Color.white;
            // _flashImage.DOColor (new Color (1, 1, 1, 0), 0.1f);
        }


        /// <summary>
        /// 체력 게이지 세팅.
        /// </summary>
        public void SetHealthGageColor (CharacterSideType characterSideType)
        {
            _hpGageElement.SetGageColor (characterSideType == CharacterSideType.Player
                ? _commonColorSetting.playerHealthGageColor : _commonColorSetting.aiHealthGageColor);
        }


        public void SetRuntimeAnimatorContoller (RuntimeAnimatorController animatorController)
        {
            _characterAniamtor.runtimeAnimatorController = animatorController;
        }


        public void ChangeSide (bool isLeft)
        {
            _characterImage.transform.localScale = isLeft ? Vector3.one : new Vector3 (-1, 1, 1);
        }

        
        public async UniTask PlayAnimation (string animationName, CancellationToken cancellationToken)
        {
            _characterAniamtor.Play (animationName);
            await UniTask.Delay (TimeSpan.FromSeconds (0.15f), cancellationToken: cancellationToken);
        }


        public void SetValueOnlyHealthGageValue (float now, float max)
        {
            _hpGageElement.SetValueOnlyGageValueAsync ((int) now, (int) max);
        }


        public void SetSkillSliderValue (float skillValue)
        {
            _skillGageElement.SetSliderValue (skillValue);
        }


        public void SetActiveDirection (bool isActive)
        {
            _directionTransform.gameObject.SetActive (isActive);
        }

        public void SetDirection (float rotationValue)
        {
            _directionTransform.localEulerAngles = new Vector3 (0, 0, rotationValue);
        }
        

        #endregion


        #region EventMethods

        #endregion
    }
}