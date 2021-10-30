using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
* This Class handles the Bird Target Game Object and checks for clicks on the Game Object
*/
public class BirdTarget : MonoBehaviour
{
  // GameController Script
  private GameController gameControllerScript; 

  // Variables
  private string bird;
  private bool waitingOnSecondClick = true;
  private long clickDelay = 200;
  private long firstClickTime;
  private long currentTime;
  private int clickCount = 0;
  private bool coroutineRunning = false;

  /* 
  * Start is called before the first frame update 
  */
  void Start()
  {
    gameControllerScript = GameObject.Find("QuizManager").GetComponent<GameController>();
  }

  /*
  * Gets triggered when the Game Object is clicked and sets the current bird object to the clicked Game Object.
  */
  private void OnMouseDown() {
    if (!gameControllerScript.isClicked && waitingOnSecondClick) {
      firstClickTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
      clickCount += 1;
      bird = gameObject.name;
      gameControllerScript.currentBird = bird;
      gameControllerScript.SetQuestions(bird);
      if (!coroutineRunning) {
        StartCoroutine(ClickRoutine());
        coroutineRunning = true;
      }
    } 
  }

  /*
  * Detects for either a single click or a double click. If a single click is detected, the
  * Bird Bio is opened. If a double click is detected, a Quiz session starts.
  */
  IEnumerator ClickRoutine() {
    while (clickCount != 0 && currentTime < firstClickTime + clickDelay) {
      yield return new WaitForEndOfFrame();
      currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

      // If the current time is greater than the time limit, then check for a single or double click.
      if (currentTime >= firstClickTime + clickDelay) {
      if (clickCount == 1) {
        gameControllerScript.DisplayBirdBio();
      } else {
        gameControllerScript.correctMsgPanel.gameObject.SetActive(false);
        gameControllerScript.QuizElementsOn();
        gameControllerScript.isClicked = true;
      }
      clickCount = 0;
      coroutineRunning = false;
      yield return null;
      }
    } 
  }
}
