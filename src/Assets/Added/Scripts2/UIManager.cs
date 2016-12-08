using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject MainPanel, PanelMainButtons, PanelHost, PanelGuest;

    public void ClickHostButton() {
        PanelMainButtons.SetActive(false);
        PanelHost.SetActive(true);
    }

    public void ClickGuestButton() {
        PanelMainButtons.SetActive(false);
        PanelGuest.SetActive(true);
    }

    public void ClickButtonBack() {
        PanelHost.SetActive(false);
        PanelGuest.SetActive(false);
        PanelMainButtons.SetActive(true);
    }

    public void ButtonStart() {
        TimeCounter tc = FindObjectOfType<TimeCounter>().GetComponent<TimeCounter>();
        MainPanel.SetActive(false);
        tc.StartTimer();
    }
}
