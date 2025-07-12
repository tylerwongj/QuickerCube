using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MarioWorld
{
    [System.Serializable]
    public class MaterialColorFixerEditor : MonoBehaviour
    {
        [Header("Click to Fix Colors in Edit Mode")]
        [Space]
        [Button("Fix All Material Colors")]
        public bool fixColorsButton;
        
        public void FixAllMaterialColors()
        {
            FixColors();
        }
        
        void FixColors()
        {
            // Find and fix all materials
            Material[] allMaterials = Resources.FindObjectsOfTypeAll<Material>();
            
            foreach (Material mat in allMaterials)
            {
                if (mat.name == "BrownGround")
                {
                    SetMaterialColor(mat, new Color(0.4f, 0.2f, 0.1f, 1f));
                    Debug.Log("Fixed Brown Ground color");
                }
                else if (mat.name == "BrickMaterial")
                {
                    SetMaterialColor(mat, new Color(0.8f, 0.4f, 0.2f, 1f));
                    Debug.Log("Fixed Brick Material color");
                }
                else if (mat.name == "GreenPipe")
                {
                    SetMaterialColor(mat, new Color(0.0f, 0.8f, 0.2f, 1f));
                    Debug.Log("Fixed Green Pipe color");
                }
                else if (mat.name == "YellowQuestion")
                {
                    SetMaterialColor(mat, new Color(1.0f, 0.8f, 0.0f, 1f));
                    Debug.Log("Fixed Yellow Question color");
                }
                else if (mat.name == "MarioRed")
                {
                    SetMaterialColor(mat, new Color(0.8f, 0.1f, 0.1f, 1f));
                    Debug.Log("Fixed Mario Red color");
                }
                else if (mat.name == "GoldCoin")
                {
                    SetMaterialColor(mat, new Color(1.0f, 0.8f, 0.0f, 1f));
                    if (mat.HasProperty("_Metallic"))
                        mat.SetFloat("_Metallic", 0.8f);
                    if (mat.HasProperty("_Smoothness"))
                        mat.SetFloat("_Smoothness", 0.9f);
                    Debug.Log("Fixed Gold Coin color");
                }
            }
            
            #if UNITY_EDITOR
            // Mark materials as dirty so Unity saves the changes
            foreach (Material mat in allMaterials)
            {
                if (mat.name.Contains("Brown") || mat.name.Contains("Brick") || 
                    mat.name.Contains("Green") || mat.name.Contains("Yellow") || 
                    mat.name.Contains("Mario") || mat.name.Contains("Gold"))
                {
                    EditorUtility.SetDirty(mat);
                }
            }
            AssetDatabase.SaveAssets();
            #endif
        }
        
        void SetMaterialColor(Material material, Color color)
        {
            if (material.HasProperty("_BaseColor"))
                material.SetColor("_BaseColor", color);
            if (material.HasProperty("_Color"))
                material.SetColor("_Color", color);
            if (material.HasProperty("_MainColor"))
                material.SetColor("_MainColor", color);
            if (material.HasProperty("_Albedo"))
                material.SetColor("_Albedo", color);
        }
    }
    
    #if UNITY_EDITOR
    [CustomEditor(typeof(MaterialColorFixerEditor))]
    public class MaterialColorFixerEditorInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            MaterialColorFixerEditor fixer = (MaterialColorFixerEditor)target;
            
            GUILayout.Space(10);
            if (GUILayout.Button("Fix All Material Colors NOW", GUILayout.Height(30)))
            {
                fixer.FixAllMaterialColors();
            }
        }
    }
    #endif
}

// Custom Button Attribute
public class ButtonAttribute : PropertyAttribute
{
    public string buttonText;
    public ButtonAttribute(string text)
    {
        buttonText = text;
    }
}