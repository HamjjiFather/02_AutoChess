using BaseFrame;
using BaseFrame.Navigation;
using Cysharp.Threading.Tasks;
using Helper;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = Helper.Scene;


namespace KKSFramework.InGame
{
    public class EntryScene : SceneController
    {
        public override void FinishTransition ()
        {
            base.FinishTransition ();
            
            Input.multiTouchEnabled = false;
            UniTaskScheduler.UnobservedExceptionWriteLogType = LogType.Exception;
            TreeNavigationHelper.ChangeScene (Scene.Title);
        }
        
        public override Configuration GetRootViewConfiguration ()
        {
            var config = new Configuration.Builder ();
            return config.SetName (Page.EntyPage.ToString (), true)
                .SetLayer (ContentLayer.Page)
                .Build ();
        }
    }
}