using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField]
    CardId identification;
    [Header("Settings in Game")]
    public float overY = 10f;
    public float smoothSpeed = 5f;
    public float smoothRot = 25f;

    [Header("User Interface")]
    public GameObject PlaneImage;

    private Vector3 startOver;
    private Quaternion startRot;

    private bool overd = false;
    private bool clicked = false;

    private void Awake()
    {
        startOver = transform.position;
        startRot = transform.rotation;
    }

    private void Update()
    {
        if (overd) {
            Vector3 nPos = startOver;
            nPos.y = startOver.y + overY;
            transform.position = Vector3.Lerp(transform.position, nPos, Time.deltaTime * smoothSpeed);
        } else
        {
            transform.position = Vector3.Lerp(transform.position, startOver, Time.deltaTime * smoothSpeed);
        }

        if (clicked) {
            Quaternion targetedRot = startRot;
            targetedRot.z = targetedRot.z + 180.0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetedRot, Time.deltaTime * smoothRot);
        }
    }

    private void OnMouseOver()
    {
        overd = true;
    }

    private void OnMouseExit()
    {
        overd = false;
    }

    private void OnMouseDown()
    {
        SetClicked(true);
    }

    public void SetClicked(bool b)
    {
        clicked = b;
    }
}
