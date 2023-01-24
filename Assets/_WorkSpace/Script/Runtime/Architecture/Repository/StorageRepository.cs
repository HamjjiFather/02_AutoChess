using KKSFramework.Repository;

namespace AutoChess
{
    public struct StorageDao : IDAO
    {
        
    }
    
    public class StorageRepository : IRepository
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override

        // public List<StorageDao> ReadAll(IEnumerable<string> indexes)
        // {
        //     throw new System.NotImplementedException();
        // }

        public StorageDao Read(string index)
        {
            // throw new System.NotImplementedException();
            return default;
        }

        public void Create(StorageDao entity)
        {
            // throw new System.NotImplementedException();
        }

        public void Update(StorageDao entity)
        {
            // throw new System.NotImplementedException();
        }

        public void Delete(StorageDao entity)
        {
            // throw new System.NotImplementedException();
        }

        public void Initialize()
        {
            // throw new System.NotImplementedException();
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}