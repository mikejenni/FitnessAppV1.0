using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Business.Services
{
    // Schnittstelle mit den Methoden die noch konkretisiert werden können. Stellt sicher dass alle Klassen di mit Datenbanken arbeiten diese 5 Methoden haben.
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(int id);

        Task<T> Create(T entity);

        Task<T> Update(int id, T entity);

        Task<bool> Delete(int id);
    }
}
