using System;
using System.Collections.Generic;
using AutoChess;
using Cysharp.Threading.Tasks;
using KKSFramework;
using KKSFramework.SceneLoad;
using Zenject;

public class ProjectInstall : MonoInstaller
{
    public override void InstallBindings ()
    {
        SceneLoadProjectManager.Instance.InitManager ();
    }

    public override void Start ()
    {
        SceneLoadProjectManager.Instance.ChangeSceneAsync (SceneType.Title).Forget();
    }
}