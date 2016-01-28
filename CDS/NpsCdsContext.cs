using System.Data.Entity;

namespace Nps.Cds.DataModels.NpsCds
{
    public partial class NpsCdsContext : DbContext
    {
        public NpsCdsContext()
            : base("name=NpsCdsContext")
        {
        }

        public virtual DbSet<Belt> BeltCodes { get; set; }
        public virtual DbSet<CrashCategory> CrashCategories { get; set; }
        public virtual DbSet<CrashCllass> CrashClasses { get; set; }
        public virtual DbSet<Crash> Crashes { get; set; }
        public virtual DbSet<CrashLocation> CrashLocations { get; set; }
        public virtual DbSet<ContFactCategory> ConFactCategories { get; set; }
        public virtual DbSet<ContFactor> ConFactCodes { get; set; }
        public virtual DbSet<DataSource> DataSources { get; set; }
        public virtual DbSet<DamageLoc> DamageLocs { get; set; }
        public virtual DbSet<Direction> Directions { get; set; }
        public virtual DbSet<DriverViolation> DriverViolations { get; set; }
        public virtual DbSet<EjectCodes> EjectCodes { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Injury> Injury { get; set; }
        public virtual DbSet<Light> Light { get; set; }
        public virtual DbSet<Narrative> Narratives { get; set; }
        public virtual DbSet<NpsNode> NpsNodes { get; set; }
        public virtual DbSet<NpsPark> NpsParks { get; set; }
        public virtual DbSet<NpsRoute> NpsRoutes { get; set; }
        public virtual DbSet<ObjectStruck> ObjectStruck { get; set; }
        public virtual DbSet<Passenger> Passengers { get; set; }
        public virtual DbSet<PedAction> PedActions { get; set; }
        public virtual DbSet<PedLocation> PedLocations { get; set; }
        public virtual DbSet<PedType> PedTypes { get; set; }
        public virtual DbSet<RoadCharacter> RoadCharacters { get; set; }
        public virtual DbSet<Seat> Seat { get; set; }
        public virtual DbSet<SpeedLimit> SpeedLimits { get; set; }
        public virtual DbSet<SurfCondition> SurfConditions { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<UsState> UsStates { get; set; }
        public virtual DbSet<VehBodyType> VehBodyTypes { get; set; }
        public virtual DbSet<VehCollision> VehCollisions { get; set; }
        public virtual DbSet<VehDamage> VehDamages { get; set; }
        public virtual DbSet<VehMake> VehMakes { get; set; }
        public virtual DbSet<VehManeuverCodes> VehManeuverCodes { get; set; }
        public virtual DbSet<VehModel> VehModels { get; set; }
        public virtual DbSet<ViolationCharge> ViolationCharges { get; set; }
        public virtual DbSet<Weather> Weather { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CrashCategory>()
                .HasMany(e => e.Crashes)
                .WithOptional(e => e.CrashCategory)
                .HasForeignKey(e => e.Category);

            modelBuilder.Entity<CrashCllass>()
                .HasMany(e => e.Crashes)
                .WithOptional(e => e.CrashClass)
                .HasForeignKey(e => e.CrashClassification);

            modelBuilder.Entity<CrashLocation>()
                .HasMany(e => e.Crashes)
                .WithOptional(e => e.CrashLocation)
                .HasForeignKey(e => e.CrashLoc);

            modelBuilder.Entity<Crash>()
                .Property(e => e.NodeDistFt)
                .HasPrecision(7, 3);

            modelBuilder.Entity<Crash>()
                .Property(e => e.NodeDistMi)
                .HasPrecision(7, 3);

            modelBuilder.Entity<Crash>()
                .Property(e => e.Latitude)
                .HasPrecision(9, 6);

            modelBuilder.Entity<Crash>()
                .Property(e => e.Longitude)
                .HasPrecision(9, 6);

            modelBuilder.Entity<Crash>()
                .Property(e => e.Milepost)
                .HasPrecision(7, 3);

            modelBuilder.Entity<Crash>()
                .Property(e => e.MpNode)
                .HasPrecision(7, 3);

            modelBuilder.Entity<Crash>()
                .HasOptional(e => e.Narrative)
                .WithRequired(e => e.Crash);

            modelBuilder.Entity<Crash>()
                .HasMany(e => e.Unit)
                .WithRequired(e => e.Crash)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Unit>()
                .Property(e => e.Repair)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.Passengers)
                .WithRequired(e => e.Unit)
                .HasForeignKey(e => new { e.IncidentNo, e.UnitNo })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Belt>()
                .HasMany(e => e.Passengers)
                .WithOptional(e => e.Belt)
                .HasForeignKey(e => e.PassBelt);

            modelBuilder.Entity<Belt>()
                .HasMany(e => e.Units)
                .WithOptional(e => e.BeltCode)
                .HasForeignKey(e => e.DriverBelt);

            modelBuilder.Entity<ContFactCategory>()
                .HasMany(e => e.ConFactCodes)
                .WithRequired(e => e.ConFactCategory)
                .HasForeignKey(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContFactor>()
                .HasMany(e => e.CrashesCF1)
                .WithOptional(e => e.ContFactor1)
                .HasForeignKey(e => e.ConFact1);

            modelBuilder.Entity<ContFactor>()
                .HasMany(e => e.CrashesCF2)
                .WithOptional(e => e.ContFactor2)
                .HasForeignKey(e => e.ConFact2);

            modelBuilder.Entity<ContFactor>()
                .HasMany(e => e.CrashesCF3)
                .WithOptional(e => e.ContFactor3)
                .HasForeignKey(e => e.ConFact3);

            modelBuilder.Entity<ContFactor>()
                .HasMany(e => e.CrashesCF4)
                .WithOptional(e => e.ContFactor4)
                .HasForeignKey(e => e.ConFact4);

            modelBuilder.Entity<ContFactor>()
                .HasMany(e => e.CrashesCF5)
                .WithOptional(e => e.ContFactor5)
                .HasForeignKey(e => e.ConFact5);

            modelBuilder.Entity<ContFactor>()
                .HasMany(e => e.CrashesCF6)
                .WithOptional(e => e.ContFactor6)
                .HasForeignKey(e => e.ConFact6);

            modelBuilder.Entity<DamageLoc>()
                .HasMany(e => e.Units)
                .WithOptional(e => e.DamageLoc)
                .HasForeignKey(e => e.DamageLocation);

            modelBuilder.Entity<DataSource>()
                .HasMany(e => e.Crashes)
                .WithRequired(e => e.DataSource)
                .HasForeignKey(e => e.DataSrc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Direction>()
                .HasMany(e => e.Crashes)
                .WithOptional(e => e.Direction)
                .HasForeignKey(e => e.NodeDir);

            modelBuilder.Entity<Direction>()
                .HasMany(e => e.Units)
                .WithOptional(e => e.Direction)
                .HasForeignKey(e => e.DirTravel);

            modelBuilder.Entity<DriverViolation>()
                .HasMany(e => e.Unit)
                .WithOptional(e => e.DriverViolation)
                .HasForeignKey(e => e.DriverViolationCode);

            modelBuilder.Entity<EjectCodes>()
                .HasMany(e => e.Passengers)
                .WithOptional(e => e.EjectCode)
                .HasForeignKey(e => e.PassEject);

            modelBuilder.Entity<EjectCodes>()
                .HasMany(e => e.Units)
                .WithOptional(e => e.EjectCode)
                .HasForeignKey(e => e.DriverEject);

            modelBuilder.Entity<Gender>()
                .HasMany(e => e.Passengers)
                .WithOptional(e => e.Gender)
                .HasForeignKey(e => e.PassSex);

            modelBuilder.Entity<Gender>()
                .HasMany(e => e.Units)
                .WithOptional(e => e.GenderCode)
                .HasForeignKey(e => e.DriverSex);

            modelBuilder.Entity<Injury>()
                .HasMany(e => e.Passengers)
                .WithOptional(e => e.Injury)
                .HasForeignKey(e => e.PassInjury);

            modelBuilder.Entity<Injury>()
                .HasMany(e => e.Units)
                .WithOptional(e => e.Injury)
                .HasForeignKey(e => e.DriverInjury);

            modelBuilder.Entity<Light>()
                .HasMany(e => e.Crashes)
                .WithOptional(e => e.Light)
                .HasForeignKey(e => e.LightCode);

            modelBuilder.Entity<NpsNode>()
                .Property(e => e.MpNode)
                .HasPrecision(7, 3);

            modelBuilder.Entity<NpsPark>()
                .HasMany(e => e.Crashes)
                .WithRequired(e => e.NpsPark)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NpsPark>()
                .HasMany(e => e.Nodes)
                .WithRequired(e => e.Park)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NpsPark>()
                .HasMany(e => e.Routes)
                .WithRequired(e => e.Park)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NpsRoute>()
                .Property(e => e.PavedMiles)
                .HasPrecision(7, 3);

            modelBuilder.Entity<NpsRoute>()
                .Property(e => e.UnpavedMiles)
                .HasPrecision(7, 3);

            modelBuilder.Entity<ObjectStruck>()
                .HasMany(e => e.Crashes)
                .WithOptional(e => e.ObjectStruck)
                .HasForeignKey(e => e.ObjStruck);

            modelBuilder.Entity<PedAction>()
                .HasMany(e => e.Units)
                .WithOptional(e => e.PedAction)
                .HasForeignKey(e => e.PedAct);

            modelBuilder.Entity<PedLocation>()
                .HasMany(e => e.Units)
                .WithOptional(e => e.PedLocation)
                .HasForeignKey(e => e.PedLoc);

            modelBuilder.Entity<PedType>()
                .HasMany(e => e.Units)
                .WithOptional(e => e.PedType)
                .HasForeignKey(e => e.PedestyrianType);

            modelBuilder.Entity<RoadCharacter>()
                .HasMany(e => e.Crashes)
                .WithOptional(e => e.RoadCharacter)
                .HasForeignKey(e => e.RoadChar);

            modelBuilder.Entity<Seat>()
                .HasMany(e => e.Passengers)
                .WithOptional(e => e.Seat)
                .HasForeignKey(e => e.PassSeat);

            modelBuilder.Entity<SpeedLimit>()
                .HasMany(e => e.Units)
                .WithOptional(e => e.SpeedLimit)
                .HasForeignKey(e => e.Speed);

            modelBuilder.Entity<SurfCondition>()
                .HasMany(e => e.Crashes)
                .WithOptional(e => e.SurfCondition)
                .HasForeignKey(e => e.SurfaceCond);

            modelBuilder.Entity<UsState>()
                .HasMany(e => e.UnitsRegState)
                .WithOptional(e => e.UsStateReg)
                .HasForeignKey(e => e.RegState);

            modelBuilder.Entity<UsState>()
                .HasMany(e => e.UnitsLicState)
                .WithOptional(e => e.UsStateLic)
                .HasForeignKey(e => e.LicState);

            modelBuilder.Entity<UsState>()
                .HasMany(e => e.RouteState1)
                .WithRequired(e => e.UsState1)
                .HasForeignKey(e => e.State1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UsState>()
                .HasMany(e => e.RoutesState2)
                .WithOptional(e => e.UsState2)
                .HasForeignKey(e => e.State2);

            modelBuilder.Entity<VehBodyType>()
                .HasMany(e => e.Units)
                .WithOptional(e => e.VehBodyType)
                .HasForeignKey(e => e.BodyType);

            modelBuilder.Entity<VehCollision>()
                .HasMany(e => e.Crashes)
                .WithOptional(e => e.VehCollision)
                .HasForeignKey(e => e.VehColl);

            modelBuilder.Entity<VehDamage>()
                .HasMany(e => e.Units)
                .WithOptional(e => e.VehDamage)
                .HasForeignKey(e => e.VehicleDamage);

            modelBuilder.Entity<VehMake>()
                .HasMany(e => e.VehModels)
                .WithRequired(e => e.VehMake)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VehManeuverCodes>()
                .HasMany(e => e.Units)
                .WithOptional(e => e.VehManeuverCode)
                .HasForeignKey(e => e.VehManeuver);

            modelBuilder.Entity<ViolationCharge>()
                .HasMany(e => e.UnitsVC1)
                .WithOptional(e => e.ViolationCharge1)
                .HasForeignKey(e => e.ViolCharge1);

            modelBuilder.Entity<ViolationCharge>()
                .HasMany(e => e.UnitsVC2)
                .WithOptional(e => e.ViolationCharge2)
                .HasForeignKey(e => e.ViolCharge2);

            modelBuilder.Entity<Weather>()
                .HasMany(e => e.Crashes)
                .WithOptional(e => e.Weather)
                .HasForeignKey(e => e.WeatherCode);
        }
    }
}
