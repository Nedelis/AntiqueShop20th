using UnityEngine;

public class FlowManager : MonoBehaviour
{
    [SerializeField] private FlowChannel flowChannel;
    [SerializeField] private FlowState currentState;

    public FlowState CurrentState => currentState;
    public static FlowManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        flowChannel.OnFlowStateRequested += SetFlowState;
    }

    private void Start()
    {
        flowChannel.RaiseFlowStateChange(currentState);
    }

    private void OnDestroy()
    {
        flowChannel.OnFlowStateRequested -= SetFlowState;
        Instance = null;
    }

    private void SetFlowState(FlowState flowState)
    {
        if (currentState != flowState)
        {
            currentState = flowState;
            flowChannel.RaiseFlowStateChange(currentState);
        }
    }
}
