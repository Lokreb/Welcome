using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIServices : MonoBehaviour
{
    public TextMeshProUGUI NB_Patient;
    public TextMeshProUGUI Multiplicator;

    public Service Service;

    

    private void Start()
    {
        Service.OnPatientChange += PatientChange;
        Service.OnMultiplicatorChange += MultiplicatorChange;
    }

    void PatientChange(int nb)
    {
        NB_Patient.text = nb.ToString();
    }

    Color alpha0f = new Color(1f, 1f, 1f, 0f);
    Color alpha1f = new Color(1f, 1f, 1f, 1f);
    void MultiplicatorChange()
    {
        Multiplicator.color = Service.VitesseMultiplicator == 1 ? alpha0f : alpha1f;
        Multiplicator.text = "x " + Service.VitesseMultiplicator.ToString();
    }
}
