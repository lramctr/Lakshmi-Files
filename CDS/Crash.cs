using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Nps.Cds.DataModels.NpsCds
{
    [Table("ALL_CRASH")]
    public partial class Crash
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Crash()
        {
            Unit = new HashSet<Unit>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("OBJECTID")]
        public int ObjectId { get; set; }

        [Key]
        [Column("INCID_NO")]
        [StringLength(16)]
        [Display(Name = "Incident Num")]
        public string IncidentNo { get; set; }

        [Column("CASE_NUM")]
        [StringLength(10)]
        [Display(Name = "Case Number")]
        public string CaseNum { get; set; }

        [Required]
        [Column("PARK_ALPHA")]
        [StringLength(4)]
        [Display(Name = "Park Alpha")]
        public string ParkAlpha { get; set; }

        [Column("STATE_CODE")]
        [StringLength(2)]
        [Display(Name = "State")]
        public string StateCode { get; set; }

        [Column("CRASH_DATE", TypeName = "datetime2")]
        [Display(Name = "Crash Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime CrashDate { get; set; }

        [Column("CRASH_TIME")]
        [Display(Name = "Time (Mil)")]
        public short CrashTime { get; set; }

        [Column("RTE_NO")]
        [StringLength(15)]
        [Display(Name = "Route Num")]
        public string RouteNo { get; set; }

        [Column("RTE_NAME")]
        [StringLength(50)]
        [Display(Name = "Road Name")]
        public string RouteName { get; set; }

        [Column("NODE_DIST_FT", TypeName = "numeric")]
        [Display(Name = "Dist. to Node (Ft)")]
        public decimal? NodeDistFt { get; set; }

        [Column("NODE_DIST_MI", TypeName = "numeric")]
        [Display(Name = "Dist. to Node (Mi)")]
        public decimal? NodeDistMi { get; set; }

        [Column("NODE_DIR")]
        [StringLength(1)]
        [Display(Name = "Dir. to Node")]
        public string NodeDir { get; set; }

        [Column("NODE_NUM")]
        [StringLength(10)]
        [Display(Name = "Node#")]
        public string NodeNum { get; set; }

        [Column("LIGHT")]
        [StringLength(2)]
        [Display(Name = "01 Light")]
        public string LightCode { get; set; }

        [Column("WEATHER")]
        [StringLength(2)]
        [Display(Name = "02 Weather")]
        public string WeatherCode { get; set; }

        [Column("CRASH_LOCATION")]
        [StringLength(2)]
        [Display(Name = "03 Crash Location")]
        public string CrashLoc { get; set; }

        [Column("SURF_COND")]
        [StringLength(2)]
        [Display(Name = "04 Surface Condition")]
        public string SurfaceCond { get; set; }

        [Column("CRASH_CLASS")]
        [StringLength(2)]
        [Display(Name = "05 Crash Class")]
        public string CrashClassification { get; set; }

        [Column("VEH_COLL")]
        [StringLength(2)]
        [Display(Name = "06 Collision Bet Vehicles")]
        public string VehColl { get; set; }

        [Column("OBJ_STRUCK")]
        [StringLength(2)]
        [Display(Name = "07 Fixed Object Struck")]
        public string ObjStruck { get; set; }

        [Column("ROAD_CHAR")]
        [StringLength(2)]
        [Display(Name = "08 Road Character")]
        public string RoadChar { get; set; }

        [Column("CON_FACT1")]
        [StringLength(3)]
        [Display(Name = "Contributing Circumstances 1")]
        public string ConFact1 { get; set; }

        [Column("CON_FACT2")]
        [StringLength(3)]
        [Display(Name = "Contributing Circumstances 2")]
        public string ConFact2 { get; set; }

        [Column("CON_FACT3")]
        [StringLength(3)]
        [Display(Name = "Contributing Circumstances 3")]
        public string ConFact3 { get; set; }

        [Column("CON_FACT4")]
        [StringLength(3)]
        [Display(Name = "Contributing Circumstances 4")]
        public string ConFact4 { get; set; }

        [Column("CON_FACT5")]
        [StringLength(3)]
        [Display(Name = "Contributing Circumstances 5")]
        public string ConFact5 { get; set; }

        [Column("CON_FACT6")]
        [StringLength(3)]
        [Display(Name = "Contributing Circumstances 6")]
        public string ConFact6 { get; set; }

        [Column("HIT_RUN")]
        [Display(Name = "Hit and Run")]
        public bool? HitRun { get; set; }

        [Column("CATEGORY")]
        [StringLength(7)]
        public string Category { get; set; }

        [Column("FATALS")]
        public short? Fatals { get; set; }

        [Column("INJURED")]
        [Display(Name = "INJ.")]
        public short? Injured { get; set; }

        [Column("PED_FAT")]
        public short? PedFatility { get; set; }

        [Column("PED_INJ")]
        public short? PedInjury { get; set; }

        [Column("BIKE_FAT")]
        public short? BikeFatilty { get; set; }

        [Column("BIKE_INJ")]
        public short? BikeInjury { get; set; }

        [Column("PED")]
        [Display(Name = "PED?")]
        public bool? Pedestrian { get; set; }

        [Column("CRASH_YEAR")]
        [StringLength(4)]
        [Display(Name = "Year")]
        public string CrashYear { get; set; }

        [Column("COMMENTS")]
        [StringLength(250)]
        public string Comments { get; set; }

        [Column("ZIPFILE")]
        [StringLength(50)]
        public string ZipFile { get; set; }

        [Column("LOCATION")]
        [StringLength(100)]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Column("PHOTOS_TAKEN")]
        [Display(Name = "Photos Taken")]
        public bool? PhotosTaken { get; set; }

        [Column("USPP_NPS_VEH_INV")]
        [Display(Name = "USPP/NPS Veh. Inv.")]
        public bool? UsppNpsNehInv { get; set; }

        [Column("PARK_PTY_DEST")]
        [Display(Name = "Park Property Destroyed")]
        public bool? ParkPropertyDest { get; set; }

        [Column("LOCKED_UPDATE")]
        public bool LockedForUpdate { get; set; }

        [Column("LOCKED_BY_USER")]
        [StringLength(40)]
        public string LockedByUser { get; set; }

        [Required]
        [Column("DATA_SRC")]
        [StringLength(10)]
        [Display(Name = "Data Source")]
        public string DataSrc { get; set; }

        [Column("LATITUDE", TypeName = "numeric")]
        [Display(Name = "Latitude")]
        public decimal? Latitude { get; set; }

        [Column("LONGITUDE", TypeName = "numeric")]
        [Display(Name = "Longitude")]
        public decimal? Longitude { get; set; }

        [Column("MILEPOST", TypeName = "numeric")]
        [Display(Name = "Milepost")]
        public decimal? Milepost { get; set; }

        [Column("IMPORT_DATE", TypeName = "datetime2")]
        public DateTime? ImportDate { get; set; }

        [Column("FILE_NAME")]
        [StringLength(55)]
        public string FileName { get; set; }

        [Column("SAVE_DATE", TypeName = "datetime2")]
        [Display(Name = "Save Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? SaveDate { get; set; }

        [Column("ROUTE_IDENT")]
        [StringLength(15)]
        public string RouteIdent { get; set; }

        [Column("RIP_CYCLE")]
        [StringLength(2)]
        public string RipCycle { get; set; }

        [Column("MP_NODE", TypeName = "numeric")]
        public decimal? MpNode { get; set; }

        [Column("SPTL_LOC")]
        public bool SpatialLoc { get; set; }

        [ForeignKey("Category")]
        public virtual CrashCategory CrashCategory { get; set; }
        public virtual CrashCllass CrashClass { get; set; }
        public virtual CrashLocation CrashLocation { get; set; }
        public virtual ContFactor ContFactor1 { get; set; }
        public virtual ContFactor ContFactor2 { get; set; }
        public virtual ContFactor ContFactor3 { get; set; }
        public virtual ContFactor ContFactor4 { get; set; }
        public virtual ContFactor ContFactor5 { get; set; }
        public virtual ContFactor ContFactor6 { get; set; }
        public virtual DataSource DataSource { get; set; }
        public virtual Direction Direction { get; set; }
        public virtual Light Light { get; set; }
        public virtual NpsPark NpsPark { get; set; }
        public virtual NpsRoute Route { get; set; }
        public virtual ObjectStruck ObjectStruck { get; set; }
        public virtual RoadCharacter RoadCharacter { get; set; }
        public virtual SurfCondition SurfCondition { get; set; }
        public virtual UsState UsState { get; set; }
        public virtual VehCollision VehCollision { get; set; }
        public virtual Weather Weather { get; set; }
        public virtual Narrative Narrative { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Unit> Unit { get; set; }
    }
}
