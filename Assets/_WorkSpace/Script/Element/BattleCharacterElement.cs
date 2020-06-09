using KKSFramework.GameSystem.GlobalText;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace HexaPuzzle
{
    public class BattleCharacterElement : MonoBehaviour
    {
        #region Fields & Property

        public Image characterImage;

        public Text characterLevelText;

        public Text characterNameText;

        public GageElement expGageElement;

        public GageElement hpGageElement;

        public GageElement skillGageElement;


#pragma warning disable CS0649

#pragma warning restore CS0649

        private CharacterModel _characterModel;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void SetElement (CharacterModel characterModel)
        {
            _characterModel = characterModel;

            characterNameText.GetTranslatedString (_characterModel.CharacterData.Name);

            hpGageElement.SetValue (_characterModel.StatusModel.Health, _characterModel.StatusModel.Health.Value);

            _characterModel.StatusModel.Health.Subscribe (hp =>
            {
                var rectT = hpGageElement.GetComponent<RectTransform> ();
                rectT.sizeDelta = new Vector2 (Mathf.Clamp (hp, 0, 760), rectT.sizeDelta.y);
                hpGageElement.SetValue (hp, hp);
            });

            _characterModel.Exp.Subscribe (exp =>
            {
                var level = GameExtension.GetCharacterLevel (exp);
                var nowExp = (int) (exp - level.CoExp);

                expGageElement.SetValue (nowExp, (int) level.ReqExp);
                characterLevelText.text = $"Lv.{level.LevelString}";
            });
        }

        #endregion


        #region EventMethods

        #endregion
    }
}