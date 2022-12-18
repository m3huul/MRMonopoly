using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CardDisplay : MonoBehaviour
{
    public int propertyId;

    public TMP_Text TitleText;
    public TMP_Text TaxText;
    public TMP_Text purchasePriceText;
    public TMP_Text mortgageValueText;
    public TMP_Text rentText;
    public TMP_Text rentWithColorSetText;
    public TMP_Text rentWith1HouseText;
    public TMP_Text rentWith2HouseText;
    public TMP_Text rentWith3HouseText;
    public TMP_Text rentWith4HouseText;
    public TMP_Text rentWithHotelText;
    public TMP_Text houseCostText;
    public TMP_Text hotelCostText;

    public void card(int propertyID ,string titleText/*, string taxText*/, string purchasePriceText, string mortgageValueText, string rentText, string rentWithColorSetText, string rentWith1HouseText, string rentWith2HouseText, string rentWith3HouseText, string rentWith4HouseText, string rentWithHotelText, string houseCostText, string hotelCostText)
    {
        propertyId = propertyID;
        this.TitleText.text = titleText;
        //this.TaxText.text = taxText;
        this.purchasePriceText.text = purchasePriceText;
        this.mortgageValueText.text = mortgageValueText;
        this.rentText.text = rentText;
        this.rentWithColorSetText.text = rentWithColorSetText;
        this.rentWith1HouseText.text = rentWith1HouseText;
        this.rentWith2HouseText.text = rentWith2HouseText;
        this.rentWith3HouseText.text = rentWith3HouseText;
        this.rentWith4HouseText.text = rentWith4HouseText;
        this.rentWithHotelText.text = rentWithHotelText;
        this.houseCostText.text = houseCostText;
        this.hotelCostText.text = hotelCostText;
    }
}
