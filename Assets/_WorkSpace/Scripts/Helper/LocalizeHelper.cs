using I2.Loc;


namespace Helper
{
    public static class LocalizeHelper
    {
        public static string FromName (string term)
        {
            return LocalizationManager.GetTranslation ($"NAME/{term}", applyParameters: true);
        }

        
        public static string FromNoun (string term)
        {
            return LocalizationManager.GetTranslation ($"NOUN/{term}", applyParameters: false);
        }
        
        
        public static string FromDescription (string term)
        {
            return LocalizationManager.GetTranslation ($"DESCRIPTION/{term}", applyParameters: true);
        }
    }
}