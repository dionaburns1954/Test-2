using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppleTree : MonoBehaviour
{

    [Header("Inscribed")]
    public GameObject applePrefab;
    public float speed = 3f;
    public float leftAndRightEdge = 24f;
    public float changeDirChance = 0.002f;
    public float appleDropDelay = 1f;

    public float[] speedDifficultyList = {3.0f, 4.0f, 5.0f};
    public float[] changeDirChanceDifficultyList = { 0.002f, 0.003f, 0.004f };
    public float[] appleDropDelayDifficultyList = { 1f, 0.5f, 0.25f };

    public ScoreCounter score;
    int difficultyIndex = 0;

    public Text dText;
    public string[] texts = { "Level 1", "Level 2", "Level 3" };


    // Start is called before the first frame update
    void Start()
    {
        Invoke("DropApple", 2f);
       
    }

    // Update is called once per frame
    void Update()
    {
        ChangeDifficulty(score.score);
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        if(pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed);
        }
        else if(pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed);
        }
    }

    private void ChangeDifficulty(int score)
    {
        if (score < 2000)
        {
            difficultyIndex = 0;


        }
        else if (score < 4000)
        {
            difficultyIndex = 1;

        }
        else if (score < 7000)
        {
            difficultyIndex = 2;
        }
        float newSpeed = speedDifficultyList[difficultyIndex];


        speed = Mathf.Sign(speed) * newSpeed;
        changeDirChance = changeDirChanceDifficultyList[difficultyIndex];
        appleDropDelay = appleDropDelayDifficultyList[difficultyIndex];
        dText.text = texts[difficultyIndex].ToString();
    }

    private void FixedUpdate()
    {
        if(Random.value < changeDirChance)
        {
            speed *= -1;
        }
    }

    void DropApple()
    {
        GameObject apple = Instantiate<GameObject>(applePrefab);
        apple.transform.position = transform.position;
        Invoke("DropApple", appleDropDelay);
    }
}
