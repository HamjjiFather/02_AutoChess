using UnityEngine;
using UnityEngine.UI;
using KKSFramework.DesignPattern;
using UniRx;

namespace HexaPuzzle
{
    public class StatusModel : ModelBase
    {
        #region Fields & Property

        public readonly IntReactiveProperty MaxHealth = new IntReactiveProperty ();
        
        public readonly IntReactiveProperty Health = new IntReactiveProperty ();

        public readonly IntReactiveProperty SkillGage = new IntReactiveProperty ();

        public readonly IntReactiveProperty Attack = new IntReactiveProperty ();
        
        public readonly IntReactiveProperty Defense = new IntReactiveProperty ();

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region Methods

        #endregion


        public StatusModel (int hp, int sg, int at, int df)
        {
            Health.Value = hp;
            MaxHealth.Value = hp;
            SkillGage.Value = sg;
            Attack.Value = at;
            Defense.Value = df;
        }
    }
}