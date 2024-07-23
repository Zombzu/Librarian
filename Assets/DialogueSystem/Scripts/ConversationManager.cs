using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    public static ConversationManager sInstance = null;

    public GameObject mGUIElements;

    public string mPlayerTag;

    [HideInInspector]
    public bool mConversationActive = false;

    [HideInInspector]
    public Convo mCurrentConversation;

    public KeyCode mActivateKey;

    public KeyCode mDeactivateKey;

    void Awake()
    {
        if (sInstance == null)
        {
            sInstance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        ResetConvo();
    }

    void Update()
    {
        if (mConversationActive)
        {
            mGUIElements.SetActive(true);
        }
        else
        {
            mGUIElements.SetActive(false);
        }

        if (Input.GetKeyDown(mDeactivateKey))
        {
            mCurrentConversation = null;
            mConversationActive = false;
        }

    }

    void SetConversation(Convo newConversation)
    {
        if (newConversation != null)
        {
            mCurrentConversation = newConversation;
        }
    }

    public void SelectSentence(int pos)
    {
        if (mConversationActive)
        {

            switch (pos)
            {
                case 0:
                    if (mCurrentConversation.mNode.GetDecision1() == null || !mCurrentConversation.mNode.GetDecision1().IsActiveInTree()) { DeactivateConvo(); return; }
                    mCurrentConversation.mNode = mCurrentConversation.mNode.GetDecision1();
                    break;
                case 1:
                    if (mCurrentConversation.mNode.GetDecision2() == null || !mCurrentConversation.mNode.GetDecision2().IsActiveInTree()) { DeactivateConvo(); return; }
                    mCurrentConversation.mNode = mCurrentConversation.mNode.GetDecision2();
                    break;
                case 2:
                    if (mCurrentConversation.mNode.GetDecision3() == null || !mCurrentConversation.mNode.GetDecision3().IsActiveInTree()) { DeactivateConvo(); return; }
                    mCurrentConversation.mNode = mCurrentConversation.mNode.GetDecision3();
                    break;
                case 3:
                    if (mCurrentConversation.mNode.GetDecision4() == null || !mCurrentConversation.mNode.GetDecision4().IsActiveInTree()) { DeactivateConvo(); return; }
                    mCurrentConversation.mNode = mCurrentConversation.mNode.GetDecision4();
                    break;

                default:
                    break;
            }
        }
    }

    void DeactivateConvo()
    {
        mConversationActive = false;
        mCurrentConversation = null;
    }

    public Convo GetConversation()
    {
        return mCurrentConversation;
    }

    public void ResetConvo()
    {
        if (mCurrentConversation != null)
        {
            mCurrentConversation.mNode = mCurrentConversation.mStartNode;
        }
    }

}
