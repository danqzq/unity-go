using UnityEditor;
using UnityEngine;

namespace Danqzq.UnityGo
{
    public class GoConvertEditor : EditorWindow
    {
        private static string _goFilePath = Application.dataPath + "/Scripts/main.go";
        private static string _outputFilePath = Application.dataPath + "/UnityGo/Generated/Models/Models.cs";
        private static string _namespace = "UnityGo.Models";
        
        private static GoConvertConfig _config;
        
        private const string GO_FILE_PATH_KEY = "GoFilePath";
        private const string AUTHOR_URL = "https://www.danqzq.games";
        private const string VERSION = "0.1.0";

        [MenuItem("UnityGo/Convert Go to C#")]
        public static void ShowWindow()
        {
            LoadConfig();
            
            GetWindow(typeof(GoConvertEditor), false, "Go Convert");
        }
        
        private static void LoadConfig()
        {
            _config = Resources.Load<GoConvertConfig>("GoConvertConfig");
            _goFilePath = EditorPrefs.GetString(GO_FILE_PATH_KEY, _goFilePath);
            _outputFilePath = _config.outputFile;
            _namespace = _config.@namespace;
        }

        private void OnGUI()
        {
            if (_config == null)
            {
                LoadConfig();
            }
            
            GUILayout.Label("Go File Path", EditorStyles.boldLabel);
            var oldGoFilePath = _goFilePath;
            _goFilePath = EditorGUILayout.TextField("Go File Path", _goFilePath);
            if (GUILayout.Button("Select Go File"))
            {
                var newGoFilePath = EditorUtility.OpenFilePanel("Select Go File", "", "go");
                if (!string.IsNullOrEmpty(newGoFilePath)) 
                    _goFilePath = newGoFilePath;
            }
            if (oldGoFilePath != _goFilePath)
            {
                EditorPrefs.SetString(GO_FILE_PATH_KEY, _goFilePath);
            }
            
            GUILayout.Space(15);
            
            GUILayout.Label("Output File Path", EditorStyles.boldLabel);
            _outputFilePath = EditorGUILayout.TextField("Output File Path", _outputFilePath);
            if (GUILayout.Button("Select Output File"))
            {
                var newOutputFilePath = EditorUtility.SaveFilePanel("Select Output File", "", "Models.cs", "cs");
                if (!string.IsNullOrEmpty(newOutputFilePath)) 
                    _outputFilePath = newOutputFilePath;
            }
            if (_config.outputFile != _outputFilePath)
            {
                _config.outputFile = _outputFilePath;
                EditorUtility.SetDirty(_config);
            }
            
            GUILayout.Space(15);
            
            GUILayout.Label("Namespace", EditorStyles.boldLabel);
            _namespace = EditorGUILayout.TextField("Namespace", _namespace);
            if (_config.@namespace != _namespace)
            {
                _config.@namespace = _namespace;
                EditorUtility.SetDirty(_config);
            }
            
            GUILayout.Space(15);

            if (GUILayout.Button("Convert"))
            {
                GoParser.Parse(_goFilePath);
                AssetDatabase.Refresh();
            }
            
            GUILayout.Label($"<color=white>Version: {VERSION}</color>", 
                new GUIStyle{alignment = TextAnchor.LowerRight, richText = true});
            if (GUILayout.Button("<color=#2a9df4>Made by @danqzq</color>",
                    new GUIStyle{alignment = TextAnchor.LowerRight, richText = true}))
                Application.OpenURL(AUTHOR_URL);
        }
    }
}
