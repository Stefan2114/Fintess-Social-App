using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using SocialApp.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SocialApp.Repository
{
    public class GroceryListRepository : IGroceryListRepository
    {
        private readonly IDataLink dataLink;

        public GroceryListRepository()
        {
            dataLink = DataLink.Instance;
        }

        public GroceryListRepository(IDataLink dataLink)
        {
            this.dataLink = dataLink;
        }

        public async Task<List<GroceryIngredient>> GetIngredientsForUser(long userId)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@u_id", userId),
            };

            var table = await Task.Run(() => this.dataLink.ExecuteReader("GetUserGroceryIngredients", parameters));
            var ingredients = new List<GroceryIngredient>();

            foreach (DataRow row in table.Rows)
            {
                ingredients.Add(new GroceryIngredient
                {
                    Id = Convert.ToInt32(row["IngredientId"]),
                    Name = row["Name"].ToString(),
                    IsChecked = Convert.ToBoolean(row["IsChecked"])
                });
            }

            return ingredients;
        }

        public async Task UpdateIsChecked(long userId, int ingredientId, bool isChecked)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@u_id", userId),
                new SqlParameter("@ingredient_id", ingredientId),
                new SqlParameter("@is_checked", isChecked),
            };

            await Task.Run(() => dataLink.ExecuteNonQuery("UpdateGroceryIngredientIsChecked", parameters));
        }

        public async Task<GroceryIngredient> AddIngredientToUser(long userId, GroceryIngredient ingredient)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@u_id", userId),
                new SqlParameter("@ingredient_name", ingredient.Name),
                new SqlParameter("@is_checked", ingredient.IsChecked)
            };



            var ingredientId = await Task.Run(() =>
                this.dataLink.ExecuteScalar<int>("InsertUserGroceryIngredient", parameters, true));

            ingredient.Id = ingredientId;
            return ingredient;
        }
    }
}