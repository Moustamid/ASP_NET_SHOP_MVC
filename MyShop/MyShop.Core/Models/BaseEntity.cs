using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    // class BaseEntity is an abstract Class - it can't be initiated only implemented
    public abstract class BaseEntity
    {
        // The role of our Base Class is to "Fix" and ensure to our InMemoryRepository<T> Generic class ,
        //  that the <T> class will always has Id prop , because T will Inherite from our Base Class .
        public string Id { get; set; }

        // Good Practice , for deging .
        public DateTimeOffset CreatedAt { get; set; }

        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedAt = DateTime.Now;
        }
    }
    
}


