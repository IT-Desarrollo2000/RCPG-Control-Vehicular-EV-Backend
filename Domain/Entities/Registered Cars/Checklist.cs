namespace Domain.Entities.Registered_Cars
{
    public class Checklist : BaseEntity
    {
        public Checklist()
        {
            this.VehicleReportUses = new HashSet<VehicleReportUse>();
        }

        public int VehicleId { get; set; } 
        public bool CirculationCard { get; set; } = false;
        public bool CarInsurancePolicy { get; set; } = false;
        public bool HydraulicTires { get; set; }
        public bool TireRefurmishment { get; set; } = false;
        public bool JumperCable { get; set; } = false;
        public bool SecurityDice { get; set; } = false;
        public bool Extinguisher { get; set; } = false;
        public bool CarJack { get; set; } = false;
        public bool CarJackKey { get; set; } = false;
        public bool ToolBag { get; set; } = false;
        public bool SafetyTriangle { get; set; } = false;


        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<VehicleReportUse> VehicleReportUses { get; set; }
        public virtual ICollection<VehicleReportUse> InitialCheckListForUseReport { get; set; }

    }
}
