using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPCState : MonoBehaviour
{
    protected NPCBase npcBase;
    protected NavMeshAgent agent;
    protected Animator animator;

    public virtual void Initialize(NPCBase npcBase, NavMeshAgent navMeshAgent, Animator anim)
    {
        this.npcBase = npcBase;
        this.agent = navMeshAgent;
        this.animator = anim;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}
