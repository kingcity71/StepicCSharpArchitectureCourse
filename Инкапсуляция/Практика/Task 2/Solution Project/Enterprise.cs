using System;
using System.Linq;

namespace Incapsulation.EnterpriseTask
{
    public class Enterprise
    {
        private readonly Guid _guid;
        private string _inn;
        public Enterprise(Guid guid) => _guid = guid;
        public Guid Guid { get => _guid; }
        public string Name { get; set; }
        public string Inn
        {
            get => _inn;
            set => _inn = value.Length != 10 || !value.All(z => char.IsDigit(z))
                ? throw new ArgumentException()
                : value;
        }
        public DateTime EstablishDate { get; set; }
        public TimeSpan ActiveTimeSpan { get => DateTime.Now - EstablishDate; }

        public double GetTotalTransactionsAmount()
        {
            DataBase.OpenConnection();
            var amount = 0.0;
            foreach (Transaction t in DataBase.Transactions().Where(z => z.EnterpriseGuid == _guid))
                amount += t.Amount;
            return amount;
        }
    }
}
