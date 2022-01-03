using System;
using System.Collections.Generic;
using AutoChess;
using Cysharp.Threading.Tasks;
using KKSFramework;
using KKSFramework.SceneLoad;
using Zenject;

public class ProjectInstall : MonoInstaller
{
    private static readonly List<Type> ManagerTypes = new List<Type> ();

    public override void InstallBindings ()
    {
        SceneLoadProjectManager.Instance.InitManager ();
        BindManager ();
    }

    public override void Start ()
    {
        SceneLoadProjectManager.Instance.ChangeSceneAsync (SceneType.Title).Forget();
    }

    private void BindManager ()
    {
        ManagerTypes.Add (typeof(AbilityManager));
        ManagerTypes.Add (typeof(CharacterManager));
        ManagerTypes.Add (typeof(EquipmentManager));
        ManagerTypes.Foreach (type => { Container.Bind (type).AsSingle (); });
    }

    public static void Initialize ()
    {
        ManagerTypes.Foreach (type =>
        {
            var mgr = (GameManagerBase) ProjectContext.Instance.Container.Resolve (type);
            mgr.Initialize ();
        });
    }


    public static void InitAfterLoadLocalData ()
    {
        ManagerTypes.Foreach (type =>
        {
            var mgr = (GameManagerBase) ProjectContext.Instance.Container.Resolve (type);
            mgr.InitAfterLoadLocalData ();
        });
    }
    
    
    public static void InitAfterLoadTableData ()
    {
        ManagerTypes.Foreach (type =>
        {
            var mgr = (GameManagerBase) ProjectContext.Instance.Container.Resolve (type);
            mgr.InitAfterLoadTableData ();
        });
    }
}