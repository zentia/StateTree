﻿#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using CinemaDirector;
using UnityEditor;
using UnityEngine;

public class CharactorSelector : EditorWindow
{
    public static CharactorSelector Instance;
    private static int _filterCount;
    private string _name;
    private Vector2 _scrollPos;
    private void OnEnable()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        Instance = null;
    }

    private void OnGUI()
    {

    }

}
public class RoleSelector : EditorWindow
{
    public static RoleSelector Instance;
    private static int _filterCount;
    private string _name;
    private Vector2 _scrollPos;

    private void OnEnable()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        Instance = null;
    }

    private void OnGUI()
    {
    }

}
public class AnimationSelector : EditorWindow
{
    public static AnimationSelector Instance;
    private string _name;
    private Vector2 _scrollPos;

    private void OnEnable()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        Instance = null;
    }

    private void OnGUI()
    {
    }
}

public class EffectSelector : EditorWindow
{
    public static EffectSelector Instance;
    private Vector2 _scrollPos;
    public string Path;
    public bool m_Extension;
    public delegate void Callback(string path);

    private Callback _callback;
    private void OnEnable()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        Instance = null;
    }

    private void ShowItem(string path)
    {
        if (!CheckFilter(path))
            return;
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(path))
        {
            _callback(path);
            Close();
        }
        EditorGUILayout.EndHorizontal();
    }
    private string m_FilterText;
    public bool CheckFilter(string text)
    {
        if (string.IsNullOrEmpty(m_FilterText))
            return true;
        if (m_FilterText.Length > 0)
        {
            if (text.IndexOf(m_FilterText) >= 0)
            {
                return true;
            }
            return false;
        }
        return true;
    }
    private void OnGUI()
    {
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
        EditorGUILayout.BeginVertical();
        m_FilterText = EditorGUILayout.TextField("筛选", m_FilterText, GUILayout.ExpandWidth(false), GUILayout.MaxWidth(200));

        Cutscene.ForeachDir(Path, ShowItem, m_Extension);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
    }

    public static void Show(Callback callback, string path, bool extension = true)
    {
        if (Instance != null)
        {
            Instance.Close();
            Instance = null;
        }

        EffectSelector comp = GetWindow<EffectSelector>("选择资源");
        comp.Path = path;
        comp.m_Extension = extension;
        comp._callback = callback;
        comp.Show();
    }
}

public class GateSelector : EditorWindow
{
    public static GateSelector Instance;
    private Vector2 _scrollPos;
    public delegate void Callback(uint path);

    private Callback _callback;
    private void OnEnable()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        Instance = null;
    }
    private void OnGUI()
    {
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
        EditorGUILayout.BeginVertical();
        
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
    }

    public static void Show(Callback callback)
    {
        if (Instance != null)
        {
            Instance.Close();
            Instance = null;
        }

        GateSelector comp = GetWindow<GateSelector>("门清单");
        comp._callback = callback;
        comp.Show();
    }
}
public class ActorTargetSelector : EditorWindow
{
    public static ActorTargetSelector Instance;
    private Vector2 _scrollPos;
    public delegate void Callback(string path);

    private Callback _callback;
    private void OnEnable()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        Instance = null;
    }

    private void OnGUI()
    {
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
        EditorGUILayout.HelpBox("actor节点不要出现重名，否则只会跟随第一个同名actor", MessageType.Warning);
        EditorGUILayout.BeginVertical();
        Cutscene cut = FindObjectOfType<Cutscene>();
        if (cut)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("none");
            if (GUILayout.Button("选择"))
            {
                _callback(null);
                Close();
            }
            EditorGUILayout.EndHorizontal();
            for (int i = 0; i < cut.transform.childCount; i++)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(cut.transform.GetChild(i).name);
                if (GUILayout.Button("选择"))
                {
                    _callback(cut.transform.GetChild(i).name);
                    Close();
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
    }

    public static void Show(Callback callback)
    {
        if (Instance != null)
        {
            Instance.Close();
            Instance = null;
        }

        ActorTargetSelector comp = GetWindow<ActorTargetSelector>("选择目标资源");
        comp._callback = callback;
        comp.Show();
    }
}

public static class EditorCoroutineRunner
{
    private class EditorCoroutine : IEnumerator
    {
        private Stack<IEnumerator> executionStack;

        public EditorCoroutine(IEnumerator iterator)
        {
            executionStack = new Stack<IEnumerator>();
            executionStack.Push(iterator);
        }

        public bool MoveNext()
        {
            IEnumerator i = executionStack.Peek();

            if (i.MoveNext())
            {
                object result = i.Current;
                if (result != null && result is IEnumerator)
                {
                    executionStack.Push((IEnumerator)result);
                }

                return true;
            }
            else
            {
                if (executionStack.Count > 1)
                {
                    executionStack.Pop();
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            throw new System.NotSupportedException("This Operation Is Not Supported.");
        }

        public object Current
        {
            get { return executionStack.Peek().Current; }
        }

        public bool Find(IEnumerator iterator)
        {
            return executionStack.Contains(iterator);
        }
    }

    private static List<EditorCoroutine> editorCoroutineList;
    private static List<IEnumerator> buffer;

    public static IEnumerator StartEditorCoroutine(IEnumerator iterator)
    {
        if (editorCoroutineList == null)
        {
            // test
            editorCoroutineList = new List<EditorCoroutine>();
        }
        if (buffer == null)
        {
            buffer = new List<IEnumerator>();
        }
        if (editorCoroutineList.Count == 0)
        {
            EditorApplication.update += Update;
        }

        // add iterator to buffer first
        buffer.Add(iterator);

        return iterator;
    }

    private static bool Find(IEnumerator iterator)
    {
        // If this iterator is already added
        // Then ignore it this time
        foreach (EditorCoroutine editorCoroutine in editorCoroutineList)
        {
            if (editorCoroutine.Find(iterator))
            {
                return true;
            }
        }

        return false;
    }

    private static void Update()
    {
        // EditorCoroutine execution may append new iterators to buffer
        // Therefore we should run EditorCoroutine first
        editorCoroutineList.RemoveAll
        (
            coroutine => { return coroutine.MoveNext() == false; }
        );

        // If we have iterators in buffer
        if (buffer.Count > 0)
        {
            foreach (IEnumerator iterator in buffer)
            {
                // If this iterators not exists
                if (!Find(iterator))
                {
                    // Added this as new EditorCoroutine
                    editorCoroutineList.Add(new EditorCoroutine(iterator));
                }
            }

            // Clear buffer
            buffer.Clear();
        }

        // If we have no running EditorCoroutine
        // Stop calling update anymore
        if (editorCoroutineList.Count == 0)
        {
            EditorApplication.update -= Update;
        }
    }
}
#endif