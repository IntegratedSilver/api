using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Services.Context;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace api.Services;

public class BlogItemService : ControllerBase
{
    private readonly DataContext _context;

    public BlogItemService(DataContext context)
    {
            _context = context;
    }
    public bool AddBlogItems(BlogItemModel newBlogItem)
    {
        bool result = false;
        _context.Add(newBlogItem);
        result = _context.SaveChanges() != 0;
        return result;
    }

    public bool DeleteBlogItem(BlogItemModel blogDelete)
    {
        throw new NotImplementedException();
    }

     public IEnumerable<BlogItemModel> GetAllBlogItems()
    {
        return _context.BlogInfo;
    }

    public IEnumerable<BlogItemModel> GetItemByCategory(string category)
    {
        return _context.BlogInfo.Where(item => item.Category == category);
    }

    public IEnumerable<BlogItemModel> GetItemsByDate(string date)
    {
        throw new NotImplementedException();
    }

    public List<BlogItemModel> GetItemsByTag(string Tag)
    {
        List<BlogItemModel> AllBlogsWithTag = new List<BlogItemModel>();
        var allItems = GetAllBlogItems().ToList();
        for(int i = 0; i < allItems.Count; i++)
        {
            BlogItemModel Item = allItems[i];
            BlogItemModel item = Item;
            var itemArr = item.Tag.Split(',');
            for(int j = 0; j < itemArr.Length; j++)
            {
                if(itemArr[j].Contains(Tag))
                {
                    AllBlogsWithTag.Add(Item);
                    break;
                }
            }
        }
        return AllBlogsWithTag;

    }

    public bool UpdateBlogItems(BlogItemModel blogUpdate)
    {
        _context.Update<BlogItemModel>(blogUpdate);
            return _context.SaveChanges() !=0;
    }

    public IEnumerable<BlogItemModel> GetItemsByUserId(int userId)
    {
        return _context.BlogInfo.Where(item => item.UserId == userId);
    }


    public IEnumerable<BlogItemModel> GetPublishedItems()
    {
        return _context.BlogInfo.Where(item => item.IsPublished && item.IsDeleted == false);
    }

}