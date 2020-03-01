using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
  
    public float moveSpeed =1f;
    public Rigidbody2D rb;
    public Animator animator;

    
    Vector2 movement = Vector2.zero;

    public void Update()
    {
        //input
        if (SceneManager.GetActiveScene().name == "Game")
         {
            movement.x =Input.GetAxisRaw("Horizontal");
            movement.y=Input.GetAxisRaw("Vertical");
         if (movement!= Vector2.zero)
            {
            animator.SetFloat("Horizontal",movement.x);
            animator.SetFloat("Vertical",movement.y);
            }
            animator.SetFloat("Speed",movement.sqrMagnitude);
         }
            
        if (SceneManager.GetActiveScene().name == "Battle")
         {
            if (Input.GetAxis("Fire1")>0) animator.SetBool("IsAttack",true);
            else animator.SetBool("IsAttack",false);
         }
    }

    void FixedUpdate()
    {
        //movement
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        
    }

   


}
