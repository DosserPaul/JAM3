using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField]
    CardId identification;
    [Header("Settings in Game")]
    [SerializeField]
    GameManager manager;
    [SerializeField]
    GameObject planeCharacter;
    public float overY = 10f;
    public float smoothSpeed = 5f;
    public float smoothRot = 25f;
    public float destroyY = 100.0f;

    [Header("User Interface")]
    public GameObject PlaneImage;

    private Transform startTransform;
    private Vector3 startOver;
    private Quaternion startRot;

    private bool overd = false;
    private bool clicked = false;
    private bool toDestroy = false;

    private void Awake()
    {
        startOver = transform.position;
        startRot = transform.rotation;
        startTransform = transform;
    }

    private void Update()
    {
        if (toDestroy)
        {
            if (transform.position.y >= startOver.y + destroyY)
            {
                Destroy(gameObject);
                return;
            }
            Vector3 nPos = startOver;
            nPos.y = startOver.y + destroyY;
            transform.position = Vector3.Lerp(transform.position, nPos, Time.deltaTime * smoothSpeed);
            return;
        }
        if (overd) {
            Vector3 nPos = startOver;
            nPos.y = startOver.y + overY;
            transform.position = Vector3.Lerp(transform.position, nPos, Time.deltaTime * smoothSpeed);
        } else
        {
            transform.position = Vector3.Lerp(transform.position, startOver, Time.deltaTime * smoothSpeed);
        }

        if (clicked) {
            Vector3 targetedRot = startTransform.eulerAngles;
            targetedRot.z = startRot.z + 180.0f;
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetedRot, smoothRot * Time.deltaTime);
        } else
        {
            Quaternion targetedRot = startRot;
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
        if (clicked)
            return;
        SetClicked(true);
    }

    public void SetClicked(bool b)
    {
        clicked = b;
        if (b == true)
        {
            if (!manager.AddCard(this))
            {
                clicked = false;
            }
        }
    }

    public void DestroyCard()
    {
        toDestroy = true;
    }

    public CardId GetCardId()
    {
        return identification;
    }

    public bool SetCard(CardId id)
    {
        if (identification != null)
        {
            return false;
        }
        identification = id;
        planeCharacter.GetComponent<MeshRenderer>().material = identification.Image;
        return true;
    }
}
