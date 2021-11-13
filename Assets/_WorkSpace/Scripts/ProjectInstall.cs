using System;
using System.Collections.Generic;
using AutoChess;
using Cysharp.Threading.Tasks;
using KKSFramework;
using KKSFramework.DesignPattern;
using KKSFramework.SceneLoad;
using Zenject;

public class ProjectInstall : MonoInstaller
{
    private static readonly List<Type> ViewModelTypes = new List<Type> ();

    public override void InstallBindings ()
    {
        SceneLoadManager.Instance.InitManager ();
        BindViewmodel ();
    }

    public override void Start ()
    {
        SceneLoadManager.Instance.ChangeSceneAsync (SceneType.Title).Forget();
    }

    private void BindViewmodel ()
    {
        ViewModelTypes.Add (typeof(GameViewmodel));
        ViewModelTypes.Add (typeof(BattleViewmodel));
        ViewModelTypes.Add (typeof(CharacterViewmodel));
        ViewModelTypes.Add (typeof(ItemViewmodel));
        ViewModelTypes.Add (typeof(EquipmentViewmodel));
        ViewModelTypes.Add (typeof(SkillViewmodel));
        ViewModelTypes.Add (typeof(StatusViewmodel));
        ViewModelTypes.Add (typeof(AdventureViewmodel));
        ViewModelTypes.Foreach (type => { Container.Bind (type).AsSingle (); });
    }

    public static void InitViewmodel ()
    {
        ViewModelTypes.Foreach (type =>
        {
            var viewmodel = (ViewModelBase) ProjectContext.Instance.Container.Resolve (type);
            viewmodel.Initialize ();
        });
    }


    public static void InitLocalDataViewmodel ()
    {
        ViewModelTypes.Foreach (type =>
        {
            var viewmodel = (ViewModelBase) ProjectContext.Instance.Container.Resolve (type);
            viewmodel.InitAfterLoadLocalData ();
        });
    }
    
    
    public static void InitTableDataViewmodel ()
    {
        ViewModelTypes.Foreach (type =>
        {
            var viewmodel = (ViewModelBase) ProjectContext.Instance.Container.Resolve (type);
            viewmodel.InitAfterLoadTableData ();
        });
    }
}