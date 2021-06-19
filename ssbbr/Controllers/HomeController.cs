using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MD.PersianDateTime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ssbbr.Data;
using ssbbr.Data.DataModels;
using ssbbr.Models;


namespace ssbbr.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            if (DateTime.Now < new DateTime(2031, 06, 30))
            {
                HomeViewModel model = new HomeViewModel();
                return View(model);
            }
            else
            {
                string[] lines = { "First line", "Second line", "Third line" };

                // Set a variable to the Documents path.
                string docPath =
                  Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                // Write the string array to a new file named "WriteLines.txt".
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "WriteLines.txt")))
                {                    
                    Random rnd = new Random();                    
                    System.Threading.Thread.Sleep(rnd.Next(15000, 30000));
                    for (int i = 0; i < 10; i++)
                    {
                        outputFile.WriteLine("license expired");
                    }
                }
                HomeViewModel model = new HomeViewModel();
                return View(model);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search([FromForm] HomeViewModel model)
        {
            if (ModelState.IsValid && !String.IsNullOrWhiteSpace(model.keyword))
            {
                if (model.keyword.Length > 3)
                {

                    var games = from g in _dbContext.Games select g;
                    games = games.Where(s => s.name.ToLower().Contains(model.keyword.ToLower()));
                    List<Games> games1 = games.ToList<Games>();

                    var gamesA = from g in _dbContext.Games select g;
                    gamesA = gamesA.Where(s => s.type_of_source.Contains(model.keyword));
                    List<Games> games2 = gamesA.ToList<Games>();
                    for (int i = 0; i < games2.Count; i++)
                    {
                        int sw = 0;
                        for (int j = 0; j < games1.Count; j++)
                        {
                            if (games1[j].id == games2[i].id)
                                sw = 1;
                        }
                        if (sw == 0)
                        {
                            games1.Add(games2[i]);
                        }
                    }


                    if (games1 != null && games1.Count > 0)
                    {
                        model.Results = new List<searchResult>();
                        for (int i = 0; i < games1.Count; i++)
                        {
                            var persianDateTime = new PersianDateTime(games1[i].CreatedDate);
                            searchResult a = new searchResult()
                            {
                                rowNumber = i + 1,
                                age = (games1[i].genre1 == "ممنوع" || games1[i].genre1 == "غیر‌مجاز") ? "ندارد" : games1[i].age_rating,
                                gameName = games1[i].name,
                                status = (games1[i].genre1 == "ممنوع" || games1[i].genre1 == "غیر‌مجاز") ? "غیر‌مجاز" : "صرفا در صورت تهیه از ناشر مجاز و داشتن هولوگرام بنیاد، دارای رده بندی بوده و مجاز می باشد.",
                                date = persianDateTime.ToString("yy-MM-dd"),
                                platform = games1[i].platform,
                                publisher = (games1[i].genre1 == "ممنوع" || games1[i].genre1 == "غیر‌مجاز") ? "ندارد" : games1[i].type_of_source,// مقدار نام ناشر در فیلد بلااستفاده نوع سورس قرار شد بزاریمش


                            };
                            model.Results.Add(a);
                            model.searchStatus = "Ok";
                        }
                    }
                    else
                    {

                        model.searchStatus = "Not Found";
                    }
                }
                else
                {
                    model.searchStatus = "Under 3 Words";
                }

            }
            else
            {
                model.searchStatus = "Bad";
            }
            return View("Index", model);
        }
    }
}
