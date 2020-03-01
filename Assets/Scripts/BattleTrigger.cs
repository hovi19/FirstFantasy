using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
       if(other.name=="Player")
     {
        SceneManager.LoadScene("Battle");
     } 
    }
}
