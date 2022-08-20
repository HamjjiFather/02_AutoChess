using System.Collections.Generic;
using KKSFramework.Repository;

namespace AutoChess
{
    public class CharacterDAO : IDAO
    {
        
    }
    
    public class CharacterRepository : IRepository<CharacterDAO>
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override
        
        public void Initialize()
        {
        }

        public List<CharacterDAO> ReadAll()
        {
            throw new System.NotImplementedException();
        }

        public CharacterDAO Read(int index)
        {
            throw new System.NotImplementedException();
        }

        public void Create(CharacterDAO entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(CharacterDAO entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(CharacterDAO entity)
        {
            throw new System.NotImplementedException();
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}