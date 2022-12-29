using JetBrains.Annotations;
using Zenject;

namespace AutoChess.Service
{
    [UsedImplicitly]
    public class EquipmentManager : ManagerBase, IUniqueIndexProvider
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

        public int GetUniqueIndex => _equipmentRepository.UpdateUniqueIndex;

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}