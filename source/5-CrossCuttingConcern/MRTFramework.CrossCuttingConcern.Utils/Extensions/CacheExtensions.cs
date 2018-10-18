using PostSharp.Aspects;
using static System.String;

namespace MRTFramework.CrossCuttingConcern.Utils.Extensions
{
    public static class CacheExtensions
    {
        public static bool IsCustomKey(string customKey)
        {
            return IsNullOrEmpty(customKey);
        }

        public static string MethodNameWithClassName(this MethodInterceptionArgs args)
        {
            return $"{args.Method.ReflectedType?.Name}.{args.Method.Name}";
        }
    }
}
