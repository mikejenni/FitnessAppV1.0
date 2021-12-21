using FitnessApp.Business.Models;
using FitnessApp.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppWPF.Stores
{
    public class HistoryStore
    {
        private readonly List<History> _historyItems;
        private readonly IHistoryService _historyService;
        public IEnumerable<History> HistoryItems => _historyItems;
        public event Action<List<History>> HistoryLoaded;
        public event Action<History> HistoryCreated;

        public HistoryStore(IHistoryService historyService)
        {
            _historyService = historyService;
            _historyItems = _historyService.GetAll().Result.ToList();

            HistoryLoaded?.Invoke(_historyItems);
        }

        public void CreateHistoryItem(History history)
        {
            _historyItems?.Add(history);
            _historyService.Create(history);
            HistoryCreated?.Invoke(history);
        }
    }
}
