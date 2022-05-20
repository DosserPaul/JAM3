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
        yield return new WaitForSeconds(timeToReturn);
        bool sameFamily = false;
        FamilyId lastFamily = null;
        for (int i = 0; i < objects.Count; i++)
        {
            if (lastFamily == null)
            {
                CardId cid = objects[i].GetCardId();
                if (cid != null)
                {
                    lastFamily = cid.family;
                }
            } else
            {
                CardId cid = objects[i].GetCardId();
                if (cid != null)
                {
                    if (cid.family.familyName != lastFamily.familyName)
                    {
                        sameFamily = false;
                    }
                }
            }
            objects[i].SetClicked(false);
        }
        objects.Clear();
        if (sameFamily)
        {
            familyGot.Add(lastFamily);
        }
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
