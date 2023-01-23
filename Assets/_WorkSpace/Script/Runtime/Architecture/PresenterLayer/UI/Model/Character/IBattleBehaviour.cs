namespace AutoChess
{
    public interface IBattleBehaviour
    {
        public void PrepareBattle();

        public void StartBattle();

        public void DoBehave();

        public void Death();

        public void EndBattle();
    }
}