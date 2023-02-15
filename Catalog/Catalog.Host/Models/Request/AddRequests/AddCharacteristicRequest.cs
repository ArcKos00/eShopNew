namespace Catalog.Host.Models.Request.AddRequests
{
    public class AddCharacteristicRequest
    {
        [Range(1, int.MaxValue)]
        public int Radiation { get; set; }

        [Range(1, int.MaxValue)]
        public int Restoration { get; set; }

        [Range(1, int.MaxValue)]
        public int RestorationHealth { get; set; }

        [Range(1, int.MaxValue)]
        public int WoundHealing { get; set; }

        [Range(1, int.MaxValue)]
        public int MaximumWeight { get; set; }

        [Range(1, int.MaxValue)]
        public int ProtectionDogs { get; set; }

        public int ThermalProtection { get; set; }

        [Range(1, int.MaxValue)]
        public int ChemicalProtection { get; set; }

        [Range(1, int.MaxValue)]
        public int ElectricalProtection { get; set; }

        [Range(1, int.MaxValue)]
        public int Saturation { get; set; }
    }
}
