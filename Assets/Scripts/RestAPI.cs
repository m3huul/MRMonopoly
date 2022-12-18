//using System;
//using SimpleJSON;
//using System.Collections;
//using System.Collections.Generic;
//using System.Globalization;
//using UnityEngine;
//using UnityEngine.Networking;
//using static System.Net.WebRequestMethods;

//public class RestAPI : MonoBehaviour
//{
//    public static RestAPI Instance { get; private set; }
//    public List<GameObject> properties;
//    public List<String> colors;
//    public Property prop;
//    //List<PropertyInfo> list = new List<PropertyInfo>();

//    private void Awake()
//    {
//        Instance = this;
//    }

//    private void Start()
//    {
//        StartCoroutine(GetPropertyInfo("http://192.168.1.34:5001/property_list"));
//    }
//    public IEnumerator GetPropertyInfo(String Url)
//    {
//        UnityWebRequest request = UnityWebRequest.Get(Url);
//        yield return request.SendWebRequest();

//        if (request.result == UnityWebRequest.Result.ConnectionError)
//        {
//            Debug.Log("Is network Server");
//        }
//        else
//        {
//            //List<PropertyInfo> units = new List<PropertyInfo>();
//            string EncryptedString = request.downloadHandler.text;
//            Debug.Log("Json data" + EncryptedString);
//            JSONNode jSONNode = JSON.Parse(EncryptedString);
//            for(int i = 0; i < jSONNode["data"].Count; i++)
//            {
//                for(int j=0; j < properties.Count; j++)
//                {
//                    if (Int32.Parse(jSONNode["data"][i]["property_id"]) == Int32.Parse(properties[j].name))
//                    {
//                        prop = properties[j].GetComponent<Property>();
//                        if (prop != null)
//                        {
//                            prop.propertyName = jSONNode["data"][i]["property_name"];
//                            prop.colorName = jSONNode["data"][i]["color"];
//                            prop.purchasePrice = jSONNode["data"][i]["price"];
//                            prop.mortgageValue = jSONNode["data"][i]["mortgage"];
//                            prop.housePrice = jSONNode["data"][i]["price_per_house"];
//                            prop.hotelPrice = jSONNode["data"][i]["hotel"];
//                            prop.rentPrices.Clear();
//                            prop.rentPrices.Add(jSONNode["data"][i]["rent"]);
//                            prop.rentPrices.Add(jSONNode["data"][i]["house1"]);
//                            prop.rentPrices.Add(jSONNode["data"][i]["house2"]);
//                            prop.rentPrices.Add(jSONNode["data"][i]["house3"]);
//                            prop.rentPrices.Add(jSONNode["data"][i]["house4"]);
//                        }
//                        break;
//                    }
//                }
                
//            }



//            //for (int i = 0; i < properties.Count; i++)
//            //{
//            //    Property property = properties[i].GetComponent<Property>();
//            //    property.colorName = jSONNode["data"][i]["property_name"];
//            //    colors.Add(jSONNode["data"][i]["property_name"]);
//            //    property.purchasePrice = jSONNode["data"][i]["price"];
//            //    property.mortgageValue = jSONNode["data"][i]["mortgage"];
//            //}
//        }
//    }
//}



