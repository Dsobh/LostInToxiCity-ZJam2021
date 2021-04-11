using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D playerRB;
    private Animator _animator;
    private AudioSource _audiosource;
    [SerializeField]
    private float speed = 10f;
    private float horizontalMovement;
    private float verticalMovement;
    private int delaySteps = 20;
    private int steps = 0;

    // Start is called before the first frame update
    void Start()
    {
       playerRB = GetComponent<Rigidbody2D>();
       _animator = GetComponent<Animator>();
       _audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        if(horizontalMovement !=0 || verticalMovement != 0){
            _animator.SetBool("IsWalking", true);
            if (!SFXManager.sfxInstance.Audio.isPlaying) {
                if(steps == delaySteps)
                {
                    SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.pasitos);
                    steps = 0;
                }
                steps++;
            }
            
            _animator.SetFloat("Horizontal", horizontalMovement);
            _animator.SetFloat("Vertical", verticalMovement);
            _animator.SetFloat("LastH", horizontalMovement);
            _animator.SetFloat("LastV", verticalMovement);
        }else{
            _animator.SetBool("IsWalking", false);
            _audiosource.Stop();
        }

    }

    void FixedUpdate()
    {
        playerRB.velocity = new Vector2(horizontalMovement, verticalMovement) * speed;
    }
}
