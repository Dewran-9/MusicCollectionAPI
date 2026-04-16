using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



[ApiController]
[Route("[controller]")]
public class MusicRecordsController : ControllerBase
{
    private MusicRecordRepository _repository;

    public MusicRecordsController(MusicRecordRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<MusicRecord>> GetAll()
    {
        return Ok(_repository.GetAll());
    }

    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<List<MusicRecord>> Search([FromQuery] string query)
    {
        if (string.IsNullOrEmpty(query))
            return BadRequest("Søgeord må ikke være tomt");

        return Ok(_repository.Search(query));
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<MusicRecord> Add([FromBody] MusicRecord record)
    {
        if (string.IsNullOrEmpty(record.Title) || string.IsNullOrEmpty(record.Artist))
            return BadRequest("Titel og artist må ikke være tomme");

        var newRecord = _repository.Add(record);
        return Created("MusicRecords/" + newRecord.Id, newRecord);
    }

    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(int id)
    {
        bool deleted = _repository.Delete(id);
        if (!deleted)
            return NotFound("Plade ikke fundet");

        return Ok("Plade slettet");
    }

    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<MusicRecord> Update(int id, [FromBody] MusicRecord updatedRecord)
    {
        var record = _repository.Update(id, updatedRecord);
        if (record == null)
            return NotFound("Plade ikke fundet");

        return Ok(record);
    }
}