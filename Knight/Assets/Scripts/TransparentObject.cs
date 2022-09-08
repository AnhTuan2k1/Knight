using System.Collections.Generic;
using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    [SerializeField] private List<GameObject> currentlyInTheWay;
    [SerializeField] private List<GameObject> alreadyTransparent;
    [SerializeField] private Transform player;
    [SerializeField] private Transform cameraa;

    void Awake()
    {
        currentlyInTheWay = new List<GameObject>();
        alreadyTransparent = new List<GameObject>();
    }

    void Start()
    {

    }

    void Update()
    {
        GetAllObjectInTheWay();
        MakeObjectTransparent();
        MakeObjectSolid();
    }

    private void GetAllObjectInTheWay()
    {
        currentlyInTheWay.Clear();
        float cameraPlayerDiatance = Vector3.Magnitude(cameraa.position - player.position);

        Ray ray1_Forward = new Ray(cameraa.position, player.position - cameraa.position);
        Ray ray1_Backward = new Ray(player.position, cameraa.position - player.position);

        var hits1_Forward = Physics.RaycastAll(ray1_Forward, cameraPlayerDiatance);
        var hits1_Backward = Physics.RaycastAll(ray1_Backward, cameraPlayerDiatance);

        foreach (var hit in hits1_Forward)
        {
            if (!currentlyInTheWay.Contains(hit.collider.gameObject))
            {
                currentlyInTheWay.Add(hit.collider.gameObject);
            }

        }
        foreach (var hit in hits1_Backward)
        {
            if (!currentlyInTheWay.Contains(hit.collider.gameObject))
            {
                currentlyInTheWay.Add(hit.collider.gameObject);
            }

        }
    }

    private void MakeObjectTransparent()
    {
        foreach (GameObject gameObject in currentlyInTheWay)
        {
            if (!alreadyTransparent.Contains(gameObject))
            {
                // add code to make object transparent here
                //Color color = gameObject.GetComponent<Renderer>().material.color;
                //Color c = new Color(color.r, color.g, color.b, 0.1f);
                //gameObject.GetComponent<Renderer>().material.color = c;
                gameObject.GetComponent<Renderer>().enabled = false;

                alreadyTransparent.Add(gameObject);
            }
        }
    }
    private void MakeObjectSolid()
    {
        foreach (GameObject gameObject in alreadyTransparent)
        {
            if (!currentlyInTheWay.Contains(gameObject))
            {
                // add code to make object solid here
                //Color color = gameObject.GetComponent<Renderer>().material.color;
                //Color c = new Color(color.r, color.g, color.b, 1);
                //gameObject.GetComponent<Renderer>().material.color = c;
                gameObject.GetComponent<Renderer>().enabled = true;

                alreadyTransparent.Remove(gameObject);
            }
        }
    }
}
