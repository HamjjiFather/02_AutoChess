using System;
using Helper;
using KKSFramework.Navigation;
using UniRx;
using UnityEngine;
using Zenject;

namespace AutoChess
{
    public class BattleBulletElement : ElementBase<BattleBulletModel>
    {
        #region Fields & Property

        public override BattleBulletModel ElementData { get; set; }

#pragma warning disable CS0649

        [Inject]
        private SkillViewmodel _skillViewmodel;

#pragma warning restore CS0649

        private IDisposable _movementDisposable;

        private Vector3 _originPosition;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void SetElement (BattleBulletModel elementData)
        {
            ElementData = elementData;
            transform.position = ElementData.Origin;
            Observable.EveryUpdate ()
                .TakeUntilDisable (this)
                .TakeWhile (_ => Vector3.Distance (transform.position, ElementData.Target.position) >= float.Epsilon)
                .Subscribe (_ =>
                {
                    transform.position =
                        Vector3.MoveTowards (transform.position, ElementData.Target.position, Time.deltaTime * 5);
                }, Complete);

            void Complete ()
            {
                _skillViewmodel.ApplySkills (elementData.SkillModel);
                ObjectPoolingHelper.Despawn (transform);
            }
        }

        #endregion


        #region EventMethods

        #endregion
    }
}