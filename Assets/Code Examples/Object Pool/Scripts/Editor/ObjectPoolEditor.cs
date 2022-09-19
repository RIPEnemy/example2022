using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using AbonyInt.EventSystem;

[CustomEditor(typeof(ObjectPool))]
public class ObjectPoolEditor : Editor
{
    // Text hints
    private readonly string AUTO_ENABLE_HINT = "'True' to automatically enable object on obtain;\n"
        + "'False' to manually control this process (for example, to change object position before enable).";

    private readonly string PRE_CREATION_HINT = "Pre-Creation is disabled by default. We do not recommend use pre-creation, "
        + "but it can be useful when you need to create a lot of objects before start any heavy gameplay "
        + "logic (objects will be created in coroutine started in 'Awake' pool's method).";

    // Private references and variables
    private SerializedProperty autoEnable;
    private SerializedProperty prefabs;
    private SerializedProperty folders;
    private SerializedProperty loadType;

    private SerializedProperty onObtain;
    private SerializedProperty onRelease;
    private SerializedProperty parentTypeProperty;
    private SerializedProperty parent;
    private SerializedProperty usePreCreation;
    private SerializedProperty preCreationCount;

    private ReorderableList prefabsList = null;
    private ReorderableList foldersList = null;

    private PoolLoadTypes poolLoadType = PoolLoadTypes.Prefabs;
    private ParentTypes parentType = ParentTypes.Pool;

    private int sectionNumber = 0;

    private void OnEnable()
    {
        autoEnable = serializedObject.FindProperty("_autoEnable");
        prefabs = serializedObject.FindProperty("_prefabs");
        folders = serializedObject.FindProperty("_folderPaths");
        loadType = serializedObject.FindProperty("_loadType");

        onObtain = serializedObject.FindProperty("_onObtain");
        onRelease = serializedObject.FindProperty("_onRelease");
        parentTypeProperty = serializedObject.FindProperty("_parentType");
        parent = serializedObject.FindProperty("_parent");
        usePreCreation = serializedObject.FindProperty("_usePreCreation");
        preCreationCount = serializedObject.FindProperty("_preCreationCount");

        prefabsList = new ReorderableList(serializedObject, prefabs, true, true, true, true);
        prefabsList.drawElementCallback = PrefabssDrawElementCallback;
        prefabsList.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.PrefixLabel(
                new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                new GUIContent("Prefabs"),
                EditorStyles.boldLabel
            );
        };

        foldersList = new ReorderableList(serializedObject, folders, true, true, true, true);
        foldersList.drawElementCallback = FoldersDrawElementCallback;
        foldersList.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.PrefixLabel(
                new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                new GUIContent("Folders"),
                EditorStyles.boldLabel
            );
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUIStyle sectionStyle = new GUIStyle(EditorStyles.miniButton);

        sectionNumber = GUI.SelectionGrid(
            new Rect(20, 20, EditorGUIUtility.currentViewWidth - 40, 20),
            sectionNumber,
            new string[] { "General", "Advanced" },
            2,
            sectionStyle
        );

        EditorGUILayout.Space(40);

        if (sectionNumber == 0)
            DrawGeneral();
        else
            DrawAdvanced();

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawGeneral()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            GUILayout.Label("Auto-Enable", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(autoEnable);

            EditorGUILayout.BeginVertical(EditorStyles.objectFieldThumb);

                GUILayout.Label(AUTO_ENABLE_HINT, EditorStyles.wordWrappedLabel);

            EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(5);
        
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            GUILayout.Label("Settings", EditorStyles.boldLabel);

            poolLoadType = (PoolLoadTypes)EditorGUILayout.EnumPopup("Type", poolLoadType);
            loadType.enumValueIndex = (int)poolLoadType;

            EditorGUILayout.BeginVertical(EditorStyles.objectFieldThumb);

                GUILayout.Label(
                    GetLoadTypeDesc(poolLoadType),
                    EditorStyles.wordWrappedLabel
                );

            EditorGUILayout.EndVertical();

            if (poolLoadType == PoolLoadTypes.Prefabs)
            {
                prefabsList.DoLayoutList();
                folders.ClearArray();
            }
            else
            {
                foldersList.DoLayoutList();
                prefabs.ClearArray();
            }

        EditorGUILayout.EndVertical();
    }

    private void DrawAdvanced()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            GUILayout.Label("Global Events", EditorStyles.boldLabel);

            EditorGUILayout.ObjectField(onObtain, typeof(GlobalEvent));
            EditorGUILayout.ObjectField(onRelease, typeof(GlobalEvent));

            EditorGUILayout.Space(1);

        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(5);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            GUILayout.Label("Parent", EditorStyles.boldLabel);

            parentType = (ParentTypes)EditorGUILayout.EnumPopup("Type", parentType);
            parentTypeProperty.enumValueIndex = (int)parentType;

            if (parentType == ParentTypes.Custom)
                EditorGUILayout.ObjectField(parent, typeof(Transform));

            EditorGUILayout.BeginVertical(EditorStyles.objectFieldThumb);

                GUILayout.Label(GetParentTypeHint(parentType), EditorStyles.wordWrappedLabel);

            EditorGUILayout.EndVertical();
        
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(5);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            GUILayout.Label("Pre-Creation", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(usePreCreation);

            if (usePreCreation.boolValue)
                EditorGUILayout.PropertyField(preCreationCount, new GUIContent("Count"));

            EditorGUILayout.BeginVertical(EditorStyles.objectFieldThumb);

            GUILayout.Label(PRE_CREATION_HINT, EditorStyles.wordWrappedLabel);

            EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical();
    }

    private void FoldersDrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty prop = foldersList.serializedProperty.GetArrayElementAtIndex(index);
        
        EditorGUI.PropertyField(
            new Rect(rect.x, rect.y + 2, rect.width, EditorGUIUtility.singleLineHeight),
            prop,
            GUIContent.none
        );
    }

    private void PrefabssDrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty prop = prefabsList.serializedProperty.GetArrayElementAtIndex(index);
        
        EditorGUI.PropertyField(
            new Rect(rect.x, rect.y + 2, rect.width, EditorGUIUtility.singleLineHeight),
            prop,
            GUIContent.none
        );
    }

    private string GetLoadTypeDesc(PoolLoadTypes loadType)
    {
        switch (loadType)
        {
            case PoolLoadTypes.Folders:
                return "Paths to folders with prefabs (has to be located in project 'Resource' folder). Useful in case with a large amount of different prefabs.";
            default:
                return "References to prefabs for use in pool. Useful in most cases with few prefabs.";
        }
    }

    private string GetParentTypeHint(ParentTypes parentType)
    {

        switch (parentType)
        {
            case ParentTypes.None:
                return "All new objects will be created without parent. Useful for a large number of moving objects (for example, projectiles or enemies).";
            case ParentTypes.Pool:
                return "Default option. All new objects will be created as this game object children. Useful for static objects (for example, floor or level parts).";
            case ParentTypes.Custom:
                return "All new objects will be created as children of custom game object (selected below or passed as argument to 'Obtain' method). Useful in case which you need to manually control parent for obtained objects.";
            default:
                return "Default option. All new objects will be created as this game object children. Useful for static objects (for example, floor or level parts).";
        }
    }
}