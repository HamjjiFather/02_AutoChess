using JetBrains.Annotations;
using Zenject;

namespace AutoChess
{
    [UsedImplicitly]
    public class EquipmentManager : ManagerBase
    {
        public EquipmentManager()
        {
        }
        
        #region Fields & Property

        [Inject]
        private EquipmentRepository _equipmentRepository;
        
        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}