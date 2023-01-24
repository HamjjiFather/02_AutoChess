using System;
using System.Collections.Generic;
using System.Linq;
using AutoChess.Domain;
using AutoChess;
using JetBrains.Annotations;
using KKSFramework;
using KKSFramework.Presenter;
using UniRx;
using Zenject;

namespace AutoChess.Presenter
{
    public class OfficeSkillInvestmentModel : IDisposable
    {
        public OfficeSkillInvestment Investment;

        public int TemporaryInvestmentPoint;

        public readonly ReadOnlyReactiveProperty<bool> CanDevestment;

        public readonly ReadOnlyReactiveProperty<bool> CanInvestment;

        public bool HasTemporaryInvested { get; private set; }

        public int Id => Investment.OfficeSkillTableData.Id;

        public OfficeSkillInvestmentModel(OfficeSkillInvestment investment)
        {
            Investment = investment;
            TemporaryInvestmentPoint = investment.InvestedPoint;

            var ob = Observable.EveryUpdate().ObserveEveryValueChanged(_ => TemporaryInvestmentPoint);
            CanInvestment = ob
                .Select(x => Investment.IsInvestable && x < Investment.InvestablePoint)
                .ToReadOnlyReactiveProperty();
            CanDevestment = ob
                .Select(x => x > Investment.InvestedPoint)
                .ToReadOnlyReactiveProperty();
        }

        ~OfficeSkillInvestmentModel()
        {
            Dispose();
        }

        public void TemporaryInvest()
        {
            if (!CanInvestment.Value) return;

            TemporaryInvestmentPoint += 1;
            HasTemporaryInvested = true;
        }


        public void Reset()
        {
            TemporaryInvestmentPoint = 0;
        }


        public void Dispose()
        {
            CanDevestment?.Dispose();
            CanInvestment?.Dispose();
        }
    }

    [UsedImplicitly]
    public class OfficeSkillViewModel : IViewModel, IDisposable
    {
        #region Fields & Property

        [Inject]
        private OfficeSkillSystemManager _officeSkillSystemManager;

        private List<OfficeSkillInvestmentModel> _investmentModels;

        public ReactiveProperty<OfficeSkillBranchType> ShowedOfficeSkillBranch;

        /// <summary>
        /// 포인트 투자 가능?
        /// </summary>
        public ReadOnlyReactiveProperty<bool> CanInvest;

        /// <summary>
        /// 포인트 투자 확인 커맨드.
        /// </summary>
        public ReactiveCommand ConfirmInvestCmd;

        #endregion


        #region Methods

        #region Override

        public void Dispose()
        {
        }

        #endregion


        #region This

        public void Initialize()
        {
            ShowedOfficeSkillBranch = new ReactiveProperty<OfficeSkillBranchType>(OfficeSkillBranchType.Battle);
            (ConfirmInvestCmd = new ReactiveCommand()).Subscribe(_ =>
            {
                var branchType = ShowedOfficeSkillBranch.Value;
                _investmentModels.Where(x => x.HasTemporaryInvested).Foreach(im =>
                {
                    _officeSkillSystemManager.InvestOfficeSkill(branchType, im.Id, im.TemporaryInvestmentPoint);
                    im.Reset();
                });
                
                CanInvest = ShowedOfficeSkillBranch
                    .Select(_ => _investmentModels.Any(x => x.HasTemporaryInvested))
                    .ToReadOnlyReactiveProperty();
            });
            
            CanInvest = ShowedOfficeSkillBranch
                .Select(_ => _investmentModels.Any(x => x.HasTemporaryInvested))
                .ToReadOnlyReactiveProperty();
        }


        public void Deinitialize()
        {
        }


        public List<OfficeSkillInvestmentModel> ChangeBranch(int page)
        {
            var nextPage = (int) ShowedOfficeSkillBranch.Value + page;
            var nextBranch =
                (OfficeSkillBranchType) nextPage.CirculationRange(0,
                    Enum.GetValues(typeof(OfficeSkillBranchType)).Length - 1);
            ShowedOfficeSkillBranch.Value = nextBranch;

            SetBranchSkills();

            return _investmentModels;
        }


        /// <summary>
        /// 해당 브랜치의 탐험대 스킬의 모델 세팅.
        /// </summary>
        public void SetBranchSkills()
        {
            var investments = _officeSkillSystemManager.RequestInvestments(ShowedOfficeSkillBranch.Value);
            var toModels = investments.Select(x => new OfficeSkillInvestmentModel(x)).ToList();
            _investmentModels = toModels;
        }


        public void TemporaryInvest(OfficeSkillInvestmentModel investmentModel)
        {
            investmentModel.TemporaryInvest();
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}