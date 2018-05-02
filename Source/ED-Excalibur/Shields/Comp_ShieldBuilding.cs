using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using UnityEngine;

namespace EnhancedDevelopment.Excalibur.Shields
{

    [StaticConstructorOnStartup]
    public class Comp_ShieldBuilding : ThingComp
    {

        Material currentMatrialColour;

        public override void PostDraw()
        {
            Log.Message("DrawComp");
            base.PostDraw();

            this.DrawShields();


        }



        /// <summary>
        /// Draw the shield Field
        /// </summary>
        public void DrawShields()
        {
            //if (!this.IsActive() || !this.m_ShowVisually_Active)
            //{
            //    return;
            //}
            
            //Draw field
            this.DrawField(EnhancedDevelopment.Excalibur.Shields.Utilities.VectorsUtils.IntVecToVec(this.parent.Position));

        }

        //public override void DrawExtraSelectionOverlays()
        //{
        //    //    GenDraw.DrawRadiusRing(base.Position, shieldField.shieldShieldRadius);
        //}

        public void DrawSubField(IntVec3 center, float radius)
        {
            this.DrawSubField(EnhancedDevelopment.Excalibur.Shields.Utilities.VectorsUtils.IntVecToVec(center), radius);
        }

        //Draw the field on map
        public void DrawField(Vector3 center)
        {
            DrawSubField(center, 10);
            //DrawSubField(center, m_Field_Radius);
        }

        public void DrawSubField(Vector3 position, float shieldShieldRadius)
        {
            position = position + (new Vector3(0.5f, 0f, 0.5f));

            Vector3 s = new Vector3(shieldShieldRadius, 1f, shieldShieldRadius);
            Matrix4x4 matrix = default(Matrix4x4);
            matrix.SetTRS(position, Quaternion.identity, s);

            if (currentMatrialColour == null)
            {
                //Log.Message("Creating currentMatrialColour");
               // currentMatrialColour = SolidColorMaterials.NewSolidColorMaterial(new Color(m_ColourRed, m_ColourGreen, m_ColourBlue, 0.15f), ShaderDatabase.MetaOverlay);
                currentMatrialColour = SolidColorMaterials.NewSolidColorMaterial(new Color(0.5f, 0.0f, 0.0f, 0.15f), ShaderDatabase.MetaOverlay);
            }

            UnityEngine.Graphics.DrawMesh(EnhancedDevelopment.Excalibur.Shields.Utilities.Graphics.CircleMesh, matrix, currentMatrialColour, 0);

        }
    }
}
