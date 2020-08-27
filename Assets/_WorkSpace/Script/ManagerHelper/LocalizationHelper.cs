using System;
using UniRx;
using UnityEngine;

namespace KKSFramework.Localization
{
    public static class LocalizationHelper
    {
        /// <summary>
        /// Change Language.
        /// </summary>
        public static void ChangeLanguage (GlobalLanguageType languageType)
        {
            LocalizationTextManager.Instance.ChangeLanguage ((int) languageType);
        }


        /// <summary>
        /// Get language changed command pattern.
        /// </summary>
        public static ReactiveCommand ChangeLanguageCommand ()
        {
            return LocalizationTextManager.Instance.LanguageChangeCommand;
        }


        /// <summary>
        /// subscribe on common 
        /// </summary>
        public static void SubscribeChangeCommand (MonoBehaviour monoBehaviour, Action<string> action,
            string key, params object[] args)
        {
            LocalizationTextManager.Instance.LanguageChangeCommand
                .TakeUntilDisable (monoBehaviour)
                .DoOnSubscribe (() => action.Invoke (GetTranslatedString (key, args)))
                .Subscribe (_ => action.Invoke (GetTranslatedString (key, args)));
        }


        /// <summary>
        /// Get translated string.
        /// </summary>
        public static string GetTranslatedString (string key, params object[] args)
        {
            return LocalizationTextManager.Instance.GetTranslatedString (key, args);
        }
    }
}