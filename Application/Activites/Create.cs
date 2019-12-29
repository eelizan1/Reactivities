using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activites
{
    public class Create
    {
        // Command object 
        public class Command : IRequest
        {
            public Guid Id {get; set;}
            public string Title {get; set;}
            public string Description {get; set;}
            public string Category {get; set;}
            public DateTime Date {get; set;}
            public string City {get; set;}
            public string Venue {get; set;} 
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context; 
            public Handler(DataContext context)
            {
                _context = context; 
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken) 
            {
               var activity = new Activity
               {
                   Id = request.Id, 
                   Title = request.Title, 
                   Description = request.Description, 
                   Category = request.Category, 
                   Date = request.Date, 
                   City = request.City, 
                   Venue = request.Venue
               };

                _context.Activites.Add(activity);
                // number of changes saved to db so if greter than 0, then something was saved therefore success
                var success = await _context.SaveChangesAsync() > 0; 
                // return 200 ok response 
                if (success) return Unit.Value; 
                // if not successful 
                throw new Exception("Problem saving changes");
            }
        }
    }
}