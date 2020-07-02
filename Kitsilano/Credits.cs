using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KitsilanoScene
{
public class Credits : MonoBehaviour
{
    public float fadeOutTime;
    public float fadeInTime;

    public float waitTime;
    
    public GameObject funders;

    public GameObject advisors;

    int creditCount;

    void Start()
    {
        creditCount = 0;
        //funders.SetActive(false);
        advisors.SetActive(false);
    }

    private IEnumerator FadeAway(GameObject objectToFade)
    {           
        CanvasGroup canvasGroup = objectToFade.GetComponent<CanvasGroup>();

        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, Mathf.Min(1, t / fadeOutTime));
            yield return null;
        }
        canvasGroup.alpha = 0;
        objectToFade.SetActive(false);
    }


    private IEnumerator FadeIn(GameObject objectToFade)
    {           
        // FADE IN
        CanvasGroup canvasGroup = objectToFade.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        
        for (float t = 0.01f; t < fadeInTime; t += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, Mathf.Max(0, t / fadeInTime));
            yield return null;
        }
        canvasGroup.alpha = 1;       
        
        // WAIT
        yield return new WaitForSeconds(waitTime);

        // FADE OUT
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, Mathf.Min(1, t / fadeOutTime));
            yield return null;
        }
        canvasGroup.alpha = 0;
        objectToFade.SetActive(false);
        
        creditCount++;
        RollCredits();
        
    }

    public void RollCredits(){
        switch(creditCount)
        {
            case 0:
                funders.SetActive(true);
                StartCoroutine(FadeIn(funders));
                break;
            case 1:
                advisors.SetActive(true);
                StartCoroutine(FadeIn(advisors));
                break;
            case 2:
                KitsilanoManager.GetInstance().CurrentState = KitsilanoManager.GameState.MainMenu;
                break;


        }
        
        
        //advisors.SetActive(true);
        //StartCoroutine(FadeIn(advisors));
        //KitsilanoManager.GetInstance().CurrentState = KitsilanoManager.GameState.MainMenu;
    }
}

}
