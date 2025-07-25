using ClusterManagement.Models;
using ClusterManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClusterManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        /// <summary>
        /// Henter alle meldinger som har denne id-en
        /// Henter altså ut en spesifik melding
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage(Guid id)
        {
            var message = await _messageService.GetMessageByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }
        /// <summary>
        /// Henter alle meldinger som er "sendt" til denne id-en
        /// Altså, har denne guid-en i feltet "To"
        /// Kan være en Cluster eller en ClusterUser, eller kanskje noe annet
        /// lurt dere lager
        /// </summary>
        /// <param name="id">Guid-en til mottakeren</param>
        /// <returns>En liste med meldinger som er sendt til denne id-en</returns>
        [HttpGet("to/{id}")]
        public async Task<List<Message>> GetMessagesTo(Guid id)
        {
            return await _messageService.GetMessagesToAsync(id);
        }
        /// <summary>
        /// Henter alle meldinger som er "sendt" fra denne id-en
        /// Altså, har denne guid-en i feltet "From"
        /// Kan være en Cluster eller en ClusterUser, eller kanskje noe annet
        /// lurt dere lager
        /// </summary>
        /// <param name="id">Guid-en til avsenderen</param>
        /// <returns>En liste med meldinger som er sendt fra denne id-en</returns>
        [HttpGet("from/{id}")]
        public async Task<List<Message>> GetMessagesFrom(Guid id)
        {
            return await _messageService.GetMessagesFromAsync(id);
        }
        /// <summary>
        /// Sender en melding fra en avsender til en mottaker
        /// Sender en melding med en guid som avsender og en guid som mottaker
        /// Det man vil si i meldingen ligger i felteet "Text"
        /// </summary>
        [HttpPost("send")]
        public async Task SendMessage([FromBody] Message message)
        {

        Message newMessage = new Message(
            from: message.From,
            to: message.To,
            about: message.About,
            content: message.Content
        );
            await _messageService.SendMessageAsync(newMessage);
        }
        /// <summary>
        /// Henter alle meldinger som er sendt til en mottaker som omhandler en spesifikk OneWayOpportunity
        /// </summary>
        [HttpGet("{toId}/{aboutId}")]
        public async Task<List<Message>> GetMessagesToAbout(Guid toId, Guid aboutId)
        {
            return await _messageService.GetMessagesToAboutAsync(toId, aboutId);
        }

    }
public class MessageDto
{
    public Guid FromId { get; set; }
    public Guid ToId { get; set; }
    public Guid AboutId { get; set; }
    public string Content { get; set; }
}
}
