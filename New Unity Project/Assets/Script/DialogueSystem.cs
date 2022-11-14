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
            //string eventName = rowValues[0]; <= �����ڵ�
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
