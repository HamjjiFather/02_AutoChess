using UnityEditor;
using UnityEngine.UI;
using KKSFramework.UI;

namespace KKSFramework.Editor
{
    public static class ConvertUGUIToExtensionUIMenu
    {
        #region Static Methods

        [MenuItem ("CONTEXT/Button/Button Component Change", validate = false)]
        public static void ChangeButtonComponent (MenuCommand command)
        {
            var btnComp = (Button) command.context;
            var btnObj = btnComp.gameObject;
            Undo.DestroyObjectImmediate ((Button) command.context);
            var buttonExtension = Undo.AddComponent<ButtonExtension> (btnObj);
            buttonExtension.ReplaceComponent (btnComp);
        }

        [MenuItem ("CONTEXT/Button/Button Component Change", validate = true)]
        public static bool IsBaseButton (MenuCommand command)
        {
            try
            {
                return !(ButtonExtension) command.context;
            }
            catch
            {
                return true;
            }
        }

        #endregion
    }
}