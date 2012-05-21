using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LabOne.Cinema.DataAccess.Database;
using LabOne.Cinema.Entities;

namespace LabOne.Cinema.DataAccess.Repository
{
    public class FilmRepository : Repository, ICrudRepository<Film>
    {
        private List<Film> _films;

        public FilmRepository(string path, string fileExtension)
            : base(path, fileExtension)
        {
            _films = GetAll<Film>().ToList();
        }

        public FilmRepository(DataBase database)
            : base(database)
        {
            _films = GetAll<Film>().ToList();
        }

        ~FilmRepository()
        {
            try
            {
                SaveAll(_films);
            }
            catch (Exception e)
            {
                Console.WriteLine("Sorry, your data is not saved.\n{0}\n{1}", e.Message, e.Source);
            }
        }

        public Film Get(string id)
        {
            return _films.FirstOrDefault(elem => elem.ID == id);
        }

        public void Remove(Film item)
        {
            Remove(item.ID);
        }

        public void Remove(string id)
        {
            _films.Remove(Get(id));
        }

        public void Update(Film item)
        {
            var film = new Film
            {
                ID = item.ID,
                Title = item.Title,
                Genre = item.Genre,
                Year = item.Year,
            };
            Remove(item);
            _films.Add(film);
        }

        public void Create(Film item)
        {
            Update(item);
        }

        public IEnumerable<string> GetAllId()
        {
            return _films.Select(elem => elem.ID);
        }

        public IEnumerable<List<string>> GetBaseInfoAboutType()
        {
            return _films.Select(elem => elem.ToString().Split('|').ToList());
        }
    }
}