using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PizzaTimeCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI remainingPizzaText;
    [SerializeField] private TextMeshProUGUI remainingTime;

    [SerializeField] private Image progressTimeBar;
    private ChangePizza _pizzaManager;
    private float time;

    [SerializeField] private CanvasGroup _thisCanvasGroup;
    
    private void Awake() {
        _thisCanvasGroup = GetComponent<CanvasGroup>();
        _pizzaManager = FindObjectOfType<ChangePizza>();

    }

    private void Start() {
        
        Hide();
    }
    public void SetPizzaTimer(float totalTime){

        time = totalTime;

    }

    private void Update() {
        
        UpdatePizzaTimer();
        UpdateGraphics();
    }

    public void UpdatePizzaTimer(){

        if(_pizzaManager.minigameState && time > 0) {
        
            time -= Time.deltaTime;
        }
    }
    
    public void UpdateGraphics(){

        remainingPizzaText.text = "Remaining pizzas :" +  (_pizzaManager.remainingPizzas + 1).ToString();
        remainingTime.text = "Time :" + ((int) time).ToString();
        progressTimeBar.fillAmount = time/_pizzaManager.GetPizzaCuttingTotalRemainingTime();

    }   

    public void Hide(){

        _thisCanvasGroup.alpha = 0;
        _thisCanvasGroup.blocksRaycasts = false;

    }

        public void Display(){

        _thisCanvasGroup.alpha = 1;
        _thisCanvasGroup.blocksRaycasts = true;

    }

    public void SurrenderButton(){

        _pizzaManager.LoseMinigame();

    }


}
