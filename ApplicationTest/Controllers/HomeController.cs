using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Test.Models;

namespace ApplicationTest.Controllers
{
    public class HomeController : Controller
    {
        private CatalogContext db = new CatalogContext();

        // GET: User
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.Profiles = db.Profiles.ToList();
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Login,Profiles")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);             
                db.SaveChanges();
                return RedirectToAction("AddProfilesforUser",user);//добавил юзер в параметр
            }                      
            return View(user);
        }

        //Get: Users/edit
        public ActionResult Edit(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Profiles = db.Profiles.ToList();
            return View(user);
        }

        //Post: User/edit
        [HttpPost]
        public ActionResult Edit(User user, int[] selectedProfiles)
        {
            User newUser = db.Users.Find(user.Id);
            newUser.Name = user.Name;
            newUser.Login= user.Login;
            newUser.Profiles.Clear();
            if (selectedProfiles != null)
            {
                //получаем выбранные профили
                foreach (var c in db.Profiles.Where(co => selectedProfiles.Contains(co.Id)))
                {
                    newUser.Profiles.Add(c);
                }
            }
            db.Entry(newUser).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);            
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Profile
        public ActionResult onProfile()
        {
            return View(db.Profiles.ToList());
        }

        // GET: Profile/Details/5
        public ActionResult DetailsProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // GET: Profile/Create
        public ActionResult CreateProfile()
        {
            //добавил набор ролей
            ViewBag.Roles = db.Roles.ToList();
            return View();
        }

        // POST: Profile/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProfile([Bind(Include = "Id,Name,Roles")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Profiles.Add(profile);
                db.SaveChanges();
                //izmenil redurect
                return RedirectToAction("AddRoleforProfile",profile);
            }
            return PartialView(profile);
        }

        //Get: profile/edit
        public ActionResult EditProfile(int id = 0)
        {
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            ViewBag.Roles = db.Roles.ToList();
            return View(profile);
        }

        //Post: Profile/edit
        [HttpPost]
        public ActionResult EditProfile(Profile profile, int[] selectedRoles)
        {
            Profile newProfile = db.Profiles.Find(profile.Id);
            newProfile.Name = profile.Name;          

            newProfile.Roles.Clear();
            if (selectedRoles != null)
            {
                //получаем выбранные роли
                foreach (var c in db.Roles.Where(co => selectedRoles.Contains(co.Id)))
                {
                    newProfile.Roles.Add(c);
                }
            }
            db.Entry(newProfile).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("onProfile");
        }

        // GET: Profile/Delete/5
        public ActionResult DeleteProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profile/Delete/5
        [HttpPost, ActionName("DeleteProfile")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedProfile(int id)
        {
            Profile profile = db.Profiles.Find(id);
            db.Profiles.Remove(profile);
            db.SaveChanges();           
            return RedirectToAction("onProfile");
        }

        // GET: User/AddProfilesforUser
        [HttpGet]
        public ActionResult AddProfilesforUser(User user, int? id)
        {
            User usernow = db.Users.Find(id);
            ViewBag.Profiles = db.Profiles.ToList();
            return View(usernow);
        }
        
        // Post: User/AddProfilesforUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProfilesforUser(User user, int[] selectedProfilesnew)
        {
            User userprofile = db.Users.Find(user.Id);
            if (selectedProfilesnew != null)
            {
                foreach (var c in db.Profiles.Where(co => selectedProfilesnew.Contains(co.Id)))
                {
                    userprofile.Profiles.Add(c);
                }
            }
            db.Entry(userprofile).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Get: Profile/AddRoleforProfile
        [HttpGet]
        public ActionResult AddRoleforProfile(Profile profile, int? id)
        {
            Profile profilenow = db.Profiles.Find(id);
            ViewBag.Roles = db.Roles.ToList();
            return View(profilenow);
        }

        // Post: Profile/AddRoleforProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRoleforProfile(Profile profile, int[] selectedRolesnew)
        {
            Profile profilerole = db.Profiles.Find(profile.Id);
            if (selectedRolesnew != null)
            {
                foreach (var c in db.Roles.Where(co => selectedRolesnew.Contains(co.Id)))
                {
                    profilerole.Roles.Add(c);
                }
            }
            db.Entry(profilerole).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("onProfile");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
