using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangePizza : MonoBehaviour
{
    [SerializeField] private GameObject pizza4;
    [SerializeField] private GameObject pizza6;
    [SerializeField] private GameObject pizza8;

    [SerializeField] private GameObject correct;
    [SerializeField] private GameObject failed;

    private System.Random _random;

    private void Awake()
    {
        correct.SetActive(false);
        failed.SetActive(false);
        _random = new System.Random();
        InstantiatePizza();
    }

    private void InstantiatePizza()
    {
        switch (_random.Next(1, 4))
        {
            case 1:
                Instantiate(pizza4, Vector3.zero, Quaternion.Euler(0,0,UnityEngine.Random.Range(0.0f, 360.0f)));
                break;
            case 2:
                Instantiate(pizza6, Vector3.zero, Quaternion.Euler(0,0,UnityEngine.Random.Range(0.0f, 360.0f)));
                break;
            case 3:
                Instantiate(pizza8, Vector3.zero, Quaternion.Euler(0,0,UnityEngine.Random.Range(0.0f, 360.0f)));
                break;
        }
    }

    public void ChangeVisiblePizza(PizzaCutCheck visiblePizza, bool result)
    {
        Destroy(visiblePizza.gameObject);
        StartCoroutine(ShowResult(result));
    }

    private IEnumerator ShowResult(bool result)
    {
        if (result)
            correct.SetActive(true);
        else
            failed.SetActive(true);

        yield return new WaitForSeconds(1);
        
        correct.SetActive(false);
        failed.SetActive(false);
        
        InstantiatePizza();
    }
}