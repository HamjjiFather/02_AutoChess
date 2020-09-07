using System;

namespace KKSFramework.DataBind
{
    public interface IResolveTarget
    {
        
    }
    
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Event)]
    public class ResolverAttribute : Attribute
    {
        public readonly string Key;

        public ResolverAttribute ()
        {
            
        }
        
        public ResolverAttribute (string key)
        {
            Key = key;
        }
    }
}