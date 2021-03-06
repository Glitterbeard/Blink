using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator playerAnim;

    public GameObject playerObj;

    public ParticleSystem ps1;
    public ParticleSystem ps2;
    public ParticleSystem ps3;

    // Start is called before the first frame update
    void Start()
    {
        var emission = ps1.emission;
        var emission2 = ps2.emission;
        var emission3 = ps3.emission;
        ps1.Play();
        ps2.Play();
        ps3.Play();
        emission.enabled = false;
        emission2.enabled = false;
        emission3.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        var emission = ps1.emission;
        var emission2 = ps2.emission;
        var emission3 = ps3.emission;

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && playerObj.gameObject.GetComponent<PlayerDash>().isPlanning == false)
        {
            playerAnim.SetBool("isRunning", true);
        }
        else
        {
            playerAnim.SetBool("isRunning", false);

        }

        if (Input.GetKey(KeyCode.Space))
        {
            playerAnim.SetBool("isDashing", true);
        }
        if (playerObj.gameObject.GetComponent<PlayerDash>().isDashing == true)
        {
            emission.enabled = true;
            emission2.enabled = true;
            emission3.enabled = true;
        }
        else
        {
            playerAnim.SetBool("isDashing", false);
            emission.enabled = false;
            emission2.enabled = false;
            emission3.enabled = false;
        }
        
        if (Input.GetKey(KeyCode.O) || playerObj.gameObject.GetComponent<PlayerDash>().isPlanning == true)
        {
            playerAnim.SetInteger("isPlanning", Random.Range(1,3));
            //playerAnim.SetBool("isPlanningB", true);
        }
        else
        {
            //playerAnim.SetBool("isPlanningB", false);
            playerAnim.SetInteger("isPlanning", 0);
        }
    }
}
