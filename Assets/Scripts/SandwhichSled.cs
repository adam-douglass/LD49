using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SandwhichSled : MonoBehaviour
{
    public GameObject SandwhichPrefab;
    public GameObject Display;
    public Text[] Requirements;
    public Font font;
    private GridControl Sandwhich;
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
        this.transform.position -= new Vector3(Time.deltaTime * 0.6f, 0, 0);
        UpdateText();
    }

    void UpdateText(){
        if(Requirements.Length != Sandwhich.Flavours.Length){
            Requirements = new Text[Sandwhich.Flavours.Length];
            for(var ii = 0; ii < Sandwhich.Flavours.Length; ii++){
                var body = new GameObject("requirementtext");
                body.transform.SetParent(Display.transform);
                body.transform.localScale = new Vector3(1, 1, 1);
                Requirements[ii] = body.AddComponent<Text>();
                Requirements[ii].color = Color.black;
                Requirements[ii].fontSize = 50;
                Requirements[ii].font = font;
            }
            LayoutRebuilder.MarkLayoutForRebuild(this.transform as RectTransform);
        }

        for(var ii = 0; ii < Sandwhich.Flavours.Length; ii++){
            if(Sandwhich.HasFlavour(Sandwhich.Flavours[ii])){
                Requirements[ii].text = Strikethrough(Sandwhich.Flavours[ii].ToString());
            } else {
                Requirements[ii].text = Sandwhich.Flavours[ii].ToString();
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

    public string Strikethrough(string s)
    {
        string strikethrough = "";
        foreach (char c in s)
        {
            strikethrough = strikethrough + c + '\u0336';
        }
        return strikethrough;
    }
}
