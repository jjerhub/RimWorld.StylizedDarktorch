using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using UnityEngine;

namespace jjerhub.StylizedDarktorch
{
    [StaticConstructorOnStartup]
    public class CompDarklightOverlay : CompFireOverlayBase
    {
        ///Based off of <see cref="RimWorld.CompDarklightOverlay" /> and <see cref="CompFireOverlay"/>
        protected CompRefuelable refuelableComp;
        public static readonly Graphic DarklightGraphic = GraphicDatabase.Get<Graphic_Flicker>("Things/Special/Darklight", ShaderDatabase.TransparentPostLight, Vector2.one, Color.white);
        public new CompProperties_DarklightOverlay Props => (CompProperties_DarklightOverlay)props;

        public override void PostDraw()
        {
            base.PostDraw();
            if (refuelableComp == null || refuelableComp.HasFuel)
            {
                Vector3 drawPos = parent.DrawPos;
                drawPos.y += 1f / 26f;
                DarklightGraphic.Draw(drawPos, parent.Rotation, parent);
            }
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            refuelableComp = parent.GetComp<CompRefuelable>();
        }

        public override void CompTickInterval(int delta)
        {
            if ((refuelableComp == null || refuelableComp.HasFuel) && startedGrowingAtTick < 0)
            {
                startedGrowingAtTick = GenTicks.TicksAbs;
            }
        }
    }

    public class CompProperties_DarklightOverlay : CompProperties_FireOverlay
    {
        public CompProperties_DarklightOverlay() { compClass = typeof(CompDarklightOverlay); }
    }
}