using ProjetoTeste.Arguments.Arguments.Base;

namespace ProjetoTeste.Arguments.Arguments.Order
{
    public class OrderValidate : BaseValidate
    {
        public InputCreateOrder InputCreateOrder { get; private set; }
        public long ExistingCustomer { get; private set; }

        public OrderValidate CreateOrder(InputCreateOrder inputCreateOrder, long existingCustomer)
        {
            InputCreateOrder = inputCreateOrder;
            ExistingCustomer = existingCustomer;
            return this;
        }
    }
}
