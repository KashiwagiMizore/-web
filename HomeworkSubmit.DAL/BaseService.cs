using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace HomeworkSubmit.DAL
{
    public class BaseService<T> : IDAL.IBaseService<T> where T : Models.BaseEntity, new()
    {
        //在构造函数中传入数据库上下文 只读字段只能在构造函数中赋值
        private readonly Models.HomeworkContext _db;
        public BaseService(Models.HomeworkContext db)
        {
            _db = db;
        }
        public async Task AddAsync(T model, bool saved = true)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;//先关闭自动检查 否则传入的类中若有空值会报错
            _db.Set<T>().Add(model);
            if (saved)
            {
                await _db.SaveChangesAsync();
                _db.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        public void Dispose()
        {
            _db.Dispose();//释放连接
        }

        public async Task EditAsync(T model, bool saved = true)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;//先关闭自动检查
            _db.Entry(model).State = EntityState.Modified;
            if (saved)
            {
                await _db.SaveChangesAsync();
                _db.Configuration.ValidateOnSaveEnabled = true;//重新开启检查
            }
        }

        public async Task FalseRemoveAsync(Guid id, bool saved = true)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;//先关闭自动检查
            var t = new T() { Id = id };
            _db.Entry(t).State = EntityState.Unchanged;
            t.IsRemoved = true;
            if (saved)
            {
                await _db.SaveChangesAsync();
                _db.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        public async Task FalseRemoveAsync(T model, bool saved = true)
        {
            await FalseRemoveAsync(model.Id, saved);
        }

        public IQueryable<T> GetAll()
        {
            return _db.Set<T>().Where(m => m.IsRemoved == false).AsNoTracking();//查询到的结果改为游离态
        }

        public IQueryable<T> GetAllByOrder(bool asc = true)
        {
            var data = GetAll();
            if (asc)
            {
                data = data.OrderBy(m => m.CreateTime);//升序
            }
            else
            {
                data = data.OrderByDescending(m => m.CreateTime);//降序
            }
            return data;
        }

        public IQueryable<T> GetAllByPage(int pageSize = 10, int pageIndex = 0)
        {
            return GetAll().Skip(pageSize * pageIndex).Take(pageSize);
        }

        public IQueryable<T> GetAllByPageOrder(int pageSize = 10, int pageIndex = 0, bool asc = true)
        {
            return GetAllByOrder(asc).Skip(pageSize * pageIndex).Take(pageSize);//先排序再分页
        }

        public async Task<T> GetOneByIdAsync(Guid id)
        {
            return await GetAll().FirstAsync(m => m.Id == id);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
            _db.Configuration.ValidateOnSaveEnabled = true;
        }

        public async Task TrueRemoveAsync(T model, bool saved = true)
        {
            await TrueRemoveAsync(model.Id, saved);

        }

        public async Task TrueRemoveAsync(Guid id, bool saved = true)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;//先关闭自动检查
            var t = new T() { Id = id };
            _db.Entry(t).State = EntityState.Deleted;
            if (saved)
            {
                await _db.SaveChangesAsync();
                _db.Configuration.ValidateOnSaveEnabled = true;
            }
        }
    }
}
