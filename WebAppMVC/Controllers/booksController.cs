using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webApi5.Model;

namespace WebAppMVC.Controllers
{
    public class booksController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:7076/api");
        HttpClient client;

        public booksController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        

        public ActionResult Get()
        {
            List<books> employeeList = new List<books>();

            HttpResponseMessage responseMessage = client.GetAsync(baseAddress + "/books").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                employeeList = JsonConvert.DeserializeObject<List<books>>(data);
            }
            return View(employeeList);

        }
        public ActionResult Index()
        {
            List<books> employeeList = new List<books>();

            HttpResponseMessage responseMessage = client.GetAsync(baseAddress + "/books").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                employeeList = JsonConvert.DeserializeObject<List<books>>(data);
            }
            return View(employeeList);

        }
        #region CREATE METHOD

        public ActionResult Create()
        {
            return View();
        }

        #endregion

        #region POST METHOD

        [HttpPost]
        public ActionResult Create(books book)
        {
            var postTask = client.PostAsJsonAsync<books>(baseAddress + "/Books/", book);
            postTask.Wait();
            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Get");
            }
            ModelState.AddModelError(string.Empty, "Book Create");
            return View(book);
        }

        #endregion

        #region UPDATE METHOD

        public ActionResult Update(books book)

        {
            return View("Update", book);
        }


        public ActionResult UpdateBook(books book)
        {
            var putTask = client.PutAsJsonAsync<books>(baseAddress + "/books/" + book.bookID.ToString(), book);
            putTask.Wait();

            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Get");
            }
            return View(book);
        }

        #endregion

        #region DELETE METHOD

        public ActionResult Delete(int id)
        {

            //HTTP DELETE
            var deleteTask = client.DeleteAsync(baseAddress + "/books/" + id.ToString());
            deleteTask.Wait();

            var result = deleteTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Get");
            }

            return RedirectToAction("Delete");
        }

        #endregion

        #region SEARCH METHOD

        
        public ActionResult Search(string searchString)
        {
            List<books> BookList = new List<books>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/books/" + searchString).Result;
            if (response.IsSuccessStatusCode)
            {
                string book = response.Content.ReadAsStringAsync().Result;
                BookList = JsonConvert.DeserializeObject<List<books>>(book);
            }
            return View("Get", BookList);
        }
        #endregion
        public ActionResult AddToCart()
        {
            return View();
        }
        public ActionResult description()
        {
            return View();
        }





    }




}
