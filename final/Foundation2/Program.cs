using System;


class Program
{
    static void Main(string[] args)
    {
        
        Product product1 = new Product("Laptop", "P001", 899.99, 2);
        Product product2 = new Product("Mouse", "P002", 19.99, 3);
        Product product3 = new Product("Keyboard", "P003", 49.99, 1);

        
        Address customerAddress = new Address("145 Spring St", "Charlotte", "NC", "USA");

        
        Customer customer = new Customer("Musa Camervienga", customerAddress);

        
        Order order1 = new Order(customer);
        order1.AddProduct(product1);
        order1.AddProduct(product2);
        order1.AddProduct(product3);

        
        Console.WriteLine("Order 1 Packing Label:");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine("\nOrder 1 Shipping Label:");
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine("\nOrder 1 Total Price: $" + order1.GetTotalPrice());

        
        Product product4 = new Product("Headphones", "P004", 79.99, 1);
        Product product5 = new Product("Monitor", "P005", 249.99, 2);

        
        Order order2 = new Order(new Customer("Jaden Smith", new Address("450 Montreal St", "Pratville", "Al", "USA")));
        order2.AddProduct(product4);
        order2.AddProduct(product5);

        
        Console.WriteLine("\nOrder 2 Packing Label:");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine("\nOrder 2 Shipping Label:");
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine("\nOrder 2 Total Price: $" + order2.GetTotalPrice());
    }
}

class Product
{
    public string Name { get; private set; }
    public string ProductId { get; private set; }
    public double Price { get; private set; }
    public int Quantity { get; private set; }

    public Product(string name, string productId, double price, int quantity)
    {
        Name = name;
        ProductId = productId;
        Price = price;
        Quantity = quantity;
    }

    public double GetTotalPrice()
    {
        return Price * Quantity;
    }
}

class Customer
{
    public string Name { get; private set; }
    public Address Address { get; private set; }

    public Customer(string name, Address address)
    {
        Name = name;
        Address = address;
    }

    public bool IsInUSA()
    {
        return Address.IsInUSA();
    }
}

class Address
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }

    public Address(string street, string city, string state, string country)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
    }

    public bool IsInUSA()
    {
        return Country.ToUpper() == "USA";
    }

    public string GetFullAddress()
    {
        return $"{Street}, {City}, {State}, {Country}";
    }
}

class Order
{
    private List<Product> Products { get; set; }
    private Customer Customer { get; set; }

    public Order(Customer customer)
    {
        Products = new List<Product>();
        Customer = customer;
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public double GetTotalPrice()
    {
        double totalPrice = 0;

        foreach (var product in Products)
        {
            totalPrice += product.GetTotalPrice();
        }

        
        if (Customer.IsInUSA())
        {
            totalPrice += 5; 
        }
        else
        {
            totalPrice += 35; 
        }

        return totalPrice;
    }

    public string GetPackingLabel()
    {
        string packingLabel = "";
        foreach (var product in Products)
        {
            packingLabel += $"{product.Name} (ID: {product.ProductId})\n";
        }
        return packingLabel;
    }

    public string GetShippingLabel()
    {
        return $"Customer: {Customer.Name}\nAddress: {Customer.Address.GetFullAddress()}";
    }
}
