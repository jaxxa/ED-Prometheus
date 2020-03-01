using EnhancedDevelopment.Prometheus.Core;
using EnhancedDevelopment.Prometheus.Settings;
using Jaxxa.EnhancedDevelopment.Core.Comp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using static EnhancedDevelopment.Prometheus.Core.GameComponent_Prometheus_Quest;

namespace EnhancedDevelopment.Prometheus.LaserDrill
{
    public class Comp_LaserDrillRequiresPrometheus : ThingComp, Jaxxa.EnhancedDevelopment.Core.Comp.Interface.IRequiresShipResources
    {

        int m_ShipPowerRequired = 900;

        private bool HasEnoughResources()
        {
            return GameComponent_Prometheus.Instance.Comp_Quest.ResourceGetReserveStatus(EnumResourceType.Power) >= this.m_ShipPowerRequired;
        }

        bool IRequiresShipResources.Satisfied
        {
            get
            {
                return this.HasEnoughResources();
            }
        }

        string IRequiresShipResources.StatusString
        {
            get
            {
                return "Power: " + GameComponent_Prometheus.Instance.Comp_Quest.ResourceGetReserveStatus(EnumResourceType.Power).ToString() + " / " + this.m_ShipPowerRequired.ToString();
            }
        }

        bool IRequiresShipResources.UseResources()
        {
            if (this.HasEnoughResources())
            {
                GameComponent_Prometheus.Instance.Comp_Quest.ResourceRequestReserve(EnumResourceType.Power, this.m_ShipPowerRequired);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
