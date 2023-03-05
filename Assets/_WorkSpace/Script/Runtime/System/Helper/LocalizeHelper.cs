using UnityEngine.Localization.Settings;


namespace Helper
{
    public static class LocalizeHelper
    {
        public static string FromName(string term) => GetLocalizedString(term);


        public static string FromNoun(string term)
        {
            return default;
            // return LocalizationManager.GetTranslation ($"NOUN/{term}", applyParameters: false);
        }


        public static string FromDescription(string term)
        {
            return default;
            // return LocalizationManager.GetTranslation ($"DESC/{term}", applyParameters: true);
        }


        public static void ChangeLocale(int index)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        }


        private static string GetLocalizedString(string keyName)
        {
            return LocalizationSettings.StringDatabase.GetLocalizedString("AutoChessStringTable", keyName);
        }
    }
}