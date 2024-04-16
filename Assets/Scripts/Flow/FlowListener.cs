using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class FlowListenerEntry
{
    public FlowState flowState;
    public UnityEvent unityEvent;
}

class FlowListener : MonoBehaviour
{
    [SerializeField] FlowChannel flowChannel;
    [SerializeField] FlowListenerEntry[] flowListenerEntries;

    private readonly Dictionary<FlowState, UnityEvent> states = new();

    private void Awake()
    {
        foreach (var entry in flowListenerEntries) states.Add(entry.flowState, entry.unityEvent);
        flowChannel.OnFlowStateChanged += OnFlowStateChanged;
    }

    private void OnDestroy()
    {
        flowChannel.OnFlowStateChanged -= OnFlowStateChanged;
    }

    private void OnFlowStateChanged(FlowState flowState)
    {
        if (!states.ContainsKey(flowState)) return;
        states[flowState]?.Invoke();
    }
}