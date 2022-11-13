using System;
using System.Collections;
using System.Collections.Generic;
using Constants;
using DG.Tweening;
using UnityEngine;

public class PlayerStackController : MonoBehaviour
{
    [Header("PLAYER STACK DATA")]
    [SerializeField] public List<PlayerStackClass> playerStackDataList;
    [Space]
    [Header("BOT STACK DATA")]
    [SerializeField] public List<BotStackClass> botStackDataList;
    
    private void OnEnable()
    {
        EventManager.OnPlayerStackSetAction += PlayerStackSetAction;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerStackSetAction -= PlayerStackSetAction;
    }

    void PlayerStackSetAction()
    {
        StackDataSet();
    }
    
    private void StackDataSet()
    {
        StartCoroutine(PunchAfterAddObj());
        for (int i = 0; i < playerStackDataList.Count; i++)
        {
            var objData = playerStackDataList[i];
            if (objData.statue) continue;
            
            GameObject cubePiece = objData.collectedCubePiece;
            Vector3 targetPos = new Vector3(0, i * 0.45f, 0.8f);
            cubePiece.transform.DOLocalMoveY(0.5f, 0.15f).OnComplete(() =>
            {
                cubePiece.transform.DOKill();
                cubePiece.transform.DOLocalJump(targetPos, 1, 1, 0.2f).OnComplete(()=> cubePiece.transform.DOLocalRotate(Vector3.zero, 0.1f));
                objData.statue = true;
            });
        }
    }
    
    public void AddCubeToList(PlayerStackClass playerStackClassData)
    {
        playerStackDataList.Add(playerStackClassData);
    }

    public void AddBotToStackList(BotController botObj)
    {
        BotStackClass botStackData = new BotStackClass(botObj, false, false, null);
        botStackDataList.Add(botStackData);
    }
    
    public Transform GetBotFollowTransformAndModify()
    {
        for (int i = 0; i < botStackDataList.Count; i++)
        {
            var botStayData = botStackDataList[i];
            if (botStayData.statue) continue;

            Vector3 targetPos;
            if (i == 0) { targetPos = new Vector3(0f, 0f, -1.5f);}//1
            else if (i == 1) { targetPos = new Vector3(0f, 0f, -2.5f);}//2
            else { targetPos = new Vector3(0f, 0f, (i + 1.5f) * -1f);}// 3
            var stackTrans = Instantiate(new GameObject(), targetPos, Quaternion.identity);
            stackTrans.transform.SetParent(transform);
            stackTrans.transform.localPosition = targetPos;
            botStayData.stayTransform = stackTrans.transform;
            botStayData.statue = true;
            return stackTrans.transform;
        }
        
        return null;
    }
    
    IEnumerator PunchAfterAddObj()
    {
        for (int i = 0; i < playerStackDataList.Count; i++)
        {
            var objData = playerStackDataList[i];
            if (!objData.statue) continue;
            if (!objData.flag) continue;
            
            if (objData.flag)
            {
                objData.flag = false;
                objData.collectedCubePiece.transform.DOPunchScale(Vector3.one * 0.15f, 0.15f).OnComplete(() => objData.flag = true);
            }

            yield return new WaitForSeconds(Parameters.PUNCH_DELAY);
        }
    }
}

[Serializable]
public class PlayerStackClass
{
    public GameObject collectedCubePiece;
    public bool statue;
    public bool flag;
    public PlayerStackClass(GameObject collectedCubePiece, bool statue, bool flag)
    {
        this.collectedCubePiece = collectedCubePiece;
        this.statue = statue;
        this.flag = flag;
    }
}

[Serializable]
public class BotStackClass
{
    public Transform stayTransform;
    public BotController botObj;
    public bool statue;
    public bool flag;
    public BotStackClass(BotController botObj, bool statue, bool flag, Transform stayTransform)
    {
        this.stayTransform = stayTransform;
        this.botObj = botObj;
        this.statue = statue;
        this.flag = flag;
    }
}