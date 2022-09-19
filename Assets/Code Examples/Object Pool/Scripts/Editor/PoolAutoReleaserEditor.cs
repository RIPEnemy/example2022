using UnityEditor;
using UnityEngine;
using UnityEditorInternal;

[CustomEditor(typeof(PoolAutoReleaser))]
public class PoolAutoReleaserEditor : Editor
{
    private SerializedProperty trigger;
    private SerializedProperty collisionTags;

    private ReorderableList tags = null;

    private PoolAutoReleaser.Trigger triggerType = PoolAutoReleaser.Trigger.OnDisable;
    
    private void OnEnable()
    {
        trigger = serializedObject.FindProperty("_trigger");
        collisionTags = serializedObject.FindProperty("_collisionTags");

        tags = new ReorderableList(serializedObject, collisionTags, true, true, true, true);
        tags.drawElementCallback = DrawElemtCallback;
        tags.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.PrefixLabel(
                new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                new GUIContent("Tags"),
                EditorStyles.boldLabel
            );
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawGeneral();

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawGeneral()
    {
        EditorGUILayout.Space(1);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            GUILayout.Label("Settings", EditorStyles.boldLabel);

            triggerType = (PoolAutoReleaser.Trigger)EditorGUILayout.EnumPopup("Type", triggerType);

            EditorGUILayout.Space(2);

            if (trigger.enumValueIndex != (int)triggerType)
            {
                trigger.enumValueIndex = (int)triggerType;
                collisionTags.ClearArray();
            }
            
            if (triggerType != PoolAutoReleaser.Trigger.Custom && triggerType != PoolAutoReleaser.Trigger.OnDisable)
                tags.DoLayoutList();

            EditorGUILayout.BeginVertical(EditorStyles.objectFieldThumb);

                GUILayout.Label(GetHint(triggerType), EditorStyles.wordWrappedLabel);

            EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical();
    }

    private void DrawElemtCallback(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty prop = tags.serializedProperty.GetArrayElementAtIndex(index);

        EditorGUI.PropertyField(
            new Rect(rect.x, rect.y + 2, rect.width, EditorGUIUtility.singleLineHeight),
            prop,
            GUIContent.none
        );
    }

    private string GetHint(PoolAutoReleaser.Trigger type)
    {
        switch (type)
        {
            case PoolAutoReleaser.Trigger.OnDisable:
                return "GameObject will be automatically released to object pool on disable.";
            case PoolAutoReleaser.Trigger.OnTriggerEnter:
                return "GameObject will be automatically released and returned to object pool 'OnTriggerEnter' event. If you don't want to check the object's tag with collision, leave the array above empty.";
            case PoolAutoReleaser.Trigger.OnTriggerExit:
                return "GameObject will be automatically released and returned to object pool 'OnTriggerExit' event. If you don't want to check the object's tag with collision, leave the array above empty.";
            case PoolAutoReleaser.Trigger.OnCollisionEnter:
                return "GameObject will be automatically released and returned to object pool 'OnCollisionEnter' event. If you don't want to check the object's tag with collision, leave the array above empty.";
            case PoolAutoReleaser.Trigger.OnCollisionExit:
                return "GameObject will be automatically released and returned to object pool 'OnСollisionExit' event. If you don't want to check the object's tag with collision, leave the array above empty.";
            case PoolAutoReleaser.Trigger.Custom:
                return "Invoke 'Release' method in the script manually to release GameObject and return it to object pool. For example, to return unit to object pool after finish dead animation.";
            default:
                return "";
        }
    }
}
