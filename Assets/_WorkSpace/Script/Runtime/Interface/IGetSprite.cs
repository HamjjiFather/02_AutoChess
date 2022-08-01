using KKSFramework.ResourcesLoad;
using ResourcesLoad;
using UnityEngine;

namespace AutoChess
{
    public interface IGetSprite
    {
        public Sprite GetSprite(string spriteName) =>
            ResourcesLoadHelper.GetResources<Sprite>(ResourceRoleType._Image, ResourcesType.Monster, spriteName);
    }
}