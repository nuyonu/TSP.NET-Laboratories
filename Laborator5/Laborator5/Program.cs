using Laborator5.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Laborator5
{
    class Program
    {
        static void Main(string[] args)
        {
            TestSelfReference();
            TestProduct();
            TestPhotograph();
            TestBusiness();
            TestEmployee();
        }

        private static void TestEmployee()
        {
            using var context = new EmployeeContext();
            context.Employees.Add(FullTimeEmployee.Create("Jane", "Doe", 71500M));
            context.Employees.Add(FullTimeEmployee.Create("John", "Smith", 62500M));
            context.Employees.Add(HourlyEmployee.Create("Tom", "Jones", 8.75M));
            context.SaveChanges();
            Console.WriteLine("--- All Employees ---");
            foreach (var emp in context.Employees)
            {
                bool fullTime = emp is HourlyEmployee ? false : true;
                Console.WriteLine("{0} {1} ({2})", emp.FirstName, emp.LastName,
                fullTime ? "Full Time" : "Hourly");
            }
            Console.WriteLine("--- Full Time ---");
            foreach (var fte in context.Employees.OfType<FullTimeEmployee>())
            {
                Console.WriteLine("{0} {1}", fte.FirstName, fte.LastName);
            }
            Console.WriteLine("--- Hourly ---");
            foreach (var hourly in context.Employees.OfType<HourlyEmployee>())
            {
                Console.WriteLine("{0} {1}", hourly.FirstName,
                hourly.LastName);
            }
        }

        private static void TestBusiness()
        {
            using var context = new BusinessContext();
            context.Businesses.Add(Business.Create("Corner Dry Cleaning", "100x1"));
            context.Businesses.Add(Retail.Create("Shop and Save", "200C", "101 Main", "Anytown", "TX", "76106"));
            context.Businesses.Add(ECommerce.Create("BuyNow.com", "300AB", "www.buynow.com"));
            context.SaveChanges();
            Console.WriteLine("\n--- All Businesses ---");
            foreach (var b in context.Businesses)
                Console.WriteLine("{0} (#{1})", b.Name, b.LicenseNumber);
            Console.WriteLine("\n--- Retail Businesses ---");
            foreach (var r in context.Businesses.OfType<Retail>())
            {
                Console.WriteLine("{0} (#{1})", r.Name, r.LicenseNumber);
                Console.WriteLine("{0}", r.Address);
                Console.WriteLine("{0}, {1} {2}", r.City, r.State, r.ZIPCode);
            }
            Console.WriteLine("\n--- eCommerce Businesses ---");
            foreach (var e in context.Businesses.OfType<ECommerce>())
            {
                Console.WriteLine("{0} (#{1})", e.Name, e.LicenseNumber);
                Console.WriteLine("Online address is: {0}", e.URL);
            }
        }

        private static void TestPhotograph()
        {
            AddPhoto();
            using var context = new PhotographContext();
            foreach (var photo in context.Photographs)
            {
                Console.WriteLine("Photo: {0}, ThumbnailSize {1} bytes", photo.Title, photo.ThumbnailBits.Length);
                context.Entry(photo).Reference(p => p.PhotographFullImage).Load();
                Console.WriteLine("Full Image Size: {0} bytes", photo.PhotographFullImage.HighResolutionBits.Length);
            }
        }

        private static void AddPhoto()
        {
            byte[] thumbBits = new byte[100];
            byte[] fullBits = new byte[2000];
            using var context = new PhotographContext();
            var photo = new Photograph
            {
                Title = "My Dog",
                ThumbnailBits = thumbBits
            };
            var fullImage = new PhotographFullImage
            {
                HighResolutionBits = fullBits
            };
            photo.PhotographFullImage = fullImage;
            context.Photographs.Add(photo);
            context.SaveChanges();
        }

        private static void TestProduct()
        {
            AddProducts();
            using var context = new ProductContext();
            foreach (var p in context.Products)
            {
                Console.WriteLine("{0} {1} {2} {3}", p.SKU, p.Description,
                p.Price.ToString("C"), p.ImageURL);
            }

        }
        private static void AddProducts()
        {
            using var context = new ProductContext();
            context.Products.Add(Product.Create(147, "Expandable Hydration Pack", 19.97M, "/pack147.jpg"));
            context.Products.Add(Product.Create(178, "Rugged Ranger Duffel Bag", 39.97M, "/pack178.jpg"));
            context.Products.Add(Product.Create(186, "Range Field Pack", 98.97M, "/noimage.jp"));
            context.Products.Add(Product.Create(202, "Small Deployment Back Pack", 29.97M, "/pack202.jpg"));
            context.SaveChanges();
        }
        private static void TestSelfReference()
        {
            AddSelfReference();
            using var context = new ModelSelfReferences();
            foreach (var x in context.SelfReferences.Include("References"))
            {
                Console.WriteLine("Name: " + x.Name);
                if (x.References.Count > 0)
                    foreach (var reference in x.References)
                        Console.WriteLine("--->referinta catre: " + reference.Name);
            }
        }
        private static void AddSelfReference()
        {
            SelfReference self = new SelfReference()
            {
                Name = "Andrei",
                References = new List<SelfReference>()
                {
                    new SelfReference()
                    {
                        Name = "Ion",
                        References = new List<SelfReference>()
                        {
                            new SelfReference()
                            {
                                Name = "Geri"
                            },
                        }
                    },
                    new SelfReference()
                    {
                        Name = "Vasile"
                    }
                }
            };
            using var context = new ModelSelfReferences();
            context.SelfReferences.Add(self);
            context.SaveChanges();
        }
    }
}
