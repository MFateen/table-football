using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject MainPanel, PanelMainButtons, PanelHost, PanelGuest;
    public InputField IPAddressField;


    public void ClickHostButton() {
        PanelMainButtons.SetActive(false);
        PanelHost.SetActive(true);

        Communication.ip = Communication.GetLocalIPAddress();

        //
        // Wait for Client as Host
        Communication.Host_Connect(Communication.ip);
        //

        //Start Game
        MainPanel.SetActive(false);
        GameController Controller = FindObjectOfType<GameController>().GetComponent<GameController>();
        Controller.StartGame(PlayerType.Host);
    }

    public void ClickGuestButton()
    {
        PanelMainButtons.SetActive(false);
        PanelGuest.SetActive(true);
    }
    
    public void ClickButtonConnect()
    {
        Communication.ip = IPAddressField.text;

        //
        // Connect to Host as Client
        Communication.Client_Connect(Communication.ip);
        //

        //Start Game
        GameController Controller = FindObjectOfType<GameController>().GetComponent<GameController>();
        Controller.StartGame(PlayerType.Guest);
        MainPanel.SetActive(false);
    }
    public void ClickButtonBack()
    {
        PanelHost.SetActive(false);
        PanelGuest.SetActive(false);
        PanelMainButtons.SetActive(true);
    }

}
