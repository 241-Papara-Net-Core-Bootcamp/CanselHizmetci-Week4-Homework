using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RepositoryPattern.Data.Abstracts;
using RepositoryPattern.Domain;

namespace RepositoryPattern.Data.Concretes
{
    public class DapperRepository<T> : IDapperRepository<T> where T : BaseEntity
    {
        private readonly IConfiguration _conf;

        public DapperRepository(IConfiguration conf)
        {
            _conf = conf;
        }
        /// <summary>
        /// Bu fonksiyon tüm entity leri listeler.
        /// </summary>
        /// <returns>Bütün entity ler</returns>
        public IReadOnlyList<T> GetAll()
        {
            using (var con = new SqlConnection(_conf.GetConnectionString("AppDbContextConStr")))
            {
                con.Open();
                return con.Query<T>(
                        $"Select * FROM {GetTableNameFromEntity()} Where IsDeleted=0")
                    .ToList();
            }
        }
        /// <summary>
        /// Bu fonksiyon ilgili entity i getirir 
        /// </summary>
        /// <param name="id">Getirelecek entity nin Id si</param>
        /// <returns>Seçili Entity</returns>
        public T? Get(int id)
        {
            using (var con = new SqlConnection(_conf.GetConnectionString("AppDbContextConStr")))
            {
                con.Open();
                return con.Query<T>(
                        $"Select * FROM {GetTableNameFromEntity()} Where IsDeleted=0 and Id={id}")
                    .FirstOrDefault();
            }
        }
        /// <summary>
        /// Entity ekleme işlemini yapar.
        /// </summary>
        /// <param name="entity">Eklenecek entity</param>
        public void Add(T entity)
        {
            using (var con = new SqlConnection(_conf.GetConnectionString("AppDbContextConStr")))
            {
                con.Open();
                var str = new StringBuilder();
                //
                str.Append(
                    $"INSERT INTO {GetTableNameFromEntity()} ");
                str.Append(
                    $"({PreparePropertiesForCreateColumns(entity)})");
                str.Append(
                    $" values({PreparePropertiesForCreateValues(entity)})");

                entity.CreateDate = DateTime.Now;
                con.Execute(str.ToString(), entity);
            }
        }
        /// <summary>
        /// Bu fonksiyon ilgili entity için silme işlemlerini yapar(Soft remove).
        /// </summary>
        /// <param name="id">Silinecek entity nin Id si </param>
        public void Remove(int id)
        {
            using (var con = new SqlConnection(_conf.GetConnectionString("AppDbContextConStr")))
            {
                con.Open();
                con.Execute(
                    $"UPDATE {GetTableNameFromEntity()} SET IsDeleted = 1 Where Id={id}");
            }
        }
        /// <summary>
        /// Bu fonksiyon ilgili entity için güncelleme işlemlerini yapar.
        /// </summary>
        /// <param name="id">Güncellenecek entity nin Id si</param>
        /// <param name="entity">Güncellenecek entity</param>
        public void Update(int id, T entity)
        {
            using (var con = new SqlConnection(_conf.GetConnectionString("AppDbContextConStr")))
            {
                con.Open();
                var str = new StringBuilder();
                str.Append(
                    $"UPDATE {GetTableNameFromEntity()} SET ");
                str.Append(PreparePropertiesForUpdate(entity));
                str.Append($" Where Id = {id} ");

                entity.LastUpdateDate = DateTime.Now;
                con.Execute(str.ToString(), entity);

            }
        }
        /// <summary>
        /// Bu fonksiyon BaseEntity den türetilmiş Generic classın tablo adını getirir.
        /// </summary>
        /// <returns>Entity nin tablo adı döner</returns>
        private string GetTableNameFromEntity()
        {
            return ((TableAttribute)typeof(T).GetCustomAttribute(typeof(TableAttribute))).Name;
        }
        /// <summary>
        /// Bu fonksiyon içine aldığı oblenin property lerini döndürür.
        /// </summary>
        /// <param name="type"> Property leri döndürülecek obje</param>
        /// <returns> PropertyInfo listesi döner </returns>
        private PropertyInfo[] GetProperties(object type)
        {
            return type.GetType().GetProperties();
        }
        /// <summary>
        /// Bu fonksiyon update query si için entity nin property lerini ayıklar.
        /// </summary>
        /// <param name="entity">Property leri ayıklanacak entity</param>
        /// <returns>Update query sinin set içeriği döner</returns>
        private string PreparePropertiesForUpdate(T entity)
        {
            return string.Join(", ",GetProperties(entity).Where(c =>
                c.Name != "Id" &&
                c.Name != "CreateDate" &&
                !typeof(BaseEntity).IsAssignableFrom(c.PropertyType) &&
                !typeof(IEnumerable<BaseEntity>).IsAssignableFrom(c.PropertyType)).
                Select(c => $"{c.Name} = @{c.Name}"));
        }
        /// <summary>
        /// Bu fonksiyon insert into query si nin columnları için entity nin property lerini ayıklar.
        /// </summary>
        /// <param name="entity">Property leri ayıklanacak entity</param>
        /// <returns>insert into query sinin column içeriği döner </returns>
        private string PreparePropertiesForCreateColumns(T entity)
        {
            return string.Join(", ",
                GetProperties(entity).Where(c =>
                    c.Name != "Id" && c.Name != "LastUpdateDate" &&
                    !typeof(BaseEntity).IsAssignableFrom(c.PropertyType) &&
                    !typeof(IEnumerable<BaseEntity>).IsAssignableFrom(c.PropertyType)).Select(c => c.Name));
        }
        /// <summary>
        /// Bu fonksiyon insert into query si nin values için entity nin property lerini ayıklar.
        /// </summary>
        /// <param name="entity">Property leri ayıklanacak entity</param>
        /// <returns>insert into query sinin values içeriği döner </returns>
        private string PreparePropertiesForCreateValues(T entity)
        {
            return string.Join(", ",
                GetProperties(entity).Where(c =>
                    c.Name != "Id" && c.Name != "LastUpdateDate" &&
                    !typeof(BaseEntity).IsAssignableFrom(c.PropertyType) &&
                    !typeof(IEnumerable<BaseEntity>).IsAssignableFrom(c.PropertyType)).Select(c =>"@"+ c.Name));
        }
    }
}
