using System.Collections.Generic;
using Xamarin.Forms;

namespace ScanApp.Renderers
{
    public static class DiccionarioExtension
    {


        public static void AddOrUpdateFile(this Dictionary<string, List<string>> targetDictionary, string key, string entry)
        {
            if (!targetDictionary.ContainsKey(key))
                targetDictionary.Add(key, new List<string>());

            targetDictionary[key].Add(entry);
        }

        public static void AddOrUpdateImageSource(this Dictionary<object, List<object>> targetDictionary, string key, string entry)
        {
            if (!targetDictionary.ContainsKey(key))
                targetDictionary.Add(key, new List<object>());

            targetDictionary[key].Add(entry);
        }

        internal static void AddOrUpdateFile(string path)
        {
            DiccionarioExtension.AddOrUpdateFile(path);
        }

        internal static void AddOrUpdateImageSource(ImageSource source)
        {
            DiccionarioExtension.AddOrUpdateImageSource(source);
        }
    }

}
