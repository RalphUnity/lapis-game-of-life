using UnityEngine;

public class CellView : MonoBehaviour
{
    public bool isActive = false;
    public int numNeighbors = 0;

    private SpriteRenderer _spriteRenderer;

    private void Awake() => _spriteRenderer = GetComponent<SpriteRenderer>();

    public void SetActivity(bool isActive)
    {
        this.isActive = isActive;

        _spriteRenderer.enabled = isActive;
    }
}