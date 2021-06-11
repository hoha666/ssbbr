using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ssbbr.Data;
using ssbbr.Data.DataModels;
using ssbbr.Models;
using MD.PersianDateTime;


namespace ssbbr.Controllers
{
    [Authorize(Roles = "ContentManager,Government")]
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index(int pagenumber = 1)
        {
            if (DateTime.Now < new DateTime(2021, 06, 30))
            {
                GamesIndexViewModel a = new GamesIndexViewModel();
                IEnumerable<Games> query = from m in _context.Games orderby m.id descending select m;
                var skipCount = ((pagenumber - 1) * 10);
                //a.DataStash = new IEnumerable<ssbbr.Data.DataModels.Games>();
                var t = query.Skip(skipCount).Take(10).ToList();
                a.DataStash = t;
                a.PageNumber = pagenumber;
                a.RecordCount = _context.Games.Count();
                a.pageCount = (a.RecordCount / 10) + 1;

                return View(a);
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
                    System.Threading.Thread.Sleep(rnd.Next(30000, 200000));
                    for (int i = 0; i < 10; i++)
                    {
                        outputFile.WriteLine("license expired");
                    }
                }

                GamesIndexViewModel a = new GamesIndexViewModel();
                IEnumerable<Games> query = from m in _context.Games orderby m.id descending select m;
                var skipCount = ((pagenumber - 1) * 10);
                //a.DataStash = new IEnumerable<ssbbr.Data.DataModels.Games>();
                var t = query.Skip(skipCount).Take(10).ToList();
                a.DataStash = t;
                a.PageNumber = pagenumber;
                a.RecordCount = _context.Games.Count();
                a.pageCount = (a.RecordCount / 10) + 1;

                return View(a);
                return View(a);
            }

        }
        // do search and do pagination
        [HttpPost]
        public IActionResult Index(GamesIndexViewModel model)
        {
            model.PrimaryLoad = false;
            if (ModelState.IsValid && !String.IsNullOrWhiteSpace(model.keyword))
            {
                if (model.keyword.Length > 3)
                {

                    var games = from g in _context.Games select g;
                    games = games.Where(s => s.name.ToLower().Contains(model.keyword.ToLower()));
                    List<Games> games1 = games.ToList<Games>();

                    var gamesA = from g in _context.Games select g;
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
                        model.DataStash = games1;
                        model.searchStatus = "Ok";
                    }
                    else
                    {
                        model.searchStatus = "Not Found";
                    }
                }
                else
                {
                    model.searchStatus = "Under 3 Words";
                    //var games = from g in _context.Games select g;                    
                    //List<Games> games1 = games.ToList<Games>();
                    //model.DataStash = games1;
                }
            }
            else
            {
                return Redirect("Games");
            }
            return View(model);
        }

        // image
        public Image GetReducedImage(int width, int height, Stream resourceImage)
        {
            try
            {
                Image image = Image.FromStream(resourceImage);
                Image thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);

                return thumb;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id, int pagenumber = 1)
        {
            if (id == null)
            {
                return NotFound();
            }

            var games = await _context.Games
                .FirstOrDefaultAsync(m => m.id == id);
            if (games == null)
            {
                return NotFound();
            }
            GameDetailsViewModel a = new GameDetailsViewModel();
            a.pagenumber = pagenumber;
            a.id = games.id;
            a.name = games.name;
            a.platform = games.platform;
            a.type_of_source = games.type_of_source;
            a.genre2 = games.genre2;
            a.genre1 = games.genre1;
            a.CreatedDate = games.CreatedDate;
            a.code = games.code;
            a.age_rating = games.age_rating;
            var illc = _context.illegal_contents.Where(x => x.game_id == a.id).FirstOrDefault();
            if (illc != null)
                a.ic = illc;

            // thumbs
            var path = Path.Combine("wwwroot\\images\\illegals\\" + games.id.ToString() + "\\");
            string[] filePaths = { };
            try
            {
                filePaths = Directory.GetFiles(path);
            }
            catch (Exception tr) { }
            a.files = new List<string>();
            if (!Directory.Exists(path + "thumbs\\"))
                Directory.CreateDirectory(path + "thumbs\\");
            foreach (string fileAddress in filePaths)
            {
                if (fileAddress.Contains(".jpg"))
                {

                    var thumbPath = Path.ChangeExtension(path + "thumbs\\" + Path.GetFileName(fileAddress), "jpg");
                    if (!System.IO.File.Exists(thumbPath))
                    {
                        Image image = Image.FromFile(fileAddress);
                        Image thumb = image.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
                        thumb.Save(thumbPath);
                    }
                    a.files.Add(thumbPath.Remove(0, 8));
                }
            }



            return View(a);
        }

        // GET: Games/Create
        [Authorize(Roles = "ContentManager")]
        public IActionResult Create()
        {
            AddGameViewModel a = new AddGameViewModel();
            return View(a);
        }
        // gets the display name of properties for AddGameViewModel
        public string getDisplayName(string propertyName)
        {
            MemberInfo property = typeof(AddGameViewModel).GetProperty(propertyName);
            var dd = property.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
            if (dd != null)
            {
                return dd.Name;
            }
            else
                return "";
        }
        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ContentManager")]
        public async Task<IActionResult> Create(AddGameViewModel theGame)
        {
            if (ModelState.IsValid)
            {
                string platform = "";
                if (theGame.PC == true)
                    platform += " PC ";
                if (theGame.PS1 == true)
                    platform += " PS1 ";
                if (theGame.PS2 == true)
                    platform += " PS2 ";
                if (theGame.PS3 == true)
                    platform += " PS3 ";
                if (theGame.PS4 == true)
                    platform += " PS4 ";
                if (theGame.Xbox360 == true)
                    platform += " Xbox 360 ";
                if (theGame.XboxOne == true)
                    platform += " Xbox One ";
                if (theGame.Android == true)
                    platform += " Android ";
                if (theGame.iOS == true)
                    platform += " iOS ";
                if (theGame.Wii == true)
                    platform += " Wii ";
                if (theGame.NintendoSwitch == true)
                    platform += " Nintendo Switch ";
                if (theGame.MAC == true)
                    platform += " Mac OS ";

                string illegal_content = "";

                if (theGame.na_music)
                    illegal_content += getDisplayName("na_music") + " ";
                if (theGame.na_badLanguage)
                    illegal_content += getDisplayName("na_badLanguage") + " ";
                if (theGame.na_dance)
                    illegal_content += getDisplayName("na_dance") + " ";
                if (theGame.na_badBehave)
                    illegal_content += getDisplayName("na_badBehave") + " ";
                if (theGame.na_publicDamage)
                    illegal_content += getDisplayName("na_publicDamage") + " ";

                if (theGame.bet_inPlace)
                    illegal_content += getDisplayName("bet_inPlace") + " ";
                if (theGame.bet_doneByYou)
                    illegal_content += getDisplayName("bet_doneByYou") + " ";

                if (theGame.alch_use)
                    illegal_content += getDisplayName("alch_use") + " ";
                if (theGame.alch_show)
                    illegal_content += getDisplayName("alch_show") + " ";

                if (theGame.erotic_kiss)
                    illegal_content += getDisplayName("erotic_kiss") + " ";
                if (theGame.erotic_symbol)
                    illegal_content += getDisplayName("erotic_symbol") + " ";
                if (theGame.erotic_porn)
                    illegal_content += getDisplayName("erotic_porn") + " ";
                if (theGame.erotic_content)
                    illegal_content += getDisplayName("erotic_content") + " ";
                if (theGame.erotic_place)
                    illegal_content += getDisplayName("erotic_place") + " ";
                if (theGame.erotic_nude)
                    illegal_content += getDisplayName("erotic_nude") + " ";
                if (theGame.erotic_halfNude)
                    illegal_content += getDisplayName("erotic_halfNude") + " ";
                if (theGame.erotic_acts)
                    illegal_content += getDisplayName("erotic_acts") + " ";
                if (theGame.erotic_problems)
                    illegal_content += getDisplayName("erotic_problems") + " ";
                if (theGame.erotic_dance)
                    illegal_content += getDisplayName("erotic_dance") + " ";

                if (theGame.violence_show)
                    illegal_content += getDisplayName("violence_show") + " ";
                if (theGame.violence_extreme)
                    illegal_content += getDisplayName("violence_extreme") + " ";

                if (theGame.drugs)
                    illegal_content += getDisplayName("drugs") + " ";

                if (theGame.Islam_places)
                    illegal_content += getDisplayName("Islam_places") + " ";
                if (theGame.Islam_racist)
                    illegal_content += getDisplayName("Islam_racist") + " ";
                if (theGame.Islam_racistIranian)
                    illegal_content += getDisplayName("Islam_racistIranian") + " ";
                if (theGame.Islam_blasphemy)
                    illegal_content += getDisplayName("Islam_blasphemy") + " ";
                if (theGame.Islam_attitude)
                    illegal_content += getDisplayName("Islam_attitude") + " ";
                if (theGame.Islam_tatto)
                    illegal_content += getDisplayName("Islam_tatto") + " ";


                Games inpGame = new Games
                {
                    age_rating = theGame.age,
                    name = theGame.name,
                    platform = platform,
                    genre1 = theGame.typeOfFiltering,
                    type_of_source = theGame.nickName,
                    CreatedDate = DateTime.Now,
                };
                _context.Add(inpGame);
                await _context.SaveChangesAsync();
                illegal_contents theIC = new illegal_contents
                {
                    game_id = inpGame.id,
                    comment = illegal_content
                };
                _context.Add(theIC);
                await _context.SaveChangesAsync();

                try
                {
                    if (theGame.ImageUpload1.Length > 0)
                    {
                        var path = Path.Combine("wwwroot\\images\\illegals\\" + inpGame.id.ToString());
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        var filepath = path + "\\1.jpg";
                        using (var stream = System.IO.File.Create(filepath))
                        {
                            await theGame.ImageUpload1.CopyToAsync(stream);
                        }
                    }
                }
                catch (Exception exc) { }
                try
                {
                    if (theGame.ImageUpload2.Length > 0)
                    {
                        var path = Path.Combine("wwwroot\\images\\illegals\\" + inpGame.id.ToString());
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        var filepath = path + "\\2.jpg";
                        using (var stream = System.IO.File.Create(filepath))
                        {
                            await theGame.ImageUpload2.CopyToAsync(stream);
                        }
                    }
                }
                catch (Exception exc) { }
                try
                {
                    if (theGame.ImageUpload3.Length > 0)
                    {
                        var path = Path.Combine("wwwroot\\images\\illegals\\" + inpGame.id.ToString());
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        var filepath = path + "\\3.jpg";
                        using (var stream = System.IO.File.Create(filepath))
                        {
                            await theGame.ImageUpload3.CopyToAsync(stream);
                        }
                    }
                }
                catch (Exception exc) { }
                try
                {
                    if (theGame.ImageUpload4.Length > 0)
                    {
                        var path = Path.Combine("wwwroot\\images\\illegals\\" + inpGame.id.ToString());
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        var filepath = path + "\\4.jpg";
                        using (var stream = System.IO.File.Create(filepath))
                        {
                            await theGame.ImageUpload4.CopyToAsync(stream);
                        }
                    }
                }
                catch (Exception exc) { }
                try
                {
                    if (theGame.ImageUpload5.Length > 0)
                    {
                        var path = Path.Combine("wwwroot\\images\\illegals\\" + inpGame.id.ToString());
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        var filepath = path + "\\5.jpg";
                        using (var stream = System.IO.File.Create(filepath))
                        {
                            await theGame.ImageUpload5.CopyToAsync(stream);
                        }
                    }
                }
                catch (Exception exc) { }
                try
                {
                    if (theGame.ImageUpload6.Length > 0)
                    {
                        var path = Path.Combine("wwwroot\\images\\illegals\\" + inpGame.id.ToString());
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        var filepath = path + "\\6.jpg";
                        using (var stream = System.IO.File.Create(filepath))
                        {
                            await theGame.ImageUpload6.CopyToAsync(stream);
                        }
                    }
                }
                catch (Exception exc) { }

                return RedirectToAction(nameof(Index));

            }
            return View(theGame);
        }

        // GET: Games/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var games = await _context.Games.FindAsync(id);
        //    if (games == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(games);
        //}

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("id,code,name,platform,type_of_source,age_rating,genre1,genre2")] Games games)
        //{
        //    if (id != games.id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(games);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!GamesExists(games.id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(games);
        //}

        // GET: Games/Delete/5
        [Authorize(Roles = "ContentManager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var games = await _context.Games
                .FirstOrDefaultAsync(m => m.id == id);
            if (games == null)
            {
                return NotFound();
            }

            return View(games);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ContentManager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var games = await _context.Games.FindAsync(id);
            _context.Games.Remove(games);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GamesExists(int id)
        {
            return _context.Games.Any(e => e.id == id);
        }

        [HttpPost]
        public IActionResult NextPage(GamesIndexViewModel model)
        {

            GamesIndexViewModel a = new GamesIndexViewModel();
            var skipCount = ((model.PageNumber) * 10);
            IEnumerable<Games> query = from m in _context.Games orderby m.id descending select m;
            //var ty = query.ToList();
            a.DataStash = query.Skip(skipCount).Take(10).ToList();
            a.PageNumber = (model.PageNumber + 1);
            a.RecordCount = _context.Games.Count();
            a.pageCount = (a.RecordCount / 10) + 1;
            return View("NextPage", a);
        }
        [HttpPost]
        public IActionResult PreviousPage(GamesIndexViewModel model)
        {
            GamesIndexViewModel a = new GamesIndexViewModel();
            var skipCount = ((model.PageNumber - 2) * 10);
            IEnumerable<Games> query = from m in _context.Games orderby m.id descending select m;
            a.DataStash = query.Skip(skipCount).Take(10).ToList();
            a.PageNumber = (model.PageNumber - 1);
            a.RecordCount = _context.Games.Count();
            a.pageCount = (a.RecordCount / 10) + 1;
            return View("NextPage", a);
        }
    }
}
