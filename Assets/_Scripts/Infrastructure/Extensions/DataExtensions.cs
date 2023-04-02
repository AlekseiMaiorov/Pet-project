using UnityEngine;

namespace Infrastructure.Extensions
{
    public static class DataExtensions
    {
        public static T ToDeserialize<T>(this string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public static string ToJson(this object obj)
        {
            return JsonUtility.ToJson(obj);
        }
    }
}