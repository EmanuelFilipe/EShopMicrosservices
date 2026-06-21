namespace Ordering.Infrastructure.Data.Extensions;

public static class InitialData
{
    public static IEnumerable<Customer> Customers => new List<Customer>
    {
        //aa
        Customer.Create(CustomerId.Of(new Guid("0dd2c00c-a51b-4825-806d-81f6309fe1e6")), "mehmet", "mehmet@gmail.com"),
        //7d
        Customer.Create(CustomerId.Of(new Guid("a449a0fe-753a-415b-ac1b-c0430f84ea49")), "john", "john@gmail.com")
    };

    public static IEnumerable<Product> Products => new List<Product>
    {
        //61
        Product.Create(ProductId.Of(new Guid("8443c2e2-01d0-41af-9988-4c9c6e8a2e39")), "IPhone X", 500),
        //14
        Product.Create(ProductId.Of(new Guid("10ab9c5b-ebb8-43d1-a5b7-afb94ff882f2")), "Samsung 10", 400),
        //b8
        Product.Create(ProductId.Of(new Guid("34b012bc-25d3-4e96-833d-9c262f7c6aa2")), "Hauwei Plus", 650),
        //27
        Product.Create(ProductId.Of(new Guid("dd7ac558-2705-4383-9fe8-85d9fc096e1a")), "Xiomi Mi", 450)
    };

    public static IEnumerable<Order> OrdersWithItems 
    {
        get 
        {
            var address1 = Address.Of("mehmet", "ozkaya", "mehmet@gmail.com", "Bahcelievler Nº 4", "Turkey", "Istambul", "38050");
            var address2 = Address.Of("john", "doe", "john@gmail.com", "Brodway nº 1", "England", "Nottingham", "08050");

            var payment1 = Payment.Of("mehmet", "555555555544444", "12/28", "355", 1);
            var payment2 = Payment.Of("john", "888555555555544", "06/30", "222", 2);

            var order1 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("0dd2c00c-a51b-4825-806d-81f6309fe1e6")),
                    OrderName.Of("ORD_1"),
                    shippingAddress: address1,
                    billingAddress: address1,
                    payment1);

            order1.Add(ProductId.Of(new Guid("8443c2e2-01d0-41af-9988-4c9c6e8a2e39")), 2, 500);
            order1.Add(ProductId.Of(new Guid("10ab9c5b-ebb8-43d1-a5b7-afb94ff882f2")), 1, 400);

            var order2 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("a449a0fe-753a-415b-ac1b-c0430f84ea49")),
                    OrderName.Of("ORD_2"),
                    shippingAddress: address2,
                    billingAddress: address2,
                    payment2);

            order2.Add(ProductId.Of(new Guid("34b012bc-25d3-4e96-833d-9c262f7c6aa2")), 1, 650);
            order2.Add(ProductId.Of(new Guid("dd7ac558-2705-4383-9fe8-85d9fc096e1a")), 2, 450);

            return new List<Order> { order1, order2 };
        }
    }
}
