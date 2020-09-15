using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AutoChess
{
    public class BattleDamageElement : PooingComponent, IElementBase<BattleDamageModel>
    {
        #region Fields & Property

        public Text damageText;

        public Image damageTypeImage;
        
        public Animator damageAnimator;

        public Color damageColor;

        public Color restoreColor;

#pragma warning disable CS0649

#pragma warning restore CS0649

        private UnityAction<BattleDamageElement> _damageElement;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        #endregion


        #region EventMethods

        #endregion

        public BattleDamageModel ElementData { get; set; }
        
        public void SetElement (BattleDamageModel elementData)
        {
            ElementData = elementData;
            damageText.text = ElementData.Amount.ToString();
            damageText.color = elementData.DamageType == DamageType.Damage ? damageColor : restoreColor;
            damageTypeImage.gameObject.SetActive ((int)elementData.DamageType >= (int)DamageType.CriticalHeal);
        }

        public async UniTask SetDespawn (UnityAction<BattleDamageElement> damageElement, CancellationToken token)
        {
            await UniTask.Delay (
                TimeSpan.FromSeconds (damageAnimator.runtimeAnimatorController.animationClips.First ().length), cancellationToken:token);
            
            damageElement.Invoke (this);
        }
    }
}