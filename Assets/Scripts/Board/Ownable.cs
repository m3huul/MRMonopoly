using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ownable : BoardLocation
{   
    public Player owner;

    public bool isMortgaged;
    [SerializeField] public string propertyName;
    [SerializeField] public string colorName;
    [SerializeField] public int purchasePrice;
    [SerializeField] public int mortgageValue;
    [SerializeField] public Sprite deed;
    [SerializeField] public MeshRenderer OwnerColorIndicator;

    public Vector3 position;
    public Vector3 size;

    protected override void AdditionalInit()
    {
        position = transform.position;
        size = transform.localScale;
    }

    public sealed override void PassBy(Player player)
    {}

    public sealed override IEnumerator LandOn(Player player)
    {
        if (owner == null)
        {
            if (!player.IsAI())
            {
                if (Gameplay.instance.CameraAnim)
                {
                    yield return LerpCameraViewToThisLocation();
                }
                yield return OwnablePurchaseDialog.instance.OfferPurchase(this);

                if (OwnablePurchaseDialog.instance.resultingDecision)
                {
                    player.currentOwnables.Add(this);
                    // todo if colorset complete
                    player.AdjustBalanceBy(-purchasePrice);
                    owner = player;

                    OwnerColorIndicator.material = player.playerColor;
                    OwnerColorIndicator.gameObject.SetActive(true);
                }
                else
                {
                    //TODO Auction logic here.
                }
                if (Gameplay.instance.CameraAnim)
                {
                    yield return LerpCameraViewBackToMainBoardView();
                }
            }
            else
            {
                print("ai will choose either to buy or skip");
                yield return new WaitForSeconds(2f);

                string result = player.ai.BuyOrSkip(this);
                if (result == "buy")
                {
                    player.AdjustBalanceBy(-purchasePrice);
                    player.currentOwnables.Add(this);
                    owner = player;
                    print(result);

                    OwnerColorIndicator.material = player.playerColor;
                    OwnerColorIndicator.gameObject.SetActive(true);
                }
                else if(result == "skip")
                {
                    print(result);
                }
                else
                    print(result);
            }
        }
        else
        {
            if (owner != player)
            {
                int toCharge = ChargePlayer();
                player.AdjustBalanceBy(-toCharge);
                owner.AdjustBalanceBy(toCharge);
            }
            else
            {
                if (BuildHouses(this))
                {
                    print("you can build house");
                }
                //else
                //{
                //    print("you dont have all the properties of this color");
                //}
            }
        }
    }

    public void ShowUnMortgagedProperty()
    {
        if (!isMortgaged)
        {
            transform.position = new Vector3(position.x, position.y + .2f, position.z);
            transform.localScale = new Vector3(size.x + .2f, size.y + .2f, size.z + .2f);
        }
    }

    public void ShowOwnable()
    {
        transform.position = new Vector3(position.x, position.y + .2f, position.z);
        transform.localScale = new Vector3(size.x + .2f, size.y + .2f, size.z + .2f);
    }

    public void reset()
    {
        transform.position = position;
        transform.localScale = size;
    }

    protected abstract int ChargePlayer();

    public abstract bool BuildHouses(Ownable owner);

    public abstract bool ColorSet(Player player);
    
}
