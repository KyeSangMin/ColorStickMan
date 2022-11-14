using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    // �ڵ� ��ó https://www.youtube.com/watch?v=_nRzoTzeyxU 
    // https://velog.io/@gkswh4860/Unity-%EC%97%91%EC%85%80-%EB%8C%80%ED%99%94-%EB%82%B4%EC%9A%A9%EC%9D%84-%EB%8C%80%ED%99%94-%EC%9D%B4%EB%A6%84%EC%9C%BC%EB%A1%9C-%EB%AC%B6%EC%96%B4%EC%84%9C-%EA%B0%80%EC%A0%B8%EC%98%A4%EA%B8%B0#%EC%98%88%EC%99%B8-%EC%B2%98%EB%A6%AC

    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    public Queue<string> sentences;
   
    string[] contextsentance;


    public struct TalkData
    {
        public string name; // ��� ġ�� ĳ���� �̸�
        public string[] contexts; // ��� ����
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


        // ���� ���� 1��° ���� ���Ǹ� ���� �з��̹Ƿ� i = 1���� ����
        for (int i = 1; i < rows.Length; i++)
        {
            // A, B, C���� �ɰ��� �迭�� ����
            string[] rowValues = rows[i].Split(new char[] { ',' });

            // ��ȿ�� �̺�Ʈ �̸��� ���ö����� �ݺ�
            if (rowValues[0].Trim() == "" || rowValues[0].Trim() == "end") continue;

            List<TalkData> talkDataList = new List<TalkData>();
            string eventName = rowValues[0]; //<= �����ڵ�
            dialogue.EvenetNum = rowValues[0];


            while (rowValues[0].Trim() != "end") // talkDataList �ϳ��� ����� �ݺ���
            {

                // ĳ���Ͱ� �ѹ��� ġ�� ����� ���̸� �𸣹Ƿ� ����Ʈ�� ����
                List<string> contextList = new List<string>();


                TalkData talkData;
                talkData.name = rowValues[1]; // ĳ���� �̸��� �ִ� B��


                do // talkData �ϳ��� ����� �ݺ���
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
            // ĳ���� �̸� ���
            //Debug.Log(talkDatas[i].name);
            // ���� ���
            foreach (string context in talkDatas[i].contexts)
            {
                sentences.Enqueue(context);
            }
        }


        

    }

    public static TalkData[] GetDialogue(string eventName)
    {
        // Ű�� ��Ī�Ǵ� ���� ������ true ������ false
      
        if(DialoueDictionary.ContainsKey(eventName))
        return DialoueDictionary[eventName];
        else
        {
            // ��� ����ϰ� null ��ȯ
            Debug.LogWarning("ã�� �� ���� �̺�Ʈ �̸� : " + eventName);
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
