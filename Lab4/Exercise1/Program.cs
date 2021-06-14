using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace NewLanguageFeatures
{
    public delegate bool KeyValueFilter<K, V>(K key, V value);

    public static class Extensions
    {
        public static List<K> FilterBy<K, V> (this Dictionary<K, V> items, KeyValueFilter<K,V> filter)
        {
            var result = new List<K>();

            foreach (KeyValuePair<K, V> element in items)
            {
                if (filter(element.Key, element.Value))
                {
                    result.Add(element.Key);
                }
            }
            return result;
        }
        public static List<T> Append<T>(this List<T> a, List<T> b)
        {
            var newList = new List<T>(a);
            newList.AddRange(b);
            return newList;
        }
        public static bool Compare(this Customer customer1, Customer customer2)
        {
            if (customer1.CustomerID == customer2.CustomerID &&
                customer1.Name == customer2.Name &&
                customer1.City == customer2.City)
            {
                return true;
            }
            return false;
        }
    }

    class Store
    {
        public string Name { get; set; }
        public string City { get; set; }

        public override string ToString()
        {
            return Name + "\t" + City;
        }
    }


    public class Customer
    {
        public string Name { get; set; }
        public string City { get; set; }

        public int CustomerID { get; private set; }

        public Customer(int ID)
        {
            CustomerID = ID;
        }
        public override string ToString()
        {
            return Name + "\t" + City + "\t" + CustomerID;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            /*
            var customers = CreateCustomers();
            /*
             * Exercise 2 3
            var addedCustomers = new List<Customer>
            {
                new Customer(9) {Name = "Paolo Accorti", City = "Torino"},
                new Customer(10) {Name = "Diego Roel", City = "Madrid"}
            };

            var updatedCustomers = customers.Append(addedCustomers);

            var newCustomer = new Customer(10)
            {
                Name = "Diego Roel",
                City = "Madrid"
            };
            foreach (var cus in customers)
            {
                if (newCustomer.Compare(cus))
                {
                    Console.WriteLine("The new customer was already in the list.");
                    return;
                }
            }
            Console.WriteLine("The new customer was not in the list.");
            */

            /*
             * Exercise 4

            foreach (var c in FindCustomersByCity(customers, "London"))
            {
                Console.WriteLine(c);
            }

            */
            /*
             * Exercise5
            var customerDictionary = new Dictionary<Customer, string>();

            foreach (var c in customers)
                customerDictionary.Add(c, c.Name.Split(' ')[1]);

            var matches = customerDictionary.FilterBy((customer, lastName) => lastName.StartsWith("A"));
            Console.WriteLine("Number of Matches: {0}", matches.Count);
            */

            /*
             * Exercise6-task1
            Expression<Func<int, bool>> filter = n => (n * 3) < 5;

            BinaryExpression lt = (BinaryExpression)filter.Body;
            BinaryExpression mult = (BinaryExpression)lt.Left;
            ParameterExpression en = (ParameterExpression)mult.Left;
            ConstantExpression three = (ConstantExpression)mult.Right;
            ConstantExpression five = (ConstantExpression)lt.Right;

            Console.WriteLine("({0} ({1} {2} {3}) {4})", lt.NodeType, mult.NodeType, en.Name, three.Value, five.Value);
            */

            /*
             * Exercise6-task2
            Func<int, int> addOne = n => n + 1;
            Console.WriteLine("Result: {0}", addOne(5));

            Expression<Func<int, int>> addOneExpression = n => n + 1;

            var addOneFunc = addOneExpression.Compile();
            Console.WriteLine("Result: {0}", addOneFunc(5));
            */

            Query();


        }

        static void Query()
        {
            var results = from c in CreateCustomers()
                          join s in CreateStores() on c.City equals s.City
                          group s by c.Name into g
                          let count = g.Count()
                          orderby count ascending
                          select new
                          {
                              CustomerName = g.Key,
                              Count = g.Count()
                          };
            foreach (var result in results)
            {
                Console.WriteLine("{0}\t{1}", result.CustomerName, result.Count);
            }
        }


        static List<Store> CreateStores()
        {
            return new List<Store>
            {
                new Store { Name = "Jim’s Hardware",    City = "Berlin" },
                new Store { Name = "John’s Books",  City = "London" },
                new Store { Name = "Lisa’s Flowers",    City = "Torino" },
                new Store { Name = "Dana’s Hardware",   City = "London" },
                new Store { Name = "Tim’s Pets",    City = "Portland" },
                new Store { Name = "Scott’s Books",     City = "London" },
                new Store { Name = "Paula’s Cafe",  City = "Marseille" },

            };
        }

        public static List<Customer> FindCustomersByCity(List<Customer> customers, string city)
        {
            return customers.FindAll((Customer c) => c.City == city);
        }

        static List<Customer> CreateCustomers()
        {
            return new List<Customer>
            {
                new Customer(1) { Name = "Maria Anders", City = "Berlin"},
                new Customer(2) { Name = "Laurence Lebihan", City = "Marseille"},
                new Customer(3) { Name = "Elizabeth Brown",  City = "London"    },
                new Customer(4) { Name = "Ann Devon",        City = "London"    },
                new Customer(5) { Name = "Paolo Accorti",    City = "Torino"    },
                new Customer(6) { Name = "Fran Wilson",      City = "Portland"  },
                new Customer(7) { Name = "Simon Crowther",   City = "London"    },
                new Customer(8) { Name = "Liz Nixon",        City = "Portland"  }

            };
        }

        static void VarTest()
        {
            /*
            var i = 43;

            var s = "...This is only a test...";

            var numbers = new int[] { 4, 9, 16 };

            var complex = new SortedDictionary<string, List<DateTime>>();
            */
            var x = new[] { 1, 2, 3 };
        }

    }

}
