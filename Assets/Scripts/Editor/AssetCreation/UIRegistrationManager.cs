using UI.Popups;
using UI.Screens;
using UnityEditor;
using UnityEngine;

namespace Tools.AssetCreation
{
    public class UIRegistrationManager
    {
        public static void RegisterScreen(GameUIContainer gameUIContainer, GameObject prefabAsset)
        {
            SerializedObject serializedUiServiceViewContainer = new SerializedObject(gameUIContainer);

            BaseScreen uiScreenComponent = prefabAsset.GetComponent<BaseScreen>();

            SerializedProperty screensPrefabProperty = serializedUiServiceViewContainer.FindProperty("_screens");

            screensPrefabProperty.arraySize++;
            screensPrefabProperty.GetArrayElementAtIndex(screensPrefabProperty.arraySize - 1).objectReferenceValue = uiScreenComponent;

            serializedUiServiceViewContainer.ApplyModifiedProperties();
        }
        
        public static void RegisterPopup(GameUIContainer gameUIContainer, GameObject prefabAsset)
        {
            SerializedObject serializedUiServiceViewContainer = new SerializedObject(gameUIContainer);

            BasePopup basePopupComponent = prefabAsset.GetComponent<BasePopup>();

            SerializedProperty popupsPrefabProperty = serializedUiServiceViewContainer.FindProperty("_popups");

            popupsPrefabProperty.arraySize++;
            popupsPrefabProperty.GetArrayElementAtIndex(popupsPrefabProperty.arraySize - 1).objectReferenceValue = basePopupComponent;

            serializedUiServiceViewContainer.ApplyModifiedProperties();
        }
    }
}