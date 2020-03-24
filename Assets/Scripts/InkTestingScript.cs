using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InkTestingScript : MonoBehaviour
{
    [SerializeField]
    private TextAsset inkJson;
    private Story story;

    [SerializeField]
    private TextMeshProUGUI textPrefab;

    [SerializeField]
    private Button buttonPrefab;
    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJson.text);
        RefreshDialogue();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChooseStoryChoice(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        RefreshDialogue();
    }

    string LoadStoryChunk()
    {
        string text = "";
        if (story.canContinue)
        {
            text = story.ContinueMaximally();
        }

        return text;
    }

    void RefreshDialogue()
    {
        ClearDialogue();
        TextMeshProUGUI storyText = Instantiate(textPrefab) as TextMeshProUGUI;
        storyText.text = LoadStoryChunk();
        storyText.transform.SetParent(this.transform, false);

        foreach (Choice choice in story.currentChoices)
        {
            Debug.Log(choice.text);
            Button choiceButton = Instantiate(buttonPrefab) as Button;
            choiceButton.transform.SetParent(this.transform, false);

            TextMeshProUGUI choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = choice.text;

            choiceButton.onClick.AddListener(delegate {
                ChooseStoryChoice(choice);
            });
        }
    }

    void ClearDialogue()
    {
        for(int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }
}
