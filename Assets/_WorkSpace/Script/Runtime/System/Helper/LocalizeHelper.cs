namespace Helper
{
    public static class LocalizeHelper
    {
        public static string FromName (string term)
        {
            return default;
        }

        
        public static string FromNoun (string term)
        {
            return default;
            // return LocalizationManager.GetTranslation ($"NOUN/{term}", applyParameters: false);
        }
        
        
        public static string FromDescription (string term)
        {
            return default;
            // return LocalizationManager.GetTranslation ($"DESC/{term}", applyParameters: true);
        }
    }
}