using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class TagCreator : EditorWindow
{
    [MenuItem("Tools/Auto Create Tags")]
    public static void CreateRequiredTags()
    {
        string[] tagsToCreate = { "Player", "Ground", "Collectible" };
        
        // Get current tags
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");
        
        List<string> existingTags = new List<string>();
        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            existingTags.Add(tagsProp.GetArrayElementAtIndex(i).stringValue);
        }
        
        bool tagsAdded = false;
        
        foreach (string tag in tagsToCreate)
        {
            if (!existingTags.Contains(tag))
            {
                tagsProp.InsertArrayElementAtIndex(tagsProp.arraySize);
                tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 1).stringValue = tag;
                tagsAdded = true;
                Debug.Log($"Added tag: {tag}");
            }
            else
            {
                Debug.Log($"Tag already exists: {tag}");
            }
        }
        
        if (tagsAdded)
        {
            tagManager.ApplyModifiedProperties();
            Debug.Log("✅ All required tags created successfully!");
            EditorUtility.DisplayDialog("Success!", "All required tags have been created:\n• Player\n• Ground\n• Collectible\n\nYour game should work now!", "OK");
        }
        else
        {
            Debug.Log("All tags already exist!");
            EditorUtility.DisplayDialog("Info", "All required tags already exist. No changes needed!", "OK");
        }
    }
    
    [MenuItem("Tools/Check Required Tags")]
    public static void CheckRequiredTags()
    {
        string[] requiredTags = { "Player", "Ground", "Collectible" };
        
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");
        
        List<string> existingTags = new List<string>();
        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            existingTags.Add(tagsProp.GetArrayElementAtIndex(i).stringValue);
        }
        
        List<string> missingTags = new List<string>();
        foreach (string tag in requiredTags)
        {
            if (!existingTags.Contains(tag))
            {
                missingTags.Add(tag);
            }
        }
        
        if (missingTags.Count > 0)
        {
            string message = "Missing tags:\n" + string.Join("\n", missingTags.ToArray());
            Debug.LogWarning(message);
            
            if (EditorUtility.DisplayDialog("Missing Tags", message + "\n\nWould you like to create them automatically?", "Yes", "No"))
            {
                CreateRequiredTags();
            }
        }
        else
        {
            Debug.Log("✅ All required tags exist!");
            EditorUtility.DisplayDialog("Success!", "All required tags exist:\n• Player\n• Ground\n• Collectible", "OK");
        }
    }
    
    [MenuItem("Tools/Game Setup/Create Tags and Setup Scene")]
    public static void QuickGameSetup()
    {
        // First create tags
        CreateRequiredTags();
        
        // Then create the game setup GameObject if it doesn't exist
        GameObject setupObject = GameObject.Find("GameSetup");
        if (setupObject == null)
        {
            setupObject = new GameObject("GameSetup");
            setupObject.AddComponent<AutoSceneSetup>();
            Debug.Log("Created GameSetup object with AutoSceneSetup script");
        }
        
        // Try to find and assign InputSystem_Actions
        var inputActions = AssetDatabase.LoadAssetAtPath<UnityEngine.InputSystem.InputActionAsset>("Assets/InputSystem_Actions.inputactions");
        if (inputActions != null)
        {
            AutoSceneSetup autoSetup = setupObject.GetComponent<AutoSceneSetup>();
            if (autoSetup != null)
            {
                autoSetup.inputActions = inputActions;
                Debug.Log("Assigned InputSystem_Actions to AutoSceneSetup");
            }
        }
        
        EditorUtility.DisplayDialog("Quick Setup Complete!", 
            "✅ Tags created\n✅ GameSetup object created\n✅ AutoSceneSetup script added\n\nPress Play to start your game!", 
            "OK");
    }
}