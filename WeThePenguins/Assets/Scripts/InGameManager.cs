using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    public static InGameManager instance;

    private float kickTimer;
    [SerializeField]
    public Unit_Base[] Units;
    PlayerController[] Controllers;

    public bool isGameOver;
    public Animator UIanimator;

    public GameObject[] EndPannel;
    private void Awake()
    {
        instance = this;
        Controllers = new PlayerController[2];
        for (int i = 0; i < Units.Length; i++)
        {
            Controllers[i] = Units[i].GetComponent<PlayerController>();
        }
    }
    public void Kick()
    {
        if (Time.time > kickTimer)
        {
            kickTimer = Time.time + 0.5f;
            Vector3 center = (Units[0].transform.position + Units[1].transform.position) / 2;
            GameObject g = Instantiate(ArtResourceManager.instance.KickEffect, center, Quaternion.identity);
            Destroy(g, 2);
        }   
    }

    public void GameStart(bool isMultiPlayer)
    {
        UIanimator.Play("GameStart");
        foreach(PlayerController pc in Controllers)
        {
            pc.canControl = true;
        }
        if (isMultiPlayer)
        {
            Destroy(Controllers[1].GetComponent<AIController>());
            PlayerController newController = Units[1].gameObject.AddComponent<PlayerController>();
            newController.camp = 1;
            newController.canControl = true;
            Controllers[1] = newController;
        }
    }
    public void GameEnd()
    {
        if (!isGameOver)
        {
            foreach (PlayerController pc in Controllers)
            {
                pc.canControl = false;
            }
            StartCoroutine(Restart());
            isGameOver = true;
        }
        if (Units[0].IsDead)
        {
            EndPannel[1].SetActive(true);
        }
        else
        {
            EndPannel[0].SetActive(true);
        }
    }
    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(0);
    }
}
