using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState{START, PLAYERTURN, ENEMYTURN , WON, LOST}
public class BattleSystem : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public Text dialogueText;
    Unit playerUnit;
    Unit enemyUnit;
    public BattleHUD battleHUD;

    public Animator animator;



   public BattleState state;



    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        
    }  

    IEnumerator SetupBattle()
    {
       
       
        GameObject playerGO = Instantiate(playerPrefab,playerBattleStation);
        playerUnit=playerGO.GetComponent<Unit>();   
        GameObject enemyGo = Instantiate(enemyPrefab,enemyBattleStation);
        enemyUnit = enemyGo.GetComponent<Unit>();
        dialogueText.text = "Készülj a harcra!";

        battleHUD.SetHUD(playerUnit);

        yield return new WaitForSeconds(3f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
    IEnumerator playerAttack()
    {
    
        //damage the enemy 
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        dialogueText.text = "Sikeres támadás";
        yield return new WaitForSeconds(2f);
         //check if enemy is dead
          // Change state based on what happend
        if (isDead)
        {
            state= BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

       
        // Change state based on what happend


    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = "Az ellenség támad";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        battleHUD.SetHP(playerUnit.currentHP);
        if(isDead)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "Megnyerted a csatát";

            yield return new WaitForSeconds(4f);

            SceneManager.LoadScene("Game");

            


        }
        else if( state == BattleState.LOST)
        {
            dialogueText.text= "Vesztettél";

            yield return new WaitForSeconds(4f);

            SceneManager.LoadScene("GameOver");
        }

    }

    private void PlayerTurn()
    {
        dialogueText.text="Válassz: ";
    }

       IEnumerator playerHeal()
    {
        playerUnit.Heal(20);
       
        dialogueText.text = "Gyógyítottad magad";
        battleHUD.SetHP(playerUnit.currentHP);
        yield return new WaitForSeconds(2f);
        state= BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton()
    {
        if(state != BattleState.PLAYERTURN)
        return;



        StartCoroutine(playerAttack());
    }
    public void OnHealButton()
    {
        if(state != BattleState.PLAYERTURN)
        return;

        StartCoroutine(playerHeal());
    }
 
    
}
