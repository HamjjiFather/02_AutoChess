﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.GameSystem.GlobalText
{
    /// <summary>
    /// 변환될 텍스트를 가지고 있는 컴포넌트.
    /// </summary>
    [RequireComponent (typeof (Text))]
    public class GlobalTextComponentUGUIText : GlobalTextComponentBase
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        private Text TargetText => GetCachedComponent<Text> ();

        #endregion


        #region UnityMethods

        private void OnEnable ()
        {
            SetComponent ();
            ChangeText ();
        }

        #endregion


        #region EventMethods

        #endregion


        #region Methods

        /// <summary>
        /// 컴포넌트 세팅.
        /// </summary>
        protected override void SetComponent ()
        {
            GlobalTextManager.Instance.RegistGlobalText (this);
        }

        /// <summary>
        /// 글로벌 텍스트 데이터를 매뉴얼로 세팅.
        /// </summary>
        public void SetGlobalTextData (string keyName, params object[] args)
        {
            translatedInfo.Key = keyName;
            translatedInfo.Args = Array.ConvertAll (args, x => x.ToString ());
        }

        public override void ChangeText ()
        {
            TargetText.GetTranslatedString (translatedInfo.Key, translatedInfo.ToObjectArgs);
        }

        #endregion
    }
}