using System;
using System.Linq;
using System.Reflection;
using System.Text;
using MRTFramework.CrossCuttingConcern.Caching.Abstract;
using MRTFramework.CrossCuttingConcern.Utils.Extensions;
using Newtonsoft.Json;
using PostSharp.Aspects;
using static System.String;

namespace MRTFramework.CrossCuttingConcern.AspectOrientedProgramming.PostSharp.CacheAspect
{
    [Serializable]
    public class CacheAspect : MethodInterceptionAspect
    {
        private readonly int _cacheByMinute;
        private readonly string _customKey;
        private readonly Type _cacheType;
        private ICacheManager _cacheManager;

        public CacheAspect(Type cacheType, int cacheByMinute, string customKey = null)
        {
            _cacheType = cacheType;
            _cacheByMinute = cacheByMinute;
            _customKey = customKey;
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

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var keyGenerator = new StringBuilder();
            var referenceTypeKey = new StringBuilder();
            var valueTypeKey = new StringBuilder();

            var methodName = CacheExtensions.IsCustomKey(_customKey) ? args.MethodNameWithClassName() : _customKey.WhiteSpaceRemove();

            keyGenerator.Append(methodName);

            var arguments = args.Arguments.ToList();

            foreach (var dataArgument in arguments)
            {
                if (dataArgument != null)
                {
                    var getTypeInfo = dataArgument.GetType().GetTypeInfo().ToString();
                    var getBaseType = dataArgument.GetType().BaseType?.ToString();

                    if (getTypeInfo != "System.String" && getBaseType != "System.ValueType")
                    {
                        var getObjectName = dataArgument.GetType().Name;

                        referenceTypeKey.AppendFormat($"{getObjectName}RT/");

                        var getPropertiesValue = dataArgument.GetType().GetProperties().Select(x => x.GetValue(dataArgument));

                        foreach (var value in getPropertiesValue)
                        {
                            referenceTypeKey.AppendFormat(value == null ? $"<Null>/" : $"{value}/");
                        }

                    }

                    if (getTypeInfo == "System.String" || getBaseType == "System.ValueType")
                    {
                        valueTypeKey.AppendFormat($"{dataArgument}-");
                    }
                }

                else
                {
                    valueTypeKey.AppendFormat($"<Null>-");
                }

            }

            if (!IsNullOrEmpty(referenceTypeKey.ToString()))
            {
                referenceTypeKey.Remove(referenceTypeKey.Length - 1, 1);
                keyGenerator.Append("&" + referenceTypeKey);
            }

            if (!IsNullOrEmpty(valueTypeKey.ToString()))
            {
                valueTypeKey = valueTypeKey.Remove(valueTypeKey.Length - 1, 1);
                keyGenerator.Append("&VT" + valueTypeKey);
            }

            var clearKeyData = keyGenerator.ToString().WhiteSpaceRemove();

            if (_cacheManager.IsAdd(clearKeyData))
            {
                var cacheValue = _cacheManager.Get<object>(clearKeyData);

                switch (_cacheType.Name)
                {
                    case "MicrosoftMemoryCacheManager":

                        args.ReturnValue = cacheValue;
                        return;

                    case "RedisCacheManager":

                        var methodInfo = args.Method as MethodInfo;
                        if (methodInfo == null) return;
                        var methodReturnType = methodInfo.ReturnType;

                        args.ReturnValue = JsonConvert.DeserializeObject(cacheValue.ToString(), methodReturnType);
                        return;

                    default:
                        return;
                }
            }

            base.OnInvoke(args);

            _cacheManager.Add(clearKeyData, args.ReturnValue, _cacheByMinute);

        }
    }
}