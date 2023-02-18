using System.Linq;

namespace AutoChess
{
    public readonly struct EquipmentDto
    {
        public EquipmentDto(EquipmentEntity entity)
        {
            UniqueIndex = entity.UniqueIndex;
            EquipmentTableDataIndex = entity.EquipmentTableData.Id;
            SlotIndexes = entity.AttachedStatusSlots.Select(x => x.SlotIndex).ToArray();
            // AmountIndexes = entity.;
        }

        public readonly int UniqueIndex;
        
        public readonly int EquipmentTableDataIndex;

        public readonly int[] SlotIndexes;
    }
}