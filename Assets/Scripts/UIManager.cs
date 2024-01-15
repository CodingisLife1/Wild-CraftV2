using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button findAnimal_button;
    public Button looting_button;
    public Button chat_button;
    public Button customPanel_button;

    [SerializeField] private TextMeshProUGUI meatAmount_txt;
    [SerializeField] private TextMeshProUGUI leavesAmount_txt;
    [SerializeField] private TextMeshProUGUI bonesAmount_txt;
    [SerializeField] private TextMeshProUGUI woodAmount_txt;

    public TextMeshProUGUI timer_txt;
    private bool firstOpenChat = true;
    [SerializeField] private GameObject castomWolf;
    [SerializeField] private GameObject cam_target;

    // Start is called before the first frame update
    void Start()
    {
        customPanel_button.onClick.AddListener(OpenCastomPanel);
        findAnimal_button.onClick.AddListener(GameManager.instance.player.StartCorFind);
        meatAmount_txt.text = Init.Instance.playerData.meatAmount.ToString();
        leavesAmount_txt.text = Init.Instance.playerData.leavesAmount.ToString();
        bonesAmount_txt.text = Init.Instance.playerData.bonesAmount.ToString();
        woodAmount_txt.text = Init.Instance.playerData.woodAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        meatAmount_txt.text = Init.Instance.playerData.meatAmount.ToString();
        leavesAmount_txt.text = Init.Instance.playerData.leavesAmount.ToString();
        bonesAmount_txt.text = Init.Instance.playerData.bonesAmount.ToString();
        woodAmount_txt.text = Init.Instance.playerData.woodAmount.ToString();
    }

    void OpenCastomPanel()
    {
        GameManager.instance.player.gameObject.SetActive(false);
        castomWolf.SetActive(true);
        GameManager.instance.cm.LookAt = cam_target.transform;
        GameManager.instance.cm.Follow = cam_target.transform;
    }

}
