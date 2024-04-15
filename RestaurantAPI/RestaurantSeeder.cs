using RestaurantAPI.entity;

namespace RestaurantAPI
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Roles.Any())
                {
                    var reoles = GetRoles();
                    _dbContext.Roles.AddRange(reoles);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }

        }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name= "User",
                },
                new Role()
                {
                    Name= "Manager",
                },
                new Role()
                {
                    Name= "Admin",
                }
            };
            return roles;
        }
        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "KFC",
                    ContactEmail = "contact@kfc.com",
                    DeliveryDistance = 50,
                    Dishes = new List<Dish>()
                    {
                       new Dish()
                       {
                           Name = "Nashille Hot Chicken",
                           Price = 10.30M,
                       },

                       new Dish()
                       {
                            Name = "Chicken Nuggets",
                            Price = 5.30M
                       },
                    },
                    Adress = new Adress()
                    {
                        City = "Kraków",
                        Street = "Długa 5",
                        PostalCode = "30-001"

                    }
                },
                new Restaurant()
                {
                    Name = "McDonald's",
                    Category = "Fast Food",
                    Description = "McDonald's",
                    ContactEmail = "contact@mcdonalds.com",
                    DeliveryDistance = 100,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Big Mac",
                            Price = 12.50M,
                        },
                        new Dish()
                        {
                            Name = "French Fries",
                            Price = 4.20M
                        }
                    },
                    Adress = new Adress()
                    {
                        City = "Gliwice",
                        Street = "Bojkowska 37",
                        PostalCode = "44-100"
                    }
                },
                new Restaurant()
                {
                    Name = "Pod Świerkiem",
                    Category = "Polska kuchnia",
                    Description = "Tradycyjne dania polskie pod świerkiem.",
                    ContactEmail = "kontakt@podswierkiem.pl",
                    DeliveryDistance = 80,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Pierogi ruskie", Price = 15.50M },
                        new Dish() { Name = "Schabowy z kapustą", Price = 20.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Katowice",
                        Street = "Świerkowa 10",
                        PostalCode = "40-001"
                    }
                },
                new Restaurant()
                {
                    Name = "Sushi Heaven",
                    Category = "Japońska kuchnia",
                    Description = "Najlepsze sushi w okolicy.",
                    ContactEmail = "kontakt@sushiheaven.pl",
                    DeliveryDistance = 70,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "California Roll", Price = 25.00M },
                        new Dish() { Name = "Nigiri z łososiem", Price = 30.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Gliwice",
                        Street = "Sushiowa 15",
                        PostalCode = "44-100"
                    }
                },
                new Restaurant()
                {
                    Name = "Bistro Kwadrat",
                    Category = "Międzynarodowa kuchnia",
                    Description = "Nowoczesne bistro z różnorodnym menu.",
                    ContactEmail = "kontakt@bistrokwadrat.pl",
                    DeliveryDistance = 90,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Sałatka grecka", Price = 18.00M },
                        new Dish() { Name = "Spaghetti carbonara", Price = 22.50M }
                    },
                    Adress = new Adress()
                    {
                        City = "Sosnowiec",
                        Street = "Kwadratowa 3",
                        PostalCode = "41-200"
                    }
                },
                new Restaurant()
                {
                    Name = "Steak House",
                    Category = "Amerykańska kuchnia",
                    Description = "Restauracja specjalizująca się w stekach.",
                    ContactEmail = "kontakt@steakhouse.pl",
                    DeliveryDistance = 80,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Stek wołowy", Price = 35.00M },
                        new Dish() { Name = "Grillowane warzywa", Price = 15.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Chorzów",
                        Street = "Steakowa 7",
                        PostalCode = "41-500"
                    }
                },
                new Restaurant()
                {
                    Name = "Pizza Hut",
                    Category = "Włoska kuchnia",
                    Description = "Restauracja serwująca najlepszą pizzę w okolicy.",
                    ContactEmail = "kontakt@pizzahut.pl",
                    DeliveryDistance = 60,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Margherita", Price = 20.00M },
                        new Dish() { Name = "Pepperoni", Price = 25.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Ruda Śląska",
                        Street = "Pizzeriowa 12",
                        PostalCode = "41-700"
                    }
                },
                new Restaurant()
                {
                    Name = "La Dolce Vita",
                    Category = "Włoska kuchnia",
                    Description = "Rodzinna restauracja z prawdziwym włoskim smakiem.",
                    ContactEmail = "kontakt@ladolcevita.pl",
                    DeliveryDistance = 70,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Risotto z grzybami", Price = 28.00M },
                        new Dish() { Name = "Tiramisu", Price = 12.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Bytom",
                        Street = "Dolce 8",
                        PostalCode = "41-902"
                    }
                },
                new Restaurant()
                {
                    Name = "Thai Paradise",
                    Category = "Tajska kuchnia",
                    Description = "Autentyczne dania tajskie w przytulnej restauracji.",
                    ContactEmail = "kontakt@thaiparadise.pl",
                    DeliveryDistance = 75,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Pad Thai", Price = 26.00M },
                        new Dish() { Name = "Tom Yum", Price = 18.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Zabrze",
                        Street = "Rajowa 9",
                        PostalCode = "41-800"
                    }
                },
                    new Restaurant()
                {
                    Name = "Sushi Master",
                    Category = "Kuchnia japońska",
                    Description = "Restauracja specjalizująca się w sushi i potrawach japońskich.",
                    ContactEmail = "kontakt@sushimaster.pl",
                    DeliveryDistance = 55,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Sashimi mix", Price = 40.00M },
                        new Dish() { Name = "Tempura krewetek", Price = 22.50M }
                    },
                    Adress = new Adress()
                    {
                        City = "Katowice",
                        Street = "Sushiowa 15",
                        PostalCode = "40-010"
                    }
                },
                new Restaurant()
                {
                    Name = "Burger Factory",
                    Category = "Fast Food",
                    Description = "Restauracja serwująca najlepsze burgery w okolicy.",
                    ContactEmail = "kontakt@burgerfactory.pl",
                    DeliveryDistance = 65,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Classic Burger", Price = 18.00M },
                        new Dish() { Name = "Double Cheeseburger", Price = 25.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Gliwice",
                        Street = "Burgerowa 22",
                        PostalCode = "44-100"
                    }
                },
                new Restaurant()
                {
                    Name = "Pierogi Heaven",
                    Category = "Polska kuchnia",
                    Description = "Restauracja specjalizująca się w różnorodnych smakach pierogów.",
                    ContactEmail = "kontakt@pierogiheaven.pl",
                    DeliveryDistance = 70,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Pierogi ruskie", Price = 15.00M },
                        new Dish() { Name = "Pierogi z mięsem", Price = 18.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Rybnik",
                        Street = "Pierogowa 10",
                        PostalCode = "44-200"
                    }
                },
                new Restaurant()
                {
                    Name = "Vege Life",
                    Category = "Wegetariańska kuchnia",
                    Description = "Restauracja oferująca zdrowe dania wegetariańskie i wegańskie.",
                    ContactEmail = "kontakt@vegelife.pl",
                    DeliveryDistance = 75,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Falafel wrap", Price = 16.00M },
                        new Dish() { Name = "Kasza jaglana z warzywami", Price = 20.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Tychy",
                        Street = "Wege 5",
                        PostalCode = "43-100"
                    }
                },
                new Restaurant()
                {
                    Name = "Dolce Vita",
                    Category = "Włoska kuchnia",
                    Description = "Rodzinna restauracja z prawdziwym włoskim smakiem.",
                    ContactEmail = "kontakt@dolcevita.pl",
                    DeliveryDistance = 60,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Pizza Capricciosa", Price = 22.00M },
                        new Dish() { Name = "Spaghetti Bolognese", Price = 18.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Sosnowiec",
                        Street = "Włoska 7",
                        PostalCode = "41-200"
                    }
                },
                new Restaurant()
                {
                    Name = "Taco Time",
                    Category = "Meksykańska kuchnia",
                    Description = "Restauracja oferująca autentyczne meksykańskie dania, w tym tacos, burrito i nachos.",
                    ContactEmail = "kontakt@tacotime.pl",
                    DeliveryDistance = 55,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Taco al pastor", Price = 14.50M },
                        new Dish() { Name = "Quesadilla z kurczakiem", Price = 18.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Zabrze",
                        Street = "Meksykańska 3",
                        PostalCode = "41-800"
                    }
                },
                new Restaurant()
                {
                    Name = "Peking Garden",
                    Category = "Chińska kuchnia",
                    Description = "Restauracja serwująca autentyczne chińskie dania, w tym smażone ryż, kurczak Kung Pao i wiele innych.",
                    ContactEmail = "kontakt@pekinggarden.pl",
                    DeliveryDistance = 60,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Krewetki curry", Price = 25.00M },
                        new Dish() { Name = "Chop suey z warzywami", Price = 20.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Bytom",
                        Street = "Chińska 8",
                        PostalCode = "41-900"
                    }
                },
                new Restaurant()
                {
                    Name = "Bella Napoli",
                    Category = "Włoska kuchnia",
                    Description = "Restauracja oferująca pyszne włoskie dania, w tym pizzę, makarony i desery.",
                    ContactEmail = "kontakt@bellanapoli.pl",
                    DeliveryDistance = 65,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Pizza Margherita", Price = 20.00M },
                        new Dish() { Name = "Ravioli z ricottą i szpinakiem", Price = 22.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Chorzów",
                        Street = "Neapolitańska 12",
                        PostalCode = "41-500"
                    }
                },
                new Restaurant()
                {
                    Name = "Fit & Fresh",
                    Category = "Zdrowa kuchnia",
                    Description = "Restauracja specjalizująca się w zdrowych i odżywczych posiłkach, w tym sałatkach, koktajlach i zdrowych przekąskach.",
                    ContactEmail = "kontakt@fitfresh.pl",
                    DeliveryDistance = 70,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Sałatka z kurczakiem i awokado", Price = 18.00M },
                        new Dish() { Name = "Smoothie z malinami i bananem", Price = 12.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Ruda Śląska",
                        Street = "Zdrowa 2",
                        PostalCode = "41-700"
                    }
                },
                new Restaurant()
                {
                    Name = "Pizzeria Roma",
                    Category = "Włoska kuchnia",
                    Description = "Rodzinna pizzeria serwująca tradycyjne włoskie pizze na cienkim cieście.",
                    ContactEmail = "kontakt@pizzeriaroma.pl",
                    DeliveryDistance = 75,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Pizza Quattro Stagioni", Price = 24.00M },
                        new Dish() { Name = "Calzone z szynką i pieczarkami", Price = 22.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Częstochowa",
                        Street = "Rzymska 6",
                        PostalCode = "42-200"
                    }
                },
                new Restaurant()
                {
                    Name = "Sushi Bar",
                    Category = "Japońska kuchnia",
                    Description = "Restauracja specjalizująca się w sushi, sashimi i innych japońskich przysmakach.",
                    ContactEmail = "kontakt@sushibar.pl",
                    DeliveryDistance = 80,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "California Roll", Price = 30.00M },
                        new Dish() { Name = "Nigiri z łososiem", Price = 25.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Gliwice",
                        Street = "Sushi 7",
                        PostalCode = "44-100"
                    }
                },
                new Restaurant()
                {
                    Name = "Burger Heaven",
                    Category = "Amerykańska kuchnia",
                    Description = "Restauracja serwująca pyszne burgery, frytki i inne amerykańskie dania.",
                    ContactEmail = "kontakt@burgerheaven.pl",
                    DeliveryDistance = 85,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Classic Cheeseburger", Price = 22.00M },
                        new Dish() { Name = "Chicken Wings", Price = 18.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Żory",
                        Street = "Burgerowa 10",
                        PostalCode = "44-240"
                    }
                },
                new Restaurant()
                {
                    Name = "Thai Spice",
                    Category = "Tajska kuchnia",
                    Description = "Restauracja serwująca autentyczne tajskie dania, w tym curry, pad thai i inne przysmaki.",
                    ContactEmail = "kontakt@thaispice.pl",
                    DeliveryDistance = 90,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Krewetki w czerwonym curry", Price = 35.00M },
                        new Dish() { Name = "Pad Thai z kurczakiem", Price = 28.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Katowice",
                        Street = "Tajska 15",
                        PostalCode = "40-001"
                    }
                },
                new Restaurant()
                {
                    Name = "La Piazza",
                    Category = "Włoska kuchnia",
                    Description = "Restauracja oferująca klasyczne włoskie dania, w tym makarony, risotto i pizzę.",
                    ContactEmail = "kontakt@lapiazza.pl",
                    DeliveryDistance = 95,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Risotto z grzybami", Price = 26.00M },
                        new Dish() { Name = "Lasagne bolognese", Price = 30.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Rybnik",
                        Street = "Włoska 20",
                        PostalCode = "44-200"
                    }
                },
                new Restaurant()
                {
                    Name = "Healthy Bowl",
                    Category = "Zdrowa kuchnia",
                    Description = "Restauracja specjalizująca się w zdrowych misach z kaszą, warzywami, mięsem i sosami.",
                    ContactEmail = "kontakt@healthybowl.pl",
                    DeliveryDistance = 100,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Miska z kurczakiem teriyaki", Price = 25.00M },
                        new Dish() { Name = "Miska z quinoą i pieczonymi warzywami", Price = 20.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Tychy",
                        Street = "Zdrowa 30",
                        PostalCode = "43-100"
                    }
                },
                new Restaurant()
                {
                    Name = "Vegan Garden",
                    Category = "Wegetariańska kuchnia",
                    Description = "Restauracja oferująca szeroki wybór wegetariańskich i wegańskich dań, w tym sałatki, dania główne i desery.",
                    ContactEmail = "kontakt@vegangarden.pl",
                    DeliveryDistance = 80,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Falafel Wrap", Price = 18.00M },
                        new Dish() { Name = "Quinoa Salad", Price = 22.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Katowice",
                        Street = "Wegetariańska 8",
                        PostalCode = "40-002"
                    }
                },
                new Restaurant()
                {
                    Name = "Steak House",
                    Category = "Kuchnia mięsna",
                    Description = "Restauracja specjalizująca się w stekach różnych gatunków mięsa, serwująca także pyszne przystawki i desery.",
                    ContactEmail = "kontakt@steakhouse.pl",
                    DeliveryDistance = 85,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "T-bone Steak", Price = 60.00M },
                        new Dish() { Name = "Porterhouse Steak", Price = 55.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Ruda Śląska",
                        Street = "Mięsna 12",
                        PostalCode = "41-700"
                    }
                },
                new Restaurant()
                {
                    Name = "Seafood Heaven",
                    Category = "Kuchnia morska",
                    Description = "Restauracja specjalizująca się w świeżych owocach morza, serwująca dania z ryb i skorupiaków w różnych wariantach.",
                    ContactEmail = "kontakt@seafoodheaven.pl",
                    DeliveryDistance = 90,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Grilled Salmon", Price = 35.00M },
                        new Dish() { Name = "Shrimp Scampi", Price = 30.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Bytom",
                        Street = "Rybna 25",
                        PostalCode = "41-900"
                    }
                },
                new Restaurant()
                {
                    Name = "Indian Spice",
                    Category = "Kuchnia indyjska",
                    Description = "Restauracja serwująca autentyczne indyjskie dania, w tym curry, tandoori, biryani i inne przysmaki.",
                    ContactEmail = "kontakt@indianspice.pl",
                    DeliveryDistance = 95,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Chicken Tikka Masala", Price = 30.00M },
                        new Dish() { Name = "Vegetable Biryani", Price = 25.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Chorzów",
                        Street = "Indyjska 17",
                        PostalCode = "41-500"
                    }
                },
                new Restaurant()
                {
                    Name = "Pasta Italia",
                    Category = "Włoska kuchnia",
                    Description = "Restauracja serwująca świeże, domowej roboty makarony, ravioli, spaghetti i inne włoskie specjały.",
                    ContactEmail = "kontakt@pastaitalia.pl",
                    DeliveryDistance = 100,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Spaghetti Carbonara", Price = 25.00M },
                        new Dish() { Name = "Ravioli z ricottą i szpinakiem", Price = 28.00M }
                    },
                    Adress = new Adress()
                    {
                        City = "Częstochowa",
                        Street = "Włoska 30",
                        PostalCode = "42-200"
                    }
                }
            };
            return restaurants;
        }
    }
}
