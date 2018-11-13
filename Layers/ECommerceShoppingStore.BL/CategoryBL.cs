using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ECommerceShoppingStore.Entities;
using ECommerceShoppingStore.Exceptions;
using ECommerceShoppingStore.DAL;
using System.Configuration;

/// <summary>
/// Author : Dipali P. Mali
/// </summary>

namespace ECommerceShoppingStore.BL
{
    public class CategoryBL
    {
        CategoryDAL dal = null;
        public CategoryBL(string conString)
        {
            dal = new CategoryDAL(ConfigurationManager.ConnectionStrings["EcommStr"].ConnectionString);
        }
        /// <summary>
        /// Validations for Category Data.
        /// </summary>
        /// <param name="newData">A Category</param>
        /// <returns>Boolean ValidateCategory</returns>
        private static bool ValidateCategory(Category newData)
        {
            StringBuilder sb = new StringBuilder();
            bool validData = true;

           

            if (newData.CategoryId.ToString().Equals(string.Empty))
            {
                validData = false;
                sb.Append(Environment.NewLine + "CategoryId cannot be blank.");
            }

            if (newData.CategoryName.ToString().Equals(string.Empty))
            {
                validData = false;
                sb.Append(Environment.NewLine + "CategoryName cannot be blank.");
            }

            if (newData.Description.ToString().Equals(string.Empty))
            {
                validData = false;
                sb.Append(Environment.NewLine + "Description cannot be blank.");
            }

            if (newData.CategoryName.Any(p => char.IsDigit(p)))
            {
                validData = false;
                sb.Append(Environment.NewLine + "CategoryName contains alphabets only.");
            }

            if (validData == false)
                throw new ECommException(sb.ToString());
            return validData;
        }

        

       

        /// <summary>
        /// To Add a Category.
        /// </summary>
        public void Add(Category cat)
        {
            try
            {
                if (ValidateCategory(cat))
                {
                    dal.Insert(cat);
                }
            }
            catch (ECommException Ex) { throw; }
            catch (Exception Ex1) { throw; }
        }

        /// <summary>
        /// To Update a Category.
        /// </summary>
        public void Modify(Category cat)
        {
            try
            {
                dal.Update(cat);
            }
            catch (ECommException Ex) { throw; }
            catch (Exception Ex1) { throw; }
        }

        /// <summary>
        /// To delete a Category.
        /// </summary>
        public void Remove(string catId)
        {
            try
            {
               dal.Delete(catId);
            }
            catch (ECommException Ex) { throw; }
            catch (Exception Ex1) { throw; }
        }

        //public List<Category> DisplayAllCategories()
        //{
        //    List<Category> categories = new List<Category>();
        //    try
        //    {
        //        categories = dal.DisplayAllCategories();
        //    }
        //    catch (ECommException Ex) { throw; }
        //    catch (Exception Ex) { throw; }
        //    return categories;
        //}
    }
}

