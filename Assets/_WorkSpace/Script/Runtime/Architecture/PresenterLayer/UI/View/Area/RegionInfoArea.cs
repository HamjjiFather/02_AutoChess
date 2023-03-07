using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Helper;
using KKSFramework.Navigation;
using TMPro;
using UniRx;
using UnityEngine;

namespace AutoChess
{
    public struct RegionInfoUpdateMessage
    {
        public RegionInfoUpdateMessage(int areaIndex)
        {
            AreaIndex = areaIndex;
        }

        public int AreaIndex;
    }

    public class RegionInfoArea : AreaView
    {
        #region Fields & Property

        public TextMeshProUGUI mapNameText;

        private Dictionary<int, string> _regionNameKeyMap = new();

        public float directionDuration = 2f;

        public Coroutine directionCoroutine;

        #endregion


        #region Methods

        #region Override

        private void Awake()
        {
            MessageBroker.Default.Receive<RegionInfoUpdateMessage>().TakeUntilDestroy(this).Subscribe(msg =>
            {
                var areaId = msg.AreaIndex;
                if (!_regionNameKeyMap.ContainsKey(areaId))
                {
                    var tableData = TableDataManager.Instance.RegionDict[areaId];
                    _regionNameKeyMap.Add(areaId, tableData.NameKey);
                }

                mapNameText.text = LocalizeHelper.FromName(_regionNameKeyMap[areaId]);
                directionCoroutine = StartCoroutine(WaitForDirection());
            });
        }

        #endregion


        #region This

        private IEnumerator WaitForDirection()
        {
            yield return Show().ToCoroutine();
            yield return new WaitForSeconds(directionDuration);
            yield return Hide().ToCoroutine();
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}