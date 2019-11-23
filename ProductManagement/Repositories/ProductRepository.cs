using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ProductManagement.Database;

namespace ProductManagement.Models.Repositories
{
    //This class that handles all communication to the SQL Database
    public class ProductRepository : IProductRepository
    {
        private CultureInfo cultureInfo = new CultureInfo("en-US");
        private readonly string productTableName = "dbo.Product";
        private IDatabaseAccess _databaseAccess;

        public ProductRepository(IDatabaseAccess databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }

        public Product Get(Guid id)
        {
            string sql = $@"SELECT * FROM {productTableName} WHERE Id = '{id}';";
            return _databaseAccess.LoadSingle<Product>(sql);
        }

        // Saves product and returns its id (guid)
        public Product Add(ProductCreateInputModel model)
        {
            if (model.IsValid())
            {
                Product p = new Product() { Id = GetNewGuid().ToString(), Name = model.Name, Price = model.Price };

                string sql = $@"INSERT INTO {productTableName} VALUES ('{p.Id}', '{p.Name}', {p.Price.ToString(cultureInfo)});";
                _databaseAccess.Execute(sql);
                return p;
            }
            return null;
        }

        private Guid GetNewGuid()
        {
            Guid g = Guid.NewGuid();
            var p = Get(g);
            if (p == null)
                return g;
            return GetNewGuid();
        }

        // Updates product with the provided ProductUpdateInputModel
        public Product Update(ProductUpdateInputModel model)
        {
            if (model.IsValid())
            {
                string sql = $@"UPDATE {productTableName} SET Name = '{model.Name}', Price = {model.Price} WHERE Id = '{model.Id}';";
                _databaseAccess.Execute(sql);
                return Get(model.Id);
            }
            return null;
        }

        // Removes product with the given id from the database
        public bool Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return false;
            }

            string sql = $@"DELETE FROM {productTableName} WHERE Id = '{id}';";
            var e = _databaseAccess.Execute(sql);
            return e != 0;
        }

        //Returns all products
        public IEnumerable<Product> Get()
        {
            string sql = $@"SELECT * FROM {productTableName}";
            return _databaseAccess.LoadData<Product>(sql);
        }
    }
}
