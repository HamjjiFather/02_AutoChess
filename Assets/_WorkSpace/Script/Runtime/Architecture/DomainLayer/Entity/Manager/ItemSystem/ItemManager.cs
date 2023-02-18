using Zenject;

namespace AutoChess
{
    public class ItemManager : ManagerBase
    {
        [Inject]
        private CurrencyManager _currencyManager;

        [Inject]
        private EquipmentManager _equipmentManager;
    }
}