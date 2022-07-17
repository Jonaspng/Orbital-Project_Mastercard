using UnityEngine.SceneManagement;
using UnityEngine;

public class CardManager : MonoBehaviour {

    [SerializeField] private GameObject warriorPanel1;

    [SerializeField] private GameObject warriorPanel2;

    [SerializeField] private GameObject archerPanel1;

    [SerializeField] private GameObject archerPanel2;

    [SerializeField] private GameObject magePanel1;

    [SerializeField] private GameObject magePanel2;

    private void Awake() {
        foreach(Transform obj in warriorPanel1.transform) {
            obj.GetComponent<Cards>().DisableAllScripts();
        }
        foreach(Transform obj in warriorPanel2.transform) {
            obj.GetComponent<Cards>().DisableAllScripts();
        }
        foreach(Transform obj in archerPanel1.transform) {
            obj.GetComponent<Cards>().DisableAllScripts();
        }
        foreach(Transform obj in archerPanel2.transform) {
            obj.GetComponent<Cards>().DisableAllScripts();
        }
        foreach(Transform obj in magePanel1.transform) {
            obj.GetComponent<Cards>().DisableAllScripts();
        }
        foreach(Transform obj in magePanel2.transform) {
            obj.GetComponent<Cards>().DisableAllScripts();
        }
    }

    private void Start() {
        onWarriorClick();
    }
    
    public void onWarriorClick() {
        warriorPanel1.SetActive(true);
        warriorPanel2.SetActive(true);
        archerPanel1.SetActive(false);
        archerPanel2.SetActive(false);
        magePanel1.SetActive(false);
        magePanel2.SetActive(false);
    }

    public void onArcherClick() {
        warriorPanel1.SetActive(false);
        warriorPanel2.SetActive(false);
        archerPanel1.SetActive(true);
        archerPanel2.SetActive(true);
        magePanel1.SetActive(false);
        magePanel2.SetActive(false);
    }

    public void onMageClick() {
        warriorPanel1.SetActive(false);
        warriorPanel2.SetActive(false);
        archerPanel1.SetActive(false);
        archerPanel2.SetActive(false);
        magePanel1.SetActive(true);
        magePanel2.SetActive(true);
    }

    public void onBackClick() {
        SceneManager.LoadScene("Start Menu");
    }
}
