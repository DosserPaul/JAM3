using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    int nbCards = 4;
    [SerializeField]
    int nbFamily = 6;
    [SerializeField]
    float timeToReturn = 5.0f;
    public List<Card> objects = new List<Card>();
    public List<FamilyId> familyGot = new List<FamilyId>();

    [Header("UI")]
    [SerializeField]
    Text txtFamily;

    private bool isReturning = false;

    // Start is called before the first frames update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objects.Count >= nbCards && !isReturning)
        {
            StartCoroutine(returnCards());
        }

        txtFamily.text = familyGot.Count + " / " + nbFamily;
    }

    IEnumerator returnCards()
    {
        isReturning = true;
        bool sameFamily = true;
        FamilyId lastFamily = null;
        for (int i = 0; i < objects.Count; i++)
        {
            if (lastFamily == null)
            {
                Debug.Log("last family is null");
                CardId cid = objects[i].GetCardId();
                if (cid != null)
                {
                    lastFamily = cid.family;
                }
            } else {
                CardId cid = objects[i].GetCardId();
                if (cid != null && cid.family != null)
                {
                    if (cid.family.familyName != lastFamily.familyName)
                    {
                        Debug.Log("last family (" + lastFamily.familyName + ") != cauu family (" + cid.family.familyName + ")");
                        sameFamily = false;
                        break;
                    }
                }
            }
        }
        if (sameFamily)
        {
            familyGot.Add(lastFamily);
        }
        yield return new WaitForSeconds(timeToReturn);
        for (int i = 0; i < objects.Count; i++)
        {
            if (sameFamily)
            {
                objects[i].DestroyCard();
            }
            objects[i].SetClicked(false);
        }
        objects.Clear();
        isReturning = false;
    }

    public bool AddCard(Card crd)
    {
        if (objects.Count >= nbCards)
        {
            Debug.LogWarning("Too many cards selected");
            return false;
        }
        objects.Add(crd);
        return true;
    }
}
