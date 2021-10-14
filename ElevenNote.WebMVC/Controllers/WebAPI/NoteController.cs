using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ElevenNote.WebMVC.Controllers.WebAPI
{
    [Authorize]
    [RoutePrefix("api/Note")]
    public class NoteController : ApiController
    {
        private async Task<bool> SetStarStateAsync(int noteId, bool newState)
        {
            // create service
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);

            // get the note
            var detail = await service.GetNoteByIdAysnc(noteId);

            //create the NoteEdit model instance with the new star state
            var updatedNote = new NoteEdit
            {
                NoteId = detail.NoteId,
                Title = detail.Title,
                Content = detail.Content,
                IsStarred = newState
            };

            // return a value indicating whether the update succeeded
            return await service.UpdateNoteAsync(updatedNote);
        }

        [Route("{id}/Star")]
        [HttpPut]
        public async Task<bool> ToggleStarOn(int id) => await SetStarStateAsync(id, true);

        [Route("{id}/Star")]
        [HttpDelete]
        public async Task<bool> ToggleStarOff(int id) => await SetStarStateAsync(id, false);

    }
}
