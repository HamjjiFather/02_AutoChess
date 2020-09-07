using I2.Loc;


namespace Helper
{
    public static class LocalizeHelper
    {
        public static string FromName (string term)
        {
            return LocalizationManager.GetTranslation ($"NAME/{term}", applyParameters: true);
        }


        public static string FromUI (string term)
        {
            return LocalizationManager.GetTranslation ($"UI/{term}", applyParameters: true);
        }
        
        
        public static string FromDescription (string term)
        {
            return LocalizationManager.GetTranslation ($"DESCRIPTION/{term}", applyParameters: true);
        }
        
        
        public static string FromSystemMessage (string term)
        {
            return LocalizationManager.GetTranslation ($"SYSTEM_MESSAGE/{term}", applyParameters: true);
        }
        
        
        public static string FromQuest (string term)
        {
            return LocalizationManager.GetTranslation ($"QUEST/{term}", applyParameters: true);
        }


        public static string FromDialogEvent (string term)
        {
            return LocalizationManager.GetTranslation ($"DIALOGUE_EVENT/{term}", applyParameters: true);
        }


        public static string FromGameTip (string term)
        {
            return LocalizationManager.GetTranslation ($"GAME_TIP/{term}", applyParameters: true);
        }


        public static string FromBook (string term)
        {
            return LocalizationManager.GetTranslation ($"BOOK/{term}", applyParameters: true);
        }
        
        
        public static string FromAnglerDialogue (string term)
        {
            return LocalizationManager.GetTranslation ($"ANGLER_DIALOGUE/{term}", applyParameters: true);
        }
        
        
        public static string FromEmbedded (string term)
        {
            return LocalizationManager.GetTranslation ($"EMBEDDED/{term}", applyParameters: true);
        }
    }
}