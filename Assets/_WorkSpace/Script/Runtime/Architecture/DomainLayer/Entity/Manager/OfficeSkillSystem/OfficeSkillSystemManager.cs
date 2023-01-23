using System;
using System.Collections.Generic;
using System.Linq;
using AutoChess.Bundle;
using AutoChess.Repository;
using AutoChess.UseCase;
using JetBrains.Annotations;
using KKSFramework;
using KKSFramework.Base;
using Zenject;

namespace AutoChess.Service
{
    public class OfficeSkillInvestment
    {
        public OfficeSkill OfficeSkillTableData;

        public int InvestedPoint;

        public OfficeSkillInvestment()
        {
        }

        public OfficeSkillInvestment(OfficeSkill officeSkillTableData, int investedPoint)
        {
            OfficeSkillTableData = officeSkillTableData;
            InvestedPoint = investedPoint;
        }

        public int InvestablePoint => OfficeSkillTableData.RequireLevels.Length;

        public bool IsInvestable => InvestablePoint > InvestedPoint;
    }


    public class OfficeSkillBranch
    {
        public OfficeSkillBranch(Dictionary<int, OfficeSkillInvestment> officeSkillSpentModels)
        {
            OfficeSkillSpentModels = officeSkillSpentModels;
        }

        public Dictionary<int, OfficeSkillInvestment> OfficeSkillSpentModels;

        public int InvestmentPoints => OfficeSkillSpentModels.Values.Sum(x => x.InvestedPoint);

        public GameSystemType InvestSkill(int index, int amount = 1)
        {
            OfficeSkillSpentModels[index].InvestedPoint += amount;
            return OfficeSkillSpentModels[index].OfficeSkillTableData.UnlockGameSystem;
        }

        public (int[], GameSystemType[]) Reset()
        {
            var gameSystems = new List<GameSystemType>();
            var indexes = new List<int>();
            OfficeSkillSpentModels.Values.Foreach(m =>
            {
                m.InvestedPoint = 0;
                gameSystems.Add(m.OfficeSkillTableData.UnlockGameSystem);
                indexes.Add(m.OfficeSkillTableData.Id);
            });

            return (indexes.ToArray(), gameSystems.Distinct().ToArray());
        }
    }


    [UsedImplicitly]
    public class OfficeSkillSystemManager : ManagerBase
    {
        #region Fields & Property

        // [Inject]
        // private OfficeSkillRepository _officeSkillRepository;

        [Inject]
        private GameSystemManager _gameSystemManager;

        public Dictionary<OfficeSkillBranchType, OfficeSkillBranch> OfficeSkillBranches;

        #endregion


        #region Methods

        #region Override

        public override void Initialize()
        {
            // OfficeSkillBranches = TableDataManager.Instance.OfficeSkillDict.Values
            //     .Select(x =>
            //     {
            //         var dao = _officeSkillRepository.Read(x.Id);
            //         return new OfficeSkillInvestment(x, dao.SpentPointAmount);
            //     })
            //     .GroupBy(x => x.OfficeSkillTableData.BranchType)
            //     .ToDictionary(x => x.Key, x => new OfficeSkillBranch(x
            //         .ToDictionary(osm => osm.OfficeSkillTableData.Id, osm => osm)));
        }

        #endregion


        #region This

        public IEnumerable<OfficeSkillInvestment> RequestInvestments(OfficeSkillBranchType branchType) =>
            OfficeSkillBranches[branchType].OfficeSkillSpentModels.Values;


        public void InvestOfficeSkill(OfficeSkillBranchType branchType, int index, int amount)
        {
            var gameSystemType = OfficeSkillBranches[branchType].InvestSkill(index, amount);

            if (gameSystemType != GameSystemType.None)
            {
                _gameSystemManager.UnlockGameSystem(gameSystemType);
            }
        }


        // public OfficeSkillResetDto ResetInvestmentPoint(OfficeSkillBranchType branchType)
        // {
        //     var (indexes, gameSystems) = OfficeSkillBranches[branchType].Reset();
        //     var dto = new OfficeSkillResetDto(indexes, gameSystems);
        //     return dto;
        // }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}