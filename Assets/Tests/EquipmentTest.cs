using AutoChess;
using Cysharp.Threading.Tasks;
using NUnit.Framework;

public class EquipmentTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void 장비를_뽑아보자()
    {
        // Use the Assert class to test conditions
        TableDataManager.Instance.LoadTableDatas().Forget();
        var equipments = EquipmentGenerator.GenerateEquipmentsForEnemy(EnemyGradeType.Legendary);
    }

    // // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // // `yield return null;` to skip a frame.
    // [UnityTest]
    // public IEnumerator EquipmentTestWithEnumeratorPasses()
    // {
    //     // Use the Assert class to test conditions.
    //     // Use yield to skip a frame.
    //     yield return null;
    // }
}
