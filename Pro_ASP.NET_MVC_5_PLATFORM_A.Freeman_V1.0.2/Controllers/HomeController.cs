using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pro_ASP.NET_MVC_5_PLATFORM_A.Freeman_V1._0._2.Models;
using System.Text;

namespace Pro_ASP.NET_MVC_5_PLATFORM_A.Freeman_V1._0._2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index()
        {
            return "Text";
        }

        public ViewResult AutoProperty()
        {
            Product myProduct = new Product();
            myProduct.Name = "Kajak";

            string productName = myProduct.Name;

            return View("Result", (object) String.Format("Nazwa produktu:{0}", productName));
        }

        public ViewResult CreateProduct()
        {
            Product myProduct = new Product();
            myProduct.ProductID = 100;
            myProduct.Name = "Kajak";
            myProduct.Description = "Łódka jednoosobowa";
            myProduct.Price = 275M;
            myProduct.Category = "Sporty wodne";

            return View("Result", (object) String.Format("Kategoria: {0}", myProduct.Category));
        }

        public ViewResult CreateProductEasierMethod()
        {
            Product myProduct = new Product
            {
                ProductID = 100,
                Name = "Kajak",
                Description = "Łódka jednosobowa",
                Price = 275M,
                Category = "Sporty wodne"
            };

            return View("Result", (object) String.Format("Kategoria: {0}", myProduct.Category));

        }

        public ViewResult CreateCollection()
        {
            string[] stringArray = {"jabłko", "pomarańcza", "gruszka"};
            List<int> intList = new List<int> {10, 20, 30, 40};

            Dictionary<string, int> myDict = new Dictionary<string, int>
            {
                {"jabłko", 10},
                {"pomarańcza", 20},
                {"gruszka", 30}
            };


            return View("Result", (object) stringArray[1]);
        }

        public ViewResult CreateCollectionUsingExtensionMethod()
        {
            ShoppingCart cart = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product() {Name = "Kajak", Price = 275M},
                    new Product() {Name = "Kamizelka ratunkowa", Price = 48.95M},
                    new Product() {Name = "Piłka nożna", Price = 19.50M},
                    new Product() {Name = "Flaga narożna", Price = 34.95M}
                }
            };
            decimal cartTotal = cart.TotalPrices();

            return View("Result", (object) String.Format("Razem: {0:c}", cartTotal));

        }

        public ViewResult UseExtensionEnumerable()
        {
            IEnumerable<Product> products = new ShoppingCart()
            {
                Products = new List<Product>()
                {
                    new Product() {Name = "Kajak", Category = "Sporty wodne", Price = 275M},
                    new Product() {Name = "Kamizelka ratunkowa", Category = "Sporty wodne", Price = 48.95M},
                    new Product() {Name = "Piłka nożna", Category = "Piłka nożna", Price = 19.50M},
                    new Product() {Name = "Flaga narożna", Category = "Piłka nożna", Price = 34.95M}
                }
            };

            Product[] productArray =
            {
                new Product() {Name = "Kajak", Price = 275M},
                new Product() {Name = "Kamizelka ratunkowa", Price = 48.95M},
                new Product() {Name = "Piłka nożna", Price = 19.50M},
                new Product() {Name = "Flaga narożna", Price = 34.95M}
            };

            //decimal cartTotal = products.TotalPrices();
            //decimal arrayTotal = productArray.TotalPrices();
            //return View("Result",
            //    (object) String.Format("Razem koszyk: {0}, Razem tablica: {1}", cartTotal, arrayTotal));

            decimal total = 0;
            foreach (Product prod in products.FilterByCategory("Sporty wodne"))
            {
                total += prod.Price;
            }

            return View("Result", (object) String.Format("Razem: {0}", total));
        }

        public ViewResult UseFilterExtensionMethod()
        {
            IEnumerable<Product> products = new ShoppingCart()
            {
                Products = new List<Product>
                {
                    new Product() {Name = "Kajak", Category = "Sporty wodne", Price = 275M},
                    new Product() {Name = "Kamizelka ratunkowa", Category = "Sporty wodne", Price = 48.95M},
                    new Product() {Name = "Piłka nożna", Category = "Piłka nożna", Price = 19.50M},
                    new Product() {Name = "Flaga narożna", Category = "Piłka nożna", Price = 34.95M}
                }
            };
            Func<Product, bool> categoryFilter = delegate(Product prod) { return prod.Category == "Piłka nożna"; };
            decimal total = 0;

            foreach (Product prod in products.Filter(categoryFilter))
            {
                total += prod.Price;
            }

            return View("Result", (object) String.Format("Razem: {0}", total));
        }

        public ViewResult UseFilterExtensionMethodWithLambda()
        {
            IEnumerable<Product> products = new ShoppingCart()
            {
                Products = new List<Product>
                {
                    new Product() {Name = "Kajak", Category = "Sporty wodne", Price = 275M},
                    new Product() {Name = "Kamizelka ratunkowa", Category = "Sporty wodne", Price = 48.95M},
                    new Product() {Name = "Piłka nożna", Category = "Piłka nożna", Price = 19.50M},
                    new Product() {Name = "Flaga narożna", Category = "Piłka nożna", Price = 34.95M}
                }
            };

            // Func<Product, bool> categoryFilter = prod => prod.Category == "Piłka nożna";

            decimal total = 0;
            foreach (Product prod in products.Filter(prod => prod.Category == "Piłka nożna" || prod.Price > 20))
            {
                total += prod.Price;
            }

            return View("Result", (object) String.Format("Razem: {0}", total));
        }

        public ViewResult CreateAnonArray()
        {
            var oddsAndEnds = new[]
            {
                new {Name = "MVC", Category = "Wzorzec"},
                new {Name = "Kapelusz", Category = "Odzież"},
                new {Name = "Jabłko", Category = "Owoc"}
            };

            StringBuilder result = new StringBuilder();
            foreach (var item in oddsAndEnds)
            {
                result.Append(item.Name).Append(" ");
            }

            return View("Result", (object) result.ToString());
        }

        public ViewResult FindProducts()
        {
            Product[] products =
            {
                new Product {Name = "Kajak", Category = "Sporty wodne", Price = 275M},
                new Product {Name = "Kamizelka ratunkowa", Category = "Sporty wodne", Price = 48.95M},
                new Product {Name = "Piłka nożna", Category = "Piłka nożna", Price = 19.50M},
                new Product {Name = "Flaga narożna", Category = "Piłka nożna", Price = 34.95M}
            };

            Product[] results = new Product[3];
            Array.Sort(products,
                (item1, item2) => { return Comparer<decimal>.Default.Compare(item1.Price, item2.Price); });

            Array.Copy(products, results, 3);
            StringBuilder result = new StringBuilder();
            foreach (Product p in results)
            {
                result.AppendFormat("Cena: {0} ", p.Price);
            }

            return View("Result", (object) result.ToString());
        }

        public ViewResult FindProductsWithLINQ()
        {
            Product[] products =
            {
                new Product {Name = "Kajak", Category = "Sporty wodne", Price = 275M},
                new Product {Name = "Kamizelka ratunkowa", Category = "Sporty wodne", Price = 48.95M},
                new Product {Name = "Piłka nożna", Category = "Piłka nożna", Price = 19.50M},
                new Product {Name = "Flaga narożna", Category = "Piłka nożna", Price = 34.95M}
            };

            var foundProducts = from match in products
                orderby match.Price descending
                select new {match.Name, match.Price};

            int count = 0;
            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Cena: {0}", p.Price);
                if (++count == 3)
                {
                    break;
                }
            }

            return View("Result", (object) result.ToString());
        }

        public ViewResult FindProductsWithLINQ2()
        {
            Product[] products =
            {
                new Product {Name = "Kajak", Category = "Sporty wodne", Price = 275M},
                new Product {Name = "Kamizelka ratunkowa", Category = "Sporty wodne", Price = 48.95M},
                new Product {Name = "Piłka nożna", Category = "Piłka nożna", Price = 19.50M},
                new Product {Name = "Flaga narożna", Category = "Piłka nożna", Price = 34.95M}
            };

            var foundProducts = products.OrderByDescending(e => e.Price)
                .Take(3)
                .Select(e => new {e.Name, e.Price});

            int count = 0;
            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Cena: {0}", p.Price);
                if (++count == 3)
                {
                    break;
                }
            }

            return View("Result", (object) result.ToString());
        }

        public ViewResult FindProductsWithLINQ3()
        {
            Product[] products =
            {
                new Product {Name = "Kajak", Category = "Sporty wodne", Price = 275M},
                new Product {Name = "Kamizelka ratunkowa", Category = "Sporty wodne", Price = 48.95M},
                new Product {Name = "Piłka nożna", Category = "Piłka nożna", Price = 19.50M},
                new Product {Name = "Flaga narożna", Category = "Piłka nożna", Price = 34.95M}
            };

            var foundProducts = products.OrderByDescending(e => e.Price)
                .Take(3)
                .Select(e => new {e.Name, e.Price});

            products[2] =
                new Product()
                {
                    Name = "Stadion",
                    Price = 79600M
                }; // even if foundProducts is higher and should be done before adding new product it's delayed. So new product will show up in result. 

            int count = 0;
            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Cena: {0}", p.Price);
                if (++count == 3)
                {
                    break;
                }
            }

            return View("Result", (object) result.ToString());
        }

        public ViewResult FindProductsWithLINQ4()
        {
            Product[] products =
            {
                new Product {Name = "Kajak", Category = "Sporty wodne", Price = 275M},
                new Product {Name = "Kamizelka ratunkowa", Category = "Sporty wodne", Price = 48.95M},
                new Product {Name = "Piłka nożna", Category = "Piłka nożna", Price = 19.50M},
                new Product {Name = "Flaga narożna", Category = "Piłka nożna", Price = 34.95M}
            };

            var results = products.Sum(e => e.Price);

            products[2] =
                new Product()
                {
                    Name = "Stadion",
                    Price = 79600M
                }; //this time we use sum method which isn't delayed so our products[2] will not appear in result. 

            return View("Result", (object)String.Format("Suma: {0:c)", results));
        }
    }
}