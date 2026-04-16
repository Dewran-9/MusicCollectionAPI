using Microsoft.EntityFrameworkCore;


public class MusicRecordRepository
{
    private MusicDbContext _context;

    public MusicRecordRepository(MusicDbContext context)
    {
        _context = context;
    }

    public List<MusicRecord> GetAll()
    {
        return _context.MusicRecords.ToList();
    }

    public List<MusicRecord> Search(string query)
    {
        return _context.MusicRecords.Where(r =>
            r.Title.Contains(query) ||
            r.Artist.Contains(query)
        ).ToList();
    }

    public MusicRecord Add(MusicRecord record)
    {
        _context.MusicRecords.Add(record);
        _context.SaveChanges();
        return record;
    }

    public bool Delete(int id)
    {
        var record = _context.MusicRecords.FirstOrDefault(r => r.Id == id);
        if (record == null)
            return false;

        _context.MusicRecords.Remove(record);
        _context.SaveChanges();
        return true;
    }

    public MusicRecord Update(int id, MusicRecord updatedRecord)
    {
        var record = _context.MusicRecords.FirstOrDefault(r => r.Id == id);
        if (record == null)
            return null;

        record.Title = updatedRecord.Title;
        record.Artist = updatedRecord.Artist;
        record.Duration = updatedRecord.Duration;
        record.PublicationYear = updatedRecord.PublicationYear;
        _context.SaveChanges();
        return record;
    }
}