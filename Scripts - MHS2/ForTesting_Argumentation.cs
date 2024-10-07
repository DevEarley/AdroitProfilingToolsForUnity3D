using UnityEngine;

namespace MHS
{
    public class ForTesting_Argumentation : MonoBehaviour
    {
        private AdroitProfiler_Heartbeat ProfilerHeartbeat;
        public string OnStart_SignalMessage = "ForTesting_";
        public string OnEnd_SignalMessage = "ForTesting_";

        private void Start()
        {
            ProfilerHeartbeat = FindAnyObjectByType<AdroitProfiler_Heartbeat>();
        }

        public void Argumentation_OnStart()
        {
            if (ProfilerHeartbeat != null)
            {
                ProfilerHeartbeat.Signal(OnStart_SignalMessage);
            }
        }

        public void Argumentation_OnEnd()
        {
            if (ProfilerHeartbeat != null)
            {
                ProfilerHeartbeat.Signal(OnEnd_SignalMessage);
            }

        }
    }
}
