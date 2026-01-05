using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    GameManager gameManager;
    public int enemies;
    bool a;
    public bool d;
    public GameObject[] seals;
    public int sealNum;
    public bool b;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies > 0)
        {
            a = true;
        }
        if (a && enemies <= 0)
        {
            RoomDone();
        }
        if (d && sealNum < seals.Length)
        {
            seals[sealNum].SetActive(true);
            sealNum++;
        }
        if (b && sealNum < seals.Length)
        {
            seals[sealNum].SetActive(false);
            sealNum++;
        }
    }
    public void RoomStart()
    {
        d = true;
    }
    public void RoomDone()
    {
        d = false;
        b = true;
        a= false;
        sealNum = 0;
        gameManager.doneRooms++;
    }
}