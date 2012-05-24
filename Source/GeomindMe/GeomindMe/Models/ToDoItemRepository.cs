using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;
using System.Linq.Expressions;

namespace GeomindMe.Models
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        GeomindMeDataContext _context;

        public ToDoItemRepository():
            this(DataProvider.CONNECTION_STRING)
        {}

        public ToDoItemRepository(string connection)
        {
            _context = new GeomindMeDataContext(connection);
            if (!_context.DatabaseExists())
            {
                _context.CreateDatabase();
            }
        }

        public IQueryable<ToDoItem> All
        {
            get { return _context.ToDoItems; }
        }

        public IQueryable<ToDoItem> AllIncluding(params Expression<Func<ToDoItem, object>>[] includeProperties)
        {
            var dataOptions = new System.Data.Linq.DataLoadOptions();
            foreach (var expression in includeProperties)
            {
                 dataOptions.LoadWith<ToDoItem>(expression);
            }
            _context.LoadOptions = dataOptions;
            IQueryable<ToDoItem> query = _context.ToDoItems;
            return query;
        }

        public ToDoItem Find(int id)
        {
            var toDoItem = _context.ToDoItems.Where(x => x.ToDoItemId == id).FirstOrDefault();
            
            return toDoItem;
        }

        public void InsertOrUpdate(ToDoItem toDoItem)
        {
            if (toDoItem.ToDoItemId == default(int))
            {
                _context.ToDoItems.InsertOnSubmit(toDoItem);
            }
            else
            if(_context.ToDoItems.Contains(toDoItem))
            {
                return;
            }
            else
            {
                _context.ToDoItems.Attach(toDoItem, true);
            }
        }

        public void Delete(int id)
        {
            var toDoItem = _context.ToDoItems.Single(x => x.ToDoItemId == id);
            _context.ToDoItems.DeleteOnSubmit(toDoItem);
        }

        public void Save()
        {
            _context.SubmitChanges();
        }



        public IQueryable<ToDoItem> GetIncompletedToDoItems()
        {
            var toDoItems = _context.ToDoItems.Where(tdi => !tdi.IsCompleted);
            return toDoItems;
        }

        public IQueryable<ToDoItem> GetCompletedToDoItems()
        {
            var toDoItems = _context.ToDoItems.Where(tdi => tdi.IsCompleted);
            return toDoItems;
        }
    }

    public interface IToDoItemRepository
    {
        IQueryable<ToDoItem> All { get; }
        IQueryable<ToDoItem> GetIncompletedToDoItems();
        IQueryable<ToDoItem> GetCompletedToDoItems();
        IQueryable<ToDoItem> AllIncluding(params Expression<Func<ToDoItem, object>>[] includeProperties);
        ToDoItem Find(int id);
        void InsertOrUpdate(ToDoItem toDoItem);
        void Delete(int id);
        void Save();
    }
}
