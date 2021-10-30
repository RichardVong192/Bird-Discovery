using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // For Text Mesh Pro Components
using UnityEngine.UI; // For UI elements

/*
* This Class handles the validation for the Quiz session and controls the visibility of the panels.
*/
public class QuizHandler : MonoBehaviour
{
  // GameController Script
  private GameController gameControllerScript; 

  // Correct Answer
  public string correctAnswer { get; set; }

  // Incorrect Image UI Element
  public Image incorrectImg;

  /* 
  * Start is called before the first frame update 
  */
  void Start()
  {
    // Sets the Quiz Controller Script
    gameControllerScript = GameObject.Find("QuizManager").GetComponent<GameController>();
  }

  /*
  * Checks if the selected answer is correct. If correct, then a Congratulatory message appears.
  * If not, then the selected answer is marked as incorrect. 
  */
  public void CheckAnswer() {
    correctAnswer = gameControllerScript.correctAnswer;
    string answer = gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
    if (correctAnswer == answer) {
      gameControllerScript.QuizElementsOff();
      gameControllerScript.correctMsgPanel.gameObject.SetActive(true);
      gameControllerScript.isClicked = false;
      Invoke("CloseCorrectMessagePanel", 4);
    } else {
      // Disables Incorrect Button
      gameObject.GetComponentInChildren<Button>().interactable = false;
      incorrectImg.gameObject.SetActive(true);
    }
  }

  /*
  * Disables the Correct Message Panel
  */
  private void CloseCorrectMessagePanel() {
    gameControllerScript.correctMsgPanel.gameObject.SetActive(false);
  }

  /*
  * Disables the Bird Bio Panel
  */
  public void CloseBirdBioPanel() {
    gameControllerScript.birdBioPanel.gameObject.SetActive(false);
  }
}
