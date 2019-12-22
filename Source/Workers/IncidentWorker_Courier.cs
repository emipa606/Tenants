﻿using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Tenants {
    public class IncidentWorker_TenantCourier : IncidentWorker {
        protected override bool CanFireNowSub(IncidentParms parms) {
            if (!base.CanFireNowSub(parms)) {
                return false;
            }
            if (parms.target != null) {
                Map map = (Map)parms.target;
                List<Map> maps = Find.Maps.Where(x => x.IsPlayerHome).ToList();
                if (map != null && maps.Contains(map)) {
                    return Utility.TryFindSpawnSpot(map, out IntVec3 spawnSpot);
                }
            }
            return false;
        }
        protected override bool TryExecuteWorker(IncidentParms parms) {
            if (parms.target != null) {
                Map map = (Map)parms.target;
                if (map != null) {
                    Building building = map.listerBuildings.allBuildingsColonist.FirstOrDefault(x => x.def == ThingDefOf.Tenants_MailBox);
                    if (building != null) {
                        return UtilityCourier.Courier((Map)parms.target, building);
                    }
                    else {
                        string letterLabel = "CourierArrivedTitle".Translate((map.Parent.Label));
                        string letterText = "CourierMiss".Translate();
                        Find.LetterStack.ReceiveLetter(letterLabel, letterText, LetterDefOf.NeutralEvent);
                    }
                }
            }
            return false;
        }
    }
}