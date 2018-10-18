using Newtonsoft.Json;

namespace MRTFramework.CrossCuttingConcern.Utils.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson(this object entity)
        {
            return JsonConvert.SerializeObject(entity);
        }
    }
}
