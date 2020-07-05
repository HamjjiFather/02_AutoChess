using System;
using System.Collections.Generic;
using System.Linq;
using UniRx.Async;
using UnityEngine;

namespace AutoChess
{
    /// <summary>
    /// 라인 체크 방향.
    /// </summary>
    public enum CheckDirectionTypes
    {
        ToUpward = 180,
        ToUpRight = 240,
        ToDownRight = 300,
        ToDownward = 0,
        ToDownLeft = 60,
        ToUpLeft = 120,
    }


    public partial class BattleViewmodel
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        private IEnumerable<CharacterModel> AllOfCharacterModels =>
            BattleMonsterModels.Concat (_characterViewmodel.BattleCharacterModels).ToList ();

        private IEnumerable<BattleCharacterElement> allOfBattleCharacterElements =>
            _playerCharacterElements.Concat (_aiCharacterElements);

        private readonly Dictionary<int, List<LandModel>> _allLineModels = new Dictionary<int, List<LandModel>> ();

        private readonly int[] RowCount =
        {
            7, 8, 7, 8, 7, 8, 7
        };

        #endregion


        #region Methods

        private void InitializeLines ()
        {
            for (var c = 0; c < RowCount.Length; c++)
            {
                _allLineModels.Add (c, new List<LandModel> ());

                for (var r = 0; r < RowCount[c]; r++)
                {
                    _allLineModels[c].Add (new LandModel ());
                }
            }
        }


        public CharacterModel GetCharacterModelByPosition (CharacterSideType sideType, PositionModel positionModel,
            SkillTarget skillTarget)
        {
            var allModel = GetCharacterModels (sideType, skillTarget);
            var foundCharacter = allModel.First (x => x.PositionModel.Equals (positionModel));

            return foundCharacter;
        }


        public List<CharacterModel> GetCharacterModels (CharacterSideType sideType, SkillTarget skillTarget)
        {
            return sideType == CharacterSideType.Player && skillTarget == SkillTarget.Enemy ||
                   sideType == CharacterSideType.AI && skillTarget == SkillTarget.Ally
                ? BattleMonsterModels
                : _characterViewmodel.BattleCharacterModels;
        }


        public List<CharacterModel> GetOtherSideCharacters (CharacterSideType sideType)
        {
            return sideType == CharacterSideType.Player
                ? BattleMonsterModels
                : _characterViewmodel.BattleCharacterModels;
        }


        public List<CharacterModel> GetSameSideCharacters (CharacterSideType sideType)
        {
            return sideType == CharacterSideType.Player
                ? _characterViewmodel.BattleCharacterModels
                : BattleMonsterModels;
        }


        private List<BattleCharacterElement> GetOtherSideCharacterElements (CharacterSideType sideType)
        {
            return sideType == CharacterSideType.Player ? AiCharacterElements : PlayerCharacterElements;
        }


        private List<BattleCharacterElement> GetSameSideCharacterElements (CharacterSideType sideType)
        {
            return sideType == CharacterSideType.Player ? PlayerCharacterElements : AiCharacterElements;
        }


        /// <summary>
        /// 옆에 있는 캐릭터를 모두 리턴.
        /// </summary>
        public List<CharacterModel> GetCharacterElementsAtNearby (CharacterSideType sideType,
            PositionModel positionModel, SkillTarget skillTarget, bool containSelf = false)
        {
            var nearPositions = GetAroundPositionModel (positionModel).ToList ();
            var foundedCharacters = skillTarget == SkillTarget.Ally
                ? GetSameSideCharacters (sideType)
                : GetOtherSideCharacters (sideType);

            if (containSelf)
                nearPositions.Add (positionModel);

            return foundedCharacters.Where (characterModel =>
            {
                return nearPositions.Any (position => position.Equals (characterModel.PositionModel));
            }).ToList ();
        }


        public BattleState CheckCharacterBattleState (CharacterSideType sideType, PositionModel positionModel,
            out PositionModel targetPosition)
        {
            var otherSideEnemyElements = GetOtherSideCharacterElements (sideType);

            // 적이 모두 사망하였다면.
            if (otherSideEnemyElements.All (x => x.BattleState == BattleState.Death))
            {
                targetPosition = PositionModel.Empty;
                return BattleState.Idle;
            }

            // 행동이 가능한지 체크.
            var isCanBehave = CanBehavior (sideType, positionModel, out var nearestPosition);
            if (isCanBehave)
            {
                targetPosition = nearestPosition;
                return BattleState.Behave;
            }

            // 가장 가까운 이동 위치를 찾음.
            var nearestCharacter = FindClosestMonster (sideType, positionModel);
            var isMovable = FindMovePosition (positionModel, nearestCharacter.PositionModel,
                out var movePosition);

            if (isMovable)
            {
                targetPosition = movePosition;
                return BattleState.Moving;
            }

            targetPosition = positionModel;
            return BattleState.Idle;
        }


        /// <summary>
        /// 근접 공격이 가능함.
        /// </summary>
        private bool CanBehavior (CharacterSideType sideType, PositionModel positionModel,
            out PositionModel besidePosition)
        {
            var nearPositions = GetAroundPositionModel (positionModel);
            var characters = GetOtherSideCharacters (sideType);
            var foundedCharacter = characters.Where (characterModel =>
            {
                return nearPositions.Any (position =>
                    position.Equals (characterModel.PositionModel) ||
                    position.Equals (characterModel.PredicatedPositionModel));
            }).ToList ();

            if (foundedCharacter.Any ())
            {
                var firstCharacter = foundedCharacter.First ();
                besidePosition = firstCharacter.PositionModel;
                return true;
            }

            besidePosition = PositionModel.Empty;
            return false;
        }


        /// <summary>
        /// 가장 가까운 상대 캐릭터를 찾음.
        /// </summary>
        public CharacterModel FindClosestMonster (CharacterSideType sideType, PositionModel positionModel)
        {
            var otherSideCharacters = GetOtherSideCharacters (sideType);
            var foundedModel = otherSideCharacters
                .Where (x => !x.IsExcuted)
                .MinSources (x => Distance (positionModel, x.PositionModel))
                .First (x => IsArrivable (x.PositionModel));
            return foundedModel;

            // 도달가능 여부.
            bool IsArrivable (PositionModel position)
            {
                var aroundPosition = GetAroundPositionModel (position);
                return aroundPosition.Intersect (AllOfCharacterModels.Select (x => x.PositionModel)).Any ();
            }
        }


        private class FindModel
        {
            public PositionModel StartPosition;

            public List<PositionModel> AroundPositionModels = new List<PositionModel> ();

            public FindModel (PositionModel startPosition)
            {
                StartPosition = startPosition;
                AroundPositionModels.Add (startPosition);
            }

            public void AddRangeAroundPositions (IEnumerable<PositionModel> positionModels)
            {
                AroundPositionModels.AddRange (positionModels);
            }
        }

        /// <summary>
        /// 목표까지 가장 빠르게 이동 가능한 주변 위치를 반환함 A star.
        /// </summary>
        private bool FindMovePosition (PositionModel myPosition, PositionModel targetPosition,
            out PositionModel positionModel)
        {
            var firstPositions = GetAroundPositionModel (myPosition).Where (IsMovablePosition).ToList ();
            var allOfCheckedPositions = new List<PositionModel> {myPosition};
            allOfCheckedPositions.AddRange (firstPositions);

            if (firstPositions.Any (position => position.Equals (targetPosition)))
            {
                positionModel = firstPositions.First (x => x.Equals (targetPosition));
                return true;
            }

            var checkPosition = PositionModel.Empty;
            var findModels = firstPositions.Select ((x, i) => new FindModel (x)).ToList ();
            CheckArounds ();

            positionModel = checkPosition;
            return !checkPosition.Equals (PositionModel.Empty);

            void CheckArounds ()
            {
                while (true)
                {
                    foreach (var findModel in findModels)
                    {
                        Debug.Log (myPosition);
                        
                        if (allOfCheckedPositions.Count >= RowCount.Sum())
                        {
                            Debug.Log ("Return");
                            return;
                        }

                        var aroundPositions = findModel
                            .AroundPositionModels.SelectMany (GetAroundPositionModel)
                            .ToList ();
                        
                        findModel.AroundPositionModels.Clear ();

                        if (CheckAround (aroundPositions))
                        {
                            checkPosition = findModel.StartPosition;
                            return;
                        }

                        allOfCheckedPositions.AddRange (aroundPositions);
                        allOfCheckedPositions = allOfCheckedPositions.Distinct ().ToList ();
                        findModel.AddRangeAroundPositions (aroundPositions);
                    }
                }
            }

            bool CheckAround (IEnumerable<PositionModel> checkPositions)
            {
                return checkPositions.Any (position => position.Equals (targetPosition));
            }
        }


        /// <summary>
        /// 해당 enum 타입 방향에 있는 바로 옆 퍼즐의 키 값을 리턴.
        /// </summary>
        private PositionModel GetPositionByDirectionType (PositionModel positionModel,
            CheckDirectionTypes checkDirectionTypes)
        {
            return GetKeyByAngle (positionModel, (float) checkDirectionTypes);
        }


        /// <summary>
        /// 해당 방향에 있는 바로 옆 퍼즐의 키 값을 리턴. 
        /// </summary>
        private PositionModel GetKeyByAngle (PositionModel positionModel, float angle)
        {
            if (!_allLineModels.ContainsKey (positionModel.Column))
            {
                return PositionModel.Empty;
            }

            // To upper direction.
            if (Enumerable.Range (150, 60).Contains ((int) angle))
            {
                return new PositionModel (positionModel.Column, positionModel.Row + 1);
            }

            // To upper right direction.
            if (Enumerable.Range (210, 60).Contains ((int) angle))
            {
                return GetPuzzlePosition (false);
            }

            // To lower right direction.
            if (Enumerable.Range (270, 60).Contains ((int) angle))
            {
                return GetPuzzlePosition (false, false);
            }

            // To upper left direction.
            if (Enumerable.Range (90, 60).Contains ((int) angle))
            {
                return GetPuzzlePosition ();
            }

            // To lower left direction.
            if (Enumerable.Range (30, 60).Contains ((int) angle))
            {
                return GetPuzzlePosition (toUpper: false);
            }

            // To lower direction.
            return new PositionModel (positionModel.Column, positionModel.Row - 1);

            PositionModel GetPuzzlePosition (bool toLeft = true, bool toUpper = true)
            {
                var checkColumn = positionModel.Column + (toLeft ? -1 : 1);

                if (!_allLineModels.ContainsKey (checkColumn))
                    return PositionModel.Empty;

                var isLargeColumn = _allLineModels[positionModel.Column].Count > _allLineModels[checkColumn].Count;
                var posCoeff = isLargeColumn && toUpper ? 0 : isLargeColumn ? -1 : toUpper ? 1 : 0;

                return new PositionModel (checkColumn, positionModel.Row + posCoeff);
            }
        }


        /// <summary>
        /// 해당 위치에서 6방향의 위치를 리턴.
        /// </summary>
        private IEnumerable<PositionModel> GetAroundPositionModel (PositionModel positionModel)
        {
            return new[]
            {
                GetPositionByDirectionType (positionModel, CheckDirectionTypes.ToUpward),
                GetPositionByDirectionType (positionModel, CheckDirectionTypes.ToDownward),
                GetPositionByDirectionType (positionModel, CheckDirectionTypes.ToUpLeft),
                GetPositionByDirectionType (positionModel, CheckDirectionTypes.ToUpRight),
                GetPositionByDirectionType (positionModel, CheckDirectionTypes.ToDownLeft),
                GetPositionByDirectionType (positionModel, CheckDirectionTypes.ToDownRight)
            };
        }


        public bool IsMovablePosition (PositionModel positionModel)
        {
            return _allLineModels.ContainsKey (positionModel.Column) &&
                   _allLineModels[positionModel.Column].ContainIndex (positionModel.Row) &&
                   !positionModel.Equals (PositionModel.Empty) &&
                   AllOfCharacterModels.All (x => !x.PositionModel.Equals (positionModel)) &&
                   AllOfCharacterModels.All (x => !x.PredicatedPositionModel.Equals (positionModel));
        }


        public float Distance (PositionModel checkPosition, PositionModel targetPosition)
        {
            if (!(_allLineModels.ContainsKey (targetPosition.Column) &&
                  _allLineModels[targetPosition.Column].ContainIndex (targetPosition.Row)))
            {
                return float.MaxValue;
            }

            var coeffValue = Mathf.Min (_allLineModels[checkPosition.Column].Count % 2, 0.5f);
            return Math.Abs (checkPosition.Row - targetPosition.Row) +
                Math.Abs (checkPosition.Column - targetPosition.Column) - coeffValue;
        }


        public void CompleteMovement (CharacterModel characterModel, PositionModel positionModel)
        {
            characterModel.PositionModel.Set (positionModel.Column, positionModel.Row);
            characterModel.RemovePredicatePosition ();
        }

        #endregion


        #region EventMethods

        #endregion
    }
}