using DemoAPI.Migrations;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Models
{
    public interface ICategoryController
    {
        public Task<IResult> CreateCategory(Category category);
        public Task<ActionResult> updatedCategory(int id, Category category);
    }
}
