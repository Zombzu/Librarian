using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[ExecuteInEditMode]
public class Connector : ScriptableObject
{

    public Node mFrom;
    public Node mTo;

    public void SetFrom(Node from)
    {
        mFrom = from;
    }

    public Node GetFrom()
    {
        return mFrom;
    }

    public Node GetTo()
    {
        return mTo;
    }

    public void CreateNode()
    {
        if(mTo == null)
        {
            mTo = ScriptableObject.CreateInstance<Node>();
        }
    }

}
