using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{

    public GameObject TextBox;  // L'objet textbox que tu as dans ton canvas, celui que tu veux "animé"

    public Text TheText;  // Le texte à l'intérieur de la TextBox

    public TextAsset TextFile;  // Le fichier .txt où sont stockés les dialogues
    public string[] TextLines;  // La liste où l'on stockera les lignes du fichier .txt

    public int CurrentLine;
    public int EndLine;

    private bool IsTyping = true;
    private int letter = 0;
    public float TypeSpeed;

    public bool defilement;

    public float timer;

    private float Phrase_lgt;



    void Start()
    {
        if (TextFile != null)  // Si le fichier texte donné n'est pas nul, pour eviter les bugs.
        {
            TextLines = (TextFile.text.Split('\n'));  // on sépare par les sauts de lignes, '\n' dans les fichier textes
        }

        if (EndLine == 0) // Connaitre le nombre de lignes.
        {
            EndLine = TextLines.Length - 1;
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (IsTyping)
        {
            if (letter <= TextLines[CurrentLine].Length - 1) // Si l'indice de la variable lettre est inferieur au maximum de lettres dans la ligne.
            {
                if (timer >= TypeSpeed) // Permet de définir la vitesse d'affichage.
                {
                    timer = 0;
                    TheText.text += TextLines[CurrentLine][letter]; // On rajoute la lettre suivante.
                    letter += 1;
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }
            else
            {
                IsTyping = false;
                Phrase_lgt = TheText.text.Length;

            }
        }
        if (!IsTyping)
        {
            letter = 0;
        }

        // Pour faire defiller le texte.
        if (timer > Phrase_lgt / 19.2f && defilement && !IsTyping)  // Tu utilises la longueur de la ligne 'Phrase_lgt' divisé par un coefficient de ton choix ( ici 19.2 ) pour que la phrase reste à l'écran pour un temps donné
        {
            timer = 0;
            CurrentLine += 1;
            TheText.text = "";
            IsTyping = true;

            if (CurrentLine > EndLine) // Ferma la box de dialogue quand tout est fini.
            {
                TextBox.SetActive(false);
                this.gameObject.SetActive(false);
              //GameManager.instance.ReloadLevel;
            }
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}