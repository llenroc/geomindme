using System;
using System.Collections.Generic;
using System.Linq;

namespace GeomindMe.Models
{
    public class DesignToDoItemRepository : IToDoItemRepository
    {

        public System.Linq.IQueryable<ToDoItem> All
        {
            get
            {
                return GenerateDesignToDoItemsList(10).AsQueryable<ToDoItem>();
            }
        }

        public System.Linq.IQueryable<ToDoItem> AllIncluding(params System.Linq.Expressions.Expression<Func<ToDoItem, object>>[] includeProperties)
        {
            return All;
        }

        public ToDoItem Find(int id)
        {
            ToDoItem found = GenerateDesignToDoItemsList(2).FirstOrDefault();
            found.ToDoItemId = id;
            return found;
        }

        public void InsertOrUpdate(ToDoItem toDoItem) { }

        public void Delete(int id) { }

        public void Save() { }

        public IList<ToDoItem> GenerateDesignToDoItemsList(int entitiesCount)
        {
            IList<ToDoItem> generatedSource = new List<ToDoItem>();

            for (int i = 1; i < entitiesCount + 1; i++)
            {
                var toDoItem =
                    new ToDoItem
                    {
                        ToDoItemId = i,
                        Text = string.Format("Text {0}", i),
                        //Priority = i,
                        LocationAddress = string.Format("LocationAddress {0}", i),
                        LocationLongitude = i,
                        LocationLatitude = i,
                        ReminderRadius = i,
                        ReminderIsEnabled = true
                    };
                generatedSource.Add(toDoItem);
            }

            return generatedSource;
        }


        public IQueryable<ToDoItem> GetIncompletedToDoItems()
        {
            return GenerateDesignToDoItemsList(10)
                    .AsQueryable<ToDoItem>()
                    .Where(tdi => !tdi.IsCompleted);
        }

        public IQueryable<ToDoItem> GetCompletedToDoItems()
        {
            return GenerateDesignToDoItemsList(10)
                    .AsQueryable<ToDoItem>()
                    .Where(tdi => tdi.IsCompleted);
        }


        public IEnumerable<ToDoItem> GetToDoItemsNearGeoLocation(double latitude, double longitude)
        {
            return GenerateDesignToDoItemsList(10)
                    .AsQueryable<ToDoItem>();                   
        }
    }
}
