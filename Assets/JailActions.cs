using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JailActions : MonoBehaviour
{
    public static JailActions Instance;

    public Button GetOutOfJailFree;

    private void Awake()
    {
        Instance = this;
    }

    public enum JailAction { JAILFREECARD, PAY, ROLL, UNDECIDED};
    private JailAction choosenJailAction = JailAction.UNDECIDED;

    public JailAction GetJailAction()
    {
        return choosenJailAction;
    }

    public IEnumerator GetUserInput(bool x)
    {
        choosenJailAction = JailAction.UNDECIDED;

        transform.GetChild(0).gameObject.SetActive(true);
        GetOutOfJailFree.interactable = x;

        while(choosenJailAction == JailAction.UNDECIDED)
        {
            yield return null;
        }

        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OnClickPAY()
    {
        choosenJailAction = JailAction.PAY;
    }

    public void OnClickJAILFREE()
    {
        choosenJailAction = JailAction.JAILFREECARD;
    }

    public void OnClickROLL()
    {
        choosenJailAction = JailAction.ROLL;
    }
}
