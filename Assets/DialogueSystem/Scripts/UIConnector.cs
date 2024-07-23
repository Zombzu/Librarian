using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIConnector : MonoBehaviour
{

    public Text mName;
    [Space(10)]
    public Text mOutput;
    [Space(10)]
    public Text mInput1;
    public Text mInput2;
    public Text mInput3;
    public Text mInput4;


    void Start()
    {

    }

    void Update()
    {
        UpdateConvo();
    }

    void UpdateConvo()
    {
        if (ConversationManager.sInstance.GetConversation() != null)
        {

            Node mCurrNode = ConversationManager.sInstance.GetConversation().mNode;

            mName.text = ConversationManager.sInstance.GetConversation().mOtherName;

            mOutput.text = mCurrNode.GetOutput();

            mInput1.text = mCurrNode.GetInput1();
            mInput2.text = mCurrNode.GetInput2();
            mInput3.text = mCurrNode.GetInput3();
            mInput4.text = mCurrNode.GetInput4();
        }

    }

    public void SelectInput(int input)
    {
        ConversationManager.sInstance.SelectSentence(input);
    }

    public void ResetConversation()
    {
        ConversationManager.sInstance.ResetConvo();
    }

}
