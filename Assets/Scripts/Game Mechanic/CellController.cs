using UnityEngine;

public class CellController : MonoBehaviour
{
    private CellModel _cellModel;

    public int GetAliveCellCount
    {
        get
        {
            return _cellModel.aliveCount;
        }
    }

    [HideInInspector] public float speed;
    private float _timer = 0f;

    private bool simulationEnabled = false;

    private void Awake() => _cellModel = new CellModel();

    // Start is called before the first frame update
    private void Start() => _cellModel.ExecuteCellProcedure(CellProcedures.Instantiation);

    // Update is called once per frame
    private void Update()
    {
        if (simulationEnabled)
        {
            if (Mathf.Abs(_timer) > Mathf.Abs(speed))
            {
                _timer = 0f;
                _cellModel.ExecuteCellProcedure(CellProcedures.Interaction);
                _cellModel.ExecuteCellProcedure(CellProcedures.Rules);
            }
            else
                _timer += Time.deltaTime;
        }

        UserInput();
    }

    public void UserInput()
    {
        if (Input.GetMouseButtonDown(0))
            _cellModel.ClickCellInBoundary();

        // Pause simulation
        if (Input.GetKeyUp(KeyCode.P))
            simulationEnabled = false;

        // Run simulation / Resume
        if (Input.GetKeyUp(KeyCode.B))
            simulationEnabled = true;

        if (Input.GetKeyUp(KeyCode.T))
            _cellModel.RandomCellPattern();

        if (Input.GetKeyDown(KeyCode.R))
            _cellModel.RestartGame();
    }
}
