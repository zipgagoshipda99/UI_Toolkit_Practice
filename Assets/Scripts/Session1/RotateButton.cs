using System;
using UITKUtils;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
//using Random = System.Random;

[RequireComponent(typeof(UIDocument))]
public class RotateButton : MonoBehaviour
{
    [FormerlySerializedAs("objectToSpin")]
    public GameObject objectToSpinRotate;
    public MeshRenderer meshRenderer;
    bool m_Spin;
    bool m_Rotate;
    Color defaultColor;
    const string SpinButtonName = "SpinButton";
    const string RotateButtonName = "RotateButton";
    const string StopButtonName = "StopButton";
    const string ResetButtonName = "ResetButton";
    const string RandomColorButtonName = "RandomColorButton";
    const string SizeUpButtonName = "SizeUpButton";

    const float SpinSpeed = 1f;

    VisualElement m_Root;
    Button m_SpinButton;
    Button m_RotateButton;
    Button m_StopButton;
    Button m_ResetButton;
    Button m_RandomColorButton;
    Button m_SizeUpButton;
    [NonSerialized] UIDocument m_UIDocument;

    void Awake()
    {
        m_UIDocument = GetComponent<UIDocument>();
        m_Root = m_UIDocument.rootVisualElement;
        meshRenderer = objectToSpinRotate.GetComponent<MeshRenderer>();
        defaultColor = meshRenderer.material.color;
    }

    void OnEnable()
    {
        m_SpinButton = m_Root.Q<Button>(SpinButtonName);
        Validation.CheckQuery(m_SpinButton, SpinButtonName);
        m_SpinButton?.RegisterCallback<ClickEvent>(StartSpin);

        m_RotateButton = m_Root.Q<Button>(RotateButtonName);
        Validation.CheckQuery(m_RotateButton, RotateButtonName);
        m_RotateButton?.RegisterCallback<ClickEvent>(StartRotate);

        m_StopButton = m_Root.Q<Button>(StopButtonName);
        Validation.CheckQuery(m_StopButton, StopButtonName);
        m_StopButton?.RegisterCallback<ClickEvent>(Stop);

        m_ResetButton = m_Root.Q<Button>(ResetButtonName);
        Validation.CheckQuery(m_ResetButton, ResetButtonName);
        m_ResetButton?.RegisterCallback<ClickEvent>(Reset);

        m_RandomColorButton = m_Root.Q<Button>(RandomColorButtonName);
        Validation.CheckQuery(m_RandomColorButton, RandomColorButtonName);
        m_RandomColorButton?.RegisterCallback<ClickEvent>(SetRandomColor);

        m_SizeUpButton = m_Root.Q<Button>(SizeUpButtonName);
        Validation.CheckQuery(m_SizeUpButton, SizeUpButtonName);
        m_SizeUpButton?.RegisterCallback<ClickEvent>(SizeIncrease);


    }

    void FixedUpdate()
    {
        SpinRotateObject();
    }

    void SpinRotateObject()
    {
        if (m_Spin)
        {
            objectToSpinRotate.transform.Rotate(Vector3.up, SpinSpeed, Space.World);
        }

        if (m_Rotate)
        {
            objectToSpinRotate.transform.Rotate(Vector3.left, SpinSpeed, Space.World);
        }
    }

    void StartSpin(ClickEvent evt)
    {
        m_Spin = true;
    }

    void StartRotate(ClickEvent evt)
    {
        m_Rotate = true;
    }

    void Stop(ClickEvent evt)
    {
        m_Spin = false;
        m_Rotate = false;
    }

    void Reset(ClickEvent evt)
    {
        objectToSpinRotate.transform.rotation = Quaternion.identity;
        objectToSpinRotate.transform.localScale = new Vector3(1,1,1);
        meshRenderer.material.color = defaultColor;
    }

    void SetRandomColor(ClickEvent evt)
    {
        Color[] RandomColors = { Color.red, Color.orange, Color.yellow, Color.green, Color.blue, Color.purple, Color.pink, Color.black, Color.white };
        var random = new System.Random();
        int pickedIndex = random.Next(0, RandomColors.Length);
        Color pickedColor = RandomColors[pickedIndex];
        meshRenderer.material.color = pickedColor;
    }

    void SizeIncrease(ClickEvent evt)
    {
        Vector3 currentSize = objectToSpinRotate.transform.localScale;
        objectToSpinRotate.transform.localScale = currentSize * 1.15f;
    }
}
