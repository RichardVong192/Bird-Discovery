using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using UnityEngine;
using TMPro; // For Text Mesh Pro Components
using UnityEngine.UI; // For UI elements
using static System.Random;

/*
* The Bird class contains all the information about the Bird.
*/
class Bird 
{
  public string birdName { get; set; }
  public string family { get; set; }
  public string nestType { get; set; }
  public string lifeSpan { get; set; }
  public string status { get; set; }
  public string description { get; set; }
  public string population { get; set; }
  public string breeding { get; set; }
  public string food { get; set; }
  public string threat { get; set; }
}

/*
* The Quiz class contains information about the quiz question and possible answers for the question.
*/
class Quiz 
{
  // The Quiz Question
  public string question { get; set; }
  // The Possible Answers for the Question
  public List<string> answers { get; set;}
}

/*
* This Controller class contains all the core functionality for setting up the UI Elements for the Quiz and the
* Bird Bio Panel
*/
public class GameController : MonoBehaviour
{
  // Canvas Object Elements
  public TextMeshProUGUI questionText; 
  public GameObject questionPanel;
  public GameObject correctMsgPanel;
  public GameObject questionNumberPanel;
  public GameObject birdBioPanel;
  public GameObject birdBioNamePanel;
  public GameObject birdBioInfoPanel;
  public List<Button> answerButtons;

  // Randomiser
  public System.Random random = new System.Random();

  // Variables
  public string currentBird { get; set; } 
  public string correctAnswer { get; set; }
  public bool isClicked = false;

  Dictionary<string, Bird> birdsInfo = new Dictionary<string, Bird>()
  {
    {"Albatross", new Bird {birdName="Albatross", family="Diomedeidae", nestType="Pedestal", lifeSpan="42", status="Critical", description = "The Antipodean albatross breed almost exclusively on the Auckland and Antipodes Islands, but are most common in the Tasman Sea and over the Chatham Rise east of New Zealand. Albatrosses are the world's largest seabirds. They spend at least 85% of their lives at sea returning to land (usually remote islands) to breed and raise their young.", population = "About 3200 pairs breed each year on the Auckland Islands and about 3700 on Antipodes Island.", breeding = "Antipodean albatrosses lay a single egg between December and February and take a whole year to hatch the egg and raise the chick. Because of the long incubation and nestling period they can only raise a chick once every two years. The nest is a low pedestal built of soil and vegetation and sometimes lined with grass. Both members of the pair incubate the eggs and care for the young, taking shifts of up to 3 weeks while incubating.", food = "They eat squid, fish and discards from boats.", threat = "On the main Auckland Island pigs may take albatross chicks, but on other breeding islands (except Chatham and Pitt Islands, plus mice on Antipodes Island) there are no exotic predators. At sea their habit of following boats and taking bait from hooks has led to large numbers being killed in long-line fisheries." }},
    {"BlueDuck", new Bird { birdName="Blue Duck", family="Anatidae", nestType="Ground-level", lifeSpan="8", status="Vulnerable", description = "The blue duck or whio is an iconic species of clear fast-flowing rivers, now mostly confined to high altitude segments of rivers in North and South Island mountain regions. It’s one of only a few waterfowl species worldwide that live year-round in this environment. Blue ducks are found no where else in the world, and are rarer than some species of kiwi.", population = "The breeding population is under 3,000.", breeding = "While the female is incubating, the male waits on the riverside in close proximity to the nest. The pale white eggs (each 60-65 g) are laid at 1.5-2 day intervals (e.g. 5 eggs in 8 days, 6 eggs in 10 days). The average clutch is 5 or 6 (range 3 – 8) and the incubation period extends for 33-35 days. Both parents guard the ducklings during their 70-80 days until fledging and the brood is raised entirely within the territory.", food = "Their diet comprised almost exclusively of freshwater invertebrates, of which chironomid larvae and cased caddis fly larvae are the most prominent.", threat = "Nesting females are especially vulnerable to mammalian predators, particularly stoats and possums, while rats and weka have been implicated in nest and egg destructions."}},
    {"Kakapo", new Bird {birdName="Kākāpō", family="Strigopidae", nestType="Ground-level", lifeSpan="60", status="Critical", description = "The kākāpō is a large, nocturnal, flightless, lek-breeding parrot. Even though they cannot fly, they climb well. They are perhaps the longest-lived bird species in the world, estimated to reach 90 years.", population = "A total of 201 kākāpō are alive today.", breeding = "Kākāpō breed in summer and autumn, but only in years of good fruit abundance. On islands in southern New Zealand they breed when the rimu trees fruit, which is once every 2 to 4 years.", food = "Kākāpō are entirely vegetarian. Their diet includes, leaves, buds, flowers, fern fronds, bark, roots, rhizomes, bulbs, fruit and seeds. Diet varies seasonally.", threat = "Adult kākāpō are vulnerable to predation by cats and stoats, and their eggs and chicks can be killed by rats. As they must spend long periods away from the nest feeding, eggs and chicks are particularly vulnerable to predation when the nest is unattended." }},
    {"LittleBluePenguin", new Bird {birdName="Little Blue Penguin", family="Spheniscidae", nestType="Burrow", lifeSpan="6", status="Declining", description = "The little penguin is the smallest species of penguin. Most commonly found around all coasts of New Zealand’s mainland and many of the surrounding islands. Primarily nocturnal on land, they are sometimes found close to human settlements and often nest under and around coastal buildings, keeping the owners awake at night with their noisy vocal displays.", population = " The largest colonies are on Motunau Island (1650 nests), Pohatu Bay, Banks Peninsula (1250 pairs),  and at the Oamaru Blue Penguin colony (>1,000 individuals).", breeding = " They are the only species of penguin capable of producing more than one clutch of eggs per breeding season, but few populations do so. The 1-2 white or lightly mottled brown eggs are laid from July to mid-November, and with rarer second (or even third) clutches beginning as late as December.", food = "Their diet is composed of varying proportions of small shoaling fish, squid and crustacean species.", threat = "Many colonies are in decline due to predation by introduced predators including cats, dogs and ferrets. Little penguins at sea are also at risk of entanglement in set nets." }},
    {"NZ Pigeon", new Bird {birdName="NZ Pigeon", family="Columbidae", nestType="Raised", lifeSpan="20", status="No Threat", description = "This large and distinctively-coloured pigeon is a familiar sight to many New Zealanders. They are frequently featured on works of art, such as paintings and sculptures. The distinctive sound of their wing beats in flight also draws attention. Since the extinction of the moa, the kererū and parea are now the only bird species that are big enough to swallow large fruit, and disperse the seed over long distances. The disappearance of these birds could be a disaster for the regeneration of our native forests.", population = "Kereru are widespread through the country, and are seasonally common at some locations where they gather in moderate-sized feeding flocks (20-50 birds, and rarely over 100).", breeding = "Kereru have been recorded breeding in all months, but most eggs are laid in September-April. The nest is a platform of dead twigs, and a single egg is laid. The chick is brooded constantly until it is about 10 days old and well covered with feathers. From then until fledging at about 35-40 days of age, it is left alone by day, with the occasional brief visit by a parent to feed it.", food = "Foods include buds, leaves, flowers and fruit from a wide variety species, both native and exotic.", threat = "The main threat to kereru is predation by introduced mammalian predators, particularly feral cats, possums, stoats and ship rats, especially when nesting." }},
    {"OrangeFrontedParakeet", new Bird {birdName="Orange Parakeet", family="Psittacidae", nestType="Tree Hole", lifeSpan="12", status="Critical", description = "This budgerigar-sized parakeet is usually quiet and difficult to observe. A loud brief chatter or quieter contact call may give away its presence, but locating the bird can be extremely difficult.", population = "This species is exceptionally difficult to count, largely because of its rarity, and cryptic and quiet nature. There are probably fewer than 100 mature parakeets on the mainland, and perhaps 200-300 on islands following translocations.", breeding = "Orange-fronted parakeets can breed in all months, but their main nesting period is between December and April, and incubation peaks in January. Mean clutch size is approximately 7, but a wide range has been recorded (from 1 to 10). Egg-laying is asynchronous, with an interval of 2 days between eggs. Incubation takes 21-26 days, and the nestling period lasts 35-45 days.", food = "They consume seeds, flowers, buds and invertebrates (e.g. scale insects and caterpillars).", threat = "The main threat to orange-fronted parakeets is from introduced predators, especially ship rats and stoats. Parakeet eggs and nestlings are also vulnerable to predation by possums, and fledglings are an easy target for cats. Habitat modification is also a threat, predominantly by deer and possums." }},
    {"ParadiseDuck", new Bird {birdName="Paradise Shelduck", family="Anatidae", nestType="Burrow", lifeSpan="2", status="No Threat", description = "The paradise shelduck is a colourful, conspicuous and noisy waterfowl that could be mistaken for a small goose. They are New Zealand’s only shelduck.", population = "The paradise shelduck is the second-most abundant waterfowl in New Zealand (after mallard). The total population probably exceeded 6-700,000 birds in 2011.", breeding = "Egg-laying is usually in August and September but young birds and some repeat nesters will lay in October, but rarely later. Clutches of 5-15 are reported but most over 12 will have been contributed to by two females. Incubation is by the female only and takes 30-35 days.", food = "They are generally herbivorous, with a preference for grass, clover, aquatic vegetation and crops of peas or grain.", threat = "The paradise shelduck is so widespread and abundant as to be without conservation threat. Excessive hunting in the past severely impacted numbers." }},
    {"Silvereye", new Bird {birdName="Silvereye", family="Zosteropidae", nestType="Woven Cup", lifeSpan="12", status="No Threat", description = "Silvereyes are small songbirds that are easily recognised by their conspicuous white eye-ring; their plumage is mainly olive-green above and cream below. They colonised New Zealand from Australia in the 1850s, and is now one of New Zealand’s most abundant and widespread bird species.", population = "Silvereyes are common and widespread, often in large flocks in winter.", breeding = "Nesting between August-September and February, peaking in September – November in most localities. Two or three clutches may be raised during a season, with 2-4 eggs per clutch (typically 3). Eggs are pale blue and laid at 24 hour intervals. Incubation is shared by the sexes and takes 10-12 days.", food = "They are omnivorous and eat a range of small insects. They also feed on a range of small and large fruits including small berries and ripening fruit.", threat = "Cats, rats and stoats are as great an enemy to silvereye as they are to other native birds." }},
    {"Takahe", new Bird {birdName="Takahē", family="Rallidae", nestType="Ground-level", lifeSpan="17", status="Vulnerable", description = "The flightless South Island takahē is the world’s largest living rail (a family of small-medium sized ground-dwelling birds with short wings, large feet and long toes). The North Island takahē is unfortunately extinct. Takahē have special cultural, spiritual and traditional significance to Ngāi Tahu, the iwi (Māori tribe) of most of New Zealand’s South Island. Ngāi Tahu value takahē as a taonga (treasure).", population = "445 as of October 2020", breeding = "Takahē in Fiordland lay late October-January. In the lower altitude sites nesting begins in September. One to three eggs (usually two) are laid two days apart. Incubation takes 30 days, with chicks hatching two days apart.", food = "They feed mainly on leaf bases of tussock grasses. Sometimes, they feed on sedges, rushes and Aciphylla spp.", threat = "Natural hazards influencing the Fiordland takahē population include avalanches and cold climate. Modern threats include predation by introduced stoats, and competition for food from introduced red deer." }},
    {"Tui", new Bird {birdName="Tūī", family="Meliphagidae", nestType="Raised", lifeSpan="12", status="No Threat", description = "Tūī are unique to New Zealand. They can often be heard singing their beautiful melodies before they are spotted. You will recognise them by their distinctive white tuft under their throat.", population = "Tūī are locally abundant on the mainland and on some offshore islands, especially where there is a concentration of flowering plants or fruiting trees and generally in higher numbers in areas where there has been pest control.", breeding = "Eggs are laid from September to January. The clutch is 2-4 white or pale pink eggs, marked with reddish-brown spots and blotches. Incubation and brooding is by the female only.", food = "They belong to the honeyeater family, which means they feed mainly on nectar from flowers of native plants.", threat = "The main threats to Tūīs are predation (such as possums, feral cats, rats, stoats, and ferrets), and the destruction of habitat." }},
  };  
  Dictionary<int, Quiz> quizQandA;

  /* 
  * Start is called before the first frame update 
  */
  void Start()
  {
    // Turns off all UI Elements
    QuizElementsOff();
    birdBioPanel.gameObject.SetActive(false);
  }

  /*
  * Makes all the Canvas Quiz UI Elements invisible in the scene.
  */
  public void QuizElementsOff() {
    questionText.gameObject.SetActive(false);
    questionPanel.gameObject.SetActive(false);
    correctMsgPanel.gameObject.SetActive(false);
    questionNumberPanel.gameObject.SetActive(false);

    foreach (Button button in answerButtons) {
      button.gameObject.SetActive(false);
    }
  }

  /*
  * Makes all the Canvas Quiz UI Elements visible in the scene.
  */
  public void QuizElementsOn() {
    questionText.gameObject.SetActive(true);
    questionPanel.gameObject.SetActive(true);
    questionNumberPanel.gameObject.SetActive(true);

    foreach (Button button in answerButtons) 
    {
      button.gameObject.SetActive(true);
      button.gameObject.GetComponentInChildren<Button>().interactable = true;
    }

    GameObject[] incorrectIcons = GameObject.FindGameObjectsWithTag("Incorrect");
    foreach (GameObject icon in incorrectIcons)
    {
      icon.gameObject.SetActive(false);
    }
      SetQuestionandAnswer();
  }

  /*
  * Sets the Questions and answers based on the bird name provided.
  */
  public void SetQuestions(string birdName) {
    quizQandA = new Dictionary<int, Quiz>() {
      { 1, new Quiz { question = "What is the name of this bird?", answers = new List<string>() { "Albatross", "Blue Duck", "Kākāpō", "Little Blue Penguin", "NZ Pigeon", "Orange Parakeet", "Paradise Shelduck", "Silvereye",
      "Takahē", "Tūī"} }},
      { 2, new Quiz { question = "What family does " + birdsInfo[currentBird].birdName + " belong to?", answers =  new List<string>() { "Diomedeidae", "Anatidae", "Strigopidae", "Spheniscidae", "Columbidae",
      "Psittacidae", "Zosteropidae", "Rallidae", "Meliphagidae"} }},
      { 3, new Quiz { question = "What type of nest does " + birdsInfo[currentBird].birdName + " make?", answers = new List<string>() { "Pedestal", "Ground-level", "Burrow", "Raised", "Tree Hole", 
      "Woven Cup"} } },
      { 4, new Quiz { question = "What is the average life span of " + birdsInfo[currentBird].birdName + "?", answers = new List<string>() { "42", "8", "60", "6", "20", "12", "2", "17"} } },
      { 5, new Quiz { question = "What is the conservational status of " + birdsInfo[currentBird].birdName + "?", answers = new List<string>() { "Critical", "No Threat", "Vulnerable", "Declining", "Extinct"} } }
    };
  }

  /*
  * Makes the Bird Bio UI elements visible in the scene. It sets all the required information based
  * on the selected Bird.
  */
  public void DisplayBirdBio() {
    birdBioPanel.gameObject.SetActive(true);
    // Resets the scroll rect positioning to the top
    birdBioPanel.gameObject.GetComponent<ScrollRect>().verticalNormalizedPosition = 1.0f; 

    TextMeshProUGUI birdNameText = birdBioNamePanel.GetComponentInChildren<TextMeshProUGUI>();
    birdNameText.text = birdsInfo[currentBird].birdName;

    TextMeshProUGUI birdInfoText = birdBioInfoPanel.GetComponentInChildren<TextMeshProUGUI>();
    birdInfoText.text = birdsInfo[currentBird].description + "\n\n" 
    + "<b>Population:</b> " + birdsInfo[currentBird].population + "\n\n" 
    + "<b>Threats:</b> " + birdsInfo[currentBird].threat + "\n\n" 
    + "<b>Breeding:</b> " + birdsInfo[currentBird].breeding + "\n\n" 
    + "<b>Food:</b> " + birdsInfo[currentBird].food;
  }

  /*
  * Returns a generated the 3-element answer set from the subset of the possible answers that does not contain
  * the correct answer.
  */
  private List<string> GeneratePossibleOptions(int number) {
    List<string> withoutCorrectAnswer = quizQandA[number].answers;
    switch (number) {
      case 1:
        withoutCorrectAnswer.Remove(birdsInfo[currentBird].birdName);
      break;
      case 2:
        withoutCorrectAnswer.Remove(birdsInfo[currentBird].family);
      break;
      case 3:
        withoutCorrectAnswer.Remove(birdsInfo[currentBird].nestType);
      break;
      case 4:
        withoutCorrectAnswer.Remove(birdsInfo[currentBird].lifeSpan);
      break;
      case 5:
        withoutCorrectAnswer.Remove(birdsInfo[currentBird].status);
      break;
    }
    return withoutCorrectAnswer.OrderBy(item => random.Next()).Take(3).ToList();
  }

  /*
  * Adds the correct answer to the list of possible answers for the question and returns
  * the new list in a random order.
  */
  private List<string> GenerateAnswerSet(int number, List<string> possibleOptions) {
    switch (number) {
      case 1:
        possibleOptions.Add(birdsInfo[currentBird].birdName);
        correctAnswer = birdsInfo[currentBird].birdName;
      break;
      case 2:
        possibleOptions.Add(birdsInfo[currentBird].family);
        correctAnswer = birdsInfo[currentBird].family;
      break;
      case 3:
        possibleOptions.Add(birdsInfo[currentBird].nestType);
        correctAnswer = birdsInfo[currentBird].nestType;
      break;
      case 4:
        possibleOptions.Add(birdsInfo[currentBird].lifeSpan);
        correctAnswer = birdsInfo[currentBird].lifeSpan;
      break;
      case 5:
        possibleOptions.Add(birdsInfo[currentBird].status);
        correctAnswer = birdsInfo[currentBird].status;
      break;
    }
    return possibleOptions.OrderBy(item => random.Next()).ToList();
  }

  /*
  * Sets the text for the Question and Answer Buttons UI Elements.
  */
  public void SetQuestionandAnswer() {      
    int number = random.Next(1, 6);
    questionText.text = quizQandA[number].question;
    List<string> withoutCorrectAnswer = GeneratePossibleOptions(number);
    List<string> randomisedCompleteAnswers = GenerateAnswerSet(number, withoutCorrectAnswer);

    for (int i = 0; i < randomisedCompleteAnswers.Count; i++) {
      answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = randomisedCompleteAnswers[i]; 
    }
  }
}
