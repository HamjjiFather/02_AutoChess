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
        ViewModelTypes.ForEach (type => { Container.Bind (type).AsSingle (); });
    }

    public static void InstallViewmodel ()
    {
        ViewModelTypes.ForEach (type =>
        {
            var viewmodel = (ViewModelBase) ProjectContext.Instance.Container.Resolve (type);
            viewmodel.Initialize ();
        });
    }
}