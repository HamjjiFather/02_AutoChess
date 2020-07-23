using System;
using System.Collections.Generic;
using AutoChess;
using KKSFramework.DesignPattern;
using Zenject;

public class ProjectInstall : MonoInstaller
{
    private static readonly List<Type> ViewModelTypes = new List<Type> ();

    public override void InstallBindings ()
    {
        BindViewmodel ();
    }

    private void BindViewmodel ()
    {
        ViewModelTypes.Add (typeof(GameViewmodel));
        ViewModelTypes.Add (typeof(BattleViewmodel));
        ViewModelTypes.Add (typeof(CharacterViewmodel));
        ViewModelTypes.Add (typeof(EquipmentViewmodel));
        ViewModelTypes.Add (typeof(SkillViewmodel));
        ViewModelTypes.Add (typeof(StatusViewmodel));
        ViewModelTypes.Add (typeof(StageViewmodel));
        ViewModelTypes.Add (typeof(FieldViewmodel));
        ViewModelTypes.ForEach (type => { Container.Bind (type).AsSingle (); });
    }

    public static void InitViewmodel ()
    {
        ViewModelTypes.ForEach (type =>
        {
            var viewmodel = (ViewModelBase) ProjectContext.Instance.Container.Resolve (type);
            viewmodel.Initialize ();
        });
    }


    public static void InitLocalDataViewmodel ()
    {
        ViewModelTypes.ForEach (type =>
        {
            var viewmodel = (ViewModelBase) ProjectContext.Instance.Container.Resolve (type);
            viewmodel.InitAfterLoadLocalData ();
        });
    }
    
    
    public static void InitTableDataViewmodel ()
    {
        ViewModelTypes.ForEach (type =>
        {
            var viewmodel = (ViewModelBase) ProjectContext.Instance.Container.Resolve (type);
            viewmodel.InitAfterLoadTableData ();
        });
    }
}