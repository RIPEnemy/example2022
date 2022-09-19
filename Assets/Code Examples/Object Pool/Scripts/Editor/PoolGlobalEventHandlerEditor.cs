using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using System.Reflection;
using Entry = PoolGlobalEventHandler.Entry;

[CustomEditor(typeof(PoolGlobalEventHandler))]
public class PoolGlobalEventHandlerEditor : Editor
{
    private static readonly List<string> restrictedMethods = new List<string>()
    {
        "Invoke",
        "InvokeRepeating",
        "CancelInvoke",
        "StopCoroutine",
        "StopAllCoroutines",
        "BroadcastMessage"  
    };

    private SerializedProperty handlers;
    private SerializedProperty e = null;

    private List<ReorderableList> methodsList = new List<ReorderableList>();

    private void OnEnable()
    {
        handlers = serializedObject.FindProperty("_handlers");

        for (int i = 0; i < handlers.arraySize; ++i)
        {
            ReorderableList list = new ReorderableList(serializedObject,
                handlers.GetArrayElementAtIndex(i).FindPropertyRelative("_methods"),
                true, true, true, true);

            list.drawHeaderCallback = (Rect rect) => { EditorGUI.PrefixLabel(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), new GUIContent("Methods"), EditorStyles.boldLabel); };

            methodsList.Add(list);
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawGeneral();

        serializedObject.ApplyModifiedProperties();
    }

    private void AddElement(int index)
    {
        handlers.InsertArrayElementAtIndex(index);

        ReorderableList list = new ReorderableList(serializedObject,
            handlers.GetArrayElementAtIndex(index).FindPropertyRelative("_methods"),
            true, true, true, true);

        list.drawHeaderCallback = (Rect rect) => { EditorGUI.PrefixLabel(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), new GUIContent("Methods"), EditorStyles.boldLabel); };

        methodsList.Add(list);
    }

    private void RemoveElement(int index)
    {
        handlers.DeleteArrayElementAtIndex(index);

        methodsList.RemoveAt(index);

        for (int i = 0; i < handlers.arraySize; ++i)
        {
            ReorderableList list = new ReorderableList(
                serializedObject,
                handlers.GetArrayElementAtIndex(i).FindPropertyRelative("_methods"),
                false,
                true,
                true,
                true
            );

            list.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.PrefixLabel(
                    new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                    new GUIContent("Methods"),
                    EditorStyles.boldLabel);
            };

            methodsList[i] = list;
        }
    }

    private void DrawGeneral()
    {
        EditorGUILayout.Space(5);

        EditorGUILayout.BeginVertical();

        EditorGUILayout.LabelField("Event Handler", EditorStyles.largeLabel);

        Rect br = GUILayoutUtility.GetLastRect();

        if (GUI.Button(new Rect(br.width - 50f, br.y, 25f, 25f), EditorGUIUtility.IconContent("d_Toolbar Plus"), EditorStyles.miniButtonLeft))
        {
            AddElement(handlers.arraySize);

            return;
        }

        if (GUI.Button(new Rect(br.width - 25f, br.y, 25f, 25f), EditorGUIUtility.IconContent("d_Toolbar Minus"), EditorStyles.miniButtonRight))
        {
            int elementIndex = handlers.arraySize - 1;

            if (!string.IsNullOrEmpty(GUI.GetNameOfFocusedControl()))
                elementIndex = GetElementNumber(GUI.GetNameOfFocusedControl());

            elementIndex = Mathf.Clamp(elementIndex, 0, handlers.arraySize - 1);

            RemoveElement(elementIndex);

            return;
        }

        EditorGUILayout.Space(5);
        
        if (handlers.arraySize == 0)
            AddElement(0);
        
        for (int i = 0; i < handlers.arraySize; ++i)
        {
            e = handlers.GetArrayElementAtIndex(i).FindPropertyRelative("_event");

            string name = (e.objectReferenceValue == null ? "Event_" + i : e.objectReferenceValue.name);

            GUI.SetNextControlName("Field_" + i);
            EditorGUILayout.PropertyField(e);
            Rect rect = GUILayoutUtility.GetLastRect();

            EditorGUILayout.Space(2f);

            if (GetElementNumber(GUI.GetNameOfFocusedControl()) == i)
                EditorGUI.DrawRect(new Rect(rect.x - 2f, rect.y - 2f, rect.width + 4f, rect.height + 4f), new Color(0f, 0f, 0f, 0.25f));
            else
                EditorGUI.DrawRect(new Rect(rect.x - 2f, rect.y - 2f, rect.width + 4f, rect.height + 4f), new Color(0f, 0f, 0f, 0.1f));

            if (e.objectReferenceValue != null)
            {
                EditorGUILayout.BeginVertical(EditorStyles.centeredGreyMiniLabel);

                methodsList[i].drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    SerializedProperty prop = methodsList[i].serializedProperty.GetArrayElementAtIndex(index);
                    int methodIndex = prop.FindPropertyRelative("methodIndex").intValue;

                    List <Entry> list = GetMethods((serializedObject.targetObject as MonoBehaviour).gameObject);

                    string[] names = GetNames(list);

                    GUI.SetNextControlName("Element(" + index + ")_" + i);
                    methodIndex = EditorGUI.Popup(
                        new Rect(rect.x, rect.y + 2, rect.width, EditorGUIUtility.singleLineHeight),
                        methodIndex,
                        names
                    );

                    prop.FindPropertyRelative("methodIndex").intValue = methodIndex;

                    string methodFullName = names[methodIndex];
                    string methodName = methodFullName.Contains('/')
                        ? methodFullName.Split('/')[1]
                        : methodFullName;

                    Entry entry = list.Find(x => x.method == methodName);

                    if (entry == null)
                        return;

                    prop.FindPropertyRelative("method").stringValue = entry.method;
                    prop.FindPropertyRelative("target").objectReferenceValue = entry.target;
                };

                GUI.SetNextControlName("List_" + i);
                methodsList[i].DoLayoutList();

                EditorGUILayout.EndVertical();

                Rect boxRect = GUILayoutUtility.GetLastRect();
                
                if (GetElementNumber(GUI.GetNameOfFocusedControl()) == i)
                    EditorGUI.DrawRect(new Rect(boxRect.x - 2f, boxRect.y - 4f, boxRect.width + 4f, boxRect.height + 6f), new Color(1f, 1f, 1f, 0.1f));
                else
                    EditorGUI.DrawRect(new Rect(boxRect.x - 2f, boxRect.y - 4f, boxRect.width + 4f, boxRect.height + 6f), new Color(0.6f, 0.6f, 0.6f, 0.1f));

                EditorGUILayout.Space(4f);
            }
        }

        EditorGUILayout.EndVertical();
    }

    private int GetElementNumber(string name)
    {
        name = name.Substring(name.LastIndexOf('_') + 1);

        if (name == "")
            return -1;

        return int.Parse(name);
    }

    private List<Entry> GetMethods(GameObject target)
    {
        Component[] comps = target.GetComponents(typeof(MonoBehaviour));
        List<Entry> list = new List<Entry>();

        for (int i = 0; i < comps.Length; i++)
        {
            MonoBehaviour mb = comps[i] as MonoBehaviour;
            
            if (mb == null)
                continue;

            MethodInfo[] methods = mb.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public);

            for (int b = 0; b < methods.Length; ++b)
            {
                MethodInfo mi = methods[b];

                if (mi.ReturnType == typeof(void))
                {
                    string n = mi.Name;
                    
                    if (restrictedMethods.Contains(n))
                        continue;
                    
                    if (n.StartsWith("GetComponent") || n.StartsWith("SendMessage") || n.StartsWith("set_"))
                        continue;

                    if (mi.GetParameters().Length > 2)
                        continue;

                    Entry ent = new Entry();
                    ent.target = mb;
                    ent.method = mi.Name;
                    list.Add(ent);
                }

            }
        }
        return list;
    }

    private string[] GetNames(List<Entry> list)
    {
        string[] names = new string[list.Count + 1];
        names[0] = "<Method>";

        for (int i = 0; i < list.Count;)
        {
            Entry ent = list[i];
            string del = GetFuncName(ent.target, ent.method);

            names[++i] = del;
        }

        return names;
    }

    static public string GetFuncName(object obj, string method)
    {
        if (obj == null)
            return "<null>";

        string type = obj.GetType().ToString();

        int period = type.LastIndexOf('/');

        if (period > 0)
            type = type.Substring(period + 1);

        return string.IsNullOrEmpty(method) ? type : $"{type}/{method}";
    }
}