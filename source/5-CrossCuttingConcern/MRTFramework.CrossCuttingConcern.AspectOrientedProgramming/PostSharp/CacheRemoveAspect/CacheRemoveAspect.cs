using System;
using System.Reflection;
using MRTFramework.CrossCuttingConcern.Caching.Abstract;
using MRTFramework.CrossCuttingConcern.Utils.Extensions;
using PostSharp.Aspects;

namespace MRTFramework.CrossCuttingConcern.AspectOrientedProgramming.PostSharp.CacheRemoveAspect
{
    [Serializable]
    public class CacheRemoveAspect : OnMethodBoundaryAspect
    {
        private readonly string _pattern;
        private readonly Type _cacheType;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(Type cacheType)
        {
            _cacheType = cacheType;

        }

        public CacheRemoveAspect(Type cacheType, string pattern)
        {
            _pattern = pattern;
            _cacheType = cacheType;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            if (typeof(ICacheManager).IsAssignableFrom(_cacheType) == false)
            {
                throw new Exception("Wrong Cache Manager");
            }

            _cacheManager = (ICacheManager)Activator.CreateInstance(_cacheType);

            base.RuntimeInitialize(method);
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            _cacheManager.RemoveByPattern(string.IsNullOrEmpty(_pattern)
                ? $"{args.Method.ReflectedType?.Name}.*"
                : _pattern.WhiteSpaceRemove());
        }
    }
}
