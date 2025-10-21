using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    private bool isFullScreen = false;

    private readonly Vector2Int[] resolutions = new Vector2Int[]
{
        new Vector2Int(1920, 1080),
        new Vector2Int(1600, 900)
};

    public enum SettingsPanelObjs
    {
        SettingsPanel,
        VideoSettings,
        SoundSettings,
        InfoSettings,
        TotalSound,
        VideoDropdown,
        Toggle,
        SettingExitButton,
        VideoButton,
        SoundButton,
        InfomationButton,
        TotalSoundSlider
    }

    private Dictionary<SettingsPanelObjs, GameObject> settingsPanelObjsMap;

    private void Awake()
    {
        settingsPanelObjsMap = Util.MapEnumChildObjects<SettingsPanelObjs, GameObject>(this.gameObject);
    }

    private void Start()
    {
        ManagerObject.instance.eventManager.SettingsPanelOnEvent -= SettingOn;
        ManagerObject.instance.eventManager.SettingsPanelOnEvent += SettingOn;

        settingsPanelObjsMap[SettingsPanelObjs.SettingExitButton].GetComponent<Button>().onClick.AddListener(() => {
            settingsPanelObjsMap[SettingsPanelObjs.SettingsPanel].gameObject.SetActive(false);
        });

        settingsPanelObjsMap[SettingsPanelObjs.VideoButton].GetComponent<Button>().onClick.AddListener(VideoButtonClick);
        settingsPanelObjsMap[SettingsPanelObjs.SoundButton].GetComponent<Button>().onClick.AddListener(SoundButtonClick);
        settingsPanelObjsMap[SettingsPanelObjs.InfomationButton].GetComponent<Button>().onClick.AddListener(InformationButtonClick);

        settingsPanelObjsMap[SettingsPanelObjs.VideoDropdown].GetComponent<Dropdown>().ClearOptions();
        settingsPanelObjsMap[SettingsPanelObjs.VideoDropdown].GetComponent<Dropdown>().AddOptions(new List<string> { "1920 x 1080", "1600 x 900" });
        settingsPanelObjsMap[SettingsPanelObjs.SettingsPanel].gameObject.SetActive(false);
        settingsPanelObjsMap[SettingsPanelObjs.TotalSoundSlider].GetComponent<Slider>().onValueChanged.AddListener((value) => { SliderChangeEvent(value); });


        VideoButtonClick();
        InitVideoSettings();
    }

    private void OnDestroy()
    {
        ManagerObject.instance.eventManager.SettingsPanelOnEvent -= SettingOn;

    }

    private void SettingOn()
    {
        settingsPanelObjsMap[SettingsPanelObjs.SettingsPanel].SetActive(true);
    }
    private void InitVideoSettings()
    {


        //�ػ�

        //�ʱ�ȭ //���������� ������ Resolution�� Fullscreen ���θ� ���Ͽ� �����Ͽ� ó���ϵ��� Ȯ�� ����
        settingsPanelObjsMap[SettingsPanelObjs.VideoDropdown].GetComponent<Dropdown>().value = 0;
        settingsPanelObjsMap[SettingsPanelObjs.VideoDropdown].GetComponent<Dropdown>().RefreshShownValue();
        ApplyResolution(settingsPanelObjsMap[SettingsPanelObjs.VideoDropdown].GetComponent<Dropdown>().value);

        // �ػ� ���� ��
        settingsPanelObjsMap[SettingsPanelObjs.VideoDropdown].GetComponent<Dropdown>().onValueChanged.AddListener(index =>
        {
            ApplyResolution(index);
        });


        //��üȭ�� ���

        //�ʱ�ȭ
        settingsPanelObjsMap[SettingsPanelObjs.Toggle].GetComponent<Toggle>().isOn = false;
        ToggleFullscreen(settingsPanelObjsMap[SettingsPanelObjs.Toggle].GetComponent<Toggle>().isOn);
        // ��üȭ�� ��� ��
        settingsPanelObjsMap[SettingsPanelObjs.Toggle].GetComponent<Toggle>().onValueChanged.AddListener(isFullscreen =>
        {
            ToggleFullscreen(isFullscreen);
        });

    }

    private void ToggleFullscreen(bool fullscreen)
    {
        isFullScreen = fullscreen;
        Screen.fullScreen = isFullScreen;
        Screen.fullScreenMode = isFullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    // �ε��� ��� �ػ� ���� (Dropdown ���� ��)
    private void ApplyResolution(int index)
    {
        if (index < 0 || index >= resolutions.Length) return;

        Vector2Int res = resolutions[index];
        Screen.SetResolution(res.x, res.y, isFullScreen);
    }



    private void VideoButtonClick()
    {
        settingsPanelObjsMap[SettingsPanelObjs.SoundSettings].gameObject.SetActive(false);
        settingsPanelObjsMap[SettingsPanelObjs.InfoSettings].gameObject.SetActive(false);
        settingsPanelObjsMap[SettingsPanelObjs.VideoSettings].gameObject.SetActive(true);
    }

    private void SoundButtonClick()
    {
        settingsPanelObjsMap[SettingsPanelObjs.VideoSettings].gameObject.SetActive(false);
        settingsPanelObjsMap[SettingsPanelObjs.InfoSettings].gameObject.SetActive(false);
        settingsPanelObjsMap[SettingsPanelObjs.SoundSettings].gameObject.SetActive(true);
    }

    private void InformationButtonClick()
    {
        settingsPanelObjsMap[SettingsPanelObjs.VideoSettings].gameObject.SetActive(false);
        settingsPanelObjsMap[SettingsPanelObjs.SoundSettings].gameObject.SetActive(false);
        settingsPanelObjsMap[SettingsPanelObjs.InfoSettings].gameObject.SetActive(true);
    }

    private void SliderChangeEvent(float value)
    {
        ManagerObject.instance.eventManager.OnSetMasterVolume(value);

    }
}
