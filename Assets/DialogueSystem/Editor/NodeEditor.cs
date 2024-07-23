using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Convo))]
[CanEditMultipleObjects]
[ExecuteInEditMode]
public class NodeEditor : Editor
{
    private Convo _Convo;
    private Node _Node;
    private Node mCurrentNode;

    string mOutput = "";
    string mInput1 = "";
    string mInput2 = "";
    string mInput3 = "";
    string mInput4 = "";

    string mLastInp = "";

    bool mFirst = false;
    bool canReset = false;
    bool last = true;
    string mName = "";
    GUIStyle colourChange = new GUIStyle();

    bool mGotoSpecificNode1;
    int mTempNodeLocation1;

    bool mGotoSpecificNode2;
    int mTempNodeLocation2;

    bool mGotoSpecificNode3;
    int mTempNodeLocation3;

    bool mGotoSpecificNode4;
    int mTempNodeLocation4;

    //string objectName;

    void OnEnable()
    {
        _Convo = (Convo)target;
        Load();
    }

    void OnDisable()
    {
        Save();
    }


    //void CreateAsset()
    //{


    //    GameObject mObj = PrefabUtility.CreatePrefab("Assets/DialogueSystem/Prefabs/SavedScripts/" + objectName + ".prefab", new GameObject());

    //    EditorUtility.SetDirty(mObj);

    //    mObj.AddComponent<Convo>();

    //    Convo newObjConvo = mObj.GetComponent<Convo>();


    //    newObjConvo.mStartNode = _Convo.mStartNode;
    //    newObjConvo.mOtherName = _Convo.mOtherName;

    //    AssetDatabase.SaveAssets();

    //}

    public override void OnInspectorGUI()
    {

        EditorGUIUtility.fieldWidth = 0;

        if (_Convo.mStartNode == null)
        {
            Save();
            _Convo.mStartNode = ScriptableObject.CreateInstance<Node>();
            mCurrentNode = _Convo.mStartNode;
            UpdateVariables();
        }

        if (mCurrentNode == null)
        {
            Save();
            mCurrentNode = _Convo.mStartNode;
            UpdateVariables();
        }

        EditorGUILayout.BeginVertical();
        EditorGUILayout.HelpBox("To set this convo as the main one, select the ConvoManager, and drag this object onto CurrentConvo", MessageType.Info);

        //GUILayout.Space(10);

        //EditorGUILayout.BeginHorizontal();

        //objectName = EditorGUILayout.TextField("Prefab name:", objectName);

        //if (GUILayout.Button("Create Prefab"))
        //{
        //    CreateAsset();
        //    GUI.FocusControl(null);
        //}


        //EditorGUILayout.EndHorizontal();

        //GUILayout.Space(10);


        EditorGUILayout.BeginHorizontal();
        canReset = EditorGUILayout.BeginToggleGroup("Reset Safeguard", canReset);
        if (GUILayout.Button("Reset"))
        {
            _Convo.mStartNode = ScriptableObject.CreateInstance<Node>();
            mCurrentNode = _Convo.mStartNode;
            UpdateVariables();
            GUI.FocusControl(null);
        }
        EditorGUILayout.EndToggleGroup();
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(20);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Go Back"))
        {
            if (mCurrentNode.GetLastNode() != null)
            {
                Save();
                mCurrentNode = mCurrentNode.GetLastNode();
                UpdateVariables();
                GUI.FocusControl(null);
            }
        }
        if (GUILayout.Button("Back to Start"))
        {
            Save();
            Load();
        }
        EditorGUILayout.EndHorizontal();


        colourChange.normal.textColor = Color.blue;
        GUILayout.Space(20);
        EditorGUILayout.LabelField("Last Input: " + mLastInp, colourChange);
        if (mCurrentNode == _Convo.mStartNode)
        {
            mFirst = true;
        }
        else
        {
            mFirst = false;
        }
        EditorGUILayout.LabelField("First Sentence: " + mFirst, colourChange);
        GUILayout.Space(20);

        mName = EditorGUILayout.TextField("Other Persons Name: ", mName);

        if (mCurrentNode == null)
        {
            last = false;
        }
        else
        {
            last = mCurrentNode.IsActiveInTree();
        }

        last = EditorGUILayout.BeginToggleGroup("active in tree", last);

        if (mCurrentNode != null)
        {
            mCurrentNode.mActiveTree = last;
        }

        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();

        //EditorGUILayout.PrefixLabel("Action: ");

        //mActionNode = (NodeAction)EditorGUILayout.ObjectField(mActionNode, typeof(NodeAction), true);

        EditorGUILayout.EndHorizontal();

        GUILayout.Space(20);


        mOutput = EditorGUILayout.TextField("Output: ", mOutput);

        GUILayout.Space(10);


        //choice 1

        EditorGUILayout.BeginHorizontal();


        mInput1 = EditorGUILayout.TextField("Input choice 1: ", mInput1);

        if (GUILayout.Button("Expand Outcome"))
        {
            Save();
            string tempLast = mCurrentNode.GetInput1();
            Node Temp = mCurrentNode;
            mCurrentNode.CreateConnections();
            mCurrentNode = mCurrentNode.GetDecision1();
            mCurrentNode.SetLastInput(tempLast);
            mCurrentNode.SetLastNode(Temp);
            //mCurrentNode.RunAction();
            UpdateVariables();
            GUI.FocusControl(null);
        }

        EditorGUILayout.EndHorizontal();

        //choice 2
        EditorGUILayout.Separator();
        EditorGUILayout.BeginHorizontal();

        mInput2 = EditorGUILayout.TextField("Input choice 2: ", mInput2);

        if (GUILayout.Button("Expand Outcome"))
        {
            Save();
            string tempLast = mCurrentNode.GetInput2();
            Node Temp = mCurrentNode;
            mCurrentNode.CreateConnections();
            mCurrentNode = mCurrentNode.GetDecision2();
            mCurrentNode.SetLastInput(tempLast);
            mCurrentNode.SetLastNode(Temp);
            //mCurrentNode.RunAction();
            UpdateVariables();
            GUI.FocusControl(null);
        }

        EditorGUILayout.EndHorizontal();

        //choice 3
        EditorGUILayout.Separator();
        EditorGUILayout.BeginHorizontal();

        mInput3 = EditorGUILayout.TextField("Input choice 3: ", mInput3);

        if (GUILayout.Button("Expand Outcome"))
        {
            Save();
            string tempLast = mCurrentNode.GetInput3();
            Node Temp = mCurrentNode;
            mCurrentNode.CreateConnections();
            mCurrentNode = mCurrentNode.GetDecision3();
            mCurrentNode.SetLastInput(tempLast);
            mCurrentNode.SetLastNode(Temp);
            //mCurrentNode.RunAction();
            UpdateVariables();
            GUI.FocusControl(null);
        }

        EditorGUILayout.EndHorizontal();

        //choice 4
        EditorGUILayout.Separator();
        EditorGUILayout.BeginHorizontal();

        mInput4 = EditorGUILayout.TextField("Input choice 4: ", mInput4);

        if (GUILayout.Button("Expand Outcome"))
        {
            Save();
            string tempLast = mCurrentNode.GetInput4();
            Node Temp = mCurrentNode;
            mCurrentNode.CreateConnections();
            mCurrentNode = mCurrentNode.GetDecision4();
            mCurrentNode.SetLastInput(tempLast);
            mCurrentNode.SetLastNode(Temp);
            //mCurrentNode.RunAction();
            UpdateVariables();
            GUI.FocusControl(null);

        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndToggleGroup();
        EditorGUILayout.EndVertical();

        Save();

    }

    void Load()
    {
        AssetDatabase.SaveAssets();
        if (mCurrentNode != null)
        {
            mCurrentNode = _Convo.mStartNode;
            UpdateVariables();
            GUI.FocusControl(null);
        }
    }

    void Save()
    {
        AssetDatabase.SaveAssets();
        if (mCurrentNode != null)
        {

            mCurrentNode.SetOutput(mOutput);

            mCurrentNode.SetInput1(mInput1);
            mCurrentNode.SetInput2(mInput2);
            mCurrentNode.SetInput3(mInput3);
            mCurrentNode.SetInput4(mInput4);

            //mCurrentNode.SetAction(mActionNode);

            _Convo.mOtherName = mName;
        }
    }

    void UpdateVariables()
    {
        mOutput = mCurrentNode.GetOutput();

        mInput1 = mCurrentNode.GetInput1();
        mInput2 = mCurrentNode.GetInput2();
        mInput3 = mCurrentNode.GetInput3();
        mInput4 = mCurrentNode.GetInput4();

        mLastInp = mCurrentNode.GetLastInput();

        mName = _Convo.mOtherName;

        //mActionNode = mCurrentNode.GetAction();
    }
}
