using System.IO;
using AutoChess.Bundle;
using UnityEngine;
using Zenject;

namespace KKSFramework.Data
{
    public class BundleInstaller : MonoInstaller
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override

        public override void InstallBindings()
        {
            RegisterInstallItem<CharacterBundle>();
            RegisterInstallItem<AdventureInventoryBundle>();
            RegisterInstallItem<AdventureBundle>();
        }


        public void RegisterInstallItem<TV>() where TV : BundleBase, new()
        {
            var bundle = new TV();
            bundle.FromJsonData();
            bundle.Initialize();
            Container.BindInstance(bundle).AsSingle();
        }


        #endregion


        #region This
        
        
        


        #endregion


        #region Event

        #endregion

        #endregion
    }
}