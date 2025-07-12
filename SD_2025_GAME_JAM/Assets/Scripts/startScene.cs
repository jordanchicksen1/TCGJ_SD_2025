using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startScene : MonoBehaviour
{
    public GameObject text1;
    public GameObject text2;
    public GameObject title;
    public GameObject diorama;
    public GameObject play;
    public GameObject quit;

    public GameObject story1;
    public GameObject story2;
    public GameObject story3;
    public GameObject story4;

    public GameObject storySet1;
    public GameObject storySet2;
    public GameObject storySet3;
    public GameObject storySet4;

    public GameObject next1;
    public GameObject next2;
    public GameObject next3;
    public GameObject next4;

    public GameObject next5;
    public GameObject next6;
    public GameObject next7;
    public GameObject next8;

    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject tutorial3;
    public GameObject tutorial4;

    public GameObject tutorialSet1;
    public GameObject tutorialSet2;
    public GameObject tutorialSet3;



    public IEnumerator GameStart()
    {
        yield return new WaitForSeconds(5f);
        text1.SetActive(true);
        yield return new WaitForSeconds(6f);
        text2.SetActive(true);
        text1.SetActive(false);
        yield return new WaitForSeconds(6f);
        text2.SetActive(false);
        title.SetActive(true);
        diorama.SetActive(true);
    }

    public void Start()
    {
        StartCoroutine(GameStart());
    }

    public void StartButton()
    {

        title.SetActive(false);
        play.SetActive(true);
        quit.SetActive(true);

    }

    public void PlayButton()
    {
        play.SetActive(false);
        quit.SetActive(false);
        diorama.SetActive(false);
        StartCoroutine(Story1());
    }

    public void Next1()
    {
        storySet1.SetActive(false);
        story1.SetActive(false);
        next1.SetActive(false);
        StartCoroutine(Story2());
    }

    public void Next2()
    {
        storySet2.SetActive(false);
        story2.SetActive(false);
        next2.SetActive(false);
        StartCoroutine(Story3());
    }

    public void Next3()
    {
        storySet3.SetActive(false);
        story3.SetActive(false);
        next3.SetActive(false);
        StartCoroutine(Story4());
    }

    public void Next4()
    {
        storySet4.SetActive(false);
        story4.SetActive(false);
        next4.SetActive(false);
        StartCoroutine(Tutorial1());
    }

    public void Next5()
    {
        tutorialSet1.SetActive(false);
        tutorial1.SetActive(false);
        next5.SetActive(false);
        StartCoroutine(Tutorial2());
    }

    public void Next6()
    {
        tutorialSet2.SetActive(false);
        tutorial2.SetActive(false);
        next6.SetActive(false);
        StartCoroutine(Tutorial3());
    }

    public void Next7()
    {
        tutorialSet3.SetActive(false);
        tutorial3.SetActive(false);
        next7.SetActive(false);
        StartCoroutine(Tutorial4());
    }

    public void Next8()
    {
        SceneManager.LoadScene("GAME");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public IEnumerator Story1()
    {
        story1.SetActive(true);
        storySet1.SetActive(true);
        yield return new WaitForSeconds(5f);
        next1.SetActive(true);
    }

    public IEnumerator Story2()
    {
        story2.SetActive(true);
        storySet2.SetActive(true);
        yield return new WaitForSeconds(5f);
        next2.SetActive(true);
    }

    public IEnumerator Story3()
    {
        story3.SetActive(true);
        storySet3.SetActive(true);
        yield return new WaitForSeconds(5f);
        next3.SetActive(true);
    }

    public IEnumerator Story4()
    {
        story4.SetActive(true);
        storySet4.SetActive(true);
        yield return new WaitForSeconds(5f);
        next4.SetActive(true);
    }

    public IEnumerator Tutorial1() 
    {
        tutorial1.SetActive(true);
        tutorialSet1.SetActive(true);
        yield return new WaitForSeconds(5f);
        next5.SetActive(true);
    }

    public IEnumerator Tutorial2()
    {
        tutorial2.SetActive(true);
        tutorialSet2.SetActive(true);
        yield return new WaitForSeconds(5f);
        next6.SetActive(true);
    }

    public IEnumerator Tutorial3()
    {
        tutorial3.SetActive(true);
        tutorialSet3.SetActive(true);
        yield return new WaitForSeconds(5f);
        next7.SetActive(true);
    }

    public IEnumerator Tutorial4()
    {
        tutorial4.SetActive(true);
        
        yield return new WaitForSeconds(5f);
        next8.SetActive(true);
    }
}
