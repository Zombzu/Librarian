using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[ExecuteInEditMode]
public class Node : ScriptableObject
{
    //TODO:: set up the goto Node function

    public Connector mDecision1;
    public Connector mDecision2;
    public Connector mDecision3;
    public Connector mDecision4;

    public Connector mLastConnection;

    public bool mActiveTree = false;

    public string mOutput;

    public string mInput1; 
    public string mInput2;
    public string mInput3;
    public string mInput4;

    public string mLastInput = "";
    public Node mLastNode;

    //public NodeAction mAction;

    //public NodeAction GetAction()
    //{
    //    return mAction;
    //}

    //public void SetAction(NodeAction newAction)
    //{
    //    mAction = newAction;
    //}

    //public void RunAction()
    //{ 
    //    if(mAction != null)
    //    {
    //        mAction.Execute();
    //    }
    //}

    public void SetLastConnection(Connector mLast)
    {
        mLastConnection = mLast;
    }

    public void SetOutput(string output)
    {
        mOutput = output;
    }

    public void SetInput1(string input)
    {
        mInput1 = input;
    }

    public void SetInput2(string input)
    {
        mInput2 = input;
    }

    public void SetInput3(string input)
    {
        mInput3 = input;
    }

    public void SetInput4(string input)
    {
        mInput4 = input;
    }

    public void SetAllInputs(string input1, string input2, string input3, string input4)
    {
        Debug.Log("HERE2");
        mInput1 = input1;
        mInput2 = input2;
        mInput3 = input3;
        mInput4 = input4;
    }

    public void SetLastInput(string inp)
    {
        mLastInput = inp;
    }

    public void SetLastNode(Node node)
    {
        mLastNode = node;
    }

    public Node GetLastNode()
    {
        return mLastNode;
    }

    public string GetLastInput()
    {
        return mLastInput;
    }

    public string GetOutput()
    {
        return mOutput;
    }

    public string GetInput1()
    {
        return mInput1;
    }

    public string GetInput2()
    {
        return mInput2;
    }

    public string GetInput3()
    {
        return mInput3;
    }

    public string GetInput4()
    {
        return mInput4;
    }

    public bool IsActiveInTree()
    {
        return mActiveTree;
    }

    public Node GetDecision1()
    {
        if (mDecision1 == null) { return null; }
        return mDecision1.GetTo();
    }

    public Node GetDecision2()
    {
        if (mDecision2 == null) { return null; }
        return mDecision2.GetTo();
    }

    public Node GetDecision3()
    {
        if (mDecision3 == null) { return null; }
        return mDecision3.GetTo();
    }

    public Node GetDecision4()
    {
        if (mDecision4 == null) { return null; }
        return mDecision4.GetTo();
    }

    public void CreateConnections()
    {
        if (mDecision1 == null)
        {
            mDecision1 = ScriptableObject.CreateInstance<Connector>();
        }
        if (mDecision2 == null)
        {
            mDecision2 = ScriptableObject.CreateInstance<Connector>();
        }
        if (mDecision3 == null)
        {
            mDecision3 = ScriptableObject.CreateInstance<Connector>();
        }
        if (mDecision4 == null)
        {
            mDecision4 = ScriptableObject.CreateInstance<Connector>();
        }

        mDecision1.CreateNode();
        mDecision2.CreateNode();
        mDecision3.CreateNode();
        mDecision4.CreateNode();

    }
}
