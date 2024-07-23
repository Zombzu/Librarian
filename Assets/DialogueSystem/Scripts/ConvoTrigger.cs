using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ConvoTrigger : MonoBehaviour
{

    public Convo mConversation;



    void Update()
    {

    }

    void OnTriggerStay(Collider col)
    {
        if(col.tag == ConversationManager.sInstance.mPlayerTag)
        {
            if(Input.GetKeyDown(ConversationManager.sInstance.mActivateKey))
            {
                ConversationManager.sInstance.mCurrentConversation = mConversation;
                ConversationManager.sInstance.ResetConvo();
                ConversationManager.sInstance.mConversationActive = true;
            }
        }
    }


}
