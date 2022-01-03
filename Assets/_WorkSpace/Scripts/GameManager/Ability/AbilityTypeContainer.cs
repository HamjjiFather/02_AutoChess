using System;
using System.Collections.Generic;
using KKSFramework;

namespace AutoChess
{
    /// <summary>
    /// 능력치 타입들의 컨테이너.
    /// </summary>
    public class AbilityTypeContainer : Dictionary<AbilityType, AbilityAllocatorContainer>, IDisposable
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public virtual void Initialize (AbilityType type)
        {
            Add (type, new AbilityAllocatorContainer ());
        }


        public double GetNumberValues (AbilityType abilityType)
        {
            var returnValue = 0d;
            if (ContainsKey (abilityType))
            {
                this[abilityType].Foreach (nc =>
                {
                    // Log.Verbose (nameof (GetNumberValues), $"AbilityType: {abilityType}, Allocator: {nc.Key}, Value: {nc.Value}");
                    returnValue += nc.Value;
                });
            }

            return returnValue;
        }


        public void SetAbilityValue (AbilityType abilityType, string allocatorToString, double value)
        {
            if (!ContainsKey (abilityType))
            {
                Add (abilityType, new AbilityAllocatorContainer ());
            }

            if (this[abilityType].ContainsKey (allocatorToString))
            {
                this[abilityType][allocatorToString] = value;
                return;
            }

            this[abilityType].Add (allocatorToString, value);
        }


        public void AddAbilityValue (AbilityType abilityType, string allocatorToString, double value)
        {
            if (!ContainsKey (abilityType))
            {
                Add (abilityType, new AbilityAllocatorContainer ());
            }

            if (this[abilityType].ContainsKey (allocatorToString))
            {
                this[abilityType][allocatorToString] += value;
                return;
            }

            this[abilityType].Add (allocatorToString, value);
        }

        #endregion


        #region Event

        #endregion

        #endregion


        public void Dispose ()
        {
            Clear ();
        }
    }
}