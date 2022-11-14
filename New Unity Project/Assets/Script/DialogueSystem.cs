using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    // 코드 출처 https://www.youtube.com/watch?v=_nRzoTzeyxU 
    // https://velog.io/@gkswh4860/Unity-%EC%97%91%EC%85%80-%EB%8C%80%ED%99%94-%EB%82%B4%EC%9A%A9%EC%9D%84-%EB%8C%80%ED%99%94-%EC%9D%B4%EB%A6%84%EC%9C%BC%EB%A1%9C-%EB%AC%B6%EC%96%B4%EC%84%9C-%EA%B0%80%EC%A0%B8%EC%98%A4%EA%B8%B0#%EC%98%88%EC%99%B8-%EC%B2%98%EB%A6%AC

    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    public Queue<string> sentences;
   
    string[] contextsentance;


    public struct TalkData
    {
        public string name; // 대사 치는 캐릭터 이름
        public string[] contexts; // 대사 내용
    }


   


    public static Dictionary<string, TalkData[]> DialoueDictionary =
                     new Dictionary<string, TalkData[]>();


    [SerializeField] public TextAsset csvFile = null;




    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
       
    }


   


    public void SplitDialogue(Dialogue dialogue)
    {

        string csvText = csvFile.text.Substring(0, csvFile.text.Length - 1);

        string[] rows = csvText.Split(new char[] { '\n' });


        // 엑셀 파일 1번째 줄은 편의를 위한 분류이므로 i = 1부터 시작
        for (int i = 1; i < rows.Length; i++)
        {
            // A, B, C열을 쪼개서 배열에 담음
            string[] rowValues = rows[i].Split(new char[] { ',' });

            // 유효한 이벤트 이름이 나올때까지 반복
            if (rowValues[0].Trim() == "" || rowValues[0].Trim() == "end") continue;

            List<TalkData> talkDataList = new List<TalkData>();
            string eventName = rowValues[0]; //<= 원본코드
            dialogue.EvenetNum = rowValues[0];


            while (rowValues[0].Trim() != "end") // talkDataList 하나를 만드는 반복문
            {

                // 캐릭터가 한번에 치는 대사의 길이를 모르므로 리스트로 선언
                List<string> contextList = new List<string>();


                TalkData talkData;
                talkData.name = rowValues[1]; // 캐릭터 이름이 있는 B열


                do // talkData 하나를 만드는 반복문
                {
                    contextList.Add(rowValues[2].ToString());
                    if (++i < rows.Length)
                        rowValues = rows[i].Split(new char[] { ',' });
                    else break;
                } while (rowValues[1] == "" && rowValues[0] != "end");



                talkData.contexts = contextList.ToArray();
                talkDataList.Add(talkData);




                //contextsentance = contextList.ToArray();
                //dialogue.sentences = contextsentance;


            }

            
            DialoueDictionary.Add(dialogue.EvenetNum, talkDataList.ToArray());

        }


    }



    public void SetEventNum(TalkData[] talkDatas)
    {

        
        for (int i = 0; i < talkDatas.Length; i++)
        {
            // 캐릭터 이름 출력
            //Debug.Log(talkDatas[i].name);
            // 대사들 출력
            foreach (string context in talkDatas[i].contexts)
            {
                sentences.Enqueue(context);
            }
        }


        

    }

    public static TalkData[] GetDialogue(string eventName)
    {
        // 키에 매칭되는 값이 있으면 true 없으면 false
      
        if(DialoueDictionary.ContainsKey(eventName))
        return DialoueDictionary[eventName];
        else
        {
            // 경고 출력하고 null 반환
            Debug.LogWarning("찾을 수 없는 이벤트 이름 : " + eventName);
            return null;
        }




    }


    public void StartDialogue (Dialogue dialogue)
    {

        animator.SetBool("IsOpen",true);

        nameText.text = dialogue.name;

        TalkData[] talkDatas;

        
        talkDatas = GetDialogue(FindObjectOfType<GameManager>().CheckEvent());
        


        sentences.Clear();
        SetEventNum(talkDatas);

        /*
        foreach (string sentence in dialogue.sentences)
        {
               sentences.Enqueue(sentence);
        }
       */
        

        DisPlayNextSentence();

    }


    public void DisPlayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        FindObjectOfType<UISensor>().ReSetDialogue();


    }




}
