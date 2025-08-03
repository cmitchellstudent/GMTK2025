using TMPro;
using UnityEngine;

public class TaxPuzzle : MonoBehaviour
{
    public GameObject InventoryHand;
    public GameObject key;
    public TextMeshProUGUI WalletText;
    public TextMeshProUGUI loanText;
    public TextMeshProUGUI pawnText;
    public Pedestal loanPed;
    public Pedestal pawnPed;

    private float bonus;
    private float sell;
    private float balance;

    private bool isSolved;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        balance = 0.00f;
        isSolved = false;
    }

    // Update is called once per frame
    void Update()
    {
        WalletText.text = "Wallet: $" + (Mathf.Round(balance * 100f) / 100f + bonus + sell);

        if (loanPed.itemOn)
        {
            loanText.text = "Antique " + loanPed.itemOn.name + ": $100 loaned.";
            bonus = 100f;
        }
        else
        {
            bonus = 0f;
            loanText.text = "";
        }
        if (pawnPed.itemOn)
        {
            pawnText.text = "Best I can do is $10.";
            sell = 10f;
        }
        else
        {
            sell = 0f;
            pawnText.text = "";
        }

        if ((Mathf.Round(balance * 100f) / 100f + bonus + sell) >= 100f && !isSolved)
        {
            isSolved = true;
            key.GetComponent<Rigidbody>().isKinematic = true;
            key.transform.parent = InventoryHand.transform;
            key.transform.position = InventoryHand.transform.position;
            key.transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
    }

    public void IncrementJob()
    {
        balance += 0.01f;
    }
}
