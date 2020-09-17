using System;
using System.Linq;
using Helper;
using KKSFramework.Navigation;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public class BattleDamageElement : ElementBase<BattleDamageModel>
    {
        #region Fields & Property

        public Text damageText;

        public Image damageTypeImage;

        public Animator damageAnimator;

        public Color damageColor;

        public Color restoreColor;

        public override BattleDamageModel ElementData { get; set; }

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region Methods

        public override void SetElement (BattleDamageModel elementData)
        {
            ElementData = elementData;
            damageText.text = ElementData.Amount.ToString ();
            damageText.color = ElementData.DamageType == DamageType.Damage ? damageColor : restoreColor;
            damageTypeImage.gameObject.SetActive ((int) ElementData.DamageType >= (int) DamageType.CriticalHeal);

            Observable
                .Timer (TimeSpan.FromSeconds (damageAnimator.runtimeAnimatorController.animationClips.First ().length))
                .TakeUntilDisable (this)
                .Subscribe (_ => { }, Complete);

            void Complete ()
            {
                ObjectPoolingHelper.Despawn (transform);
            }
        }

        #endregion
    }
}