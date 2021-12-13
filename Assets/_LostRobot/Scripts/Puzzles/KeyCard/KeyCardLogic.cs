
using UnityEngine;

public class KeyCardLogic : MonoBehaviour
{
    public bool obtainedInfo = false;
    public bool obtainedKey = false;

    public GameObject keyObject;
    public DoorController[] doors;

    [SerializeField] string id = Utils.GetUniqueId();
    PlayerData data;

    private void Start() {
        data = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().data;
        obtainedKey = data.obtainedKeyCards.Contains(id);
        obtainedInfo = data.obtainedKeyInfo.Contains(id);
        
        foreach (DoorController door in doors) {
            door.Locked = !obtainedKey || !obtainedInfo;//true;
            door.cardRequired = !obtainedKey || !obtainedInfo;//true;
        }
    }

    public void checkInfo()
    {
       /* if (!obtainedInfo)
        {
            keyObject.GetComponent<MeshRenderer>().material.color = Color.red;       //For Testing Purposes
        }
        else
        {
            keyObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        }*/
    }

    public void checkKey()
    {
        if (!obtainedKey) {
            foreach (DoorController door in doors)
                door.Close();
        } else { 
            foreach (DoorController door in doors)
                door.Locked = false;

            // keyObject.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }

    public void setInfo(bool obtained)
    {
        obtainedInfo = obtained;
    }

    public void gotKey(bool key)
    {
        if (obtainedInfo)
        {
            obtainedKey = key;
            data.AddKeyCard(id);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (!obtainedInfo)
            {
                obtainedInfo = true;
                data.AddObtainedInfo(id);
            }
            checkInfo();
        }
    }

}
