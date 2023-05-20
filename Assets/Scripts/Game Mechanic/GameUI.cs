using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private CellController _cellController;
    [SerializeField] private GameObject _instruction;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _liveCellsText;

    private bool isActive = false;

    private void Start()
    {
        _slider.onValueChanged.AddListener((v) =>
        {
            _cellController.speed = v;
        });
    }

    private void Update()
    {
        _liveCellsText.text = _cellController.GetAliveCellCount.ToString();
    }

    public void SetActive()
    {
        isActive = !isActive;
        _instruction.SetActive(isActive);
    }
}
