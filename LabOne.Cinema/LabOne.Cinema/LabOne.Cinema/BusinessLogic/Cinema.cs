using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LabOne.Cinema.DataAccess.Repository;
using LabOne.Cinema.Entities;

namespace LabOne.Cinema.BusinessLogic
{
    public class Cinema
    {
        private IRepository _iRepository;
        private ICrudRepository<EntityBase> _crudRepository;
        private Repository _repository;

        public Cinema(IRepository iRepository, ICrudRepository<EntityBase> crudRepository)
        {
            _iRepository = iRepository;
            _crudRepository = crudRepository;
        }

        public Cinema(Repository repository)
        {
            _repository = repository;
        }

        public void CanBuyTicket()
        {
            Func<Visitor, Visitor> update = _repository.Update(_repository.DataToObject<T>().Last());
        }
    }
}
