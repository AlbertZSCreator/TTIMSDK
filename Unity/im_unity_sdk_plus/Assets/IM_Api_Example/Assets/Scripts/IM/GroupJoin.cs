using UnityEngine;
using UnityEngine.UI;
using com.tencent.im.unity.demo.types;
using com.tencent.imsdk.unity;
using com.tencent.imsdk.unity.types;
using com.tencent.imsdk.unity.enums;
using com.tencent.im.unity.demo.utils;

public class GroupJoin : MonoBehaviour
{
  public Text Header;
  public InputField GroupID;
  public InputField Greeting;
  public Text Result;
  public Button Submit;
  public Button Copy;

  void Start()
  {
    GroupID = GameObject.Find("GroupID").GetComponent<InputField>();
    Greeting = GameObject.Find("Greeting").GetComponent<InputField>();
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    if (CurrentSceneInfo.info != null)
    {
      Header.text = CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiText;
    }
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Submit.onClick.AddListener(GroupJoinSDK);
    Copy.onClick.AddListener(CopyText);
  }
  void GroupJoinSDK()
  {
    print(GroupID.text);
    TIMResult res = TencentIMSDK.GroupJoin(GroupID.text, Greeting.text, Utils.addAsyncNullDataToScreen(GetResult));
    Result.text = Utils.SynchronizeResult(res);
  }

  void GetResult(params object[] parameters)
  {
    Result.text += (string)parameters[0];
  }

  void CopyText()
  {
    Utils.Copy(Result.text);
  }
  void OnApplicationQuit()
  {
    TencentIMSDK.Uninit();
  }
}