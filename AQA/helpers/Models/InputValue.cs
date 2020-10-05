namespace AQA.helpers
{
    public class InputValue
    {
       public decimal OpenPrice { get; set; }
       public decimal ClosePrice{ get; set; }
       public decimal Volume{ get; set; }
       public decimal ContractSize{ get; set; }
       public decimal Leverage{ get; set; }
       public decimal Commission{ get; set; }
       public string CommissionType{ get; set; }
    }
}