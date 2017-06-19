using System.Collections.Generic;

namespace SellYourCar.Entities
{
    public class ModelCar
    {
        public int Id {get; set; }
        public string Name {get; set;}
        //needed RowVersion here too beacuse we could have many Admins
        public byte[] RowVersion {get; set;}
        public virtual MakeCar Make {get; set; }
        //instead of newing in the constructor newing it here
        public virtual ICollection<Advertise> Advertises {get; set;} = new List<Advertise>();
    }
}