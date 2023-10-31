using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class Obstacle : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject text;
    public GameObject winParticles;
    public Transform winParticlesPos;
    public GameObject looseParticles;

    void Start()
    {
        try
        {
            winParticlesPos = GameObject.Find("PosWinParticles").transform;
        }
        catch (Exception e)
        {
            print(e.Message);
        }
        text = GameObject.Find("ScoreText");
    }

    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Instantiate(looseParticles, collision.gameObject.transform.position, Quaternion.identity);
            iTween.PunchScale(collision.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 1.0f);
            Destroy(collision.gameObject, 0.5f);
            Destroy(gameObject);
            Invoke("ReloadLevel", 3);
        }
        if (collision.gameObject.name == "Out")
        {
            iTween.PunchScale(text, new Vector3(2, 2, 2), 1.0f);
            try
            {
                Instantiate(winParticles, winParticlesPos.position, Quaternion.identity);
            }
            catch (Exception e)
            {
                print(e.Message);
            }
            int newScore = int.Parse(text.GetComponent<TextMeshProUGUI>().text) + 1;
            text.GetComponent<TextMeshProUGUI>().text = newScore.ToString();
            Destroy(gameObject);
        }
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
