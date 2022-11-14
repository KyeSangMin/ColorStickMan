using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{


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
            //string eventName = rowValues[0]; <= 원본코드
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

                contextsentance = contextList.ToArray();
                dialogue.sentences = contextsentance;

               
            }
            
            DialoueDictionary.Add(dialogue.EvenetNum, talkDataList.ToArray());
            
        }


    }





    public void SetEventNum(Dialogue dialogue, string eventNum)
    {

        TalkData[] talkDatas;
        TalkData talk;

        List<string> contextList = new List<string>();

        foreach (KeyValuePair<string, TalkData[]> item in DialoueDictionary)
        {

            if(item.Key == eventNum)
            {
                talkDatas = item.Value;
                contextList.Add(talkDatas.ToString());
                Debug.Log(talkDatas);
            }
            

        }
        talk.contexts = contextList.ToArray();
      
        contextsentance = talk.contexts;
        dialogue.sentences = contextsentance;
        
    }




    public void StartDialogue (Dialogue dialogue)
    {

        animator.SetBool("IsOpen",true);

        nameText.text = dialogue.name;

        sentences.Clear();
        //SetEventNum(dialogue,"2");


        foreach (string sentence in dialogue.sentences)
        {
                sentences.Enqueue(sentence);
        }
       
        

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
