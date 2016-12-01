using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOrganizer.Classes
{
    class TyresSupplier
    {
        public int Id { get; set; }
        public TyreSuppliers Supplier { get; set; }
        public int DryPerformance { get; set; }
        public int WetPerformance {get; set; }
        public int PeakTemperature {get; set; }
        public int Durability {get; set; }
        public int WarmUpDistance {get; set; }
        public int CostPerRace {get; set; }
        public float TdcVariable { get; set; }

        public void LoadTyre(TyreSuppliers supplier)
        {
            int id = (int)supplier;
            var tyre = DB.Tyres.ReadTyreFromSupplierDB(id);
            Id = tyre.Id;
            Supplier = (TyreSuppliers)Id;
            DryPerformance = tyre.DryPerformance;
            WetPerformance = tyre.WetPerformance;
            PeakTemperature = tyre.PeakTemperature;
            Durability = tyre.Durability;
            WarmUpDistance = tyre.WarmUpDistance;
            CostPerRace = tyre.CostPerRace;
            TdcVariable = tyre.TdcVariable;
        }
        public string GetActiveSupplierName()
        {
            string supplierName = "Error";

            TyreSuppliers supplier = (TyreSuppliers)Supplier;

            switch (supplier)
            {
                case TyreSuppliers.Pipirelli:
                    supplierName = "Pipirelli";
                    break;
                case TyreSuppliers.Avonn:
                    supplierName = "Avonn";
                    break;
                case TyreSuppliers.Yokomama:
                    supplierName = "Yokomama";
                    break;
                case TyreSuppliers.Dunnolop:
                    supplierName = "Dunnolop";
                    break;
                case TyreSuppliers.Contimental:
                    supplierName = "Contimental";
                    break;
                case TyreSuppliers.Badyear:
                    supplierName = "Badyear";
                    break;
                case TyreSuppliers.Hancock:
                    supplierName = "Hancock";
                    break;
                case TyreSuppliers.Michelini:
                    supplierName = "Michelini";
                    break;
                case TyreSuppliers.Bridgerock:
                    supplierName = "Bridgerock";
                    break;
            }

            return supplierName;
        }

        public int GetActiveSupplierCost()
        {
            int cost = Constants.GetSupplierCost((TyreSuppliers)Supplier);

            return cost;
        }
        public int GetSupplierCodeFromName(string name)
        {
            int code = 0;

            if(name!="")
                code = (int)Enum.Parse(typeof(TyreSuppliers), name);

            return code;
        }
    }

    enum Compound
    {
        XSoft,
        Soft,
        Medium,
        Hard,
        Rain
    }
    enum TyreSuppliers
    {
        Pipirelli,
        Avonn,
        Yokomama,
        Dunnolop,
        Contimental,
        Badyear, 
        Hancock,
        Michelini,
        Bridgerock
    }
}
