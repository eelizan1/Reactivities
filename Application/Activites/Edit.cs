using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Activites
{
    public class Edit
    {
         // Command object 
                public class Command : IRequest
                {
                     // properties 
                    public Guid Id {get; set;}
                    public string Title {get; set;}
                    public string Description {get; set;}
                    public string Category {get; set;}
                    public DateTime? Date {get; set;}
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
                       // Handler Logic
                       var activity = await _context.Activites.FindAsync(request.Id);

                       if (activity == null)
                            throw new Exception("Could not find activity");

                        // if request.Title then anything on the right of the ?? will execute 
                        activity.Title = request.Title ?? activity.Title; 
                        activity.Description = request.Description ?? activity.Description; 
                        activity.Category = request.Category ?? activity.Category; 
                        activity.Date = request.Date ?? activity.Date; 
                        activity.City = request.City ?? activity.City; 
                        activity.Venue = request.Venue ?? activity.Venue;      
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