using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public PlayerDash dashScript;
    public DashRecharge rechargeScript;
    public EnemySwitch switchScript;

    public GameObject player;
    public GameObject dashRecharge;
         
    public Rigidbody playerBody;
    public Rigidbody enemyBody;

    [SerializeField] float nudgeForce;
    public float knockMultiplier = 2f;    
    public float reactionRadius = 2f;

    public Vector3 forceOrigin;
    public Vector3 spawnOffset;

    void Start() 
    {
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
        switchScript = GetComponent<EnemySwitch>();
        playerBody = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        enemyBody = GetComponent<Rigidbody>();        
        spawnOffset = new Vector3 (0,2,0);    
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (dashScript.isDashing == false)
            {
                RunReaction();
            }
            else if (dashScript.isDashing == true)
            {
                DashReaction();
            }
        }
    }

    void RunReaction()
    {
        forceOrigin = transform.position;

        switch (switchScript.enemyType)
        {
        case 0: 
            playerBody.AddExplosionForce(nudgeForce, forceOrigin, reactionRadius, 0, ForceMode.Impulse);
            break;
        case 1:
            playerBody.AddExplosionForce(nudgeForce * knockMultiplier, forceOrigin, reactionRadius, 0, ForceMode.Impulse); //ADD PLAYER INPUT DISABLE?
            break;
        case 2:
            //dashScript.enemyDirection = player.transform.position - transform.position;
            playerBody.AddExplosionForce(nudgeForce * knockMultiplier, forceOrigin, reactionRadius, 0, ForceMode.Impulse);
            dashScript.dashCharges--;
            dashScript.maxDash--;
    
            if(dashScript.dashCharges >= 0)
            {
                Instantiate(dashRecharge, player.transform.position, Quaternion.identity);
                //rechargeScript = GameObject.FindWithTag("Dash Recharge").GetComponent<DashRecharge>();
                //rechargeScript.rechargeDirection = (player.transform.position - transform.position).normalized;
            }    
            break;
        }
    }

    void DashReaction()
    {
        forceOrigin = player.transform.position;

        switch (switchScript.enemyType)
        {
        case 0: 
            Destroy(gameObject);
            break;
            case 1:
                switchScript.enemyType = 0;
                //enemyBody.AddExplosionForce(nudgeForce * knockMultiplier, forceOrigin, reactionRadius, 0, ForceMode.Impulse);
                break;
            case 2: 
            Debug.Log("Nudge armour");
            enemyBody.AddExplosionForce(nudgeForce, forceOrigin, reactionRadius, 0, ForceMode.Impulse);//SWAP TO LERP? RENABLING MOVEMENT MIGHT HELP
            break;
        }
    }
}