using UnityEngine;
using static RagDollControl;

public class RecoverFall : MonoBehaviour
{
    public float recoverheight;
    public bool alive = false;
    public float height;
    public float lastfall = 0f;
    public RagDollControl RagDollControl;
    public float deathtimer = 0f;

    private void Awake()
    {
        RagDollControl = FindAnyObjectByType<RagDollControl>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        height = transform.position.y;
        deathtimer = RagDollControl.Clock.GameTime - lastfall;

        //Temp Input Method
        if (deathtimer >= RagDollControl.deathtimer && RagDollControl.diedonce == false)
        {
            Debug.Log("10 seconds have passed since the game started!");
            RagDollControl.currentstate = AgentState.Ragdoll;
            RagDollControl.diedonce = true;
            alive = false;
        }
        else if (RagDollControl.diedonce == true && alive == true)
        {
            Debug.Log("Revive Please");
            RagDollControl.currentstate = AgentState.Walking;
            lastfall = Time.time;
            lastfall = RagDollControl.Clock.GameTime;
            RagDollControl.diedonce = false;
        }

        if (height >= recoverheight)
        {
            //RagDollControl.DisableRagdoll();
            RagDollControl.querevive = true;
            alive = true;
        }

    }

    private void FixedUpdate()
    {
        
    }
}
