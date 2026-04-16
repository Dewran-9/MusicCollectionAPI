using MusicCollectionAPI.Models;
using System.Linq;

namespace MusicCollectionAPI.Repositories
{
    public class MusicRecordRepository
    {
        private List<MusicRecord> _records = new List<MusicRecord>
    {
        new MusicRecord { Id = 1, Title = "Blue Lines", Artist = "Massive Attack", Duration = 223, PublicationYear = 1991 },
        new MusicRecord { Id = 2, Title = "Dummy", Artist = "Portishead", Duration = 198, PublicationYear = 1994 },
        new MusicRecord { Id = 3, Title = "Mezzanine", Artist = "Massive Attack", Duration = 310, PublicationYear = 1998 }
    };

        public List<MusicRecord> GetAll()
        {
            return _records;
        }

        public List<MusicRecord> Search(string query)
        {
            return _records.Where(r =>
                r.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                r.Artist.Contains(query, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }


        public MusicRecord Add(MusicRecord record)
        {
            record.Id = _records.Count > 0 ? _records.Max(r => r.Id) + 1 : 1;
            _records.Add(record);
            return record;
        }

        public bool Delete(int id)
        {
            var record = _records.FirstOrDefault(r => r.Id == id);
            if (record == null)
                return false;

            _records.Remove(record);
            return true;
        }

        public MusicRecord Update(int id, MusicRecord updatedRecord)
        {
            var record = _records.FirstOrDefault(r => r.Id == id);
            if (record == null)
                return null;

            record.Title = updatedRecord.Title;
            record.Artist = updatedRecord.Artist;
            record.Duration = updatedRecord.Duration;
            record.PublicationYear = updatedRecord.PublicationYear;
            return record;
        }
    }
}
