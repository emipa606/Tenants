﻿using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Tenants {
    public class MapComponent_Tenants : MapComponent {
        #region Fields
        private float karma;
        private bool broadcast = true;
        private List<Pawn> deadTenantsToAvenge = new List<Pawn>();
        private List<Pawn> capturedTenantsToAvenge = new List<Pawn>();
        private List<Pawn> moles = new List<Pawn>();
        #endregion Fields
        #region Properties
        public List<Pawn> DeadTenantsToAvenge {
            get {
                if (deadTenantsToAvenge == null) { deadTenantsToAvenge = new List<Pawn>(); }
                return deadTenantsToAvenge;
            }
        }
        public List<Pawn> CapturedTenantsToAvenge {
            get {
                if (capturedTenantsToAvenge == null) { capturedTenantsToAvenge = new List<Pawn>(); }
                return capturedTenantsToAvenge;
            }
        }
        public List<Pawn> Moles {
            get {
                if (moles == null) { moles = new List<Pawn>(); }
                return moles;
            }
        }
        public float Karma { get { return karma; } set { karma = value; } }
        public bool Broadcast { get { return broadcast; } set { broadcast = value; } }
        #endregion Properties
        #region Constructors
        public MapComponent_Tenants(Map map)
            : base(map) {
        }
        public MapComponent_Tenants(bool generateComponent, Map map)
            : base(map) {
            if (generateComponent) {
                map.components.Add(this);
            }
        }
        #endregion Constructors
        #region Methods
        public static MapComponent_Tenants GetComponent(Map map) {
            return map.GetComponent<MapComponent_Tenants>() ?? new MapComponent_Tenants(generateComponent: true, map);
        }
        public override void ExposeData() {
            Scribe_Collections.Look(ref deadTenantsToAvenge, "DeadTenants", LookMode.Deep);
            Scribe_Collections.Look(ref capturedTenantsToAvenge, "CapturedTenants", LookMode.Deep);
            Scribe_Collections.Look(ref moles, "Moles", LookMode.Deep);
            Scribe_Values.Look(ref karma, "Karma");
            Scribe_Values.Look(ref broadcast, "Broadcast");
        }
        #endregion Methods
    }
}
