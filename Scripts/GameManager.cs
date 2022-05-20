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
    List<Card> objects = new List<Card>();
    List<FamilyId> familyGot = new List<FamilyId>();

    [Header("Init Random")]
    [SerializeField]
    List<Card> allCards;
    [SerializeField]
    List<FamilySelect> allFamilies = new List<FamilySelect>();

    [Header("UI")]
    [SerializeField]
    Text txtFamily;

    [Header("Scene")]
    [SerializeField]
    string mainMenu;

    private bool isReturning = false;

    // Start is called before the first frames update
    void Start()
    {
        CheckValide();
        InitCards();
    }

    // Update is called once per frame
    void Update()
    {
        if (objects.Count >= nbCards && !isReturning)
        {
            StartCoroutine(ReturnCards());
        }

        txtFamily.text = familyGot.Count + " / " + nbFamily;
    }

    IEnumerator ReturnCards()
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

    private void InitCards()
    {
        if (allCards.Count == 0)
            return;
        for (int i = 0; i <nbFamily; i++)
        {
            int rd = Random.Range(0, allFamilies.Count - 1);

            allFamilies[rd].used = true;
            for (int x = 0; x < nbCards; x++)
            {
                int rdCard = Random.Range(0, allCards.Count - 1);
                if (!allCards[rdCard].SetCard(allFamilies[rd].id.members[x]))
                {
                    Debug.LogWarning("Failled to set family for " + allCards[rdCard].gameObject.name);
                }
                allCards.RemoveAt(rdCard);
            }
            allFamilies.RemoveAt(rd);
        }
    }

    private void CheckValide()
    {
        int nbValideFamilies = 0;
        for (int i = 0; i < allFamilies.Count; i++) {
            if (allFamilies[i].id.members.Length >= nbCards) {
                allFamilies[i].valide = true;
                nbValideFamilies++;
            } else
            {
                Debug.LogWarning("Family " + allFamilies[i].id.familyName + " invalide");
            }
        }
        if (nbValideFamilies < nbFamily)
        {
            Debug.LogWarning("Too many invalide families to run game");
            LoadMainMenu();
        }
    }

    public void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenu);
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

[System.Serializable]
public class FamilySelect
{
    public FamilyId id;
    public bool used = false;
    public bool valide = false;
}
