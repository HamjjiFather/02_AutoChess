using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using UnityEngine;

namespace AutoChess
{
    public class CharacterAppearanceModuleBase : MonoBehaviour, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649
        
        [Resolver]
        private Animator _characterAniamtor;

        [Resolver]
        private SpriteRenderer _characterImage;

#pragma warning restore CS0649

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
        /// 캐릭터 설정.
        /// </summary>
        [Bind]
        public void SetCharacterAppearance (CharacterModel characterModel)
        {
            SetSprite (characterModel.IconImageResources);
            SetRuntimeAnimatorContoller (characterModel.CharacterAnimatorResources);
        }
        

        /// <summary>
        /// 캐릭터 이미지.
        /// </summary>
        public void SetSprite (Sprite sprite)
        {
            _characterImage.sprite = sprite;
        }
        
        
        public async UniTask PlayAnimation (string animationName, CancellationToken cancellationToken)
        {
            _characterAniamtor.Play (animationName);
            await UniTask.Delay (TimeSpan.FromSeconds (0.15f), cancellationToken: cancellationToken);
        }
        

        public void SetRuntimeAnimatorContoller (RuntimeAnimatorController animatorController)
        {
            _characterAniamtor.runtimeAnimatorController = animatorController;
        }

        #endregion
    }
}