using EnhancedDevelopment.Excalibur.Settings;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Excalibur.NanoShields
{
    [StaticConstructorOnStartup]
    internal class Gizmo_NanoShieldStatus : Gizmo
    {
        public Gizmo_NanoShieldStatus(CompNanoShield QuantumShield)
        {
            this.QuantumShield = QuantumShield;
        }

        public CompNanoShield QuantumShield;

        private static readonly Texture2D FullShieldBarTex = SolidColorMaterials.NewSolidColorTexture(new Color(0.2f, 0.2f, 0.24f));

        private static readonly Texture2D EmptyShieldBarTex = SolidColorMaterials.NewSolidColorTexture(Color.clear);

        public override float GetWidth(float maxWidth)
        {
            if (maxWidth < 140f)
            {
                return maxWidth;
            }
            else
            {
                return 140f;
            }
        }

        public override GizmoResult GizmoOnGUI(Vector2 topLeft, float maxWidth)
        {
            Rect overRect = new Rect(topLeft.x, topLeft.y, this.GetWidth(maxWidth), 75f);
            Find.WindowStack.ImmediateWindow(984688, overRect, WindowLayer.GameUI, delegate
            {
                Rect rect = overRect.AtZero().ContractedBy(6f);
                Rect rect2 = rect;
                rect2.height = overRect.height / 2f;
                Text.Font = GameFont.Tiny;
                Widgets.Label(rect2, "Nano Shield Status");
                Rect rect3 = rect;
                rect3.yMin = overRect.height / 2f;
                float fillPercent = Mathf.Min(1f, (float)(Math.Max(1, this.QuantumShield.QuantumShieldChargeLevelCurrent)) / (float)Mod_EDExcalibur.Settings.NanoShields.ShieldChargeLevelMax);
                //Log.Message("Fill: " + fillPercent);
                Widgets.FillableBar(rect3, fillPercent, Gizmo_NanoShieldStatus.FullShieldBarTex, Gizmo_NanoShieldStatus.EmptyShieldBarTex, false);
                Text.Font = GameFont.Small;
                Text.Anchor = TextAnchor.MiddleCenter;
                Widgets.Label(rect3, (this.QuantumShield.QuantumShieldChargeLevelCurrent).ToString("F0") + " / " + (Mod_EDExcalibur.Settings.NanoShields.ShieldChargeLevelMax).ToString("F0"));
                Text.Anchor = TextAnchor.UpperLeft;
            }, true, false, 1f);
            return new GizmoResult(GizmoState.Clear);
        }
    }
}
