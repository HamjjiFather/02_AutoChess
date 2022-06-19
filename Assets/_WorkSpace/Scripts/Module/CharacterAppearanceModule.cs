using KKSFramework.DataBind;
using UnityEngine;
using Zenject;

namespace AutoChess
{
    public class CharacterAppearanceModule : CharacterAppearanceModuleBase
    {
        #region Fields & Property

#pragma warning disable CS0649
        
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



        public void ChangeSide (bool isLeft)
        {
            characterImage.transform.localScale = isLeft ? Vector3.one : new Vector3 (-1, 1, 1);
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
    }
}