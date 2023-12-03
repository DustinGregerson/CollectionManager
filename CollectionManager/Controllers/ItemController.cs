using CollectionManager.Models;
using CollectionManager.tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Tokens;
using static System.Net.Mime.MediaTypeNames;

namespace CollectionManager.Controllers
{
    public class ItemController : Controller
    {
        private CollectersContext context;
        public ItemController(CollectersContext ctx)
        {
            context = ctx;
        }
        public IActionResult Delete(string id)
        {
            Item item=context.items.Find(int.Parse(id));
            context.Remove(item);
            context.SaveChanges();
            return RedirectToAction("details", "User");
        }
        public IActionResult List(string filterBy,string value)
        {
            List<ItemsToUsersList> list = new List<ItemsToUsersList>();
            List<string> tags= new List<string>();
            List<string> userNames= new List<string>();
            List<string> itemNames= new List<string>();
            var tagsResult = from item in context.items
                         join user in context.users on item.userID equals user.userID
                         group item by item.tag into groupedItems
                         select new { itemTags = groupedItems.Key};
            foreach(var row in tagsResult)
            {
                tags.Add(row.itemTags);
            }
            var itemNamesResult = from item in context.items
                         join user in context.users on item.userID equals user.userID
                         group item by item.Name into groupedItems
                         select new { itemNames = groupedItems.Key };
            foreach (var row in itemNamesResult)
            {
                itemNames.Add(row.itemNames);
            }
            var userNamesResult = from item in context.items
                              join user in context.users on item.userID equals user.userID
                              group user by user.userName into groupedItems
                              select new { userNames = groupedItems.Key };
            foreach (var row in userNamesResult)
            {
                userNames.Add(row.userNames);
            }
            ViewBag.tags= tags;
            ViewBag.userNames= userNames;
            ViewBag.itemNames= itemNames;
            if (filterBy == null)
            {
                var result1 = from item in context.items
                             join user in context.users on item.userID equals user.userID
                             select new { ItemName = item.Name, ItemDescription = item.Description, ItemTag = item.tag, UserName = user.userName, ItemPic = item.image, ItemId = item.itemID };
                
                foreach (var row in result1)
                {
                    ItemsToUsersList item = new ItemsToUsersList();
                    item.name = row.ItemName;
                    item.description = row.ItemDescription;
                    item.tag = row.ItemTag;
                    item.userName = row.UserName;
                    item.itemId = "" + row.ItemId;
                    item.pic = imageConverter.byteArrayTo64BaseEncode(row.ItemPic);
                    list.Add(item);
                }

                return View(list);
            }
            else
            {
                switch (filterBy)
                {
                    case "name":
                        var result2 = from item in context.items
                                     join user in context.users on item.userID equals user.userID
                                     where item.Name==value
                                     select new { ItemName = item.Name, ItemDescription = item.Description, ItemTag = item.tag, UserName = user.userName, ItemPic = item.image, ItemId = item.itemID };
                        foreach (var row in result2)
                        {
                            ItemsToUsersList item = new ItemsToUsersList();
                            item.name = row.ItemName;
                            item.description = row.ItemDescription;
                            item.tag = row.ItemTag;
                            item.userName = row.UserName;
                            item.itemId = "" + row.ItemId;
                            item.pic = imageConverter.byteArrayTo64BaseEncode(row.ItemPic);
                            list.Add(item);
                        }
                        return View(list);
                    case "tag" :
                        var result3 = from item in context.items
                                     join user in context.users on item.userID equals user.userID
                                     where item.tag == value
                                     select new { ItemName = item.Name, ItemDescription = item.Description, ItemTag = item.tag, UserName = user.userName, ItemPic = item.image, ItemId = item.itemID };
                        foreach (var row in result3)
                        {
                            ItemsToUsersList item = new ItemsToUsersList();
                            item.name = row.ItemName;
                            item.description = row.ItemDescription;
                            item.tag = row.ItemTag;
                            item.userName = row.UserName;
                            item.itemId = "" + row.ItemId;
                            item.pic = imageConverter.byteArrayTo64BaseEncode(row.ItemPic);
                            list.Add(item);
                        }
                        return View(list);
                    case "userName":
                        var result4 = from item in context.items
                                      join user in context.users on item.userID equals user.userID
                                      where user.userName == value
                                      select new { ItemName = item.Name, ItemDescription = item.Description, ItemTag = item.tag, UserName = user.userName, ItemPic = item.image, ItemId = item.itemID };
                        foreach (var row in result4)
                        {
                            ItemsToUsersList item = new ItemsToUsersList();
                            item.name = row.ItemName;
                            item.description = row.ItemDescription;
                            item.tag = row.ItemTag;
                            item.userName = row.UserName;
                            item.itemId = "" + row.ItemId;
                            item.pic = imageConverter.byteArrayTo64BaseEncode(row.ItemPic);
                            list.Add(item);
                        }
                        return View(list);
                        
                    default:
                        var result5 = from item in context.items
                                     join user in context.users on item.userID equals user.userID
                                     select new { ItemName = item.Name, ItemDescription = item.Description, ItemTag = item.tag, UserName = user.userName, ItemPic = item.image, ItemId = item.itemID };

                        foreach (var row in result5)
                        {
                            ItemsToUsersList item = new ItemsToUsersList();
                            item.name = row.ItemName;
                            item.description = row.ItemDescription;
                            item.tag = row.ItemTag;
                            item.userName = row.UserName;
                            item.itemId = "" + row.ItemId;
                            item.pic = imageConverter.byteArrayTo64BaseEncode(row.ItemPic);
                            list.Add(item);
                        }

                        return View(list);
                }
            }
        }
        [HttpGet]
        public IActionResult Detailed(string id)
        {
            string id1 = id;
            if(id == null)
            {
                return RedirectToAction("List", "Item");
            }
            else
            {
                int ID=int.Parse(id);
                var result = from item in context.items
                             join user in context.users on item.userID equals user.userID
                             where item.itemID == ID
                             select new { ItemName = item.Name, ItemDescription = item.Description, ItemTag = item.tag, UserName = user.userName, ItemPic = item.image, ItemId = item.itemID };

                ItemsToUsersList itemsToUsersList = new ItemsToUsersList();
                foreach (var row in result)
                {
                    itemsToUsersList.name = row.ItemName;
                    itemsToUsersList.description = row.ItemDescription;
                    itemsToUsersList.tag = row.ItemTag;
                    itemsToUsersList.userName = row.UserName;
                    itemsToUsersList.itemId = "" + row.ItemId;
                    itemsToUsersList.pic = imageConverter.byteArrayTo64BaseEncode(row.ItemPic);
                }

                return View(itemsToUsersList);
            }
        }
        public IActionResult Add()
        {
            Error error = new Error();
            return View("Add",error);
        }

        [HttpPost]
        public IActionResult AddItem() {
            Item item = new Item();
                
                string itemName = HttpContext.Request.Form["addName"];
                string itemDescription = HttpContext.Request.Form["addDescription"];
                IFormFile itemImage = HttpContext.Request.Form.Files["addPic"];
                string itemTag = HttpContext.Request.Form["addTag"];
                string userId = HttpContext.Session.GetString("id");

                int error = 0;
                if (itemName.Equals(""))
                {
                    error += 0x1;
                }
                if (itemDescription.Equals(""))
                {
                    error += 0x2;
                }
                if (itemImage==null)
                {
                    error += 0x4;
                }
                if(itemTag.Equals(""))
                {
                    error += 0x8;
                }
            if (error == 0&&userId!=null)
            {
                User user = context.users.Find(int.Parse(userId));
                if (user != null)
                {
                    
                    byte[] imageData;
                    MemoryStream memoryStream = new MemoryStream();
                    itemImage.CopyTo(memoryStream);
                    byte[] data = memoryStream.ToArray();
                    item.image = data;
                    item.tag = itemTag;
                    item.Name= itemName;
                    item.user= user;
                    item.Description = itemDescription;
                    item.userID = int.Parse(userId);
                    context.items.Add(item);
                    context.SaveChanges();
                    return RedirectToAction("details", "User");
                }
                else
                {
                    return RedirectToAction("login","User");
                }
            }
            else
            {
                Error errorNum = new Error();
                errorNum.errorNumber= error;
                return View("Add",errorNum);
            }
        }
        //The edit action is used to edit the logged in users item
        [HttpPost]
        public IActionResult Edit(string id)
        {

            string name = HttpContext.Request.Form["editName"];
            string description = HttpContext.Request.Form["editDescription"];
            IFormFile image = HttpContext.Request.Form.Files["editPic"];
            string tag = HttpContext.Request.Form["editTag"];
            int index = int.Parse(id);
            //grabbing the item from the data set
            Item item=context.items.Find(index);
            if (item != null)
            {
                if (!name.IsNullOrEmpty())
                {
                    item.Name = name;
                }
                if (!description.IsNullOrEmpty())
                {
                    item.Description = description;
                }
                if (!tag.IsNullOrEmpty())
                {
                    item.tag = tag;
                }
                if (image != null)
                {
                    MemoryStream memoryStream= new MemoryStream();
                    image.CopyTo(memoryStream);
                    byte[] data = memoryStream.ToArray();
                    item.image = data;
                }
                //since item is a refrance to an item in the data set when save changes is called
                //the ef core will automaticly update the data based to the new values
                context.SaveChanges();
            }

            //this redirects to items detailed. the new {id=id} fills out the id parameter of the Detailed IAction
            //so the user can see the changes they are makeing each time they hit submit
            return RedirectToAction("Detailed", "Item",new { id=id });
        }
    }
}
