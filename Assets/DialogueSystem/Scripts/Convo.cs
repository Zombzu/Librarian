using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Convo : MonoBehaviour
{
    public Node mStartNode;
    public Node mCurrentNode;
    public Node mNode;
    public string mOtherName;

    void Start()
    {
        mNode = mStartNode;
    }
}
