using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingBarManagerAddressable : MonoBehaviour
{
    [SerializeField] AddressableDownLoader addressabledownloader;
    Slider slider;
    [SerializeField] float progress_fake;
    [SerializeField] TextMeshProUGUI nametxt;
    [SerializeField] TextMeshProUGUI filesizetxt;
    //long totalfilesize_long;
    //string totalfilesize_string;


    [Header("Code support")]
    bool beginload;
    public void Init(AddressableDownLoader input_addressabledownloader, long totalfilesize)
    {
        addressabledownloader = input_addressabledownloader;
        //totalfilesize_long = totalfilesize;
        //totalfilesize_string = GameFunction.Instance.Parse_Byte_Filesizestring(totalfilesize);
        nametxt.text = addressabledownloader.label;
        name = addressabledownloader.filesize.ToString();

        beginload = true;
    }
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = 0.03f;
    }
    private void Update()
    {
        if (beginload)
        {
            progress_fake += Time.deltaTime / 0.2f;

            addressabledownloader.Update_Progress();
            slider.value = Mathf.Min(progress_fake, addressabledownloader.progress);
            //Debug.Log(addressabledownloader.progress);

            if (slider.value >= 1)
            {
                gameObject.SetActive(false);
                beginload = false;
                EventManager.Instance.SendEvent(GameConst.GameEvent.Loading_nextloadingbar_addressable.ToString(), null);
            }

            //
            //filesizetxt.text = "/ " + totalfilesize_string;
        }
    }
}
