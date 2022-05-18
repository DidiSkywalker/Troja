using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class QuizQuestionCreator : EditorWindow
{
    [MenuItem("Window/UI Toolkit/QuizQuestionCreator")]
    public static void ShowExample()
    {
        QuizQuestionCreator wnd = GetWindow<QuizQuestionCreator>();
        wnd.titleContent = new GUIContent("QuizQuestionCreator");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/MiniGames/QuizMinigame/QuizQuestionCreator.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);
    }
}