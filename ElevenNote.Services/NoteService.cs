using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class NoteService
    {
        private readonly Guid _userId;

        public NoteService(Guid userId)
        {
            _userId = userId;
        }

        // CREATE
        public async Task<bool> CreateNoteAsync(NoteCreate model)
        {
            var entity = new Note
            {
                OwnerId = _userId,
                Title = model.Title,
                Content = model.Content,
                CreatedUtc = DateTimeOffset.Now
            };

            using (var context = new ApplicationDbContext())
            {
                context.Notes.Add(entity);
                return await context.SaveChangesAsync() == 1;
            }
        }

        // READ
        public async Task<IEnumerable<NoteListItem>> GetNotesAsync()
        {
            using (var context = new ApplicationDbContext())
            {
                var query = context
                    .Notes
                    .Where(e => e.OwnerId == _userId)
                    .Select(e => new NoteListItem
                    {
                        NoteId = e.NoteId,
                        Title = e.Title,
                        CreatedUtc = e.CreatedUtc
                    });

                return await query.ToArrayAsync();
            }
        }
        // READ
        public async Task<NoteDetail> GetNoteByIdAysnc(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity = await context
                    .Notes
                    .SingleAsync(e => e.NoteId == id && e.OwnerId == _userId);

                return new NoteDetail
                {
                    NoteId = entity.NoteId,
                    Title = entity.Title,
                    Content = entity.Content,
                    CreatedUtc = entity.CreatedUtc,
                    ModifiedUtc = entity.ModifiedUtc
                };
            } 
        }

        public async Task<bool> UpdateNoteAsync(NoteEdit model)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity = await context
                    .Notes
                    .SingleAsync(e => e.NoteId == model.NoteId && e.OwnerId == _userId);

                if (model.Title != null)
                {
                    entity.Title = model.Title;
                }
                if (model.Content != null)
                {
                    entity.Content = model.Content;
                }

                return await context.SaveChangesAsync() == 1;
            }
        }
    }
}
