using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GenerateMonster : MonoBehaviour
{
    private float range, angle;
    public int moleCnt;
    public GameObject monster1;
    public GameObject monster2;
    public GameObject itemHeart;
    public GameObject itemAmmoCount;
    public Transform startPosition;
    public Transform user;
    public bool isFeverTime = false;
    private Vector3 generatePosition;

    public List<GameObject> chickList = new List<GameObject>();
    public List<GameObject> moleList = new List<GameObject>();

    public GameObject chickDeathEffect;
    public GameObject moleDeathEffect;

    private AudioSource backgraoundMusic;
    public AudioClip bgmTimoSong;
    public AudioClip fevertimeSong;

    private float lastTimoStopTime;

    private int goFevercnt;


    ESCPanel isStop;

    private void Awake()
    {
        isStop = gameObject.GetComponent<ESCPanel>();
        lastTimoStopTime = 0;
        range = 8f;
        moleCnt = 0;
        StartCoroutine(monsterSpawner());
        backgraoundMusic = GetComponent<AudioSource>();
        playSound(bgmTimoSong, backgraoundMusic);
    }
    //몹 생성
    private void Update()
    {
        if (isStop.IsPause){ 
            backgraoundMusic.mute = true;
        }
        else
        {
            backgraoundMusic.mute = false;
        }
    }
    IEnumerator monsterSpawner()
    {
        float time = 0;
        float spawnTime = 2f;
        goFevercnt = 10;
        while (true)
        {
            time += 0.1f;

            if (moleCnt < goFevercnt && time > spawnTime)
            {
                GenerateMole();
                time = 0;
                if (spawnTime > 0.6f) spawnTime -= 0.05f;
            }

            if (moleCnt >= goFevercnt)
            {
                if (!isFeverTime)
                {
                    time = 0;
                    isFeverTime = true;
                    feverTime();
                }
                if (Random.Range(1, 3) == 1)
                {
                    GenerateChick();
                }
            }

            if (moleCnt >= goFevercnt && time > 20f)
            {
                moleCnt = 0;
                time = 0;
                DestroyChick();
                Debug.Log("DestroyChick");
                if (isFeverTime)
                {
                    isFeverTime = false;
                    finishFeverTime();
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void GenerateMole()
    {
        angle = Random.Range(0, 360);
        generatePosition.x = range * (float)(Mathf.Cos(angle));
        generatePosition.y = 10;
        generatePosition.z = range * (float)(Mathf.Sin(angle));

        moleList.Add(Instantiate(monster2, startPosition.position + generatePosition, startPosition.rotation));
    }
    private void GenerateChick()
    {
        angle = Random.Range(0, 360);

        generatePosition.x = range * (float)(Mathf.Cos(angle));
        generatePosition.y = Random.Range(4,8);
        generatePosition.z = range * (float)(Mathf.Sin(angle));

        chickList.Add(Instantiate(monster1, startPosition.position + generatePosition, startPosition.rotation));
    }
    private void feverTime()
    {
        Debug.Log("Start Fever Time");
        //fever time 효과음 넣기
        playSound(fevertimeSong, backgraoundMusic);
        for (int i = 0; i < moleList.Count; i++)
            moleList[i].SetActive(false);
    }

    private void finishFeverTime()
    {
        Debug.Log("Finish Fever Time");
        goFevercnt += 10;
        Debug.Log("go fever cnt : " + goFevercnt);
        playSound(bgmTimoSong, backgraoundMusic);
        for (int i = 0; i < moleList.Count; i++)
            moleList[i].SetActive(true);
    }

    public void GenerateHeartItem(Vector3 pos)
    {
        Vector3 rot = new Vector3(-90f, 0f, 0f);
        Instantiate(itemHeart, pos, Quaternion.Euler(rot));
    }
    public void GenerateAmmoItem(Vector3 pos)
    {
        Vector3 rot = new Vector3(-90f, 0f, 0f);
        Instantiate(itemAmmoCount, pos, Quaternion.Euler(rot));
    }

    void DestroyChick()
    {
        int cnt;
        GameObject deleteChick;
        cnt = chickList.Count;
        Debug.Log(cnt);
        for (var i = 0; i < cnt; i++)
        {

            makeChickParticleSystem(chickList[0].transform.position);


            //remove object and ramove at list
            deleteChick = chickList[0];
            Destroy(deleteChick);
            chickList.RemoveAt(0);
        }
    }

    public void makeChickParticleSystem(Vector3 startPos)
    {
        Instantiate(chickDeathEffect, startPos, Quaternion.Euler(Vector3.forward));
    }
    public void makeMoleParticleSystem(Vector3 startPos)
    {
        Instantiate(moleDeathEffect, startPos, Quaternion.Euler(Vector3.forward));
    }

    public void playSound(AudioClip clip, AudioSource player)
    {
        if (isFeverTime) lastTimoStopTime = player.time;

        player.Stop();
        player.clip = clip;
        player.loop = true;

        if (!isFeverTime) player.time = lastTimoStopTime;
        else player.time = 20;
        player.Play();
    }


}
