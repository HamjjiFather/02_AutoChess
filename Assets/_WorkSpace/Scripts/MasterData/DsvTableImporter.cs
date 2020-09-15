// 이 파일은 자동생성 되었습니다.


using System.Collections.Generic;
using BaseFrame;
using Cysharp.Threading.Tasks;



namespace MasterData
{
    public static class TsvTableData
    {
        public static int NumberOfTable = 11;
        public static bool IsLoaded { get; private set; }

        #region Loader
        
        public static async UniTask LoadAsync (string basePath,
            ILoadingStepper loadingStepper = null, float startLoadingBarValue = 0, float endLoadingBarValue = 0)
        {
            IsLoaded = true;

            var count = 0;
            var totalCount = (float)NumberOfTable;
            var loadingStepWeight = endLoadingBarValue - startLoadingBarValue;
            await Character.Manager.LoadAsync (basePath);
            IncreaseLoadingStepper ();
            await CharacterLevel.Manager.LoadAsync (basePath);
            IncreaseLoadingStepper ();
            await Skill.Manager.LoadAsync (basePath);
            IncreaseLoadingStepper ();
            await Equipment.Manager.LoadAsync (basePath);
            IncreaseLoadingStepper ();
            await EquipmentStatus.Manager.LoadAsync (basePath);
            IncreaseLoadingStepper ();
            await Status.Manager.LoadAsync (basePath);
            IncreaseLoadingStepper ();
            await StatusGradeRange.Manager.LoadAsync (basePath);
            IncreaseLoadingStepper ();
            await BattleStage.Manager.LoadAsync (basePath);
            IncreaseLoadingStepper ();
            await Particle.Manager.LoadAsync (basePath);
            IncreaseLoadingStepper ();
            await Currency.Manager.LoadAsync (basePath);
            IncreaseLoadingStepper ();
            await AdventureField.Manager.LoadAsync (basePath);
            IncreaseLoadingStepper ();

            void IncreaseLoadingStepper ()
            {
                loadingStepper?.SetLoadingStep (++count / totalCount * loadingStepWeight + startLoadingBarValue);
            }
        }


        public static void Load(string basePath)
        {
            Character.Manager.Load(basePath);
            CharacterLevel.Manager.Load(basePath);
            Skill.Manager.Load(basePath);
            Equipment.Manager.Load(basePath);
            EquipmentStatus.Manager.Load(basePath);
            Status.Manager.Load(basePath);
            StatusGradeRange.Manager.Load(basePath);
            BattleStage.Manager.Load(basePath);
            Particle.Manager.Load(basePath);
            Currency.Manager.Load(basePath);
            AdventureField.Manager.Load(basePath);
        }

        #endregion
    }
}