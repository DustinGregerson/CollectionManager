using CollectionManager.Models;
using CollectionManager.tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CollectionManager.Controllers
{
    public class ItemController : Controller
    {
        private CollectersContext context;
        public ItemController(CollectersContext ctx)
        {
            context = ctx;
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
        public IActionResult Detailed(String id)
        {
            
            if(id == null)
            {
                return RedirectToAction("Item", "List");
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
        [HttpPost]
        public IActionResult Add() {
            Item item = new Item();
            
                string name = HttpContext.Request.Form["name"];
                string description = HttpContext.Request.Form["description"];
                IFormFile image = HttpContext.Request.Form.Files["imageFile"];
                string tag = HttpContext.Request.Form["tag"];
                string id = HttpContext.Request.Form["id"];
                int error = 0;
                if (name.Equals(""))
                {
                    error += 0x1;
                }
                if (description.Equals(""))
                {
                    error += 0x2;
                }
                if (image==null || image.Length==0)
                {
                    error += 0x4;
                }
                if(tag.Equals(""))
                {
                    error += 0x8;
                }
                if(id.Equals(""))
                {
                    error += 0x10;
                }
            if (error == 0)
            {
                User user = context.users.Find(id);
                if (user != null)
                {
                    item.Description = description;
                    byte[] imageData;
                    using(var stream = new MemoryStream())
                    {
                        image.CopyTo(stream);
                        imageData = stream.ToArray();
                    }
                    item.image= imageData;
                    item.tag = tag;
                    item.Name= name;
                    item.user= user;


                    context.items.Add(item);
                    return View("Detailed",item);
                }
                else
                {
                    return RedirectToAction("login","User");
                }
            }
            else
            {
                return View("Add",error);
            }
        }
    }
}
