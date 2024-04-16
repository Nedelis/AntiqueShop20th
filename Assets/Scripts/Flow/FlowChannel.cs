using UnityEngine;

[CreateAssetMenu(menuName = "Flow/Flow Channel")]
public class FlowChannel : ScriptableObject
{
    public delegate void FlowStateCallback(FlowState flowState);
    public FlowStateCallback OnFlowStateRequested;
    public FlowStateCallback OnFlowStateChanged;

    public void RaiseFlowStateRequest(FlowState flowState)
    {
        OnFlowStateRequested?.Invoke(flowState);
    }

    public void RaiseFlowStateChange(FlowState flowState)
    {
        OnFlowStateChanged?.Invoke(flowState);
    }
}