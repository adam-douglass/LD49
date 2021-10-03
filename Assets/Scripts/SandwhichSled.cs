using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SandwhichSled : MonoBehaviour
{
    public GameObject SandwhichPrefab;
    public GameObject Display;
    public TextMeshProUGUI[] Requirements;
    public Font font;
    public bool finished = false;
    private GridControl Sandwhich;
    public GameObject FlavourText;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        var created = Instantiate(SandwhichPrefab, this.gameObject.transform.position, Quaternion.identity);
        created.transform.SetParent(this.transform);   
        Sandwhich = created.GetComponent<GridControl>();

        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
        this.transform.position -= new Vector3(Time.deltaTime * 0.6f, 0, 0);
        if(finished) Sandwhich.Lock();
    }

    void UpdateText(){
        if(Requirements.Length != Sandwhich.Flavours.Length){
            Requirements = new TextMeshProUGUI[Sandwhich.Flavours.Length];
            for(var ii = 0; ii < Sandwhich.Flavours.Length; ii++){
                //var body = new GameObject("requirementtext");
                GameObject body = Instantiate(FlavourText, Display.transform);
                body.name = "requirementtext";
                Requirements[ii] = body.GetComponent<TextMeshProUGUI>();
                Requirements[ii].text = Sandwhich.Flavours[ii].ToString();
            }

            LayoutRebuilder.MarkLayoutForRebuild(this.transform as RectTransform);
        }

        finished = true;
        for(var ii = 0; ii < Sandwhich.Flavours.Length; ii++){
            if (Sandwhich.HasFlavour(Sandwhich.Flavours[ii]))
            {
                Requirements[ii].fontStyle |= FontStyles.Strikethrough;
            }
            else
            {
                Requirements[ii].fontStyle &= ~FontStyles.Strikethrough;
                finished = false;
            }
        }
    }

    private void OnDestroy()
    {
        if (audioManager != null)
        {
            audioManager.RingBell();
        }
    }
}
