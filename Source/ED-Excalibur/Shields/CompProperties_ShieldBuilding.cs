using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Shields
{
    class CompProperties_ShieldBuilding : CompProperties
    {

        public CompProperties_ShieldBuilding()
        {
            this.compClass = typeof(Comp_ShieldBuilding);
        }

        public int m_FieldIntegrity_Max = 0;
        public int m_FieldIntegrity_Initial = 0;
        public int m_Field_Radius = 0;

        public int m_PowerRequiredCharging = 0;
        public int m_PowerRequiredSustaining = 0;

        public int m_RechargeTickDelayInterval = 0;
        public int m_RecoverWarmupDelayTicks = 0;

        public bool m_BlockIndirect_Avalable = false;
        public bool m_BlockDirect_Avalable = false;
        public bool m_FireSupression_Avalable = false;
        public bool m_InterceptDropPod_Avalable = false;

        public bool m_StructuralIntegrityMode = false;

        public float m_ColourRed = 0.0f;
        public float m_ColourGreen = 0.0f;
        public float m_ColourBlue = 0.0f;

        public List<string> SIFBuildings = new List<string>();
    }
}
