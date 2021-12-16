using System.Collections.Generic;
using UnityEngine;

public class AreasOfInterest : MonoBehaviour
{

    static Dictionary<GameObject, GameObject> spawnedObjects = new Dictionary<GameObject, GameObject>();
    static Color32 hasObjectColor = new Color32(83, 0, 252, 50);

    SpriteRenderer spriteRenderer;
    bool kill;
    float timePassed;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        kill = true;
    }

    void Update() {
        if (kill)
            return;

        timePassed += Time.unscaledDeltaTime;
        float alpha = Mathf.PingPong(timePassed * 0.5f, 0.8f);
     
        if (alpha < 0.3f)
            alpha = 0.3f;
        spriteRenderer.color = new Color32(hasObjectColor.r, hasObjectColor.g, hasObjectColor.b, (byte) (alpha * 255));
    }


    private void OnTriggerStay(Collider other) {
        kill = false;
        spriteRenderer.color = hasObjectColor;
    }

    private void OnTriggerExit(Collider other) {
        kill = true;
        spriteRenderer.color = new Color(0, 0, 0, 0);
    }

    public static void Register(GameObject gameObject) {
        if (spawnedObjects.ContainsKey(gameObject))
            return;

        GameObject mapObject = new GameObject();
        mapObject.AddComponent<CharacterController>();
        Vector3 pos = gameObject.transform.position;
        pos.y = 185f;
        mapObject.transform.position = pos;
        mapObject.transform.SetParent(FindObjectOfType<AreasOfInterest>().transform);
        spawnedObjects.Add(gameObject, mapObject);
    }

    internal static void DeRegister(GameObject gameObject) {
        if (!spawnedObjects.ContainsKey(gameObject))
            return;
        spawnedObjects[gameObject].transform.position = Vector3.zero;
        Destroy(spawnedObjects[gameObject], 1);
        spawnedObjects.Remove(gameObject);
    }

}
