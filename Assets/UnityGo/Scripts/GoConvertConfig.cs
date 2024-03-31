using UnityEngine;

namespace Danqzq.UnityGo
{
    public class GoConvertConfig : ScriptableObject
    {
        public string @namespace;
        public string outputFile = Application.dataPath + "/UnityGo/Generated/Models/Models.cs";
    }
}