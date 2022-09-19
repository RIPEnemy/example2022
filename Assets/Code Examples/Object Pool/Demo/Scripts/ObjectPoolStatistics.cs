using UnityEngine;
using UnityEngine.UI;
using AbonyInt.EventSystem;

public class ObjectPoolStatistics : MonoBehaviour
{
    [Header("General")]
    [SerializeField]
    private ObjectPool _pool = null;

    [Header("Global Events")]
    [SerializeField]
    private GlobalEvent _onObtain = null;
    [SerializeField]
    private GlobalEvent _onRelease = null;

    [Header("UI")]
    [SerializeField]
    private Text _activeLabel = null;
    [SerializeField]
    private Text _inPoolLabel = null;

    private int activeObjects = 0;
    private int inPool = 0;

    private void Awake()
    {
        _pool.OnPreCreate += HandlePreCreate;
    }

    private void OnEnable()
    {
        activeObjects = 0;
        inPool = _pool.Count;
        
        _onObtain?.Subscribe(HandleObtain);
        _onRelease?.Subscribe(HandleRelease);
    }

    private void OnDisable()
    {
        _onObtain?.Unsubscribe(HandleObtain);
        _onRelease?.Unsubscribe(HandleRelease);
    }

    private void UpadateLabels()
    {
        _activeLabel.text = $"Active: {activeObjects}";
        _inPoolLabel.text = $"In Pool: {inPool}";
    }

    private void HandlePreCreate(object sender, GameObject[] _)
    {
        inPool = _pool.Count;

        UpadateLabels();
    }

    private void HandleObtain(object sender, object obj)
    {
        activeObjects++;
        inPool = _pool.Count;

        UpadateLabels();
    }

    private void HandleRelease(object sender, object e)
    {
        activeObjects--;
        inPool = _pool.Count;

        UpadateLabels();
    }
}
